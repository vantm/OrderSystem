using System.Reflection;

using SharedLib.DependencyInjection;

namespace IdentityService.ApiService.MediatR;

public class MediatRServicesInstaller : IServicesInstaller
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
                Assembly.GetEntryAssembly()!,
            };

            c.RegisterServicesFromAssemblies(assemblies);

            c.AddOpenBehavior(typeof(ValidateRequestPipelineBehavior<,>));
        });
    }
}