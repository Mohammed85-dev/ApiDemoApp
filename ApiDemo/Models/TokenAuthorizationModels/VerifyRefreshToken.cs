using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiDemo.Models.TokenAuthorizationModels;

public class VerifyRefreshToken {
    [Required] [JsonPropertyName("uuid")] public required Guid Uuid { get; set; }
    [Required]
    [JsonPropertyName("refreshToken")]
    public required string RefreshToken { get; set; }
}