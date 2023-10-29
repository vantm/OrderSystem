using MassTransit;

using Microsoft.Extensions.Options;

using SharedLib.DependencyInjection;
using SharedLib.Options;

namespace CatalogService.ApiService.MassTransit;

public sealed class Installer : IServiceInstaller
{
    public void AddServices(IServiceCollection services,
        IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddOptions<GrpcTransportOptions>()
            .BindConfiguration(GrpcTransportOptions.Name)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddMassTransit(x =>
        {
            x.AddConsumersFromNamespaceContaining<ProjectRoot>();
            x.AddSagasFromNamespaceContaining<ProjectRoot>();
            x.AddSagaStateMachinesFromNamespaceContaining<ProjectRoot>();
            x.AddActivitiesFromNamespaceContaining<ProjectRoot>();
            x.AddFuturesFromNamespaceContaining<ProjectRoot>();

            x.UsingGrpc((ctx, cfg) =>
            {
                var options =
                    ctx.GetRequiredService<IOptions<GrpcTransportOptions>>();

                cfg.Host(options.Value.Host, c =>
                {
                    foreach (Uri server in options.Value.Servers)
                    {
                        c.AddServer(server);
                    }
                });

                cfg.ConfigureEndpoints(ctx);
            });
        });
    }
}