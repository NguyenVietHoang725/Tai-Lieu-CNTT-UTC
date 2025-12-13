using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DTO
{
    public class TheBanDocDTO
    {
        // 1. Dữ liệu thô từ tTheBanDoc
        public string MaTBD { get; set; }
        public string MaBD { get; set; }
        public string MaTK { get; set; }
        public DateTime NgayCap { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public string TrangThai { get; set; }

        // 2. Dữ liệu JOIN (Hiển thị)
        public string HoTenBD { get; set; }
        public string HoTenNV { get; set; }

        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; } // Hiển thị Nam/Nữ
        public string DiaChi { get; set; }
        public string SDT { get; set; }

        // 3. Dữ liệu tính toán (Hiển thị trên DGV)
        public string NgayHetHanHienThi
        {
            // Định dạng hiển thị ngày hết hạn
            get { return NgayHetHan.HasValue ? NgayHetHan.Value.ToShortDateString() : "N/A"; }
        }
    }
}
