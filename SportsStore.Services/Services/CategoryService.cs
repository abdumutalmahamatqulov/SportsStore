using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Models;
using SportsStore.Domain.Repositories;
using SportsStore.Domain.Services;

namespace SportsStore.Services.Services;

public class CategoryService : ICategoryService
{
	readonly ICategoryRepository _repository;

	public CategoryService(ICategoryRepository repository)
	{
		_repository = repository;
	}
	public Task<List<CategoryModel>> GetCategoriesAsync()
	{

		CategoryModel x = new Category();
		return Task.FromResult(_repository.GetAll(true).Select(x => (CategoryModel)x).ToList());
	}
	public Task Create(CategoryCreateModel model)
	{
		try
		{

			var newCategory = new Category
			{
				Id = Guid.NewGuid(),
				Name = model.Name,
				Description = model.Description,
				CreateDate = DateTime.Now

			};
			_repository.Create(newCategory);
			return Task.CompletedTask;
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}
	public async Task Update(CategoryUpdateModel model)
	{
		try
		{
			var newCategory = new Category();
			newCategory.Id = model.Id;
			newCategory.Name = model.Name;
			newCategory.Description = model.Description;
			await	_repository.Update(newCategory);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}


	public Task<CategoryUpdateModel> GetUpdateModel(Guid id)
	{
		var category = _repository.GetAll(false).Where(x=>x.Id==id).FirstOrDefault();

		var updatemodel = new CategoryUpdateModel
		{
			Id = category.Id,
			Name = category.Name,
			Description = category.Description
		};
		return Task.FromResult(updatemodel); 
	}
	public Task Delete(Guid id)
	{
		_repository.Delete(id);
		return Task.CompletedTask;
	}
}
