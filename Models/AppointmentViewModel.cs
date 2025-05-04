using System;
using System.ComponentModel.DataAnnotations;

namespace DriveBots.Models
{
    public class AppointmentViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string AppointmentType { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public string Location { get; set; }

        public List<Appointment> UpcomingAppointments { get; set; } = new List<Appointment>();
    }
}
