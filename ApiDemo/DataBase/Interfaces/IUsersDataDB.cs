using ApiDemo.Models;
using ApiDemo.Models.User;
using ApiDemo.TypesData;

namespace ApiDemo.DataBase.Interfaces;

public interface IUsersDataDB {
    public int GetUserCount();
    public void AddUser(UserDataModel user);
    public bool tryGetUser(Guid uuid, out UserDataModel userDataModel);
    public bool tryGetUser(string username, out UserDataModel userDataModel);

}