using SportsStore.Domain.Entities;
using SportsStore.Domain.Models;
using SportsStore.Domain.Repositories;
using SportsStore.Domain.Services;

namespace SportsStore.Services.Services;

public class ProductService : IProductService
{
	readonly IProductRepository _repository;
	readonly ICategoryRepository _categoryRepository;

	public ProductService(IProductRepository repository, ICategoryRepository categoryRepository )
	{
		_repository = repository;
		_categoryRepository = categoryRepository;
	}

	public Task<List<ProductModel>> GetProductsAsync()
	{
		return Task.FromResult(_repository.GetAll(true).Select(s => new ProductModel().MapFromEntity(s)).ToList());
	}
	public async  Task Create(ProductCreateModel model)
	{

		try
		{
			Validate(model);
			Category newcategpory= new();

			if (!model.CategoryId.HasValue)
			{
				var categorynew = new Category
				{
					Name = model.CategoryName,
					Description = model.CategoryDescription
				};
				 newcategpory = await  _categoryRepository.Create(categorynew);
			}
			
			//string fileName = Guid.NewGuid().ToString() + model.Img.FileName;
			//var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
			//model.Img.CopyTo(File.Create(path));

			var newProduct = new Product
			{
				Id = Guid.NewGuid(),
				Name = model.Name,
				Description = model.Description,
				Discount = model.Discount,
				Price = model.Price,
				CategoryId = model.CategoryId.HasValue?model.CategoryId.Value: newcategpory.Id,
				CreateDate = DateTime.Now,
				//ImgName = fileName
			};
			await _repository.Create(newProduct);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}
	public Task Delete(Guid Id)
	{
		_repository.Delete(Id);
		return Task.CompletedTask;
	}
	public async Task Update(ProductUpdateModel model)
	{
		try
		{
			Validate(model);
			Category newcategory = new Category();
			if(!model.CategoryId.HasValue)
			{
				var categorynew = new Category
				{
					Name = model.CategoryName,
					Description = model.CategoryDescription
				};
				newcategory = await _categoryRepository.Update(categorynew);
			}

			var newProduct = new Product();
			newProduct.Id = model.Id;
			newProduct.Name = model.Name;
			newProduct.Description = model.Description;
			newProduct.Discount = model.Discount;
			newProduct.Price = model.Price;
			newProduct.CategoryId = model.CategoryId.HasValue?model.CategoryId.Value:newcategory.Id;
			await _repository.Update(newProduct);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

	}
	private void Validate(ProductCreateModel model)
	{
		if (model.Price<=0)
		{
			throw new Exception("Insert valid value to price:");
		}
		if (model.Discount<0 || model.Discount>=100)
		{
			throw new Exception("Discount should be between 0 and 99:");
		}
		
		if (!model.CategoryId.HasValue && (string.IsNullOrEmpty(model.CategoryName )||string.IsNullOrEmpty(model.CategoryDescription))) 
		{
			throw new Exception("Please select category:");
		}
	}
	private void Validate(ProductUpdateModel model)
	{
		if (model.Price <= 0)
		{
			throw new Exception("Insert valid value to price:");
		}
		if (model.Discount < 0 || model.Discount >= 100)
		{
			throw new Exception("Discount should be between 0 and 99:");
		}

		if (!model.CategoryId.HasValue)
		{
			throw new Exception("Please select category:");
		}
	}
	public Task<ProductUpdateModel> GetUpdateModel(Guid id)
	{
		var product =  _repository.GetAll(false).Where(x=>x.Id == id).FirstOrDefault();

		var updatemodel = new ProductUpdateModel
		{
			Id = product.Id,
			Name = product.Name,
			Price = product.Price,
			Discount = product.Discount,
			CategoryId = product.CategoryId,
			Description = product.Description,
		};
		
		return Task.FromResult(updatemodel);

	}

}
