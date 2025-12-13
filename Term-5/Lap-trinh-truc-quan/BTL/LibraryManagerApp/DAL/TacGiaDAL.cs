using LibraryManagerApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DAL
{
    internal class TacGiaDAL
    {
        public List<TacGiaDTO> GetAllTacGiaDTO()
        {
            using (var db = new QLThuVienDataContext())
            {
                // JOIN tTacGia và tQuocGia
                var query = from tg in db.tTacGias
                            join qg in db.tQuocGias on tg.MaQG equals qg.MaQG
                            select new TacGiaDTO
                            {
                                MaTG = tg.MaTG,
                                MaQG = tg.MaQG,
                                HoDem = tg.HoDem,
                                Ten = tg.Ten,
                                TenQG = qg.TenQG
                            };
                return query.ToList();
            }
        }

        public TacGiaDTO GetTacGiaByMaTG(string maTG)
        {
            using (var db = new QLThuVienDataContext())
            {
                // Truy vấn JOIN để lấy đủ thông tin
                var result = from tg in db.tTacGias
                             join qg in db.tQuocGias on tg.MaQG equals qg.MaQG
                             where tg.MaTG == maTG
                             select new TacGiaDTO
                             {
                                 MaTG = tg.MaTG,
                                 MaQG = tg.MaQG,
                                 HoDem = tg.HoDem,
                                 Ten = tg.Ten,
                                 TenQG = qg.TenQG
                             };
                return result.SingleOrDefault();
            }
        }

        // Cần hàm lấy danh sách Quốc gia cho ComboBox
        public List<tQuocGia> GetAllQuocGia()
        {
            using (var db = new QLThuVienDataContext())
            {
                return db.tQuocGias.ToList();
            }
        }

        // 1. Hàm gọi Stored Procedure SP_GenerateNewMaTg
        public string GenerateNewMaTg(string maQG)
        {
            using (var db = new QLThuVienDataContext())
            {
                // Thường sử dụng ExecuteCommand hoặc method đã map từ SP
                string newMaTg = string.Empty;

                try
                {
                    // Giả định LINQ to SQL tạo method cho SP: SP_GenerateNewMaTg
                    // Tham số đầu ra @NewMaTg là NVARCHAR(50) OUTPUT
                    db.SP_GenerateNewMaTg(maQG, ref newMaTg);

                    return newMaTg;
                }
                catch (Exception ex)
                {
                    // Lỗi hệ thống/SP (ví dụ: vượt quá giới hạn 999)
                    // Trong trường hợp SP trả về lỗi RAISERROR, nó sẽ bị bắt ở đây
                    Console.WriteLine("Lỗi khi gọi SP sinh mã Tác giả: " + ex.Message);
                    throw new Exception("Lỗi hệ thống khi sinh mã Tác giả. Chi tiết: " + ex.Message);
                }
            }
        }

        // 2. Hàm Insert (CREATE)
        public bool InsertTacGia(TacGiaDTO model)
        {
            using (var db = new QLThuVienDataContext())
            {
                // Tác giả không có MaTG trong DTO khi gọi hàm này, nhưng sẽ có sau khi gọi SP
                tTacGia newTacGia = new tTacGia
                {
                    MaTG = model.MaTG, // Mã đã được sinh ra
                    MaQG = model.MaQG,
                    HoDem = model.HoDem,
                    Ten = model.Ten
                };

                db.tTacGias.InsertOnSubmit(newTacGia);

                try
                {
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    // Lỗi DB (trùng Mã TG, sai FK,...)
                    Console.WriteLine("Lỗi khi thêm Tác giả: " + ex.Message);
                    return false;
                }
            }
        }

        // Hàm Update (UPDATE)
        public bool UpdateTacGia(TacGiaDTO model)
        {
            using (var db = new QLThuVienDataContext())
            {
                tTacGia existingTg = db.tTacGias.SingleOrDefault(tg => tg.MaTG == model.MaTG);

                if (existingTg != null)
                {
                    // Cập nhật các thuộc tính có thể thay đổi
                    existingTg.MaQG = model.MaQG;
                    existingTg.HoDem = model.HoDem;
                    existingTg.Ten = model.Ten;

                    try
                    {
                        db.SubmitChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi khi cập nhật Tác giả: " + ex.Message);
                        return false;
                    }
                }
                return false;
            }
        }

        // Hàm Delete (DELETE)
        public bool DeleteTacGia(string maTG)
        {
            using (var db = new QLThuVienDataContext())
            {
                tTacGia tgToDelete = db.tTacGias.SingleOrDefault(tg => tg.MaTG == maTG);

                if (tgToDelete != null)
                {
                    db.tTacGias.DeleteOnSubmit(tgToDelete);

                    try
                    {
                        db.SubmitChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Lỗi ràng buộc (ví dụ: Tác giả đang có tài liệu)
                        Console.WriteLine("Lỗi khi xóa Tác giả: " + ex.Message);
                        return false;
                    }
                }
                return false;
            }
        }
        // Hàm Tìm kiếm Tác Giả (Logic tìm kiếm Dynamic)
        public List<TacGiaDTO> SearchTacGia(List<SearchFilter> filters)
        {
            using (var db = new QLThuVienDataContext())
            {
                // Bắt đầu với JOIN tTacGia và tQuocGia để có dữ liệu HoTen và TenQG
                var query = from tg in db.tTacGias
                            join qg in db.tQuocGias on tg.MaQG equals qg.MaQG
                            select new { TacGia = tg, QuocGia = qg, HoTen = tg.HoDem + " " + tg.Ten };

                // Áp dụng filters
                foreach (var filter in filters)
                {
                    string fieldName = filter.FieldName;
                    string op = filter.Operator;
                    string value = filter.Value;

                    if (fieldName == "MaTG")
                    {
                        if (op == "=") query = query.Where(x => x.TacGia.MaTG == value);
                        else if (op == "LIKE") query = query.Where(x => x.TacGia.MaTG.Contains(value));
                        else if (op == "Bắt đầu bằng") query = query.Where(x => x.TacGia.MaTG.StartsWith(value));
                    }
                    else if (fieldName == "HoTen")
                    {
                        string hoTenKeyword = value.Trim();
                        if (op == "LIKE")
                            query = query.Where(x => x.HoTen.Contains(hoTenKeyword));
                        else if (op == "Bắt đầu bằng")
                            query = query.Where(x => x.HoTen.StartsWith(hoTenKeyword));
                    }
                    else if (fieldName == "MaQG" && op == "=")
                    {
                        query = query.Where(x => x.TacGia.MaQG == value);
                    }
                    else if (fieldName == "TenQG" && op == "LIKE")
                    {
                        query = query.Where(x => x.QuocGia.TenQG.Contains(value));
                    }
                }

                // Map kết quả cuối cùng sang DTO
                return query.ToList().Select(x => new TacGiaDTO
                {
                    MaTG = x.TacGia.MaTG,
                    MaQG = x.TacGia.MaQG,
                    HoDem = x.TacGia.HoDem,
                    Ten = x.TacGia.Ten,
             
                    TenQG = x.QuocGia.TenQG
                }).ToList();
            }
        }
    }
}
