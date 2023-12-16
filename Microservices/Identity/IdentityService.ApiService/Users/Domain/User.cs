using IdentityService.ApiService.Users.Dto;

using SharedLib.Domain;

namespace IdentityService.ApiService.Users.Domain;

public class User : Entity
{
    public Guid Id { get; private set; }
    public UserName UserName { get; private set; }
    public HashedPassword Password { get; private set; }
    public FullName FullName { get; private set; }
    public EmailAddress Email { get; set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public static User New(
        UserName userName,
        HashedPassword password,
        FullName fullName,
        EmailAddress email,
        bool isActive)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            Password = password,
            FullName = fullName,
            Email = email,
            IsActive = isActive,
            CreatedAt = DateTime.UtcNow
        };

        var domainEvent = UserCreatedDomainEvent.FromEntity(user);

        user.AddDomainEvent(domainEvent);

        return user;
    }

    internal static User Restore(UserDto dto)
    {
        return new User()
        {
            Id = dto.Id,
            UserName = UserName.New(dto.UserName),
            Password =
                new HashedPassword(dto.PasswordHash, dto.PasswordSalt)
        };
    }

    public void Update(FullName fullName, bool isActive)
    {
        if (FullName == fullName)
        {
            return;
        }

        var prevFullName = fullName;
        var prevIsActive = IsActive;

        FullName = fullName;
        IsActive = isActive;
        UpdatedAt = DateTime.UtcNow;

        var domainEvent = UserUpdatedDomainEvent.FromEntity(
            this, prevFullName.Value, prevIsActive);

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