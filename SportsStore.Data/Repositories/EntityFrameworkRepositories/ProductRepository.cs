using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Repositories;

namespace SportsStore.Data.Repositories.EntityFrameworkRepositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly AppDbContext _appDbContext;
		private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(AppDbContext appDbContext, ILogger<ProductRepository> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<Product> Create(Product product)
		{
			var createProduct = new Product();
			createProduct.Id = Guid.NewGuid();
			createProduct.Name = product.Name;
			createProduct.Description = product.Description;
			createProduct.Price = product.Price;
			createProduct.Discount = product.Discount;
			createProduct.CategoryId = product.CategoryId;
			createProduct.CreateDate = DateTime.Now;
			_appDbContext.Products.Add(createProduct);
			await _appDbContext.SaveChangesAsync();
			return createProduct;
		}

		public async Task<Product> Delete(Guid Id)
		{
			try
			{
                _appDbContext.Products.Remove(new Product { Id = Id });

                await _appDbContext.SaveChangesAsync();

                return null;
            }
			catch(Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return null;
			}
		}

		public async Task<Product> Get(Guid Id)
		{
			return await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id.Equals(Id));
		}

		public  IQueryable<Product> GetAll(bool includeCategory)
		{
			if (includeCategory)
			{
				return  _appDbContext.Products.Include(x => x.Category);
			}
			return _appDbContext.Products.AsQueryable();
		}

		public async Task<Product> Update(Product product)
		{
			var updateProduct = await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
			updateProduct.Name = product.Name;
			updateProduct.Description = product.Description;
			updateProduct.Price = product.Price;
			updateProduct.Discount = product.Discount;
			updateProduct.CategoryId = product.CategoryId;
			updateProduct.CreateDate = product.CreateDate;
			updateProduct.Category = product.Category;
			_appDbContext.Products.Update(updateProduct);
			await _appDbContext.SaveChangesAsync();
			return updateProduct;
		}
	}
}
