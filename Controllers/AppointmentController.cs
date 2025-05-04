using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DriveBots.Models;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> Appointment()
    {
        var userId = _userManager.GetUserId(User);

        var upcomingAppointments = await _context.Appointments
            .Where(a => a.UserId == userId && a.Date >= DateTime.Today)
            .ToListAsync();

        var model = new AppointmentViewModel
        {
            UpcomingAppointments = upcomingAppointments
        };

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Appointment(AppointmentViewModel model)
    {
        if (ModelState.IsValid)
        {
            var existingCount = await _context.Appointments
                .Where(a => a.Date == model.Date && a.Location == model.Location)
                .CountAsync();

            if (existingCount >= 50)
            {
                ModelState.AddModelError("", "This location is fully booked for the selected date.");
                return View(model);
            }

            var userId = _userManager.GetUserId(User);
            var appointment = new Appointment
            {
                UserId = userId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                AppointmentType = model.AppointmentType,
                Date = model.Date,
                Location = model.Location
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Appointment");
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CancelAppointment(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
        {
            return NotFound();
        }

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();

        return RedirectToAction("Appointment");
    }

}
