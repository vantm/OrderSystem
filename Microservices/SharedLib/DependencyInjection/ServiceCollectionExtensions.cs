using System.Reflection;

using Microsoft.Extensions.Hosting;

namespace SharedLib.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddServiceInstallers(
        this IHostApplicationBuilder builder)
        => builder.AddServiceInstallers(
            Assembly.GetEntryAssembly()!,
            Assembly.GetExecutingAssembly());

    private static void AddServiceInstallers(
        this IHostApplicationBuilder builder, params Assembly[] assemblies)
    {
        var installers = assemblies
            .SelectMany(x => x.GetTypes().Where(IsInstaller))
            .Select(Activator.CreateInstance)
            .Cast<IServicesInstaller>()
            .ToArray();

        foreach (var installer in installers)
        {
            installer.AddServices(
                builder.Services,
                builder.Configuration,
                builder.Environment);
        }
    }

    private static bool IsInstaller(Type x)
        => x.IsClass && !x.IsAbstract &&
           x.IsAssignableTo(typeof(IServicesInstaller));
}