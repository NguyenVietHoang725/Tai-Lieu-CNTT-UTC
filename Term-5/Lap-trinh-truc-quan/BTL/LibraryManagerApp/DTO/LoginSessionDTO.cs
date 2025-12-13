using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DTO
{
    public class LoginSessionDTO
    {
        // 1. Thông tin Tài khoản (tTaiKhoan)
        public string MaTK { get; set; }
        public string TenDangNhap { get; set; } // Dùng cho hiển thị
        public string TrangThaiTK { get; set; } // Cần kiểm tra "Hoạt động"

        // 2. Thông tin Nhân viên (tNhanVien)
        public string MaNV { get; set; }
        public string HoTenNV { get; set; }     // Dùng cho hiển thị trên Form chính

        // 3. Thông tin Phân quyền (tVaiTro)
        public string MaVT { get; set; }
        public string TenVT { get; set; }       // Dùng cho logic phân quyền sau này
    }
}
