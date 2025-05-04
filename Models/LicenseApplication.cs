using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriveBots.Models
{
    public class LicenseApplication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }  // Navigation property

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string ApplicationType { get; set; }

        [Required]
        public string LicenceClass { get; set; }

        public string UploadedFilePath { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }

}
