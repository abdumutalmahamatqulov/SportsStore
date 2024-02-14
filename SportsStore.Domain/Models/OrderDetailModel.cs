using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Models;

public  class OrderDetailModel
{
	public Guid OrderId { get; set; }
	public Guid ProductId { get; set; }
	public int Count { get; set; }
	public DateTime CreateDate { get; set; }
	public ProductModel Product { get; set; }
	public OrderModel Order { get; set; }
	public OrderDetailModel MapOrderDetailEntity(OrderDetail entity)
	{
		OrderId = entity.OrderId;
		ProductId = entity.ProductId;
		Count = entity.Count;
		CreateDate = entity.CreateDate;
		Product = entity.Product is null ? null : new ProductModel().MapFromEntity(entity.Product);
		Order = entity.Order is null ?null : new OrderModel().MapFromEntity(entity.Order);
		return this;
	}
}
