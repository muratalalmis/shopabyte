using Checkout.API.Entities;
using Checkout.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Checkout.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet("{customerId}", Name = "GetShoppingCart")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetShoppingCart(int customerId)
        {
            var cart = await _shoppingCartService.Get(customerId);

            return Ok(cart);
        }

        [HttpGet("{customerId}", Name = "AddItem")]
        [ProducesResponseType(typeof(ShoppingCartItem), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartItem>> AddItem(int customerId, [FromQuery] int productId, [FromQuery] int quantity)
        {
            var item = await _shoppingCartService.AddItem(customerId, productId, quantity);
            return Ok(item);
        }

        [HttpGet("{customerId}", Name = "RemoveItem")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartItem>> RemoveItem(int customerId, [FromQuery] Guid id)
        {
            await _shoppingCartService.RemoveItem(customerId, id);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout(int customerId)
        {
            var basket = await _shoppingCartService.Get(customerId);
            if (basket == null)
            {
                return BadRequest();
            }

            // send checkout event to rabbitmq
            // TODO : use rabbit mq

            // remove the basket
            await _shoppingCartService.Drop(customerId);

            return Accepted();
        }
    }
}
