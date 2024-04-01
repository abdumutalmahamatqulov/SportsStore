using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Data.Repositories.AdoNetRepositories;
using SportsStore.Domain.Repositories;

namespace SportsStore.Data.Repositories.EntityFrameworkRepositories;

public static class Extensions
{
	public static IServiceCollection AddEntityFrameworkRepositories(this IServiceCollection services)
	{

		var provider = services.BuildServiceProvider();

		var configuration = provider.GetRequiredService<IConfiguration>();

		var sqlOption = configuration.GetSection("ConnectionStrings").GetSection("Sql")
			.Bind<SqlConnectionOptions>();

		services.AddDbContext<AppDbContext>(options =>
		{
			options.UseSqlServer(sqlOption.ConnectionString,
				o=> o.MigrationsAssembly("SportsStore"));
			
			
		});

		services.AddScoped<ICategoryRepository, CategoryRepository>();
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
		services.AddScoped<IOrderRepository, OrderRepository>();
		services.AddSqlOptions();
		return services;
	}
}
