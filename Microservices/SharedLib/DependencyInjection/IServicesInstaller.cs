﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SharedLib.DependencyInjection;

public interface IServicesInstaller
{
    void AddServices(
        IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment);
}