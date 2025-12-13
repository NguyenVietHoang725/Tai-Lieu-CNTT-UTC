using Microsoft.AspNetCore.Mvc;

namespace MyWebApp.Controllers
{
    public class BranchesController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
