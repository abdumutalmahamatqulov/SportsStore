using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Domain.Repositories;

namespace SportsStore.Data.Repositories.FakeRepositories;

public static class Extensions
{
    public static IServiceCollection AddFakeRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository,CategoryRepository>();
		services.AddScoped<IOrderDetailRepository,OrderDetailRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository,OrderRepository>();

        return services;
	}
}
