using System;

namespace LibraryManagerApp.DTO
{
    public class NhanVienDTO
    {
        // 1. Dữ liệu thô (dùng cho CRUD và hiển thị lên Input)
        public string MaNV { get; set; }        // Ví dụ: NV25-01
        public string HoDem { get; set; }     
        public string Ten { get; set; }        
        public DateTime NgaySinh { get; set; }  
        public string GioiTinh { get; set; }    //  'M' hoặc 'F'
        public string DiaChi { get; set; }   
        public string SDT { get; set; }      
        public string Email { get; set; }       
        public string PhuTrach { get; set; }    

        // 2. Dữ liệu tính toán/hiển thị (dùng cho DataGridView)
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