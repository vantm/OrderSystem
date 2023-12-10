using IdentityService.ApiService.IdentityServer.Services;

using SharedLib.DependencyInjection;

namespace IdentityService.ApiService.IdentityServer;

public class IdentityServerServicesInstaller : IServicesInstaller
{
    public void AddServices(IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.AddIdentityServer()
            .AddProfileService<UserProfileService>();
    }
}