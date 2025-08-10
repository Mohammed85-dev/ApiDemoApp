using ApiDemo.DataBase.Classes.Interfaces;
using ApiDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(ILogger<UsersController> _logger, IUsersDataManger _usersDataManger) : ControllerBase {
        /*public UsersController(ILogger<UsersController> logger, IUsersDataManger usersDatMangerDataManger) {
            _logger = logger;
            _usersDatManger = usersDatMangerDataManger;
        }*/

        // GET: api/Users/Count
        [HttpGet]
        [Route("Count")]
        public int Get() {
            return _usersDataManger.GetCount();
        }

        // GET api/Users/{uuid}
        [HttpGet("{id:guid}")]
        public PublicUserDataModel Get(Guid id) {
            return _usersDataManger.GetPublicUserData(id);
        }

        // POST api/Users/Auth/SignUp
        /// <summary>
        /// 
        /// </summary>
        /// <param name="signUp"></param>
        [HttpPost]
        [Route("Auth/SignUp")]
        public ActionResult<TokenRequestDataModel> Post([FromBody] SignUpModel signUp) {
            return Ok(_usersDataManger.SignUpUser(signUp));
        }

        //Post api/Users/Auth/GetTokens
        [HttpPost]
        [Route("Auth/GetTokens")]
        public ActionResult<TokenRequestDataModel> Post([FromBody] GetTokensModel getTokens) {
            TokenRequestDataModel tokenRequestData =  _usersDataManger.getTokens(getTokens);
            if(!tokenRequestData.Succeeded)
                return BadRequest(tokenRequestData);
            return Ok(tokenRequestData.TokensModelData);
        }

        //Post api/Users/Auth/VerifyAccessToken
        [HttpPost]
        [Route("Auth/VerifyAccessToken")]
        public IActionResult Post([FromBody] VerifyAccessTokenModel verifyAccessToken) {
            if (_usersDataManger.VerifyAccessToken(verifyAccessToken))
                return Ok();
            return BadRequest();
        }

        //Post api/Users/Auth/VerifyRefreshToken
        [HttpPost]
        [Route("Auth/VerifyRefreshToken")]
        public IActionResult Post([FromBody] VerifyRefreshTokenModel verifyRefreshToken) {
            if (_usersDataManger.VerifyRefreshToken(verifyRefreshToken))
                return Ok();
            return BadRequest();
        }

        // DELETE api/Users/5
        [HttpDelete("{id:guid}")]
        public void Delete(Guid id) { }
    }
}