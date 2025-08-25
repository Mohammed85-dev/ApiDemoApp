using ApiDemo.Mangers.Interfaces;
using ApiDemo.Models.AccountModels;
using ApiDemo.Models.TokenAuthorizationModels;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IAccountManger _accountData, ITokenAuthorizationManger tokenAuthorization) : ControllerBase {
    // POST api/Account/ChangePassword
    [HttpPut]
    [Route("ChangePassword")]
    public IActionResult Put([FromHeader(Name = "Authorization")] string Authorization, [FromBody] PasswordChange passwordChangeChange) {
        if (!tokenAuthorization.IsAuthorized(
            passwordChangeChange.Uuid,
            Authorization,
            PresetTokenPermissions.permissionsLevelZero,
            out var tokenAuthResponse))
            return BadRequest(tokenAuthResponse);
        if (_accountData.PasswordRest(passwordChangeChange, out var passwordRestResponse))
            return Ok(passwordRestResponse);
        return BadRequest(passwordRestResponse);
    }

    // POST api/Account/Login
    [HttpPost]
    [Route("Login")]
    public ActionResult<TokenRequestResponse> Post([FromBody] Login login) {
        var response = _accountData.LoginUser(login);
        return response.Succeeded ? Ok(response) : BadRequest(response);
    }

    //METHODS
    //GET
    //POST [PUT, DELETE]

    // POST api/Account/SignUp
    /// <summary>
    /// </summary>
    /// <param name="signUp"></param>
    [HttpPost]
    [Route("SignUp")]
    public ActionResult<TokenRequestResponse> Post([FromBody] SignUp signUp) {
        var response = _accountData.SignUpUser(signUp);
        return response.Succeeded ? Ok(response) : BadRequest(response);
    }
}