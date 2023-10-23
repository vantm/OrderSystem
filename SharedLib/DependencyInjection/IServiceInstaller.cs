using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SharedLib.DependencyInjection;

public interface IServiceInstaller
{
    void AddServices(
        IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment);
}