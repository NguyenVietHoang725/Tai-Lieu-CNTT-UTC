using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Bai7
{
    public partial class Form1 : Form
    {
        private double[] daySo;
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
            txtNhapSoPT.Clear();
            txtNhapSoPT.Focus();
            cbChucNang.SelectedIndex = -1;
            lblKetQua.Enabled = false;
            lblDaySo.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLamLai_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void btnNhapDay_Click(object sender, EventArgs e)
        {
            if (!validateSoPhanTu())
            {
                return;
            }

            int n = int.Parse(txtNhapSoPT.Text);
            daySo = new double[n];

            for (int i = 0; i < n; i++)
            {
                string input;
                double value;

                do
                {
                    input = Microsoft.VisualBasic.Interaction.InputBox(
                        $"Nhập phần tử thứ {i + 1}:",
                        "Nhập dãy số",
                        "0"
                    );
                }
                while (!double.TryParse(input, out value));

                daySo[i] = value;
            }

            lblDaySo.Text = "Dãy số vừa nhập: " + string.Join(", ", daySo);
            lblDaySo.Visible = true;
        }

        private bool validateSoPhanTu()
        {
            if (string.IsNullOrWhiteSpace(txtNhapSoPT.Text))
            {
                MessageBox.Show("Vui lòng nhập số phần tử.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNhapSoPT.Text = "";
                txtNhapSoPT.Focus();
                return false;
            }

            if (!int.TryParse(txtNhapSoPT.Text, out int soPhanTu) || soPhanTu <= 0)
            {
                MessageBox.Show("Số phần tử phải là một số nguyên dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNhapSoPT.Text = "";
                txtNhapSoPT.Focus();
                return false;
            }

            return true;
        }

        private void cbChucNang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbChucNang.SelectedIndex == -1)
            {
                return;
            }

            string chucNang = cbChucNang.SelectedItem.ToString();

            if (chucNang == "Trung bình cộng của dãy")
            {
                double kq = tinhTrungBinhCong(daySo);
                lblKetQua.Text = "Trung bình cộng dãy số là: " + kq;
                lblKetQua.Visible = true;
            }
            else if (chucNang == "Đếm số phần tử âm")
            {
                int kq = demSoPhanTuAm(daySo);   
                lblKetQua.Text = "Số phần tử âm trong dãy là: " + kq;
                lblKetQua.Visible = true;
            }
        }

        private double tinhTrungBinhCong(double[] arr)
        {
            if (arr == null || arr.Length == 0)
                return 0;
            double sum = 0;
            foreach (var num in arr)
            {
                sum += num;
            }
            return sum / arr.Length;
        }

        private int demSoPhanTuAm(double[] arr)
        {
            if (arr == null || arr.Length == 0)
                return 0;
            int count = 0;
            foreach (var num in arr)
            {
                if (num < 0)
                    count++;
            }
            return count;
        }
    }
}
