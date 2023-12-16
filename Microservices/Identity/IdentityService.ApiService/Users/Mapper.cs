using IdentityService.ApiService.Users.Domain;
using IdentityService.ApiService.Users.Dto;
using IdentityService.Contracts;

namespace IdentityService.ApiService.Users;

public static class Mapper
{
    public static UserModel MapUserEntityToModel(User entity)
    {
        return new(
            entity.Id,
            entity.UserName.Value,
            entity.FullName.Value,
            entity.Email.Value,
            entity.IsActive,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}