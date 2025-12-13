using LibraryManagerApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DAL
{
    internal class BanDocDAL
    {
        public List<BanDocDTO> GetAllBanDocDTO() 
        {
            using (var db = new QLThuVienDataContext())
            {
                var query = from bd in db.tBanDocs
                            select new BanDocDTO
                            {
                                MaBD = bd.MaBD,
                                HoDem = bd.HoDem, 
                                Ten = bd.Ten,     
                                GioiTinh = bd.GioiTinh.ToString(), 
                                NgaySinh = bd.NgaySinh,
                                DiaChi = bd.DiaChi,
                                SDT = bd.SDT,
                                Email = bd.Email
                            };

                return query.ToList();
            }
        }

        public BanDocDTO GetBanDocByMaBD(string maBD) 
        {
            using (var db = new QLThuVienDataContext())
            {
                var bd = db.tBanDocs.SingleOrDefault(p => p.MaBD == maBD);
                if (bd != null)
                {
                    return new BanDocDTO
                    {
                        MaBD = bd.MaBD,
                        HoDem = bd.HoDem,
                        Ten = bd.Ten,
                        GioiTinh = bd.GioiTinh.ToString(),
                        NgaySinh = bd.NgaySinh,
                        DiaChi = bd.DiaChi,
                        SDT = bd.SDT,
                        Email = bd.Email
                    };
                }
                return null;
            }
        }

        public List<BanDocChuaCoTheDTO> GetBanDocChuaCoThe()
        {
            using (var db = new QLThuVienDataContext())
            {
                var query = from bd in db.tBanDocs
                            where !db.tTheBanDocs.Any(tbd => tbd.MaBD == bd.MaBD) // Lọc những bạn đọc chưa có thẻ
                            select new BanDocChuaCoTheDTO
                            {
                                MaBD = bd.MaBD,
                                HoTen = bd.HoDem + " " + bd.Ten
                            };

                return query.ToList();
            }
        }

        public bool InsertBanDoc(BanDocDTO model)
        {
            using (var db = new QLThuVienDataContext())
            {
                // 1. Tạo một đối tượng tBanDoc từ BanDocDTO
                tBanDoc newBanDoc = new tBanDoc
                {
                    MaBD = model.MaBD,
                    HoDem = model.HoDem,
                    Ten = model.Ten,
                    NgaySinh = model.NgaySinh,
                    GioiTinh = char.Parse(model.GioiTinh), // Chuyển string 'M'/'F' sang char
                    DiaChi = model.DiaChi,
                    SDT = model.SDT,
                    Email = model.Email
                };

                // 2. Thêm vào bảng
                db.tBanDocs.InsertOnSubmit(newBanDoc);

                try
                {
                    // 3. Thực hiện lưu thay đổi vào DB
                    db.SubmitChanges();
                    return true; // Thành công
                }
                catch (Exception ex)
                {
                    // Ghi log lỗi hoặc xử lý cụ thể hơn
                    Console.WriteLine("Lỗi khi thêm Bạn Đọc: " + ex.Message);
                    return false; // Thất bại
                }
            }
        }

        public bool UpdateBanDoc(BanDocDTO model)
        {
            using (var db = new QLThuVienDataContext())
            {
                // 1. Tìm bản ghi cần cập nhật
                tBanDoc existingBanDoc = db.tBanDocs.SingleOrDefault(bd => bd.MaBD == model.MaBD);

                if (existingBanDoc != null)
                {
                    // 2. Cập nhật các thuộc tính (trừ MaBD)
                    existingBanDoc.HoDem = model.HoDem;
                    existingBanDoc.Ten = model.Ten;
                    existingBanDoc.NgaySinh = model.NgaySinh;
                    existingBanDoc.GioiTinh = char.Parse(model.GioiTinh); // Chuyển string 'M'/'F' sang char
                    existingBanDoc.DiaChi = model.DiaChi;
                    existingBanDoc.SDT = model.SDT;
                    existingBanDoc.Email = model.Email;

                    try
                    {
                        // 3. Thực hiện lưu thay đổi vào DB
                        db.SubmitChanges();
                        return true; // Cập nhật thành công
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi khi cập nhật Bạn Đọc: " + ex.Message);
                        return false; // Thất bại
                    }
                }
                return false; // Không tìm thấy Mã BD
            }
        }

        public bool DeleteBanDoc(string maBD)
        {
            using (var db = new QLThuVienDataContext())
            {
                // 1. Tìm bản ghi cần xóa
                tBanDoc banDocToDelete = db.tBanDocs.SingleOrDefault(bd => bd.MaBD == maBD);

                if (banDocToDelete != null)
                {
                    // 2. Thực hiện xóa khỏi bảng
                    db.tBanDocs.DeleteOnSubmit(banDocToDelete);

                    try
                    {
                        // 3. Thực hiện lưu thay đổi vào DB
                        db.SubmitChanges();
                        return true; // Xóa thành công
                    }
                    catch (Exception ex)
                    {
                        // Lỗi ràng buộc khóa ngoại (ví dụ: Bạn Đọc đang mượn sách)
                        Console.WriteLine("Lỗi khi xóa Bạn Đọc: " + ex.Message);
                        return false;
                    }
                }
                return false; // Không tìm thấy Mã BD
            }
        }

        public List<BanDocDTO> SearchBanDoc(List<SearchFilter> filters)
        {
            using (var db = new QLThuVienDataContext())
            {
                // Khởi tạo truy vấn ban đầu (SELECT * FROM tBanDocs)
                IQueryable<tBanDoc> query = db.tBanDocs.AsQueryable();

                foreach (var filter in filters)
                {
                    // Lấy tên trường thực tế trong tBanDoc
                    string fieldName = filter.FieldName;
                    string op = filter.Operator;
                    string value = filter.Value;
                    string valueTo = filter.ValueTo;

                    // --- Xử lý Logic tìm kiếm cho từng trường ---

                    if (fieldName == "MaBD")
                    {
                        if (op == "=") query = query.Where(bd => bd.MaBD == value);
                        else if (op == "LIKE") query = query.Where(bd => bd.MaBD.Contains(value));
                        else if (op == "Bắt đầu bằng") query = query.Where(bd => bd.MaBD.StartsWith(value));
                    }
                    else if (fieldName == "HoTen")
                    {
                        // Ghép HoDem và Ten để tìm kiếm
                        string hoTenKeyword = value.Trim();
                        if (op == "LIKE")
                            query = query.Where(bd => (bd.HoDem + " " + bd.Ten).Contains(hoTenKeyword));
                        else if (op == "Bắt đầu bằng")
                            query = query.Where(bd => (bd.HoDem + " " + bd.Ten).StartsWith(hoTenKeyword));
                    }
                    else if (fieldName == "Email")
                    {
                        if (op == "=") query = query.Where(bd => bd.Email == value);
                        else if (op == "LIKE") query = query.Where(bd => bd.Email.Contains(value));
                    }
                    else if (fieldName == "DiaChi")
                    {
                        if (op == "=") query = query.Where(bd => bd.DiaChi == value);
                        else if (op == "LIKE") query = query.Where(bd => bd.DiaChi.Contains(value));
                    }
                    else if (fieldName == "SDT" && op == "=")
                    {
                        query = query.Where(bd => bd.SDT == value);
                    }
                    else if (fieldName == "NgaySinh" && DateTime.TryParse(value, out DateTime dtValue))
                    {
                        // Xử lý cho trường DateTime (NgaySinh)
                        DateTime dtStart;
                        DateTime dtEnd;

                        if (op == "=")
                            query = query.Where(bd => bd.NgaySinh.Date == dtValue.Date);
                        else if (op == ">")
                            query = query.Where(bd => bd.NgaySinh > dtValue);
                        else if (op == "<")
                            query = query.Where(bd => bd.NgaySinh < dtValue);
                        else if (op == ">=")
                            query = query.Where(bd => bd.NgaySinh >= dtValue);
                        else if (op == "<=")
                            query = query.Where(bd => bd.NgaySinh <= dtValue);

                        // Xử lý Toán tử Khoảng và Đoạn
                        else if (DateTime.TryParse(valueTo, out dtEnd))
                        {
                            dtStart = dtValue;
                            if (op == "Khoảng")
                            {
                                // Lấy nằm trong (không bao gồm hai đầu)
                                query = query.Where(bd => bd.NgaySinh > dtStart && bd.NgaySinh < dtEnd);
                            }
                            else if (op == "Đoạn")
                            {
                                // Lấy bằng ở cả hai đầu
                                query = query.Where(bd => bd.NgaySinh >= dtStart && bd.NgaySinh <= dtEnd);
                            }
                        }
                    }
                    // Bỏ qua bộ lọc nếu FieldName không khớp hoặc không hợp lệ
                }

                // Chuyển kết quả sang DTO và trả về
                var result = from bd in query
                             select new BanDocDTO
                             {
                                 MaBD = bd.MaBD,
                                 HoDem = bd.HoDem,
                                 Ten = bd.Ten,
                                 GioiTinh = bd.GioiTinh.ToString(),
                                 NgaySinh = bd.NgaySinh,
                                 DiaChi = bd.DiaChi,
                                 SDT = bd.SDT,
                                 Email = bd.Email
                             };

                return result.ToList();
            }
        }

        // Lấy tổng số bạn đọc
        public int GetTotalBanDoc()
        {
            using (var db = new QLThuVienDataContext())
            {
                return db.tBanDocs.Count();
            }
        }
    }
}
