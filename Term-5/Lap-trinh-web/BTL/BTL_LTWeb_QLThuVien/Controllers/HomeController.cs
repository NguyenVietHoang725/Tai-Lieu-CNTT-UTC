using Library_Manager.Filters;
using Library_Manager.Models;
using Library_Manager.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Library_Manager.Controllers
{
    [Route("Tong-quan")]
    public class HomeController : Controller
    {
        private readonly QlthuVienContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, QlthuVienContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorization("QTV,QLB,QLT,QLM")]
        [Route("Trang-chu")]
        [Route("")]
        public IActionResult Index()
        {
            string? currentMaTk = HttpContext.Session.GetString("MaTk");
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            DateOnly expiryThreshold = today.AddDays(30);


            // =======================================================
            // KHỐI A: THÔNG TIN NHÂN VIÊN/TÀI KHOẢN CÁ NHÂN
            // =======================================================
            var account = _context.TTaiKhoan
                .Include(tk => tk.MaNvNavigation)
                .Include(tk => tk.MaVtNavigation)
                .FirstOrDefault(tk => tk.MaTk == currentMaTk);

            ViewBag.CurrentAccount = account;
            ViewBag.CurrentEmployee = account?.MaNvNavigation;


            // =======================================================
            // KHỐI B: SỐ LIỆU TỔNG QUAN CHO CÁC KHỐI CHỨC NĂNG
            // =======================================================

            // 1. Quản lý Mượn/Trả
            ViewBag.TotalLoans = _context.TGiaoDichMuonTra.Count(g => g.NgayTra == null);
            ViewBag.OverdueLoans = _context.TGiaoDichMuonTra
                .Count(g => g.NgayHenTra < today && g.NgayTra == null);
            ViewBag.LoansToday = _context.TGiaoDichMuonTra
                 .Count(g => g.NgayMuon == today && g.NgayTra == null);

            // 2. Quản lý Tài liệu/Kho
            ViewBag.TotalCopies = _context.TBanSao.Count();
            ViewBag.AvailableCopies = _context.TBanSao.Count(bs => bs.TrangThai == "Sẵn sàng");

            // 3. Quản lý Bạn đọc
            ViewBag.TotalReaders = _context.TBanDoc.Count();
            ViewBag.LockedReaders = _context.TTheBanDoc.Count(t => t.TrangThai == "Bị khóa");


            // =======================================================
            // KHỐI C: DANH SÁCH CHI TIẾT (LISTS)
            // =======================================================

            // 1. Danh sách Quá hạn lâu nhất (Top 5)
            ViewBag.OverdueList = _context.TGiaoDichMuonTra
                .Where(g => g.NgayHenTra < today && g.NgayTra == null)
                .OrderBy(g => g.NgayHenTra).Take(5).Select(g => new {
                    TenSach = g.TGiaoDichBanSao.FirstOrDefault() != null ?
                                  g.TGiaoDichBanSao.First().MaBsNavigation.MaTlNavigation.TenTl : "N/A",
                    DocGia = g.MaTbdNavigation.MaBdNavigation.HoDem + " " + g.MaTbdNavigation.MaBdNavigation.Ten,
                    // (Đã sửa) Giữ nguyên, đã là string
                    NgayHenTra = g.NgayHenTra.ToString("dd/MM/yyyy")
                }).ToList<dynamic>();

            // 2. Danh sách Tồn kho thấp (Ngưỡng <= 2 bản, Top 5)
            ViewBag.LowStockList = _context.TTaiLieu
                .Select(tl => new {
                    MaTl = tl.MaTl,
                    TenTl = tl.TenTl,
                    SoBanSanSang = tl.TBanSao.Count(bs => bs.TrangThai == "Sẵn sàng")
                }).Where(x => x.SoBanSanSang <= 2).OrderBy(x => x.SoBanSanSang).Take(5).ToList<dynamic>();

            // 3. Danh sách Giao dịch gần nhất (Top 5)
            ViewBag.RecentTransactions = _context.TGiaoDichMuonTra
                .OrderByDescending(g => g.NgayMuon).Take(5).Select(g => new {
                    MaGd = g.MaGd,
                    Loai = g.NgayTra == null ? "Mượn" : "Trả",
                    DocGia = g.MaTbdNavigation.MaBdNavigation.Ten,
                    // (Đã sửa) NgayTra.Value.ToString() - NgayTra là DateOnly?, dùng .Value là an toàn
                    ThoiGian = g.NgayTra != null ? g.NgayTra.Value.ToString("dd/MM/yyyy") : g.NgayMuon.ToString("dd/MM/yyyy")
                }).ToList<dynamic>();

            // 4. Danh sách Bạn đọc mới (Top 5)
            ViewBag.NewReadersList = _context.TBanDoc
                .OrderByDescending(bd => bd.MaBd)
                .Take(5)
                .Select(bd => new {
                    MaBd = bd.MaBd,
                    Ten = bd.HoDem + " " + bd.Ten,
                    // SỬA ĐỔI HOÀN TOÀN: Chuyển NgayCap (DateOnly) thành STRING ngay tại đây
                    NgayTao = bd.TTheBanDoc.FirstOrDefault() != null ?
                              bd.TTheBanDoc.First().NgayCap.ToString("dd/MM/yyyy") : "N/A"
                }).ToList<dynamic>();

            // 5. Số lượng Đầu sách tồn kho thấp (đếm số đầu sách)
            ViewBag.LowStockCount = ((List<dynamic>)ViewBag.LowStockList).Count;

            // 6. Danh sách Thẻ độc giả sắp/đã hết hạn (Top 5)
            // SỬA LỖI: Buộc đánh giá phía client (AsEnumerable) để dùng ToDateTime và .Date
            ViewBag.ExpiringReaders = _context.TTheBanDoc
                .Where(t => t.NgayHetHan.HasValue)
                .Where(t => t.NgayHetHan.Value <= expiryThreshold)
                .OrderBy(t => t.NgayHetHan)
                .Take(5)
                .Select(t => new {
                    MaThe = t.MaTbd,
                    DocGia = t.MaBdNavigation.HoDem + " " + t.MaBdNavigation.Ten,
                    // (Đã sửa) Giữ nguyên, đã là string
                    NgayHetHan = t.NgayHetHan.HasValue ? t.NgayHetHan.Value.ToString("dd/MM/yyyy") : "N/A",
                    IsExpired = t.NgayHetHan.HasValue && t.NgayHetHan.Value < today
                }).ToList<dynamic>();

            return View();
        }

        [Route("Dieu-khoan")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Loi")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}