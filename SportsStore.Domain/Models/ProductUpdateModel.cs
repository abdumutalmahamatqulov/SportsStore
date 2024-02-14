using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Models;

public class ProductUpdateModel
{
	public Guid Id { get; set; }
	[Required]
	public string Name { get; set; }
	[Required]
	public string Description { get; set; }
	[Required]
	public decimal Price { get; set; }
	public int Discount { get; set; }
	[Required]
	public Guid? CategoryId { get; set; }
	public string? CategoryName { get; set; }
	[AllowNull]
	public string? CategoryDescription { get; set; }
}
