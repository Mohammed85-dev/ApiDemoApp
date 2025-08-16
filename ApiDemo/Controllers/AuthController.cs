using ApiDemo.Mangers.Interfaces;
using ApiDemo.Models.TokenAuthorizationModels;
using Microsoft.AspNetCore.Mvc;


namespace ApiDemo.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ITokenAuthorizationManger _token) : ControllerBase {
        //POST api/Auth/TokenData 
        [HttpPost]
        [Route("TokenData")]
        public ActionResult<TokenData> Post([FromBody] string token) {
            if (!_token.TryGetTokenData(token, out var tokenData, out var response)) {
                return BadRequest(response);
            }
            return Ok(tokenData);
        }
    }
}