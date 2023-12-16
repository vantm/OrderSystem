using MediatR;

namespace IdentityService.ApiService.Users.Domain;

public record UserCreatedDomainEvent : INotification
{
    public required Guid Id { get; init; }
    public required string UserName { get; init; }
    public required string FullName { get; init; }
    public required string EmailAddress { get; init; }
    public required bool IsActive { get; init; }
    public required DateTime CreatedAt { get; init; }


    public static UserCreatedDomainEvent FromEntity(User user)
    {
        return new()
        {
            Id = user.Id,
            UserName = user.UserName.Value,
            FullName = user.FullName.Value,
            EmailAddress = user.Email.Value,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
    }
}