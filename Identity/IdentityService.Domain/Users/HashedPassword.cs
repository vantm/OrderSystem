using System.Security.Cryptography;
using System.Text;

namespace IdentityService.Domain.Users;

public record HashedPassword(string Hash, string Salt)
{
    public static HashedPassword Create(string password)
    {
        const int SaltLength = 32;

        var salt = new byte[SaltLength];

        using (var csp = RandomNumberGenerator.Create())
        {
            csp.GetBytes(salt);
        }

        byte[] hashed;

        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var input = new byte[SaltLength + bytes.Length];

            Array.Copy(salt, input, 0);
            Array.Copy(bytes, input, salt.Length);

            hashed = sha256.ComputeHash(input);
        }

        var hexSalt = Convert.ToHexString(salt);
        var hexHashed = Convert.ToHexString(hashed);

        return new(hexSalt, hexHashed);
    }
}