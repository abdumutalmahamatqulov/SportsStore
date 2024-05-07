using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportsStore.Data.Repositories.EntityFrameworkRepositories.Configurations;
using SportsStore.Domain.Entities;

namespace SportsStore.Data.Repositories.EntityFrameworkRepositories;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>> 
{
    readonly RoleManager<IdentityRole<Guid>> _roleManager;
	readonly UserManager<User> _userManager;
    public AppDbContext(DbContextOptions<AppDbContext> options
		//, RoleManager<IdentityRole<Guid>> roleManager, UserManager<User> userManager
		) : base(options)
    {
        //_roleManager = roleManager;
        //_userManager = userManager;
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
		//user bn order relashship
		modelBuilder.Entity<Order>().HasOne(u => u.UserName)
			.WithMany()
			.HasForeignKey(u => u.UserId);
		modelBuilder.Entity<User>().HasMany(x => x.Roles)
			.WithOne()
			.HasForeignKey(Roles => Roles.UserId);
		modelBuilder.Entity<UserRole>().HasOne(x=>x.Role)
			.WithMany()
			.HasForeignKey(Roles => Roles.RoleId);
		modelBuilder.Entity<UserRole>().HasKey(x => x.Id);


        modelBuilder.ApplyConfiguration(new RoleConfiguration());
		modelBuilder.ApplyConfiguration(new UserConfiguration());
		modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
	}


}
