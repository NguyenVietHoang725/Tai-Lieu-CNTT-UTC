using Library_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Manager.Controllers
{
    [Route("Tai-lieu-Tac-gia")]
    public class TaiLieu_TacGiaController : Controller
    {
        private readonly QlthuVienContext _context;

        public TaiLieu_TacGiaController(QlthuVienContext context)
        {
            _context = context;
        }

        // GET: TaiLieu_TacGia
        [Route("Danh-sach")]
        public IActionResult Index(int? page, string searchString)
        {
            int pageNumber = page ?? 1;
            int pageSize = 6;

            // 1. Giữ IQueryable để có thể kết hợp điều kiện và phân trang
            IQueryable<TTaiLieuTacGia> query = _context.TTaiLieuTacGia
                .Include(t => t.MaTgNavigation)
                .Include(t => t.MaTlNavigation);

            // 2. Nếu có chuỗi tìm kiếm
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(t =>
                    t.MaTlNavigation.TenTl.Contains(searchString) ||    // tên tài liệu
                    t.MaTgNavigation.Ten.Contains(searchString) ||    // tên tác giả
                    t.MaTgNavigation.HoDem.Contains(searchString) ||  // họ đệm tác giả
                    t.MaTl.Contains(searchString) ||                    // mã tài liệu
                    t.MaTg.Contains(searchString));                     // mã tác giả
            }

            // 3. Sắp xếp
            query = query.OrderBy(t => t.MaTl);

            // 4. Chuyển sang PagedList
            var pagedList = new PagedList<TTaiLieuTacGia>(query, pageNumber, pageSize);

            // 5. Lưu giá trị tìm kiếm để hiển thị lại trên View
            ViewBag.CurrentFilter = searchString;

            return View(pagedList);
        }

        // GET: TaiLieu_TacGia/Details/5
        [Route("Chi-tiet/{id}")]
        public async Task<IActionResult> Details(string id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tTaiLieuTacGia = await _context.TTaiLieuTacGia
                .Where(t => t.MaTl == id)
                .Include(t => t.MaTgNavigation)
                .Include(t => t.MaTlNavigation)
                .ToListAsync();

            if (tTaiLieuTacGia == null)
            {
                return NotFound();
            }

            ViewBag.ReturnUrl = returnUrl;

            return View(tTaiLieuTacGia);
        }

        // GET: TaiLieu_TacGia/Create
        [Route("Tao-moi")]
        public IActionResult Create()
        {
            ViewData["MaTg"] = new SelectList(_context.TTacGia, "MaTg", "MaTg");
            ViewData["MaTl"] = new SelectList(_context.TTaiLieu, "MaTl", "MaTl");
            return View();
        }

        // POST: TaiLieu_TacGia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Tao-moi")]
        public async Task<IActionResult> Create([Bind("MaTl,MaTg,VaiTro")] TTaiLieuTacGia tTaiLieuTacGia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tTaiLieuTacGia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaTg"] = new SelectList(_context.TTacGia, "MaTg", "MaTg", tTaiLieuTacGia.MaTg);
            ViewData["MaTl"] = new SelectList(_context.TTaiLieu, "MaTl", "MaTl", tTaiLieuTacGia.MaTl);
            return View(tTaiLieuTacGia);
        }

        // GET: TaiLieu_TacGia/Edit/5
        [Route("Chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tTaiLieuTacGia = await _context.TTaiLieuTacGia.FindAsync(id);
            if (tTaiLieuTacGia == null)
            {
                return NotFound();
            }
            ViewData["MaTg"] = new SelectList(_context.TTacGia, "MaTg", "MaTg", tTaiLieuTacGia.MaTg);
            ViewData["MaTl"] = new SelectList(_context.TTaiLieu, "MaTl", "MaTl", tTaiLieuTacGia.MaTl);
            return View(tTaiLieuTacGia);
        }

        // POST: TaiLieu_TacGia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id, [Bind("MaTl,MaTg,VaiTro")] TTaiLieuTacGia tTaiLieuTacGia)
        {
            if (id != tTaiLieuTacGia.MaTl)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tTaiLieuTacGia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TTaiLieuTacGiaExists(tTaiLieuTacGia.MaTl))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaTg"] = new SelectList(_context.TTacGia, "MaTg", "MaTg", tTaiLieuTacGia.MaTg);
            ViewData["MaTl"] = new SelectList(_context.TTaiLieu, "MaTl", "MaTl", tTaiLieuTacGia.MaTl);
            return View(tTaiLieuTacGia);
        }

        // GET: TaiLieu_TacGia/Delete/5
        [Route("Xoa/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tTaiLieuTacGia = await _context.TTaiLieuTacGia
                .Include(t => t.MaTgNavigation)
                .Include(t => t.MaTlNavigation)
                .FirstOrDefaultAsync(m => m.MaTl == id);
             
            if (tTaiLieuTacGia == null)
            {
                return NotFound();
            }

            return View(tTaiLieuTacGia);
        }

        // POST: TaiLieu_TacGia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Xoa/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tTaiLieuTacGia = await _context.TTaiLieuTacGia.FindAsync(id);
            if (tTaiLieuTacGia != null)
            {
                _context.TTaiLieuTacGia.Remove(tTaiLieuTacGia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TTaiLieuTacGiaExists(string id)
        {
            return _context.TTaiLieuTacGia.Any(e => e.MaTl == id);
        }
    }
}
