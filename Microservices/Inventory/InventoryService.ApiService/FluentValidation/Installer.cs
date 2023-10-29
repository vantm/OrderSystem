using System.Reflection;

using FluentValidation;

using SharedLib.DependencyInjection;

namespace InventoryService.ApiService.FluentValidation;

public class Installer : IServiceInstaller
{
    public void AddServices(IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.AddValidatorsFromAssembly(Assembly.GetEntryAssembly()!);
    }
}