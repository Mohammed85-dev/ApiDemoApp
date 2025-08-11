using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiDemo.Models;

public class VerifyRefreshTokenModel {
    [Required] [JsonPropertyName("uuid")] public required Guid Uuid { get; set; }
    [Required]
    [JsonPropertyName("refreshToken")]
    public required string RefreshToken { get; set; }
}