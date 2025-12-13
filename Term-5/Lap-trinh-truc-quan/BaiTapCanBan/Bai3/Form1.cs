using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            List<DoUong> doUongs = new List<DoUong>()
            {
                new DoUong() { tenDoUong = "Coca cola", gia = 0.5 },
                new DoUong() { tenDoUong = "Pepsi", gia = 0.8 },
                new DoUong() { tenDoUong = "Seven up", gia = 1.0 }
            };

            cbDoUong.DataSource = doUongs;
            cbDoUong.DisplayMember = "tenDoUong";
            cbDoUong.ValueMember = "gia";

            for (int i = 1; i <= 10; i++)
            {
                cbSL.Items.Add(i);
            }

            resetForm();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dlg = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dlg == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void resetForm()
        {
            txtHoTen.Text = "";
            txtHoTen.Focus();
            cbDoUong.SelectedIndex = -1;
            cbSL.SelectedIndex = -1;
            txtTien.Text = "";
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void rdb_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCaNgay.Checked)
            {
                txtGiaThuyen.Text = "200";
            }
            else if (rdbNuaNgay.Checked)
            {
                txtGiaThuyen.Text = "100";
            }
            else
            {
                txtGiaThuyen.Text = "";
            }
        }
     
        private double tinhTongTienDoUong(double _gia, double _sl)
        {
            double tongTien = 0;

            tongTien = _gia * _sl;

            return tongTien;
        }

        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDoUong.SelectedIndex == -1 || cbSL.SelectedIndex == -1)
            {
                txtTien.Text = "";
                return;
            }

            double gia = Convert.ToDouble(cbDoUong.SelectedValue);
            double sl = Convert.ToDouble(cbSL.SelectedItem);

            double tongTienDoUong = tinhTongTienDoUong(gia, sl);
            txtTien.Text = tongTienDoUong.ToString("0.00");
        }

        private void btnThemDS_Click(object sender, EventArgs e)
        {
            if (!validateForm()) return;

            string hoTen = txtHoTen.Text;
            string loaiThuyen = rdbCaNgay.Checked ? "Cả ngày" : rdbNuaNgay.Checked ? "Nửa ngày" : "";
            double giaThuyen = Convert.ToDouble(txtGiaThuyen.Text);

            double tienDoUong = 0;
            if (!string.IsNullOrWhiteSpace(txtTien.Text))
            {
                double.TryParse(txtTien.Text, out tienDoUong);
            }

            double tongTien = giaThuyen + tienDoUong;

            string thongTin = $"{hoTen}|{loaiThuyen} {giaThuyen}|Đồ uống {tienDoUong}$|Tổng {tongTien}";
            lstDSKH.Items.Add(thongTin);
        }

        private bool validateForm()
        {
            if (string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Họ tên không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHoTen.Focus();
                return false;
            }

            return true;
        }
    }
}
