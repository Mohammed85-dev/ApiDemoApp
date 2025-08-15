using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiDemo.Mangers.Interfaces;
using Cassandra.Mapping;
using Cassandra.Mapping.Attributes;

namespace ApiDemo.Models.TokenAuthorizationModels;

[TableName("tokens")]
public class TokenData {
    [Required][SecondaryIndex] 
    [JsonPropertyName("ownerUUID")]
    public required Guid OwnerUUID { get; init; }

    [Required][PartitionKey]
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