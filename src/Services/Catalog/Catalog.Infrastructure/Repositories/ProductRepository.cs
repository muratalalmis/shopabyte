using Catalog.API.Persistence;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _catalogContext;

        public ProductRepository(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task<Product> Get(int id)
        {
            return await _catalogContext.Products.FindAsync(id);
        }

        public async Task<IReadOnlyCollection<Product>> GetProducts(int[] ids)
        {
            return await _catalogContext.Products.Where(o => ids.Contains(o.Id)).ToListAsync();
        }
    }
}
