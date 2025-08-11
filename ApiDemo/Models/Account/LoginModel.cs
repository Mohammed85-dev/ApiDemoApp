using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiDemo.Core.Properties;

namespace ApiDemo.Models.Account {
    public partial class LoginModel {
        [JsonPropertyName("usingUsername")]
        [Required]
        public required bool UsingUsername { get; init; }
        [JsonPropertyName("username")] public string Username { get; init; } = string.Empty;
        [EmailAddress] 
        [JsonPropertyName("email")]
        public string Email { get; init; } = string.Empty;
        [JsonPropertyName("password")]
        [Required]
        [Length(8, 32)]
        [DeniedValues("/")]
        [IsEncrypted]
        public required string Password { get; init; }
    }
}