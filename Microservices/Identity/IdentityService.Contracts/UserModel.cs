namespace IdentityService.Contracts;

public record UserModel(
    Guid Id,
    string UserName,
    string FullName,
    string EmailAddress,
    bool IsActive,
    DateTime CreatedAt,
    DateTime UpdatedAt)
{
    
}