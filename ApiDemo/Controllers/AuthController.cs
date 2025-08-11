using Microsoft.AspNetCore.Mvc;
using ApiDemo.DataBase.Interfaces;
using ApiDemo.Models;
using ApiDemo.Models.Auth;


namespace ApiDemo.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ILogger<UsersController> _logger, ITokenAutherizationManger _authDataManger) : ControllerBase {
        // [HttpPut]
        // [Route("PasswordRest")]
        // public IActionResult Put([FromHeader(Name = "Authorization")] string authorization, [FromBody] PasswordRestModel passwordRest) {
        //     // if(!_authDataManger.Authorization(authorization))
        //     //     return Unauthorized();
        //     if (_authDataManger.PasswordRest(passwordRest,  out var response))
        //         return Ok(response);
        //     else 
        //         return BadRequest(response);
        // }
        //
        // [HttpPost]
        // [Route("Login")]
        // public ActionResult<TokenRequestDataModel> Post([FromBody] LoginModel login) {
        //     return _authDataManger.LoginUser(login);
        // }
        //
        // // POST api/Users/Auth/SignUp
        // /// <summary>
        // /// 
        // /// </summary>
        // /// <param name="signUp"></param>
        // [HttpPost]
        // [Route("SignUp")]
        // public ActionResult<TokenRequestDataModel> Post([FromBody] SignUpModel signUp) {
        //     _logger.Log(LogLevel.Information, "User created");
        //     return Ok(_authDataManger.SignUpUser(signUp));
        // }
        //
        // //Post api/Users/Auth/GetTokens
        // [HttpPost]
        // [Route("GetTokens")]
        // public ActionResult<TokenRequestDataModel> Post([FromBody] GetTokensModel getTokens) {
        //     TokenRequestDataModel tokenRequestData = _authDataManger.getTokens(getTokens);
        //     if (!tokenRequestData.Succeeded)
        //         return BadRequest(tokenRequestData);
        //     return Ok(tokenRequestData.TokensModelData);
        // }
        //
        // //Post api/Users/Auth/VerifyAccessToken
        // [HttpPost]
        // [Route("VerifyAccessToken")]
        // public IActionResult Post([FromBody] VerifyAccessTokenModel verifyAccessToken) {
        //     if (_authDataManger.VerifyAccessToken(verifyAccessToken, out var response))
        //         return Ok(response);
        //     return BadRequest();
        // }
        //
        // //Post api/Users/Auth/VerifyRefreshToken
        // [HttpPost]
        // [Route("VerifyRefreshToken")]
        // public IActionResult Post([FromBody] VerifyRefreshTokenModel verifyRefreshToken) {
        //     if (_authDataManger.VerifyRefreshToken(verifyRefreshToken, out var response))
        //         return Ok(response);
        //     return BadRequest();
        // }
    }
}