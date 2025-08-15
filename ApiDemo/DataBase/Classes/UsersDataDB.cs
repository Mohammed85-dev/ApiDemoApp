using ApiDemo.DataBase.Interfaces;
using ApiDemo.Models.UserModels;
using Cassandra;
using Cassandra.Data.Linq;
using Cassandra.Mapping;
using ISession = Cassandra.ISession;

namespace ApiDemo.DataBase.Classes;

public class UsersDataDB : IUsersDataDB {
    private readonly Table<User> _users;

    private Dictionary<string, string> test = new();
    
    public UsersDataDB(ISession cassandraSession) {
        _users = new Table<User>(cassandraSession);
        _users.CreateIfNotExists();
    }

    public long GetUserCount() => _users.Count().Execute();
    public void SetUserAvatar(Guid uuid, byte[] imageBlob) =>
        _users.Where(u => u.Uuid == uuid)
            .Select(u => new User() { Avatar = imageBlob })
            .Update()
            .Execute();
    public void AddUser(User user) => _users.Insert(user).Execute();

    public User? GetUser(Guid uuid) => _users.FirstOrDefault(u => u.Uuid == uuid).Execute();

    public User? GetUser(string username) => _users.FirstOrDefault(u => u.Username == username).Execute();

    public (bool success, User user) tryGetUser(Guid uuid) {
        var user = GetUser(uuid);
        return ((user == null)
            ? (false, null)
            : (true, user))!;
    }

    public (bool success, User user) tryGetUser(string username) {
        var user = GetUser(username);
        return ((user == null)
            ? (false, null)
            : (true, user))!;
    }
}