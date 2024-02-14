using SportsStore.Domain.Entities;
using SportsStore.Domain.Repositories;

namespace SportsStore.Data.Repositories.FakeRepositories;

public class CategoryRepository : ICategoryRepository
{
	readonly IProductRepository _repository;

	public CategoryRepository(IProductRepository repository) 
		=> _repository = repository;

	public async Task<Category> Create(Category entity)
	{
		var createproduct = new Category
		{
			Id = Guid.NewGuid(),
			Name = entity.Name,
			Description = entity.Description,
			CreateDate = DateTime.Now,
		};
		DataStore.Categories.Add(createproduct);
		return createproduct;
	}

	public Task<Category> Delete(Guid id)
	{
		var products = DataStore.Products.Where(x=>x.CategoryId == id).ToList();

		foreach(var product in products)
		{
			DataStore.Products.Remove(product);
		}

		var category = DataStore.Categories.FirstOrDefault(x =>x.Id.Equals(id));
		DataStore.Categories.Remove(category);
		return Task.FromResult(category);
		
	}

	public Task<Category> Get(Guid id)
	{
		return Task.FromResult(DataStore.Categories.FirstOrDefault(x=>x.Id.Equals(id)));
	}

	public IQueryable<Category> GetAll(bool includecategory)
	{
		return DataStore.Categories.AsQueryable();
	}

	public Task<Category> Update(Category entity)
	{
		var updatefilter = DataStore.Categories.FirstOrDefault(c =>c.Id == entity.Id);
		if (updatefilter is null)
		{
			throw new Exception($"Category not found with Id: {entity.Id}");
		}
		updatefilter.Name = entity.Name;
		updatefilter.Description = entity.Description;
		updatefilter.CreateDate = entity.CreateDate;
		return Task.FromResult(updatefilter);

	}
}
