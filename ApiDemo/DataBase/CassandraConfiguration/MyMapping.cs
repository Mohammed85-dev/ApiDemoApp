using ApiDemo.Models.AccountModels;
using Cassandra.Mapping;

namespace ApiDemo.DataBase.CassandraConfiguration;

public class MyMapping : Mappings {
    private const string keyspace = "my_keyspace";

    public MyMapping() {
        For<Account>()
            .KeyspaceName(keyspace)
            .TableName("accounts")
            .PartitionKey(a => a.UUID)
            .Column(a => a.UUID, cm => cm.WithName("uuid"))
            .Column(a => a.UserUsername, cm => cm.WithName("user_username"))
            .Column(a => a.Password, cm => cm.WithName("password"))
            .Column(a => a.Email, cm => cm.WithName("email"))
            .Column(a => a.HashedUserAccessTokens, cm => cm.WithName("hashed_user_access_tokens"))
            .Column(a => a.HashedRefreshTokens, cm => cm.WithName("hashed_refresh_tokens"));
    }
}