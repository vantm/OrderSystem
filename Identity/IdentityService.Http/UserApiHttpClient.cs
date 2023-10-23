using System.Net.Http.Json;

using IdentityService.ApiContracts;

namespace IdentityService.Http;

internal class UserApiHttpClient(IHttpClientFactory httpClientFactory)
    : IUserApi
{
    public const string ClientName = "UserApi";

    public async Task<UserModel?> FindAsync(Guid id, CancellationToken cancellationToken)
    {
        using var http = httpClientFactory.CreateClient(ClientName);

        var path = $"/users/{id}";
        var user = await http.GetFromJsonAsync<UserModel?>(path, cancellationToken);

        return user;
    }
}