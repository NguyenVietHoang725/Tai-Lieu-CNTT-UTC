using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DTO
{
    public class TacGiaDTO
    {
        // Dữ liệu thô từ tTacGia
        public string MaTG { get; set; }
        public string MaQG { get; set; }
        public string HoDem { get; set; }
        public string Ten { get; set; }

        // Dữ liệu JOIN (Hiển thị)
        public string TenQG { get; set; }

        // Dữ liệu tính toán
        public string HoTen
        {
            get { return HoDem + " " + Ten; }
        }
    }
}
