namespace IdentityService.Contracts;

public interface IUserApi
{
    Task<UserModel?> FindAsync(Guid id, CancellationToken cancellationToken);
}