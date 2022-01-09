using Ordering.Domain.Entities;

namespace Ordering.Application.Contracts.Persistence
{
    public interface ISalesRepository : IAsyncRepository<Sales>
    {
        Task<IReadOnlyCollection<Sales>> GetSalesByDate(DateTime begin, DateTime end);
    }
}
