using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WooliesX.Api.Models;
using WooliesX.Domain.Requests;
using WooliesX.Domain.Responses;

namespace WooliesX.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrolleyCalculatorController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TrolleyCalculatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST api/trolleyCalculator
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]CalculateTrolley request)
        {
            var calculateResponse = await _mediator.Send(request.Map());
            return Ok(calculateResponse);
        }
    }
}
