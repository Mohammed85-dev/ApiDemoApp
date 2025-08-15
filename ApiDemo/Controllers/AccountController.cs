using Microsoft.AspNetCore.Mvc;
using ApiDemo.Mangers.Interfaces;
using ApiDemo.Models.AccountModels;
using ApiDemo.Models.TokenAuthorizationModels;


namespace ApiDemo.Controllers {
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
                TokenPermissions.userDataRW,
                out string tokenAuthResponse)) {
                return BadRequest(tokenAuthResponse);
            }
            if (_accountData.PasswordRest(passwordChangeChange, out string passwordRestResponse))
                return Ok(passwordRestResponse);
            else
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
        /// 
        /// </summary>
        /// <param name="signUp"></param>
        [HttpPost] 
        [Route("SignUp")]
        public ActionResult<TokenRequestResponse> Post([FromBody] SignUp signUp) {
            var response = _accountData.SignUpUser(signUp);
            return response.Succeeded ? Ok(response) : BadRequest(response);
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