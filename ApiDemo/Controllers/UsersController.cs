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
        [HttpGet("{uuid:guid}")]
        public PublicUserDataModel Get(Guid uuid) {
            return _usersDataManger.GetPublicUserData(uuid);
        }

        // DELETE api/Users/5
        [HttpDelete("{uuid:guid}")]
        public void Delete(Guid uuid) { }
    }
}