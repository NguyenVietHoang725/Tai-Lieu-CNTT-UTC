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
    [Route("Ngon-ngu")]
    public class NgonNguController : Controller
    {
        private readonly QlthuVienContext _context;

        public NgonNguController(QlthuVienContext context)
        {
            _context = context;
        }

        // GET: TNgonNgu
        [Route("Danh-sach")]
        public IActionResult Index(int? page, string searchString)
        {
            var pageNumber = page ?? 1;
            var pageSize = 6;

            // Giữ ở dạng IQueryable
            IQueryable<TNgonNgu> ngonNgus = _context.TNgonNgu;

            // Tìm kiếm
            if (!string.IsNullOrEmpty(searchString))
            {
                ngonNgus = ngonNgus.Where(nn =>
                    nn.TenNn.ToLower().Contains(searchString.ToLower()) ||
                    nn.MaNn.ToLower().Contains(searchString.ToLower()));
            }

            // Sắp xếp
            ngonNgus = ngonNgus.OrderBy(nn => nn.MaNn);

            // Phân trang
            var pagedNgonNgus = new PagedList<TNgonNgu>(ngonNgus, pageNumber, pageSize);
            // hoặc nếu dùng ToPagedList() thì:
            // var pagedNgonNgus = ngonNgus.ToPagedList(pageNumber, pageSize);

            // Giữ lại giá trị tìm kiếm để hiển thị lại trong View
            ViewBag.CurrentFilter = searchString;

            return View(pagedNgonNgus);
        }

        // GET: TNgonNgus/Details/5
        [Route("Chi-tiet/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNgonNgu = await _context.TNgonNgu
                .FirstOrDefaultAsync(m => m.MaNn == id);
            if (tNgonNgu == null)
            {
                return NotFound();
            }

            return View(tNgonNgu);
        }

        // GET: TNgonNgus/Create
        [Route("Tao-moi")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TNgonNgus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Tao-moi")]
        public async Task<IActionResult> Create([Bind("MaNn,TenNn")] TNgonNgu tNgonNgu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tNgonNgu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tNgonNgu);
        }

        // GET: TNgonNgus/Edit/5
        [Route("Chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNgonNgu = await _context.TNgonNgu.FindAsync(id);
            if (tNgonNgu == null)
            {
                return NotFound();
            }
            return View(tNgonNgu);
        }

        // POST: TNgonNgus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id, [Bind("MaNn,TenNn")] TNgonNgu tNgonNgu)
        {
            if (id != tNgonNgu.MaNn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tNgonNgu);
                    await _context.SaveChangesAsync();

                    // THÀNH CÔNG: Set TempData
                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = "Thông tin Ngôn ngữ đã được cập nhật thành công.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TNgonNguExists(tNgonNgu.MaNn))
                    {
                        return NotFound();
                    }
                    else
                    {
                        // LỖI XUNG ĐỘT: Set TempData
                        TempData["StatusMessage"] = "danger";
                        TempData["Message"] = "Lỗi xung đột dữ liệu. Vui lòng thử lại.";
                    }
                }
                catch (Exception ex)
                {
                    // LỖI HỆ THỐNG: Set TempData
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = "Lỗi hệ thống khi lưu: " + ex.Message;
                }
            }
            else
            {
                // LỖI VALIDATION: Set TempData
                TempData["StatusMessage"] = "danger";
                var errors = ModelState.Where(x => x.Value.Errors.Any())
                   .Select(x => $"{x.Key}: {string.Join("; ", x.Value.Errors.Select(e => e.ErrorMessage))}");
                TempData["Message"] = $"Dữ liệu không hợp lệ. Vui lòng kiểm tra: <ul><li>{string.Join("</li><li>", errors)}</li></ul>";
            }

            // LUÔN LUÔN: Return View để hiển thị thông báo
            return View(tNgonNgu);
        }

        // GET: TNgonNgus/Delete/5
        [Route("Xoa/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNgonNgu = await _context.TNgonNgu
                .FirstOrDefaultAsync(m => m.MaNn == id);
            if (tNgonNgu == null)
            {
                return NotFound();
            }

            return View(tNgonNgu);
        }

        // POST: TNgonNgus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Xoa/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tNgonNgu = await _context.TNgonNgu.FindAsync(id);
            if (tNgonNgu != null)
            {
                _context.TNgonNgu.Remove(tNgonNgu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TNgonNguExists(string id)
        {
            return _context.TNgonNgu.Any(e => e.MaNn == id);
        }
    }
}
