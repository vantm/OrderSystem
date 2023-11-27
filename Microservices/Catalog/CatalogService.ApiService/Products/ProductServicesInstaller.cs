using CatalogService.ApiService.Products.Data;
using CatalogService.ApiService.Products.Domain;

using SharedLib.DependencyInjection;

namespace CatalogService.ApiService.Products;

public sealed class ProductServicesInstaller : IServicesInstaller
{
    public void AddServices(IServiceCollection services,
        IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddScoped<IProductRepo, ProductRepo>();
    }
}