using Cassandra.Mapping;
using Cassandra.Mapping.Attributes;

namespace ApiDemo.TypesData;

[TableName("FileInfo")]
public class DBFileInfo {
    [PartitionKey] public Guid UniqueFileId { get; set; }
    public string? FileName { get; set; }
    public string? Path { get; set; }
    public Guid OwnerUserId { get; set; }
    public List<string> UniqueRequiredPermission { get; set; } = [];
}