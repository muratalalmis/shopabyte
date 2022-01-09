using Ordering.Domain.Entities;

namespace Ordering.Application.Contracts.Persistence
{
    public interface ISalesRepository : IAsyncRepository<Sales>
    {
        Task<IReadOnlyCollection<Sales>> GetByDateBetween(DateTime begin, DateTime end);
    }
}
