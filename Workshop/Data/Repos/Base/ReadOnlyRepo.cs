using Microsoft.EntityFrameworkCore;
using MVCWorkshop.Data.IRepos.Base;
using MVCWorkshop.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MVCWorkshop.Data.Repos.Base
{
    public class ReadOnlyRepo<TEntity, TId> : IReadOnlyRepo<TEntity, TId> where TEntity : class
    {
        private readonly NewsDbContext _dbContext;

        public ReadOnlyRepo(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            if (includeExpressions.Any())
            {
                return await getIQuerableWithIncludes(includeExpressions).FirstOrDefaultAsync(filter ?? (s => true));
            }
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(filter ?? (s => true));
        }

        public async Task<IReadOnlyList<TEntity>> ListAllAsync(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            if (includeExpressions.Any())
            {
                return await getIQuerableWithIncludes(includeExpressions).ToListAsync();
            }
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> ListAllAsync(Expression<Func<TEntity, bool>> filter = null, int page = 0, int size = 10, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            if (includeExpressions.Any())
            {
                return await getIQuerableWithIncludes(includeExpressions).Where(filter ?? (s => true))
                .Skip(page * size).Take(size)
                .ToListAsync();
            }
            return await _dbContext.Set<TEntity>().Where(filter ?? (s => true))
                .Skip(page * size).Take(size)
                .ToListAsync();
        }
        public async Task<IReadOnlyList<TEntity>> ListOrderedAsync<TKey>(Expression<Func<TEntity, TKey>> orderBy,
            Expression<Func<TEntity, bool>> filter = null, int page = 0, int size = 10, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            if (includeExpressions.Any())
            {
                return await getIQuerableWithIncludes(includeExpressions).Where(filter ?? (s => true))
                .OrderBy(orderBy).Where(filter ?? (s => true)).Skip(page * size).Take(size)
                .ToListAsync();
            }
            return await _dbContext.Set<TEntity>().OrderBy(orderBy).Where(filter ?? (s => true)).Skip(page * size).Take(size).ToListAsync();
        }
        public async Task<IReadOnlyList<TEntity>> ListDescendingOrderedAsync<TKey>(Expression<Func<TEntity, TKey>> orderBy,
            Expression<Func<TEntity, bool>> filter = null, int page = 0, int size = 10, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            if (includeExpressions.Any())
            {
                return await getIQuerableWithIncludes(includeExpressions).Where(filter ?? (s => true))
                .OrderByDescending(orderBy).Where(filter ?? (s => true)).Skip(page * size).Take(size)
                .ToListAsync();
            }
            return await _dbContext.Set<TEntity>().OrderByDescending(orderBy).Where(filter ?? (s => true)).Skip(page * size).Take(size).ToListAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await _dbContext.Set<TEntity>().CountAsync(filter ?? (s => true));
        }

        private IQueryable<TEntity> getIQuerableWithIncludes(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            return includeExpressions
              .Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>
               (_dbContext.Set<TEntity>(), (current, expression) => current.Include(expression));
        }

        public IQueryable<T> IncludeEntities<T>(params Expression<Func<T, object>>[] includes)
        {
            return null;
        }
    }
}
