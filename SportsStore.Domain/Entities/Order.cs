using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities.Interfaces;

namespace SportsStore.Domain.Entities;

public class Order : IEntity
{
	public Guid Id { get; set; }

	public OrderStatus Status { get; set; }
	public Guid UserId { get; set; }
	public DateTime CreateDate { get; set; }
	public User UserName { get; set; }
	public List<OrderDetail> OrderDetails { get; set; }
}
