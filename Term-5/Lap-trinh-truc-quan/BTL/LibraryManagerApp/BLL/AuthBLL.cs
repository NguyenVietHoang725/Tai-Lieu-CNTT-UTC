using LibraryManagerApp.DAL;
using LibraryManagerApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LibraryManagerApp.BLL
{
    internal class AuthBLL
    {
        private AuthDAL _dal = new AuthDAL();

        // Hàm nghiệp vụ: Xử lý đăng nhập
        // Trả về đối tượng LoginSessionDTO nếu thành công, null nếu thất bại hoặc bị khóa.
        public LoginSessionDTO PerformLogin(string username, string password)
        {
            try
            {
                Debug.WriteLine("=== BLL: Bắt đầu xử lý đăng nhập ===");

                // Validate input
                if (string.IsNullOrWhiteSpace(username))
                {
                    Debug.WriteLine("❌ BLL: Username rỗng");
                    return null;
                }

                if (string.IsNullOrEmpty(password))
                {
                    Debug.WriteLine("❌ BLL: Password rỗng");
                    return null;
                }

                // Trim username để loại bỏ khoảng trắng thừa
                username = username.Trim();

                Debug.WriteLine($"BLL: Gọi DAL với username='{username}', password length={password.Length}");

                // 1. Gọi DAL để xác thực và lấy dữ liệu phiên làm việc
                LoginSessionDTO userSession = _dal.AuthenticateUser(username, password);

                if (userSession == null)
                {
                    Debug.WriteLine("❌ BLL: DAL trả về null");
                    // Sai tên đăng nhập/mật khẩu hoặc tài khoản không hoạt động
                    return null;
                }

                // 2. Kiểm tra thêm trạng thái (Mặc dù DAL đã lọc, nhưng kiểm tra lại cho chắc chắn)
                if (userSession.TrangThaiTK != "Hoạt động")
                {
                    Debug.WriteLine($"❌ BLL: Trạng thái không hợp lệ: {userSession.TrangThaiTK}");
                    // Tài khoản không hoạt động/bị khóa
                    return null;
                }

                Debug.WriteLine("✓ BLL: Xác thực thành công");

                // 3. Đăng nhập thành công, trả về dữ liệu phiên làm việc
                return userSession;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ BLL Exception: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");

                // Log lỗi chi tiết hơn
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                // Không throw exception để UI có thể hiển thị thông báo thân thiện
                return null;
            }
        }
    }
}