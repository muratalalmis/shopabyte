using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Ordering.Infrastructure.Repositories
{
    public abstract class AsyncRepositoryBase<TEntity> : IAsyncRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected internal readonly OrderContext _dbContext;

        public AsyncRepositoryBase(OrderContext orderContext)
        {
            _dbContext = orderContext ?? throw new ArgumentNullException(nameof(orderContext));
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> predicate = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            List<Expression<Func<TEntity, object>>> includes = null, 
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _dbContext.Set<TEntity>().FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
