using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiDemo.Core.Properties;

namespace ApiDemo.Models {
    public class SignUpModel {
        [JsonPropertyName("username")]
        [Required]
        [MinLength(3)]
        public required string Username { get; set; }
        [JsonPropertyName("email")]
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [JsonPropertyName("password")]
        [Required]
        [MinLength(8)]
        [IsEncrypted]
        public required string Password { get; set; }
    }
}