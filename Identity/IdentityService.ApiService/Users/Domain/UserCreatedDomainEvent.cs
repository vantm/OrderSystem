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
            UserName = user.UserName,
            FullName = user.FullName,
            EmailAddress = user.EmailAddress,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
    }
}