using System.Reflection;

using SharedLib.DependencyInjection;

namespace CatalogService.ApiService.MediatR;

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