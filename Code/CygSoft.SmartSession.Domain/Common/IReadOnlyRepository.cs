using CygSoft.SmartSession.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.Common
{
    public interface IReadOnlyRepository<TEntity> where TEntity : IEntity
    {
        TEntity Get(int id);
        IReadOnlyList<TEntity> Find(object criteria);
    }
}
