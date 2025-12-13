// File: TaiKhoanDAL.cs
using LibraryManagerApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DAL
{
    internal class TaiKhoanDAL
    {
        // === BỔ SUNG: Hàm MapToDTO cơ bản (Chỉ dùng cho các hàm không JOIN, ví dụ: Search) ===
        // Dùng để chuyển đổi từ tTaiKhoan sang TaiKhoanDTO
        // Lưu ý: Hàm này KHÔNG điền HoTenNV và TenVaiTro
        private TaiKhoanDTO MapToDTO(tTaiKhoan tk)
        {
            return new TaiKhoanDTO
            {
                MaTK = tk.MaTK,
                MaNV = tk.MaNV,
                MaVT = tk.MaVT,
                TenDangNhap = tk.TenDangNhap,
                MatKhau = tk.MatKhau,
                TrangThai = tk.TrangThai,
                NgayTao = tk.NgayTao
                // HoTenNV và TenVaiTro để trống (null) trong trường hợp này
            };
        }
        #region LẤY THÔNG TIN

        // Hàm READ chính: Lấy tất cả Tài Khoản kèm thông tin liên quan
        public List<TaiKhoanDTO> GetAllTaiKhoanDTO()
        {
            using (var db = new QLThuVienDataContext())
            {
                // Truy vấn phức tạp: JOIN tTaiKhoan, tNhanVien, tVaiTro
                var query = from tk in db.tTaiKhoans
                            join nv in db.tNhanViens on tk.MaNV equals nv.MaNV
                            join vt in db.tVaiTros on tk.MaVT equals vt.MaVT
                            select new TaiKhoanDTO
                            {
                                // Dữ liệu thô từ tTaiKhoan
                                MaTK = tk.MaTK,
                                MaNV = tk.MaNV,
                                MaVT = tk.MaVT,
                                TenDangNhap = tk.TenDangNhap,
                                MatKhau = tk.MatKhau, // Vẫn lấy MatKhau (đã mã hóa)
                                TrangThai = tk.TrangThai,
                                NgayTao = tk.NgayTao,

                                // Dữ liệu JOIN
                                HoTenNV = nv.HoDem + " " + nv.Ten,
                                TenVaiTro = vt.TenVT
                            };

                return query.ToList();
            }
        }
        public List<NhanVienChuaCoTaiKhoanDTO> GetNhanVienChuaCoTaiKhoan()
        {
            using (var db = new QLThuVienDataContext())
            {
                // Tìm những MaNV có trong tNhanVien nhưng KHÔNG có trong tTaiKhoan
                var query = from nv in db.tNhanViens
                            where !db.tTaiKhoans.Any(tk => tk.MaNV == nv.MaNV)
                            select new NhanVienChuaCoTaiKhoanDTO
                            {
                                MaNV = nv.MaNV,
                                HoTen = nv.HoDem + " " + nv.Ten
                            };
                return query.ToList();
            }
        }
        // Hàm READ Chi tiết (dùng cho CellClick)
        public TaiKhoanDTO GetTaiKhoanByMaTK(string maTK)
        {
            // Tối ưu: Dùng GetAllTaiKhoanDTO().SingleOrDefault() để đơn giản
            return GetAllTaiKhoanDTO().SingleOrDefault(p => p.MaTK == maTK);
        }

        // Hàm lấy danh sách tất cả Vai Trò (dùng cho ComboBox)
        public List<VaiTroDTO> GetAllVaiTro()
        {
            using (var db = new QLThuVienDataContext())
            {
                var query = from vt in db.tVaiTros
                            select new VaiTroDTO
                            {
                                MaVT = vt.MaVT,
                                TenVT = vt.TenVT
                            };
                return query.ToList();
            }
        }

        // Hàm kiểm tra Mã NV có tồn tại và chưa được cấp TK không
        public bool IsMaNVAvailable(string maNV)
        {
            using (var db = new QLThuVienDataContext())
            {
                // 1. Kiểm tra MaNV có tồn tại trong tNhanVien không
                bool nvExists = db.tNhanViens.Any(nv => nv.MaNV == maNV);
                if (!nvExists) return false;

                // 2. Kiểm tra MaNV đã có tài khoản chưa
                bool tkExists = db.tTaiKhoans.Any(tk => tk.MaNV == maNV);

                // Nếu tồn tại NV và chưa có TK thì là AVAILABLE (True)
                return nvExists && !tkExists;
            }
        }

        // Hàm gọi Stored Procedure SP_GenerateNewMaTK
        public string GenerateNewMaTK()
        {
            using (var db = new QLThuVienDataContext())
            {
                string newMaTK = string.Empty;

                try
                {
                    //SP_GenerateNewMaTK có tham số OUTPUT là ref string
                    db.SP_GenerateNewMaTK(ref newMaTK);

                    // SP này không trả về errorStatus, chỉ trả về chuỗi rỗng nếu lỗi
                    if (string.IsNullOrEmpty(newMaTK))
                    {
                        // Giả định rằng nếu SP trả về rỗng, có lỗi xảy ra
                        throw new Exception("Lỗi khi gọi SP_GenerateNewMaTK hoặc đã đạt giới hạn.");
                    }
                    return newMaTK;
                }
                catch (Exception ex)
                {
                    // Ghi log lỗi SP
                    Console.WriteLine("Lỗi khi gọi SP_GenerateNewMaTK: " + ex.Message);
                    return string.Empty;
                }
            }
        }
        #endregion

        #region NHẬP SỬA XÓA

        // Hàm Insert (CREATE)
        public bool InsertTaiKhoan(TaiKhoanDTO model)
        {
            using (var db = new QLThuVienDataContext())
            {
                // Tạo một đối tượng tTaiKhoan từ DTO (chỉ lấy các trường thô)
                tTaiKhoan newTK = new tTaiKhoan
                {
                    MaTK = model.MaTK,
                    MaNV = model.MaNV,
                    MaVT = model.MaVT,
                    TenDangNhap = model.TenDangNhap,
                    MatKhau = model.MatKhau, // Đã mã hóa từ BLL
                    TrangThai = model.TrangThai,
                    NgayTao = model.NgayTao.Date // Chỉ lấy phần ngày
                };

                db.tTaiKhoans.InsertOnSubmit(newTK);

                try
                {
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    // Lỗi DB (trùng MaTK, sai FK, trùng TenDangNhap,...)
                    Console.WriteLine("Lỗi khi thêm Tài Khoản: " + ex.Message);
                    return false;
                }
            }
        }

        // Hàm Update
        public bool UpdateTaiKhoan(TaiKhoanDTO model)
        {
            using (var db = new QLThuVienDataContext())
            {
                // 1. Tìm bản ghi cần cập nhật
                tTaiKhoan existingTK = db.tTaiKhoans.SingleOrDefault(tk => tk.MaTK == model.MaTK);

                if (existingTK != null)
                {
                    // 2. Cập nhật các thuộc tính (Không cập nhật MaTK và MaNV)
                    existingTK.MaVT = model.MaVT;
                    existingTK.TenDangNhap = model.TenDangNhap;

                    // Chỉ cập nhật MatKhau nếu BLL đã truyền vào giá trị MỚI (đã mã hóa)
                    if (!string.IsNullOrEmpty(model.MatKhau))
                    {
                        existingTK.MatKhau = model.MatKhau;
                    }

                    existingTK.TrangThai = model.TrangThai;
                    existingTK.NgayTao = model.NgayTao.Date;

                    try
                    {
                        // 3. Thực hiện lưu thay đổi vào DB
                        db.SubmitChanges();
                        return true; // Cập nhật thành công
                    }
                    catch (Exception ex)
                    {
                        // Lỗi DB (Ví dụ: MaVT không hợp lệ, trùng TenDangNhap)
                        Console.WriteLine("Lỗi khi cập nhật Tài Khoản: " + ex.Message);
                        return false;
                    }
                }
                return false; // Không tìm thấy Mã TK
            }
        }

        // Hàm Delete
        public bool DeleteTaiKhoan(string maTK)
        {
            using (var db = new QLThuVienDataContext())
            {
                // 1. Tìm bản ghi cần xóa
                tTaiKhoan tkToDelete = db.tTaiKhoans.SingleOrDefault(tk => tk.MaTK == maTK);

                if (tkToDelete != null)
                {
                    // 2. Thực hiện xóa khỏi bảng
                    db.tTaiKhoans.DeleteOnSubmit(tkToDelete);

                    try
                    {
                        // 3. Thực hiện lưu thay đổi vào DB
                        db.SubmitChanges();
                        return true; // Xóa thành công
                    }
                    catch (Exception ex)
                    {
                        // Lỗi ràng buộc khóa ngoại (ví dụ: Tài khoản này đã cấp Thẻ Bạn Đọc,...)
                        Console.WriteLine("Lỗi khi xóa Tài Khoản: " + ex.Message);
                        return false;
                    }
                }
                return false; // Không tìm thấy Mã TK
            }
        }
        //  Tìm kiếm Tài Khoản
        public List<TaiKhoanDTO> SearchTaiKhoan(List<SearchFilter> filters)
        {
            using (var db = new QLThuVienDataContext()) 
            {
                IQueryable<tTaiKhoan> query = db.tTaiKhoans.AsQueryable();

                foreach (var filter in filters)
                {
                    string fieldName = filter.FieldName;
                    string op = filter.Operator;
                    string value = filter.Value;
                    string valueTo = filter.ValueTo;

                    if (fieldName == "MaTK")
                    {
                        if (op == "=") query = query.Where(tk => tk.MaTK == value);
                        else if (op == "LIKE") query = query.Where(tk => tk.MaTK.Contains(value));
                        else if (op == "Bắt đầu bằng") query = query.Where(tk => tk.MaTK.StartsWith(value));
                    }
                    else if (fieldName == "MaNV")
                    {
                        if (op == "=") query = query.Where(tk => tk.MaNV == value);
                        else if (op == "LIKE") query = query.Where(tk => tk.MaNV.Contains(value));
                        else if (op == "Bắt đầu bằng") query = query.Where(tk => tk.MaNV.StartsWith(value));
                    }
                    else if (fieldName == "MaVT" && op == "=")
                    {
                        query = query.Where(tk => tk.MaVT == value);
                    }
                    else if (fieldName == "TenDangNhap")
                    {
                        if (op == "=") query = query.Where(tk => tk.TenDangNhap == value);
                        else if (op == "LIKE") query = query.Where(tk => tk.TenDangNhap.Contains(value));
                        else if (op == "Bắt đầu bằng") query = query.Where(tk => tk.TenDangNhap.StartsWith(value));
                    }
                    else if (fieldName == "TrangThai" && op == "=")
                    {
                        query = query.Where(tk => tk.TrangThai == value);
                    }
                    else if (fieldName == "NgayTao" && DateTime.TryParse(value, out DateTime dtValue))
                    {
                        DateTime dtEnd;
                        if (op == "=")
                            query = query.Where(tk => tk.NgayTao.Date == dtValue.Date);
                        else if (op == ">")
                            query = query.Where(tk => tk.NgayTao > dtValue);
                        else if (op == "<")
                            query = query.Where(tk => tk.NgayTao < dtValue);
                        else if (op == ">=")
                            query = query.Where(tk => tk.NgayTao >= dtValue);
                        else if (op == "<=")
                            query = query.Where(tk => tk.NgayTao <= dtValue);
                        else if (DateTime.TryParse(valueTo, out dtEnd))
                        {
                            DateTime dtStart = dtValue;
                            if (op == "Khoảng")
                                query = query.Where(tk => tk.NgayTao > dtStart && tk.NgayTao < dtEnd);
                            else if (op == "Đoạn")
                                query = query.Where(tk => tk.NgayTao >= dtStart && tk.NgayTao <= dtEnd);
                        }
                    }
                }

                // 2. Sau khi lọc, thực hiện JOIN và MAP ra DTO đầy đủ
                var finalQuery = from tk in query // Lấy kết quả đã lọc
                                 join nv in db.tNhanViens on tk.MaNV equals nv.MaNV
                                 join vt in db.tVaiTros on tk.MaVT equals vt.MaVT
                                 select new TaiKhoanDTO
                                 {
                                     // Dữ liệu thô từ tTaiKhoan
                                     MaTK = tk.MaTK,
                                     MaNV = tk.MaNV,
                                     MaVT = tk.MaVT,
                                     TenDangNhap = tk.TenDangNhap,
                                     MatKhau = tk.MatKhau,
                                     TrangThai = tk.TrangThai,
                                     NgayTao = tk.NgayTao,

                                     // Dữ liệu JOIN
                                     HoTenNV = nv.HoDem + " " + nv.Ten,
                                     TenVaiTro = vt.TenVT
                                 };

                return finalQuery.ToList();
            }
        }

        #endregion
    }
}