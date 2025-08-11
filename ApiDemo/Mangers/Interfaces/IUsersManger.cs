using ApiDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.DataBase.Interfaces;

public interface IUsersManger {
    public int GetCount();
    public ActionResult<PublicUserDataModel> GetPublicUserData(Guid uuid);
}