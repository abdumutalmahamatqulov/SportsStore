using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Models;

public class CategoryModel
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public DateTime CreateDate { get; set; }

	public static implicit operator CategoryModel(Category entity)
	{
		if (entity is null)
			return null;
/*		double d = 12.343;
		int i = (int)d;*/
		return new CategoryModel
		{
			Id = entity.Id,
			Name = entity.Name,
			Description = entity.Description,
			CreateDate = entity.CreateDate
		};
	}

}
