using Microsoft.AspNetCore.Mvc;
using ApiDemo.DataBase.Classes.Interfaces;
using ApiDemo.Models;


namespace ApiDemo.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ILogger<UsersController> _logger, IUsersDataManger _usersDataManger) : ControllerBase {
        // [HttpPost]
        // [Route("PasswordRest")]
        // public IActionResult Post([FromBody] PasswordRestModel passwordRest) {
        //     if (_usersDataManger.PasswordRest(passwordRest))
        //         return Ok();
        //     else 
        //         return BadRequest();
        // }

        [HttpPost]
        [Route("Login")]
        public ActionResult<TokenRequestDataModel> Post([FromBody] LoginModel login) {
            return _usersDataManger.LoginUser(login);
        }

        // POST api/Users/Auth/SignUp
        /// <summary>
        /// 
        /// </summary>
        /// <param name="signUp"></param>
        [HttpPost]
        [Route("SignUp")]
        public ActionResult<TokenRequestDataModel> Post([FromBody] SignUpModel signUp) {
            _logger.Log(LogLevel.Information, "User created");
            return Ok(_usersDataManger.SignUpUser(signUp));
        }

        //Post api/Users/Auth/GetTokens
        [HttpPost]
        [Route("GetTokens")]
        public ActionResult<TokenRequestDataModel> Post([FromBody] GetTokensModel getTokens) {
            TokenRequestDataModel tokenRequestData = _usersDataManger.getTokens(getTokens);
            if (!tokenRequestData.Succeeded)
                return BadRequest(tokenRequestData);
            return Ok(tokenRequestData.TokensModelData);
        }

        //Post api/Users/Auth/VerifyAccessToken
        [HttpPost]
        [Route("VerifyAccessToken")]
        public IActionResult Post([FromBody] VerifyAccessTokenModel verifyAccessToken) {
            if (_usersDataManger.VerifyAccessToken(verifyAccessToken))
                return Ok();
            return BadRequest();
        }

        //Post api/Users/Auth/VerifyRefreshToken
        [HttpPost]
        [Route("VerifyRefreshToken")]
        public IActionResult Post([FromBody] VerifyRefreshTokenModel verifyRefreshToken) {
            if (_usersDataManger.VerifyRefreshToken(verifyRefreshToken))
                return Ok();
            return BadRequest();
        }
    }
}