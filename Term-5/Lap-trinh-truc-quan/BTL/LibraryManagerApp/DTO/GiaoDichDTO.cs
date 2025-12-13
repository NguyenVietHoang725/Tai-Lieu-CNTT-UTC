using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DTO
{
    public class GiaoDichDTO
    {
        // 1. Dữ liệu thô (tGiaoDichMuonTra)
        public string MaGD { get; set; }
        public string MaTBD { get; set; }
        public string MaTK { get; set; }
        public DateTime NgayMuon { get; set; }
        public DateTime NgayHenTra { get; set; }
        public DateTime? NgayTra { get; set; }
        public string TrangThai { get; set; }

        // 2. Dữ liệu JOIN (Hiển thị)
        public string HoTenBD { get; set; } // tTheBanDoc -> tBanDoc
        public string HoTenNV { get; set; } // tTaiKhoan -> tNhanVien
    }
}
