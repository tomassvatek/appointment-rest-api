using System;
using System.Threading.Tasks;

namespace Appointment.DataAccess.Repositories
{
    public interface IBaseRepository
    {
        Task<int> SaveAsync();
    }
    
    public abstract class BaseRepository : IBaseRepository, IDisposable
    {
        protected AppointmentContext Context;

        protected BaseRepository(AppointmentContext context)
        {
            Context = context;
        }

        public Task<int> SaveAsync()
            => Context.SaveChangesAsync();

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