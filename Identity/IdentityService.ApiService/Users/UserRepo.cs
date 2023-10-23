using IdentityService.Domain.Users;

namespace IdentityService.ApiService.Users;

public class UserRepo : IUserRepo
{
    public async Task<User?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<User> InsertAsync(User user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<User> DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}