using Library_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient; // Thêm
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data; // Thêm

namespace Library_Manager.Controllers
{
    [Route("The-loai")]
    public class TheLoaiController : Controller
    {
        private readonly QlthuVienContext _context;

        public TheLoaiController(QlthuVienContext context)
        {
            _context = context;
        }

        // =======================================================
        // GET: TheLoai (Giữ nguyên)
        // =======================================================
        [Route("Danh-sach")]
        public IActionResult Index(int? page, string searchString)
        {
            var pageNumber = page ?? 1;
            var pageSize = 6;

            IQueryable<TTheLoai> theLoais = _context.TTheLoai;

            if (!string.IsNullOrEmpty(searchString))
            {
                theLoais = theLoais.Where(tl =>
                    tl.TenThL.ToLower().Contains(searchString.ToLower()) ||
                    tl.MaThL.ToLower().Contains(searchString.ToLower()));
            }

            theLoais = theLoais.OrderBy(tl => tl.MaThL);
            var pagedTheLoais = new PagedList<TTheLoai>(theLoais, pageNumber, pageSize);

            ViewBag.CurrentFilter = searchString;

            return View(pagedTheLoais);
        }

        // =======================================================
        // GET: TheLoai/Details/5 (Giữ nguyên)
        // =======================================================
        [Route("Chi-tiet/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) { return NotFound(); }

            var tTheLoai = await _context.TTheLoai
                .FirstOrDefaultAsync(m => m.MaThL == id);
            if (tTheLoai == null) { return NotFound(); }

            return View(tTheLoai);
        }

        // =======================================================
        // GET: TheLoai/Create (Giữ nguyên)
        // =======================================================
        [Route("Tao-moi")]
        public IActionResult Create()
        {
            return View();
        }

        // =======================================================
        // POST: TheLoai/Create (THÊM LOGIC SINH MÃ VÀ XỬ LÝ LỖI)
        // =======================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Tao-moi")]
        // BỎ MaThL khỏi Bind để nó được sinh tự động
        public async Task<IActionResult> Create([Bind("TenThL")] TTheLoai tTheLoai)
        {
            // Loại bỏ MaThL khỏi validation vì nó sẽ được sinh sau
            ModelState.Remove("MaThL");

            if (ModelState.IsValid)
            {
                try
                {
                    // === LOGIC SINH MÃ TỰ ĐỘNG ===
                    var newMaThLParam = new SqlParameter("@NewMaThL", SqlDbType.Char, 6) { Direction = ParameterDirection.Output };
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC SP_GenerateNewMaThL @NewMaThL OUTPUT",
                        newMaThLParam
                    );
                    var newMaThLValue = newMaThLParam.Value;
                    if (newMaThLValue == DBNull.Value || string.IsNullOrEmpty(newMaThLValue.ToString()))
                    {
                        throw new InvalidOperationException("Không thể sinh mã Thể loại mới.");
                    }
                    tTheLoai.MaThL = newMaThLValue.ToString();
                    // ==============================

                    _context.Add(tTheLoai);
                    await _context.SaveChangesAsync();

                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Đã tạo mới Thể loại: <strong>{tTheLoai.TenThL}</strong> với Mã ThL: <strong>{tTheLoai.MaThL}</strong>";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbEx) // Bắt lỗi DB Update (bao gồm trùng lặp tên ThL)
                {
                    if (dbEx.InnerException is SqlException sqlEx &&
                        (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                    {
                        TempData["StatusMessage"] = "danger";
                        TempData["Message"] = $"Không thể lưu. Tên Thể loại <strong>{tTheLoai.TenThL}</strong> đã tồn tại.";
                    }
                    else
                    {
                        TempData["StatusMessage"] = "danger";
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
            else
            {
                // LỖI VALIDATION
                TempData["StatusMessage"] = "danger";
                var errors = ModelState.Where(x => x.Value.Errors.Any())
                   .Select(x => $"{x.Key}: {string.Join("; ", x.Value.Errors.Select(e => e.ErrorMessage))}").ToList();
                TempData["Message"] = $"Dữ liệu không hợp lệ. Vui lòng kiểm tra: <ul><li><strong>{string.Join("</strong></li><li><strong>", errors)}</strong></li></ul>";
            }
            // QUAN TRỌNG: Trả về View để hiển thị lỗi ngay trên trang Create
            return View(tTheLoai);
        }

        // =======================================================
        // GET: TheLoai/Edit/5 (Giữ nguyên)
        // =======================================================
        [Route("Chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) { return NotFound(); }

            var tTheLoai = await _context.TTheLoai.FindAsync(id);
            if (tTheLoai == null) { return NotFound(); }
            return View(tTheLoai);
        }

        // =======================================================
        // POST: TheLoai/Edit/5 (THÊM LOGIC BẮT LỖI TRÙNG LẶP VÀ CHUẨN HÓA THÔNG BÁO)
        // =======================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id, [Bind("MaThL,TenThL")] TTheLoai tTheLoai)
        {
            if (id != tTheLoai.MaThL) { return NotFound(); }

            if (ModelState.IsValid)
            {
                try
                {
                    // Lấy Entity gốc, không theo dõi để tránh xung đột
                    var originalTheLoai = await _context.TTheLoai
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.MaThL == id);

                    if (originalTheLoai == null) { return NotFound(); }

                    // Chỉ cập nhật các trường được phép sửa
                    originalTheLoai.TenThL = tTheLoai.TenThL;

                    _context.Update(originalTheLoai);
                    await _context.SaveChangesAsync();

                    // THÀNH CÔNG: Set TempData
                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Thông tin Thể loại <strong>{originalTheLoai.TenThL}</strong> đã được cập nhật thành công.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TTheLoaiExists(tTheLoai.MaThL))
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
                catch (DbUpdateException dbEx) // Bắt lỗi DB Update (bao gồm trùng lặp tên ThL)
                {
                    // Kiểm tra lỗi trùng lặp UNIQUE (Mã lỗi SQL 2627 hoặc 2601)
                    if (dbEx.InnerException is SqlException sqlEx &&
                        (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                    {
                        TempData["StatusMessage"] = "danger";
                        TempData["Message"] = $"Không thể lưu. Tên Thể loại <strong>{tTheLoai.TenThL}</strong> đã tồn tại.";
                    }
                    else
                    {
                        // LỖI DB khác
                        TempData["StatusMessage"] = "danger";
                        string innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                        TempData["Message"] = $"Lỗi hệ thống khi lưu: <strong>{innerMessage}</strong>";
                    }
                }
                catch (Exception ex)
                {
                    // LỖI HỆ THỐNG: Set TempData
                    TempData["StatusMessage"] = "danger";
                    string innerMessage = ex.InnerException?.Message ?? ex.Message;
                    TempData["Message"] = $"Lỗi hệ thống khi lưu: <strong>{innerMessage}</strong>";
                }
            }
            else
            {
                // LỖI VALIDATION: Set TempData
                TempData["StatusMessage"] = "danger";
                var errors = ModelState.Where(x => x.Value.Errors.Any())
                   .Select(x => $"{x.Key}: {string.Join("; ", x.Value.Errors.Select(e => e.ErrorMessage))}");
                TempData["Message"] = $"Dữ liệu không hợp lệ. Vui lòng kiểm tra: <ul><li><strong>{string.Join("</strong></li><li><strong>", errors)}</strong></li></ul>";
            }

            // LUÔN LUÔN: Return View để hiển thị thông báo
            return View(tTheLoai);
        }

        // =======================================================
        // GET: TheLoai/Delete/5 (Giữ nguyên)
        // =======================================================
        [Route("Xoa/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) { return NotFound(); }

            var tTheLoai = await _context.TTheLoai
                .FirstOrDefaultAsync(m => m.MaThL == id);
            if (tTheLoai == null) { return NotFound(); }

            return View(tTheLoai);
        }

        // =======================================================
        // POST: TheLoai/Delete/5 (THÊM XỬ LÝ LỖI KHÓA NGOẠI VÀ CHUẨN HÓA THÔNG BÁO)
        // =======================================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Xoa/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tTheLoai = await _context.TTheLoai.FindAsync(id);

            if (tTheLoai != null)
            {
                try
                {
                    _context.TTheLoai.Remove(tTheLoai);
                    await _context.SaveChangesAsync();

                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Đã xóa Thể loại có Mã: <strong>{id}</strong> thành công.";
                }
                catch (DbUpdateException dbEx)
                {
                    // Lỗi: Ràng buộc Khóa Ngoại (Foreign Key Constraint)
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = $"Không thể xóa Thể loại <strong>{id}</strong> vì đang có tài liệu tham chiếu đến. Vui lòng xóa các tài liệu liên quan trước.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Lỗi hệ thống khác
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = $"Lỗi hệ thống khi xóa: <strong>{ex.Message}</strong>";
                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TTheLoaiExists(string id)
        {
            return _context.TTheLoai.Any(e => e.MaThL == id);
        }
    }
}