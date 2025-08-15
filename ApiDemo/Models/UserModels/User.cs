using Cassandra.Mapping;
using Cassandra.Mapping.Attributes;
using Microsoft.Build.Framework;

namespace ApiDemo.Models.UserModels;

[TableName("users")]
public class User {
    [PartitionKey]
    public Guid Uuid { get; init; }
    [SecondaryIndex]
    public string Username { get; init; }
    public byte[] Avatar { get; set; } = default!;
} 