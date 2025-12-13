// File: LibraryManagerApp.DAL/ThongKeDAL.cs

using System.Collections.Generic;
using System.Linq;

namespace LibraryManagerApp.DAL
{
    // DTO nội bộ (Raw data) cho Biểu đồ Đường (Line/Spline)
    public class MonthlyStatRaw
    {
        public int Thang { get; set; }
        public int SoLuong { get; set; }
    }

    // DTO nội bộ (Raw data) cho Biểu đồ Tròn (Pie)
    public class CategoryStatRaw
    {
        public string TenNhom { get; set; } // (Tên VT, Tên Trạng thái, Tên Thể loại)
        public int SoLuong { get; set; }
    }

    internal class ThongKeDAL
    {
        // 1. Thống kê Thẻ Bạn Đọc (theo tháng của 1 năm)
        public List<MonthlyStatRaw> GetTheBanDocStats(int nam)
        {
            using (var db = new QLThuVienDataContext())
            {
                var query = from tbd in db.tTheBanDocs
                            where tbd.NgayCap.Year == nam
                            group tbd by tbd.NgayCap.Month into g
                            select new MonthlyStatRaw
                            {
                                Thang = g.Key,
                                SoLuong = g.Count()
                            };
                return query.ToList();
            }
        }

        // 2. Thống kê Giao dịch (theo tháng của 1 năm)
        public List<MonthlyStatRaw> GetGiaoDichStats(int nam)
        {
            using (var db = new QLThuVienDataContext())
            {
                var query = from gd in db.tGiaoDichMuonTras
                            where gd.NgayMuon.Year == nam
                            group gd by gd.NgayMuon.Month into g
                            select new MonthlyStatRaw
                            {
                                Thang = g.Key,
                                SoLuong = g.Count()
                            };
                return query.ToList();
            }
        }

        // 3. Thống kê Tài khoản (theo Vai trò)
        public List<CategoryStatRaw> GetVaiTroStats()
        {
            using (var db = new QLThuVienDataContext())
            {
                var query = from tk in db.tTaiKhoans
                            join vt in db.tVaiTros on tk.MaVT equals vt.MaVT
                            group tk by vt.TenVT into g
                            select new CategoryStatRaw
                            {
                                TenNhom = g.Key,
                                SoLuong = g.Count()
                            };
                return query.ToList();
            }
        }

        // 4. Thống kê Bản sao (theo Trạng thái)
        public List<CategoryStatRaw> GetBanSaoTrangThaiStats()
        {
            using (var db = new QLThuVienDataContext())
            {
                var query = from bs in db.tBanSaos
                            group bs by bs.TrangThai into g
                            select new CategoryStatRaw
                            {
                                TenNhom = g.Key,
                                SoLuong = g.Count()
                            };
                return query.ToList();
            }
        }

        // 5. Thống kê Tài liệu (theo Thể loại)
        public List<CategoryStatRaw> GetTaiLieuTheLoaiStats()
        {
            using (var db = new QLThuVienDataContext())
            {
                var query = from tl in db.tTaiLieus
                            join thl in db.tTheLoais on tl.MaThL equals thl.MaThL
                            group tl by thl.TenThL into g
                            select new CategoryStatRaw
                            {
                                TenNhom = g.Key,
                                SoLuong = g.Count()
                            };
                return query.ToList();
            }
        }
    }
}