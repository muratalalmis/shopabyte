using Checkout.API.Entities;

namespace Checkout.API.Services
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> Get(int customerId);
        Task<ShoppingCartItem> AddItem(int customerId, int productId, int quantity);
        Task RemoveItem(int customerId, Guid id);
        Task<string> Checkout(int customerId);
        Task<ShoppingCart> ChangeCurrency(int customerId, string currency);
    }
}
