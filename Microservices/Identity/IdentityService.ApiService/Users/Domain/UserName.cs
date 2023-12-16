namespace IdentityService.ApiService.Users.Domain;

public sealed record UserName(string Value)
{
    public string Value { get; } = Value;

    public static UserName New(string userName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userName, nameof(userName));
        return new(userName);
    }
}