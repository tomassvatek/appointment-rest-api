using System;
using System.Collections.Generic;
using System.Linq;
using Appoitment.Entities;
using AppoitmentWebApp.Models;

namespace AppoitmentWebApp.Mappers
{
    public static class AppoitmentMapper
    {
        public static Appointment Map(EditAppoitment appoitment)
            => new Appointment
            {
                //TODO: Let the GUID generation on DB
                EntityId = Guid.NewGuid(),
                Name = appoitment.AppoitmentName,
                Guests = appoitment.Guests,
                CreatedById = appoitment.CreatedByUser,
                StartDate = appoitment.StartDate,
                EndDate = appoitment.EndDate
            };

        public static IReadOnlyList<Models.Appoitment> Map(IReadOnlyList<Appointment> appointments)
            => appointments.Select(Map).ToList();

        private static Models.Appoitment Map(Appointment appointment)
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