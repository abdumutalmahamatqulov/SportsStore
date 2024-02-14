using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Models;

namespace SportsStore.Domain.Services;

public interface IProductService
{
	Task<List<ProductModel>> GetProductsAsync();
	Task Create(ProductCreateModel product);
	Task Delete(Guid Id);
	Task Update(ProductUpdateModel product);
	Task<ProductUpdateModel> GetUpdateModel(Guid id);
}
