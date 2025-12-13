using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TinhLaiSuatTienGuiTietKiem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AddComboBox();
            this.KeyPreview = true;
        }

        private void AddComboBox()
        {
            cbThoiGianGui.Items.Add("1");
            cbThoiGianGui.Items.Add("3");
            cbThoiGianGui.Items.Add("6");
            cbThoiGianGui.Items.Add("12");
        }

        private void txtMaKH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtDiaChi.Text = "";
            txtSoTienGui.Text = "";
            cbThoiGianGui.Text = "";
            cbThoiGianGui.SelectedIndex = -1;
            rdoThuong.Checked = false;
            rdoPhatLoc.Checked = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnThemVaoDS_Click(object sender, EventArgs e)
        {
            int kt = 1;
            if (txtMaKH.TextLength < 6)
            {
                MessageBox.Show("Mã khách hàng phải có ít nhất 6 ký tự.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                kt = 0;
            }

            if (txtDiaChi.TextLength == 0 || txtTenKH.TextLength == 0)
            {
                MessageBox.Show("Tên khách hàng và địa chỉ không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                kt = 0;
            }

            double soTienLai = 0;
            if (kt == 1)
            {
                if (rdoThuong.Checked == true)
                {
                    if (cbThoiGianGui.SelectedItem == "1")
                    {
                        soTienLai = Convert.ToInt32(txtSoTienGui.Text) * 0.06;
                    }
                    if (cbThoiGianGui.SelectedItem == "3")
                    {
                        soTienLai = Convert.ToInt32(txtSoTienGui.Text) * 0.07;
                    }
                    if (cbThoiGianGui.SelectedItem == "6")
                    {
                        soTienLai = Convert.ToInt32(txtSoTienGui.Text) * 0.08;
                    }
                    if (cbThoiGianGui.SelectedItem == "12")
                    {
                        soTienLai = Convert.ToInt32(txtSoTienGui.Text) * 0.09;
                    }
                } 
                else if (rdoPhatLoc.Checked == true)
                {
                    if (cbThoiGianGui.SelectedItem == "1")
                    {
                        soTienLai = Convert.ToInt32(txtSoTienGui.Text) * 0.07;
                    }
                    if (cbThoiGianGui.SelectedItem == "3")
                    {
                        soTienLai = Convert.ToInt32(txtSoTienGui.Text) * 0.08;
                    }
                    if (cbThoiGianGui.SelectedItem == "6")
                    {
                        soTienLai = Convert.ToInt32(txtSoTienGui.Text) * 0.09;
                    }
                    if (cbThoiGianGui.SelectedItem == "12")
                    {
                        soTienLai = Convert.ToInt32(txtSoTienGui.Text) * 0.1;
                    }
                }
                lstDanhSach.Items.Add(txtMaKH.Text + " | " + txtTenKH.Text + " | " + txtDiaChi.Text + " | " + dtpNgayGui.Text + " | " + txtSoTienGui.Text + " | " + cbThoiGianGui.SelectedItem + " tháng | " + soTienLai.ToString("N0") + " VND" );

                StaticData._NguoiGui.Add(new NguoiGui(
                    Convert.ToInt32(txtMaKH.Text),
                    txtTenKH.Text,
                    txtDiaChi.Text,
                    Convert.ToInt32(txtSoTienGui.Text),
                    dtpNgayGui.Text,
                    cbThoiGianGui.Text,
                    soTienLai
                ));
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
