using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories
{
    public class SalesRepository : AsyncRepositoryBase<Sales>, ISalesRepository
    {
        public SalesRepository(OrderContext orderContext)
            : base(orderContext)
        {
        }

        public async Task<IReadOnlyCollection<Sales>> GetByDateBetween(DateTime begin, DateTime end)
        {
            return await _dbContext.Sales
                .Where(o => begin >= o.DocDate && o.DocDate <= o.DocDate)
                .ToListAsync();
        }
    }
}
