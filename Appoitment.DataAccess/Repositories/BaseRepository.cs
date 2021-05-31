using System;

namespace Appoitment.DataAccess.Repositories
{
    public abstract class BaseRepository : IDisposable
    {
        protected AppointmentContext Context;

        public BaseRepository(AppointmentContext context)
        {
            Context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (Context != null)
            {
                Context.Dispose();
                Context = null;
            }
        }
    }
}