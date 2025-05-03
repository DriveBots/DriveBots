using DriveBots.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace DriveBots.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppointmentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Appointment()
        {
            return View(new AppointmentViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Appointment(AppointmentViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var appointment = new Appointment
            {
                UserId = userId,
                AppointmentType = model.AppointmentType,
                AppointmentDate = model.AppointmentDate.Date + model.AppointmentTime,
                Location = model.Location,
                Status = "Pending"
            }; 

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Appointment booked successfully!";
            return RedirectToAction("MyAppointments");
        }

        public IActionResult MyAppointments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appointments = _context.Appointments
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.AppointmentDate)
                .ToList();

            return View(appointments);
        }
    }
}
