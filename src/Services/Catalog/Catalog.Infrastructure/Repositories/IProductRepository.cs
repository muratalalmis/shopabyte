using Catalog.Domain.Entities;

namespace Catalog.Infrastructure.Repositories
{
    public interface IProductRepository
    {
        Task<Product> Get(int id);
        Task<IReadOnlyCollection<Product>> GetProducts(int[] ids);
    }
}
