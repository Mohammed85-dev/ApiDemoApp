using Cassandra.Mapping;
using Cassandra.Mapping.Attributes;

namespace ApiDemo.Models.UserModels;

[TableName("users")]
public class User {
    [PartitionKey] public Guid UnqiueUserId { get; init; }
    [SecondaryIndex] public string Username { get; init; } = default!;
    public Guid AvatarFileId { get; init; } = Guid.Empty;
}