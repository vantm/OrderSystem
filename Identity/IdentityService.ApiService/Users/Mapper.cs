using IdentityService.ApiService.Users.Domain;
using IdentityService.Contracts;

namespace IdentityService.ApiService.Users;

public static class Mapper
{
    public static UserModel MapUserEntityToModel(User entity)
    {
        return new(
            entity.Id,
            entity.UserName,
            entity.FullName,
            entity.EmailAddress,
            entity.IsActive,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}