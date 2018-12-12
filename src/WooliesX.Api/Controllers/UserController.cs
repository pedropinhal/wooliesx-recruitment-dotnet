using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WooliesX.Domain.Responses;

namespace WooliesX.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET api/user
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var user = new UserResponse { Name = "name", Token = "token" };
            return Ok(user);
        }
    }
}
