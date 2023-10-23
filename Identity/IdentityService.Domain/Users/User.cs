using SharedLib;
using SharedLib.Domain;

namespace IdentityService.Domain.Users;

public class User : Entity
{
    public Guid Id { get; private set; }
    public string UserName { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string PasswordSalt { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public static User New(
        UserName userName,
        HashedPassword password,
        FullName fullName,
        EmailAddress emailAddress,
        bool isActive)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            UserName = userName.Value,
            PasswordHash = password.Hash,
            PasswordSalt = password.Salt,
            FullName = fullName.Value,
            EmailAddress = emailAddress.Value,
            IsActive = isActive,
            CreatedAt = DateTime.UtcNow
        };

        var domainEvent = UserCreatedDomainEvent.FromEntity(user);

        user.AddDomainEvent(domainEvent);

        return user;
    }

    public void Update(FullName fullName, bool isActive)
    {
        if (FullName == fullName.Value)
        {
            return;
        }

        var previousFullName = FullName;
        var previousIsActive = isActive;

        FullName = fullName.Value;
        IsActive = isActive;
        UpdatedAt = DateTime.UtcNow;

        var domainEvent = UserUpdatedDomainEvent.FromEntity(
            this, previousFullName, previousIsActive);

        AddDomainEvent(domainEvent);
    }

    public void Delete()
    {
        if (DeletedAt.HasValue)
        {
            return;
        }

        DeletedAt = DateTime.UtcNow;

        var domainEvent = UserDeletedDomainEvent.FromEntity(this);

        AddDomainEvent(domainEvent);
    }
}