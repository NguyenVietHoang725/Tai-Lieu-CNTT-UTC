using Library_Manager.Filters;
using Library_Manager.Models;
using Library_Manager.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using System.IO;

namespace Library_Manager.Controllers
{
    [Authorization("QTV,QLB,QLT,QLM")]
    [Route("Tai-lieu")]
    public class TaiLieuController : Controller
    {
        private readonly QlthuVienContext _context;
        private readonly IBufferedFileUploadService _fileUploadService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public TaiLieuController(QlthuVienContext context,
                                 IBufferedFileUploadService fileUploadService,
                                 IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _fileUploadService = fileUploadService;
            _hostingEnvironment = hostingEnvironment;
        }

        #region Các chức năng cơ bản (Index, Details, PopulateSelectList, Delete)

        [Route("Danh-sach")]
        public IActionResult Index(int? page, string searchString, string category, string publisher, string language, string returnUrl)
        {
            var pageNumber = page ?? 1;
            var pageSize = 6;

            IQueryable<TTaiLieu> taiLieus = _context.TTaiLieu
                .Include(t => t.MaDdNavigation).Include(t => t.MaNnNavigation).Include(t => t.MaNxbNavigation)
                .Include(t => t.MaThLNavigation).Include(t => t.MaTkNavigation);

            if (!string.IsNullOrEmpty(searchString))
            {
                taiLieus = taiLieus.Where(tl =>
                    tl.TenTl.ToLower().Contains(searchString.ToLower()) ||
                    tl.MaTl.ToLower().Contains(searchString.ToLower()) ||
                    (tl.MaNxbNavigation != null && tl.MaNxbNavigation.TenNxb.ToLower().Contains(searchString.ToLower())) ||
                    (tl.MaThLNavigation != null && tl.MaThLNavigation.TenThL.ToLower().Contains(searchString.ToLower())));
            }

            if (!string.IsNullOrEmpty(category))
                taiLieus = taiLieus.Where(tl => tl.MaThLNavigation.TenThL == category);

            if (!string.IsNullOrEmpty(publisher))
                taiLieus = taiLieus.Where(tl => tl.MaNxbNavigation.TenNxb == publisher);

            if (!string.IsNullOrEmpty(language))
                taiLieus = taiLieus.Where(tl => tl.MaNnNavigation.TenNn == language);

            taiLieus = taiLieus.OrderBy(tl => tl.MaTl);

            var pagedTaiLieus = new PagedList.Core.PagedList<TTaiLieu>(taiLieus, pageNumber, pageSize);

            ViewBag.CurrentFilter = searchString;
            ViewBag.Categories = _context.TTheLoai.Select(c => c.TenThL).Distinct().ToList();
            ViewBag.Publishers = _context.TNhaXuatBan.Select(p => p.TenNxb).Distinct().ToList();
            ViewBag.Languages = _context.TNgonNgu.Select(l => l.TenNn).Distinct().ToList();
            ViewBag.SelectedCategory = category;
            ViewBag.SelectedPublisher = publisher;
            ViewBag.SelectedLanguage = language;
            ViewBag.ReturnUrl = returnUrl;

            return View(pagedTaiLieus);
        }

        [Route("Chi-tiet/{id}")]
        public async Task<IActionResult> Details(string id, string returnUrl)
        {
            if (id == null) { return NotFound(); }
            var tTaiLieu = await _context.TTaiLieu
                .Include(t => t.MaDdNavigation)
                .Include(t => t.MaNnNavigation)
                .Include(t => t.MaNxbNavigation)
                .Include(t => t.MaThLNavigation)
                .Include(t => t.MaTkNavigation)
                .Include(t => t.TTaiLieuTacGia)
                .Include(t => t.TBanSao)
                .FirstOrDefaultAsync(m => m.MaTl == id);
            if (tTaiLieu == null) { return NotFound(); }
            ViewBag.ReturnUrl = returnUrl;
            return View(tTaiLieu);
        }

        private void PopulateSelectList(TTaiLieu tTaiLieu = null)
        {
            ViewData["MaDd"] = new SelectList(_context.TDinhDang, "MaDd", "TenDd", tTaiLieu?.MaDd);
            ViewData["MaNn"] = new SelectList(_context.TNgonNgu, "MaNn", "TenNn", tTaiLieu?.MaNn);
            ViewData["MaNxb"] = new SelectList(_context.TNhaXuatBan, "MaNxb", "TenNxb", tTaiLieu?.MaNxb);
            ViewData["MaThL"] = new SelectList(_context.TTheLoai, "MaThL", "TenThL", tTaiLieu?.MaThL);
            ViewData["MaTk"] = new SelectList(_context.TTaiKhoan, "MaTk", "MaTk", tTaiLieu?.MaTk);
            ViewData["QuocGiaList"] = new SelectList(_context.TQuocGia.OrderBy(q => q.TenQg), "MaQg", "TenQg");
            ViewData["TacGiaList"] = new SelectList(
                _context.TTacGia.Select(tg => new { tg.MaTg, FullName = tg.HoDem + " " + tg.Ten }).OrderBy(x => x.FullName),
                "MaTg", "FullName"
            );
        }

        [Route("Chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id, string returnUrl)
        {
            if (id == null) { return NotFound(); }
            var tTaiLieu = await _context.TTaiLieu
                .Include(t => t.TTaiLieuTacGia)
                .ThenInclude(ttg => ttg.MaTgNavigation)
                .FirstOrDefaultAsync(m => m.MaTl == id);

            if (tTaiLieu == null) { return NotFound(); }
            PopulateSelectList(tTaiLieu);
            ViewBag.ReturnUrl = returnUrl;
            return View(tTaiLieu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Xoa/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tTaiLieu = await _context.TTaiLieu.FindAsync(id);
            if (tTaiLieu != null) { _context.TTaiLieu.Remove(tTaiLieu); }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Route("Xoa/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) { return NotFound(); }
            var tTaiLieu = await _context.TTaiLieu
                .Include(t => t.MaNxbNavigation)
                .Include(t => t.MaThLNavigation)
                .Include(t => t.MaNnNavigation)
                .Include(t => t.MaDdNavigation)
                .Include(t => t.MaTkNavigation)
                .Include(t => t.TBanSao)
                .FirstOrDefaultAsync(m => m.MaTl == id);

            if (tTaiLieu == null) { return NotFound(); }
            return View(tTaiLieu);
        }

        private bool TTaiLieuExists(string id)
        {
            return _context.TTaiLieu.Any(e => e.MaTl == id);
        }
        #endregion

        #region Create & Edit Actions

        [Route("Tao-moi")]
        public IActionResult Create(string returnUrl)
        {
            PopulateSelectList();
            ViewBag.ReturnUrl = returnUrl;
            var maTk = HttpContext.Session.GetString("MaTk");
            var newTaiLieu = new TTaiLieu();
            if (!string.IsNullOrEmpty(maTk))
            {
                newTaiLieu.MaTk = maTk;
            }
            return View(newTaiLieu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Tao-moi")]
        public async Task<IActionResult> Create(
            [Bind("MaNxb,MaNn,MaThL,MaDd,TenTl,LanXuatBan,NamXuatBan,SoTrang,KhoCo")] TTaiLieu tTaiLieu,
            ICollection<TTaiLieuTacGia> TTaiLieuTacGia,
            IFormFile imageFile)
        {
            var maTkSession = HttpContext.Session.GetString("MaTk");

            if (string.IsNullOrEmpty(maTkSession))
            {
                TempData["StatusMessage"] = "danger";
                TempData["Message"] = "Lỗi: Phiên đăng nhập không hợp lệ. Vui lòng đăng nhập lại.";
                PopulateSelectList(tTaiLieu);
                return View(tTaiLieu);
            }

            tTaiLieu.MaTk = maTkSession;

            ModelState.Remove("MaTl");
            ModelState.Remove("MaTk");
            ModelState.Remove("MaDdNavigation");
            ModelState.Remove("MaNnNavigation");
            ModelState.Remove("MaTkNavigation");
            ModelState.Remove("MaNxbNavigation");
            ModelState.Remove("MaThLNavigation");

            if (string.IsNullOrEmpty(tTaiLieu.MaNxb)) ModelState.AddModelError("MaNxb", "Nhà xuất bản là bắt buộc.");
            if (string.IsNullOrEmpty(tTaiLieu.MaNn)) ModelState.AddModelError("MaNn", "Ngôn ngữ là bắt buộc.");
            if (string.IsNullOrEmpty(tTaiLieu.MaThL)) ModelState.AddModelError("MaThL", "Thể loại là bắt buộc.");
            if (string.IsNullOrEmpty(tTaiLieu.MaDd)) ModelState.AddModelError("MaDd", "Định dạng là bắt buộc.");

            if (TTaiLieuTacGia != null)
            {
                for (int i = 0; i < TTaiLieuTacGia.Count; i++)
                {
                    ModelState.Remove($"TTaiLieuTacGia[{i}].MaTgNavigation");
                    ModelState.Remove($"TTaiLieuTacGia[{i}].MaTlNavigation");
                    ModelState.Remove($"TTaiLieuTacGia[{i}].MaTl");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string newMaTl = await GenerateNewMaTlBySP(tTaiLieu.MaNn);

                    if (string.IsNullOrEmpty(newMaTl))
                    {
                        throw new Exception("Lỗi sinh mã tài liệu: Stored Procedure trả về giá trị rỗng.");
                    }

                    tTaiLieu.MaTl = newMaTl;

                    if (imageFile != null)
                    {
                        string relativePath = await _fileUploadService.UploadFile(imageFile, tTaiLieu.MaTl);
                        tTaiLieu.Anh = relativePath;
                    }

                    _context.Add(tTaiLieu);

                    if (TTaiLieuTacGia != null)
                    {
                        foreach (var author in TTaiLieuTacGia)
                        {
                            author.MaTl = tTaiLieu.MaTl;
                            _context.TTaiLieuTacGia.Add(author);
                        }
                    }

                    await _context.SaveChangesAsync();

                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Tài liệu **{tTaiLieu.MaTl}** ({tTaiLieu.TenTl}) đã được tạo mới thành công.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrEmpty(tTaiLieu.Anh))
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, tTaiLieu.Anh.TrimStart('/'));
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }

                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = "Lỗi hệ thống khi tạo mới: " + (ex.InnerException?.Message ?? ex.Message);
                }
            }
            else
            {
                TempData["StatusMessage"] = "danger";
                var errors = ModelState.Where(x => x.Value.Errors.Any())
                   .Select(x => $"{x.Key}: {string.Join("; ", x.Value.Errors.Select(e => e.ErrorMessage))}");
                TempData["Message"] = $"Dữ liệu không hợp lệ. Vui lòng kiểm tra: <ul><li>{string.Join("</li><li>", errors)}</li></ul>";
            }

            if (TTaiLieuTacGia != null)
            {
                tTaiLieu.TTaiLieuTacGia = new List<TTaiLieuTacGia>();
                foreach (var item in TTaiLieuTacGia)
                {
                    item.MaTgNavigation = await _context.TTacGia.AsNoTracking().FirstOrDefaultAsync(t => t.MaTg == item.MaTg);
                    tTaiLieu.TTaiLieuTacGia.Add(item);
                }
            }

            PopulateSelectList(tTaiLieu);
            return View(tTaiLieu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id,
            [Bind("MaTl,MaNxb,MaThL,MaDd,TenTl,LanXuatBan,NamXuatBan,SoTrang,KhoCo,Anh")] TTaiLieu tTaiLieu,
            ICollection<TTaiLieuTacGia> TTaiLieuTacGia,
            IFormFile imageFile)
        {
            if (id != tTaiLieu.MaTl) { return NotFound(); }

            var maTkSession = HttpContext.Session.GetString("MaTk");
            if (string.IsNullOrEmpty(maTkSession))
            {
                TempData["StatusMessage"] = "danger";
                TempData["Message"] = "Lỗi: Phiên đăng nhập không hợp lệ. Vui lòng đăng nhập lại.";
                PopulateSelectList(tTaiLieu);
                return View(tTaiLieu);
            }

            var originalTaiLieuForMaNn = await _context.TTaiLieu.AsNoTracking().FirstOrDefaultAsync(m => m.MaTl == id);
            string maNnOriginal = originalTaiLieuForMaNn?.MaNn;

            ModelState.Remove("MaDdNavigation");
            ModelState.Remove("MaNnNavigation");
            ModelState.Remove("MaTkNavigation");
            ModelState.Remove("MaNxbNavigation");
            ModelState.Remove("MaThLNavigation");
            ModelState.Remove("MaNn");
            ModelState.Remove("MaTk");

            var tacGiaKeys = ModelState.Keys.Where(k => k.StartsWith("TTaiLieuTacGia")).ToList();
            foreach (var key in tacGiaKeys) { ModelState.Remove(key); }

            if (imageFile == null && ModelState.ContainsKey("imageFile") && ModelState["imageFile"].Errors.Any())
            {
                ModelState["imageFile"].Errors.Clear();
            }

            if (string.IsNullOrEmpty(tTaiLieu.MaNxb)) ModelState.AddModelError("MaNxb", "Nhà xuất bản là bắt buộc.");
            if (string.IsNullOrEmpty(tTaiLieu.MaThL)) ModelState.AddModelError("MaThL", "Thể loại là bắt buộc.");
            if (string.IsNullOrEmpty(tTaiLieu.MaDd)) ModelState.AddModelError("MaDd", "Định dạng là bắt buộc.");

            var remainingErrors = ModelState.Where(x => x.Value.Errors.Any()).ToList();
            if (!remainingErrors.Any()) { ModelState.Clear(); }

            if (ModelState.IsValid)
            {
                var originalTaiLieu = await _context.TTaiLieu
                    .Include(t => t.TTaiLieuTacGia).FirstOrDefaultAsync(m => m.MaTl == id);
                if (originalTaiLieu == null) { return NotFound(); }

                try
                {
                    if (imageFile != null)
                    {
                        if (!string.IsNullOrEmpty(originalTaiLieu.Anh))
                        {
                            string oldFilePath = Path.Combine(_hostingEnvironment.WebRootPath, originalTaiLieu.Anh.TrimStart('/'));
                            if (System.IO.File.Exists(oldFilePath)) { System.IO.File.Delete(oldFilePath); }
                        }
                        string newRelativePath = await _fileUploadService.UploadFile(imageFile, originalTaiLieu.MaTl);
                        tTaiLieu.Anh = newRelativePath;
                    }
                    else
                    {
                        tTaiLieu.Anh = originalTaiLieu.Anh;
                    }

                    tTaiLieu.MaNn = maNnOriginal;
                    tTaiLieu.MaTk = maTkSession;

                    _context.Entry(originalTaiLieu).CurrentValues.SetValues(tTaiLieu);

                    originalTaiLieu.TTaiLieuTacGia.Clear();
                    if (TTaiLieuTacGia != null && TTaiLieuTacGia.Any())
                    {
                        foreach (var author in TTaiLieuTacGia)
                        {
                            author.MaTl = originalTaiLieu.MaTl;
                            originalTaiLieu.TTaiLieuTacGia.Add(author);
                        }
                    }
                    await _context.SaveChangesAsync();

                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Thông tin Tài liệu <strong>{tTaiLieu.TenTl}</strong> đã được cập nhật thành công.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = "Lỗi xung đột dữ liệu. Vui lòng thử lại.";
                }
                catch (Exception ex)
                {
                    if (imageFile != null && !string.IsNullOrEmpty(tTaiLieu.Anh))
                    {
                        string newFilePath = Path.Combine(_hostingEnvironment.WebRootPath, tTaiLieu.Anh.TrimStart('/'));
                        if (System.IO.File.Exists(newFilePath)) { System.IO.File.Delete(newFilePath); }
                    }
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = "Lỗi hệ thống khi lưu dữ liệu: <strong>" + (ex.InnerException?.Message ?? ex.Message) + "</strong>";
                }
            }

            tTaiLieu.MaNn = maNnOriginal;
            tTaiLieu.MaTk = maTkSession;

            if (TempData["StatusMessage"] == null)
            {
                var actualErrors = ModelState
                    .Where(x => x.Value.Errors.Any() && !x.Key.Contains("Navigation"))
                    .Select(x => $"{x.Key}: {string.Join("; ", x.Value.Errors.Select(e => e.ErrorMessage))}")
                    .ToList();

                if (actualErrors.Any())
                {
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = $"Dữ liệu không hợp lệ. Vui lòng kiểm tra: <ul><li><strong>{string.Join("</strong></li><li><strong>", actualErrors)}</strong></li></ul>";
                }
            }

            if (TTaiLieuTacGia != null)
            {
                var tTaiLieuDisplay = new TTaiLieu();
                _context.Entry(tTaiLieuDisplay).CurrentValues.SetValues(tTaiLieu);

                var currentTaiLieu = await _context.TTaiLieu
                    .Include(t => t.MaNnNavigation)
                    .AsNoTracking().FirstOrDefaultAsync(m => m.MaTl == id);

                tTaiLieuDisplay.MaNnNavigation = currentTaiLieu?.MaNnNavigation;
                tTaiLieuDisplay.MaTkNavigation = await _context.TTaiKhoan
                    .Include(tk => tk.MaNvNavigation)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(tk => tk.MaTk == tTaiLieu.MaTk);

                tTaiLieuDisplay.TTaiLieuTacGia = new List<TTaiLieuTacGia>();
                foreach (var item in TTaiLieuTacGia)
                {
                    item.MaTgNavigation = await _context.TTacGia.AsNoTracking().FirstOrDefaultAsync(t => t.MaTg == item.MaTg);
                    tTaiLieuDisplay.TTaiLieuTacGia.Add(item);
                }
                tTaiLieu = tTaiLieuDisplay;
            }

            PopulateSelectList(tTaiLieu);
            return View(tTaiLieu);
        }

        #endregion

        #region Stored Procedure Helpers

        private async Task<string> GenerateNewMaTlBySP(string maNn)
        {
            if (string.IsNullOrEmpty(maNn)) { maNn = "VI"; }
            var newMaTlParam = new SqlParameter("@NewMaTl", System.Data.SqlDbType.Char, 10)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            var maNnParam = new SqlParameter("@MaNN", maNn);
            await _context.Database.ExecuteSqlRawAsync("EXEC SP_GenerateNewMaTl @MaNN, @NewMaTl OUTPUT", maNnParam, newMaTlParam);
            return newMaTlParam.Value?.ToString();
        }

        private async Task<string> GenerateNewMaTgBySP(string maQg)
        {
            if (string.IsNullOrEmpty(maQg)) { return null; }
            var newMaTgParam = new SqlParameter("@NewMaTg", System.Data.SqlDbType.NVarChar, 50)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            var maQgParam = new SqlParameter("@MaQg", maQg);
            await _context.Database.ExecuteSqlRawAsync("EXEC SP_GenerateNewMaTg @MaQg, @NewMaTg OUTPUT", maQgParam, newMaTgParam);
            return newMaTgParam.Value?.ToString();
        }

        private async Task<string> GenerateNewMaNxbBySP(string maQg)
        {
            if (string.IsNullOrEmpty(maQg)) { return null; }
            var newMaNxbParam = new SqlParameter("@NewMaNxb", System.Data.SqlDbType.NVarChar, 50)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            var maQgParam = new SqlParameter("@MaQg", maQg);
            await _context.Database.ExecuteSqlRawAsync("EXEC SP_GenerateNewMaNxb @MaQg, @NewMaNxb OUTPUT", maQgParam, newMaNxbParam);
            return newMaNxbParam.Value?.ToString();
        }

        private async Task<string> GenerateNewMaThLBySP()
        {
            var newMaThLParam = new SqlParameter("@NewMaThL", System.Data.SqlDbType.Char, 6)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC SP_GenerateNewMaThL @NewMaThL OUTPUT", newMaThLParam);
            return newMaThLParam.Value?.ToString();
        }

        private async Task<string> GenerateNewMaDdBySP()
        {
            var newMaDdParam = new SqlParameter("@NewMaDD", System.Data.SqlDbType.Char, 5)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC SP_GenerateNewMaDD @NewMaDD OUTPUT", newMaDdParam);
            return newMaDdParam.Value?.ToString();
        }

        #endregion

        #region AJAX Actions

        // ***** SỬA CHÍNH: THÊM MaQg VÀO TTacGia *****
        [HttpPost]
        [Authorization("QLT")]
        [Route(nameof(CreateNewTacGiaAjax))]
        public async Task<IActionResult> CreateNewTacGiaAjax([FromBody] TacGiaModel model)
        {
            // Debug log
            System.Diagnostics.Debug.WriteLine($"Received - HoDem: {model?.HoDem}, Ten: {model?.Ten}, MaQg: {model?.MaQg}");

            if (string.IsNullOrEmpty(model?.HoDem) || string.IsNullOrEmpty(model.Ten) || string.IsNullOrEmpty(model.MaQg))
            {
                return Json(new { success = false, message = "Họ đệm, Tên và Quốc gia không được để trống." });
            }

            try
            {
                string newMaTg = await GenerateNewMaTgBySP(model.MaQg);

                if (string.IsNullOrEmpty(newMaTg))
                {
                    return Json(new { success = false, message = "Lỗi sinh mã Tác giả. Đã đạt giới hạn (999) hoặc MaQg không hợp lệ." });
                }

                var newTacGia = new TTacGia
                {
                    MaTg = newMaTg,
                    HoDem = model.HoDem,
                    Ten = model.Ten,
                    MaQg = model.MaQg  // *** QUAN TRỌNG: THÊM DÒNG NÀY ***
                };

                _context.TTacGia.Add(newTacGia);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    maTg = newTacGia.MaTg,
                    hoDem = newTacGia.HoDem,
                    ten = newTacGia.Ten,
                    fullName = newTacGia.HoDem + " " + newTacGia.Ten
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating TacGia: {ex.Message}");
                return Json(new { success = false, message = "Lỗi Database: " + (ex.InnerException?.Message ?? ex.Message) });
            }
        }

        // ***** SỬA CHÍNH: THÊM MaQg VÀO TNhaXuatBan *****
        [HttpPost]
        [Authorization("QLT")]
        [Route(nameof(CreateNewNxbAjax))]
        public async Task<IActionResult> CreateNewNxbAjax([FromBody] NxbModel model)
        {
            System.Diagnostics.Debug.WriteLine($"Received NXB - TenNxb: {model?.TenNxb}, MaQg: {model?.MaQg}");

            if (string.IsNullOrEmpty(model?.TenNxb) || string.IsNullOrEmpty(model.MaQg))
            {
                return Json(new { success = false, message = "Tên Nhà xuất bản và Quốc gia không được để trống." });
            }

            try
            {
                string newMaNxb = await GenerateNewMaNxbBySP(model.MaQg);

                if (string.IsNullOrEmpty(newMaNxb))
                {
                    return Json(new { success = false, message = "Lỗi sinh mã Nhà xuất bản. Đã đạt giới hạn (999) hoặc MaQg không hợp lệ." });
                }

                var newNxb = new TNhaXuatBan
                {
                    MaNxb = newMaNxb,
                    TenNxb = model.TenNxb,
                    MaQg = model.MaQg  // *** QUAN TRỌNG: THÊM DÒNG NÀY (nếu TNhaXuatBan có trường MaQg) ***
                };

                _context.TNhaXuatBan.Add(newNxb);
                await _context.SaveChangesAsync();
                return Json(new { success = true, maNxb = newNxb.MaNxb, tenNxb = newNxb.TenNxb });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating NXB: {ex.Message}");
                return Json(new { success = false, message = "Lỗi Database: " + (ex.InnerException?.Message ?? ex.Message) });
            }
        }

        [HttpPost]
        [Authorization("QLT")]
        [Route(nameof(CreateNewTheLoaiAjax))]
        public async Task<IActionResult> CreateNewTheLoaiAjax([FromBody] ThLModel model)
        {
            if (string.IsNullOrEmpty(model?.TenThL))
            {
                return Json(new { success = false, message = "Tên Thể loại không được để trống." });
            }

            try
            {
                string newMaThL = await GenerateNewMaThLBySP();

                if (string.IsNullOrEmpty(newMaThL))
                {
                    return Json(new { success = false, message = "Lỗi sinh mã Thể loại. Đã đạt giới hạn (999)." });
                }

                var newThL = new TTheLoai { MaThL = newMaThL, TenThL = model.TenThL };

                _context.TTheLoai.Add(newThL);
                await _context.SaveChangesAsync();
                return Json(new { success = true, maThL = newThL.MaThL, tenThL = newThL.TenThL });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi Database: " + (ex.InnerException?.Message ?? ex.Message) });
            }
        }

        [HttpPost]
        [Authorization("QLT")]
        [Route(nameof(CreateNewDinhDangAjax))]
        public async Task<IActionResult> CreateNewDinhDangAjax([FromBody] DdModel model)
        {
            if (string.IsNullOrEmpty(model?.TenDd))
            {
                return Json(new { success = false, message = "Tên Định dạng không được để trống." });
            }

            try
            {
                string newMaDd = await GenerateNewMaDdBySP();

                if (string.IsNullOrEmpty(newMaDd))
                {
                    return Json(new { success = false, message = "Lỗi sinh mã Định dạng. Đã đạt giới hạn (999)." });
                }

                var newDd = new TDinhDang { MaDd = newMaDd, TenDd = model.TenDd };

                _context.TDinhDang.Add(newDd);
                await _context.SaveChangesAsync();
                return Json(new { success = true, maDd = newDd.MaDd, tenDd = newDd.TenDd });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi Database: " + (ex.InnerException?.Message ?? ex.Message) });
            }
        }

        #endregion

        #region AJAX Models

        public class TacGiaModel
        {
            public string HoDem { get; set; }
            public string Ten { get; set; }
            public string MaQg { get; set; }
        }

        public class NxbModel
        {
            public string TenNxb { get; set; }
            public string MaQg { get; set; }
        }

        public class ThLModel
        {
            public string TenThL { get; set; }
        }

        public class DdModel
        {
            public string TenDd { get; set; }
        }

        #endregion
    }
}