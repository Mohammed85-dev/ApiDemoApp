using System.Text.Json.Serialization;
using Cassandra.Mapping;
using Cassandra.Mapping.Attributes;

namespace ApiDemo.Models.Courses.PlayList;

[TableName("CoursesPlayLists")]
public class PlaylistData {
    [PartitionKey]
    [JsonPropertyName("upid")]
    public Guid uniquePlaylistId { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; } = null!;
    [JsonPropertyName("description")] public string Description { get; set; } = null!;
    [JsonPropertyName("tags")] public List<string> Tags { get; set; } = null!;
    [JsonPropertyName("createdOn")] public DateTime Created { get; set; }
    [JsonPropertyName("lastedUpdateOn")] public DateTime LastUpdateDate { get; set; } = DateTime.Now;
    [JsonPropertyName("Courses")] public Dictionary<int, Guid> Courses { get; set; } = new();
    [JsonIgnore] public Guid PictureFileId { get; set; } = Guid.Empty;
}