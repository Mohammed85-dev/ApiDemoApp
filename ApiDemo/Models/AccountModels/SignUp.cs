using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiDemo.Core.Properties;

namespace ApiDemo.Models.AccountModels {
    public class SignUp {
        [JsonPropertyName("username")]
        [Required]
        [MinLength(3)]
        public required string Username { get; init; }
        [JsonPropertyName("email")]
        [Required]
        [EmailAddress]
        public required string Email { get; init; }
        [JsonPropertyName("password")]
        [Required]
        [MinLength(8)]
        [MaxLength(64)]
        [IsEncrypted]
        public required string Password { get; init; }
    }
}