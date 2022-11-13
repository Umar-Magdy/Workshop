using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MVCWorkshop.Data.IRepos.Base
{
    public interface IReadOnlyRepo<TEntity, TId>
    {
        Task<TEntity> GetByIdAsync(TId id);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeExpressions);
        Task<IReadOnlyList<TEntity>> ListAllAsync(params Expression<Func<TEntity, object>>[] includeExpressions);
        Task<IReadOnlyList<TEntity>> ListAllAsync(Expression<Func<TEntity, bool>> filter = null, int page = 0, int size = 10, params Expression<Func<TEntity, object>>[] includeExpressions);
        Task<IReadOnlyList<TEntity>> ListOrderedAsync<TKey>(Expression<Func<TEntity, TKey>> orderBy,
          Expression<Func<TEntity, bool>> filter = null, int page = 0, int size = 10, params Expression<Func<TEntity, object>>[] includeExpressions);
        Task<IReadOnlyList<TEntity>> ListDescendingOrderedAsync<TKey>(Expression<Func<TEntity, TKey>> orderBy,
            Expression<Func<TEntity, bool>> filter = null, int page = 0, int size = 10, params Expression<Func<TEntity, object>>[] includeExpressions);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);
        IQueryable<T> IncludeEntities<T>(params Expression<Func<T, object>>[] includes);
    }
}
