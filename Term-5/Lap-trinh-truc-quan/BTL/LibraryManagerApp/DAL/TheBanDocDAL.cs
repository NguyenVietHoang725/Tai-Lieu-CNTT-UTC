using LibraryManagerApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DAL
{
    internal class TheBanDocDAL
    {
        #region LẤY THÔNG TIN

        // Hàm READ chính: Lấy tất cả Thẻ Bạn Đọc kèm thông tin liên quan đầy đủ
        public List<TheBanDocDTO> GetAllTheBanDocDTO()
        {
            using (var db = new QLThuVienDataContext())
            {
                // Truy vấn phức tạp: JOIN tTheBanDoc, tBanDoc, tTaiKhoan, tNhanVien
                var query = from tbd in db.tTheBanDocs
                            join bd in db.tBanDocs on tbd.MaBD equals bd.MaBD
                            join tk in db.tTaiKhoans on tbd.MaTK equals tk.MaTK
                            join nv in db.tNhanViens on tk.MaNV equals nv.MaNV
                            select new TheBanDocDTO
                            {
                                // 1. Thông tin Thẻ
                                MaTBD = tbd.MaTBD,
                                MaBD = tbd.MaBD,
                                MaTK = tbd.MaTK,
                                NgayCap = tbd.NgayCap,
                                NgayHetHan = tbd.NgayHetHan,
                                TrangThai = tbd.TrangThai,

                                // 2. Thông tin hiển thị (JOIN)
                                HoTenBD = bd.HoDem + " " + bd.Ten,
                                HoTenNV = nv.HoDem + " " + nv.Ten,

                                // 3. Thông tin chi tiết Bạn đọc (Dùng cho in thẻ/báo cáo)
                                NgaySinh = bd.NgaySinh,
                                // Chuyển đổi dữ liệu hiển thị ngay tại DAL
                                GioiTinh = (bd.GioiTinh == 'M') ? "Nam" : "Nữ",
                                DiaChi = bd.DiaChi,
                                SDT = bd.SDT
                            };

                return query.ToList();
            }
        }

        // Hàm hỗ trợ Autocomplete: Lấy danh sách tất cả Mã Thẻ
        public List<string> GetAllMaTBD()
        {
            using (var db = new QLThuVienDataContext())
            {
                return db.tTheBanDocs
                         .Select(t => t.MaTBD)
                         .ToList();
            }
        }

        // Hàm READ Chi tiết (dùng cho CellClick và In Báo Cáo)
        public TheBanDocDTO GetTheBanDocByMaTBD(string maTBD)
        {
            // Tối ưu: Lấy từ danh sách đã JOIN đầy đủ
            return GetAllTheBanDocDTO().SingleOrDefault(p => p.MaTBD == maTBD);
        }

        // Hàm Kiểm tra Thẻ (Cho chức năng Mượn Trả - Chỉ lấy thông tin cần thiết)
        public TheBanDocDTO GetTheBanDocForMuon(string maTBD)
        {
            using (var db = new QLThuVienDataContext())
            {
                var query = from tbd in db.tTheBanDocs
                            join bd in db.tBanDocs on tbd.MaBD equals bd.MaBD
                            where tbd.MaTBD == maTBD
                            select new TheBanDocDTO
                            {
                                MaTBD = tbd.MaTBD,
                                MaBD = tbd.MaBD,
                                HoTenBD = bd.HoDem + " " + bd.Ten,
                                TrangThai = tbd.TrangThai
                            };

                return query.SingleOrDefault(); // Trả về DTO hoặc null
            }
        }

        #endregion

        #region SINH MÃ TỰ ĐỘNG

        // Hàm gọi Stored Procedure SP_GenerateNewMaTBD
        public string GenerateNewMaTBD(string maBD, out int errorStatus)
        {
            using (var db = new QLThuVienDataContext())
            {
                string newMaTBD = string.Empty;

                // Sử dụng int? (Nullable int) để tương thích với tham số OUTPUT của LINQ to SQL
                int? nullableErrorStatus = -1;

                try
                {
                    // Gọi SP đã được ánh xạ trong DataContext
                    db.SP_GenerateNewMaTBD(maBD, ref newMaTBD, ref nullableErrorStatus);

                    // Chuyển kết quả từ int? sang int
                    errorStatus = nullableErrorStatus ?? 99; // Nếu NULL, gán lỗi 99

                    return newMaTBD;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi gọi SP: " + ex.Message);
                    errorStatus = 99; // Lỗi hệ thống/SP
                    return string.Empty;
                }
            }
        }

        #endregion

        #region NHẬP SỬA XÓA

        // Hàm Insert (CREATE)
        public bool InsertTheBanDoc(TheBanDocDTO model)
        {
            using (var db = new QLThuVienDataContext())
            {
                // Tạo đối tượng Entity từ DTO
                tTheBanDoc newThe = new tTheBanDoc
                {
                    MaTBD = model.MaTBD,
                    MaBD = model.MaBD,
                    MaTK = model.MaTK,
                    NgayCap = model.NgayCap,
                    NgayHetHan = model.NgayHetHan,
                    TrangThai = model.TrangThai
                };

                db.tTheBanDocs.InsertOnSubmit(newThe);

                try
                {
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi thêm Thẻ Bạn Đọc: " + ex.Message);
                    return false;
                }
            }
        }

        // Hàm Update (UPDATE)
        public bool UpdateTheBanDoc(TheBanDocDTO model)
        {
            using (var db = new QLThuVienDataContext())
            {
                // 1. Tìm bản ghi cần cập nhật
                tTheBanDoc existingThe = db.tTheBanDocs.SingleOrDefault(tbd => tbd.MaTBD == model.MaTBD);

                if (existingThe != null)
                {
                    // 2. Cập nhật các thuộc tính (Không cập nhật MaTBD và MaBD)
                    existingThe.MaTK = model.MaTK;
                    existingThe.NgayCap = model.NgayCap;
                    existingThe.NgayHetHan = model.NgayHetHan;
                    existingThe.TrangThai = model.TrangThai;

                    try
                    {
                        // 3. Lưu thay đổi
                        db.SubmitChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi khi cập nhật Thẻ Bạn Đọc: " + ex.Message);
                        return false;
                    }
                }
                return false; // Không tìm thấy
            }
        }

        // Hàm Delete (DELETE)
        public bool DeleteTheBanDoc(string maTBD)
        {
            using (var db = new QLThuVienDataContext())
            {
                // 1. Tìm bản ghi cần xóa
                tTheBanDoc theToDelete = db.tTheBanDocs.SingleOrDefault(tbd => tbd.MaTBD == maTBD);

                if (theToDelete != null)
                {
                    // 2. Xóa khỏi bảng
                    db.tTheBanDocs.DeleteOnSubmit(theToDelete);

                    try
                    {
                        // 3. Lưu thay đổi
                        db.SubmitChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Lỗi ràng buộc khóa ngoại (ví dụ: Thẻ đang có phiếu mượn)
                        Console.WriteLine("Lỗi khi xóa Thẻ Bạn Đọc: " + ex.Message);
                        return false;
                    }
                }
                return false; // Không tìm thấy
            }
        }

        #endregion

        #region TÌM KIẾM
        public List<TheBanDocDTO> SearchTheBanDoc(List<SearchFilter> filters)
        {
            using (var db = new QLThuVienDataContext())
            {
                IQueryable<tTheBanDoc> query = db.tTheBanDocs.AsQueryable();

                foreach (var filter in filters)
                {
                    string fieldName = filter.FieldName;
                    string op = filter.Operator;
                    string value = filter.Value;
                    string valueTo = filter.ValueTo;

                    // --- 1. Xử lý các trường cơ bản ---
                    if (fieldName == "MaTBD")
                    {
                        if (op == "=") query = query.Where(tbd => tbd.MaTBD == value);
                        else if (op == "LIKE") query = query.Where(tbd => tbd.MaTBD.Contains(value));
                        else if (op == "Bắt đầu bằng") query = query.Where(tbd => tbd.MaTBD.StartsWith(value));
                    }
                    else if (fieldName == "MaBD")
                    {
                        if (op == "=") query = query.Where(tbd => tbd.MaBD == value);
                        else if (op == "LIKE") query = query.Where(tbd => tbd.MaBD.Contains(value));
                        else if (op == "Bắt đầu bằng") query = query.Where(tbd => tbd.MaBD.StartsWith(value));
                    }
                    else if (fieldName == "MaTK" && op == "=")
                    {
                        query = query.Where(tbd => tbd.MaTK == value);
                    }
                    else if (fieldName == "TrangThai" && op == "=")
                    {
                        query = query.Where(tbd => tbd.TrangThai == value);
                    }

                    // --- 2. Xử lý TÌM THEO TÊN (Liên kết bảng) ---
                    else if (fieldName == "HoTenBD")
                    {
                        // Tìm thẻ mà Bạn đọc sở hữu có tên chứa từ khóa
                        query = query.Where(tbd => db.tBanDocs.Any(bd =>
                            bd.MaBD == tbd.MaBD &&
                            (bd.HoDem + " " + bd.Ten).Contains(value)));
                    }
                    else if (fieldName == "HoTenNV")
                    {
                        // Tìm thẻ mà Nhân viên cấp có tên chứa từ khóa
                        // tTheBanDoc -> tTaiKhoan -> tNhanVien
                        query = query.Where(tbd => db.tTaiKhoans.Any(tk =>
                            tk.MaTK == tbd.MaTK &&
                            db.tNhanViens.Any(nv => nv.MaNV == tk.MaNV && (nv.HoDem + " " + nv.Ten).Contains(value))
                        ));
                    }

                    // --- 3. Xử lý Ngày tháng ---
                    else if (fieldName == "NgayCap" && DateTime.TryParse(value, out DateTime dtValue))
                    {
                        DateTime dtEnd;
                        if (op == "=") query = query.Where(tbd => tbd.NgayCap.Date == dtValue.Date);
                        else if (op == ">") query = query.Where(tbd => tbd.NgayCap > dtValue);
                        else if (op == "<") query = query.Where(tbd => tbd.NgayCap < dtValue);
                        else if (op == ">=") query = query.Where(tbd => tbd.NgayCap >= dtValue);
                        else if (op == "<=") query = query.Where(tbd => tbd.NgayCap <= dtValue);
                        else if (DateTime.TryParse(valueTo, out dtEnd))
                        {
                            if (op == "Khoảng") query = query.Where(tbd => tbd.NgayCap > dtValue && tbd.NgayCap < dtEnd);
                            else if (op == "Đoạn") query = query.Where(tbd => tbd.NgayCap >= dtValue && tbd.NgayCap <= dtEnd);
                        }
                    }
                    else if (fieldName == "NgayHetHan" && DateTime.TryParse(value, out DateTime dtValueHetHan))
                    {
                        // (Logic tương tự Ngày Cấp)
                        DateTime dtEnd;
                        if (op == "=") query = query.Where(tbd => tbd.NgayHetHan.Value.Date == dtValueHetHan.Date);
                        else if (op == ">") query = query.Where(tbd => tbd.NgayHetHan > dtValueHetHan);
                        else if (op == "<") query = query.Where(tbd => tbd.NgayHetHan < dtValueHetHan);
                        else if (op == ">=") query = query.Where(tbd => tbd.NgayHetHan >= dtValueHetHan);
                        else if (op == "<=") query = query.Where(tbd => tbd.NgayHetHan <= dtValueHetHan);
                        else if (DateTime.TryParse(valueTo, out dtEnd))
                        {
                            if (op == "Khoảng") query = query.Where(tbd => tbd.NgayHetHan > dtValueHetHan && tbd.NgayHetHan < dtEnd);
                            else if (op == "Đoạn") query = query.Where(tbd => tbd.NgayHetHan >= dtValueHetHan && tbd.NgayHetHan <= dtEnd);
                        }
                    }
                }

                // --- 4. KẾT QUẢ HIỂN THỊ (SỬA LỖI MẤT TÊN NHÂN VIÊN TẠI ĐÂY) ---
                var finalQuery = from tbd in query
                                 join bd in db.tBanDocs on tbd.MaBD equals bd.MaBD
                                 join tk in db.tTaiKhoans on tbd.MaTK equals tk.MaTK
                                 join nv in db.tNhanViens on tk.MaNV equals nv.MaNV // <--- BỔ SUNG JOIN NÀY
                                 select new TheBanDocDTO
                                 {
                                     MaTBD = tbd.MaTBD,
                                     MaBD = tbd.MaBD,
                                     MaTK = tbd.MaTK,
                                     NgayCap = tbd.NgayCap,
                                     NgayHetHan = tbd.NgayHetHan,
                                     TrangThai = tbd.TrangThai,

                                     // Thông tin JOIN
                                     HoTenBD = bd.HoDem + " " + bd.Ten,
                                     HoTenNV = nv.HoDem + " " + nv.Ten // <--- LẤY DỮ LIỆU TỪ BẢNG NHÂN VIÊN
                                 };

                return finalQuery.ToList();
            }
        }
        #endregion
    }
}