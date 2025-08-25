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
    [JsonPropertyName("customPermissions")]
    [Column("customPermissions")]
    public List<string> customPermissions { get; init; } = [];

    [Required]
    [JsonPropertyName("presetPermissions")]
    [Column("presetPermissions")]
    public List<string> PresetPermissions { get; init; } = [];

    [JsonIgnore]
    [Ignore]
    public IEnumerable<PresetTokenPermissions> PresetPermissionEnums {
        get => PresetPermissions.Select(p => Enum.Parse<PresetTokenPermissions>(p));
        set {
            PresetPermissions.Clear();
            PresetPermissions.AddRange(value.Select(p => p.ToString()));
        }
    }
}