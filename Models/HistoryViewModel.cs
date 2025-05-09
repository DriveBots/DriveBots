using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.ComponentModel.DataAnnotations;

namespace DriveBots.Models
{
    public class HistoryViewModel
    {
        public List<LicenseApplication> Applications { get; set; } = new List<LicenseApplication>();
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        public string ErrorMessage { get; set; }
    }
}
