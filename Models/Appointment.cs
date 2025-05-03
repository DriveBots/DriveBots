using System;
using System.ComponentModel.DataAnnotations;

namespace DriveBots.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string AppointmentType { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Status { get; set; } = "Pending"; // or Confirmed, Cancelled, etc.
    }
}
