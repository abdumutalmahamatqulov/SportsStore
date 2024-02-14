using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Repositories
{
	public interface ICategoryRepository
	{
		Task<Category> Create(Category entity);
		Task<Category> Update(Category entity);

		Task<Category> Delete(Guid id);
		Task<Category> Get(Guid id);
		IQueryable<Category> GetAll(bool includeCategory);
	}
}
