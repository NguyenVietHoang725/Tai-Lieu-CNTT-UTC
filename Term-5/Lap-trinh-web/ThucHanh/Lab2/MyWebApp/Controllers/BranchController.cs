using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;

namespace MyWebApp.Controllers
{
    [Route("Branches")]
    public class BranchController : Controller
    {
        [Route("List")]
        public IActionResult Index()
        {
            var branches = Enum.GetValues(typeof(Branch)).Cast<Branch>().ToList();
            return View(branches);
        }
    }
}
