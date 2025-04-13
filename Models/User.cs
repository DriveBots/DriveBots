using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DriveBots.Models
{
    public class User : IdentityUser
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLogin { get; set; }
        public string Role { get; set; } = "Applicant"; // Default role
    }
}
