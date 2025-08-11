using ApiDemo.DataBase.Interfaces;
using ApiDemo.TypesData;

namespace ApiDemo.DataBase.Classes;

public class UsersDataDB : IUsersDataDB {
    private readonly LinkedList<UserData> _users = new();
    
    public int GetUserCount() => _users.Count;
    public void AddUser(UserData user) {
        _users.AddLast(user);
    }

    public bool tryGetUser(Guid uuid, out UserData userData) {
        var node = _users.First;
        while (node != null) {
            if (node.Value!.Uuid == uuid) {
                userData = node.Value;
                return true;
            }
            node = node.Next;
        }
        userData = null!;
        return false;
    }

    public bool tryGetUser(string username, out UserData userData) {
        var node = _users.First;
        while (node != null) {
            if (node.Value!.Username == username) {
                userData = node.Value;
                return true;
            }
            node = node.Next;
        }
        userData = null!;
        return false;
    }
}