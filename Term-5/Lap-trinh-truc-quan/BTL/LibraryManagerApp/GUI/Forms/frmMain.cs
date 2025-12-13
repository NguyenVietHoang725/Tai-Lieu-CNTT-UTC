using LibraryManagerApp.DTO;
using LibraryManagerApp.GUI.UserControls;
using LibraryManagerApp.GUI.UserControls.DangNhap;
using LibraryManagerApp.GUI.UserControls.QLBanDoc;
using LibraryManagerApp.GUI.UserControls.QLMuonTra;
using LibraryManagerApp.GUI.UserControls.QLPhanQuyen;
using LibraryManagerApp.GUI.UserControls.QLTaiLieu;
using LibraryManagerApp.GUI.UserControls.ThongKeBaoCao;
using LibraryManagerApp.GUI.UserControls.TrangChu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagerApp
{
    public partial class frmMain : Form
    {
        private Button currentActiveButton;
        private ucFrmDangNhap _loginUC;
        private LoginSessionDTO _currentUserSession;

        public frmMain()
        {
            InitializeComponent();

            btnTrangChu.Tag = "trangchu";
            btnQLBanDoc.Tag = "bandoc";
            btnQLTaiLieu.Tag = "tailieu";
            btnQLMuonTra.Tag = "muontra";
            btnQLPhanQuyen.Tag = "phanquyen";
            btnThongKeBaoCao.Tag = "thongke";

            // 1. Thiết lập trạng thái ban đầu cho Menu (Ẩn)
            SetMenuVisibility(false);

            // 2. Tải UC Đăng nhập khi khởi động (phải sau khi form đã load xong)
            this.Load += FrmMain_Load;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Hiển thị màn hình đăng nhập sau khi form đã load
            ShowLoginScreen();
        }

        private void ShowLoginScreen()
        {
            // Tạo instance mới của UC đăng nhập
            _loginUC = new ucFrmDangNhap();

            // Đăng ký Event để lắng nghe khi đăng nhập thành công
            _loginUC.OnLoginSuccess += HandleLoginSuccess;

            // Thiết lập Dock trước khi add vào container
            _loginUC.Dock = DockStyle.Fill;

            // Tạm thời suspend layout để tránh flicker
            tlpMain.SuspendLayout();

            // Add UC vào tlpMain tại vị trí (0, 0)
            tlpMain.Controls.Add(_loginUC, 0, 0);

            // Set column span để UC kéo dài qua cả 2 cột
            tlpMain.SetColumnSpan(_loginUC, 2);

            // Đưa UC lên trên cùng (Z-order)
            _loginUC.BringToFront();

            // Resume layout
            tlpMain.ResumeLayout(true);
        }

        // Xử lý khi đăng nhập thành công
        private void HandleLoginSuccess(LoginSessionDTO userSession)
        {
            try
            {
                // Lưu session
                _currentUserSession = userSession;

                // Suspend layout để tránh flicker
                tlpMain.SuspendLayout();

                // 1. Hủy đăng ký event để tránh memory leak
                if (_loginUC != null)
                {
                    _loginUC.OnLoginSuccess -= HandleLoginSuccess;
                }

                // 2. Ẩn và loại bỏ UC Đăng nhập khỏi TLPMain
                tlpMain.Controls.Remove(_loginUC);

                // 3. Dispose UC để giải phóng tài nguyên
                _loginUC?.Dispose();
                _loginUC = null;

                // 4. Hiện Menu và áp dụng Phân quyền
                SetMenuVisibility(true);

                // Resume layout
                tlpMain.ResumeLayout(true);

                // 5. Chuyển sang Trang Chủ (Load UC Trang Chủ vào pnlContent)
                btnTrangChu_Click(btnTrangChu, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chuyển màn hình: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Hàm ẩn/hiện toàn bộ panel Menu
        private void SetMenuVisibility(bool visible)
        {
            panelMenu.Visible = visible;
        }

        // Hàm phân quyền (Dựa trên MaVT)
        private void ApplyPermissions(string maVT)
        {
            // Mặc định tắt tất cả các nút (trừ Trang Chủ)
            btnQLBanDoc.Visible = false;
            btnQLTaiLieu.Visible = false;
            btnQLMuonTra.Visible = false;
            btnQLPhanQuyen.Visible = false;
            btnThongKeBaoCao.Visible = false;

            // Trang Chủ luôn hiện
            btnTrangChu.Visible = true;

            // Logic phân quyền cơ bản:
            switch (maVT?.ToUpper())
            {
                case "QTV": // Quản trị viên: Toàn quyền
                    btnQLBanDoc.Visible = true;
                    btnQLTaiLieu.Visible = true;
                    btnQLMuonTra.Visible = true;
                    btnQLPhanQuyen.Visible = true;
                    btnThongKeBaoCao.Visible = true;
                    break;

                case "QLB": // Quản lý bạn đọc
                    btnQLBanDoc.Visible = true;
                    btnThongKeBaoCao.Visible = true;
                    break;

                case "QLT": // Quản lý tài liệu
                    btnQLTaiLieu.Visible = true;
                    btnThongKeBaoCao.Visible = true;
                    break;

                case "QLM": // Quản lý mượn trả
                    btnQLMuonTra.Visible = true;
                    btnThongKeBaoCao.Visible = true;
                    break;

                default:
                    // Chỉ hiển thị Trang Chủ cho các vai trò không xác định
                    break;
            }

            // Refresh panel để cập nhật UI
            panelMenu.Refresh();
        }

        private void SetActiveButton(Button activeButton)
        {
            string newButtonTag = activeButton.Tag as string;

            if (currentActiveButton != null)
            {
                string oldButtonTag = currentActiveButton.Tag as string;

                // Reset trạng thái nút cũ
                currentActiveButton.BackColor = Color.FromArgb(48, 52, 129);
                currentActiveButton.ForeColor = Color.FromArgb(255, 242, 0);

                if (!string.IsNullOrEmpty(oldButtonTag))
                {
                    try
                    {
                        // Tên hình ảnh cũ: "icon_{Tag}_default" hoặc "icon_{Tag}"
                        var oldImage = Properties.Resources.ResourceManager.GetObject($"icon_{oldButtonTag}");
                        if (oldImage != null)
                        {
                            currentActiveButton.Image = (Image)oldImage;
                        }
                    }
                    catch
                    {
                        // Bỏ qua nếu không tìm thấy icon
                    }
                }
            }

            currentActiveButton = activeButton;

            // Set trạng thái Active cho nút mới
            currentActiveButton.BackColor = Color.FromArgb(214, 230, 242);
            currentActiveButton.ForeColor = Color.FromArgb(48, 52, 129);

            if (!string.IsNullOrEmpty(newButtonTag))
            {
                try
                {
                    // Tên hình ảnh mới: "icon_{Tag}_active"
                    var newImage = Properties.Resources.ResourceManager.GetObject($"icon_{newButtonTag}_active");
                    if (newImage != null)
                    {
                        currentActiveButton.Image = (Image)newImage;
                    }
                }
                catch
                {
                    // Bỏ qua nếu không tìm thấy icon
                }
            }
        }

        private void LoadUserControl(UserControl uc)
        {
            try
            {
                // Suspend layout để tránh flicker
                pnlContent.SuspendLayout();

                // Clear các control cũ và dispose chúng
                foreach (Control ctrl in pnlContent.Controls)
                {
                    ctrl.Dispose();
                }
                pnlContent.Controls.Clear();

                // Thiết lập UC mới
                uc.Dock = DockStyle.Fill;
                pnlContent.Controls.Add(uc);
                uc.BringToFront();

                // Resume layout
                pnlContent.ResumeLayout(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải giao diện: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnTrangChu);

            // Tạo instance của ucFrmTrangChu
            var ucTrangChu = new ucFrmTrangChu();

            // Truyền thông tin user session vào
            if (_currentUserSession != null)
            {
                ucTrangChu.SetUserInfo(_currentUserSession);
            }

            // Load UserControl
            LoadUserControl(ucTrangChu);
        }

        private void btnQLBanDoc_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnQLBanDoc);
            LoadUserControl(new ucFrmQuanLyBanDoc());
        }

        private void btnQLTaiLieu_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnQLTaiLieu);
            LoadUserControl(new ucFrmQuanLyTaiLieu());
        }

        private void btnQLMuonTra_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnQLMuonTra);
            LoadUserControl(new ucFrmQuanLyMuonTra());
        }

        private void btnQLPhanQuyen_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnQLPhanQuyen);
            LoadUserControl(new ucFrmQuanLyPhanQuyen());
        }

        private void btnThongKeBaoCao_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnThongKeBaoCao);
            LoadUserControl(new ucFrmThongKeBaoCao());
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Cleanup khi đóng form
            if (_loginUC != null)
            {
                _loginUC.OnLoginSuccess -= HandleLoginSuccess;
                _loginUC.Dispose();
            }

            base.OnFormClosing(e);
        }
    }
}