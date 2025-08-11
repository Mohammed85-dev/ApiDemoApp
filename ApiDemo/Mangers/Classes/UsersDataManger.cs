using ApiDemo.Core.Tokens;
using ApiDemo.DataBase.Interfaces;
using ApiDemo.TypesData;
using Microsoft.AspNetCore.Mvc;
using ApiDemo.Models;
using ApiDemo.Models.Auth;

namespace ApiDemo.DataBase.Classes {
    public class UsersDataManger(IUsersDataDB _usersDB) : IUsersDataManger {
        public int GetCount() => _usersDB.GetUserCount();

        public ActionResult<PublicUserDataModel> GetPublicUserData(Guid uuid) {
            if (!_usersDB.tryGetUser(uuid, out UserData userData)) {
                return new BadRequestResult();
            }
            return new PublicUserDataModel() {
                Uuid = uuid,
                Username = userData.Username,
            };
        }
    }
}