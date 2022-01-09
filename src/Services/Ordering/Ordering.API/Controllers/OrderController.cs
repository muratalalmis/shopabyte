using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Sales.Commands.PlaceOrder;
using Ordering.Application.Features.Sales.Queries.GetSalesByDate;
using System.Net;

namespace Ordering.API.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetSales")]
        [ProducesResponseType(typeof(IEnumerable<SalesDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<SalesDto>>> GetSalesBetweenDate([FromQuery] DateTime beginDate, [FromQuery] DateTime endDate)
        {
            var query = new GetSalesByDateQuery(beginDate, endDate);
            var sales = await _mediator.Send(query);

            return Ok(sales);
        }

        [HttpPost(Name = "PlaceOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> PlaceOrder([FromBody] PlaceOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
