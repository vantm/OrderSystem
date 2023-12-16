using MediatR;

namespace IdentityService.ApiService.Users.Domain;

public class UserUpdatedDomainEvent : INotification
{
    public required Guid Id { get; init; }
    public required string FullName { get; init; }
    public required bool IsActive { get; init; }
    public required string PreviousName { get; init; }
    public required bool PreviousIsActive { get; init; }
    public required DateTime UpdatedAt { get; init; }

    public static UserUpdatedDomainEvent FromEntity(
        User user,
        string previousFullName,
        bool previousIsActive)
    {
        return new()
        {
            Id = user.Id,
            FullName = user.FullName.Value,
            IsActive = user.IsActive,
            PreviousName = previousFullName,
            PreviousIsActive = previousIsActive,
            UpdatedAt = user.UpdatedAt
        };
    }
}