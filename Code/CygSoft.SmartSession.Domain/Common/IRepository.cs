using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Common
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        IReadOnlyList<TEntity> Find(Specification<TEntity> specification, int page = 0, int pageSize = 100);
        TEntity Get(int id);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
    }
}
