using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ApiDemo.Models {
    public class PublicUserDataModel {
        [JsonPropertyName("uuid")]
        public required Guid Uuid { get; init; }
        [JsonPropertyName("username")] public required string Username { get; init; }
    }
}