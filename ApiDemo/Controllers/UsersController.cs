using ApiDemo.DataBase.Interfaces;
using ApiDemo.Mangers.Interfaces;
using ApiDemo.Models;
using ApiDemo.Models.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUsersManger _users, ITokenAuthorizationManger _auth) : ControllerBase {
    /*public UsersController(ILogger<UsersController> logger, IUsersDataManger usersDatMangerDataManger) {
        _logger = logger;
        _usersDatManger = usersDatMangerDataManger;
    }*/

    // GET: [api/Users/Count]
    [HttpGet]
    [Route("Count")]
    public long Get() {
        return _users.GetCount();
    }

    // GET api/Users/{uuid}
    [HttpGet("{uuid:guid}")]
    public ActionResult<UserByUuid> Get(Guid uuid) {
        return Ok(_users.GetPublicUserData(uuid));
    }

    [HttpGet]
    [Route("Avatar/{uuid:guid}")]
    public IActionResult GetAvatar(Guid uuid) {
        return File(_users.GetUserAvatar(uuid), "image/png");
    }
    
    // POST api/Users/Avatar/{uuid} 
    [HttpPut]
    [Route("Avatar/{uuid:guid}")]
    public async Task<IActionResult> UploadAvatar(Guid uuid, [FromHeader(Name = "Authorization")] string Authorization,
        IFormFile? avatar) {
        if (!_auth.IsAuthorized(uuid, Authorization, TokenPermissions.userDataRW, out var response))
            return Unauthorized(response);
        if (avatar == null || avatar.Length == 0)
            return BadRequest("No file uploaded");
        using var ms = new MemoryStream();
        await avatar.CopyToAsync(ms);
        byte[] avatarBytes = ms.ToArray();

        _users.SetUserAvatar(uuid, avatarBytes);
        return Ok("Avatar uploaded.");
    }

    // DELETE api/Users/5
    // [HttpDelete("{uuid:guid}")]
    // public void Delete(Guid uuid) { }
}