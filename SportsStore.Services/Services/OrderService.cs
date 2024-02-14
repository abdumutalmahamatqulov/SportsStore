using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Models;
using SportsStore.Domain.Repositories;
using SportsStore.Domain.Services;

namespace SportsStore.Services.Services;

public class OrderService : IOrderService
{
    readonly IOrderRepository _orderRepository;
    readonly IOrderDetailRepository _orderDetailRepository;
	public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
	{
		_orderRepository = orderRepository;
		_orderDetailRepository = orderDetailRepository;
	}

	public Task<List<OrderModel>> GetOrdersAsync()
    {
        return Task.FromResult(_orderRepository.GetAll(true)
			.Select(o => new OrderModel().MapFromEntity(o)).ToList());
    }
    public Task<List<OrderDetailModel>> GetOrderDetailsAsync()
    {
        return Task.FromResult(_orderDetailRepository.GetAll(true)
			.Select(d => new OrderDetailModel()
			.MapOrderDetailEntity(d)).ToList());
    }
	public async Task Create(List<Guid> IdList, string customerName)
	{
		try
		{

			var newOrder = new Order
			{
				Status = OrderStatus.Created,
				CustomerName = customerName
			};
			newOrder  = await _orderRepository.Create(newOrder);
			foreach (var id in IdList)
			{
				var orderDetailnew = new OrderDetail
				{
					OrderId = newOrder.Id,
					ProductId = id,
					Count = 1
				};
				await _orderDetailRepository.Create(orderDetailnew);
			}
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}
	public async Task<OrderExtendedModel> GetByOrderId(Guid id)
	{
		return new OrderExtendedModel().MapFromEntity(await _orderRepository.Get(id, true));
	} 

}
