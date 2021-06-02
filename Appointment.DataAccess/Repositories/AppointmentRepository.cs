using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Appointment.DataAccess.Repositories
{
    public interface IAppointmentRepository : IBaseRepository
    {
        Task<IReadOnlyList<Entities.Appointment>> GetAppointmentsAsync();
        Task<IReadOnlyList<Entities.Appointment>> GetAppointmentsAsync(int createdByUser);
        Task<Entities.Appointment> GetAppointmentAsync(Guid entityId);

        Task<Entities.Appointment> CreateAppointmentAsync(Entities.Appointment appointment);
        void UpdateAppointment(Entities.Appointment appointment);
        Task DeleteAppointmentAsync(Guid entityId);
    }

    public class AppointmentRepository : BaseRepository, IAppointmentRepository
    {
        public AppointmentRepository(AppointmentContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Appointment.Entities.Appointment>> GetAppointmentsAsync()
            => await Context.Appointments
                .Include("CreatedBy")
                .AsNoTracking()
                .ToListAsync();

        public async Task<IReadOnlyList<Entities.Appointment>> GetAppointmentsAsync(int createdByUser)
            => await Context.Appointments
                .Include("CreatedBy")
                .Where(s => s.CreatedById == createdByUser)
                .AsNoTracking()
                .ToListAsync();

        public async Task<Entities.Appointment> GetAppointmentAsync(Guid entityId)
            => await Context.Appointments
                .Include("CreatedBy")
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.EntityId == entityId);

        public async Task<Entities.Appointment> CreateAppointmentAsync(Entities.Appointment appointment)
        {
            await Context.Appointments.AddAsync(appointment);
            return appointment;
        }

        public void UpdateAppointment(Entities.Appointment appointment)
        {
             Context.Appointments.Attach(appointment);
             Context.Entry(appointment).State = EntityState.Modified;
        }

        public async Task DeleteAppointmentAsync(Guid entityId)
        {
            var appointment = await GetAppointmentAsync(entityId);
            Context.Appointments.Remove(appointment);
        }
    }
}