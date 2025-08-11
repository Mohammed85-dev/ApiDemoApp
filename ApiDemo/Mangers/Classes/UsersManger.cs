using ApiDemo.DataBase.Interfaces;
using ApiDemo.Models;
using ApiDemo.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Mangers.Classes {
    public class UsersManger(IUsersDataDB _usersDB) : IUsersManger {
        public int GetCount() => _usersDB.GetUserCount();

        public ActionResult<PublicUserDataModel> GetPublicUserData(Guid uuid) {
            if (!_usersDB.tryGetUser(uuid, out UserDataModel userData)) {
                return new BadRequestResult();
            }
            return new PublicUserDataModel() {
                Uuid = uuid,
                Username = userData.Username,
            };
        }
    }
}