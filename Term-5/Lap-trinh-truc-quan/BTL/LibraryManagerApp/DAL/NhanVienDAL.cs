using LibraryManagerApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DAL
{
    internal class NhanVienDAL
    {
        // Hàm chuyển đổi LINQ object sang DTO (Dùng lại logic từ BanDocDAL)
        private NhanVienDTO MapToDTO(tNhanVien nv)
        {
            return new NhanVienDTO
            {
                MaNV = nv.MaNV,
                HoDem = nv.HoDem,
                Ten = nv.Ten,
                NgaySinh = nv.NgaySinh,
                GioiTinh = nv.GioiTinh.ToString(), // Chuyển char sang string
                DiaChi = nv.DiaChi,
                SDT = nv.SDT,
                Email = nv.Email,
                PhuTrach = nv.PhuTrach
            };
        }

        // 1. Lấy tất cả Nhân viên
        public List<NhanVienDTO> GetAllNhanVienDTO()
        {
            using (var db = new QLThuVienDataContext())
            {
                var query = from nv in db.tNhanViens
                            select MapToDTO(nv);

                return query.ToList();
            }
        }

        // 2. Lấy chi tiết Nhân viên theo Mã
        public NhanVienDTO GetNhanVienByMaNV(string maNV)
        {
            using (var db = new QLThuVienDataContext())
            {
                var nv = db.tNhanViens.SingleOrDefault(p => p.MaNV == maNV);
                return nv != null ? MapToDTO(nv) : null;
            }
        }

        // 3. Sinh Mã Nhân viên mới (Gọi Stored Procedure)
        public string GenerateNewMaNV()
        {
            using (var db = new QLThuVienDataContext())
            {
                string newMaNV = string.Empty;
                // Gọi Stored Procedure SP_GenerateNewMaNV
                db.SP_GenerateNewMaNV(ref newMaNV);
                return newMaNV;
            }
        }

        // 4. Thêm Nhân viên
        public bool InsertNhanVien(NhanVienDTO model)
        {
            using (var db = new QLThuVienDataContext())
            {
                tNhanVien newNhanVien = new tNhanVien
                {
                    MaNV = model.MaNV,
                    HoDem = model.HoDem,
                    Ten = model.Ten,
                    NgaySinh = model.NgaySinh,
                    GioiTinh = char.Parse(model.GioiTinh),
                    DiaChi = model.DiaChi,
                    SDT = model.SDT,
                    Email = model.Email,
                    PhuTrach = model.PhuTrach
                };

                db.tNhanViens.InsertOnSubmit(newNhanVien);

                try
                {
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi thêm Nhân viên: " + ex.Message);
                    return false;
                }
            }
        }

        // 5. Cập nhật Nhân viên
        public bool UpdateNhanVien(NhanVienDTO model)
        {
            using (var db = new QLThuVienDataContext())
            {
                tNhanVien existingNhanVien = db.tNhanViens.SingleOrDefault(nv => nv.MaNV == model.MaNV);

                if (existingNhanVien != null)
                {
                    // Cập nhật các thuộc tính
                    existingNhanVien.HoDem = model.HoDem;
                    existingNhanVien.Ten = model.Ten;
                    existingNhanVien.NgaySinh = model.NgaySinh;
                    existingNhanVien.GioiTinh = char.Parse(model.GioiTinh);
                    existingNhanVien.DiaChi = model.DiaChi;
                    existingNhanVien.SDT = model.SDT;
                    existingNhanVien.Email = model.Email;
                    existingNhanVien.PhuTrach = model.PhuTrach;

                    try
                    {
                        db.SubmitChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi khi cập nhật Nhân viên: " + ex.Message);
                        return false;
                    }
                }
                return false;
            }
        }

        // 6. Xóa Nhân viên
        public bool DeleteNhanVien(string maNV)
        {
            using (var db = new QLThuVienDataContext())
            {
                tNhanVien nhanVienToDelete = db.tNhanViens.SingleOrDefault(nv => nv.MaNV == maNV);

                if (nhanVienToDelete != null)
                {
                    db.tNhanViens.DeleteOnSubmit(nhanVienToDelete);

                    try
                    {
                        db.SubmitChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Lỗi ràng buộc khóa ngoại (ví dụ: NV có liên quan đến Tài khoản)
                        Console.WriteLine("Lỗi khi xóa Nhân viên: " + ex.Message);
                        return false;
                    }
                }
                return false;
            }
        }

        // 7. Tìm kiếm Nhân viên
        public List<NhanVienDTO> SearchNhanVien(List<SearchFilter> filters)
        {
            using (var db = new QLThuVienDataContext())
            {
                IQueryable<tNhanVien> query = db.tNhanViens.AsQueryable();

                foreach (var filter in filters)
                {
                    string fieldName = filter.FieldName;
                    string op = filter.Operator;
                    string value = filter.Value;
                    string valueTo = filter.ValueTo;

                    if (fieldName == "MaNV")
                    {
                        if (op == "=") query = query.Where(nv => nv.MaNV == value);
                        else if (op == "LIKE") query = query.Where(nv => nv.MaNV.Contains(value));
                        else if (op == "Bắt đầu bằng") query = query.Where(nv => nv.MaNV.StartsWith(value));
                    }
                    else if (fieldName == "HoTen")
                    {
                        string hoTenKeyword = value.Trim();
                        if (op == "LIKE")
                            query = query.Where(nv => (nv.HoDem + " " + nv.Ten).Contains(hoTenKeyword));
                        else if (op == "Bắt đầu bằng")
                            query = query.Where(nv => (nv.HoDem + " " + nv.Ten).StartsWith(hoTenKeyword));
                    }
                    else if (fieldName == "PhuTrach")
                    {
                        if (op == "=") query = query.Where(nv => nv.PhuTrach == value);
                        else if (op == "LIKE") query = query.Where(nv => nv.PhuTrach.Contains(value));
                    }
                    else if (fieldName == "Email")
                    {
                        if (op == "=") query = query.Where(nv => nv.Email == value);
                        else if (op == "LIKE") query = query.Where(nv => nv.Email.Contains(value));
                    }
                    else if (fieldName == "DiaChi")
                    {
                        if (op == "=") query = query.Where(nv => nv.DiaChi == value);
                        else if (op == "LIKE") query = query.Where(nv => nv.DiaChi.Contains(value));
                    }
                    else if (fieldName == "SDT" && op == "=")
                    {
                        query = query.Where(nv => nv.SDT == value);
                    }
                    else if (fieldName == "NgaySinh" && DateTime.TryParse(value, out DateTime dtValue))
                    {
                        DateTime dtEnd;
                        if (op == "=")
                            query = query.Where(nv => nv.NgaySinh.Date == dtValue.Date);
                        else if (op == ">")
                            query = query.Where(nv => nv.NgaySinh > dtValue);
                        else if (op == "<")
                            query = query.Where(nv => nv.NgaySinh < dtValue);
                        else if (op == ">=")
                            query = query.Where(nv => nv.NgaySinh >= dtValue);
                        else if (op == "<=")
                            query = query.Where(nv => nv.NgaySinh <= dtValue);
                        else if (DateTime.TryParse(valueTo, out dtEnd))
                        {
                            DateTime dtStart = dtValue;
                            if (op == "Khoảng")
                                query = query.Where(nv => nv.NgaySinh > dtStart && nv.NgaySinh < dtEnd);
                            else if (op == "Đoạn")
                                query = query.Where(nv => nv.NgaySinh >= dtStart && nv.NgaySinh <= dtEnd);
                        }
                    }
                }

                return query.ToList().Select(nv => MapToDTO(nv)).ToList();
            }
        }
        public List<string> GetDistinctPhuTrach()
        {
            using (var db = new QLThuVienDataContext())
            {
                // Lấy tất cả các giá trị PhuTrach, loại bỏ giá trị trùng lặp và giá trị NULL (nếu có)
                var query = db.tNhanViens
                              .Select(nv => nv.PhuTrach)
                              .Where(pt => pt != null && pt != "")
                              .Distinct()
                              .ToList();
                return query;
            }
        }
    }
}