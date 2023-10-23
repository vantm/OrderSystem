﻿using System.Reflection;

using SharedLib.DependencyInjection;

namespace BasketService.ApiService.MediatR;

public class Installer : IServiceInstaller
{
    public void AddServices(IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssembly(Assembly.GetEntryAssembly()!);
        });
    }
}