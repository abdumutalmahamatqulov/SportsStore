using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Models;

public  class OrderDetailCreateModel
{
	public Guid OrderId { get; set; }
	public Guid ProductId { get; set; }
	public int Count { get; set; }
}
