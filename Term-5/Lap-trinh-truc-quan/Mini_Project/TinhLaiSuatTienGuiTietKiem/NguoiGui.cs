using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinhLaiSuatTienGuiTietKiem
{
    internal class NguoiGui
    {
        private int maKH;
        private string tenKH;
        private string diaChi;
        private int soTienGui;
        private string ngayGui;
        private string thoiGianGui;
        private double tien;
        
        public NguoiGui(int maKH)
        {
            this.maKH = maKH;
        }

        public NguoiGui(int maKH, string tenKH, string diaChi, int soTienGui, string ngayGui, string thoiGianGui, double tien)
        {
            this.maKH = maKH;
            this.tenKH = tenKH;
            this.diaChi = diaChi;
            this.soTienGui = soTienGui;
            this.ngayGui = ngayGui;
            this.thoiGianGui = thoiGianGui;
            this.tien = tien;
        }

        public int MaKH1 { get => maKH; set => maKH = value; }
        public string TenKH1 { get => tenKH; set => tenKH = value; }
        public double Tien1 { get => tien; set => tien = value; }
    }
}
