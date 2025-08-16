using Cassandra.Mapping;
using Cassandra.Mapping.Attributes;

namespace ApiDemo.Models.AccountModels;

[TableName("accounts")]
public class Account {
    public readonly List<string> HashedRefreshTokens = [];
    public readonly List<string> HashedUserAccessTokens = [];
    [PartitionKey] public required Guid UUID { get; set; }
    [SecondaryIndex] public required string UserUsername { get; set; }
    [SecondaryIndex] public required string Email { get; set; }
    public required string Password { get; set; }
}