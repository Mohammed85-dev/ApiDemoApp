using ApiDemo.Models.UserModels;

namespace ApiDemo.DataBase.Interfaces;

public interface IUsersDataDB {
    public long GetUserCount();
    public void AddUser(User user);
    public User? GetUser(Guid uuid);
    public User? GetUser(string username);
    public (bool success, User user) tryGetUser(Guid uuid);
    public (bool success, User user) tryGetUser(string username);
    public void SetUserAvatarFileId(Guid uuid, Guid avatarFileId);
    public Guid GetUserAvatarFileId(Guid uuid);
}