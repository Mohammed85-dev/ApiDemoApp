using Microsoft.AspNetCore.Mvc;
using ApiDemo.DataBase.Interfaces;
using ApiDemo.Mangers.Interfaces;
using ApiDemo.Models;
using ApiDemo.Models.Account;
using ApiDemo.Models.Auth;


namespace ApiDemo.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAccountManger _accountData, ITokenAuthorizationManger tokenAuthorization) : ControllerBase {
        // POST api/Account/PasswordRest
        [HttpPut]
        [Route("PasswordRest")]
        public IActionResult Put([FromHeader] string Authorization, [FromBody] PasswordRestModel passwordRest) {
            if (!tokenAuthorization.IsAuthorized(
                passwordRest.Uuid,
                Authorization,
                TokenPermissions.userDataRW,
                out string tokenAuthResponse)) {
                return BadRequest(tokenAuthResponse);
            }
            if (_accountData.PasswordRest(passwordRest, out string passwordRestResponse))
                return Ok(passwordRestResponse);
            else
                return BadRequest(passwordRestResponse);
        }

        // POST api/Account/Login
        [HttpPost]
        [Route("Login")]
        public ActionResult<TokenRequestResponseDataModel> Post([FromBody] LoginModel login) {
            // return UnprocessableEntity();
            return _accountData.LoginUser(login);
        }

        // POST api/Account/SignUp
        /// <summary>
        /// 
        /// </summary>
        /// <param name="signUp"></param>
        [HttpPost]
        [Route("SignUp")]
        public ActionResult<TokenRequestResponseDataModel> Post([FromBody] SignUpModel signUp) {
            return Ok(_accountData.SignUpUser(signUp));
        }

        /*//Post api/Users/Auth/GetTokens
        [HttpPost]
        [Route("GetTokens")]
        public ActionResult<TokenRequestResponseDataModel> Post([FromBody] GetTokensModel getTokens) {
            TokenRequestResponseDataModel tokenRequestResponseData = _accountDataManger.getTokens(getTokens);
            if (!tokenRequestResponseData.Succeeded)
                return BadRequest(tokenRequestResponseData);
            return Ok(tokenRequestResponseData.TokensModelData);
        }

        //Post api/Users/Auth/VerifyAccessToken
        [HttpPost]
        [Route("VerifyAccessToken")]
        public IActionResult Post([FromBody] VerifyAccessTokenModel verifyAccessToken) {
            if (_accountDataManger.VerifyAccessToken(verifyAccessToken, out var response))
                return Ok(response);
            return BadRequest();
        }

        //Post api/Users/Auth/VerifyRefreshToken
        [HttpPost]
        [Route("VerifyRefreshToken")]
        public IActionResult Post([FromBody] VerifyRefreshTokenModel verifyRefreshToken) {
            if (_accountDataManger.VerifyRefreshToken(verifyRefreshToken, out var response))
                return Ok(response);
            return BadRequest();
        }*/
    }
}