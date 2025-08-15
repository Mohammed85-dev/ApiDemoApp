using System.Text.Json.Serialization;

namespace ApiDemo.Models.UserModels {
    public class UserByUuid {
        [JsonPropertyName("username")] public required string Username { get; init; }
    }
}