using LibraryManagerApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DAL
{
    internal class BanSaoDAL
    {
        // 1. READ (List): Lấy tất cả bản sao của một Tài liệu
        public List<BanSaoDTO> GetBanSaoByMaTL(string maTL)
        {
            using (var db = new QLThuVienDataContext())
            {
                var query = from bs in db.tBanSaos
                            where bs.MaTL == maTL
                            select new BanSaoDTO
                            {
                                MaBS = bs.MaBS,
                                MaTL = bs.MaTL,
                                TrangThai = bs.TrangThai
                            };
                return query.ToList();
            }
        }

        // 2. READ (Detail): Lấy chi tiết một bản sao
        public BanSaoDTO GetBanSaoByMaBS(string maBS)
        {
            using (var db = new QLThuVienDataContext())
            {
                var bs = db.tBanSaos.SingleOrDefault(b => b.MaBS == maBS);
                if (bs != null)
                {
                    return new BanSaoDTO { MaBS = bs.MaBS, MaTL = bs.MaTL, TrangThai = bs.TrangThai };
                }
                return null;
            }
        }

        // 3. Generate (Gọi SP_GenerateNewMaBS)
        public string GenerateNewMaBS(string maTL)
        {
            using (var db = new QLThuVienDataContext())
            {
                string newMaBS = string.Empty;
                try
                {
                    // Giả định LINQ to SQL đã ánh xạ SP
                    db.SP_GenerateNewMaBS(maTL, ref newMaBS);
                    return newMaBS;
                }
                catch (Exception ex)
                {
                    // Bắt lỗi RAISERROR từ SP (ví dụ: Vượt quá giới hạn 999)
                    throw new Exception("Lỗi SP khi sinh mã Bản sao: " + ex.Message);
                }
            }
        }

        // 4. CREATE (Insert)
        public bool InsertBanSao(BanSaoDTO model)
        {
            using (var db = new QLThuVienDataContext())
            {
                tBanSao newBS = new tBanSao
                {
                    MaBS = model.MaBS, // Mã đã được sinh
                    MaTL = model.MaTL,
                    TrangThai = model.TrangThai
                };
                db.tBanSaos.InsertOnSubmit(newBS);
                try { db.SubmitChanges(); return true; }
                catch (Exception ex) { return false; }
            }
        }

        // 5. UPDATE
        public bool UpdateBanSao(BanSaoDTO model)
        {
            using (var db = new QLThuVienDataContext())
            {
                tBanSao existingBS = db.tBanSaos.SingleOrDefault(b => b.MaBS == model.MaBS);
                if (existingBS != null)
                {
                    existingBS.TrangThai = model.TrangThai;
                    try { db.SubmitChanges(); return true; }
                    catch (Exception ex) { return false; }
                }
                return false;
            }
        }

        // 6. DELETE
        public bool DeleteBanSao(string maBS)
        {
            using (var db = new QLThuVienDataContext())
            {
                tBanSao bsToDelete = db.tBanSaos.SingleOrDefault(b => b.MaBS == maBS);
                if (bsToDelete != null)
                {
                    db.tBanSaos.DeleteOnSubmit(bsToDelete);
                    try { db.SubmitChanges(); return true; }
                    catch (Exception ex) { return false; } // Lỗi do ràng buộc (đang được mượn)
                }
                return false;
            }
        }

        // Hàm Kiểm tra Bản sao (Cho chức năng Mượn)
        public BanSaoDTO GetBanSaoForMuon(string maBS)
        {
            using (var db = new QLThuVienDataContext())
            {
                var query = from bs in db.tBanSaos
                            join tl in db.tTaiLieus on bs.MaTL equals tl.MaTL
                            where bs.MaBS == maBS
                            select new
                            {
                                BanSao = bs,
                                TenTL = tl.TenTL
                            };

                var result = query.SingleOrDefault();
                if (result != null)
                {
                    return new BanSaoDTO
                    {
                        MaBS = result.BanSao.MaBS,
                        MaTL = result.BanSao.MaTL,
                        TrangThai = result.BanSao.TrangThai,
                        // Dùng GiaoDich_BanSaoDTO để chứa TenTL
                        // Tạm thời gán vào MaTL để BLL sử dụng
                        // MaTL = result.TenTL 
                        // Tốt hơn là tạo 1 DTO mới, nhưng để đơn giản:
                        // Chúng ta sẽ tạo DTO mới
                    };
                }
                return null;
            }
        }

        // Hàm Cập nhật Trạng thái (Sau khi Mượn/Trả)
        public bool UpdateTrangThaiBanSao(string maBS, string trangThaiMoi)
        {
            using (var db = new QLThuVienDataContext())
            {
                tBanSao bs = db.tBanSaos.SingleOrDefault(b => b.MaBS == maBS);
                if (bs != null)
                {
                    bs.TrangThai = trangThaiMoi;
                    try { db.SubmitChanges(); return true; }
                    catch { return false; }
                }
                return false;
            }
        }
        public List<BanSaoDTO> SearchBanSao(string maTL, List<SearchFilter> filters)
        {
            using (var db = new QLThuVienDataContext())
            {
                // 1. Lọc BanSao theo MaTL cố định trước
                var query = from bs in db.tBanSaos
                            where bs.MaTL == maTL // BƯỚC QUAN TRỌNG: Lọc theo MaTL
                            join tl in db.tTaiLieus on bs.MaTL equals tl.MaTL
                            select new { BanSao = bs, TaiLieu = tl };

                // 2. Áp dụng filters MaBS và TrangThai từ người dùng
                foreach (var filter in filters)
                {
                    string fieldName = filter.FieldName;
                    string op = filter.Operator;
                    string value = filter.Value;

                    if (fieldName == "MaBS")
                    {
                        if (op == "=") query = query.Where(x => x.BanSao.MaBS == value);
                        else if (op == "LIKE") query = query.Where(x => x.BanSao.MaBS.Contains(value));
                        else if (op == "Bắt đầu bằng") query = query.Where(x => x.BanSao.MaBS.StartsWith(value));
                    }
                    else if (fieldName == "TrangThai" && op == "=")
                    {
                        query = query.Where(x => x.BanSao.TrangThai == value);
                    }
                }

                // 3. Map kết quả cuối cùng sang DTO
                return query.ToList().Select(x => new BanSaoDTO
                {
                    MaBS = x.BanSao.MaBS,
                    MaTL = x.BanSao.MaTL,
                    TrangThai = x.BanSao.TrangThai           
                }).ToList();
            }
        }

        // Hàm hỗ trợ Autocomplete: Lấy danh sách Mã Bản Sao đang "Có sẵn"
        public List<string> GetAvailableMaBS()
        {
            using (var db = new QLThuVienDataContext())
            {
                return db.tBanSaos
                         .Where(b => b.TrangThai == "Có sẵn")
                         .Select(b => b.MaBS)
                         .ToList();
            }
        }
    }
}
