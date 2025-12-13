using LibraryManagerApp.DAL;
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

namespace LibraryManagerApp.GUI.UserControls.QLTaiLieu
{
    public partial class ucFrmQuanLyTaiLieu : UserControl
    {
        private Button currentActiveButton;

        public ucFrmQuanLyTaiLieu()
        {
            InitializeComponent();

            SetDefaultButtonStyle(btnThongTinTaiLieu);
            SetDefaultButtonStyle(btnThongTinDanhMuc);
        }

        // Hàm hỗ trợ thiết lập style mặc định (Inactive)
        private void SetDefaultButtonStyle(Button btn)
        {
            btn.BackColor = Color.WhiteSmoke; // Màu nền nhạt
            btn.ForeColor = Color.FromArgb(48, 52, 129); // Màu chữ xanh đậm
            btn.Font = new Font("Consolas", 12F, FontStyle.Regular); // Font thường
        }

        private void ucFrmQuanLyTaiLieu_Load(object sender, EventArgs e)
        {
            btnThongTinTaiLieu_Click(btnThongTinTaiLieu, EventArgs.Empty);
        }

        private void LoadSubUserControl(UserControl uc)
        {
            this.pnlContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;

            // Kiểm tra xem UC con có phải là loại ucFrmThongTinDanhMuc không
            if (uc is ucFrmThongTinDanhMuc ucInfo)
            {
                ucInfo.OnStatusRequest += Child_OnStatusRequest;
            }
            // Kiểm tra xem UC con có phải là loại ucFrmThongTinTaiLieu không
            else if (uc is ucFrmThongTinTaiLieu ucCard)
            {
                ucCard.OnStatusRequest += Child_OnStatusRequest;
            }

            this.pnlContent.Controls.Add(uc);
            uc.BringToFront();
        }

        // Hàm xử lý sự kiện chung
        private void Child_OnStatusRequest(object sender, StatusRequestEventArgs e)
        {
            // Cập nhật giao diện của UC Cha (Panel Title và Label)
            //
            pnlTitle.BackColor = e.BackColor;
            label1.Text = e.TitleText;
            label1.ForeColor = e.ForeColor;
        }


        private void btnThongTinTaiLieu_Click(object sender, EventArgs e)
        {
            LoadSubUserControl(new ucFrmThongTinTaiLieu());
            SetActiveButton(btnThongTinTaiLieu);
        }

        private void btnThongTinDanhMuc_Click(object sender, EventArgs e)
        {
            LoadSubUserControl(new ucFrmThongTinDanhMuc());
            SetActiveButton(btnThongTinDanhMuc);
        }

        private void SetActiveButton(Button activeButton)
        {
            // 1. Reset nút cũ (nếu có) về trạng thái Inactive
            if (currentActiveButton != null && currentActiveButton != activeButton)
            {
                SetDefaultButtonStyle(currentActiveButton); // Tái sử dụng hàm style mặc định
            }

            // 2. Gán nút hiện tại
            currentActiveButton = activeButton;

            // 3. Cấu hình nút Active (Màu đậm nổi bật)
            if (currentActiveButton != null)
            {
                currentActiveButton.BackColor = Color.FromArgb(48, 52, 129); // Nền xanh đậm
                currentActiveButton.ForeColor = Color.White; // Chữ trắng
                currentActiveButton.Font = new Font("Consolas", 12F, FontStyle.Bold); // Chữ đậm
            }
        }
    }
}
