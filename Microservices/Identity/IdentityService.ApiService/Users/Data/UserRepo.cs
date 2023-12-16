using IdentityService.ApiService.Users.Domain;

using SharedLib.Data;

namespace IdentityService.ApiService.Users;

public class UserRepo : IUserRepo
{
    private readonly CreateDbConnection _createDbConnection;

    public UserRepo(CreateDbConnection createDbConnection)
    {
        _createDbConnection = createDbConnection;
    }

    public async Task<User?> FindAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        
    }

    public async Task<User> InsertAsync(User user,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<User> UpdateAsync(User user,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<User> DeleteAsync(User user,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}