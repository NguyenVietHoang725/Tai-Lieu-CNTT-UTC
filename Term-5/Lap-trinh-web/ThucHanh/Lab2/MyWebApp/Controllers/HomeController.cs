using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;

namespace MyWebApp.Controllers
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
            var menu = new List<MenuItem>
            {
                new MenuItem { Name = "Dashboard", Link = "/Admin/Dashboard" },
                new MenuItem { Name = "Students", Link = "/Admin/Student/List" },
                new MenuItem { Name = "Branches", Link = "/Admin/Student/Branches" },
                new MenuItem { Name = "Branches", Link = "/Admin/Student/Courses" }
            };

            return View(menu); 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
