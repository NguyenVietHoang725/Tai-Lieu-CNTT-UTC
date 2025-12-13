using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DTO
{
    internal class BanDocDTO
    {
        // 1. Dữ liệu thô (dùng cho CRUD và hiển thị lên Input)
        public string MaBD { get; set; }
        public string HoDem { get; set; } // Giữ lại
        public string Ten { get; set; }   // Giữ lại
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; } // 'M' hoặc 'F' (Giữ lại)
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }

        // 2. Dữ liệu tính toán/hiển thị (dùng cho DataGridView)
        // Dùng thuộc tính chỉ đọc (ReadOnly Property) để tự động tính toán từ dữ liệu thô
        public string HoTen
        {
            get { return HoDem + " " + Ten; }
        }

        public string GioiTinhHienThi
        {
            get { return GioiTinh.Equals("M") ? "Nam" : "Nữ"; }
        }
    }
}
