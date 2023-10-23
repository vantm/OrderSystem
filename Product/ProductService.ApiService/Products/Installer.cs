using SharedLib.DependencyInjection;
using SharedLib.Domain;

namespace ProductService.ApiService.Products;

public sealed class Installer : IServiceInstaller
{
    public void AddServices(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddScoped<IRepo<Product, Guid>, ProductRepo>();
    }
}