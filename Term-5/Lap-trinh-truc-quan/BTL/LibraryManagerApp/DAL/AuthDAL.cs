using LibraryManagerApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LibraryManagerApp.DAL
{
    internal class AuthDAL
    {
        public LoginSessionDTO AuthenticateUser(string username, string password)
        {
            try
            {
                using (var db = new QLThuVienDataContext())
                {
                    // Debug: Log thông tin đăng nhập
                    Debug.WriteLine($"=== ĐĂNG NHẬP ===");
                    Debug.WriteLine($"Username: {username}");
                    Debug.WriteLine($"Password Length: {password?.Length}");

                    // Bước 1: Kiểm tra xem tài khoản có tồn tại không
                    var taiKhoan = db.tTaiKhoans
                        .FirstOrDefault(tk => tk.TenDangNhap == username);

                    if (taiKhoan == null)
                    {
                        Debug.WriteLine("❌ Không tìm thấy tài khoản");
                        return null;
                    }

                    Debug.WriteLine($"✓ Tìm thấy tài khoản: {taiKhoan.MaTK}");
                    Debug.WriteLine($"  - Mật khẩu trong DB: {taiKhoan.MatKhau}");
                    Debug.WriteLine($"  - Mật khẩu nhập vào: {password}");
                    Debug.WriteLine($"  - Trùng khớp: {taiKhoan.MatKhau == password}");
                    Debug.WriteLine($"  - Trạng thái: {taiKhoan.TrangThai}");

                    // Bước 2: Kiểm tra mật khẩu
                    //if (taiKhoan.MatKhau != password)
                    //{
                    //    Debug.WriteLine("❌ Mật khẩu không đúng");
                    //    return null;
                    //}

                    bool isPasswordValid = false;
                    bool isHashed = LibraryManagerApp.Helpers.PasswordHasher.IsHashedPassword(taiKhoan.MatKhau);

                    if (isHashed)
                    {
                        // Nếu là mật khẩu đã hash theo Identity
                        isPasswordValid = LibraryManagerApp.Helpers.IdentityPasswordHelper.VerifyIdentityPassword(taiKhoan.MatKhau, password);
                    }
                    else
                    {
                        // Nếu là mật khẩu thường (chưa hash)
                        if (taiKhoan.MatKhau == password)
                        {
                            isPasswordValid = true;

                            try
                            {
                                // Hash lại bằng Identity và lưu vào DB để chuyển đổi dần
                                string newHashedPassword = LibraryManagerApp.Helpers.IdentityPasswordHelper.HashIdentityPassword(password);
                                taiKhoan.MatKhau = newHashedPassword;
                                db.SubmitChanges();

                                Debug.WriteLine($"🔄 Đã hash lại mật khẩu cho tài khoản: {taiKhoan.TenDangNhap}");
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"⚠️ Không thể hash lại mật khẩu: {ex.Message}");
                            }
                        }
                    }

                    if (!isPasswordValid)
                    {
                        Debug.WriteLine("❌ Mật khẩu không đúng");
                        return null;
                    }

                    // Bước 3: Kiểm tra trạng thái
                    if (taiKhoan.TrangThai != "Hoạt động")
                    {
                        Debug.WriteLine($"❌ Tài khoản không hoạt động: {taiKhoan.TrangThai}");
                        return null;
                    }

                    Debug.WriteLine("✓ Mật khẩu và trạng thái hợp lệ");

                    // Bước 4: Lấy thông tin nhân viên
                    var nhanVien = db.tNhanViens
                        .FirstOrDefault(nv => nv.MaNV == taiKhoan.MaNV);

                    if (nhanVien == null)
                    {
                        Debug.WriteLine("❌ Không tìm thấy thông tin nhân viên");
                        return null;
                    }

                    Debug.WriteLine($"✓ Tìm thấy nhân viên: {nhanVien.HoDem} {nhanVien.Ten}");

                    // Bước 5: Lấy thông tin vai trò
                    var vaiTro = db.tVaiTros
                        .FirstOrDefault(vt => vt.MaVT == taiKhoan.MaVT);

                    if (vaiTro == null)
                    {
                        Debug.WriteLine("❌ Không tìm thấy thông tin vai trò");
                        return null;
                    }

                    Debug.WriteLine($"✓ Tìm thấy vai trò: {vaiTro.TenVT}");

                    // Bước 6: Tạo DTO
                    var result = new LoginSessionDTO
                    {
                        // Thông tin Tài khoản
                        MaTK = taiKhoan.MaTK,
                        TenDangNhap = taiKhoan.TenDangNhap,
                        TrangThaiTK = taiKhoan.TrangThai,

                        // Thông tin Nhân viên
                        MaNV = taiKhoan.MaNV,
                        HoTenNV = $"{nhanVien.HoDem} {nhanVien.Ten}".Trim(),

                        // Thông tin Vai trò
                        MaVT = taiKhoan.MaVT,
                        TenVT = vaiTro.TenVT
                    };

                    Debug.WriteLine("✓ Đăng nhập thành công!");
                    Debug.WriteLine($"  - Họ tên: {result.HoTenNV}");
                    Debug.WriteLine($"  - Vai trò: {result.TenVT}");

                    return result;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ LỖI: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                throw; // Re-throw để BLL có thể xử lý
            }
        }

        // Phương thức thay thế: Sử dụng JOIN như code gốc nhưng có debug
        public LoginSessionDTO AuthenticateUserWithJoin(string username, string password)
        {
            try
            {
                using (var db = new QLThuVienDataContext())
                {
                    Debug.WriteLine($"=== ĐĂNG NHẬP (JOIN) ===");
                    Debug.WriteLine($"Username: {username}");

                    // Thực hiện JOIN 3 bảng: tTaiKhoan, tNhanVien, tVaiTro
                    // QUAN TRỌNG: Phải Trim() các trường CHAR và NVARCHAR để loại bỏ khoảng trắng thừa
                    var query = from tk in db.tTaiKhoans
                                join nv in db.tNhanViens on tk.MaNV.Trim() equals nv.MaNV.Trim()
                                join vt in db.tVaiTros on tk.MaVT.Trim() equals vt.MaVT.Trim()
                                where tk.TenDangNhap.Trim() == username &&
                                      tk.MatKhau.Trim() == password &&
                                      tk.TrangThai.Trim() == "Hoạt động"
                                select new LoginSessionDTO
                                {
                                    // Thông tin Tài khoản
                                    MaTK = tk.MaTK.Trim(),
                                    TenDangNhap = tk.TenDangNhap.Trim(),
                                    TrangThaiTK = tk.TrangThai.Trim(),

                                    // Thông tin Nhân viên
                                    MaNV = tk.MaNV.Trim(),
                                    HoTenNV = (nv.HoDem.Trim() + " " + nv.Ten.Trim()).Trim(),

                                    // Thông tin Vai trò
                                    MaVT = tk.MaVT.Trim(),
                                    TenVT = vt.TenVT.Trim()
                                };

                    var result = query.FirstOrDefault();

                    if (result == null)
                    {
                        Debug.WriteLine("❌ Không tìm thấy kết quả từ JOIN query");

                        // Debug thêm: Kiểm tra từng điều kiện
                        var checkUser = db.tTaiKhoans.FirstOrDefault(tk => tk.TenDangNhap == username);
                        if (checkUser == null)
                        {
                            Debug.WriteLine("  - Username không tồn tại");
                        }
                        else
                        {
                            Debug.WriteLine($"  - Username tồn tại: {checkUser.MaTK}");
                            Debug.WriteLine($"  - Password match: {checkUser.MatKhau == password}");
                            Debug.WriteLine($"  - Status: {checkUser.TrangThai}");
                        }
                    }
                    else
                    {
                        Debug.WriteLine("✓ Đăng nhập thành công!");
                        Debug.WriteLine($"  - Họ tên: {result.HoTenNV}");
                        Debug.WriteLine($"  - Vai trò: {result.TenVT}");
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ LỖI: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                throw;
            }
        }
    }
}