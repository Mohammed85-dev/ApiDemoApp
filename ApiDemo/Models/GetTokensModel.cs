using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiDemo.Models;

public class GetTokensModel {
    [Required][JsonPropertyName("uuid")]
    public required Guid UUID { get; set; }
    [Required][JsonPropertyName("token")]
    public required string AccessToken { get; set; }
    [Required][JsonPropertyName("refreshToken")]
    public required string RefreshToken { get; set; }
}