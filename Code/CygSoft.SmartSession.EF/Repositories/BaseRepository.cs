using System;

namespace CygSoft.SmartSession.EF.Repositories
{
    public abstract class BaseRepository : IDisposable
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
