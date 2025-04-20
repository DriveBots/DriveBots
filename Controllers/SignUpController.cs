using Microsoft.AspNetCore.Mvc;
using DriveBots.Models;

namespace DriveBots.Controllers
{
    public class SignUpController : Controller
    {
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignupModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Applicant", "Home"); // Redirect after successful signup
            }

            return View(model);
        }
    }
}