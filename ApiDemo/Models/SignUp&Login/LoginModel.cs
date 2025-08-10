using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiDemo.Core.Properties;

namespace ApiDemo.Models {
    public class LoginModel {
        [JsonPropertyName("usingUsername")]
        [Required]
        public required bool UsingUsername { get; set; }
        [JsonPropertyName("username")] public string? Username { get; set; }
        [JsonPropertyName("Email")] public string? Email { get; set; }
        [JsonPropertyName("password")] [Required] [IsEncrypted] public required string Password { get; set; }
    }
}