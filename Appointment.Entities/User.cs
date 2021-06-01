using System;
using System.Collections.Generic;

namespace Appointment.Entities
{
    public class User
    {
        public int Id { get; set; }
        public Guid EntityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        
        public ICollection<Appointment> Appointments { get; set; }
    }
}