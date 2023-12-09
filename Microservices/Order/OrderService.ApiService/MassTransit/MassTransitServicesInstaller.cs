using MassTransit;

using Microsoft.Extensions.Options;

using SharedLib.DependencyInjection;

namespace OrderService.ApiService.MassTransit;

public sealed class MassTransitServicesInstaller : IServicesInstaller
{
    public void AddServices(IServiceCollection services,
        IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddOptionsWithValidateOnStart<MassTransitModuleOptions>()
            .BindConfiguration(MassTransitModuleOptions.Name)
            .ValidateDataAnnotations();

        services.AddMassTransit(x =>
        {
            x.AddConsumersFromNamespaceContaining<ProjectRoot>();
            x.AddSagasFromNamespaceContaining<ProjectRoot>();
            x.AddSagaStateMachinesFromNamespaceContaining<ProjectRoot>();
            x.AddActivitiesFromNamespaceContaining<ProjectRoot>();
            x.AddFuturesFromNamespaceContaining<ProjectRoot>();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                var options = ctx
                    .GetRequiredService<IOptions<MassTransitModuleOptions>>()
                    .Value;

                cfg.Host(options.Host, r =>
                {
                    r.Username(options.Username);
                    r.Password(options.Password);
                });
                
                cfg.ConfigureEndpoints(ctx);
            });
        });
    }
}