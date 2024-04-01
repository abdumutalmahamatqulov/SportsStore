using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Repositories;

namespace SportsStore.Data.Repositories.EntityFrameworkRepositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly AppDbContext _appDbContext;

		public ProductRepository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
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
			var deleteProduct = await _appDbContext.Products.FirstOrDefaultAsync(x=>x.Id == Id); 
			_appDbContext.Products.Remove(deleteProduct);
			await _appDbContext.SaveChangesAsync();
			return deleteProduct;
		}

		public Task<Product> Get(Guid Id)
		{
			return _appDbContext.Products.FirstOrDefaultAsync(p => p.Id.Equals(Id));
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
