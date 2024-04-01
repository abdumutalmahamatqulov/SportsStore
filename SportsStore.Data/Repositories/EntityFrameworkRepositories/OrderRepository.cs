using Microsoft.EntityFrameworkCore;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Repositories;

namespace SportsStore.Data.Repositories.EntityFrameworkRepositories;

public class OrderRepository : IOrderRepository
{
	private readonly AppDbContext _appDbContext;

	public OrderRepository(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	public async Task<Order> Create(Order order)
	{
		var orderCreate = new Order
		{
			Id = Guid.NewGuid(),
			Status = order.Status,
			CreateDate = DateTime.Now,
			CustomerName = order.CustomerName,
		};
		_appDbContext.Orders.Add(orderCreate);
		await _appDbContext.SaveChangesAsync();
		return orderCreate;
	}

	public async Task<Order> Delete(Guid Id)
	{
		var ID = _appDbContext.Orders.FirstOrDefault(x => x.Id.Equals(Id));
		_appDbContext.Orders.Remove(ID);
		await _appDbContext.SaveChangesAsync();
		return ID;
	}

	public async Task<Order> Get(Guid Id, bool includeDetail)
	{
		if (includeDetail)
		{
			var order = _appDbContext.Orders
				.Include(o => o.OrderDetails)
				.ThenInclude(x => x.Product)
				
				.First(x => x.Id == Id);
			return order;
		}
		return await  _appDbContext.Orders.FirstOrDefaultAsync(x => x.Id.Equals(Id));
	}

	public IQueryable<Order> GetAll(bool includeProduct)
	{
		return _appDbContext.Orders.AsQueryable();
	}

	public async Task<Order> Update(Order order)
	{
		var UpdateOrder = await _appDbContext.Orders.FirstOrDefaultAsync(x => x.Id == order.Id);
		if (UpdateOrder is null)
		{
			throw new Exception($"Order not found with Id: {order.Id}");
		}
		UpdateOrder.Status = order.Status;
		UpdateOrder.CreateDate = order.CreateDate;
		UpdateOrder.CustomerName = order.CustomerName;
		return UpdateOrder;
	}
}
