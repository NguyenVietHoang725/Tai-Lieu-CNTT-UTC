using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai13
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void resetForm()
        {
            lblXuatDS.Visible = false;
            lblXuatSoDuongNN.Visible = false;

            btnTim.Enabled = false;
            btnTimSoDuongNN.Enabled = false;

            txtNhapN.Text = "";
            txtNhapK.Text = "";
            lblXuatDS.Text = "";
            lblXuatSoDuongNN.Text = "";
            daySo = null;
        }

        private void btnLamLai_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void btnNhapDay_Click(object sender, EventArgs e)
        {
            if (txtNhapN.Text.Length > 0 && int.TryParse(txtNhapN.Text, out int n) && n > 0)
            {
                daySo = new double[n];

                for (int i = 0; i < n; i++)
                {
                    string input;
                    double value;

                    do
                    {
                        input = Interaction.InputBox(
                            $"Nhập phần tử thứ {i + 1}:",
                            "Nhập dãy số",
                            "0"
                        );

                        if (string.IsNullOrWhiteSpace(input))
                        {
                            MessageBox.Show("Bạn đã hủy nhập!");
                            return; 
                        }

                    } while (!double.TryParse(input, out value));

                    daySo[i] = value;
                }

                lblXuatDS.Text = "Dãy số vừa nhập là: " + string.Join(", ", daySo);
                lblXuatDS.Visible = true;
                btnTim.Enabled = true;
                btnTimSoDuongNN.Enabled = true;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập một số nguyên hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNhapN.Text = "";
                txtNhapN.Focus();
                return;
            }
        }

        private void btnTimSoDuongNN_Click(object sender, EventArgs e)
        {
            double min = double.MaxValue;
            bool check = false;
            for (int i = 0; i < daySo.Length; i++)
            {
                if (daySo[i] > 0 && daySo[i] < min)
                {
                    min = daySo[i];
                    check = true;
                }
            }

            lblXuatSoDuongNN.Visible = true;

            if (check)
            {
                lblXuatSoDuongNN.Text = "Số dương nhỏ nhất trong dãy là: " + min;
            }
            else
            {
                lblXuatSoDuongNN.Text = "Dãy không có số dương!";
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (txtNhapK.Text.Length > 0 && double.TryParse(txtNhapK.Text, out double k))
            {
                bool check = false;
                string positions = "";
                for (int i = 0; i < daySo.Length; i++)
                {
                    if (daySo[i] == k)
                    {
                        positions += (i + 1) + " ";
                        check = true;
                    }
                }
                
                if (check)
                {
                    MessageBox.Show($"Số {k} xuất hiện tại vị trí: {positions}", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Số {k} không xuất hiện trong dãy!", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Vui lòng nhập một số hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNhapK.Text = "";
                txtNhapK.Focus();
            }
        }
    }
}
