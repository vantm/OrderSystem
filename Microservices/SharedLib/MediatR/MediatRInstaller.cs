using System.Reflection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SharedLib.DependencyInjection;

namespace SharedLib.MediatR;

public class MediatRInstaller : IServicesInstaller
{
    public void AddServices(
        IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.AddMediatR(c =>
        {
            Assembly[] assemblies = new[]
            {
                Assembly.GetEntryAssembly()!, Assembly.GetEntryAssembly()!
            };

            c.RegisterServicesFromAssemblies(assemblies);

            c.AddOpenBehavior(typeof(ValidateRequestPipelineBehavior<,>));
        });
    }
}