using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Appointment.DataAccess.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IReadOnlyList<Entities.Appointment>> GetAppointmentsAsync();
        Task<IReadOnlyList<Entities.Appointment>> GetAppointmentsAsync(int createdByUser);
        Task<Entities.Appointment> GetAppointmentAsync(Guid entityId);

        Task<Guid> CreateAppointmentAsync(Entities.Appointment appointment);
        Task UpdateAppointmentAsync(Guid entityId, Entities.Appointment appointment);
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
                .ToListAsync();

        public async Task<IReadOnlyList<Entities.Appointment>> GetAppointmentsAsync(int createdByUser)
            => await Context.Appointments
                .Include("CreatedBy")
                .Where(s => s.CreatedById == createdByUser)
                .ToListAsync();

        public async Task<Entities.Appointment> GetAppointmentAsync(Guid entityId)
            => await Context.Appointments
                .Include("CreatedBy")
                .SingleOrDefaultAsync(s => s.EntityId == entityId);

        public async Task<Guid> CreateAppointmentAsync(Entities.Appointment appointment)
        {
            await Context.Appointments.AddAsync(appointment);
            await Context.SaveChangesAsync();

            return appointment.EntityId;
        }

        public async Task UpdateAppointmentAsync(Guid entityId, Entities.Appointment appointment)
        {
            //TODO: Just pass the object and dont retrieve again
            var entity = await GetAppointmentAsync(entityId);

            entity.Name = appointment.Name;
            entity.Guests = appointment.Guests;
            entity.StartDate = appointment.StartDate;
            entity.EndDate = appointment.EndDate;

            await Context.SaveChangesAsync();
        }

        public async Task DeleteAppointmentAsync(Guid entityId)
        {
            //TODO: Just pass the object and dont retrieve again
            var appointment = await GetAppointmentAsync(entityId);
            
            Context.Appointments.Remove(appointment);
            await Context.SaveChangesAsync();
        }
    }
}