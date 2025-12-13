using Library_Manager.Filters;
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
    [Authorization("QTV,QLB,QLT,QLM")]
    [Route("Nhan-vien")] // THÊM: Route cho Controller
    public class NhanVienController : Controller
    {
        private readonly QlthuVienContext _context;

        public NhanVienController(QlthuVienContext context)
        {
            _context = context;
        }

        // =======================================================
        // GET: NhanVien/Index
        // =======================================================
        [Route("Danh-sach")] // THÊM
        [Route("")] // THÊM
        public IActionResult Index(int? page, string searchString)
        {
            var pageNumber = page ?? 1;
            var pageSize = 6;
            IQueryable<TNhanVien> nhanViens = _context.TNhanVien;

            if (!string.IsNullOrEmpty(searchString))
            {
                nhanViens = nhanViens.Where(nv =>
                    nv.MaNv.Contains(searchString) ||
                    nv.HoDem.ToLower().Contains(searchString.ToLower()) ||
                    nv.Ten.ToLower().Contains(searchString.ToLower()) ||
                    nv.Email.ToLower().Contains(searchString.ToLower()));
            }

            nhanViens = nhanViens.OrderBy(nv => nv.MaNv);
            var pagedNhanViens = new PagedList<TNhanVien>(nhanViens, pageNumber, pageSize);
            ViewBag.CurrentFilter = searchString;

            return View(pagedNhanViens);
        }

        // =======================================================
        // GET: NhanVien/Details/5
        // =======================================================
        [Route("Chi-tiet/{id}")] // THÊM
        public async Task<IActionResult> Details(string id, string returnUrl = null)
        {
            if (id == null) { return NotFound(); }
            var tNhanVien = await _context.TNhanVien.FirstOrDefaultAsync(m => m.MaNv == id);
            if (tNhanVien == null) { return NotFound(); }

            ViewBag.ReturnUrl = returnUrl;
            return View(tNhanVien);
        }

        // XÓA: Đã xóa phương thức Edit(GET) bị trùng lặp ở đây

        // =======================================================
        // GET: NhanVien/Create (Giữ nguyên)
        // =======================================================
        [Route("Them-moi")] // THÊM
        public async Task<IActionResult> Create()
        {
            await PopulatePhuTrachDropDownList();
            return View();
        }

        // =======================================================
        // POST: NhanVien/Create (ĐÃ HOÀN THIỆN)
        // =======================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorization("QTV")]
        [Route("Them-moi")] // THÊM
        public async Task<IActionResult> Create([Bind("HoDem,Ten,NgaySinh,GioiTinh,DiaChi,Sdt,Email,PhuTrach")] TNhanVien tNhanVien)
        {
            // Loại bỏ MaNv khỏi ModelState vì nó được sinh tự động
            ModelState.Remove("MaNv");

            if (ModelState.IsValid)
            {
                try
                {
                    // === LOGIC SINH MÃ TỰ ĐỘNG BẰNG STORED PROCEDURE ===
                    // Khai báo tham số Output: MaNv là CHAR(7)
                    var newMaNvParam = new SqlParameter("@NewMaNV", SqlDbType.Char, 7) { Direction = ParameterDirection.Output };

                    // Thực thi Stored Procedure
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC SP_GenerateNewMaNV @NewMaNV OUTPUT",
                        newMaNvParam
                    );

                    var newMaNvValue = newMaNvParam.Value;

                    if (newMaNvValue == DBNull.Value || string.IsNullOrEmpty(newMaNvValue.ToString()))
                    {
                        // Stored procedure RAISERROR hoặc đạt giới hạn 99
                        throw new InvalidOperationException("Không thể sinh Mã Nhân viên mới (Đã đạt giới hạn/Lỗi hệ thống).");
                    }

                    // 🌟 KHẮC PHỤC: Loại bỏ khoảng trắng đệm (padding) của kiểu CHAR(7)
                    // Đảm bảo Mã NV không bị thừa khoảng trắng, giữ nguyên dấu '-'
                    string maNvString = newMaNvValue.ToString();
                    tNhanVien.MaNv = maNvString.Trim();
                    // ==============================

                    _context.Add(tNhanVien);
                    await _context.SaveChangesAsync();

                    // THÀNH CÔNG -> Redirect về Index
                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Đã tạo mới Nhân viên: <strong>{tNhanVien.HoDem} {tNhanVien.Ten}</strong> với Mã NV: <strong>{tNhanVien.MaNv}</strong>";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbEx)
                {
                    TempData["StatusMessage"] = "danger";
                    if (dbEx.InnerException is SqlException sqlEx &&
                        (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                    {
                        TempData["Message"] = $"Không thể lưu. Email <strong>{tNhanVien.Email}</strong> đã tồn tại trong hệ thống.";
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
            // LỖI -> return View(model)
            else
            {
                TempData["StatusMessage"] = "danger";
                var errors = ModelState.Where(x => x.Value.Errors.Any())
                   .Select(x => $"{x.Key}: {string.Join("; ", x.Value.Errors.Select(e => e.ErrorMessage))}").ToList();
                TempData["Message"] = $"Dữ liệu không hợp lệ. Vui lòng kiểm tra: <ul><li><strong>{string.Join("</strong></li><li><strong>", errors)}</strong></li></ul>";
            }
            await PopulatePhuTrachDropDownList(tNhanVien.PhuTrach); // Tải lại Dropdown
            return View(tNhanVien); // Trả về View để hiển thị lỗi
        }

        // =======================================================
        // GET: NhanVien/Edit/5
        // =======================================================
        [Route("Cap-nhat/{id}")] // THÊM
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) { return NotFound(); }
            var tNhanVien = await _context.TNhanVien.FindAsync(id);
            if (tNhanVien == null) { return NotFound(); }
            await PopulatePhuTrachDropDownList(tNhanVien.PhuTrach); // Tải Dropdown
            return View(tNhanVien);
        }

        // =======================================================
        // POST: NhanVien/Edit/5
        // POST: NhanVien/Edit/5 (Giữ nguyên)
        // =======================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Cap-nhat/{id}")] // THÊM
        public async Task<IActionResult> Edit(string id, [Bind("MaNv,HoDem,Ten,NgaySinh,GioiTinh,DiaChi,Sdt,Email,PhuTrach")] TNhanVien tNhanVien)
        {
            if (id != tNhanVien.MaNv) { return NotFound(); }

            if (ModelState.IsValid)
            {
                try
                {
                    var originalNhanVien = await _context.TNhanVien
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.MaNv == id);

                    if (originalNhanVien == null) { return NotFound(); }

                    _context.Update(tNhanVien);
                    await _context.SaveChangesAsync();

                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Thông tin Nhân viên <strong>{tNhanVien.HoDem} {tNhanVien.Ten}</strong> (Mã: <strong>{tNhanVien.MaNv}</strong>) đã được cập nhật thành công.";
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
                    if (dbEx.InnerException is SqlException sqlEx &&
                        (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                    {
                        TempData["Message"] = $"Không thể lưu. Email <strong>{tNhanVien.Email}</strong> đã tồn tại trong hệ thống.";
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
                    TempData["Message"] = "Lỗi hệ thống khi lưu dữ liệu: <strong>" + ex.Message + "</strong>";
                }
            }
            // LỖI -> return View(model)
            else
            {
                TempData["StatusMessage"] = "danger";
                var errors = ModelState.Where(x => x.Value.Errors.Any())
                   .Select(x => $"{x.Key}: {string.Join("; ", x.Value.Errors.Select(e => e.ErrorMessage))}").ToList();
                TempData["Message"] = $"Dữ liệu không hợp lệ. Vui lòng kiểm tra: <ul><li><strong>{string.Join("</strong></li><li><strong>", errors)}</strong></li></ul>";
            }
            await PopulatePhuTrachDropDownList(tNhanVien.PhuTrach);
            return View(tNhanVien);
        }

        // =======================================================
        // GET & POST: NhanVien/Delete/5 (Giữ nguyên)
        // =======================================================
        [Route("Xoa/{id}")] // THÊM
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) { return NotFound(); }
            var tNhanVien = await _context.TNhanVien.FirstOrDefaultAsync(m => m.MaNv == id);
            if (tNhanVien == null) { return NotFound(); }
            return View(tNhanVien);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Xoa/{id}")] // THÊM
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tNhanVien = await _context.TNhanVien.FindAsync(id);
            if (tNhanVien != null)
            {
                try
                {
                    _context.TNhanVien.Remove(tNhanVien);
                    await _context.SaveChangesAsync();

                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Đã xóa Nhân viên có Mã: <strong>{id}</strong> thành công.";
                }
                catch (DbUpdateException)
                {
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = $"Không thể xóa Nhân viên <strong>{id}</strong> vì đang có dữ liệu (tài khoản, tài liệu) tham chiếu đến.";
                }
                catch (Exception ex)
                {
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = $"Lỗi hệ thống khi xóa: <strong>{ex.Message}</strong>";
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TNhanVienExists(string id)
        {
            return _context.TNhanVien.Any(e => e.MaNv == id);
        }

        // HELPER: Lấy danh sách Phụ trách duy nhất cho Dropdown 
        private async Task PopulatePhuTrachDropDownList(object selectedPhuTrach = null)
        {
            var phuTrachList = await _context.TNhanVien
                .Select(nv => nv.PhuTrach)
                .Where(pt => pt != null && pt != "")
                .Distinct()
                .OrderBy(pt => pt)
                .ToListAsync();

            ViewData["PhuTrachList"] = new SelectList(phuTrachList, selectedPhuTrach);
        }
    }
}