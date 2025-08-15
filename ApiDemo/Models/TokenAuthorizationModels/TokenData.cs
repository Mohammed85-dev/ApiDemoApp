using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiDemo.Mangers.Interfaces;

namespace ApiDemo.Models.TokenAuthorizationModels;

public class TokenData {
    [Required]
    [JsonPropertyName("ownerUUID")]
    public required Guid OwnerUUID { get; init; }

    [Required]
    [JsonPropertyName("accessToken")]
    public required string AccessToken { get; init; }

    [Required]
    [JsonPropertyName("refreshToken")]
    public required string RefreshToken { get; init; }

    [Required]
    [JsonPropertyName("expiresAt")]
    public DateTime ExpiresAT { get; init; } = DateTime.MaxValue;

    [Required]
    [JsonPropertyName("permissions")]
    public required List<TokenPermissions> Permissions { get; init; }
}