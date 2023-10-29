using System.Security.Cryptography;
using System.Text;

namespace IdentityService.ApiService.Users.Domain;

public record HashedPassword(byte[] PassHash, byte[] PassSalt)
{
    public static readonly HashedPassword Empty = new();

    private HashedPassword() : this(Array.Empty<byte>(), Array.Empty<byte>())
    {
    }

    public bool IsMatch(string password)
    {
        var passHash = Hash(password, PassSalt);
        return passHash.SequenceEqual(PassHash);
    }

    public static HashedPassword Create(string password, int saltLength = 128)
    {
        var salt = CreateSalt(saltLength);
        var hashed = Hash(password, salt);

        return new(hashed, salt);
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