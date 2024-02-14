using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities.Interfaces;

namespace SportsStore.Domain.Entities;

public class Product : IEntity, ICloneable
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public int Discount { get; set; }
	public Guid CategoryId { get; set; }
	public DateTime CreateDate { get; set; }
	public Category Category { get; set; }

	public object Clone()
	{
		return this.MemberwiseClone();
	}
}
