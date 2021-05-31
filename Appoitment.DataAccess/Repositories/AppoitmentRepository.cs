using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appoitment.Entities;
using Microsoft.EntityFrameworkCore;

namespace Appoitment.DataAccess.Repositories
{
    public interface IAppoitmentRepository
    {
        Task<IReadOnlyList<Appointment>> GetAppointments();
        Task<int> CreateAppointment(Appointment appointment);
        Task UpdateAppointment(int appointmentId, Appointment appointment);
        Task DeleteAppoitment(int appointmentId);
    }

    public class AppoitmentRepository : BaseRepository, IAppoitmentRepository
    {
        public AppoitmentRepository(AppointmentContext context) : base(context)
        {
        }

        // TODO: Should I eager load the user?
        public async Task<IReadOnlyList<Appointment>> GetAppointments()
            => await Context.Appointments
                .Include("CreatedBy")
                .ToListAsync();

        public async Task<int> CreateAppointment(Appointment appointment)
        {
            await Context.Appointments.AddAsync(appointment);
            await Context.SaveChangesAsync();

            return appointment.Id;
        }

        public async Task UpdateAppointment(int appointmentId, Appointment appointment)
        {
            var update = await Context.Appointments.SingleOrDefaultAsync(s => s.Id == appointmentId);
            if (appointment == null)
                throw new Exception($"Appoitment '{appointmentId}' not found.");

            update.Name = appointment.Name;
            update.Guests = appointment.Guests;
            update.StartDate = appointment.StartDate;
            update.EndDate = appointment.EndDate;

            await Context.SaveChangesAsync();
        }

        public async Task DeleteAppoitment(int appointmentId)
        {
            var appointment = await Context.Appointments.SingleOrDefaultAsync(s => s.Id == appointmentId);
            if (appointment == null)
                throw new Exception($"Appoitment '{appointmentId}' not found.");

            Context.Appointments.Remove(appointment);
            await Context.SaveChangesAsync();
        }
    }
}