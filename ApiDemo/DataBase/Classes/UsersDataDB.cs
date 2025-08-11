using System.Linq.Expressions;
using ApiDemo.DataBase.Interfaces;
using ApiDemo.Models;
using ApiDemo.Models.User;
using ApiDemo.TypesData;

namespace ApiDemo.DataBase.Classes;

public class UsersDataDB : IUsersDataDB {
    private readonly LinkedList<UserDataModel> _users = [];

    public int GetUserCount() => _users.Count;

    public void AddUser(UserDataModel user) {
        _users.AddLast(user);
    }

    public UserDataModel? GetUserData(string username) {
        return find(user => user.Username, username);
    }

    public UserDataModel? GetUserData(Guid uuid) {
        return find(user => user.Uuid, uuid);
    }
    
    public bool tryGetUser(Guid uuid, out UserDataModel userDataModel) {
        return tryFind(user => user.Uuid, uuid, out userDataModel);
    }

    public bool tryGetUser(string username, out UserDataModel userDataModel) {
        return tryFind(user => user.Username, username, out userDataModel);
    }

    private bool tryFind<TField>(Expression<Func<UserDataModel, TField>> fieldSelector, TField value, out UserDataModel userDataModel) {
        var getter = fieldSelector.Compile();
        foreach (var item in _users) {
            if (EqualityComparer<TField>.Default.Equals(getter(item), value)) {
                userDataModel = item;
                return true;
            }
        }
        userDataModel = null!;
        return false;
    }

    private UserDataModel? find<TField>(Expression<Func<UserDataModel, TField>> fieldSelector, TField value) {
        var getter = fieldSelector.Compile();
        foreach (var item in _users) {
            if (EqualityComparer<TField>.Default.Equals(getter(item), value))
                return item;
        }
        return null;
    }
}