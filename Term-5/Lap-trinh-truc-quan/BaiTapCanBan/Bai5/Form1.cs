using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai5
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

        private void resetForm()
        {
            resetButton();

            txtNhapSo.Clear();
            txtNhapSo.Focus();
            lstDaySo.Items.Clear();

            lblTong.Visible = false;
            lblMax.Visible = false;
            lblMin.Visible = false;
        }

        private void resetButton()
        {
            btnTong.Enabled = false;
            btnMax.Enabled = false;
            btnMin.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!validateNhapSo())
            {
                return;
            }

            lstDaySo.Items.Add(txtNhapSo.Text);
            txtNhapSo.Clear();
            txtNhapSo.Focus();

            btnXoa.Enabled = true;
            btnTong.Enabled = true;
            btnMax.Enabled = true;
            btnMin.Enabled = true;
        }

        private bool validateNhapSo()
        {
            if (string.IsNullOrWhiteSpace(txtNhapSo.Text))
            {
                MessageBox.Show("Vui lòng nhập số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNhapSo.Focus();
                return false;
            }

            if (!int.TryParse(txtNhapSo.Text, out _))
            {
                MessageBox.Show("Vui lòng nhập số hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNhapSo.Focus();
                return false;
            }

            return true;
        }

        private void btnTong_Click(object sender, EventArgs e)
        {
            int sum = 0;
            foreach (var item in lstDaySo.Items)
            {
                sum += int.Parse(item.ToString());
            }
            lblTong.Visible = true;
            lblTong.Text = "Tổng: " + sum;
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            int max = int.MinValue;
            foreach (var item in lstDaySo.Items)
            {
                int number = int.Parse(item.ToString());
                if (number > max)
                {
                    max = number;
                }
            }
            lblMax.Visible = true;
            lblMax.Text = "Max: " + max;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            int min = int.MaxValue; 
            foreach (var item in lstDaySo.Items)
            {
                int number = int.Parse(item.ToString());
                if (number < min)
                {
                    min = number;
                }
            }
            lblMin.Visible = true;
            lblMin.Text = "Min: " + min;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lstDaySo.SelectedItem != null)
            {
                lstDaySo.Items.Remove(lstDaySo.SelectedItem);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn số cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (lstDaySo.Items.Count == 0)
            {
                resetButton();
            }
        }
    }
}
