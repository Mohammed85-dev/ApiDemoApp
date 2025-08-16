using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiDemo.Mangers.Interfaces;
using Cassandra.Mapping;
using Cassandra.Mapping.Attributes;

namespace ApiDemo.Models.TokenAuthorizationModels;

[TableName("tokens")]
public class TokenData {
    [Required]
    [PartitionKey]
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; init; } = null!;
    [Required]
    [SecondaryIndex]
    [JsonPropertyName("ownerUUID")]
    public Guid OwnerUUID { get; init; }
    [Required]
    [JsonPropertyName("refreshToken")]
    public string RefreshToken { get; init; } = null!;

    [Required]
    [JsonPropertyName("expiresAt")]
    public DateTime ExpiresAT { get; init; } = DateTime.MaxValue;

    [Required]
    [JsonPropertyName("permissions")]
    [Column("permissions")]
    public List<string> Permissions { get; init; } = new();

    [JsonIgnore]
    [Ignore]
    public IEnumerable<TokenPermissions> PermissionEnums {
        get => Permissions.Select(p => Enum.Parse<TokenPermissions>(p));
        set {
            Permissions.Clear();
            Permissions.AddRange(value.Select(p => p.ToString()));
        }
    }
}