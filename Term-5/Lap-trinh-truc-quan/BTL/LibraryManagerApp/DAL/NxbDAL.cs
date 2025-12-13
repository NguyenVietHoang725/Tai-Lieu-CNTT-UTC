using LibraryManagerApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DAL
{
    internal class NxbDAL
    {
        public List<NxbDTO> GetAllNxbDTO()
        {
            using (var db = new QLThuVienDataContext())
            {
                var query = from nxb in db.tNhaXuatBans
                            join qg in db.tQuocGias on nxb.MaQG equals qg.MaQG
                            select new NxbDTO
                            {
                                MaNXB = nxb.MaNXB,
                                MaQG = nxb.MaQG,
                                TenNXB = nxb.TenNXB,
                                TenQG = qg.TenQG
                            };
                return query.ToList();
            }
        }

        // Hàm Read Detail
        public NxbDTO GetNxbByMaNXB(string maNXB)
        {
            using (var db = new QLThuVienDataContext())
            {
                var result = from nxb in db.tNhaXuatBans
                             join qg in db.tQuocGias on nxb.MaQG equals qg.MaQG
                             where nxb.MaNXB == maNXB
                             select new NxbDTO
                             {
                                 MaNXB = nxb.MaNXB,
                                 MaQG = nxb.MaQG,
                                 TenNXB = nxb.TenNXB,
                                 TenQG = qg.TenQG
                             };
                return result.SingleOrDefault();
            }
        }

        // Hàm gọi SP GenerateNewMaNxb
        public string GenerateNewMaNxb(string maQG)
        {
            // Tương tự GenerateNewMaTg, cần xử lý ref string/int?
            string newMaNxb = string.Empty;
            using (var db = new QLThuVienDataContext())
            {
                try { db.SP_GenerateNewMaNxb(maQG, ref newMaNxb); return newMaNxb; }
                catch (Exception ex) { throw new Exception("Lỗi khi sinh Mã NXB. Chi tiết: " + ex.Message); }
            }
        }

        // Hàm Insert
        public bool InsertNxb(NxbDTO model)
        {
            using (var db = new QLThuVienDataContext())
            {
                tNhaXuatBan newNxb = new tNhaXuatBan { MaNXB = model.MaNXB, MaQG = model.MaQG, TenNXB = model.TenNXB };
                db.tNhaXuatBans.InsertOnSubmit(newNxb);
                try { db.SubmitChanges(); return true; }
                catch (Exception ex) { return false; }
            }
        }

        // Hàm Update
        public bool UpdateNxb(NxbDTO model)
        {
            using (var db = new QLThuVienDataContext())
            {
                tNhaXuatBan existingNxb = db.tNhaXuatBans.SingleOrDefault(n => n.MaNXB == model.MaNXB);
                if (existingNxb != null)
                {
                    existingNxb.MaQG = model.MaQG;
                    existingNxb.TenNXB = model.TenNXB;
                    try { db.SubmitChanges(); return true; }
                    catch (Exception ex) { return false; }
                }
                return false;
            }
        }

        // Hàm Delete
        public bool DeleteNxb(string maNXB)
        {
            using (var db = new QLThuVienDataContext())
            {
                tNhaXuatBan nxbToDelete = db.tNhaXuatBans.SingleOrDefault(n => n.MaNXB == maNXB);
                if (nxbToDelete != null)
                {
                    db.tNhaXuatBans.DeleteOnSubmit(nxbToDelete);
                    try { db.SubmitChanges(); return true; }
                    catch (Exception ex) { return false; } // Lỗi do ràng buộc khóa ngoại
                }
                return false;
            }
        }
        // Hàm Tìm kiếm Nhà Xuất Bản (Logic tìm kiếm Dynamic)
        public List<NxbDTO> SearchNxb(List<SearchFilter> filters)
        {
            using (var db = new QLThuVienDataContext())
            {
                // Bắt đầu với JOIN tNhaXuatBan và tQuocGia
                var query = from nxb in db.tNhaXuatBans
                            join qg in db.tQuocGias on nxb.MaQG equals qg.MaQG
                            select new { Nxb = nxb, QuocGia = qg };

                // Áp dụng filters
                foreach (var filter in filters)
                {
                    string fieldName = filter.FieldName;
                    string op = filter.Operator;
                    string value = filter.Value;

                    if (fieldName == "MaNXB")
                    {
                        if (op == "=") query = query.Where(x => x.Nxb.MaNXB == value);
                        else if (op == "LIKE") query = query.Where(x => x.Nxb.MaNXB.Contains(value));
                        else if (op == "Bắt đầu bằng") query = query.Where(x => x.Nxb.MaNXB.StartsWith(value));
                    }
                    else if (fieldName == "TenNXB")
                    {
                        if (op == "LIKE") query = query.Where(x => x.Nxb.TenNXB.Contains(value));
                        else if (op == "Bắt đầu bằng") query = query.Where(x => x.Nxb.TenNXB.StartsWith(value));
                    }
                    else if (fieldName == "MaQG" && op == "=")
                    {
                        query = query.Where(x => x.Nxb.MaQG == value);
                    }
                    else if (fieldName == "TenQG" && op == "LIKE")
                    {
                        query = query.Where(x => x.QuocGia.TenQG.Contains(value));
                    }
                }

                // Map kết quả cuối cùng sang DTO
                return query.ToList().Select(x => new NxbDTO
                {
                    MaNXB = x.Nxb.MaNXB,
                    MaQG = x.Nxb.MaQG,
                    TenNXB = x.Nxb.TenNXB,
                    TenQG = x.QuocGia.TenQG
                }).ToList();
            }
        }
    }
}
