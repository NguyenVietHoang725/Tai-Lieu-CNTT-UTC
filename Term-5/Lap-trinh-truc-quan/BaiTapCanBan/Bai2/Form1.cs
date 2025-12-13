using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbThoiGianGui.Items.Add("1");
            cbThoiGianGui.Items.Add("3");
            cbThoiGianGui.Items.Add("6");
            cbThoiGianGui.Items.Add("12");

            resetForm();
        }

        private void resetForm()
        {
            txtMaKH.Text = "";
            txtHoTen.Text = "";
            txtDiaChi.Text = "";
            txtSoTienGui.Text = "";
            dtpNgayGui.Value = DateTime.Now;
            cbThoiGianGui.SelectedIndex = -1;
            rdbPhatLoc.Checked = false;
            rdbThuong.Checked = false;
        }

        private void txtMaKH_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateNhapLieu(txtMaKH))
            {
                e.Cancel = true; 
            }
        }

        private void txtSoTienGui_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateNhapLieu(txtSoTienGui))
            {
                e.Cancel = true;
            }
        }

        private bool ValidateNhapLieu(TextBox txt)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                MessageBox.Show("Không được để trống");
                return false;
            }

            if (!int.TryParse(txt.Text, out int value) || value < 0)
            {
                MessageBox.Show("Dữ liệu phải là số nguyên dương");
                return false;
            }

            return true;
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnThemDS_Click(object sender, EventArgs e)
        {

        }
    }
}
