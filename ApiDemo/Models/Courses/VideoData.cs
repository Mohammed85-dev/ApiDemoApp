using Cassandra.Mapping;
using Cassandra.Mapping.Attributes;

namespace ApiDemo.Models.Courses;

[TableName("videos")]
public class VideoData {
   [PartitionKey] public string Id { get; set; }
   [SecondaryIndex] public Guid CourseId { get; set; }
   [SecondaryIndex] public int chunkId { get; set; }
   public byte[] chunk { get; set; }
}