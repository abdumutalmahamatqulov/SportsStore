using SportsStore.Domain.Entities;
using SportsStore.Domain.Repositories;

namespace SportsStore.Data.Repositories.FakeRepositories;

public  class OrderRepository : IOrderRepository
{
	readonly IOrderDetailRepository _orderDetailRepository;

	public OrderRepository(IOrderDetailRepository orderDetailRepository)
	{
		_orderDetailRepository = orderDetailRepository;
	}

	public Task<Order> Create(Order order)
	{
		var orderCreate = new Order
		{
			Id = Guid.NewGuid(),
			Status = order.Status,
			CreateDate = DateTime.Now,
			CustomerName = order.CustomerName,
		};
		 DataStore.Orders.Add(orderCreate);
		return Task.FromResult( orderCreate);
	}

	public Task<Order> Delete(Guid Id)
	{
		var ID = DataStore.Orders.FirstOrDefault(x=>x.Id.Equals(Id));
		 DataStore.Orders.Remove(ID);
		return Task.FromResult(ID);
	}

	public Task<Order> Get(Guid Id, bool includeDetail)
	{
		if (includeDetail)
		{
			var order = DataStore.Orders.First(x => x.Id == Id);
			order = new Order
			{
				Id = order.Id,
				Status = order.Status,
				CreateDate = order.CreateDate,
				CustomerName = order.CustomerName,
			/*	OrderDetails = _orderDetailRepository.OrderById(order.Id).ToArray()*/
			};
			return Task.FromResult(order);
		}
		return Task.FromResult(DataStore.Orders.FirstOrDefault(x=>x.Id.Equals(Id)));
	}

	public IQueryable<Order> GetAll(bool includeProduct)
	{
		return DataStore.Orders.AsQueryable();
	}

	public Task<Order> Update(Order order)
	{
		var UpdateOrder = DataStore.Orders.FirstOrDefault(x => x.Id==order.Id);
		if (UpdateOrder is null)
		{
			throw new Exception($"Order not found with Id: {order.Id}");
		}
		UpdateOrder.Status = order.Status;
		UpdateOrder.CreateDate = order.CreateDate;
		UpdateOrder.CustomerName = order.CustomerName;
		return Task.FromResult(UpdateOrder);
	}
}
