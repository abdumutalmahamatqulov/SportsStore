using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Repositories;

public  interface IOrderDetailRepository
{
	Task <OrderDetail>Create(OrderDetail orderDetail);
	Task<OrderDetail> Update(OrderDetail orderDetail);
	Task<OrderDetail> Delete(Guid Id);
	Task<OrderDetail> Get(Guid Id);
	IQueryable<OrderDetail> GetAll(bool includeProduct);
	Task<List<OrderDetail>> GetByOrderId(Guid orderId);
}
