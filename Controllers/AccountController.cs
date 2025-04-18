using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DriveBots.Models;
using DriveBots.Utilities;

namespace DriveBots.Controllers
{
    public class AccountController : Controller
    {

        /*private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: /Account/Signup
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        // POST: /Account/Signup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(SignupModel model)
        {
            if (!ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // You can add custom roles here if needed
                    // await _userManager.AddToRoleAsync(user, "Applicant");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }*/

        //GET SignUp
        public ActionResult Applicant()
        {
            return View("Applicant");
        }

        public string SignUp (SignupModel signupModel)
        {
            return "Results: Username = " + signupModel.Email + "PW = " + signupModel.Password;
        }
    }
}
