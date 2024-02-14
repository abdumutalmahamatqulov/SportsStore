using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Models;

public class ProductModel
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public int Discount { get; set; }
	public Guid CategoryId { get; set; }
	public DateTime CreateDate { get; set; }
	public decimal DiscountedPrice => Price - (Price / 100) * Discount; 
	public bool HasDiscount => Discount > 0;
	public CategoryModel Category { get; set; }

	public ProductModel MapFromEntity(Product entity)
	{
		Id = entity.Id; 
		Name = entity.Name; 
		Description = entity.Description;
		Price = entity.Price;
		Discount = entity.Discount;
		CategoryId = entity.CategoryId;
		CreateDate = entity.CreateDate;
		Category = entity.Category is null ? null : entity.Category;
		return this;
	}
}
