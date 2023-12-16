namespace IdentityService.ApiService.Users.Dto;

public sealed record UserDto
{
    public required Guid Id { get; init; }
    public required string UserName { get; init; }
    public required byte[] PasswordHash { get; init; }
    public required byte[] PasswordSalt { get; init; }
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public required bool IsActive { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime UpdatedAt { get; init; }
    public required DateTime? DeletedAt { get; init; }
}