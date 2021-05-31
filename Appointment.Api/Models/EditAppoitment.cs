using System;
using System.ComponentModel.DataAnnotations;

namespace AppoitmentWebApp.Models
{
    public class EditAppoitment
    {
        [Required]
        [MaxLength(200)]
        public string AppoitmentName { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Guests { get; set; }
        
        [Required] 
        public int CreatedByUser { get; set; }
        
        [Required]
        public DateTimeOffset StartDate { get; set; }
        
        [Required]
        public DateTimeOffset EndDate { get; set; }
    }
}