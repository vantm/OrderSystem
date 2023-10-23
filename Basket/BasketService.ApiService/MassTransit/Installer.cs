using MassTransit;

using Microsoft.Extensions.Options;

using SharedLib.DependencyInjection;
using SharedLib.Options;

namespace BasketService.ApiService.MassTransit;

public sealed class Installer : IServiceInstaller
{
    public void AddServices(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumersFromNamespaceContaining<Program>();
            x.AddSagasFromNamespaceContaining<Program>();
            x.AddSagaStateMachinesFromNamespaceContaining<Program>();
            x.AddActivitiesFromNamespaceContaining<Program>();
            x.AddFuturesFromNamespaceContaining<Program>();

            x.UsingGrpc((ctx, cfg) =>
            {
                var options = ctx.GetRequiredService<IOptions<GrpcTransportOptions>>();

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