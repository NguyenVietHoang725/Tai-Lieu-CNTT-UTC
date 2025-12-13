using Library_Manager.Helpers;
using Library_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http; // Đảm bảo có using này để dùng HttpContext.Session.GetString

namespace Library_Manager.Controllers
{
    [Route("Auth")]
    public class AccountController : Controller
    {
        private readonly QlthuVienContext _context;

        public AccountController(QlthuVienContext context)
        {
            _context = context;
        }

        // ==========================================================
        // KHỐI 1: ACTION LOGIN (HIỂN THỊ FORM)
        // ==========================================================
        [HttpGet]
        [Route("Dang-nhap")]
        [Route("")]
        [Route("~/")]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("MaTk") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // ==========================================================
        // KHỐI 2: ACTION LOGIN (XỬ LÝ ĐĂNG NHẬP)
        // ==========================================================
        [HttpPost]
        [Route("Dang-nhap")]
        public IActionResult Login(TTaiKhoan user)
        {
            // --- BƯỚC 1: TRUY VẤN TÀI KHOẢN ---
            var taiKhoan = _context.TTaiKhoan
                .Include(tk => tk.MaNvNavigation)
                .Include(tk => tk.MaVtNavigation)
                .FirstOrDefault(x => x.TenDangNhap == user.TenDangNhap);

            if (taiKhoan == null)
            {
                ViewBag.Error = "Tên đăng nhập không tồn tại.";
                return View();
            }

            // --- BƯỚC 2: KIỂM TRA VÀ HASH MẬT KHẨU ---
            if (!PasswordHelper.IsBase64String(taiKhoan.MatKhau))
            {
                taiKhoan.MatKhau = PasswordHelper.HashPassword(taiKhoan.TenDangNhap, taiKhoan.MatKhau);
                _context.Update(taiKhoan);
                _context.SaveChanges();
            }

            bool isValid = PasswordHelper.VerifyPassword(taiKhoan.TenDangNhap, user.MatKhau, taiKhoan.MatKhau);

            // --- BƯỚC 3: XỬ LÝ NẾU ĐĂNG NHẬP THÀNH CÔNG ---
            if (isValid)
            {
                // 1. Lấy Họ Tên
                string hoTen = taiKhoan.MaNvNavigation != null
                             ? taiKhoan.MaNvNavigation.HoDem + " " + taiKhoan.MaNvNavigation.Ten
                             : "Không rõ";

                // 2. Lấy Tên Vai Trò
                string tenVaiTro = taiKhoan.MaVtNavigation != null
                                 ? taiKhoan.MaVtNavigation.TenVt
                                 : taiKhoan.MaVt;

                // 3. LƯU TẤT CẢ THÔNG TIN CẦN THIẾT VÀO SESSION
                HttpContext.Session.SetString("UserName", taiKhoan.TenDangNhap.ToString());
                HttpContext.Session.SetString("UserRole", taiKhoan.MaVt.ToString());

                // === LƯU KEY MA TK VÀ THÔNG TIN CÁ NHÂN ===
                HttpContext.Session.SetString("MaTk", taiKhoan.MaTk.ToString());     // KEY QUAN TRỌNG ĐÃ ĐƯỢC CHUẨN HÓA
                HttpContext.Session.SetString("MaNv", taiKhoan.MaNv ?? "");
                HttpContext.Session.SetString("hoTen", hoTen);
                HttpContext.Session.SetString("tenVaiTro", tenVaiTro);
                // ==============================================

                // BỎ DÒNG LƯU MA_TK LẶP LẠI (HttpContext.Session.SetString("MaTk", taiKhoan.MaTk);)

                // Chuyển về trang chủ
                return RedirectToAction("Index", "Home");
            }

            // --- BƯỚC 4: XỬ LÝ NẾU SAI MẬT KHẨU ---
            ModelState.AddModelError("", "Sai mật khẩu!");
            return View(user);
        }

        // ==========================================================
        // KHỐI 3: ACTION LOGOUT
        // ==========================================================
        [Route("Dang-xuat")]
        public IActionResult Logout()
        {
            // Xóa tất cả Session
            HttpContext.Session.Clear();

            // Trở về trang đăng nhập
            return RedirectToAction("Login", "Account");
        }
    }
}