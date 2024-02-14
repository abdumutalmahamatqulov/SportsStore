using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Repositories;

public  interface IOrderRepository
{
	Task <Order>Create(Order order);
	Task<Order> Update(Order order);
	Task<Order> Delete(Guid Id);
	Task <Order>Get(Guid Id, bool includeDetail);
	IQueryable<Order> GetAll(bool includeProduct);
}
