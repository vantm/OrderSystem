using SharedLib.DependencyInjection;

namespace CatalogService.ApiService.HttpLogging;

public sealed class Installer : IServicesInstaller
{
    public void AddServices(IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.AddHttpLogging(c =>
        {
        });
    }
}