using AutoMapper;
using Checkout.API.Entities;
using Checkout.API.Services;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Checkout.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(
            IMapper mapper,
            IPublishEndpoint publishEndpoint,
            IShoppingCartService shoppingCartService)
        {
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet("{customerId}", Name = "GetShoppingCart")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetShoppingCart(int customerId)
        {
            var cart = await _shoppingCartService.Get(customerId);

            return Ok(cart);
        }

        [HttpGet("{customerId}/[action]", Name = "AddItem")]
        [ProducesResponseType(typeof(ShoppingCartItem), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartItem>> AddItem(int customerId, [FromQuery] int productId, [FromQuery] int quantity)
        {
            var item = await _shoppingCartService.AddItem(customerId, productId, quantity);
            return Ok(item);
        }

        [HttpGet("{customerId}/[action]", Name = "RemoveItem")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RemoveItem(int customerId, [FromQuery] Guid id)
        {
            await _shoppingCartService.RemoveItem(customerId, id);
            return Ok();
        }

        [Route("{customerId}/[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout(int customerId)
        {
            var cart = await _shoppingCartService.Get(customerId);
            if (cart == null)
            {
                return BadRequest();
            }

            // send checkout event to rabbitmq
            var eventMessage = _mapper.Map<CheckoutEvent>(cart);
            await _publishEndpoint.Publish(eventMessage);

            // remove the basket
            await _shoppingCartService.Drop(customerId);

            return Accepted();
        }
    }
}
