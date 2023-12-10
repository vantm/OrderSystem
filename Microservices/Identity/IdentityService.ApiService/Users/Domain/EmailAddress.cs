namespace IdentityService.ApiService.Users.Domain;

public sealed record EmailAddress
{
    private EmailAddress() { }

    public required string Value { get; init; } = default!;

    public static EmailAddress New(string emailAddress)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress,
            nameof(emailAddress));

        if (!emailAddress.Contains('@'))
        {
            throw new ArgumentException("Not a email", nameof(emailAddress));
        }

        return new EmailAddress() { Value = emailAddress };
    }
}