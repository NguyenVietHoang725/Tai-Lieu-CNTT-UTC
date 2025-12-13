using Microsoft.AspNetCore.Mvc;

namespace MyWebApp.Controllers
{
    [Route("Subjects")]
    public class SubjectController : Controller
    {
        [Route("List")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
 