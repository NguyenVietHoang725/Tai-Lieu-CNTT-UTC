// File: LibraryManagerApp.GUI.UserControls.QLPhanQuyen/ucFrmQuanLyPhanQuyen.cs

using LibraryManagerApp.DAL;
using LibraryManagerApp.Helpers; // Để dùng StatusRequestEventArgs (nếu cần)
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagerApp.GUI.UserControls.QLPhanQuyen
{
    public partial class ucFrmQuanLyPhanQuyen : UserControl
    {
        private Button currentActiveButton;

        public ucFrmQuanLyPhanQuyen()
        {
            InitializeComponent();

            SetDefaultButtonStyle(btnThongTinNhanVien);
            SetDefaultButtonStyle(btnThongTinTaiKhoan);

        }

        // Hàm hỗ trợ thiết lập style mặc định (Inactive)
        private void SetDefaultButtonStyle(Button btn)
        {
            btn.BackColor = Color.WhiteSmoke; // Màu nền nhạt
            btn.ForeColor = Color.FromArgb(48, 52, 129); // Màu chữ xanh đậm
            btn.Font = new Font("Consolas", 12F, FontStyle.Regular); // Font thường
        }

        private void ucFrmQuanLyPhanQuyen_Load(object sender, EventArgs e)
        {
            // Kích hoạt mặc định nút "Thông tin nhân viên"
            btnThongTinNhanVien_Click(btnThongTinNhanVien, EventArgs.Empty);
        }

        private void LoadSubUserControl(UserControl uc)
        {
            this.pnlContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;

            // --- BỔ SUNG: Đăng ký sự kiện đổi tiêu đề (Tương tự Bạn Đọc) ---
            if (uc is ucFrmThongTinNhanVien ucNV)
            {
                ucNV.OnStatusRequest += Child_OnStatusRequest;
            }
            else if (uc is ucFrmThongTinTaiKhoan ucTK)
            {
                ucTK.OnStatusRequest += Child_OnStatusRequest;
            }

            this.pnlContent.Controls.Add(uc);
            uc.BringToFront();
        }

        // Hàm xử lý sự kiện đổi tiêu đề
        private void Child_OnStatusRequest(object sender, StatusRequestEventArgs e)
        {
            // Cập nhật giao diện của UC Cha
            pnlTitle.BackColor = e.BackColor;
            label1.Text = e.TitleText;
            label1.ForeColor = e.ForeColor;
        }

        private void btnThongTinNhanVien_Click(object sender, EventArgs e)
        {
            LoadSubUserControl(new ucFrmThongTinNhanVien());
            SetActiveButton(btnThongTinNhanVien);
        }

        private void btnThongTinTaiKhoan_Click(object sender, EventArgs e)
        {
            LoadSubUserControl(new ucFrmThongTinTaiKhoan());
            SetActiveButton(btnThongTinTaiKhoan);
        }

        // --- PHẦN ĐIỀU CHỈNH MÀU SẮC (Inverted Colors) ---
        private void SetActiveButton(Button activeButton)
        {
            if (currentActiveButton != null && currentActiveButton != activeButton)
            {
                // Inactive: Nền Xám nhạt, Chữ Xanh đậm
                currentActiveButton.BackColor = Color.WhiteSmoke;
                currentActiveButton.ForeColor = Color.FromArgb(48, 52, 129);
                currentActiveButton.Font = new Font("Consolas", 12F, FontStyle.Regular);
            }

            currentActiveButton = activeButton;

            if (currentActiveButton != null)
            {
                // Active: Nền Xanh đậm, Chữ Trắng
                currentActiveButton.BackColor = Color.FromArgb(48, 52, 129);
                currentActiveButton.ForeColor = Color.White;
                currentActiveButton.Font = new Font("Consolas", 12F, FontStyle.Bold);
            }
        }
    }
}