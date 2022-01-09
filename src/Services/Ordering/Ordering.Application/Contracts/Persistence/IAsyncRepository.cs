using Ordering.Domain.Entities;
using System.Linq.Expressions;

namespace Ordering.Application.Contracts.Persistence
{
    /// <summary>
    /// Abstraction of the repository pattern
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAsyncRepository<TEntity> 
        where TEntity : class, IEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// It calls find method, may be more helpful then get async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// Get by query
        /// </summary>
        /// <param name="predicate">where predicate</param>
        /// <param name="orderBy">order by expression</param>
        /// <param name="includes">join tables</param>
        /// <param name="disableTracking">use true when you dont need to change data</param>
        /// <returns></returns>
        Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
                                       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                       List<Expression<Func<TEntity, object>>> includes = null,
                                       bool disableTracking = true);
    }
}
