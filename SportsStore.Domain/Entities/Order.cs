using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities.Interfaces;

namespace SportsStore.Domain.Entities;

public class Order : IEntity
{
	public Guid Id { get; set; }

	public OrderStatus Status { get; set; }
	[MaxLength(100)]
	public string CustomerName { get; set; }
	public DateTime CreateDate { get; set; }
	public List<OrderDetail> OrderDetails { get; set; }
}
