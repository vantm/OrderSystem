using Microsoft.Data.SqlClient;

using SharedLib.Data;
using SharedLib.DependencyInjection;

namespace CatalogService.ApiService.Data;

public class DataServicesInstaller : IServicesInstaller
{
    public void AddServices(IServiceCollection services,
        IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddScoped(CreateConnection);
    }

    private static CreateDbConnection CreateConnection(
        IServiceProvider serviceProvider) =>
        () =>
        {
            var configuration =
                serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString =
                configuration.GetConnectionString("SqlServer");
            return new SqlConnection(connectionString);
        };
}