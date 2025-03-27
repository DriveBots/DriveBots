using DriveBots.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
}
