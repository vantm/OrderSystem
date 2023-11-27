using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SharedLib.DependencyInjection;

namespace SharedLib.Swagger;

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