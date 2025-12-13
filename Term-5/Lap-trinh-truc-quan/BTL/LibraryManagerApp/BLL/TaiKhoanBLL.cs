// File: TaiKhoanBLL.cs
using LibraryManagerApp.DAL;
using LibraryManagerApp.DTO;
using LibraryManagerApp.Helpers; // Giả định có lớp PasswordHasher hoặc tương đương
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.BLL
{
    internal class TaiKhoanBLL
    {
        private TaiKhoanDAL _dal = new TaiKhoanDAL();
        // private readonly string _salt = "Your_Secret_Salt"; // Nếu sử dụng Salt tĩnh

        #region CHỨC NĂNG READ

        public List<TaiKhoanDTO> LayDanhSachTaiKhoan()
        {
            return _dal.GetAllTaiKhoanDTO();
        }

        public TaiKhoanDTO LayChiTietTaiKhoan(string maTK)
        {
            return _dal.GetTaiKhoanByMaTK(maTK);
        }

        public List<VaiTroDTO> LayTatCaVaiTro()
        {
            return _dal.GetAllVaiTro();
        }

        // Hàm mới: Lấy danh sách Nhân Viên chưa có Tài Khoản
        public List<NhanVienChuaCoTaiKhoanDTO> LayNhanVienChuaCoTaiKhoan()
        {
            return _dal.GetNhanVienChuaCoTaiKhoan();
        }
        #endregion

        #region CHỨC NĂNG CREATE

        public string ThemTaiKhoan(TaiKhoanDTO model, string rawPassword, out int errorStatus)
        {
            errorStatus = 0; // 0: Thành công

            // 1. Kiểm tra nghiệp vụ: Mã NV phải tồn tại và chưa có TK
            if (!_dal.IsMaNVAvailable(model.MaNV))
            {
                // Giả định: 101 - Mã NV không tồn tại HOẶC đã có TK
                errorStatus = 101;
                return string.Empty;
            }

            // 2. Sinh Mã TK mới
            string maTK = _dal.GenerateNewMaTK();

            if (string.IsNullOrEmpty(maTK))
            {
                // Giả định: 102 - Lỗi sinh mã (đã đạt giới hạn/lỗi SP)
                errorStatus = 102;
                return string.Empty;
            }

            // 3. Gán Mã TK đã sinh vào model
            model.MaTK = maTK;

            // 4. Mã hóa Mật khẩu trước khi lưu (Giả định PasswordHasher có hàm Hash)
            // Cần triển khai lớp PasswordHasher riêng hoặc sử dụng thư viện
            try
            {
                model.MatKhau = PasswordHasher.HashPassword(rawPassword);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi mã hóa mật khẩu: " + ex.Message);
                errorStatus = 99; // Lỗi hệ thống
                return string.Empty;
            }

            // 5. Thiết lập các giá trị mặc định/bổ sung
            model.TrangThai = model.TrangThai ?? "Hoạt động";
            model.NgayTao = DateTime.Now.Date; // Ghi đè bằng ngày hiện tại của hệ thống

            // 6. Thực hiện Insert
            if (!_dal.InsertTaiKhoan(model))
            {
                errorStatus = 99; // Lỗi hệ thống/DB khi Insert (có thể do trùng TenDangNhap)
                return string.Empty;
            }

            return maTK; // Trả về Mã TK nếu thành công
        }

        #endregion

        #region CHỨC NĂNG UPDATE

        // isPasswordChanged: flag từ GUI báo hiệu người dùng có nhập mật khẩu mới hay không
        public bool CapNhatTaiKhoan(TaiKhoanDTO model, string newRawPassword = null)
        {
            // Nếu người dùng có nhập mật khẩu mới
            if (!string.IsNullOrEmpty(newRawPassword))
            {
                // Mã hóa mật khẩu mới và gán vào model
                model.MatKhau = PasswordHasher.HashPassword(newRawPassword);
            }
            else
            {
                // Nếu không đổi, gán rỗng hoặc null. 
                // DAL sẽ dùng điều kiện if (!string.IsNullOrEmpty(model.MatKhau)) để quyết định có update không.
                model.MatKhau = null;
            }

            // (Không cho phép đổi MaNV sau khi tạo)

            return _dal.UpdateTaiKhoan(model);
        }

        #endregion

        #region CHỨC NĂNG DELETE

        public bool XoaTaiKhoan(string maTK)
        {
            // Logic nghiệp vụ: Cần kiểm tra Tài khoản này có đang được sử dụng (ví dụ: đang đăng nhập) hay không.
            // Tạm thời, ta chỉ gọi DAL.
            // if (SessionManager.IsCurrentUser(maTK)) return false; // Ví dụ kiểm tra nghiệp vụ

            return _dal.DeleteTaiKhoan(maTK);
        }

        #endregion

        #region MÃ LỖI NGHIỆP VỤ BLL
        public string GetErrorMessage(int status)
        {
            switch (status)
            {
                case 101: return "Lỗi: Mã Nhân Viên không tồn tại hoặc Nhân Viên đã được cấp Tài Khoản.";
                case 102: return "Lỗi: Không thể sinh Mã Tài Khoản (Đã đạt giới hạn hoặc lỗi hệ thống).";
                case 99: return "Lỗi hệ thống: Không thể lưu Tài Khoản vào CSDL.";
                default: return "Lỗi không xác định.";
            }
        }
        #endregion

        #region CHỨC NĂNG TÌM KIẾM
        public List<TaiKhoanDTO> TimKiemTaiKhoan(List<SearchFilter> filters)
        {
            return _dal.SearchTaiKhoan(filters);
        }

        // Hàm cung cấp Metadata cho UI (FrmTimKiem)
        public List<FieldMetadata> GetSearchFields()
        {
            return SearchMetadata.GetTaiKhoanFields();
        }
        #endregion
    }
}