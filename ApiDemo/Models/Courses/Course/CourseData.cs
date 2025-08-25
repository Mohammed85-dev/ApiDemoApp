using System.Text.Json.Serialization;
using Cassandra.Mapping;
using Cassandra.Mapping.Attributes;

namespace ApiDemo.Models.Courses.Course;

[TableName("Courses")]
public class CourseData {
    [JsonPropertyName("ucid")]
    [PartitionKey]
    public Guid Ucid { get; set; }
    public Guid OwnerUserId { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
    [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
    [JsonPropertyName("tags")] public List<string> Tags { get; set; } = [];
    [JsonPropertyName("createdOn")] public DateTime Created { get; set; }
    [JsonPropertyName("lastedUpdateOn")] public DateTime LastUpdateDate { get; set; } = DateTime.Now;
    [JsonPropertyName("chapters")][Ignore] public List<CourseChapter.CourseChapter> Chapters { get; set; } = [];
    [JsonIgnore] public Guid videoFileId { get; set; } = Guid.Empty;
}