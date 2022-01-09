using Checkout.API.Entities;

namespace Checkout.API.Services
{
    internal class ShoppingCartItemFactory : IShoppingCartItemFactory
    {
        public ShoppingCartItem Create(int productId, int quantity)
        {
            return new ShoppingCartItem()
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                Price = quantity * 0,
                Quantity = 1
            };
        }
    }
}
