namespace Appoitment.DataAccess.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly AppointmentContext Context;

        public BaseRepository(AppointmentContext context)
        {
            Context = context;
        }
    }
}