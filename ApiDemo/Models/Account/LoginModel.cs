using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiDemo.Core.Properties;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ApiDemo.Models.Account {
    public class LoginModel {
        [JsonPropertyName("usingUsername")]
        [Required]
        public required bool UsingUsername { get; init; }
        [JsonPropertyName("username")] public string? Username { get; init; }
        [EmailAddress] 
        [JsonPropertyName("email")]
        public string? Email { get; init; }
        [JsonPropertyName("password")]
        [Required]
        [MinLength(8)]
        [MaxLength(64)]
        [DeniedValues("/")]
        [IsEncrypted]
        public required string Password { get; init; }
    }
}