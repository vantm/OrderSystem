using IdentityService.ApiContracts;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IdentityService.Http;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserApiHttpClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<UserApiHttpClientOptions>()
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddHttpClient(UserApiHttpClient.ClientName, (serviceProvider, httpClient) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<UserApiHttpClientOptions>>();
            var baseUrl = options.Value.BaseUrl;
            httpClient.BaseAddress = new Uri(baseUrl);
        });

        services.AddScoped<IUserApi, UserApiHttpClient>();

        return services;
    }
}