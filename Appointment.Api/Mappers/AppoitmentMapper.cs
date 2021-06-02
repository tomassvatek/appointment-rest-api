using System.Collections.Generic;
using System.Linq;
using AppointmentWebApp.Models;

namespace AppointmentWebApp.Mappers
{
    public static class AppointmentMapper
    {
        public static Appointment.Entities.Appointment Map(CreateAppointment appointment, int userId)
            => new Appointment.Entities.Appointment
            {
                Name = appointment.AppointmentName,
                Guests = appointment.Guests,
                CreatedById = userId,
                StartDate = appointment.StartDate,
                EndDate = appointment.EndDate
            };

        public static IReadOnlyList<Appoitment> Map(IReadOnlyList<Appointment.Entities.Appointment> appointments)
            => appointments.Select(Map).ToList();

        public static Appoitment Map(Appointment.Entities.Appointment appointment)
            => new Appoitment
            {
                AppoitmentId = appointment.EntityId,
                AppoitmentName = appointment.Name,
                Guests = appointment.Guests,
                CreatedBy = FormatName(appointment.CreatedBy?.FirstName, appointment.CreatedBy?.LastName),
                StartDate = appointment.StartDate,
                EndDate = appointment.EndDate
            };

        private static string FormatName(string firstName, string lastName)
            => $"{firstName} {lastName}";
    }
}