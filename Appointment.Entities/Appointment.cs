using System;

namespace Appointment.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public Guid EntityId { get; set; }

        public string Name { get; set; }
        public User CreatedBy { get; set; }
        public int CreatedById { get; set; }

        public string Guests { get; set; }
        
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}