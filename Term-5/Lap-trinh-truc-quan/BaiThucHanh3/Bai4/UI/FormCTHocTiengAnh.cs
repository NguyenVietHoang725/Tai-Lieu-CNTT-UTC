using Bai4.Data;

using Bai4.Services;
using System;
using System.Collections.Generic;

using System.Windows.Forms;

namespace Bai4
{
    public partial class FormCTHocTiengAnh : Form
    {

        private List<BaiTapDienTu> danhSachBaiTap;

        public FormCTHocTiengAnh()
        {
            InitializeComponent();
            KhoiTaoMenu();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void KhoiTaoMenu()
        {
            try
            {
                danhSachBaiTap = BaiTapDienTuService.LoadFromFile("baitap.txt");

                // Tạo menu chính
                var menuStrip = new MenuStrip();
                var menuBaiTap = new ToolStripMenuItem("Bài tập");
                var menuDienTu = new ToolStripMenuItem("Điền từ");

                // Thêm từng bài con
                for (int i = 0; i < danhSachBaiTap.Count; i++)
                {
                    int index = i; // cần biến riêng để không bị capture sai
                    var item = new ToolStripMenuItem($"Điền từ {i + 1}");
                    item.Click += (s, e) => MoBaiTap(index);
                    menuDienTu.DropDownItems.Add(item);
                }

                menuBaiTap.DropDownItems.Add(menuDienTu);
                menuStrip.Items.Add(menuBaiTap);

                this.MainMenuStrip = menuStrip;
                this.Controls.Add(menuStrip);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load menu: " + ex.Message);
            }
        }

        private void MoBaiTap(int index)
        {
            if (index < 0 || index >= danhSachBaiTap.Count)
            {
                MessageBox.Show("Bài tập không tồn tại!");
                return;
            }

            var bai = danhSachBaiTap[index];
            var form = new FormBaiTapDienTu(bai, $"Điền từ {index + 1}");
            form.ShowDialog();
        }
    }
}
