using LibraryManagerApp.BLL;
using LibraryManagerApp.DTO;
using LibraryManagerApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace LibraryManagerApp.GUI.UserControls.DangNhap
{
    public partial class ucFrmDangNhap : UserControl
    {
        private AuthBLL _authBll = new AuthBLL();

        // Cần một Delegate để Form cha biết khi nào đăng nhập thành công
        public delegate void LoginSuccessHandler(LoginSessionDTO userSession);
        public event LoginSuccessHandler OnLoginSuccess;

        public ucFrmDangNhap()
        {
            InitializeComponent();

            // Đảm bảo txtMatKhau sử dụng ký tự mật khẩu
            txtMatKhau.UseSystemPasswordChar = true;

            // Liên kết sự kiện (nếu chưa có trong Designer)
            btnXacNhan.Click += btnXacNhan_Click;

            // Cho phép nhấn Enter để đăng nhập
            txtTenDangNhap.KeyPress += TxtLogin_KeyPress;
            txtMatKhau.KeyPress += TxtLogin_KeyPress;
        }

        private void TxtLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnXacNhan_Click(sender, EventArgs.Empty);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtTenDangNhap.Text.Trim();
                string password = txtMatKhau.Text;

                Debug.WriteLine("=== UC: Bắt đầu xử lý đăng nhập ===");
                Debug.WriteLine($"Username nhập vào: '{username}'");
                Debug.WriteLine($"Password length: {password?.Length}");
                Debug.WriteLine($"Password: '{password}'"); // CHỈ DÙNG ĐỂ DEBUG, XÓA SAU KHI XONG

                // 1. Validation cơ bản
                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Vui lòng nhập Tên đăng nhập.",
                        "Thiếu thông tin",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtTenDangNhap.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Vui lòng nhập Mật khẩu.",
                        "Thiếu thông tin",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtMatKhau.Focus();
                    return;
                }

                // Disable nút để tránh click nhiều lần
                btnXacNhan.Enabled = false;
                btnXacNhan.Text = "Đang xử lý...";

                // 2. Gọi BLL để xác thực
                LoginSessionDTO userSession = _authBll.PerformLogin(username, password);

                if (userSession != null)
                {
                    Debug.WriteLine("✓ UC: Đăng nhập thành công");

                    // 3. Đăng nhập thành công: Lưu Session và thông báo cho Form cha
                    SessionManager.Login(userSession);

                    MessageBox.Show($"Đăng nhập thành công!\n\n" +
                                  $"Họ tên: {userSession.HoTenNV}\n" +
                                  $"Vai trò: {userSession.TenVT}",
                                    "Thành công",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    // Kích hoạt Event để Form chính ẩn UC đăng nhập
                    OnLoginSuccess?.Invoke(userSession);
                }
                else
                {
                    Debug.WriteLine("❌ UC: Đăng nhập thất bại");

                    // 4. Đăng nhập thất bại
                    MessageBox.Show("Đăng nhập thất bại!\n\n" +
                                  "Nguyên nhân có thể:\n" +
                                  "• Tên đăng nhập hoặc Mật khẩu không đúng\n" +
                                  "• Tài khoản đang bị khóa\n" +
                                  "• Lỗi kết nối cơ sở dữ liệu\n\n" +
                                  "Vui lòng kiểm tra lại thông tin hoặc liên hệ quản trị viên.",
                                    "Lỗi đăng nhập",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    txtMatKhau.Clear();
                    txtTenDangNhap.Focus();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ UC Exception: {ex.Message}");
                MessageBox.Show($"Đã xảy ra lỗi trong quá trình đăng nhập:\n\n{ex.Message}\n\n" +
                              $"Vui lòng thử lại hoặc liên hệ quản trị viên.",
                                "Lỗi hệ thống",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            finally
            {
                // Re-enable nút
                btnXacNhan.Enabled = true;
                btnXacNhan.Text = "Xác Nhận";
            }
        }
    }
}