using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsStore.Domain.Entities;

namespace SportsStore.Data.Repositories.EntityFrameworkRepositories;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{

	}
	public DbSet<Product> Products { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderDetail> OrderDetails { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Product>().HasOne(p => p.Category)
			.WithMany()
			.HasForeignKey(p => p.CategoryId);

		modelBuilder.Entity<OrderDetail>().HasOne(p => p.Product)
			.WithMany()
			.HasForeignKey(o => o.ProductId);

		modelBuilder.Entity<OrderDetail>().HasOne(o => o.Order)
			.WithMany(o => o.OrderDetails)
			.HasForeignKey(o => o.OrderId);

		modelBuilder.Entity<Product>().Property(p => p.ImgName)
			.HasDefaultValue(null)
			.IsRequired(false)
			.HasMaxLength(250);
	}


}
