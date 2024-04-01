using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using SportsStore.Data.Repositories.EntityFrameworkRepositories;
using SportsStore.Domain.Repositories;

namespace SportsStore.Data.Repositories.AdoNetRepositories;

public static class Extensions
{
	public static IServiceCollection AddAdoNetRepositories(this IServiceCollection services)
	{
		services.AddScoped<ICategoryRepository, CategoryRepository>();
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
		services.AddScoped<IOrderRepository, OrderRepository>();
		services.AddSqlOptions();
		return services;
	}


	/*
	 * {
				"ConnectionString": "Server=(LOCALDB)\\\\MSSQLLocaldb;Database=AdventureWorks2019;User Id=client;Password=web123$"
			}
	 * */

	public static IServiceCollection AddSqlOptions(this IServiceCollection services)
	{


		var provider = services.BuildServiceProvider();

		var configuration = provider.GetRequiredService<IConfiguration>();

		var option = configuration.GetSection("ConnectionStrings").GetSection("Sql")
			.Bind<SqlConnectionOptions>();

		services.AddSingleton(option);

		services.AddScoped<DatabaseConnection>();
		return services;
	}


	public static T Bind<T>(this IConfigurationSection section)
	{
		T option = Activator.CreateInstance<T>();
		var properties = typeof(T).GetProperties();
		foreach (var property in properties)
		{
			var value = section[property.Name];
			property.SetValue(option, value);
		}

		return option;
	}
}