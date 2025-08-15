using ApiDemo.DataBase.Interfaces;
using ApiDemo.Models.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Mangers.Classes {
    public class UsersManger(IUsersDataDB _usersDB) : IUsersManger {
        public long GetCount() => _usersDB.GetUserCount();
        public void SetUserAvatar(Guid uuid, byte[] imageBlob) => _usersDB.SetUserAvatar(uuid, imageBlob);
        
        public ActionResult<UserByUuid> GetPublicUserData(Guid uuid) {
            var result = _usersDB.tryGetUser(uuid);
            if (!result.success) {
                return new BadRequestResult();
            }
            return new UserByUuid() {
                Username = result.user.Username,
            };
        }
    }
}