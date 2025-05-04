using System;
using System.ComponentModel.DataAnnotations;

namespace DriveBots.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public string UserId { get; set; } // Foreign key to AspNetUsers

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string AppointmentType { get; set; }
        public DateTime Date { get; set; }

        public string Location { get; set; }
    }

}
