using Checkout.API.Entities;
using Checkout.API.Repositories;

namespace Checkout.API.Services
{
    internal class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartItemFactory _shoppingCartItemFactory;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(
            IShoppingCartItemFactory shoppingCartItemFactory,
            IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartItemFactory = shoppingCartItemFactory;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<ShoppingCartItem> AddItem(int customerId, int productId, int quantity)
        {
            var cart = await _shoppingCartRepository.Get(customerId);
            if (cart == null)
            {
                cart = CreateDefault(customerId);
            }

            var item = _shoppingCartItemFactory.Create(productId, quantity, cart.Currency);

            cart.Items.Add(item);
            cart.Total += item.Price;

            await _shoppingCartRepository.Save(cart);

            return item;
        }

        public async Task RemoveItem(int customerId, Guid id)
        {
            var cart = await _shoppingCartRepository.Get(customerId);
            if (cart == null)
            {
                return;
            }

            var item = cart.Items.FirstOrDefault(o => o.Id == id);
            if (item != null)
            {
                cart.Items.Remove(item);
                await _shoppingCartRepository.Save(cart);
            }
        }

        public async Task Drop(int customerId)
        {
            await _shoppingCartRepository.Drop(customerId);
        }

        public async Task<ShoppingCart> ChangeCurrency(int customerId, string currency)
        {
            var cart = await _shoppingCartRepository.Get(customerId);
            cart.Currency = currency;

            return await _shoppingCartRepository.Save(cart);
        }

        public async Task<ShoppingCart> Get(int customerId)
        {
            var cart = await _shoppingCartRepository.Get(customerId);
            if (cart == null)
            {
                cart = CreateDefault(customerId);
                await _shoppingCartRepository.Save(cart);
            }

            return cart;
        }

        static ShoppingCart CreateDefault(int customerId)
        {
            return new ShoppingCart()
            {
                Currency = "TRY",
                CustomerId = customerId,
                Total = 0,
                Items = new List<ShoppingCartItem>()
            };
        }
    }
}
