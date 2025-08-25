using System.Security.Cryptography;
using System.Text;

namespace ApiDemo.Core.Tokens;

public class TokenGenerator : ITokenGenerator {
    /// <summary>
    ///     Generates a cryptographically secure random token.
    /// </summary>
    /// <returns>URL-safe string (Base64 without +/=)</returns>
    public string GenerateToken() {
        //Number of random bytes (default 32 ~ 43 chars)
        const int bytes = 32;
        var data = new byte[bytes];
        RandomNumberGenerator.Fill(data); // CSPRNG
        var b64 = Convert.ToBase64String(data);
        return b64.Replace('+', '-').Replace('/', '_').TrimEnd('=');
    }

    // Hashes a token using SHA-256 for secure DB storage.
    public string HashToken(string token) {
        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(token));
        return Convert.ToHexString(hash).ToLower();
    }
}