using Microsoft.Data.SqlClient;

using SharedLib.Data;
using SharedLib.DependencyInjection;

namespace OrderService.ApiService.Data;

public class Installer : IServiceInstaller
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