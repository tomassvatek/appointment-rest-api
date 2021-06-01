using System;
using System.ComponentModel.DataAnnotations;

namespace AppointmentWebApp.Models
{
    public class CreateAppointment
    {
        [Required]
        [MaxLength(200)]
        public string AppointmentName { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Guests { get; set; }
        
        [Required] 
        public Guid CreatedByUser { get; set; }
        
        [Required]
        public DateTimeOffset StartDate { get; set; }
        
        [Required]
        public DateTimeOffset EndDate { get; set; }
    }
}