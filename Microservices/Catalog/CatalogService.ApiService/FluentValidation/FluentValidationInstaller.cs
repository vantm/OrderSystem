using System.Reflection;

using SharedLib.DependencyInjection;

namespace CatalogService.ApiService.FluentValidation;

public class FluentValidationInstaller : IServicesInstaller
{
    public void AddServices(IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        Assembly[] assemblies = new[]
        {
            Assembly.GetEntryAssembly()!, Assembly.GetEntryAssembly()!
        };

        services.AddValidatorsFromAssemblies(assemblies);
    }
}