using Library_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace Library_Manager.Controllers
{
    [Route("Tac-gia")]
    public class TacGiaController : Controller
    {
        private readonly QlthuVienContext _context;

        public TacGiaController(QlthuVienContext context)
        {
            _context = context;
        }

        // =======================================================
        // GET: TacGia
        // =======================================================
        [Route("Danh-sach")]
        public IActionResult Index(int? page, string searchString, string maQg)
        {
            var pageNumber = page ?? 1;
            var pageSize = 6;

            var countries = _context.TQuocGia.Select(q => new { q.MaQg, q.TenQg }).ToList();
            ViewBag.Countries = countries.ToDictionary(c => c.MaQg, c => c.TenQg);
            ViewBag.SelectedCountry = maQg;

            IQueryable<TTacGia> tacGias = _context.TTacGia
                .Include(tg => tg.MaQgNavigation);

            // ÁP DỤNG LỌC
            if (!string.IsNullOrEmpty(maQg))
            {
                tacGias = tacGias.Where(tg => tg.MaQg == maQg);
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                tacGias = tacGias.Where(tg =>
                    tg.Ten.ToLower().Contains(searchString.ToLower()) ||
                    tg.HoDem.ToLower().Contains(searchString.ToLower()) ||
                    (tg.MaQgNavigation != null && tg.MaQgNavigation.TenQg.ToLower().Contains(searchString.ToLower())) ||
                    tg.MaTg.Contains(searchString));
            }

            // PHÂN TRANG
            tacGias = tacGias.OrderBy(tg => tg.MaTg);
            var pagedTacGias = new PagedList<TTacGia>(tacGias, pageNumber, pageSize);

            // LƯU TRẠNG THÁI LỌC CHO PHÂN TRANG
            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentMaQg = maQg;

            return View(pagedTacGias);
        }

        // =======================================================
        // GET: TacGia/Details/5
        // =======================================================
        [Route("Chi-tiet/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) { return NotFound(); }
            var tTacGium = await _context.TTacGia
                .Include(t => t.MaQgNavigation)
                .FirstOrDefaultAsync(m => m.MaTg == id);
            if (tTacGium == null) { return NotFound(); }
            return View(tTacGium);
        }

        // =======================================================
        // GET: TacGia/Create
        // =======================================================
        [Route("Tao-moi")]
        public async Task<IActionResult> Create()
        {
            ViewData["MaQg"] = new SelectList(_context.TQuocGia.OrderBy(q => q.TenQg), "MaQg", "TenQg");
            return View();
        }

        // =======================================================
        // POST: TacGia/Create
        // =======================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Tao-moi")]
        public async Task<IActionResult> Create([Bind("MaQg,HoDem,Ten")] TTacGia tTacGium)
        {
            ViewData["MaQg"] = new SelectList(_context.TQuocGia.OrderBy(q => q.TenQg), "MaQg", "TenQg", tTacGium.MaQg);

            if (ModelState.IsValid)
            {
                try
                {
                    var newMaTgParam = new SqlParameter("@NewMaTg", SqlDbType.NVarChar, 50) { Direction = ParameterDirection.Output };
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC SP_GenerateNewMaTg @MaQg, @NewMaTg OUTPUT",
                        new SqlParameter("@MaQg", tTacGium.MaQg),
                        newMaTgParam
                    );
                    var newMaTgValue = newMaTgParam.Value;
                    if (newMaTgValue == DBNull.Value || string.IsNullOrEmpty(newMaTgValue.ToString()))
                    {
                        throw new InvalidOperationException("Không thể sinh mã tác giả mới. Có thể đã đạt giới hạn 999 trong năm nay cho Quốc gia này.");
                    }
                    tTacGium.MaTg = newMaTgValue.ToString();
                    _context.Add(tTacGium);
                    await _context.SaveChangesAsync();

                    TempData["StatusMessage"] = "success";
                    // SỬA: Dùng <strong> thay vì **
                    TempData["Message"] = $"Đã tạo mới Tác giả: <strong>{tTacGium.HoDem} {tTacGium.Ten}</strong> với Mã TG: <strong>{tTacGium.MaTg}</strong>";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["StatusMessage"] = "danger";
                    string errorMessage = ex.InnerException?.Message ?? ex.Message;
                    if (errorMessage.Contains("ExecuteSqlRawAsync failed with the following error:"))
                    {
                        errorMessage = errorMessage.Substring(errorMessage.IndexOf("ExecuteSqlRawAsync failed with the following error:") + 52).Trim();
                    }
                    if (errorMessage.Contains("RAISERROR"))
                    {
                        errorMessage = errorMessage.Substring(errorMessage.IndexOf("RAISERROR") + 10).Trim();
                    }
                    // SỬA: Dùng <strong>
                    TempData["Message"] = "Lỗi hệ thống khi tạo mới: <strong>" + errorMessage + "</strong>";
                }
            }
            else
            {
                TempData["StatusMessage"] = "danger";
                var errors = ModelState.Where(x => x.Value.Errors.Any())
                   .Select(x => $"{x.Key}: {string.Join("; ", x.Value.Errors.Select(e => e.ErrorMessage))}").ToList();

                if (tTacGium.MaQg == null && ModelState.ContainsKey("MaQg") && ModelState["MaQg"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    errors.Add("Quốc gia: Vui lòng chọn một Quốc gia.");
                }
                // SỬA: Dùng <ul><li> và <strong>
                TempData["Message"] = $"Dữ liệu không hợp lệ. Vui lòng kiểm tra: <ul><li><strong>{string.Join("</strong></li><li><strong>", errors)}</strong></li></ul>";
            }
            // QUAN TRỌNG: Trả về View để hiển thị lỗi ngay trên trang
            return View(tTacGium);
        }

        // =======================================================
        // GET: TacGia/Delete/5
        // =======================================================
        [Route("Xoa/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) { return NotFound(); }
            // Cần Include MaQgNavigation để hiển thị Tên Quốc gia trong View
            var tTacGium = await _context.TTacGia
                .Include(t => t.MaQgNavigation)
                .FirstOrDefaultAsync(m => m.MaTg == id);
            if (tTacGium == null) { return NotFound(); }
            return View(tTacGium);
        }

        // =======================================================
        // POST: TacGia/Delete/5
        // =======================================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Xoa/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tTacGium = await _context.TTacGia.FindAsync(id);

            if (tTacGium != null)
            {
                try
                {
                    _context.TTacGia.Remove(tTacGium);
                    await _context.SaveChangesAsync();

                    TempData["StatusMessage"] = "success";
                    // SỬA: Dùng <strong>
                    TempData["Message"] = $"Đã xóa Tác giả có Mã: <strong>{id}</strong> thành công.";
                }
                catch (DbUpdateException ex)
                {
                    TempData["StatusMessage"] = "danger";
                    // SỬA: Dùng <strong>
                    TempData["Message"] = $"Không thể xóa tác giả <strong>{id}</strong> vì đang có tài liệu tham chiếu đến tác giả này. Vui lòng xóa các tài liệu liên quan trước.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["StatusMessage"] = "danger";
                    // SỬA: Dùng <strong>
                    TempData["Message"] = $"Lỗi hệ thống khi xóa: <strong>{ex.Message}</strong>";
                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TTacGiumExists(string id)
        {
            return _context.TTacGia.Any(e => e.MaTg == id);
        }

        // =======================================================
        // GET: TacGia/Edit/5
        // =======================================================
        [Route("Chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tTacGium = await _context.TTacGia.FindAsync(id);
            if (tTacGium == null)
            {
                return NotFound();
            }
            ViewData["MaQg"] = new SelectList(_context.TQuocGia.OrderBy(q => q.TenQg), "MaQg", "TenQg", tTacGium.MaQg);
            return View(tTacGium);
        }

        // =======================================================
        // POST: TacGia/Edit/5
        // =======================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id, [Bind("MaTg,MaQg,HoDem,Ten")] TTacGia tTacGium)
        {
            if (id != tTacGium.MaTg)
            {
                return NotFound();
            }

            ViewData["MaQg"] = new SelectList(_context.TQuocGia.OrderBy(q => q.TenQg), "MaQg", "TenQg", tTacGium.MaQg);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tTacGium);
                    await _context.SaveChangesAsync();

                    TempData["StatusMessage"] = "success";
                    // SỬA: Dùng <strong>
                    TempData["Message"] = $"Thông tin Tác giả <strong>{tTacGium.HoDem} {tTacGium.Ten}</strong> đã được cập nhật thành công.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TTacGiumExists(tTacGium.MaTg))
                    {
                        return NotFound();
                    }
                    else
                    {
                        TempData["StatusMessage"] = "danger";
                        TempData["Message"] = "Lỗi xung đột dữ liệu. Vui lòng thử lại.";
                    }
                }
                catch (Exception ex)
                {
                    TempData["StatusMessage"] = "danger";
                    // SỬA: Dùng <strong>
                    TempData["Message"] = "Lỗi hệ thống khi lưu: <strong>" + ex.Message + "</strong>";
                }
            }
            else
            {
                TempData["StatusMessage"] = "danger";
                var errors = ModelState.Where(x => x.Value.Errors.Any())
                   .Select(x => $"{x.Key}: {string.Join("; ", x.Value.Errors.Select(e => e.ErrorMessage))}");
                // SỬA: Dùng <ul><li> và <strong>
                TempData["Message"] = $"Dữ liệu không hợp lệ. Vui lòng kiểm tra: <ul><li><strong>{string.Join("</strong></li><li><strong>", errors)}</strong></li></ul>";
            }

            return View(tTacGium);
        }
    }
}