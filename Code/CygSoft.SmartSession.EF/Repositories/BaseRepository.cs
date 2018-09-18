using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections.Generic;

namespace CygSoft.SmartSession.EF.Repositories
{
    public abstract class BaseRepository<TEntity> 
        : IDisposable where TEntity : Entity
    {
        private bool isDisposed = false;
        protected SmartSessionContext context;

        public BaseRepository(SmartSessionContext context)
        {
            this.context = context;
        }

        ~BaseRepository()
        {
            Dispose(false);
        }

        public abstract TEntity Get(int id);

        public abstract void Add(TEntity entity); // add but don't save

        public abstract void Update(TEntity entity); // update but don't save

        public abstract void Remove(TEntity entity); // remove but don't save
        public abstract void Remove(int id);

        public abstract IReadOnlyList<TEntity> Find(Specification<TEntity> specification, int page = 0, int pageSize = 100);
        //{
            //using (ISession session = SessionFactory.OpenSession())
            //{
            //    //return session.Query<T>()
            //    //    .Where(specification.ToExpression())
            //    //    .Skip(page * pageSize)
            //    //    .Take(pageSize)
            //    //    .ToList();
            //}
            //throw new NotImplementedException();
        //}

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // make sure we have not already been disposed!
            if (!isDisposed)
            {
                if (disposing)
                {
                    // cleanup managed objects by calling their
                    // dispose methods.
                    context.Dispose();
                }
                // cleanup unmanaged objects...
                // should not try to cleanup managed objects here, since the 
                // garbage collector might have disposed of them already...
            }
            isDisposed = true;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
