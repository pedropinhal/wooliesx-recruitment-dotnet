using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WooliesX.Domain.Requests;
using WooliesX.Domain.Responses;

namespace WooliesX.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/user
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var user = await _mediator.Send(new GetUserRequest());
            return Ok(user);
        }
    }
}
