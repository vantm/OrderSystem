namespace IdentityService.ApiService.Users.Domain;

public interface IUserRepo
{
    Task<User?> FindAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User> InsertAsync(User user, CancellationToken cancellationToken = default);
    Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default);
    Task<User> DeleteAsync(User user, CancellationToken cancellationToken = default);
}