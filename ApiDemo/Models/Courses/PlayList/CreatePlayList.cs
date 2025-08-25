using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiDemo.Models.Courses.PlayList;

public class CreatePlayList {
    [Required][JsonPropertyName("name")] public required string Name { get; set; }
    [Required]
    [JsonPropertyName("description")]
    public required string Description { get; set; }
    [Required][JsonPropertyName("tags")] public required List<string> Tags { get; set; }
}