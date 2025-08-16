using Cassandra.Mapping;
using Cassandra.Mapping.Attributes;

namespace ApiDemo.Models.UserModels;

[TableName("users")]
public class User {
    [PartitionKey] public Guid Uuid { get; init; }
    [SecondaryIndex] public string Username { get; init; } = default!;
    public byte[] Avatar { get; init; } = null!;
}