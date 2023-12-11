using SharedLib.DependencyInjection;

namespace OrderService.ApiService.Swagger;

public class SwaggerModule : IServicesInstaller
{
    public void AddServices(IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}