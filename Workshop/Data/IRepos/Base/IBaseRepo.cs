using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCWorkshop.Data.IRepos.Base
{
    public interface IBaseRepo<TEntity, TId> : IReadOnlyRepo<TEntity, TId>
    {
        void Add(TEntity entity);
        void AddList(List<TEntity> entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity, bool saveChanges = true);
    }
}
