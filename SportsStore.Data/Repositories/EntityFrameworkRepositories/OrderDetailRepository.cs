using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SportsStore.Data.Repositories.FakeRepositories;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Repositories;

namespace SportsStore.Data.Repositories.EntityFrameworkRepositories;

public class OrderDetailRepository : IOrderDetailRepository
{
	private readonly AppDbContext _appDbContext;

	public OrderDetailRepository(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	public async Task<OrderDetail> Create(OrderDetail orderDetail)
	{
		var createOrderDetail = new OrderDetail();
		createOrderDetail.Id = Guid.NewGuid();
		createOrderDetail.ProductId= orderDetail.ProductId;
		createOrderDetail.OrderId = orderDetail.OrderId;
		createOrderDetail.Count = orderDetail.Count;
		createOrderDetail.CreateDate = DateTime.Now;
		_appDbContext.OrderDetails.Add(createOrderDetail);
		await _appDbContext.SaveChangesAsync();
		return createOrderDetail;
	}

	public async Task<OrderDetail> Delete(Guid Id)
	{
		var DeleteById = _appDbContext.OrderDetails.FirstOrDefault(x => x.Id.Equals(Id));
		_appDbContext.OrderDetails.Remove(DeleteById);
		await _appDbContext.SaveChangesAsync();
		return DeleteById;
	}

	public async Task<OrderDetail> Get(Guid Id)
	{
		return await  _appDbContext.OrderDetails.FirstOrDefaultAsync(x => x.Id.Equals(Id));
	}

	public IQueryable<OrderDetail> GetAll(bool includeProduct)
	{
		if (includeProduct)
		{
			return _appDbContext.OrderDetails.Include(x => x.Product)
				.Include(x => x.Order);
		}
		return _appDbContext.OrderDetails.AsQueryable();
	}

	public async Task<List<OrderDetail>> GetByOrderId(Guid orderId)
	{
		return await _appDbContext.OrderDetails.Include(x => x.Product)
				.Include(x => x.Order)
			.Where(x => x.OrderId == orderId).ToListAsync();
	}

	public async Task<OrderDetail> Update(OrderDetail orderDetail)
	{
		var updateFilter = await _appDbContext.OrderDetails.FirstOrDefaultAsync(x => x.Id == orderDetail.Id);
		if (updateFilter is null)
		{
			throw new Exception($"OrderDetail not found with Id: {orderDetail.Id}");
		}
		updateFilter.OrderId = orderDetail.OrderId;
		updateFilter.ProductId = orderDetail.ProductId;
		updateFilter.Count = orderDetail.Count;
		updateFilter.CreateDate = orderDetail.CreateDate;
		await _appDbContext.SaveChangesAsync();
		return updateFilter;
	}
}
