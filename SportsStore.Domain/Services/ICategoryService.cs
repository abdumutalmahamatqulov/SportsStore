using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Models;

namespace SportsStore.Domain.Services;

public interface ICategoryService
{
	Task<List<CategoryModel>> GetCategoriesAsync();
	Task Create(CategoryCreateModel model);
	Task Delete(Guid id);
	Task Update(CategoryUpdateModel category);
	Task<CategoryUpdateModel> GetUpdateModel(Guid id);
}
