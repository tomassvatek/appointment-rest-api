using System;

namespace Appointment.DataAccess.Repositories
{
    public abstract class BaseRepository : IDisposable
    {
        protected AppointmentContext Context;

        protected BaseRepository(AppointmentContext context)
        {
            Context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        private void Dispose(bool disposing)
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