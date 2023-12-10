using SharedLib.Domain;

namespace IdentityService.ApiService.Users.Domain;

public class User : Entity
{
    public Guid Id { get; private set; }
    public string UserName { get; private set; } = default!;
    public byte[] PasswordHash { get; private set; } = default!;
    public byte[] PasswordSalt { get; private set; } = default!;
    public string FullName { get; private set; } = default!;
    public string EmailAddress { get; private set; } = default!;
    public bool IsActive { get; private set; }
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
            PasswordHash = password.HashedValue,
            PasswordSalt = password.HashedValue,
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
        var previousIsActive = IsActive;

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