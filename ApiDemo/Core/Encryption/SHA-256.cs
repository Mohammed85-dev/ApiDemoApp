using System.Security.Cryptography;
using System.Text;

namespace ApiDemo.Core.Encryption;

public class SHA_256 {
    public static string ComputeSha256Hash(string rawData) {
        using (var sha256Hash = SHA256.Create()) {
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert the byte array to a hex string
            var builder = new StringBuilder();
            foreach (var b in bytes)
                builder.Append(b.ToString("x2"));

            return builder.ToString();
        }
    }
}