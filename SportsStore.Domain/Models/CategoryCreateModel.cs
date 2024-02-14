using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Models;

public  class CategoryCreateModel
{
	[Required]
	public string Name { get; set; }
	[Required]
	public string Description { get; set; }
}
