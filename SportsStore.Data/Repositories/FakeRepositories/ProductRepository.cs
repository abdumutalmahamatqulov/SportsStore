
using SportsStore.Domain.Entities;
using SportsStore.Domain.Repositories;

namespace SportsStore.Data.Repositories.FakeRepositories;

public class ProductRepository : IProductRepository
{

	public  Task<Product> Create(Product product)
	{
		Validate(product);
		var createproduct =  new Product 
		{
			Id = Guid.NewGuid(), 
			Name = product.Name,
			Price = product.Price,
			Discount = product.Discount,
			Description = product.Description,
			CreateDate = product.CreateDate,
			CategoryId = product.CategoryId,
			
		};
		DataStore.Products.Add(createproduct);
		return Task.FromResult(createproduct);
	}

	public Task<Product> Delete(Guid Id)
	{
		var ID = DataStore.Products.FirstOrDefault(x=>x.Id.Equals(Id));
		 DataStore.Products.Remove(ID);
		return Task.FromResult(ID);
	}

	public Task<Product> Get(Guid Id)
	{
		return Task.FromResult(DataStore.Products.FirstOrDefault(p => p.Id.Equals(Id)));
	}

	public IQueryable<Product> GetAll(bool includeCategory)
	{
		if (includeCategory)
		{
			return DataStore.Products.AsQueryable().Select(x => new Product
			{
				Id = x.Id,
				Name = x.Name,
				Price = x.Price,
				Discount = x.Discount,
				Description = x.Description,
				CreateDate = x.CreateDate,
				CategoryId = x.CategoryId,
				Category = DataStore.Categories.First(s => s.Id == x.CategoryId)
			}) ;
		}
		return DataStore.Products.AsQueryable();
	}

	public Task<Product> Update(Product product)
	{
		var updateProduct = DataStore.Products.FirstOrDefault(p => p.Id == product.Id);
		if (updateProduct is null)
		{
			throw new Exception($"Product not found with Id: {product.Id}");
		}

		Validate(product);
		updateProduct.Name = product.Name;
		updateProduct.Price = product.Price;
		updateProduct.Discount = product.Discount;
		updateProduct.Description = product.Description;
		updateProduct.CategoryId = product.CategoryId;

		return Task.FromResult(updateProduct);
	}
	private void Validate(Product product)
	{
		if (!DataStore.Categories.Any(x => x.Id == product.CategoryId))
		{
			throw new Exception($"CategoryId not found : {product.CategoryId}");
		}
	}
}
