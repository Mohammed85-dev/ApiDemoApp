using Cassandra.Mapping;
using Cassandra.Mapping.Attributes;

namespace ApiDemo.Models.AccountModels;

[TableName("accounts")]
public class Account {
    [Column("hashedUserRefreshTokens")] public List<string> HashedRefreshTokens = [];
    [Column("hashedUserAccessTokens")] public List<string> HashedUserAccessTokens = [];
    [Column("uuid")][PartitionKey] public Guid UUID { get; init; }
    [Column("userUsername")]
    [SecondaryIndex]
    public string UserUsername { get; init; } = default!;
    [Column("email")][SecondaryIndex] public string Email { get; init; } = default!;
    [Column("password")] public string Password { get; init; } = default!;
}