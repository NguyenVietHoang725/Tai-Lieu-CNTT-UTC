using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai6
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
            btnXoa.Enabled = false;
            lblLonNhat.Visible = false;
            txtNhapSo.Text = "";

            lstDaySo.Items.Clear();
            lstDaySo.Items.Add("4");
            lstDaySo.Items.Add("46.8");
            lstDaySo.Items.Add("95");
        }

        private void btnLamLai_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!validateNhapSo())
            {
                return;
            }

            lstDaySo.Items.Add(txtNhapSo.Text);
            txtNhapSo.Text = "";
            txtNhapSo.Focus();
        }

        private bool validateNhapSo()
        {
            if (string.IsNullOrWhiteSpace(txtNhapSo.Text))
            {
                MessageBox.Show("Vui lòng nhập số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNhapSo.Focus();
                return false;
            }

            if (!double.TryParse(txtNhapSo.Text, out _))
            {
                MessageBox.Show("Vui lòng nhập số hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNhapSo.Focus();
                return false;
            }

            return true;
        }

        private void lstDaySo_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnXoa.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa số đã chọn?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (lstDaySo.SelectedItem != null)
                {
                    lstDaySo.Items.Remove(lstDaySo.SelectedItem);
                }
                btnXoa.Enabled = false;
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            double max = double.MinValue;
            foreach (var item in lstDaySo.Items)
            {
                if (double.TryParse(item.ToString(), out double number))
                {
                    if (number > max)
                    {
                        max = number;
                    }
                }
            }
            lblLonNhat.Visible = true;  
            lblLonNhat.Text = "Giá trị lớn nhất của dãy là: " + max;
        }
    }
}
