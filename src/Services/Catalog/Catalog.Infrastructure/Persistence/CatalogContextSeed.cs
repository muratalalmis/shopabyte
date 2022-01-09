using Catalog.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Persistence
{
    public class CatalogContextSeed
    {
        public static async Task SeedAsync(CatalogContext catalogContext, ILogger<CatalogContextSeed> logger)
        {
            if (!catalogContext.Products.Any())
            {
                logger.LogInformation($"There is no data in {nameof(CatalogContext)}. Seed started async.");

                catalogContext.Products.AddRange(GetPreConfiguration());
                await catalogContext.SaveChangesAsync();

                logger.LogInformation($"{nameof(CatalogContext)} seed completed async.");
            }
        }

        private static IEnumerable<Product> GetPreConfiguration()
        {
            return new List<Product>
            {
                new Product() { Name = "Iphone 11", Currency = "TRY", Price = 10999 },
                new Product() { Name = "Iphone 12", Currency = "TRY", Price = 15699 },
                new Product() { Name = "Iphone 13", Currency = "TRY", Price = 25699 },
            };
        }
    }
}
