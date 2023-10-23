using Dapper;

using Microsoft.Data.Sqlite;

using SharedLib.Data;
using SharedLib.DependencyInjection;

namespace OrderService.ApiService.Sqlite;

public class Installer : IServiceInstaller
{
    public void AddServices(IServiceCollection services,
        IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddScoped(CreateConnection);

        SqlMapper.AddTypeHandler(new DateTimeHandler());
        SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        SqlMapper.AddTypeHandler(new GuidHandler());
        SqlMapper.AddTypeHandler(new TimeSpanHandler());
    }

    private static OpenDbConnection CreateConnection(
        IServiceProvider serviceProvider) =>
        () =>
        {
            var configuration =
                serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("Sqlite");
            return new SqliteConnection(connectionString);
        };
}