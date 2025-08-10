using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiDemo.Models;

public class VerifyAccessTokenModel {
    [Required] [JsonPropertyName("uuid")] public required Guid Uuid { get; set; }
    [Required] [JsonPropertyName("token")] public required string Token { get; set; }
}