using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

namespace IdentityService.ApiService.IdentityServer.Services;

public class UserProfileService : IProfileService
{
    public Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        throw new NotImplementedException();
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        throw new NotImplementedException();
    }
}