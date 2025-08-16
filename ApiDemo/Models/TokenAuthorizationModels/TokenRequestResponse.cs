using System.Text.Json.Serialization;

namespace ApiDemo.Models.TokenAuthorizationModels;

public class TokenRequestResponse {
    public required bool Succeeded { get; set; }
    public required string Message { get; set; }
    [JsonPropertyName("tokensData")] public TokenData? TokensModelData { get; set; }
}