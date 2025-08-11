using ApiDemo.TypesData;

namespace ApiDemo.DataBase.Interfaces;

public interface IUsersDataDB {
    public int GetUserCount();
    public void AddUser(UserData user);
    public bool tryGetUser(Guid uuid, out UserData userData);
    public bool tryGetUser(string username, out UserData userData);

}