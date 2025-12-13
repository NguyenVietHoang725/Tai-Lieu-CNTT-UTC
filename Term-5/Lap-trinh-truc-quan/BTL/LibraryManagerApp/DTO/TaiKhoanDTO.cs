using System;

namespace LibraryManagerApp.DTO
{
    public class TaiKhoanDTO
    {
        // 1. Dữ liệu thô từ tTaiKhoan
        public string MaTK { get; set; }
        public string MaNV { get; set; }
        public string MaVT { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayTao { get; set; }

        // 2. Dữ liệu JOIN (Hiển thị)
        public string HoTenNV { get; set; }     // Họ tên nhân viên (JOIN tNhanVien)
        public string TenVaiTro { get; set; }   // Tên vai trò (JOIN tVaiTro)

        // 3. Dữ liệu bổ sung (Chỉ dùng trong GUI/Validate)
        // Không thêm trường 'NhacLaiMK' vào DTO, nó chỉ nên được xử lý trong ValidateInputs của GUI.

        // 4. Dữ liệu tính toán (Hiển thị trên DGV)
        public string NgayTaoHienThi
        {
            get { return NgayTao.ToShortDateString(); }
        }
    }
}