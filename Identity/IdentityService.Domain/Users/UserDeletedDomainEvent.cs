using MediatR;

namespace IdentityService.Domain.Users;

public class UserDeletedDomainEvent : INotification
{
    public required Guid Id { get; init; }
    public required string UserName { get; init; }
    public required string FullName { get; init; }
    public required string EmailAddress { get; init; }
    public required bool IsActive { get; init; }
    public required DateTime DeletedAt { get; init; }

    public static UserDeletedDomainEvent FromEntity(User user)
    {
        if (!user.DeletedAt.HasValue)
        {
            throw new InvalidOperationException("User wasn't marked as deleted");
        }

        return new()
        {
            Id = user.Id,
            UserName = user.UserName,
            FullName = user.FullName,
            EmailAddress = user.EmailAddress,
            IsActive = user.IsActive,
            DeletedAt = user.DeletedAt.Value
        };
    }
}