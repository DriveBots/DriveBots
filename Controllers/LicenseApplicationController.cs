using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DriveBots.Models;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace DriveBots.Controllers
{
    public class LicenseApplicationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;

        public LicenseApplicationController(
             UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            IWebHostEnvironment env,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _env = env;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult ApplicationPage()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
                return NotFound();

            var model = new LicenseApplicationViewModel
            {
                FirstName = user?.FirstName,
                LastName = user?.LastName,
                Email = user?.Email,
                Address = user?.Address
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ApplicationPage(LicenseApplicationViewModel model)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Prevent duplicate applications
                bool alreadyApplied = _context.LicenseApplications.Any(a =>
                    a.UserId == userId &&
                    a.ApplicationType == model.ApplicationType &&
                    a.LicenceClass == model.LicenceClass);


                if (alreadyApplied)
                {
                    ModelState.AddModelError("", "You've already submitted this application.");
                    return View(model);
                }

                if (!ModelState.IsValid)
                {
                    // Log validation errors
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                    }
                    return View(model);
                }

                // Save file
                string filePath = null;
                if (model.IDDocument != null)
                {
                    var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadsFolder);
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.IDDocument.FileName;
                    var fullPath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await model.IDDocument.CopyToAsync(fileStream);
                    }

                    filePath = "/uploads/" + uniqueFileName;
                }

                // Save to DB
                var application = new LicenseApplication
                {
                    UserId = userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Address = model.Address,
                    ApplicationType = model.ApplicationType,
                    LicenceClass = model.LicenceClass,
                    UploadedFilePath = filePath,
                    SubmittedAt = DateTime.Now
                };

                _context.LicenseApplications.Add(application);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    // Send confirmation email
                    await _emailSender.SendEmailAsync(model.Email, "Application Received",
                        $"Thank you {model.FirstName}, your {model.ApplicationType} application has been received.");

                    TempData["Success"] = "Application submitted successfully!";
                }
                else
                {
                    TempData["Error"] = "Failed to save application to database.";
                }
                return RedirectToAction("ApplicationPage", "Home");
            }

            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception: {ex.Message}");
                TempData["Error"] = "An error occurred while processing your application.";
                return View(model);
            }
        }
    }
}
