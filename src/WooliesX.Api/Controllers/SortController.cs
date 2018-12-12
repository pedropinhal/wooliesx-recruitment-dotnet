using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WooliesX.Domain.Requests;
using WooliesX.Domain.Responses;

namespace WooliesX.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SortController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/sort
        [HttpGet]
        public async Task<ActionResult> Get(string sortOption)
        {
            if (string.IsNullOrEmpty(sortOption))
            {
                return BadRequest(sortOption);
            }
            
            var user = await _mediator.Send(new GetSortRequest { SortOption = sortOption });
            return Ok(user);
        }
    }
}
