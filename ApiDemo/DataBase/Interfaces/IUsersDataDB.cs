using ApiDemo.Models;
using ApiDemo.Models.UserModels;
using ApiDemo.TypesData;
using Cassandra.Data.Linq;

namespace ApiDemo.DataBase.Interfaces;

public interface IUsersDataDB {
    public long GetUserCount();
    public void AddUser(User user);
    public void SetUserAvatar(Guid uuid, byte[] imageBlob);
    public User? GetUser(Guid uuid);
    public User? GetUser(string username);
    public (bool success, User user) tryGetUser(Guid uuid);
    public (bool success, User user) tryGetUser(string username);
    public byte[] GetUserAvatar(Guid uuid);
}