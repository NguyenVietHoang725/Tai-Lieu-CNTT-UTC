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
    [Route("Nha-xuat-ban")]
    public class NhaXuatBanController : Controller
    {
        private readonly QlthuVienContext _context;

        public NhaXuatBanController(QlthuVienContext context)
        {
            _context = context;
        }

        // =======================================================
        // GET: NhaXuatBan
        // =======================================================
        [Route("")]
        [Route("Danh-sach")]
        public IActionResult Index(int? page, string searchString, string maQg)
        {
            var pageNumber = page ?? 1;
            var pageSize = 6;

            var countries = _context.TQuocGia.Select(q => new { q.MaQg, q.TenQg }).ToList();
            ViewBag.Countries = countries.ToDictionary(c => c.MaQg, c => c.TenQg);
            ViewBag.SelectedCountry = maQg;

            IQueryable<TNhaXuatBan> nhaXuatBans = _context.TNhaXuatBan
                .Include(nxb => nxb.MaQgNavigation);

            if (!string.IsNullOrEmpty(maQg))
            {
                nhaXuatBans = nhaXuatBans.Where(nxb => nxb.MaQg == maQg);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                nhaXuatBans = nhaXuatBans.Where(nxb =>
                    nxb.TenNxb.ToLower().Contains(searchString.ToLower()) ||
                    (nxb.MaQgNavigation != null && nxb.MaQgNavigation.TenQg.ToLower().Contains(searchString.ToLower())) ||
                    nxb.MaNxb.Contains(searchString));
            }

            nhaXuatBans = nhaXuatBans.OrderBy(nxb => nxb.MaNxb);
            var pagedNXBs = new PagedList<TNhaXuatBan>(nhaXuatBans, pageNumber, pageSize);

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentMaQg = maQg;

            return View(pagedNXBs);
        }

        // =======================================================
        // GET: NhaXuatBan/Details/5
        // =======================================================
        [Route("Chi-tiet/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) { return NotFound(); }
            var tNhaXuatBan = await _context.TNhaXuatBan
                .Include(t => t.MaQgNavigation)
                .FirstOrDefaultAsync(m => m.MaNxb == id);
            if (tNhaXuatBan == null) { return NotFound(); }
            return View(tNhaXuatBan);
        }

        // =======================================================
        // GET: NhaXuatBan/Create
        // =======================================================
        [Route("Tao-moi")]
        public IActionResult Create()
        {
            ViewData["MaQg"] = new SelectList(_context.TQuocGia.OrderBy(q => q.TenQg), "MaQg", "TenQg");
            return View();
        }

        // =======================================================
        // POST: NhaXuatBan/Create
        // =======================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Tao-moi")]
        public async Task<IActionResult> Create([Bind("MaQg,TenNxb")] TNhaXuatBan tNhaXuatBan)
        {
            ViewData["MaQg"] = new SelectList(_context.TQuocGia.OrderBy(q => q.TenQg), "MaQg", "TenQg", tNhaXuatBan.MaQg);

            // === KHẮC PHỤC LỖI VALIDATION TỰ ĐỘNG SINH MÃ VÀ NAVIGATION PROPERTY ===
            ModelState.Remove("MaNxb");
            ModelState.Remove("MaQgNavigation");
            // =======================================================================

            if (ModelState.IsValid)
            {
                try
                {
                    var newMaNxbParam = new SqlParameter("@NewMaNxb", SqlDbType.NVarChar, 50) { Direction = ParameterDirection.Output };
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC SP_GenerateNewMaNxb @MaQg, @NewMaNxb OUTPUT",
                        new SqlParameter("@MaQg", tNhaXuatBan.MaQg),
                        newMaNxbParam
                    );
                    var newMaNxbValue = newMaNxbParam.Value;
                    if (newMaNxbValue == DBNull.Value || string.IsNullOrEmpty(newMaNxbValue.ToString()))
                    {
                        throw new InvalidOperationException("Không thể sinh mã Nhà Xuất Bản mới. Có thể đã đạt giới hạn 999 trong năm nay cho Quốc gia này.");
                    }
                    tNhaXuatBan.MaNxb = newMaNxbValue.ToString();

                    _context.Add(tNhaXuatBan);
                    await _context.SaveChangesAsync();

                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Đã tạo mới Nhà Xuất Bản: <strong>{tNhaXuatBan.TenNxb}</strong> với Mã NXB: <strong>{tNhaXuatBan.MaNxb}</strong>";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["StatusMessage"] = "danger";
                    string errorMessage = ex.InnerException?.Message ?? ex.Message;
                    if (errorMessage.Contains("RAISERROR"))
                    {
                        errorMessage = errorMessage.Substring(errorMessage.IndexOf("RAISERROR") + 10).Trim();
                    }
                    TempData["Message"] = "Lỗi hệ thống khi tạo mới: <strong>" + errorMessage + "</strong>";
                }
            }
            else
            {
                TempData["StatusMessage"] = "danger";
                var errors = ModelState.Where(x => x.Value.Errors.Any())
                   .Select(x => $"{x.Key}: {string.Join("; ", x.Value.Errors.Select(e => e.ErrorMessage))}").ToList();

                if (tNhaXuatBan.MaQg == null && ModelState.ContainsKey("MaQg") && ModelState["MaQg"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    errors.Add("Quốc gia: Vui lòng chọn một Quốc gia.");
                }
                TempData["Message"] = $"Dữ liệu không hợp lệ. Vui lòng kiểm tra: <ul><li><strong>{string.Join("</strong></li><li><strong>", errors)}</strong></li></ul>";
            }
            return View(tNhaXuatBan);
        }

        // =======================================================
        // GET: NhaXuatBan/Edit/5
        // =======================================================
        [Route("Chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) { return NotFound(); }
            var tNhaXuatBan = await _context.TNhaXuatBan
                .Include(t => t.MaQgNavigation)
                .FirstOrDefaultAsync(m => m.MaNxb == id);
            if (tNhaXuatBan == null) { return NotFound(); }
            return View(tNhaXuatBan);
        }

        // =======================================================
        // POST: NhaXuatBan/Edit/5 (ĐÃ SỬA: Bắt lỗi trùng lặp tên NXB)
        // =======================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id, [Bind("MaNxb,MaQg,TenNxb")] TNhaXuatBan tNhaXuatBan)
        {
            if (id != tNhaXuatBan.MaNxb) { return NotFound(); }

            // Loại bỏ kiểm tra Navigation Property (vẫn cần thiết khi có lỗi Validation)
            ModelState.Remove("MaQgNavigation");

            if (ModelState.IsValid)
            {
                try
                {
                    // Lấy Entity gốc, không theo dõi để tránh xung đột
                    var originalNxb = await _context.TNhaXuatBan
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.MaNxb == id);

                    if (originalNxb == null)
                    {
                        return NotFound();
                    }

                    // Chỉ cập nhật các trường được phép sửa
                    originalNxb.TenNxb = tNhaXuatBan.TenNxb;

                    // Thêm Entity vào context với trạng thái Modified
                    _context.Update(originalNxb);
                    await _context.SaveChangesAsync();

                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Thông tin Nhà Xuất Bản <strong>{originalNxb.TenNxb}</strong> đã được cập nhật thành công.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TNhaXuatBanExists(tNhaXuatBan.MaNxb)) { return NotFound(); }
                    else
                    {
                        TempData["StatusMessage"] = "danger";
                        TempData["Message"] = "Lỗi xung đột dữ liệu. Vui lòng thử lại.";
                    }
                }
                catch (DbUpdateException dbEx) // Bắt lỗi DB Update (bao gồm trùng lặp)
                {
                    // Kiểm tra lỗi trùng lặp UNIQUE (Mã lỗi SQL 2627 hoặc 2601)
                    if (dbEx.InnerException is SqlException sqlEx &&
                        (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                    {
                        TempData["StatusMessage"] = "danger";
                        TempData["Message"] = $"Không thể lưu. Tên Nhà Xuất Bản <strong>{tNhaXuatBan.TenNxb}</strong> đã tồn tại.";
                    }
                    else
                    {
                        // Lỗi DB khác
                        TempData["StatusMessage"] = "danger";
                        string innerMessage = dbEx.InnerException != null ? dbEx.InnerException.Message : dbEx.Message;
                        TempData["Message"] = $"Lỗi hệ thống khi lưu: <strong>{innerMessage}</strong>";
                    }
                }
                catch (Exception ex)
                {
                    TempData["StatusMessage"] = "danger";
                    string innerMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    TempData["Message"] = $"Lỗi hệ thống khi lưu: <strong>{innerMessage}</strong>";
                }
            }
            else
            {
                // Xử lý lỗi Validation
                TempData["StatusMessage"] = "danger";
                var errors = ModelState.Where(x => x.Value.Errors.Any())
                   .Select(x => $"{x.Key}: {string.Join("; ", x.Value.Errors.Select(e => e.ErrorMessage))}");
                TempData["Message"] = $"Dữ liệu không hợp lệ. Vui lòng kiểm tra: <ul><li><strong>{string.Join("</strong></li><li><strong>", errors)}</strong></li></ul>";
            }

            // Load lại đầy đủ model (bao gồm navigation property) để hiển thị lỗi trong View
            var nxbForView = await _context.TNhaXuatBan
                .Include(t => t.MaQgNavigation)
                .FirstOrDefaultAsync(m => m.MaNxb == tNhaXuatBan.MaNxb);

            // Gán lại tên NXB đã nhập nếu có lỗi (vì View cần hiển thị giá trị người dùng vừa nhập)
            if (nxbForView != null)
                nxbForView.TenNxb = tNhaXuatBan.TenNxb;

            return View(nxbForView);
        }

        // =======================================================
        // GET: NhaXuatBan/Delete/5
        // =======================================================
        [Route("Xoa/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) { return NotFound(); }

            var tNhaXuatBan = await _context.TNhaXuatBan
                .Include(t => t.MaQgNavigation)
                .FirstOrDefaultAsync(m => m.MaNxb == id);
            if (tNhaXuatBan == null) { return NotFound(); }

            return View(tNhaXuatBan);
        }

        // =======================================================
        // POST: NhaXuatBan/Delete/5
        // =======================================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Xoa/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tNhaXuatBan = await _context.TNhaXuatBan.FindAsync(id);

            if (tNhaXuatBan != null)
            {
                try
                {
                    _context.TNhaXuatBan.Remove(tNhaXuatBan);
                    await _context.SaveChangesAsync();

                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Đã xóa Nhà Xuất Bản có Mã: <strong>{id}</strong> thành công.";
                }
                catch (DbUpdateException ex)
                {
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = $"Không thể xóa Nhà Xuất Bản <strong>{id}</strong> vì đang có tài liệu tham chiếu đến. Vui lòng xóa các tài liệu liên quan trước.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = $"Lỗi hệ thống khi xóa: <strong>{ex.Message}</strong>";
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TNhaXuatBanExists(string id)
        {
            return _context.TNhaXuatBan.Any(e => e.MaNxb == id);
        }
    }
}