using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            txtMaKH.Text = "";
            txtHoTenKH.Text = "";
            txtDiaChi.Text = "";
            dtpNgayChotSo.Text = DateTime.Now.ToString();
            txtSoThangTruoc.Text = "";
            txtSoThangNay.Text = "";
            txtMaKH.Focus();
        }

        private void validateSoThang(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnThemVaoDS_Click(object sender, EventArgs e)
        {

        }

        private void validateKH(object sender, CancelEventArgs e)
        {
            // 1. Mã khách hàng đủ 6 ký tự
            if (txtMaKH.Text.Trim().Length != 6)
            {
                MessageBox.Show("Mã khách hàng phải đủ 6 ký tự.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaKH.Focus();
                e.Cancel = true;
                return;
            }

            // 2. Họ tên không được rỗng
            if (string.IsNullOrWhiteSpace(txtHoTenKH.Text))
            {
                MessageBox.Show("Họ tên khách hàng không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHoTenKH.Focus();
                e.Cancel = true;
                return;
            }

            // 3. Địa chỉ không được rỗng
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Địa chỉ khách hàng không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDiaChi.Focus();
                e.Cancel = true;
                return;
            }

            // 4. Ngày chốt số hợp lệ
            DateTime ngayChot;
            if (!DateTime.TryParse(dtpNgayChotSo.Text, out ngayChot))
            {
                MessageBox.Show("Ngày chốt số không hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpNgayChotSo.Focus();
                e.Cancel = true;
                return;
            }

            // 5. Số tháng trước < Số tháng này
            int soThangTruoc, soThangNay;
            if (!int.TryParse(txtSoThangTruoc.Text, out soThangTruoc) ||
                !int.TryParse(txtSoThangNay.Text, out soThangNay))
            {
                MessageBox.Show("Số tháng phải là số nguyên.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            if (soThangTruoc >= soThangNay)
            {
                MessageBox.Show("Số tháng trước phải nhỏ hơn số tháng này.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoThangTruoc.Focus();
                e.Cancel = true;
                return;
            }
        }

    }
}
