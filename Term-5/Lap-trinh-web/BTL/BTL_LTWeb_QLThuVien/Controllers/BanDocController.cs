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
using Microsoft.Data.SqlClient; // Giữ nếu có sử dụng SQL Parameter, mặc dù BanDoc không dùng sinh mã

namespace Library_Manager.Controllers
{
    [Authorization("QTV,QLB,QLT,QLM")]
    [Route("Ban-doc")]
    public class BanDocController : Controller
    {
        private readonly QlthuVienContext _context;

        public BanDocController(QlthuVienContext context)
        {
            _context = context;
        }

        // GET: TBanDoc/Index
        [Route("")]
        [Route("Danh-sach")]
        public IActionResult Index(int? page, string searchString, string returnUrl)
        {
            var pageNumber = page ?? 1;
            var pageSize = 6;
            IQueryable<TBanDoc> banDocs = _context.TBanDoc;

            if (!string.IsNullOrEmpty(searchString))
            {
                // Giữ nguyên logic tìm kiếm
                banDocs = banDocs.Where(t =>
                t.Ten.ToLower().Contains(searchString) ||
                t.HoDem.ToLower().Contains(searchString) ||
                t.MaBd.ToLower().Contains(searchString) ||
                t.Email.ToLower().Contains(searchString) ||
                (t.GioiTinh == "M" && "nam".Contains(searchString)) ||
                (t.GioiTinh == "F" && "nữ".Contains(searchString)) ||
                (t.GioiTinh != "M" && t.GioiTinh != "F" && "khác".Contains(searchString)) ||
                EF.Functions.Like(t.NgaySinh.Year.ToString(), $"%{searchString}%"));
            }

            banDocs = banDocs.OrderBy(bd => bd.MaBd);
            var pagedBanDocs = new PagedList<TBanDoc>(banDocs, pageNumber, pageSize);

            ViewBag.CurrentFilter = searchString;
            ViewBag.ReturnUrl = returnUrl;

            return View(pagedBanDocs);
        }

        // GET: TBanDoc/Details/5
        [Route("Chi-tiet/{id}")]
        public async Task<IActionResult> Details(string id, string returnUrl = null)
        {
            if (id == null) { return NotFound(); }
            var tBanDoc = await _context.TBanDoc.FirstOrDefaultAsync(m => m.MaBd == id);
            if (tBanDoc == null) { return NotFound(); }
            ViewBag.ReturnUrl = returnUrl;
            return View(tBanDoc);
        }

        // GET: TBanDoc/Create
        [Route("Tao-moi")]
        public IActionResult Create(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: TBanDoc/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Tao-moi")]
        public async Task<IActionResult> Create([Bind("MaBd,HoDem,Ten,NgaySinh,GioiTinh,DiaChi,Sdt,Email")] TBanDoc tBanDoc)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(tBanDoc);
                    await _context.SaveChangesAsync();

                    // LUỒNG THÀNH CÔNG: Redirect về Index
                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Đã tạo mới Bạn đọc: <strong>{tBanDoc.HoDem} {tBanDoc.Ten}</strong> (Mã: <strong>{tBanDoc.MaBd}</strong>) thành công.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbEx)
                {
                    // Lỗi DB (Trùng mã MaBd hoặc lỗi ràng buộc khác)
                    TempData["StatusMessage"] = "danger";
                    if (dbEx.InnerException is SqlException sqlEx && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                    {
                        // Giả định lỗi trùng lặp là do MaBd (Primary Key)
                        TempData["Message"] = $"Không thể lưu. Mã Bạn đọc <strong>{tBanDoc.MaBd}</strong> đã tồn tại.";
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
            // LUỒNG THẤT BẠI: Lỗi Validation
            else
            {
                TempData["StatusMessage"] = "danger";
                var errors = ModelState.Where(x => x.Value.Errors.Any()).Select(x => $"{x.Key}: {string.Join("; ", x.Value.Errors.Select(e => e.ErrorMessage))}");
                TempData["Message"] = $"Dữ liệu không hợp lệ. Vui lòng kiểm tra: <ul><li><strong>{string.Join("</strong></li><li><strong>", errors)}</strong></li></ul>";
            }
            // Trả về View để hiển thị lỗi (Dùng TempData để hiển thị thông báo)
            return View(tBanDoc);
        }

        // GET: TBanDoc/Edit/5
        [Route("Chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) { return NotFound(); }
            var tBanDoc = await _context.TBanDoc.FindAsync(id);
            if (tBanDoc == null) { return NotFound(); }
            return View(tBanDoc);
        }

        // POST: TBanDoc/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id, [Bind("MaBd,HoDem,Ten,NgaySinh,GioiTinh,DiaChi,Sdt,Email")] TBanDoc tBanDoc)
        {
            if (id != tBanDoc.MaBd) { return NotFound(); }

            if (ModelState.IsValid)
            {
                try
                {
                    // Lấy bản ghi gốc để tránh lỗi Concurrency/Tracking
                    var originalBanDoc = await _context.TBanDoc.AsNoTracking().FirstOrDefaultAsync(m => m.MaBd == id);
                    if (originalBanDoc == null) { return NotFound(); }

                    // Cập nhật các trường
                    _context.Update(tBanDoc);
                    await _context.SaveChangesAsync();

                    // LUỒNG THÀNH CÔNG: Redirect về Index
                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Thông tin Bạn đọc <strong>{tBanDoc.HoDem} {tBanDoc.Ten}</strong> (Mã: <strong>{tBanDoc.MaBd}</strong>) đã được cập nhật thành công.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Lỗi xung đột
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = "Lỗi xung đột dữ liệu. Vui lòng tải lại trang và thử lại.";
                }
                catch (DbUpdateException dbEx)
                {
                    // Lỗi DB (Trùng mã MaBd hoặc lỗi ràng buộc khác)
                    TempData["StatusMessage"] = "danger";
                    string innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                    TempData["Message"] = $"Lỗi hệ thống khi lưu: <strong>{innerMessage}</strong>";
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
                var errors = ModelState.Where(x => x.Value.Errors.Any()).Select(x => $"{x.Key}: {string.Join("; ", x.Value.Errors.Select(e => e.ErrorMessage))}");
                TempData["Message"] = $"Dữ liệu không hợp lệ. Vui lòng kiểm tra: <ul><li><strong>{string.Join("</strong></li><li><strong>", errors)}</strong></li></ul>";
            }
            // Trả về View để hiển thị lỗi
            return View(tBanDoc);
        }

        // GET: TBanDoc/Delete/5
        [Route("Xoa/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) { return NotFound(); }
            var tBanDoc = await _context.TBanDoc.FirstOrDefaultAsync(m => m.MaBd == id);
            if (tBanDoc == null) { return NotFound(); }
            return View(tBanDoc);
        }

        // POST: TBanDoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Xoa/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tBanDoc = await _context.TBanDoc.FindAsync(id);
            if (tBanDoc != null)
            {
                try
                {
                    _context.TBanDoc.Remove(tBanDoc);
                    await _context.SaveChangesAsync();

                    // LUỒNG THÀNH CÔNG: Redirect về Index
                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Đã xóa Bạn đọc có Mã: <strong>{id}</strong> thành công.";
                }
                catch (DbUpdateException dbEx)
                {
                    // LUỒNG THẤT BẠI (Khóa ngoại): Redirect về Index
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = $"Không thể xóa Bạn đọc <strong>{id}</strong> vì đang có giao dịch/tài liệu tham chiếu đến. Vui lòng xóa các mục liên quan trước.";
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

        private bool TBanDocExists(string id)
        {
            return _context.TBanDoc.Any(e => e.MaBd == id);
        }
    }
}