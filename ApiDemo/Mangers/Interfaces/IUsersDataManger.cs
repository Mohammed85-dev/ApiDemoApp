using ApiDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.DataBase.Interfaces;

public interface IUsersDataManger {
    public int GetCount();
    public ActionResult<PublicUserDataModel> GetPublicUserData(Guid uuid);
}