using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities.Interfaces;

namespace SportsStore.Domain.Entities;

public class Order : IEntity
{
	public Guid Id { get; set; }

	public OrderStatus Status { get; set; }
	public string CustomerName { get; set; }
	public DateTime CreateDate { get; set; }
	public OrderDetail[] OrderDetails { get; set; }
}
