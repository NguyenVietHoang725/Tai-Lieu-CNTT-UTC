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
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http; // Cần thiết cho HttpContext.Session.GetString

namespace Library_Manager.Controllers
{
    [Authorization("QTV,QLB,QLT,QLM")]
    [Route("Giao-dich-muon-tra")]
    public class GiaoDichMuonTraController : Controller
    {
        private readonly QlthuVienContext _context;

        public GiaoDichMuonTraController(QlthuVienContext context)
        {
            _context = context;
        }

        // --- Index và Details (Giữ nguyên) ---

        // GET: GiaoDichMuonTra
        [Route("Danh-sach")]
        public IActionResult Index(int? page, string searchString, string returnUrl)
        {
            var pageNumber = page ?? 1;
            var pageSize = 6;

            IQueryable<TGiaoDichMuonTra> giaoDiches = _context.TGiaoDichMuonTra
                .Include(t => t.MaTbdNavigation)
                    .ThenInclude(TheBanDocController => TheBanDocController.MaBdNavigation)
                .Include(t => t.MaTkNavigation);

            if (!string.IsNullOrEmpty(searchString))
            {
                var searchLower = searchString.ToLower();

                giaoDiches = giaoDiches.Where(gd =>
                    gd.MaTbd.ToLower().Contains(searchLower) ||
                    gd.MaGd.ToLower().Contains(searchLower) ||
                    (gd.TrangThai != null && gd.TrangThai.ToLower().Contains(searchLower)) ||
                    EF.Functions.Like(gd.NgayMuon.Year.ToString(), $"%{searchString}%") ||
                    (
                        gd.MaTbdNavigation != null &&
                        gd.MaTbdNavigation.MaBdNavigation != null &&
                        (
                            gd.MaTbdNavigation.MaBdNavigation.Ten.ToLower().Contains(searchLower) ||
                            gd.MaTbdNavigation.MaBdNavigation.HoDem.ToLower().Contains(searchLower) ||
                            gd.MaTbdNavigation.MaBdNavigation.MaBd.ToLower().Contains(searchLower)
                        )
                    )
                );
            }

            giaoDiches = giaoDiches.OrderBy(gd => gd.MaTbd);
            var pagedGiaoDiches = new PagedList<TGiaoDichMuonTra>(giaoDiches, pageNumber, pageSize);
            ViewBag.CurrentFilter = searchString;
            ViewBag.ReturnUrl = returnUrl;
            return View(pagedGiaoDiches);
        }

        // GET: GiaoDichMuonTra/Details/5
        [Route("Chi-tiet/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();

            var tGiaoDichMuonTra = await _context.TGiaoDichMuonTra
                .Include(t => t.MaTbdNavigation)
                    .ThenInclude(BanDocController => BanDocController.MaBdNavigation)
                .Include(t => t.MaTkNavigation)
                    .ThenInclude(NhanVienController => NhanVienController.MaNvNavigation)
                .Include(t => t.TGiaoDichBanSao)
                    .ThenInclude(gdbs => gdbs.MaBsNavigation)
                        .ThenInclude(bs => bs.MaTlNavigation)
                .FirstOrDefaultAsync(m => m.MaGd == id);

            if (tGiaoDichMuonTra == null) return NotFound();

            return View(tGiaoDichMuonTra);
        }


        // --- HÀNH ĐỘNG CREATE ĐÃ ĐIỀU CHỈNH ---

        // GET: GiaoDichMuonTra/Create
        [Route("Tao-moi")]
        public IActionResult Create(string returnUrl)
        {
            var defaultGd = new TGiaoDichMuonTra
            {
                NgayMuon = DateOnly.FromDateTime(DateTime.Now),
                NgayHenTra = DateOnly.FromDateTime(DateTime.Now.AddDays(7))
            };

            ViewBag.ReturnUrl = returnUrl;
            return View(defaultGd);
        }

        // POST: GiaoDichMuonTra/Create (ĐÃ CHỈNH SỬA)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Tao-moi")]
        public async Task<IActionResult> Create(
            [Bind("MaTbd,NgayHenTra")] TGiaoDichMuonTra tGiaoDichMuonTra,
            [FromForm] List<string> selectedBanSaoList)
        {
            // === BƯỚC 1: XÓA LỖI VALIDATION CHO CÁC TRƯỜNG ĐƯỢC GÁN TỰ ĐỘNG VÀ KHÓA NGOẠI NAVIGATION ===

            // Loại bỏ các lỗi Model State cho các trường sẽ được gán giá trị tự động/ẩn.
            // Việc này phải làm trước khi gọi ModelState.IsValid
            ModelState.Remove(nameof(tGiaoDichMuonTra.MaGd));
            ModelState.Remove(nameof(tGiaoDichMuonTra.MaTk));
            ModelState.Remove(nameof(tGiaoDichMuonTra.NgayMuon));
            ModelState.Remove(nameof(tGiaoDichMuonTra.NgayTra));
            ModelState.Remove(nameof(tGiaoDichMuonTra.TrangThai));

            // **KHẮC PHỤC LỖI CỦA BẠN:** Xóa lỗi cho Navigation Properties
            ModelState.Remove(nameof(tGiaoDichMuonTra.MaTkNavigation));
            ModelState.Remove(nameof(tGiaoDichMuonTra.MaTbdNavigation));


            // === BƯỚC 2: GÁN CÁC GIÁ TRỊ TỰ ĐỘNG VÀ KIỂM TRA ĐIỀU KIỆN TIÊN QUYẾT ===

            // 2.1. Tự động sinh MaGD
            string newMaGd;
            try
            {
                newMaGd = await GenerateNewMaGd();
                tGiaoDichMuonTra.MaGd = newMaGd;
            }
            catch (Exception ex)
            {
                TempData["StatusMessage"] = "danger";
                TempData["Message"] = $"Lỗi hệ thống khi sinh mã: <strong>{ex.Message}</strong>";
                return View(tGiaoDichMuonTra);
            }

            // 2.2. Lấy MaTK từ người dùng đang đăng nhập (MaTk)
            var loggedInMaTk = HttpContext.Session.GetString("MaTk");
            if (string.IsNullOrEmpty(loggedInMaTk))
            {
                TempData["StatusMessage"] = "danger";
                TempData["Message"] = "Lỗi hệ thống: <strong>Không tìm thấy Mã Tài khoản nhân viên đang đăng nhập.</strong> Vui lòng đăng nhập lại.";
                return View(tGiaoDichMuonTra);
            }
            tGiaoDichMuonTra.MaTk = loggedInMaTk;

            // 2.3. Mặc định Ngày Mượn, Trang Thái, NgayTra
            tGiaoDichMuonTra.NgayMuon = DateOnly.FromDateTime(DateTime.Now);
            tGiaoDichMuonTra.TrangThai = "Đang mượn";
            tGiaoDichMuonTra.NgayTra = null;

            // 2.4. Kiểm tra danh sách Bản sao
            if (selectedBanSaoList == null || !selectedBanSaoList.Any())
            {
                TempData["StatusMessage"] = "danger";
                TempData["Message"] = "Dữ liệu không hợp lệ: <strong>Giao dịch phải có ít nhất một bản sao tài liệu được chọn.</strong>";
                return View(tGiaoDichMuonTra);
            }

            // === BƯỚC 3: THỰC HIỆN LƯU DATABASE (CHỈ CÒN KIỂM TRA MA TBD VÀ NGÀY HẸN TRẢ) ===
            if (ModelState.IsValid)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // 1. Lưu Giao dịch chính
                    _context.Add(tGiaoDichMuonTra);
                    await _context.SaveChangesAsync();

                    // 2. Lưu Chi tiết Bản sao VÀ CẬP NHẬT TRẠNG THÁI BẢN SAO
                    foreach (var maBs in selectedBanSaoList)
                    {
                        // 2.1. Tạo chi tiết giao dịch
                        var gdbs = new TGiaoDichBanSao
                        {
                            MaGd = tGiaoDichMuonTra.MaGd,
                            MaBs = maBs,
                            TinhTrang = false // false = Đang mượn
                        };
                        _context.TGiaoDichBanSao.Add(gdbs);

                        // 2.2. CẬP NHẬT TRẠNG THÁI BẢN SAO TRONG BẢNG TBanSao
                        var banSao = await _context.TBanSao.FindAsync(maBs);
                        if (banSao != null)
                        {
                            // Cập nhật trạng thái
                            banSao.TrangThai = "Đã mượn"; // Hoặc "Đang mượn" tùy theo quy ước của bạn
                            _context.TBanSao.Update(banSao);
                        }
                    }

                    // Lưu tất cả thay đổi (Giao dịch chi tiết VÀ Cập nhật trạng thái bản sao)
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    // THÔNG BÁO THÀNH CÔNG VÀ CHUYỂN HƯỚNG VỀ INDEX
                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Đã tạo mới Giao dịch: <strong>{tGiaoDichMuonTra.MaGd}</strong> thành công.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    TempData["StatusMessage"] = "danger";
                    // Lấy lỗi sâu hơn
                    string errorMessage = ex.InnerException?.Message ?? ex.Message;
                    TempData["Message"] = "Lỗi hệ thống khi lưu: <strong>" + errorMessage + "</strong>";

                    // Trả về View để hiển thị lỗi ngay trên trang
                    return View(tGiaoDichMuonTra);
                }
            }

            // === BƯỚC 4: XỬ LÝ LỖI MODELSTATE KHÔNG HỢP LỆ ===
            TempData["StatusMessage"] = "danger";
            var errors = ModelState.Where(x => x.Value.Errors.Any())
                           .Select(x => $"{x.Key}: {string.Join("; ", x.Value.Errors.Select(e => e.ErrorMessage))}");
            TempData["Message"] = $"Dữ liệu không hợp lệ. Vui lòng kiểm tra: <ul><li><strong>{string.Join("</strong></li><li><strong>", errors)}</strong></li></ul>";

            return View(tGiaoDichMuonTra);
        }

        // --- CÁC ACTIONS HỖ TRỢ CHO AJAX ---
        // Action: Tìm Thẻ Bạn đọc đang hoạt động (ĐÃ SỬA LỖI ROUTING)
        [HttpGet]
        [Route("Tim-kiem-the-ban-doc")] // THÊM ROUTE
        public async Task<IActionResult> SearchActiveTheBanDoc(string searchTerm)
        {
            try
            {
                // 1. Lấy truy vấn cơ bản: Thẻ đang "hoạt động"
                var query = _context.TTheBanDoc
                                    .Include(t => t.MaBdNavigation)
                                    .Where(t => t.TrangThai.ToLower() == "hoạt động")
                                    .AsQueryable();

                // 2. [ĐÃ SỬA] Chỉ áp dụng bộ lọc TÌM KIẾM nếu searchTerm CÓ NỘI DUNG
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    var searchLower = searchTerm.Trim().ToLower();

                    query = query.Where(t =>
                        // Tìm theo Mã Thẻ
                        t.MaTbd.ToLower().Contains(searchLower) ||

                        // [ĐÃ SỬA] Tìm theo Họ Tên đầy đủ (ghép 2 cột)
                        (t.MaBdNavigation != null &&
                            (t.MaBdNavigation.HoDem + " " + t.MaBdNavigation.Ten).ToLower().Contains(searchLower))
                    );
                }

                // 3. Lấy kết quả từ DB
                var activeCardsQuery = await query
                    .OrderBy(t => t.MaBdNavigation.Ten) // Sắp xếp theo Tên
                    .Select(t => new
                    {
                        t.MaTbd,
                        HoTen = t.MaBdNavigation != null
                                ? t.MaBdNavigation.HoDem + " " + t.MaBdNavigation.Ten
                                : "(Bạn đọc không rõ)",
                        t.NgayHetHan
                    })
                    .Take(50) // Lấy 50 kết quả (nên lấy nhiều hơn 10)
                    .ToListAsync();

                // 4. Định dạng dữ liệu (giữ nguyên logic DateOnly? của bạn)
                var formattedCards = activeCardsQuery.Select(t => new
                {
                    t.MaTbd,
                    t.HoTen,
                    NgayHetHan = t.NgayHetHan.HasValue
                                    ? t.NgayHetHan.Value.ToDateTime(TimeOnly.MinValue).ToString("dd/MM/yyyy")
                                    : ""
                }).ToList();

                return Json(new { success = true, data = formattedCards });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi server khi tìm kiếm: " + ex.Message });
            }
        }

        // Action: Tìm Bản sao tài liệu đang SẴN CÓ (ĐÃ SỬA LỖI ROUTING)
        [HttpGet]
        [Route("Tim-kiem-ban-sao")] // THÊM ROUTE
        public async Task<IActionResult> SearchAvailableBanSao(string searchTerm)
        {
            try
            {
                if (string.IsNullOrEmpty(searchTerm))
                {
                    return Json(new { success = false, message = "Vui lòng nhập từ khóa tìm kiếm." });
                }

                var searchLower = searchTerm.Trim().ToLower();

                var availableCopies = await _context.TBanSao
                    .Include(bs => bs.MaTlNavigation)
                    .Where(bs => !_context.TGiaoDichBanSao.Any(gdbs => gdbs.MaBs == bs.MaBs && gdbs.TinhTrang == false))
                    .Where(bs => bs.MaBs.ToLower().Contains(searchLower)
                                 // Kỹ thuật kiểm tra NULL an toàn cho EF Core
                                 || (bs.MaTlNavigation != null && bs.MaTlNavigation.TenTl.ToLower().Contains(searchLower)))
                    .Select(bs => new
                    {
                        MaBs = bs.MaBs,
                        // Kỹ thuật kiểm tra NULL an toàn khi tạo đối tượng ẩn danh
                        TenTaiLieu = (bs.MaTlNavigation != null ? bs.MaTlNavigation.TenTl : "Không rõ tên tài liệu"),
                        TrangThai = "Sẵn có"
                    })
                    .Take(10)
                    .ToListAsync();

                return Json(new { success = true, data = availableCopies });
            }
            catch (Exception ex)
            {
                // BẮT LỖI: Chuyển lỗi 500 thành phản hồi JSON success: false
                return Json(new { success = false, message = "Lỗi server khi tìm kiếm Bản sao: " + ex.Message });
            }
        }

        // --- HÀM PRIVATE HỖ TRỢ SINH MÃ ---

        private async Task<string> GenerateNewMaGd()
        {
            var pMaGd = new SqlParameter("@NewMaGD", System.Data.SqlDbType.Char, 12)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC SP_GenerateNewMaGD @NewMaGD OUTPUT", pMaGd);

            return pMaGd.Value != DBNull.Value ? pMaGd.Value.ToString().Trim() : throw new Exception("Không thể sinh Mã Giao dịch mới.");
        }


        // --- Edit, Delete (Giữ nguyên) ---

        // GET: GiaoDichMuonTra/Edit/5
        [Route("Chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            // Eager load các thuộc tính cần thiết cho view (Bạn đọc và Danh sách bản sao)
            var tGiaoDichMuonTra = await _context.TGiaoDichMuonTra
                .Include(t => t.MaTbdNavigation)
                    .ThenInclude(tbd => tbd.MaBdNavigation)
                .Include(t => t.TGiaoDichBanSao) // Load chi tiết các bản sao
                    .ThenInclude(gdbs => gdbs.MaBsNavigation)
                        .ThenInclude(bs => bs.MaTlNavigation) // Load tên tài liệu
                .FirstOrDefaultAsync(m => m.MaGd == id);

            if (tGiaoDichMuonTra == null) return NotFound();

            // Không cần ViewData cho MaTbd và MaTk vì chúng được ẩn/tự động gán

            return View(tGiaoDichMuonTra);
        }

        // POST: GiaoDichMuonTra/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Chinh-sua/{id}")]
        // Cần Bind tất cả các trường khóa ngoại và các trường dữ liệu cần thiết
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "MaTbd and NgayMuon are required for entity binding.")]
        public async Task<IActionResult> Edit(string id, [Bind("MaGd,MaTbd,MaTk,NgayMuon,NgayHenTra,NgayTra,TrangThai")] TGiaoDichMuonTra tGiaoDichMuonTra)
        {
            if (id != tGiaoDichMuonTra.MaGd) return NotFound();

            // 1. Lấy MaTK từ người dùng đang đăng nhập VÀ CẬP NHẬT
            var loggedInMaTk = HttpContext.Session.GetString("MaTk");
            if (string.IsNullOrEmpty(loggedInMaTk))
            {
                TempData["StatusMessage"] = "danger";
                TempData["Message"] = "Lỗi hệ thống: <strong>Không tìm thấy Mã Tài khoản nhân viên đang đăng nhập.</strong> Vui lòng đăng nhập lại.";
                return RedirectToAction("Login", "Account");
            }

            // Cập nhật MaTk (Nhân viên xử lý) bằng người dùng đang đăng nhập
            tGiaoDichMuonTra.MaTk = loggedInMaTk;

            // Loại bỏ lỗi ModelState cho các trường được gán/quản lý tự động/ẩn
            ModelState.Remove(nameof(tGiaoDichMuonTra.MaTk));
            ModelState.Remove(nameof(tGiaoDichMuonTra.MaTkNavigation));
            ModelState.Remove(nameof(tGiaoDichMuonTra.MaTbdNavigation));


            if (ModelState.IsValid)
            {
                try
                {
                    // === LOGIC TỰ ĐỘNG CẬP NHẬT NGÀY TRẢ VÀ TRẠNG THÁI GIAO DỊCH ===

                    // 1. Kiểm tra xem tất cả bản sao đã được trả hết chưa
                    // TinhTrang == true (Đã trả)
                    bool allCopiesReturned = await _context.TGiaoDichBanSao
                        .Where(gdbs => gdbs.MaGd == tGiaoDichMuonTra.MaGd)
                        .AllAsync(gdbs => gdbs.TinhTrang == true);

                    // 2. Cập nhật ngày trả và trạng thái giao dịch chính
                    if (allCopiesReturned)
                    {
                        // Nếu tất cả đã được trả, gán Ngày Trả thực tế là ngày hôm nay
                        tGiaoDichMuonTra.NgayTra = DateOnly.FromDateTime(DateTime.Now);

                        // Cập nhật trạng thái giao dịch chính thành "Đã trả"
                        if (tGiaoDichMuonTra.TrangThai != "Đã trả")
                        {
                            tGiaoDichMuonTra.TrangThai = "Đã trả";
                        }
                    }
                    else
                    {
                        // Nếu vẫn còn sách chưa trả, đảm bảo Ngày Trả là NULL
                        tGiaoDichMuonTra.NgayTra = null;

                        // Nếu người dùng lỡ chọn trạng thái là "Đã trả" nhưng chưa trả hết, đặt lại
                        if (tGiaoDichMuonTra.TrangThai == "Đã trả")
                        {
                            tGiaoDichMuonTra.TrangThai = "Đang mượn";
                        }
                    }
                    // ====================================================================

                    // 3. Tiến hành Update
                    _context.Update(tGiaoDichMuonTra);
                    await _context.SaveChangesAsync();

                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Thông tin Giao dịch <strong>{tGiaoDichMuonTra.MaGd}</strong> đã được cập nhật thành công.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TGiaoDichMuonTraExists(tGiaoDichMuonTra.MaGd))
                    {
                        return NotFound();
                    }
                    else
                    {
                        TempData["StatusMessage"] = "danger";
                        TempData["Message"] = "Lỗi xung đột dữ liệu. Vui lòng thử lại.";
                        return RedirectToAction(nameof(Edit), new { id = id });
                    }
                }
                catch (Exception ex)
                {
                    TempData["StatusMessage"] = "danger";
                    TempData["Message"] = "Lỗi hệ thống khi lưu: <strong>" + ex.Message + "</strong>";
                    return RedirectToAction(nameof(Edit), new { id = id });
                }
                return RedirectToAction(nameof(Index));
            }

            // Xử lý ModelState không hợp lệ. Gọi lại GET Edit để hiển thị View với dữ liệu đầy đủ và Validation Error
            TempData["StatusMessage"] = "danger";
            var errors = ModelState.Where(x => x.Value.Errors.Any())
                                    .Select(x => $"{x.Key}: {string.Join("; ", x.Value.Errors.Select(e => e.ErrorMessage))}");
            TempData["Message"] = $"Dữ liệu không hợp lệ. Vui lòng kiểm tra: <ul><li><strong>{string.Join("</strong></li><li><strong>", errors)}</strong></li></ul>";

            // Phải gọi lại action Edit (GET) để load lại các Navigation Properties (MaTbdNavigation, TGiaoDichBanSao)
            // trước khi trả về View
            return await Edit(id);
        }

        // GET: GiaoDichMuonTra/Delete/5
        [Route("Xoa/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();

            var tGiaoDichMuonTra = await _context.TGiaoDichMuonTra
                // 1. Load thông tin Thẻ Bạn đọc (MaTbdNavigation)
                .Include(t => t.MaTbdNavigation)
                    // 2. TỪ Thẻ Bạn đọc, load thông tin Bạn đọc (MaBdNavigation)
                    // (Đã thêm ThenInclude để lấy Tên bạn đọc)
                    .ThenInclude(tbd => tbd.MaBdNavigation)

                // 3. Load thông tin Tài khoản (MaTkNavigation)
                .Include(t => t.MaTkNavigation)
                .FirstOrDefaultAsync(m => m.MaGd == id);

            if (tGiaoDichMuonTra == null) return NotFound();

            return View(tGiaoDichMuonTra);
        }

        // POST: GiaoDichMuonTra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Xoa/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tGiaoDichMuonTra = await _context.TGiaoDichMuonTra.FindAsync(id);

            if (tGiaoDichMuonTra != null)
            {
                try
                {
                    _context.TGiaoDichMuonTra.Remove(tGiaoDichMuonTra);
                    await _context.SaveChangesAsync();

                    TempData["StatusMessage"] = "success";
                    TempData["Message"] = $"Đã xóa Giao dịch có Mã: <strong>{id}</strong> thành công.";
                }
                catch (DbUpdateException ex)
                {
                    TempData["StatusMessage"] = "danger";
                    // Thông báo lỗi nếu có ràng buộc ngoại
                    TempData["Message"] = $"Không thể xóa giao dịch <strong>{id}</strong> vì có thể đang có các bản sao liên quan. Vui lòng kiểm tra các bản sao trong giao dịch.";
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

        [HttpPost]
        [Route("Cap-nhat-tinh-trang-ban-sao")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBanSaoStatus(string maGd, string maBs, bool tinhTrang)
        {
            try
            {
                // 1. Tìm chi tiết giao dịch bản sao
                var giaoDichBanSao = await _context.TGiaoDichBanSao
                    .FirstOrDefaultAsync(gdbs => gdbs.MaGd == maGd && gdbs.MaBs == maBs);

                if (giaoDichBanSao == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy bản sao trong giao dịch này." });
                }

                // 2. Cập nhật tình trạng
                giaoDichBanSao.TinhTrang = tinhTrang;
                _context.TGiaoDichBanSao.Update(giaoDichBanSao);

                // 3. Nếu đánh dấu là "Đã trả", cập nhật trạng thái bản sao trong bảng TBanSao
                if (tinhTrang) // true = Đã trả
                {
                    var banSao = await _context.TBanSao.FindAsync(maBs);
                    if (banSao != null)
                    {
                        banSao.TrangThai = "Sẵn có"; // Hoặc "Có sẵn" tùy quy ước của bạn
                        _context.TBanSao.Update(banSao);
                    }
                }

                // 4. Lưu thay đổi
                await _context.SaveChangesAsync();

                // 5. Kiểm tra xem tất cả bản sao đã được trả hết chưa để tự động cập nhật trạng thái giao dịch
                bool allCopiesReturned = await _context.TGiaoDichBanSao
                    .Where(gdbs => gdbs.MaGd == maGd)
                    .AllAsync(gdbs => gdbs.TinhTrang == true);

                if (allCopiesReturned)
                {
                    // Tự động cập nhật trạng thái giao dịch chính
                    var giaoDich = await _context.TGiaoDichMuonTra.FindAsync(maGd);
                    if (giaoDich != null)
                    {
                        giaoDich.NgayTra = DateOnly.FromDateTime(DateTime.Now);
                        giaoDich.TrangThai = "Đã trả";
                        _context.TGiaoDichMuonTra.Update(giaoDich);
                        await _context.SaveChangesAsync();
                    }
                }

                return Json(new
                {
                    success = true,
                    message = $"Đã cập nhật tình trạng bản sao {maBs} thành công.",
                    allReturned = allCopiesReturned
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = $"Lỗi khi cập nhật: {ex.Message}"
                });
            }
        }

        private bool TGiaoDichMuonTraExists(string id)
        {
            return _context.TGiaoDichMuonTra.Any(e => e.MaGd == id);
        }
    }
}