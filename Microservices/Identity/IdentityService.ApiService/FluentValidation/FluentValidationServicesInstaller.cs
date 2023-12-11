using System.Reflection;

using FluentValidation;

using SharedLib.DependencyInjection;

namespace IdentityService.ApiService.FluentValidation;

public class FluentValidationServicesInstaller : IServicesInstaller
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