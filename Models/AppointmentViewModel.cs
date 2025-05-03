using System;
using System.ComponentModel.DataAnnotations;

namespace DriveBots.Models
{
    public class AppointmentViewModel
    {
        [Required]
        [Display(Name = "Appointment Type")]
        public string AppointmentType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Select Date")]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Select Time")]
        public TimeSpan AppointmentTime { get; set; }

        [Required]
        [Display(Name = "Preferred Location")]
        public string Location { get; set; }
    }
}
