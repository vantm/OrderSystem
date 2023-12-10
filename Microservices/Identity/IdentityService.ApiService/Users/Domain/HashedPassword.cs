using System.Security.Cryptography;
using System.Text;

namespace IdentityService.ApiService.Users.Domain;

public record HashedPassword
{
    public static readonly HashedPassword Empty = new()
    {
        HashedValue = Array.Empty<byte>(),
        Salt = Array.Empty<byte>()
    };

    public required byte[] HashedValue { get; init; }
    public required byte[] Salt { get; init; }

    private HashedPassword()
    {
    }

    public bool IsMatch(string password)
    {
        var passHash = Hash(password, Salt);
        return passHash.SequenceEqual(HashedValue);
    }

    public static HashedPassword Create(string password, int saltLength = 128)
    {
        var salt = CreateSalt(saltLength);
        var hashedPass = Hash(password, salt);

        return new() { HashedValue = hashedPass, Salt = salt };
    }

    private static byte[] CreateSalt(int length)
    {
        var salt = new byte[length];

        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(salt);

        return salt;
    }

    private static byte[] Hash(string password, byte[] salt)
    {
        using var sha256 = SHA256.Create();

        var bytes = Encoding.UTF8.GetBytes(password);
        var input = new byte[salt.Length + bytes.Length];

        Array.Copy(salt, input, 0);
        Array.Copy(bytes, input, salt.Length);

        var hashed = sha256.ComputeHash(input);

        return hashed;
    }
}