using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiDemo.Core.Properties;

namespace ApiDemo.Models.AccountModels;

public class PasswordChange {
    [JsonPropertyName("uuid")][Required] public required Guid Uuid { get; init; }
    [JsonPropertyName("oldPassword")]
    [Required]
    [IsEncrypted]
    public required string OldPassword { get; init; }
    [JsonPropertyName("newPassword")]
    [Required]
    [IsEncrypted]
    public required string NewPassword { get; init; }
}