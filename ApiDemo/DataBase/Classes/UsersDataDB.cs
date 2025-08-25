using ApiDemo.DataBase.Interfaces;
using ApiDemo.Models.UserModels;
using Cassandra.Data.Linq;
using ISession = Cassandra.ISession;

namespace ApiDemo.DataBase.Classes;

public class UsersDataDB : IUsersDataDB {
    private readonly Table<User> _users;

    private Dictionary<string, string> test = new();

    public UsersDataDB(ISession cassandraSession) {
        _users = new Table<User>(cassandraSession);
        _users.CreateIfNotExists();
    }

    public long GetUserCount() {
        return _users.Count().Execute();
    }

    public void SetUserAvatarFileId(Guid uuid, Guid avatarFileId) {
        _users.Where(u => u.UnqiueUserId == uuid)
            .Select(u => new User { AvatarFileId = avatarFileId, })
            .Update()
            .Execute();
    }

    public void AddUser(User user) {
        _users.Insert(user).Execute();
    }

    public User? GetUser(Guid uuid) {
        return _users.FirstOrDefault(u => u.UnqiueUserId == uuid).Execute();
    }

    public User? GetUser(string username) {
        return _users.FirstOrDefault(u => u.Username == username).Execute();
    }

    //todo DO NOT LEAVE THIS LIKE THIS
    public (bool success, User user) tryGetUser(Guid uuid) {
        var user = GetUser(uuid);
        return (user == null
            ? (false, null)
            : (true, user))!;
    }

    public (bool success, User user) tryGetUser(string username) {
        var user = GetUser(username);
        return (user == null
            ? (false, null)
            : (true, user))!;
    }

    public Guid GetUserAvatarFileId(Guid uuid) {
        return _users.FirstOrDefault(u => u.UnqiueUserId == uuid).Execute().AvatarFileId;
    }
}