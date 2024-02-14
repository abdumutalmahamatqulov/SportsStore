using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Domain.Services;

namespace SportsStore.Services.Services;

public static class Extensions
{
	public static IServiceCollection AddServices(this  IServiceCollection services)
	{
		services.AddScoped<IProductService, ProductService>();
		services.AddScoped<ICategoryService, CategoryService>();
		services.AddScoped<IOrderService, OrderService>();
		services.AddScoped<IOrderDetailService, OrderDetailService>();
		return services;
	}
}
