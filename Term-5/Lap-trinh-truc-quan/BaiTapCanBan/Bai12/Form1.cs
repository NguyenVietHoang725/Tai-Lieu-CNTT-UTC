using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            resetForm();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void resetForm()
        {
            txtNhapSo.Text = "";
            lstDaySo.Items.Clear();
            lblKetQua.Visible = false;
        }

        private void resetNhapSo()
        {
            txtNhapSo.Text = "";
            txtNhapSo.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNhapSo.Text))
            {
                MessageBox.Show("Bạn phải nhập số vào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNhapSo.Focus();
                return;
            }

            if (!int.TryParse(txtNhapSo.Text, out int n))
            {
                MessageBox.Show("Giá trị nhập không hợp lệ, vui lòng nhập số tự nhiên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                resetNhapSo();
                return;
            }

            if (n < 0)
            {
                MessageBox.Show("Vui lòng nhập số tự nhiên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                resetNhapSo();
                return;
            }

            lstDaySo.Items.Add(n);

            resetNhapSo();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lstDaySo.Items.Count < 0)
            {
                MessageBox.Show("Danh sách rỗng, không thể xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lstDaySo.SelectedIndex < 0)
            {
                MessageBox.Show("Bạn phải chọn số cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lstDaySo.Items.RemoveAt(lstDaySo.SelectedIndex);
        }

        private void btnKiemTra_Click(object sender, EventArgs e)
        {
            if (lstDaySo.Items.Count < 0)
            {
                MessageBox.Show("Danh sách rỗng, không thể kiểm tra!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lstDaySo.SelectedIndex < 0)
            {
                MessageBox.Show("Bạn phải chọn số cần kiểm tra!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int number = (int)lstDaySo.SelectedItem;

            if (isPrime(number))
            {
                lblKetQua.Text = $"{number} là số nguyên tố.";
            }
            else
            {
                lblKetQua.Text = $"{number} không phải là số nguyên tố.";
            }

            lblKetQua.Visible = true;
        }

        private bool isPrime(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i <= Math.Sqrt(n); i++)
            {
                if (n % i == 0) return false;
            }
            return true;
        }
    }
}
