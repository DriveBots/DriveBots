using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DriveBots.Models
{
    public class LicenseApplicationViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Residential Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Application Type")]
        public string ApplicationType { get; set; } // Learner or Renewal

        [Required]
        [Display(Name = "License Type/Class")]
        public string LicenceClass { get; set; }

        [Required]
        [Display(Name = "Upload ID")]
        public IFormFile IDDocument { get; set; } // Accepts PDF/IMG
    }
}
