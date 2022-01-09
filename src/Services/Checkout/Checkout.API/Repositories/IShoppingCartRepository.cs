using Checkout.API.Entities;

namespace Checkout.API.Repositories
{
    internal interface IShoppingCartRepository
    {
        Task<ShoppingCart> Get(int customerId);
        Task<ShoppingCart> Save(ShoppingCart basket);
        Task Drop(int customerId);
    }
}
