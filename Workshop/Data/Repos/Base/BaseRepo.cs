using Microsoft.EntityFrameworkCore;
using MVCWorkshop.Data.IRepos.Base;
using MVCWorkshop.DbContexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCWorkshop.Data.Repos.Base
{
    public class BaseRepo<TEntity, TId> : ReadOnlyRepo<TEntity, TId>,IBaseRepo<TEntity, TId> where TEntity : class
    {
        private readonly NewsDbContext _dbContext;

        public BaseRepo(NewsDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
        public async void AddList(List<TEntity> entity)
        {
            await _dbContext.AddRangeAsync(entity);
        }
        public async Task UpdateAsync(TEntity entity, bool saveChanges = true)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            if (saveChanges)
                await _dbContext.SaveChangesAsync();
        }
        public virtual void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
             _dbContext.SaveChanges();
        }
    }
}
