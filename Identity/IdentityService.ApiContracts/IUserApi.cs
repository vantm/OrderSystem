namespace IdentityService.ApiContracts;

public interface IUserApi
{
    Task<UserModel?> FindAsync(Guid id, CancellationToken cancellationToken);
}