using LibraryManagerApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DAL
{
    internal class GiaoDichDAL
    {
        // 1. READ: Lấy tất cả Giao dịch (cho dgvDanhSachGiaoDich)
        public List<GiaoDichDTO> GetAllGiaoDich()
        {
            using (var db = new QLThuVienDataContext())
            {
                var query = from gd in db.tGiaoDichMuonTras
                            join tbd in db.tTheBanDocs on gd.MaTBD equals tbd.MaTBD
                            join bd in db.tBanDocs on tbd.MaBD equals bd.MaBD
                            join tk in db.tTaiKhoans on gd.MaTK equals tk.MaTK
                            join nv in db.tNhanViens on tk.MaNV equals nv.MaNV
                            orderby gd.NgayMuon descending
                            select new GiaoDichDTO
                            {
                                MaGD = gd.MaGD,
                                MaTBD = gd.MaTBD,
                                NgayMuon = gd.NgayMuon,
                                NgayHenTra = gd.NgayHenTra,
                                NgayTra = gd.NgayTra,
                                TrangThai = gd.TrangThai,
                                HoTenBD = bd.HoDem + " " + bd.Ten,
                                HoTenNV = nv.HoDem + " " + nv.Ten
                            };
                return query.ToList();
            }
        }

        // 2. READ: Lấy Chi tiết Giao dịch (cho dgvDuLieuBanSao)
        public List<GiaoDich_BanSaoDTO> GetChiTietGiaoDich(string maGD)
        {
            using (var db = new QLThuVienDataContext())
            {
                var query = from gdbs in db.tGiaoDich_BanSaos
                            join bs in db.tBanSaos on gdbs.MaBS equals bs.MaBS
                            join tl in db.tTaiLieus on bs.MaTL equals tl.MaTL
                            where gdbs.MaGD == maGD
                            select new GiaoDich_BanSaoDTO
                            {
                                MaGD = gdbs.MaGD,
                                MaBS = gdbs.MaBS,
                                TinhTrang = gdbs.TinhTrang,
                                TenTL = tl.TenTL
                            };
                return query.ToList();
            }
        }

        // 3. GENERATE: Gọi SP_GenerateNewMaGD
        public string GenerateNewMaGD()
        {
            using (var db = new QLThuVienDataContext())
            {
                string newMaGD = string.Empty;
                try
                {
                    db.SP_GenerateNewMaGD(ref newMaGD);
                    return newMaGD;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi SP khi sinh mã Giao dịch: " + ex.Message);
                }
            }
        }

        // 4. CREATE: Thêm Giao dịch MỚI (Bảng chính)
        public bool InsertGiaoDich(GiaoDichDTO model)
        {
            using (var db = new QLThuVienDataContext())
            {
                tGiaoDichMuonTra newGD = new tGiaoDichMuonTra
                {
                    MaGD = model.MaGD,
                    MaTBD = model.MaTBD,
                    MaTK = model.MaTK,
                    NgayMuon = model.NgayMuon,
                    NgayHenTra = model.NgayHenTra,
                    NgayTra = null,
                    TrangThai = "Đang mượn"
                };
                db.tGiaoDichMuonTras.InsertOnSubmit(newGD);
                try { db.SubmitChanges(); return true; }
                catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
            }
        }

        // 5. CREATE: Thêm Chi tiết Giao dịch (Bảng phụ)
        public bool InsertGiaoDich_BanSao(GiaoDich_BanSaoDTO model)
        {
            using (var db = new QLThuVienDataContext())
            {
                tGiaoDich_BanSao newDetail = new tGiaoDich_BanSao
                {
                    MaGD = model.MaGD,
                    MaBS = model.MaBS,
                    TinhTrang = false // Đang mượn
                };
                db.tGiaoDich_BanSaos.InsertOnSubmit(newDetail);
                try { db.SubmitChanges(); return true; }
                catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
            }
        }

        // 6. UPDATE (Bảng chính): Cập nhật Giao dịch (Khi trả sách)
        public bool UpdateGiaoDich(string maGD, DateTime ngayTra, string trangThaiMoi)
        {
            using (var db = new QLThuVienDataContext())
            {
                tGiaoDichMuonTra gd = db.tGiaoDichMuonTras.SingleOrDefault(g => g.MaGD == maGD);
                if (gd != null)
                {
                    gd.NgayTra = ngayTra;
                    gd.TrangThai = trangThaiMoi;
                    try { db.SubmitChanges(); return true; }
                    catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
                }
                return false;
            }
        }

        // 7. UPDATE (Bảng phụ): Cập nhật Chi tiết Bản sao (Set TinhTrang = 1)
        public bool UpdateChiTietGiaoDich(string maGD, string maBS)
        {
            using (var db = new QLThuVienDataContext())
            {
                tGiaoDich_BanSao detail = db.tGiaoDich_BanSaos.SingleOrDefault(d => d.MaGD == maGD && d.MaBS == maBS);
                if (detail != null)
                {
                    detail.TinhTrang = true; // 1 = Đã trả
                    try { db.SubmitChanges(); return true; }
                    catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
                }
                return false;
            }
        }

        // 8. DELETE: Xóa Giao dịch
        public bool DeleteGiaoDich(string maGD)
        {
            using (var db = new QLThuVienDataContext())
            {
                tGiaoDichMuonTra gdToDelete = db.tGiaoDichMuonTras.SingleOrDefault(g => g.MaGD == maGD);

                if (gdToDelete != null)
                {
                    db.tGiaoDichMuonTras.DeleteOnSubmit(gdToDelete);
                    // tGiaoDich_BanSao sẽ bị xóa tự động (ON DELETE CASCADE)
                    try
                    {
                        db.SubmitChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Lỗi (hiếm khi xảy ra nếu CASCADE hoạt động)
                        Console.WriteLine("Lỗi khi xóa Giao dịch: " + ex.Message);
                        return false;
                    }
                }
                return false; // Không tìm thấy
            }
        }

        // Lấy số phiếu đang mượn (chưa trả)
        public int GetCountDangMuon()
        {
            using (var db = new QLThuVienDataContext())
            {
                return db.tGiaoDichMuonTras
                         .Count(gd => gd.TrangThai == "Đang mượn" || gd.NgayTra == null);
            }
        }

        // Lấy số phiếu quá hạn (chưa trả và đã quá ngày hẹn trả)
        public int GetCountQuaHan()
        {
            using (var db = new QLThuVienDataContext())
            {
                DateTime today = DateTime.Now.Date;
                return db.tGiaoDichMuonTras
                         .Count(gd => (gd.TrangThai == "Đang mượn" || gd.NgayTra == null)
                                   && gd.NgayHenTra < today);
            }
        }

        // Lấy danh sách hoạt động gần đây (10 giao dịch mới nhất)
        public List<GiaoDichDTO> GetRecentActivities(int topCount = 10)
        {
            using (var db = new QLThuVienDataContext())
            {
                var query = from gd in db.tGiaoDichMuonTras
                            join tbd in db.tTheBanDocs on gd.MaTBD equals tbd.MaTBD
                            join bd in db.tBanDocs on tbd.MaBD equals bd.MaBD
                            orderby gd.NgayMuon descending
                            select new GiaoDichDTO
                            {
                                MaGD = gd.MaGD,
                                NgayMuon = gd.NgayMuon,
                                NgayTra = gd.NgayTra,
                                TrangThai = gd.TrangThai,
                                HoTenBD = bd.HoDem + " " + bd.Ten
                            };

                return query.Take(topCount).ToList();
            }
        }

        // Lấy chi tiết bản sao của một giao dịch (dùng để hiển thị tên sách)
        public List<GiaoDich_BanSaoDTO> GetBanSaoByMaGD(string maGD)
        {
            using (var db = new QLThuVienDataContext())
            {
                var query = from gdbs in db.tGiaoDich_BanSaos
                            join bs in db.tBanSaos on gdbs.MaBS equals bs.MaBS
                            join tl in db.tTaiLieus on bs.MaTL equals tl.MaTL
                            where gdbs.MaGD == maGD
                            select new GiaoDich_BanSaoDTO
                            {
                                MaGD = gdbs.MaGD,
                                MaBS = gdbs.MaBS,
                                TenTL = tl.TenTL,
                                TinhTrang = gdbs.TinhTrang
                            };

                return query.ToList();
            }
        }
    }
}
