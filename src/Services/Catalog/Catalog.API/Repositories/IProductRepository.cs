using Catalog.API.Entities;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        Task<Product> Get(int id);
        Task<IReadOnlyCollection<Product>> GetProducts(int[] ids);
    }
}
