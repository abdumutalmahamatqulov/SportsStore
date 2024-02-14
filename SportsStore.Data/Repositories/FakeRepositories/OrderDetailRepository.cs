using SportsStore.Domain.Entities;
using SportsStore.Domain.Models;
using SportsStore.Domain.Repositories;

namespace SportsStore.Data.Repositories.FakeRepositories;

public  class OrderDetailRepository : IOrderDetailRepository
{
	public  Task<OrderDetail> Create(OrderDetail orderDetail)
	{

		Validate(orderDetail);
		var createOrderdetail = new OrderDetail
		{
			Id = Guid.NewGuid(),
			ProductId = orderDetail.ProductId,
			OrderId = orderDetail.OrderId,
			Count = orderDetail.Count,
			CreateDate = DateTime.Now,
		};
		 DataStore.OrderDetails.Add(createOrderdetail);
		return Task.FromResult(createOrderdetail);
	}

	public Task<OrderDetail> Delete(Guid Id)
	{
		var DeleteById = DataStore.OrderDetails.FirstOrDefault(x => x.Id.Equals(Id));
		DataStore.OrderDetails.Remove(DeleteById);
		return Task.FromResult(DeleteById);
	}

	public Task<OrderDetail> Get(Guid Id)
	{

		return Task.FromResult(DataStore.OrderDetails.FirstOrDefault(x =>x.Id.Equals(Id)));
	}

	public IQueryable<OrderDetail> GetAll(bool includeProduct)
	{
		if (includeProduct)
		{
			return DataStore.OrderDetails.AsQueryable().Select(x => new OrderDetail
			{
				Id = x.Id,
				ProductId = x.ProductId,
				OrderId = x.OrderId,
				Count = x.Count,
				CreateDate = x.CreateDate,
				Product = DataStore.Products.First(p=>p.Id == x.ProductId),
				Order = DataStore.Orders.First(o=>o.Id==x.OrderId)
			});
		}
		return DataStore.OrderDetails.AsQueryable();

	}
	public Task<List<OrderDetail>> GetByOrderId(Guid orderId)
	{
		return Task.FromResult( DataStore.OrderDetails.AsQueryable()
			.Where(x => x.OrderId == orderId)
			.Select(x=> new OrderDetail()
		{
			Id=x.Id,
			ProductId=x.ProductId,
			OrderId=x.OrderId,
			Count = x.Count,
			CreateDate = x.CreateDate,
			Product = DataStore.Products.First(p => p.Id == x.ProductId),
			Order = DataStore.Orders.First(o => o.Id == x.OrderId)
		}).ToList());
	}

	public Task<OrderDetail> Update(OrderDetail orderDetail)
	{
		var UpdateFilter = DataStore.OrderDetails.FirstOrDefault(x=>x.Id==orderDetail.Id);
		if (UpdateFilter is null)
		{
			throw new Exception($"OrderDetail not found with Id: {orderDetail.Id}");
		}
		Validate(orderDetail);
		UpdateFilter.OrderId = orderDetail.OrderId;
		UpdateFilter.ProductId = orderDetail.ProductId;
		UpdateFilter.Count = orderDetail.Count;
		UpdateFilter.CreateDate = orderDetail.CreateDate;
		return Task.FromResult(UpdateFilter);
	}

	private void Validate(OrderDetail entity)
	{

		if (!DataStore.Products.Any(x => x.Id == entity.ProductId))
		{
			throw new Exception($"CategoryId not found : {entity.ProductId}");
		}
		if (!DataStore.Orders.Any(x => x.Id == entity.OrderId))
		{
			throw new Exception($"OrderId not found : {entity.OrderId}");
		}
	}
}
