using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Bai9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Sach> dsSach = new List<Sach>()
            {
                new Sach{ tenSach="Tin đại cương", giaTien="22000"},
                new Sach{ tenSach="Tiếng Anh F2", giaTien="27000"},
                new Sach{ tenSach="Giải tích F1", giaTien="25000"},
                new Sach{ tenSach="Đại số tuyến tính", giaTien="26000"}
            };

            cbSach.DataSource = dsSach;
            cbSach.DisplayMember = "tenSach";
            cbSach.ValueMember = "giaTien";

            resetForm();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void cbSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sach selected = cbSach.SelectedItem as Sach;
            if (selected != null)
            {
                txtGia.Text = selected.giaTien;
            }
        }

        private void rdb_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbATM.Checked)
            {
                txtGiamGia.Text = "10";
            }
            else if (rdbTienMat.Checked)
            {
                txtGiamGia.Text = "5";
            }
            else
            {
                txtGiamGia.Text = "0";
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!validateForm())
            {
                return; 
            }

            string tenSach = cbSach.Text;
            int gia = int.Parse(txtGia.Text);
            int soLuong = int.Parse(txtSL.Text);
            int giamGia = int.Parse(txtGiamGia.Text);

            double thanhTien = soLuong * gia * (1 - giamGia / 100.0);

            string thongTin = $"TS - {tenSach}, SL - {soLuong}, TT - {thanhTien}";

            lstSachDaMua.Items.Add(thongTin);

            resetForm();
        }

        private void resetForm()
        {
            txtSL.Clear();
            cbSach.SelectedIndex = -1;
            cbSach.Focus();
            txtGia.Clear();
        }

        private bool validateForm()
        {
            if (cbSach.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn sách!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbSach.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSL.Text))
            {
                MessageBox.Show("Vui lòng nhập số lượng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSL.Focus();
                return false;
            }

            if (!int.TryParse(txtSL.Text, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSL.Focus();
                return false;
            }

            if (!rdbTienMat.Checked && !rdbATM.Checked)
            {
                MessageBox.Show("Vui lòng chọn phương thức thanh toán!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lstSachDaMua.SelectedIndex >= 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa mục đã chọn không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    lstSachDaMua.Items.RemoveAt(lstSachDaMua.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn mục cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTongTien_Click(object sender, EventArgs e)
        {
            double tongTien = 0;

            foreach (var item in lstSachDaMua.Items)
            {
                string[] parts = item.ToString().Split(',');
                foreach (var part in parts)
                {
                    if (part.Trim().StartsWith("TT -"))
                    {
                        string value = part.Replace("TT -", "").Trim();
                        if (double.TryParse(value, out double thanhTien))
                        {
                            tongTien += thanhTien;
                        }
                    }
                }
            }

            txtTongTien.Text = tongTien.ToString("N0");
        }
    }
}
