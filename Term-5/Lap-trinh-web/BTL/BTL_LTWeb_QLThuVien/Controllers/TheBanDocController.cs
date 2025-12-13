using Library_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Library_Manager.Controllers
{
    [Route("The-ban-doc")]
    public class TheBanDocController : Controller
    {
        private readonly QlthuVienContext _context;

        public TheBanDocController(QlthuVienContext context)
        {
            _context = context;
        }

        // =======================================================
        // GET: TheBanDoc/Index (Kích hoạt thủ tục tự động khóa)
        // =======================================================
        [Route("Danh-sach")]
        public IActionResult Index(int? page, string searchString)
        {
            // === KÍCH HOẠT TỰ ĐỘNG KHÓA THẺ DO HẾT HẠN (Thay thế SQL Agent/Express) ===
            try
            {
                // Gọi thủ tục khóa thẻ đã hết hạn mỗi khi trang Index được truy cập
                _context.Database.ExecuteSqlRaw("EXEC SP_LockExpiredCards");
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần, nhưng không chặn người dùng
                Console.WriteLine($"Lỗi khi chạy SP_LockExpiredCards: {ex.Message}");
            }
            // =============================================================================

            var pageNumber = page ?? 1;
            var pageSize = 6;

            IQueryable<TTheBanDoc> theBanDocs = _context.TTheBanDoc
                .Include(t => t.MaBdNavigation);

            if (!string.IsNullOrEmpty(searchString))
            {
                theBanDocs = theBanDocs.Where(t =>
                    t.MaTbd.Contains(searchString) ||
                    t.MaBd.Contains(searchString) ||
                    t.MaBdNavigation.HoDem.ToLower().Contains(searchString.ToLower()) ||
                    t.MaBdNavigation.Ten.ToLower().Contains(searchString.ToLower()) ||
                    t.TrangThai.Contains(searchString));
            }

            theBanDocs = theBanDocs.OrderBy(t => t.MaTbd);

            var pagedTheBanDocs = new PagedList<TTheBanDoc>(theBanDocs, pageNumber, pageSize);

            ViewBag.CurrentFilter = searchString;
            return View(pagedTheBanDocs);
        }

        // =======================================================
        // GET: TheBanDoc/Details/5
        // =======================================================
        [Route("Chi-tiet/{id}")]
        public async Task<IActionResult> Details(string id, string returnUrl = null)
        {
            if (id == null) { return NotFound(); }

            var tTheBanDoc = await _context.TTheBanDoc
                .Include(t => t.MaBdNavigation)
                .Include(t => t.MaTkNavigation)
                .ThenInclude(t => t.MaNvNavigation)
                .FirstOrDefaultAsync(m => m.MaTbd == id);

            if (tTheBanDoc == null) { return NotFound(); }

            ViewBag.ReturnUrl = returnUrl;
            return View(tTheBanDoc);
        }

        // =======================================================
        // GET: TheBanDoc/Create
        // =======================================================
        [Route("Tao-moi")]
        public async Task<IActionResult> Create()
        {
            // Lấy thông tin người tạo từ Session
            ViewBag.MaTkDangNhap = HttpContext.Session.GetString("MaTk");
            ViewBag.HoTenNhanVien = HttpContext.Session.GetString("hoTen");

            // Tự động gán NgayCap là ngày hiện tại cho Model
            var tTheBanDoc = new TTheBanDoc { NgayCap = DateOnly.FromDateTime(DateTime.Today) };

            await PopulateBanDocChuaCoTheDropDownList();
            return View(tTheBanDoc); // Truyền model đã có NgayCap mặc định
        }

        // =======================================================
        // POST: TheBanDoc/Create
        // =======================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Tao-moi")]
        public async Task<IActionResult> Create([Bind("MaBd,NgayHetHan,TrangThai")] TTheBanDoc tTheBanDoc)
        {
            var maTk = HttpContext.Session.GetString("MaTk");

            // Loại bỏ Navigation Properties khỏi Validation (KHẮC PHỤC LỖI Navigation Required)
            ModelState.Remove("MaBdNavigation");
            ModelState.Remove("MaTkNavigation");
            // Loại bỏ các trường sẽ được gán
            ModelState.Remove("MaTk");
            ModelState.Remove("MaTbd");

            // 1. Kiểm tra Mã Tài khoản (Người tạo)
            if (string.IsNullOrEmpty(maTk))
            {
                TempData["StatusMessage"] = "danger";
                TempData["Message"] = "Lỗi: Bạn phải đăng nhập để tạo thẻ.";
                // Tải lại ViewBag và NgayCap mặc định
                tTheBanDoc.NgayCap = DateOnly.FromDateTime(DateTime.Today);
                await PopulateBanDocChuaCoTheDropDownList(tTheBanDoc.MaBd);
                ViewBag.MaTkDangNhap = HttpContext.Session.GetString("MaTk");
                ViewBag.HoTenNhanVien = HttpContext.Session.GetString("hoTen");
                return View(tTheBanDoc);
            }

            tTheBanDoc.MaTk = maTk; // Gán Mã TK đang đăng nhập
            tTheBanDoc.NgayCap = DateOnly.FromDateTime(DateTime.Today); // **TỰ ĐỘNG GÁN NGÀY CẤP**

            if (ModelState.IsValid)
            {
                // === LOGIC SINH MÃ TỰ ĐỘNG VÀ KIỂM TRA TÍNH DUY NHẤT ===
                var newMaTbdParam = new SqlParameter("@NewMaTBD", SqlDbType.Char, 12) { Direction = ParameterDirection.Output };
                var maBdParam = new SqlParameter("@MaBD", SqlDbType.Char, 9) { Value = tTheBanDoc.MaBd };
                var errorStatusParam = new SqlParameter("@ErrorStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SP_GenerateNewMaTBD @MaBD, @NewMaTBD OUTPUT, @ErrorStatus OUTPUT",
                    maBdParam,
                    newMaTbdParam,
                    errorStatusParam
                );

                var newMaTbdValue = newMaTbdParam.Value;
                var errorStatus = (int)errorStatusParam.Value;

                string errorMessage = null;
                if (errorStatus == 1) errorMessage = $"Mã Bạn đọc '{tTheBanDoc.MaBd}' không tồn tại trong hệ thống.";
                else if (errorStatus == 2) errorMessage = $"Bạn đọc có mã '{tTheBanDoc.MaBd}' đã có thẻ. Vui lòng chọn bạn đọc khác.";
                else if (errorStatus != 0 || newMaTbdValue == DBNull.Value || string.IsNullOrEmpty(newMaTbdValue.ToString())) errorMessage = "Không thể sinh mã Thẻ Bạn đọc mới (Lỗi định dạng/Hệ thống).";

                if (errorMessage != null)
                {
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = errorMessage;
                    // Tải lại ViewBag và trả về View
                    await PopulateBanDocChuaCoTheDropDownList(tTheBanDoc.MaBd);
                    ViewBag.MaTkDangNhap = HttpContext.Session.GetString("MaTk");
                    ViewBag.HoTenNhanVien = HttpContext.Session.GetString("hoTen");
                    return View(tTheBanDoc);
                }

                tTheBanDoc.MaTbd = newMaTbdValue.ToString();
                // ===========================================

                try
                {
                    _context.Add(tTheBanDoc);
                    await _context.SaveChangesAsync();

                    // LUỒNG THÀNH CÔNG: Redirect về Index
                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Đã tạo mới Thẻ: <strong>{tTheBanDoc.MaTbd}</strong> thành công.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbEx)
                {
                    TempData["StatusMessage"] = "danger";
                    string innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                    TempData["Message"] = $"Lỗi hệ thống khi tạo mới: <strong>{innerMessage}</strong>";
                }
                catch (Exception ex)
                {
                    TempData["StatusMessage"] = "danger";
                    string errorMessageFinal = ex.InnerException?.Message ?? ex.Message;
                    TempData["Message"] = "Lỗi hệ thống khi tạo mới: <strong>" + errorMessageFinal + "</strong>";
                }
            }
            // LUỒNG THẤT BẠI: Lỗi Validation
            else
            {
                TempData["StatusMessage"] = "danger";
                var errors = ModelState.Where(x => x.Value.Errors.Any()).Select(x => $"{x.Key}: {string.Join("; ", x.Value.Errors.Select(e => e.ErrorMessage))}");
                TempData["Message"] = $"Dữ liệu không hợp lệ. Vui lòng kiểm tra: <ul><li><strong>{string.Join("</strong></li><li><strong>", errors)}</strong></li></ul>";
            }
            // Trả về View để hiển thị lỗi
            tTheBanDoc.NgayCap = DateOnly.FromDateTime(DateTime.Today); // Gán lại NgayCap mặc định trước khi return
            await PopulateBanDocChuaCoTheDropDownList(tTheBanDoc.MaBd);
            ViewBag.MaTkDangNhap = HttpContext.Session.GetString("MaTk");
            ViewBag.HoTenNhanVien = HttpContext.Session.GetString("hoTen");
            return View(tTheBanDoc);
        }

        // =======================================================
        // GET: TheBanDoc/Edit/5
        // =======================================================
        [Route("Chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) { return NotFound(); }

            var tTheBanDoc = await _context.TTheBanDoc.FindAsync(id);
            if (tTheBanDoc == null) { return NotFound(); }

            // Logic lấy Họ Tên Bạn đọc (Hiển thị)
            var banDoc = await _context.TBanDoc.FindAsync(tTheBanDoc.MaBd);
            ViewBag.HoTenBanDoc = banDoc != null ? (banDoc.HoDem + " " + banDoc.Ten) : "Lỗi";

            return View(tTheBanDoc);
        }

        // =======================================================
        // POST: TheBanDoc/Edit/5
        // =======================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id, [Bind("MaTbd,MaBd,NgayCap,NgayHetHan,TrangThai")] TTheBanDoc tTheBanDoc)
        {
            if (id != tTheBanDoc.MaTbd) { return NotFound(); }

            // Loại bỏ Navigation Properties và MaTk
            ModelState.Remove("MaBdNavigation");
            ModelState.Remove("MaTkNavigation");
            ModelState.Remove("MaTk");

            if (ModelState.IsValid)
            {
                // Lấy lại MaTk gốc (Người tạo thẻ)
                var originalMaTk = await _context.TTheBanDoc
                    .Where(t => t.MaTbd == id)
                    .Select(t => t.MaTk)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (originalMaTk == null)
                {
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = "Lỗi: Không tìm thấy thẻ gốc để cập nhật.";
                    // Phải tải lại ViewBag để View hiển thị đúng
                    var banDocDisplay = await _context.TBanDoc.FindAsync(tTheBanDoc.MaBd);
                    ViewBag.HoTenBanDoc = banDocDisplay != null ? (banDocDisplay.HoDem + " " + banDocDisplay.Ten) : "Lỗi";
                    return View(tTheBanDoc);
                }

                try
                {
                    tTheBanDoc.MaTk = originalMaTk; // Gán lại MaTk gốc

                    _context.Update(tTheBanDoc);
                    await _context.SaveChangesAsync();

                    // LUỒNG THÀNH CÔNG: Redirect về Index
                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Thông tin Thẻ <strong>{tTheBanDoc.MaTbd}</strong> đã được cập nhật thành công.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = "Lỗi xung đột dữ liệu. Thẻ này vừa được chỉnh sửa bởi người khác. Vui lòng tải lại trang.";
                }
                catch (Exception ex)
                {
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = "Lỗi hệ thống khi lưu dữ liệu: <strong>" + ex.Message + "</strong>";
                }
            }
            // LUỒNG THẤT BẠI: Lỗi Validation
            else
            {
                TempData["StatusMessage"] = "danger";
                var errors = ModelState.Where(x => x.Value.Errors.Any())
                    .Select(x => $"{x.Key}: {string.Join("; ", x.Value.Errors.Select(e => e.ErrorMessage))}");
                TempData["Message"] = $"Dữ liệu không hợp lệ. Vui lòng kiểm tra: <ul><li><strong>{string.Join("</strong></li><li><strong>", errors)}</strong></li></ul>";
            }

            // Xử lý khi THẤT BẠI
            var banDoc = await _context.TBanDoc.FindAsync(tTheBanDoc.MaBd);
            ViewBag.HoTenBanDoc = banDoc != null ? (banDoc.HoDem + " " + banDoc.Ten) : "Lỗi";

            return View(tTheBanDoc);
        }

        // =======================================================
        // GET: TheBanDoc/Delete/5
        // =======================================================
        [Route("Xoa/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) { return NotFound(); }

            var tTheBanDoc = await _context.TTheBanDoc
                .Include(t => t.MaBdNavigation)
                .FirstOrDefaultAsync(m => m.MaTbd == id);

            if (tTheBanDoc == null) { return NotFound(); }

            return View(tTheBanDoc);
        }

        // =======================================================
        // POST: TheBanDoc/Delete/5
        // =======================================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Xoa/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tTheBanDoc = await _context.TTheBanDoc.FindAsync(id);
            if (tTheBanDoc != null)
            {
                try
                {
                    _context.TTheBanDoc.Remove(tTheBanDoc);
                    await _context.SaveChangesAsync();

                    // LUỒNG THÀNH CÔNG: Redirect về Index
                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Đã xóa Thẻ có Mã: <strong>{id}</strong> thành công.";
                }
                catch (DbUpdateException dbEx)
                {
                    // LUỒNG THẤT BẠI (Khóa ngoại): Redirect về Index
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = $"Không thể xóa Thẻ <strong>{id}</strong> vì đang có giao dịch tham chiếu đến. Vui lòng xóa các mục liên quan trước.";
                }
                catch (Exception ex)
                {
                    // LUỒNG THẤT BẠI (Hệ thống): Redirect về Index
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = $"Lỗi hệ thống khi xóa: <strong>{ex.Message}</strong>";
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TTheBanDocExists(string id)
        {
            return _context.TTheBanDoc.Any(e => e.MaTbd == id);
        }

        // Helper private: Populate BanDoc chưa có thẻ
        private async Task PopulateBanDocChuaCoTheDropDownList(object selectedBanDoc = null)
        {
            var maBdDaCoThe = _context.TTheBanDoc.Select(t => t.MaBd);
            var banDocChuaCoThe = await _context.TBanDoc
                .Where(b => !maBdDaCoThe.Contains(b.MaBd))
                .Select(b => new { MaBd = b.MaBd, HoTen = b.HoDem + " " + b.Ten })
                .OrderBy(b => b.HoTen)
                .ToListAsync();
            ViewData["MaBd"] = new SelectList(banDocChuaCoThe, "MaBd", "HoTen", selectedBanDoc);
        }
    }
}