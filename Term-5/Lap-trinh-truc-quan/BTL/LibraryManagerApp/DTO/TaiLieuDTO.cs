using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DTO
{
    public class TaiLieuDTO
    {
        // 1. Dữ liệu thô từ tTaiLieu (Dùng cho CRUD)
        public string MaTL { get; set; }
        public string MaNXB { get; set; }
        public string MaNN { get; set; }
        public string MaThL { get; set; }
        public string MaDD { get; set; }
        public string MaTK { get; set; } // Tài khoản nhập
        public string TenTL { get; set; }
        public int? LanXuatBan { get; set; }
        public int? NamXuatBan { get; set; }
        public int? SoTrang { get; set; }
        public string KhoCo { get; set; }
        public string Anh { get; set; } // Đường dẫn ảnh

        // 2. Dữ liệu JOIN (Hiển thị)
        public string TenNXB { get; set; }
        public string TenNN { get; set; }
        public string TenThL { get; set; }
        public string TenDD { get; set; }

        // 3. Danh sách Tác giả đính kèm (Quan hệ 1-nhiều)
        public List<TL_TGDTO> DanhSachTacGia { get; set; } = new List<TL_TGDTO>();

        public string TacGiaExcel { get; set; }
    }
}
