using ApiDemo.Models;
using ApiDemo.Models.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.DataBase.Interfaces;

public interface IUsersManger {
    public long GetCount();
    public void SetUserAvatar(Guid uuid, byte[] imageBlob);
    public ActionResult<UserByUuid> GetPublicUserData(Guid uuid);
}