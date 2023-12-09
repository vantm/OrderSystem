using System.Reflection;

using FluentValidation;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SharedLib.DependencyInjection;

namespace SharedLib.FluentValidation;

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