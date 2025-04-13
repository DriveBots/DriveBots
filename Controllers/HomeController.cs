using DriveBots.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DriveBots.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View(); //Looks for "Login.cshtml" in Views/Home/
        }

        public IActionResult SignUp()
        {
            return View(); //Looks for "SignUp.cshtml" in Views/Home/
        }

        public IActionResult ContactUs()
        {
            return View(); //Looks for "ContactUs.cshtml" in Views/Home/
        }

        public IActionResult AboutUs()
        {
            return View(); //Looks for "Services.cshtml" in Views/Home/
        }
        public IActionResult Services()
        {
            return View(); //Looks for "Services.cshtml" in Views/Home/
        }
        
        public IActionResult ApplicationPage()
        {
            return View(); //Looks for "ApplicationPage.cshtml" in Views/Home/
        }

        public IActionResult Applicant()
        {
            return View(); //Looks for "Applicant.cshtml" in Views/Home/
        }

        public IActionResult Admin()
        {
            return View(); //Looks for "Admin.cshtml" in Views/Home/
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    /*public class AccountController : Controller
    {
        // GET: Account/Login
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                // Replace this with your actual authentication logic
                var user = await AuthenticateUser(model.Email, model.Password);

                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim("FullName", user.FullName),
                        new Claim(ClaimTypes.Role, "User"),
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe, // Persistent cookie if "Remember me" checked
                        ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30) // 30-day cookie
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Applicant", "Home"); // Redirect to dashboard
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }
            return View(model);
        }
        // GET: Account/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task<User> AuthenticateUser(string email, string password)
        {
            // Replace with your actual authentication logic
            // This is just a placeholder - you would typically check against a database
            if (email == "test@example.com" && password == "password")
            {
                return await Task.FromResult(new User
                {
                    Email = email,
                    FullName = "Test User"
                });
            }
            return null;
        }
    }

    public class User
    {
        public string Email { get; set; }
        public string FullName { get; set; }
    }*/
}
