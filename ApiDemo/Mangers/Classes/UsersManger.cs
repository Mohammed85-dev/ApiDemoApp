using ApiDemo.DataBase.Interfaces;
using ApiDemo.Mangers.Interfaces;
using ApiDemo.Models.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Mangers.Classes;

public class UsersManger(IFileManger fileManger, IUsersDataDB _usersDB) : IUsersManger {
    public long GetCount() {
        return _usersDB.GetUserCount();
    }

    public async Task SetUserAvatar(Guid uuid, Stream avatar) {
        var avatarFileId = await fileManger.UploadFile(uuid, avatar, "randomAvatar", FileManger.FileType.UserAvatar);
        _usersDB.SetUserAvatarFileId(uuid,
            avatarFileId
        );
        fileManger.GetRequiredPermission(avatarFileId).ForEach(rp => fileManger.RemoveFilePermission(avatarFileId, rp));
    }

    public Stream GetUserAvatar(Guid uuid) {
        var userAvatarFileId = _usersDB.GetUserAvatarFileId(uuid);
        return fileManger.GetFileStream(userAvatarFileId);
    }

    public ActionResult<UserByUuid> GetPublicUserData(Guid uuid) {
        var result = _usersDB.tryGetUser(uuid);
        if (!result.success) return new BadRequestResult();
        return new UserByUuid {
            Username = result.user.Username,
        };
    }
}