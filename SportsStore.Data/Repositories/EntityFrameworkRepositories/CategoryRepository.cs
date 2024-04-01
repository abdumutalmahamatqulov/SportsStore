
using Microsoft.EntityFrameworkCore;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Repositories;

namespace SportsStore.Data.Repositories.EntityFrameworkRepositories;

public class CategoryRepository : ICategoryRepository
{
	private readonly AppDbContext _database;

	public CategoryRepository(AppDbContext database)
	{
		_database = database;
	}

	public async Task<Category> Create(Category entity)
	{
		var categoryCreate = new Category();
		categoryCreate.Id = entity.Id;
		categoryCreate.Name = entity.Name;
		categoryCreate.Description = entity.Description;
		categoryCreate.CreateDate = DateTime.Now;
		_database.Categories.Add(categoryCreate);
		await _database.SaveChangesAsync();
		return categoryCreate;
	}

	public async Task<Category> Delete(Guid id)
	{
		var deleteProduct = await _database.Products.Where(x=>x.CategoryId == id).ToListAsync();
		foreach(var product in deleteProduct)
		{
			_database.Products.Remove(product);
		}
		var category = await _database.Categories.FirstOrDefaultAsync(c=>c.Id ==id);
		_database.Categories.Remove(category);
		await _database.SaveChangesAsync();
		return category;
	}

	public async Task<Category> Get(Guid id)
	{
		return await _database.Categories.FirstOrDefaultAsync(c => c.Id==id);
	}

	public IQueryable<Category> GetAll(bool includeCategory)
	{
		return _database.Categories.AsQueryable();
	}

	public async Task<Category> Update(Category entity)
	{
		var categoryCreate = await _database.Categories.FirstOrDefaultAsync(c => c.Id == entity.Id);
		categoryCreate.Name = entity.Name;
		categoryCreate.Description = entity.Description;
		categoryCreate.CreateDate = entity.CreateDate;
		_database.Categories.Update(categoryCreate);
		await _database.SaveChangesAsync();
		return categoryCreate;
	}
}
