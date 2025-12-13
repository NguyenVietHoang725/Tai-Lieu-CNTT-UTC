using Microsoft.AspNetCore.Mvc;

namespace DemoController.Controllers
{
    public class HelloWorldController : Controller
    {
        //
        // GET: /HelloWorld/
        public IActionResult Index()
        {
            ViewData["Name"] = "James Bond";
            ViewBag.Age = 30;
            TempData["Email"] = "jb@utc.edu.vn";
            return View();
        }

        //
        // GET: /HelloWorld/Welcome/
        public IActionResult Welcome()
        {
            return View();
        }

        //
        // GET: /HelloWorld/Sample/
        public IActionResult Sample()
        {
            return View("Index");
        }

        //  
        // GET: /HelloWorld/Redirect/
        public IActionResult Redirect()
        {
            return RedirectToAction("Index");
        }
    }
}
