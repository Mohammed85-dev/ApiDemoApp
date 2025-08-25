using System.Text.Json.Serialization;
using Cassandra.Mapping;
using Cassandra.Mapping.Attributes;

namespace ApiDemo.Models.Courses.CourseChapter;

[TableName("courseChapters")]
public class CourseChapter {
    [JsonIgnore][PartitionKey] public Guid Ucid;
    [SecondaryIndex]
    [JsonPropertyName("chapterId")]
    public int ChapterId { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
    [JsonPropertyName("timeStamp")] public DateTime TimeStamp { get; set; }
}