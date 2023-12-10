namespace IdentityService.ApiService.Users.Domain;

public sealed record FullName
{
    private FullName() { }

    public required string Value { get; init; }

    public static FullName New(string fullName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fullName, nameof(fullName));
        return new() { Value = fullName };
    }
}