using System.Text.Json.Serialization;

namespace ApiDemo.Models;

public class TokenRequestDataModel {
    public required bool Succeeded { get; set; }
    public required string Message { get; set; }

    public TokensModelData? TokensModelData { get; set; }
}

public class TokensModelData {
    [JsonPropertyName("uuid")]
    public required Guid UUID { get; set; }
    [JsonPropertyName("token")]
    public required string AccessToken { get; set; }
    [JsonPropertyName("refreshToken")]
    public required string RefreshToken { get; set; }
}