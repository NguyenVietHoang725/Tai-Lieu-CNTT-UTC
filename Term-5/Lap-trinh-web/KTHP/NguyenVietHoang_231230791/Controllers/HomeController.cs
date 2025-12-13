using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NguyenVietHoang_231230791.Models;
using System.Diagnostics;

namespace NguyenVietHoang_231230791.Controllers
{
    public class HomeController : Controller
    {
        private readonly VanTai2512V2Context _context;

        public HomeController(VanTai2512V2Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Xes;
            return View(data);
        }

        public IActionResult PhanTrangXe(int? page)
        {
            int pageSize = 4;
            int pageNumber = page ?? 1;

            var data = _context.Xes
                        .Include(x => x.MaCongTyNavigation) 
                        .OrderByDescending(x => x.MaCongTyNavigation.TenCongTy)
                        .Skip((pageNumber - 1) * pageSize) 
                        .Take(pageSize) 
                        .ToList();

            return PartialView("_DanhSachXe", data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.MaCongTy = new SelectList(_context.CongTies.ToList(), "MaCongTy", "TenCongTy");
            ViewBag.MaLoaiXe = new SelectList(_context.LoaiXes.ToList(), "MaLoaiXe", "TenLoaiXe");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Xe xe)
        {
            if (ModelState.IsValid)
            {
                _context.Xes.Add(xe);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaCongTy = new SelectList(_context.CongTies.ToList(), "MaCongTy", "TenCongTy");
            ViewBag.MaLoaiXe = new SelectList(_context.LoaiXes.ToList(), "MaLoaiXe", "TenLoaiXe");

            return View(xe);
        }
    }
}
