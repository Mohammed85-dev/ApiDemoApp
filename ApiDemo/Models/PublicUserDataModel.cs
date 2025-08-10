using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ApiDemo.Models {
    public class PublicUserDataModel {
        [JsonPropertyName("username")] public required string Username { get; set; }
        [JsonPropertyName("email")]
        [EmailAddress]
        public required string Email { get; set; }
    }
}