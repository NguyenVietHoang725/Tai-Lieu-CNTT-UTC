using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DTO
{
    public class GiaoDich_BanSaoDTO
    {
        // 1. Dữ liệu thô (tGiaoDich_BanSao)
        public string MaGD { get; set; }
        public string MaBS { get; set; }
        public bool TinhTrang { get; set; } // 0 = Đang mượn, 1 = Đã trả

        // 2. Dữ liệu JOIN (Hiển thị)
        public string TenTL { get; set; } // tBanSao -> tTaiLieu
    }
}
