using Checkout.API.Entities;

namespace Checkout.API.Services
{
    internal interface IShoppingCartItemFactory
    {
        ShoppingCartItem Create(int productId, int quantity);
    }
}
