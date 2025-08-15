using Cassandra;
using Cassandra.Mapping;
using ISession = Cassandra.ISession;

namespace ApiDemo.DataBase.Classes;

public static class CassandraSession {
    public static async Task AddCassandraAsync(this IServiceCollection services, ILogger _logger) {
        var cluster = Cluster.Builder()
            .AddContactPoint("localhost") // Host on your machine
            .WithPort(9042) // Cassandra CQL native transport port
            // .WithCredentials("cassandra", "cassandra") // Default user/password
            .Build();

        ISession session = null;
            while (session == null){
            try {
                session = await cluster.ConnectAsync();
                break;
            }
            catch (Exception ex) {
                _logger.LogError("Failed to connect to cluster");
            }
        }
        //Delete preexisting data
        // session.DeleteKeyspaceIfExists("my_keyspace");
        //Create KeySpace
        session.CreateKeyspaceIfNotExists("my_keyspace");

        // 2. Switch to keyspace
        session.ChangeKeyspace("my_keyspace");
        var mapper = new Mapper(session);

        services.AddSingleton<ICluster>(cluster);
        services.AddSingleton<ISession>(session);
        services.AddSingleton<IMapper>(mapper);
    }
}