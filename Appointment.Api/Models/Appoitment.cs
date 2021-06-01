using System;

namespace AppointmentWebApp.Models
{
    public class Appoitment
    {
        public Guid AppoitmentId { get; set; }
        public string AppoitmentName { get; set; }
        public string CreatedBy { get; set; }
        public string Guests { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}