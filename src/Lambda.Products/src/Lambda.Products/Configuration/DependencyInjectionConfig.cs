using Lambda.Products.Data.Repositories;

namespace Auth.API.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddDependencyConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IProductRepository, ProductRepository>();

        return services;
    }
}