using Microsoft.AspNetCore.Mvc;
using ApiDemo.DataBase.Interfaces;
using ApiDemo.Models;
using ApiDemo.Models.Auth;


namespace ApiDemo.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAccountDataManger _accountDataManger) : ControllerBase {
        // POST api/Account/PasswordRest
        [HttpPut]
        [Route("PasswordRest")]
        public IActionResult Put([FromBody] PasswordRestModel passwordRest) {
            return UnprocessableEntity();
            if (_accountDataManger.PasswordRest(passwordRest))
                return Ok();
            else 
                return BadRequest();
        }

        // POST api/Account/Login
        [HttpPost]
        [Route("Login")]
        public ActionResult<TokenRequestResponseDataModel> Post([FromBody] LoginModel login) {
            // return UnprocessableEntity();
            return _accountDataManger.LoginUser(login);
        }

        // POST api/Account/SignUp
        /// <summary>
        /// 
        /// </summary>
        /// <param name="signUp"></param>
        [HttpPost]
        [Route("SignUp")]
        public ActionResult<TokenRequestResponseDataModel> Post([FromBody] SignUpModel signUp) {
            return Ok(_accountDataManger.SignUpUser(signUp));
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