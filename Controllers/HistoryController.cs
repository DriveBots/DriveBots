using DriveBots.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace DriveBots.Controllers
{
    [Authorize]
    public class HistoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HistoryController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public HistoryController(
            ApplicationDbContext context,
            ILogger<HistoryController> logger,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Applications()
        {
            try
            {
                // Get current user with additional checks
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    _logger.LogWarning("User not found - possibly not logged in");
                    return RedirectToAction("Login", "Account");
                }

                var userID = user.Id;
                _logger.LogInformation("Loading history for user {UserId}", userID);

                var model = new HistoryViewModel
                {
                    Applications = await _context.LicenseApplications
            .Where(a => a.UserId == user.Id)
            .ToListAsync() ?? new List<LicenseApplication>(),
                    Appointments = await _context.Appointments
            .Where(a => a.UserId == user.Id)
            .ToListAsync() ?? new List<Appointment>()
                };

                // Immediate verification before passing to view
                Debug.Assert(model != null, "Model is null before view rendering");
                Console.WriteLine($"Model created with {model.Applications.Count} apps");

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading history data");

                // Return empty model with error message
                var errorModel = new HistoryViewModel
                {
                    ErrorMessage = "An error occurred while loading your history. Please try again later."
                };

                return View(errorModel);
            }
        }
    }

}
