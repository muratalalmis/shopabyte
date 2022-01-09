using Catalog.Grpc;
using Checkout.API.Exceptions;
using Checkout.API.Services;
using NSubstitute;
using NUnit.Framework;

namespace Checkout.API.Tests
{
    [TestFixture]
    public class ShoppingCartItemFactoryFixture
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private CatalogGrpcService.CatalogGrpcServiceClient _grpcClient;
        private ShoppingCartItemFactory _factory;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [SetUp]
        public void Setup()
        {
            _grpcClient = Substitute.For<CatalogGrpcService.CatalogGrpcServiceClient>();
            _factory = new ShoppingCartItemFactory(_grpcClient);
        }

        [Test]
        [TestCase("1011")]
        [TestCase("1011231")]
        public void PriceMustBeEqualFromGrpcService(string expectedPrice)
        {
            var productModel = new ProductListModel() { Price = long.Parse(expectedPrice.ToString()) };
            _grpcClient.GetProductById(default).ReturnsForAnyArgs(productModel);
            var item = _factory.Create(1, 1, "TRY");

            Assert.IsNotNull(item);
            Assert.AreEqual(decimal.Parse(expectedPrice), item.Price);
        }

        [Test]
        public void IfGrpcServiceDoesNoReturnsThenThrowNotFoundException()
        {
            _grpcClient.GetProductById(default).ReturnsForAnyArgs(null, new ProductListModel[0]);

            Assert.Throws<ProductNotFoundException>(() =>
            {
                _factory.Create(1, 1, "TRY");
            });
        }
    }
}