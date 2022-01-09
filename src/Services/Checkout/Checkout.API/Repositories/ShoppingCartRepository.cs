using Checkout.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Checkout.API.Repositories
{
    internal class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IDistributedCache _redisCache;

        public ShoppingCartRepository(IDistributedCache cache)
        {
            _redisCache = cache;
        }

        public async Task Drop(int customerId)
        {
            await _redisCache.RemoveAsync(customerId.ToString());
        }

        public async Task<ShoppingCart> Get(int customerId)
        {
            var basket = await _redisCache.GetStringAsync(customerId.ToString());

            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> Save(ShoppingCart basket)
        {
            await _redisCache.SetStringAsync(basket.CustomerId.ToString(), JsonConvert.SerializeObject(basket));

            return await Get(basket.CustomerId);
        }
    }
}
