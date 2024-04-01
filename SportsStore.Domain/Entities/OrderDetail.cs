using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities.Interfaces;

namespace SportsStore.Domain.Entities;

public class OrderDetail:IEntity
{
	public Guid Id { get; set; }
	public Guid OrderId { get; set; }
	public Guid ProductId { get; set; }
	public int? Count { get; set; }
	public DateTime CreateDate { get; set; }
	public Product Product { get; set; }
	public Order Order { get; set; }
}
