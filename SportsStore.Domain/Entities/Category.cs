using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using SportsStore.Domain.Entities.Interfaces;
using SportsStore.Domain.Models;

namespace SportsStore.Domain.Entities;

public class Category : IEntity
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public DateTime CreateDate { get; set; }


}
