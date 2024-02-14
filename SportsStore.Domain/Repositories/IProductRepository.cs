using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Repositories;

public  interface IProductRepository
{
	Task<Product> Create(Product product);
	Task <Product>Update(Product product);
	Task<Product> Delete(Guid Id);
	Task<Product> Get(Guid Id);
	IQueryable<Product> GetAll(bool includeCategory);
}
