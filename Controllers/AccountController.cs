using DriveBots.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DriveBots.Controllers
{
    public class AccountController : Controller
    {
        // Dependency injection for Identity services
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // Displays the registration form
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        // Handles registration form submission
        [HttpPost]
        public async Task<IActionResult> Register(SignupModel model)
        {
            // If the model is invalid, re-render the form with validation messages
            if (!ModelState.IsValid)
                return View("~/Views/Home/SignUp.cshtml", model);

            // Create a new ApplicationUser using provided email and password
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true, // Immediately confirm email for simplicity
                Address = "Not provided",
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            // Create the user in the database
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Assign the user to the "User" role
                await _userManager.AddToRoleAsync(user, "User");

                // Sign the user in
                await _signInManager.SignInAsync(user, isPersistent: false);

                // Redirect to User Dashboard after successful registration
                return RedirectToAction("Applicant", "Home");
            }

            // Display any errors encountered during user creation
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("Applicant",model);
        }

        // Displays the login form
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Handles login form submission
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // Validate the form data
            if (!ModelState.IsValid)
                // stay on the same page if fields are empty or invalid
                return View("~/Views/Home/Login.cshtml", model);

            // Attempt to find the user by email
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                // If user is not found, return invalid login attempt
                ModelState.AddModelError("", "Invalid username or password");
                Console.WriteLine("User not found.");
                return View("~/Views/Home/Login.cshtml", model);
            }

            // Attempt to sign the user in
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                Console.WriteLine("Login successful.");
                // Check user roles and redirect accordingly
                var roles = await _userManager.GetRolesAsync(user);
                Console.WriteLine("Roles: " + string.Join(", ", roles));
                if (roles.Contains("Admin"))
                {
                    return RedirectToAction("Admin", "Home");
                }
                else if (roles.Contains("User"))
                {
                    return RedirectToAction("Applicant", "Home");
                }
            }
            else
            {
                Console.WriteLine("Login failed.");
                Console.WriteLine($"LockedOut: {result.IsLockedOut}, NotAllowed: {result.IsNotAllowed}, Requires2FA: {result.RequiresTwoFactor}");
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }
            // Show error if login failed
            ModelState.AddModelError("", "Invalid username or password");
            return View("~/Views/Home/Login.cshtml", model);
        }

        // Handles user logout
        public async Task<IActionResult> Logout()
        {
            // Sign the user out of the session
            await _signInManager.SignOutAsync();

            // Redirect back to login page
            return RedirectToAction("Login", "Home");
        }
    }
}
