using System.Reflection;

using Microsoft.Extensions.Hosting;

namespace SharedLib.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddServiceInstallers(this IHostApplicationBuilder builder)
        => builder.AddServiceInstallers(Assembly.GetEntryAssembly()!);

    public static void AddServiceInstallers(this IHostApplicationBuilder builder, Assembly assembly)
    {
        var installers = assembly
            .GetTypes()
            .Where(x => x.IsClass && !x.IsAbstract && x.IsAssignableTo(typeof(IServiceInstaller)))
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>()
            .ToArray();

        foreach (var installer in installers)
        {
            installer.AddServices(
                builder.Services,
                builder.Configuration,
                builder.Environment);
        }
    }
}