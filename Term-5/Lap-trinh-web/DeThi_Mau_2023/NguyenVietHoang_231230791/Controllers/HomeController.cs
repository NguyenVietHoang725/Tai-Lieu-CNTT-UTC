using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NguyenVietHoang_231230791.Models;

namespace NguyenVietHoang_231230791.Controllers
{
    public class HomeController : Controller
    {
        private readonly QlhangHoaContext _context;

        public HomeController(QlhangHoaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.HangHoas.Where(h => h.Gia >= 100).ToList();
            return View(data);
        }

        [HttpGet]
        public IActionResult LocHangTheoLoai(int maloai)
        {
            var data = _context.HangHoas
                .Where(h => h.MaLoai == maloai && h.Gia >= 100)
                .ToList();

            return PartialView("_DanhSachHangHoa", data);
        }

        // GET: Hiển thị form thêm hàng hóa mới
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.MaLoai = _context.LoaiHangs.ToList();
            return View();
        }

        // POST: Xử lý thêm mới hàng hóa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HangHoa hanghoa)
        {
            if (ModelState.IsValid)
            {
                _context.HangHoas.Add(hanghoa);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoai = _context.LoaiHangs.ToList();
            return View(hanghoa);
        }



    }
}
