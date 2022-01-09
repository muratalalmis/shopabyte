using Catalog.Grpc;
using Checkout.API.Entities;
using Checkout.API.Exceptions;

namespace Checkout.API.Services
{
    public class ShoppingCartItemFactory : IShoppingCartItemFactory
    {
        private readonly CatalogGrpcService.CatalogGrpcServiceClient _catalogGrpcServiceClient;

        public ShoppingCartItemFactory(CatalogGrpcService.CatalogGrpcServiceClient catalogGrpcServiceClient)
        {
            _catalogGrpcServiceClient = catalogGrpcServiceClient;
        }

        public ShoppingCartItem Create(int productId, int quantity, string currency)
        {
            var product = _catalogGrpcServiceClient.GetProductById(new GetDiscountRequest() { Id = productId });
            if (product == null)
            {
                throw new ProductNotFoundException(productId);
            }

            var basePrice = ConvertTo(product.Currency, currency, product.Price);

            return new ShoppingCartItem()
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                Price = quantity * basePrice,
                Quantity = 1
            };
        }

        private decimal ConvertTo(string from, string to, decimal amount)
        {
            if (from != to)
            {
                return default;
            }
            else
            {
                return amount;
            }
        }
    }
}
