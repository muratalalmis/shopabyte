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

        [HttpGet(Name = "GetShoppingCart")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetShoppingCart(int customerId)
        {
            var cart = await _shoppingCartService.Get(customerId);
            
            return Ok(cart);
        }
    }
}
