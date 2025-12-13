using System.Diagnostics;
using DeThi210702.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeThi210702.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OnlineShopContext _context; // 1. Khai báo biến Context

        // 2. Inject Context vào Constructor
        public HomeController(ILogger<HomeController> logger, OnlineShopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // 3. Truy vấn: Sắp xếp theo UnitPrice tăng dần (nhỏ nhất lên đầu) -> Lấy 4 cái
            var top4Products = _context.Products
                                .OrderBy(p => p.UnitPrice)
                                .Take(4)
                                .ToList();

            // 4. Truyền dữ liệu sang View
            return View(top4Products);
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