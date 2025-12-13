using Library_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Manager.Controllers
{
    [Route("Ban-sao")]
    public class BanSaoController : Controller
    {
        private readonly QlthuVienContext _context;

        public BanSaoController(QlthuVienContext context)
        {
            _context = context;
        }

        private bool TBanSaoExists(string id)
        {
            return _context.TBanSao.Any(e => e.MaBs == id);
        }

        // =======================================================
        // 1. INDEX: Danh sách Bản sao (chỉ theo MaTl)
        // =======================================================
        [Route("Danh-sach/{id}")]
        public IActionResult Index(string id, int? page, string searchString)
        {
            var maTl = id;

            if (string.IsNullOrEmpty(maTl))
            {
                TempData["StatusMessage"] = "danger";
                TempData["Message"] = "Vui lòng chọn Tài liệu gốc để xem danh sách Bản sao.";
                return RedirectToAction("Index", "TaiLieu");
            }

            var pageNumber = page ?? 1;
            var pageSize = 6;

            IQueryable<TBanSao> banSaos = _context.TBanSao
                .Include(t => t.MaTlNavigation)
                .Where(bs => bs.MaTl == maTl);

            ViewBag.TenTaiLieu = _context.TTaiLieu
                .Where(t => t.MaTl == maTl)
                .Select(t => t.TenTl)
                .FirstOrDefault();

            ViewBag.MaTl = maTl;
            TempData["LastMaTl"] = maTl;

            if (!string.IsNullOrEmpty(searchString))
            {
                banSaos = banSaos.Where(bs =>
                    bs.MaBs.ToLower().Contains(searchString.ToLower()) ||
                    bs.TrangThai.ToLower().Contains(searchString.ToLower()));
            }

            banSaos = banSaos.OrderBy(bs => bs.MaBs);

            var pagedBanSaos = new PagedList<TBanSao>(banSaos, pageNumber, pageSize);
            ViewBag.CurrentFilter = searchString;

            return View(pagedBanSaos);
        }

        // =======================================================
        // 2. CREATE (GET): Hiển thị form tạo mới
        // =======================================================
        [Route("Them-moi/{MaTl}")]
        public IActionResult Create(string MaTl)
        {
            if (string.IsNullOrEmpty(MaTl) || !_context.TTaiLieu.Any(t => t.MaTl == MaTl))
            {
                TempData["StatusMessage"] = "danger";
                TempData["Message"] = "Không tìm thấy Tài liệu gốc. Vui lòng thử lại.";
                return RedirectToAction("Index", "TaiLieu");
            }

            var taiLieuList = _context.TTaiLieu
                .Select(t => new
                {
                    Value = t.MaTl,
                    Text = t.MaTl + " - " + t.TenTl
                })
                .ToList();

            ViewData["MaTl"] = new SelectList(taiLieuList, "Value", "Text", MaTl);
            ViewBag.SelectedMaTl = MaTl;

            return View();
        }

        // =======================================================
        // 2. CREATE (POST): Sinh mã và Lưu
        // =======================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Them-moi/{MaTl?}")]
        public async Task<IActionResult> Create([Bind("MaTl,TrangThai")] TBanSao tBanSao)
        {
            // ✅ XÓA CÁC TRƯỜNG NAVIGATION KHỎI MODELSTATE
            ModelState.Remove("MaTlNavigation");
            ModelState.Remove("MaBs");

            // ✅ KIỂM TRA MaTl
            if (string.IsNullOrEmpty(tBanSao.MaTl))
            {
                ModelState.AddModelError("MaTl", "Vui lòng chọn Tài liệu gốc.");
            }

            // ✅ KIỂM TRA TRANG THÁI
            if (string.IsNullOrEmpty(tBanSao.TrangThai))
            {
                ModelState.AddModelError("TrangThai", "Vui lòng chọn Tình trạng.");
            }

            // ✅ KIỂM TRA MODELSTATE
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["StatusMessage"] = "danger";
                TempData["Message"] = $"Dữ liệu không hợp lệ: <ul><li>{string.Join("</li><li>", errors)}</li></ul>";
                goto OnError;
            }

            var maTl = tBanSao.MaTl;

            try
            {
                // ✅ SINH MÃ BẢN SAO TỪ STORED PROCEDURE
                var newMaBsParam = new SqlParameter
                {
                    ParameterName = "@NewMaBS",
                    SqlDbType = System.Data.SqlDbType.Char,
                    Size = 14,
                    Direction = System.Data.ParameterDirection.Output
                };
                var maTlParam = new SqlParameter("@MaTL", maTl);

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SP_GenerateNewMaBS @MaTL, @NewMaBS OUT",
                    maTlParam, newMaBsParam);

                var generatedMaBs = newMaBsParam.Value?.ToString()?.Trim();

                if (string.IsNullOrEmpty(generatedMaBs))
                {
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = "Lỗi hệ thống: Stored Procedure không thể tạo mã bản sao. Vui lòng kiểm tra dữ liệu hoặc liên hệ quản trị viên.";
                    goto OnError;
                }

                // ✅ GÁN MÃ VÀ LƯU VÀO DATABASE
                tBanSao.MaBs = generatedMaBs;
                _context.Add(tBanSao);
                await _context.SaveChangesAsync();

                // ✅ THÀNH CÔNG
                TempData["StatusMessage"] = "success";
                TempData["Message"] = $"Tạo Bản sao <strong>{generatedMaBs}</strong> thành công!";
                return RedirectToAction(nameof(Index), new { id = maTl });
            }
            catch (SqlException sqlEx)
            {
                TempData["StatusMessage"] = "danger";
                TempData["Message"] = $"Lỗi cơ sở dữ liệu: {sqlEx.Message}";
                System.Diagnostics.Debug.WriteLine($"SQL Error: {sqlEx}");
                goto OnError;
            }
            catch (Exception ex)
            {
                TempData["StatusMessage"] = "danger";
                TempData["Message"] = $"Lỗi hệ thống: {ex.InnerException?.Message ?? ex.Message}";
                System.Diagnostics.Debug.WriteLine($"Error: {ex}");
                goto OnError;
            }

        OnError:
            // ✅ TẠO LẠI SELECTLIST
            var taiLieuList = _context.TTaiLieu
                .Select(t => new
                {
                    Value = t.MaTl,
                    Text = t.MaTl + " - " + t.TenTl
                })
                .ToList();
            ViewData["MaTl"] = new SelectList(taiLieuList, "Value", "Text", tBanSao.MaTl);
            ViewBag.SelectedMaTl = tBanSao.MaTl;

            return View(tBanSao);
        }

        // =======================================================
        // 3. DELETE (GET): Hiển thị chi tiết xác nhận xóa
        // =======================================================
        [Route("Xoa/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tBanSao = await _context.TBanSao
                .Include(t => t.MaTlNavigation)
                .FirstOrDefaultAsync(m => m.MaBs == id);

            if (tBanSao == null)
            {
                return NotFound();
            }

            ViewBag.MaTlGoc = tBanSao.MaTl;
            return View(tBanSao);
        }

        // =======================================================
        // 3. DELETE (POST): Xác nhận xóa
        // =======================================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Xoa/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tBanSao = await _context.TBanSao.FindAsync(id);
            string maTl = null;

            if (tBanSao != null)
            {
                maTl = tBanSao.MaTl;
                _context.TBanSao.Remove(tBanSao);
                await _context.SaveChangesAsync();

                TempData["StatusMessage"] = "success";
                TempData["Message"] = $"Xóa Bản sao <strong>{id}</strong> thành công!";
                return RedirectToAction(nameof(Index), new { id = maTl });
            }

            return RedirectToAction(nameof(Index), new { id = TempData["LastMaTl"] });
        }
    }
}