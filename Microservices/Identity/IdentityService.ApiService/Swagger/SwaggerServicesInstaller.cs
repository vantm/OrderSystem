using SharedLib.DependencyInjection;

namespace IdentityService.ApiService.Swagger;

public class SwaggerServicesInstaller : IServicesInstaller
{
    public void AddServices(IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}