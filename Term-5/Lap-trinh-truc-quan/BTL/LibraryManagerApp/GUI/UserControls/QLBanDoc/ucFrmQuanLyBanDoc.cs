// File: LibraryManagerApp.GUI.UserControls.QLBanDoc/ucFrmQuanLyBanDoc.cs

using LibraryManagerApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagerApp.GUI.UserControls.QLBanDoc
{
    public partial class ucFrmQuanLyBanDoc : UserControl
    {
        private Button currentActiveButton;

        public ucFrmQuanLyBanDoc()
        {
            InitializeComponent();

            // >>> BỔ SUNG: Ép kiểu giao diện mặc định (Inactive) cho tất cả các nút ngay khi khởi tạo
            // Điều này đảm bảo dù Designer bạn chỉnh màu gì, khi chạy lên nó sẽ đồng bộ theo code
            SetDefaultButtonStyle(btnThongTinBanDoc);
            SetDefaultButtonStyle(btnTheBanDoc);
        }

        // Hàm hỗ trợ thiết lập style mặc định (Inactive)
        private void SetDefaultButtonStyle(Button btn)
        {
            btn.BackColor = Color.WhiteSmoke; // Màu nền nhạt
            btn.ForeColor = Color.FromArgb(48, 52, 129); // Màu chữ xanh đậm
            btn.Font = new Font("Consolas", 12F, FontStyle.Regular); // Font thường
        }

        private void LoadSubUserControl(UserControl uc)
        {
            this.pnlContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;

            // --- BỔ SUNG: Đăng ký sự kiện ---

            // Kiểm tra xem UC con có phải là loại ucFrmThongTinBanDoc không
            if (uc is ucFrmThongTinBanDoc ucInfo)
            {
                ucInfo.OnStatusRequest += Child_OnStatusRequest;
            }
            // Kiểm tra xem UC con có phải là loại ucFrmTheBanDoc không
            else if (uc is ucFrmTheBanDoc ucCard)
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
            lblTieuDe.Text = e.TitleText;
            lblTieuDe.ForeColor = e.ForeColor;
        }

        private void btnThongTinBanDoc_Click(object sender, EventArgs e)
        {
            LoadSubUserControl(new ucFrmThongTinBanDoc());
            SetActiveButton(btnThongTinBanDoc);
        }

        private void btnTheBanDoc_Click(object sender, EventArgs e)
        {
            LoadSubUserControl(new ucFrmTheBanDoc());
            SetActiveButton(btnTheBanDoc);
        }

        private void ucFrmQuanLyBanDoc_Load(object sender, EventArgs e)
        {
            // Kích hoạt nút đầu tiên. 
            // Lúc này btnTheBanDoc đã được set style Inactive ở Constructor nên sẽ hiển thị đúng.
            btnThongTinBanDoc_Click(btnThongTinBanDoc, EventArgs.Empty);
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