using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiDemo.Models.Courses.Course;

public class CreateCourse {
    [Required][JsonPropertyName("uuid")] public required Guid uuid { get; set; }
    [Required][JsonPropertyName("name")] public required string Name { get; set; }
    [Required]
    [JsonPropertyName("description")]
    public required string Description { get; set; }
    [Required][JsonPropertyName("tags")] public required List<string> Tags { get; set; }
}