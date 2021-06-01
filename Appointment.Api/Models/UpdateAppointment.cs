using System;
using System.ComponentModel.DataAnnotations;

namespace AppointmentWebApp.Models
{
    public class UpdateAppointment
    {
        [Required]
        [MaxLength(200)]
        public string AppointmentName { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Guests { get; set; }
        
        [Required]
        public DateTimeOffset StartDate { get; set; }
        
        [Required]
        public DateTimeOffset EndDate { get; set; }
    }
}