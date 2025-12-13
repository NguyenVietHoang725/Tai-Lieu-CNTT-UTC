using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DTO
{
    public class TL_TGDTO
    {
        // Dữ liệu thô từ tTaiLieu_TacGia
        public string MaTL { get; set; }
        public string MaTG { get; set; }
        public string VaiTro { get; set; }

        // Dữ liệu JOIN (Hiển thị)
        public string HoTenTG { get; set; } // Họ tên đầy đủ của Tác giả
    }
}
