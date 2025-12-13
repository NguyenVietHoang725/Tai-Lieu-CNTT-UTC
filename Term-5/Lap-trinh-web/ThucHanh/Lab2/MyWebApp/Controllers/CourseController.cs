using Microsoft.AspNetCore.Mvc;

namespace MyWebApp.Controllers
{
    [Route("Courses")]
    public class CourseController : Controller
    {
        [Route("List")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
