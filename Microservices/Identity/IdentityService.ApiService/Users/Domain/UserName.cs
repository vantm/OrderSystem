namespace IdentityService.ApiService.Users.Domain;

public sealed class UserName
{
    private UserName() { }

    public string Value { get; private set; } = default!;

    public static UserName New(string userName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userName, nameof(userName));

        return new() { Value = userName };
    }
}