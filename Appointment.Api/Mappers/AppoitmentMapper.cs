using System;
using System.Collections.Generic;
using System.Linq;
using AppointmentWebApp.Models;
using Appointment.Entities;

namespace AppointmentWebApp.Mappers
{
    public static class AppointmentMapper
    {
        public static Appointment.Entities.Appointment Map(CreateAppointment appointment, int userId)
            => new Appointment.Entities.Appointment
            {
                //TODO: Let the GUID generation on DB
                EntityId = Guid.NewGuid(),
                Name = appointment.AppointmentName,
                Guests = appointment.Guests,
                CreatedById = userId,
                StartDate = appointment.StartDate,
                EndDate = appointment.EndDate
            };
        
        public static Appointment.Entities.Appointment Map(UpdateAppointment appointment)
            => new Appointment.Entities.Appointment
            {
                //TODO: Let the GUID generation on DB
                Name = appointment.AppointmentName,
                Guests = appointment.Guests,
                StartDate = appointment.StartDate,
                EndDate = appointment.EndDate
            };

        public static IReadOnlyList<Models.Appoitment> Map(IReadOnlyList<Appointment.Entities.Appointment> appointments)
            => appointments.Select(Map).ToList();

        public static Models.Appoitment Map(Appointment.Entities.Appointment appointment)
            => new Models.Appoitment
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