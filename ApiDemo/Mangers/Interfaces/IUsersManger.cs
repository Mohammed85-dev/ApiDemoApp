using ApiDemo.Models.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.DataBase.Interfaces;

public interface IUsersManger {
    public long GetCount();
    public Task SetUserAvatar(Guid uuid, Stream avatar);
    public Stream GetUserAvatar(Guid uuid);
    public ActionResult<UserByUuid> GetPublicUserData(Guid uuid);
}