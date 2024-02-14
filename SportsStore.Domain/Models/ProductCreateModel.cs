using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Models;

public class ProductCreateModel
{
	[Required]
	public string Name { get; set; }
	[Required]
	public string Description { get; set; }
	[Required]
	public decimal Price { get; set; }
	public int Discount { get; set; }
	[AllowNull]
	public Guid? CategoryId { get; set; }
	[AllowNull]
	public string? CategoryName { get; set; }
	[AllowNull]
	public string? CategoryDescription { get; set; }
}
