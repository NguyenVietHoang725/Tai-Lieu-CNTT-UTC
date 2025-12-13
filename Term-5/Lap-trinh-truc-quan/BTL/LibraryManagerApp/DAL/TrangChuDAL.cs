using LibraryManagerApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagerApp.DAL
{
    internal class TrangChuDAL
    {
        #region DTO Classes

        /// <summary>
        /// DTO cho hoạt động gần đây
        /// </summary>
        public class ActivityDTO
        {
            public string MaGD { get; set; }
            public string HoTenBD { get; set; }
            public string TenTL { get; set; }
            public string LoaiGD { get; set; }
            public DateTime NgayGD { get; set; }
        }

        /// <summary>
        /// DTO cho Top sách được mượn
        /// </summary>
        public class TopBookDTO
        {
            public int STT { get; set; }
            public string MaTL { get; set; }
            public string TenTL { get; set; }
            public int SoLuotMuon { get; set; }
        }

        #endregion

        #region Constants

        private const string STATUS_DANG_MUON = "Đang mượn";
        private const string LOAI_GD_TRA = "Trả";
        private const string LOAI_GD_MUON = "Mượn";
        private const string DEFAULT_BOOK_NAME = "N/A";

        #endregion

        #region Thống kê tổng quan

        /// <summary>
        /// Lấy thống kê tổng quan của thư viện
        /// </summary>
        /// <returns>Dictionary chứa các thống kê</returns>
        public Dictionary<string, int> GetStatistics()
        {
            using (var db = new QLThuVienDataContext())
            {
                DateTime today = DateTime.Now.Date;

                return new Dictionary<string, int>
                {
                    ["TongTaiLieu"] = db.tTaiLieus.Count(),
                    ["TongBanDoc"] = db.tBanDocs.Count(),
                    ["DangMuon"] = db.tGiaoDichMuonTras.Count(gd =>
                        gd.TrangThai == STATUS_DANG_MUON || gd.NgayTra == null),
                    ["QuaHan"] = db.tGiaoDichMuonTras.Count(gd =>
                        (gd.TrangThai == STATUS_DANG_MUON || gd.NgayTra == null) &&
                        gd.NgayHenTra < today)
                };
            }
        }

        #endregion

        #region Hoạt động gần đây

        /// <summary>
        /// Lấy danh sách hoạt động gần đây
        /// </summary>
        /// <param name="topCount">Số lượng giao dịch cần lấy</param>
        /// <returns>Danh sách hoạt động</returns>
        public List<ActivityDTO> GetRecentActivities(int topCount = 10)
        {
            using (var db = new QLThuVienDataContext())
            {
                // Lấy danh sách giao dịch với thông tin bạn đọc
                var activities = (from gd in db.tGiaoDichMuonTras
                                  join tbd in db.tTheBanDocs on gd.MaTBD equals tbd.MaTBD
                                  join bd in db.tBanDocs on tbd.MaBD equals bd.MaBD
                                  orderby gd.NgayMuon descending
                                  select new
                                  {
                                      gd.MaGD,
                                      HoTenBD = bd.HoDem + " " + bd.Ten,
                                      gd.NgayMuon,
                                      gd.NgayTra,
                                      gd.TrangThai
                                  }).Take(topCount).ToList();

                // Không có dữ liệu
                if (!activities.Any())
                    return new List<ActivityDTO>();

                // Lấy thông tin sách cho từng giao dịch
                var result = new List<ActivityDTO>(activities.Count);

                foreach (var act in activities)
                {
                    // Lấy tên sách đầu tiên trong giao dịch
                    string bookName = GetFirstBookName(db, act.MaGD);

                    result.Add(new ActivityDTO
                    {
                        MaGD = act.MaGD,
                        HoTenBD = act.HoTenBD,
                        TenTL = bookName,
                        LoaiGD = act.NgayTra.HasValue ? LOAI_GD_TRA : LOAI_GD_MUON,
                        NgayGD = act.NgayTra ?? act.NgayMuon
                    });
                }

                return result;
            }
        }

        /// <summary>
        /// Lấy tên sách đầu tiên trong giao dịch
        /// </summary>
        private string GetFirstBookName(QLThuVienDataContext db, string maGD)
        {
            var bookName = (from gdbs in db.tGiaoDich_BanSaos
                            join bs in db.tBanSaos on gdbs.MaBS equals bs.MaBS
                            join tl in db.tTaiLieus on bs.MaTL equals tl.MaTL
                            where gdbs.MaGD == maGD
                            select tl.TenTL).FirstOrDefault();

            return bookName ?? DEFAULT_BOOK_NAME;
        }

        #endregion

        #region Top sách được mượn

        /// <summary>
        /// Lấy danh sách Top sách được mượn nhiều nhất
        /// </summary>
        /// <param name="topCount">Số lượng sách cần lấy</param>
        /// <returns>Danh sách top sách</returns>
        public List<TopBookDTO> GetTopBorrowedBooks(int topCount = 10)
        {
            using (var db = new QLThuVienDataContext())
            {
                // Query lấy top sách
                var topBooks = (from gdbs in db.tGiaoDich_BanSaos
                                join bs in db.tBanSaos on gdbs.MaBS equals bs.MaBS
                                join tl in db.tTaiLieus on bs.MaTL equals tl.MaTL
                                group tl by new { tl.MaTL, tl.TenTL } into g
                                orderby g.Count() descending
                                select new TopBookDTO
                                {
                                    MaTL = g.Key.MaTL,
                                    TenTL = g.Key.TenTL,
                                    SoLuotMuon = g.Count()
                                }).Take(topCount).ToList();

                // Thêm số thứ tự
                for (int i = 0; i < topBooks.Count; i++)
                {
                    topBooks[i].STT = i + 1;
                }

                return topBooks;
            }
        }

        #endregion

        #region Helper Methods - Các phương thức bổ sung

        /// <summary>
        /// Kiểm tra có phiếu quá hạn hay không
        /// </summary>
        public bool HasOverdueBooks()
        {
            using (var db = new QLThuVienDataContext())
            {
                DateTime today = DateTime.Now.Date;
                return db.tGiaoDichMuonTras.Any(gd =>
                    (gd.TrangThai == STATUS_DANG_MUON || gd.NgayTra == null) &&
                    gd.NgayHenTra < today);
            }
        }

        /// <summary>
        /// Lấy số lượng phiếu mượn hôm nay
        /// </summary>
        public int GetTodayBorrowCount()
        {
            using (var db = new QLThuVienDataContext())
            {
                DateTime today = DateTime.Now.Date;
                return db.tGiaoDichMuonTras.Count(gd =>
                    gd.NgayMuon.Date == today);
            }
        }

        /// <summary>
        /// Lấy số lượng phiếu trả hôm nay
        /// </summary>
        public int GetTodayReturnCount()
        {
            using (var db = new QLThuVienDataContext())
            {
                DateTime today = DateTime.Now.Date;
                return db.tGiaoDichMuonTras.Count(gd =>
                    gd.NgayTra.HasValue && gd.NgayTra.Value.Date == today);
            }
        }

        /// <summary>
        /// Lấy tổng số bản sao đang được mượn
        /// </summary>
        public int GetTotalBorrowedCopies()
        {
            using (var db = new QLThuVienDataContext())
            {
                return (from gd in db.tGiaoDichMuonTras
                        where gd.TrangThai == STATUS_DANG_MUON || gd.NgayTra == null
                        join gdbs in db.tGiaoDich_BanSaos on gd.MaGD equals gdbs.MaGD
                        where gdbs.TinhTrang == false // Chưa trả
                        select gdbs.MaBS).Count();
            }
        }

        #endregion
    }
}