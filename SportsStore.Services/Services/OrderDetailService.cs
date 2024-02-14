using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Models;
using SportsStore.Domain.Repositories;
using SportsStore.Domain.Services;

namespace SportsStore.Services.Services;

public class OrderDetailService : IOrderDetailService
{
	readonly IOrderDetailRepository _orderDetailRepository;

	public OrderDetailService(IOrderDetailRepository orderDetailRepository)
	{
		_orderDetailRepository = orderDetailRepository;
	}

	public Task<List<OrderDetailModel>> GetOrderDetailsAsync()
	{
		return Task.FromResult(_orderDetailRepository.GetAll(true).Select(d =>new OrderDetailModel().MapOrderDetailEntity(d)).ToList());
	}
}
