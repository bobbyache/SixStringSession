using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.EF.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        protected SmartSessionContext context;

        public BaseRepository(SmartSessionContext context)
        {
            this.context = context;
        }

        public TEntity Get(int id) => context.Set<TEntity>().Find(id);
        public void Add(TEntity entity) => context.Add(entity);
        public void AddRange(IEnumerable<TEntity> entities) => context.AddRange(entities);
        public void Update(TEntity entity) => context.Update(entity);
        public void Remove(TEntity entity) => context.Remove(entity);
        public void RemoveRange(IEnumerable<TEntity> entities) => context.RemoveRange(entities);

        public abstract IReadOnlyList<TEntity> Find(Specification<TEntity> specification, int page = 0, int pageSize = 100);
    }
}
