using Library_Manager.Filters;
using Library_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using Library_Manager.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace Library_Manager.Controllers
{
    
    [Route("Tai-khoan")]
    public class TaiKhoanController : Controller
    {
        private readonly QlthuVienContext _context;

        public TaiKhoanController(QlthuVienContext context)
        {
            _context = context;
        }

        // =======================================================
        // GET: TTaiKhoans/Index
        // =======================================================
        [Authorization("QTV")]
        [Route("Danh-sach")]
        [Route("")]
        public IActionResult Index(int? page, string searchString, string roleFilter)
        {
            var pageNumber = page ?? 1;
            var pageSize = 6;

            IQueryable<TTaiKhoan> taiKhoans = _context.TTaiKhoan
                .Include(t => t.MaNvNavigation)
                .Include(t => t.MaVtNavigation);

            // Lọc theo Vai trò
            if (!string.IsNullOrEmpty(roleFilter))
            {
                taiKhoans = taiKhoans.Where(tk => tk.MaVt == roleFilter);
            }

            // Lọc theo Từ khóa tìm kiếm
            if (!string.IsNullOrEmpty(searchString))
            {
                taiKhoans = taiKhoans.Where(tk =>
                    tk.TenDangNhap.ToLower().Contains(searchString.ToLower()) ||
                    tk.MaTk.Contains(searchString) ||
                    (tk.MaNvNavigation != null && (tk.MaNvNavigation.HoDem + " " + tk.MaNvNavigation.Ten).ToLower().Contains(searchString.ToLower())));
            }

            taiKhoans = taiKhoans.OrderBy(tk => tk.MaTk);

            var pagedTaiKhoans = new PagedList<TTaiKhoan>(taiKhoans, pageNumber, pageSize);

            // Gửi các giá trị filter về View
            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentRoleFilter = roleFilter;

            // Gửi danh sách Vai trò về View để làm Dropdown Filter
            ViewData["RoleList"] = new SelectList(_context.TVaiTro, "MaVt", "TenVt", roleFilter);

            return View(pagedTaiKhoans);
        }

        // =======================================================
        // GET: TTaiKhoans/Details/5
        // =======================================================
        [Route("Chi-tiet/{id}")]
        public async Task<IActionResult> Details(string id, string returnUrl = null)
        {
            if (id == null) { return NotFound(); }

            var tTaiKhoan = await _context.TTaiKhoan
                .Include(t => t.MaNvNavigation)
                .Include(t => t.MaVtNavigation)
                .FirstOrDefaultAsync(m => m.MaTk == id);
            if (tTaiKhoan == null) { return NotFound(); }

            ViewBag.ReturnUrl = returnUrl;
            return View(tTaiKhoan);
        }

        // =======================================================
        // GET: TTaiKhoans/Create
        // =======================================================
        [Authorization("QTV")]
        [Route("Tao-moi")]
        public async Task<IActionResult> Create()
        {
            // Tải ViewData cho các Dropdown
            await LoadCreateViewData(null, null);

            // Đảm bảo model rỗng khi GET
            return View(new TTaiKhoan());
        }

        // =======================================================
        // POST: TTaiKhoans/Create
        // =======================================================
        [Authorization("QTV")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Tao-moi")]
        public async Task<IActionResult> Create([Bind("TenDangNhap,MatKhau,MaVt,MaNv")] TTaiKhoan tTaiKhoan)
        {
            // Loại bỏ các trường tự sinh/gán khỏi validation
            ModelState.Remove("MaTk");
            ModelState.Remove("NgayTao");
            ModelState.Remove("TrangThai");
            ModelState.Remove("MaNvNavigation");
            ModelState.Remove("MaVtNavigation");

            tTaiKhoan.TrangThai = "Hoạt động"; // Gán trạng thái mặc định

            // --- VALIDATION SERVER-SIDE CHO CONFIRM PASSWORD ---
            if (!string.IsNullOrEmpty(tTaiKhoan.MatKhau) && !Request.Form["MatKhauConfirm"].Equals(tTaiKhoan.MatKhau))
            {
                ModelState.AddModelError("MatKhauConfirm", "Mật khẩu xác nhận không khớp.");
            }

            // --- VALIDATION SERVER-SIDE CHO MA NV ---
            if (string.IsNullOrEmpty(tTaiKhoan.MaNv))
            {
                ModelState.AddModelError("MaNv", "Vui lòng chọn một Nhân viên liên kết.");
            }

            if (ModelState.IsValid)
            {
                // === 1. SINH MÃ TÀI KHOẢN (MaTk) ===
                var newMaTkParam = new SqlParameter("@NewMaTK", SqlDbType.Char, 7) { Direction = ParameterDirection.Output };
                try
                {
                    await _context.Database.ExecuteSqlRawAsync("EXEC SP_GenerateNewMaTK @NewMaTK OUTPUT", newMaTkParam);
                    var newMaTkValue = newMaTkParam.Value;

                    if (newMaTkValue == DBNull.Value || string.IsNullOrEmpty(newMaTkValue.ToString()))
                    {
                        throw new InvalidOperationException("Không thể sinh Mã Tài khoản mới (Đã đạt giới hạn/Lỗi hệ thống).");
                    }
                    tTaiKhoan.MaTk = newMaTkValue.ToString().Trim();
                }
                catch (Exception ex)
                {
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = "Lỗi sinh mã: <strong>" + (ex.InnerException?.Message ?? ex.Message) + "</strong>";
                    await LoadCreateViewData(tTaiKhoan.MaVt, tTaiKhoan.MaNv);
                    tTaiKhoan.MatKhau = null;
                    return View(tTaiKhoan);
                }

                try
                {
                    // === 2. GÁN NGÀY TẠO & HASH MẬT KHẨU ===
                    tTaiKhoan.NgayTao = DateOnly.FromDateTime(DateTime.Today);
                    tTaiKhoan.MatKhau = PasswordHelper.HashPassword(tTaiKhoan.TenDangNhap, tTaiKhoan.MatKhau);

                    _context.Add(tTaiKhoan);
                    await _context.SaveChangesAsync();

                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Đã tạo mới Tài khoản: <strong>{tTaiKhoan.TenDangNhap}</strong> (Mã: <strong>{tTaiKhoan.MaTk}</strong>) thành công.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbEx) // Bắt lỗi DB (Trùng Tên Đăng nhập/Khóa ngoại)
                {
                    TempData["StatusMessage"] = "danger";
                    if (dbEx.InnerException is SqlException sqlEx && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                    {
                        TempData["Message"] = $"Không thể tạo. Tên đăng nhập <strong>{tTaiKhoan.TenDangNhap}</strong> đã tồn tại hoặc Nhân viên đã có Tài khoản liên kết.";
                    }
                    else
                    {
                        string innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                        TempData["Message"] = $"Lỗi hệ thống khi tạo: <strong>{innerMessage}</strong>";
                    }
                }
                catch (Exception ex)
                {
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = "Lỗi hệ thống: <strong>" + (ex.InnerException?.Message ?? ex.Message) + "</strong>";
                }
            }
            // LỖI VALIDATION HOẶC LỖI LƯU KHÁC -> return View(model)
            else
            {
                TempData["StatusMessage"] = "danger";
                var errors = ModelState.Where(x => x.Value.Errors.Any()).Select(x => $"{x.Value.Errors.First().ErrorMessage}").ToList();
                TempData["Message"] = $"Dữ liệu không hợp lệ. Vui lòng kiểm tra: <ul><li><strong>{string.Join("</strong></li><li><strong>", errors)}</strong></li></ul>";
            }

            // Tải lại ViewData (cả Vai trò và Nhân viên) khi thất bại
            await LoadCreateViewData(tTaiKhoan.MaVt, tTaiKhoan.MaNv);
            tTaiKhoan.MatKhau = null; // Đảm bảo MatKhau bị xóa khi lỗi
            return View(tTaiKhoan);
        }

        // =======================================================
        // GET: TTaiKhoans/Edit/5
        // =======================================================
        [Authorization("QTV")]
        [Route("Chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) { return NotFound(); }

            var tTaiKhoan = await _context.TTaiKhoan.FindAsync(id);
            if (tTaiKhoan == null) { return NotFound(); }

            // Lấy danh sách Vai trò (Hiển thị TenVt)
            ViewData["MaVt"] = new SelectList(_context.TVaiTro, "MaVt", "TenVt", tTaiKhoan.MaVt);

            // Lấy thông tin Nhân viên liên kết để hiển thị (READONLY trong ViewBag)
            var nhanVien = await _context.TNhanVien.FindAsync(tTaiKhoan.MaNv);
            ViewBag.NhanVienLienKet = nhanVien != null ? $"{nhanVien.HoDem} {nhanVien.Ten} ({nhanVien.MaNv})" : "(Không rõ)";

            // Đảm bảo MatKhau được truyền về là null (clear input)
            tTaiKhoan.MatKhau = null;

            return View(tTaiKhoan);
        }

        // =======================================================
        // POST: TTaiKhoans/Edit/5
        // =======================================================
        [Authorization("QTV")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id, [Bind("MaTk,MaVt,TenDangNhap,MatKhau,TrangThai")] TTaiKhoan tTaiKhoan, [FromForm] string MatKhauHienTai)
        {
            if (id != tTaiKhoan.MaTk) { return NotFound(); }

            var existing = await _context.TTaiKhoan.AsNoTracking().FirstOrDefaultAsync(x => x.MaTk == id);
            if (existing == null) { return NotFound(); }

            // Gán lại các giá trị cố định từ bản ghi gốc
            tTaiKhoan.MaNv = existing.MaNv;
            tTaiKhoan.NgayTao = existing.NgayTao;
            tTaiKhoan.TenDangNhap = existing.TenDangNhap;

            // Loại bỏ các trường không thay đổi/navigation khỏi validation
            ModelState.Remove("MaNv");
            ModelState.Remove("NgayTao");
            ModelState.Remove("MaNvNavigation");
            ModelState.Remove("MaVtNavigation");
            ModelState.Remove("MatKhauHienTai"); // Loại bỏ lỗi validation (nếu có)


            // Logic xử lý Validation cho Mật khẩu
            // 1: KHÔNG muốn đổi mật khẩu (cả 2 đều rỗng)
            if (string.IsNullOrEmpty(tTaiKhoan.MatKhau) && string.IsNullOrEmpty(MatKhauHienTai))
            {
                ModelState.Remove("MatKhau");
                tTaiKhoan.MatKhau = existing.MatKhau; // Gán lại mật khẩu cũ
            }
            // 2: MUỐN đổi mật khẩu (cái mới có giá trị)
            else if (!string.IsNullOrEmpty(tTaiKhoan.MatKhau))
            {
                if (string.IsNullOrEmpty(MatKhauHienTai))
                {
                    ModelState.AddModelError("MatKhauHienTai", "Bắt buộc nhập mật khẩu hiện tại để thay đổi.");
                }
                else if (!PasswordHelper.VerifyPassword(existing.TenDangNhap, MatKhauHienTai, existing.MatKhau))
                {
                    ModelState.AddModelError("MatKhauHienTai", "Mật khẩu hiện tại không đúng.");
                }
                else
                {
                    // Hash mật khẩu mới
                    tTaiKhoan.MatKhau = PasswordHelper.HashPassword(tTaiKhoan.TenDangNhap, tTaiKhoan.MatKhau);
                }
            }
            // 3: Chỉ nhập mật khẩu cũ, không nhập mật khẩu mới
            else if (string.IsNullOrEmpty(tTaiKhoan.MatKhau) && !string.IsNullOrEmpty(MatKhauHienTai))
            {
                ModelState.AddModelError("MatKhau", "Vui lòng nhập mật khẩu mới.");
            }

            // Kiểm tra lại ModelState sau khi xử lý logic mật khẩu
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tTaiKhoan);
                    await _context.SaveChangesAsync();

                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Đã cập nhật Tài khoản <strong>{tTaiKhoan.TenDangNhap}</strong> thành công.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = "Lỗi xung đột dữ liệu. Vui lòng tải lại trang và thử lại.";
                }
                catch (DbUpdateException dbEx)
                {
                    TempData["StatusMessage"] = "danger";
                    if (dbEx.InnerException is SqlException sqlEx && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                    {
                        TempData["Message"] = $"Không thể lưu. Tên đăng nhập <strong>{tTaiKhoan.TenDangNhap}</strong> đã tồn tại.";
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
                    TempData["Message"] = "Lỗi hệ thống: <strong>" + (ex.InnerException?.Message ?? ex.Message) + "</strong>";
                }
            }
            // Xử lý khi LỖI VALIDATION (của mật khẩu hoặc các trường khác)
            else
            {
                TempData["StatusMessage"] = "danger";
                var errors = ModelState.Where(x => x.Value.Errors.Any()).Select(x => $"{x.Value.Errors.First().ErrorMessage}").ToList();
                TempData["Message"] = $"Dữ liệu không hợp lệ. Vui lòng kiểm tra: <ul><li><strong>{string.Join("</strong></li><li><strong>", errors)}</strong></li></ul>";
            }

            // Tải lại ViewBag/ViewData khi thất bại
            ViewData["MaVt"] = new SelectList(_context.TVaiTro, "MaVt", "TenVt", tTaiKhoan.MaVt);
            var nhanVienF = await _context.TNhanVien.FindAsync(tTaiKhoan.MaNv);
            ViewBag.NhanVienLienKet = nhanVienF != null ? $"{nhanVienF.HoDem} {nhanVienF.Ten} ({nhanVienF.MaNv})" : "(Không rõ)";
            tTaiKhoan.MatKhau = null; // Đảm bảo trường mật khẩu hiển thị trống
            return View(tTaiKhoan);
        }

        // =======================================================
        // GET: TTaiKhoans/Delete/5
        // =======================================================
        [Authorization("QTV")]
        [Route("Xoa/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) { return NotFound(); }

            var tTaiKhoan = await _context.TTaiKhoan
                .Include(t => t.MaNvNavigation)
                .Include(t => t.MaVtNavigation)
                .FirstOrDefaultAsync(m => m.MaTk == id);
            if (tTaiKhoan == null) { return NotFound(); }

            return View(tTaiKhoan);
        }

        // =======================================================
        // POST: TTaiKhoans/Delete/5
        // =======================================================
        [Authorization("QTV")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Xoa/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tTaiKhoan = await _context.TTaiKhoan.FindAsync(id);
            if (tTaiKhoan != null)
            {
                try
                {
                    _context.TTaiKhoan.Remove(tTaiKhoan);
                    await _context.SaveChangesAsync();

                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Đã xóa Tài khoản có Mã: <strong>{id}</strong> thành công.";
                }
                catch (DbUpdateException)
                {
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = $"Không thể xóa Tài khoản <strong>{id}</strong> vì đang có dữ liệu tham chiếu đến.";
                }
                catch (Exception ex)
                {
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = $"Lỗi hệ thống khi xóa: <strong>{ex.Message}</strong>";
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // =======================================================
        // HELPER METHODS
        // =======================================================
        private async Task LoadCreateViewData(string selectedMaVt, string selectedMaNv)
        {
            ViewData["MaVt"] = new SelectList(_context.TVaiTro, "MaVt", "TenVt", selectedMaVt);

            var maNvDaCoTaiKhoan = await _context.TTaiKhoan.Select(tk => tk.MaNv).ToListAsync();

            // Danh sách NV chưa có TK
            var nhanVienChuaCoTaiKhoan = await _context.TNhanVien
                .Where(nv => !maNvDaCoTaiKhoan.Contains(nv.MaNv))
                .Select(nv => new { nv.MaNv, TenHienThi = nv.MaNv + " - " + nv.HoDem + " " + nv.Ten })
                .OrderBy(nv => nv.MaNv)
                .ToListAsync();

            // Nếu MaNv đã chọn không null (khi quay lại view do lỗi), và nó không nằm trong danh sách
            // (vì nó đã có TK), hãy thêm nó vào để hiển thị đúng giá trị đã chọn
            if (!string.IsNullOrEmpty(selectedMaNv) && !nhanVienChuaCoTaiKhoan.Any(nv => nv.MaNv == selectedMaNv))
            {
                var selectedNv = await _context.TNhanVien.FindAsync(selectedMaNv);
                if (selectedNv != null)
                {
                    // Thêm vào danh sách để SelectList có thể hiển thị nó
                    var list = nhanVienChuaCoTaiKhoan.Select(nv => new { nv.MaNv, nv.TenHienThi }).ToList();
                    list.Add(new { selectedNv.MaNv, TenHienThi = selectedNv.MaNv + " - " + selectedNv.HoDem + " " + selectedNv.Ten });
                    ViewData["MaNv"] = new SelectList(list.OrderBy(nv => nv.MaNv), "MaNv", "TenHienThi", selectedMaNv);
                }
            }
            else
            {
                ViewData["MaNv"] = new SelectList(nhanVienChuaCoTaiKhoan, "MaNv", "TenHienThi", selectedMaNv);
            }
        }

        private bool TTaiKhoanExists(string id)
        {
            return _context.TTaiKhoan.Any(e => e.MaTk == id);
        }
    }
}