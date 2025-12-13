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
    [Route("Dinh-dang")]
    public class DinhDangController : Controller
    {
        private readonly QlthuVienContext _context;

        public DinhDangController(QlthuVienContext context)
        {
            _context = context;
        }

        // =======================================================
        // GET: DinhDang/Index
        // =======================================================
        [Route("Danh-sach")]
        public IActionResult Index(int? page, string searchString)
        {
            var pageNumber = page ?? 1;
            var pageSize = 6;

            IQueryable<TDinhDang> dinhDangs = _context.TDinhDang;

            if (!string.IsNullOrEmpty(searchString))
            {
                dinhDangs = dinhDangs.Where(dd =>
                    dd.TenDd.ToLower().Contains(searchString.ToLower()) ||
                    dd.MaDd.ToLower().Contains(searchString.ToLower()));
            }

            dinhDangs = dinhDangs.OrderBy(dd => dd.MaDd);
            var pagedDinhDangs = new PagedList<TDinhDang>(dinhDangs, pageNumber, pageSize);

            ViewBag.CurrentFilter = searchString;

            return View(pagedDinhDangs);
        }

        // =======================================================
        // GET: DinhDang/Details/5
        // =======================================================
        [Route("Chi-tiet/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) { return NotFound(); }

            var tDinhDang = await _context.TDinhDang
                .FirstOrDefaultAsync(m => m.MaDd == id);
            if (tDinhDang == null) { return NotFound(); }

            return View(tDinhDang);
        }

        // =======================================================
        // GET: DinhDang/Create
        // =======================================================
        [Route("Tao-moi")]
        public IActionResult Create()
        {
            return View();
        }

        // =======================================================
        // POST: DinhDang/Create (Sinh mã và xử lý luồng lỗi/thành công)
        // =======================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Tao-moi")]
        public async Task<IActionResult> Create([Bind("TenDd")] TDinhDang tDinhDang)
        {
            ModelState.Remove("MaDd");

            if (ModelState.IsValid)
            {
                try
                {
                    // === LOGIC SINH MÃ TỰ ĐỘNG (DÙNG SP_GenerateNewMaDD) ===
                    var newMaDdParam = new SqlParameter("@NewMaDD", SqlDbType.Char, 5) { Direction = ParameterDirection.Output };
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC SP_GenerateNewMaDD @NewMaDD OUTPUT",
                        newMaDdParam
                    );
                    var newMaDdValue = newMaDdParam.Value;
                    if (newMaDdValue == DBNull.Value || string.IsNullOrEmpty(newMaDdValue.ToString()))
                    {
                        throw new InvalidOperationException("Không thể sinh mã Định dạng mới. Vui lòng kiểm tra SP_GenerateNewMaDD.");
                    }
                    tDinhDang.MaDd = newMaDdValue.ToString();
                    // ==============================

                    _context.Add(tDinhDang);
                    await _context.SaveChangesAsync();

                    // LUỒNG THÀNH CÔNG: Redirect về Index
                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Đã tạo mới Định dạng: <strong>{tDinhDang.TenDd}</strong> với Mã DD: <strong>{tDinhDang.MaDd}</strong>";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbEx)
                {
                    TempData["StatusMessage"] = "danger";
                    if (dbEx.InnerException is SqlException sqlEx && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                    {
                        TempData["Message"] = $"Không thể lưu. Tên Định dạng <strong>{tDinhDang.TenDd}</strong> đã tồn tại.";
                    }
                    else
                    {
                        string innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                        TempData["Message"] = $"Lỗi hệ thống khi tạo mới: <strong>{innerMessage}</strong>";
                    }
                }
                catch (Exception ex)
                {
                    TempData["StatusMessage"] = "danger";
                    string errorMessage = ex.InnerException?.Message ?? ex.Message;
                    TempData["Message"] = "Lỗi hệ thống khi tạo mới: <strong>" + errorMessage + "</strong>";
                }
            }
            // LUỒNG THẤT BẠI: Hiển thị lỗi ngay trên View Create
            else
            {
                TempData["StatusMessage"] = "danger";
                var errors = ModelState.Where(x => x.Value.Errors.Any())
                   .Select(x => $"{x.Key}: {string.Join("; ", x.Value.Errors.Select(e => e.ErrorMessage))}").ToList();
                TempData["Message"] = $"Dữ liệu không hợp lệ. Vui lòng kiểm tra: <ul><li><strong>{string.Join("</strong></li><li><strong>", errors)}</strong></li></ul>";
            }
            return View(tDinhDang);
        }

        // =======================================================
        // GET: DinhDang/Edit/5
        // =======================================================
        [Route("Chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) { return NotFound(); }

            var tDinhDang = await _context.TDinhDang.FindAsync(id);
            if (tDinhDang == null) { return NotFound(); }
            return View(tDinhDang);
        }

        // =======================================================
        // POST: DinhDang/Edit/5 (Xử lý luồng lỗi/thành công)
        // =======================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id, [Bind("MaDd,TenDd")] TDinhDang tDinhDang)
        {
            if (id != tDinhDang.MaDd) { return NotFound(); }

            if (ModelState.IsValid)
            {
                try
                {
                    var originalDinhDang = await _context.TDinhDang
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.MaDd == id);

                    if (originalDinhDang == null) { return NotFound(); }

                    originalDinhDang.TenDd = tDinhDang.TenDd;

                    _context.Update(originalDinhDang);
                    await _context.SaveChangesAsync();

                    // LUỒNG THÀNH CÔNG: Redirect về Index
                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Thông tin Định dạng <strong>{originalDinhDang.TenDd}</strong> đã được cập nhật thành công.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = "Lỗi xung đột dữ liệu. Vui lòng thử lại.";
                }
                catch (DbUpdateException dbEx)
                {
                    TempData["StatusMessage"] = "danger";
                    if (dbEx.InnerException is SqlException sqlEx && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                    {
                        TempData["Message"] = $"Không thể lưu. Tên Định dạng <strong>{tDinhDang.TenDd}</strong> đã tồn tại.";
                    }
                    else
                    {
                        string innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                        TempData["Message"] = $"Lỗi hệ thống khi lưu: <strong>{innerMessage}</strong>";
                    }
                }
                catch (Exception ex)
                {
                    TempData["StatusMessage"] = "danger";
                    string innerMessage = ex.InnerException?.Message ?? ex.Message;
                    TempData["Message"] = $"Lỗi hệ thống khi lưu: <strong>{innerMessage}</strong>";
                }
            }
            // LUỒNG THẤT BẠI: Hiển thị lỗi ngay trên View Edit
            else
            {
                TempData["StatusMessage"] = "danger";
                var errors = ModelState.Where(x => x.Value.Errors.Any())
                   .Select(x => $"{x.Key}: {string.Join("; ", x.Value.Errors.Select(e => e.ErrorMessage))}");
                TempData["Message"] = $"Dữ liệu không hợp lệ. Vui lòng kiểm tra: <ul><li><strong>{string.Join("</strong></li><li><strong>", errors)}</strong></li></ul>";
            }

            return View(tDinhDang);
        }

        // =======================================================
        // GET: DinhDang/Delete/5
        // =======================================================
        [Route("Xoa/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) { return NotFound(); }

            var tDinhDang = await _context.TDinhDang
                .FirstOrDefaultAsync(m => m.MaDd == id);
            if (tDinhDang == null) { return NotFound(); }

            return View(tDinhDang);
        }

        // =======================================================
        // POST: DinhDang/Delete/5 (Xử lý luồng lỗi/thành công)
        // =======================================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Xoa/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tDinhDang = await _context.TDinhDang.FindAsync(id);

            if (tDinhDang != null)
            {
                try
                {
                    _context.TDinhDang.Remove(tDinhDang);
                    await _context.SaveChangesAsync();

                    // LUỒNG THÀNH CÔNG: Redirect về Index
                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Đã xóa Định dạng có Mã: <strong>{id}</strong> thành công.";
                }
                catch (DbUpdateException dbEx)
                {
                    // LUỒNG THẤT BẠI: Redirect về Index (Hiển thị lỗi trên Index)
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = $"Không thể xóa Định dạng <strong>{id}</strong> vì đang có tài liệu tham chiếu đến. Vui lòng xóa các tài liệu liên quan trước.";
                }
                catch (Exception ex)
                {
                    // LUỒNG THẤT BẠI: Redirect về Index (Hiển thị lỗi trên Index)
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = $"Lỗi hệ thống khi xóa: <strong>{ex.Message}</strong>";
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TDinhDangExists(string id)
        {
            return _context.TDinhDang.Any(e => e.MaDd == id);
        }
    }
}