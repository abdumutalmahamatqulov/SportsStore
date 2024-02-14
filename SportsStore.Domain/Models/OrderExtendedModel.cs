using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Models;

public class OrderExtendedModel : OrderModel
{
	public OrderDetailModel[] OrderDetails { get; set; }

	public override OrderExtendedModel MapFromEntity(Order order)
	{
		base.MapFromEntity(order);
		OrderDetails = order.OrderDetails is not null && order.OrderDetails.Any()?
			order.OrderDetails
			.Select(x => new OrderDetailModel().MapOrderDetailEntity(x)).ToArray()
			: Array.Empty<OrderDetailModel>();
		return this;
	}
}
