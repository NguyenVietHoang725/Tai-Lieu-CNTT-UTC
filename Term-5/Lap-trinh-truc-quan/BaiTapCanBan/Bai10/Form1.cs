using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai10
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
            txtNhapA.Text = "";
            txtNhapB.Text = "";
            txtNhapC.Text = "";
            txtKetQua.Text = "";

            rdbBacNhat.Checked = true;
            changeMode();
        }

        private void changeMode()
        {
            if (rdbBacNhat.Checked)
            {
                txtNhapC.Enabled = false;
                txtNhapC.Text = "";
            }
            else
            {
                txtNhapC.Enabled = true;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dlg = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
                Application.Exit();
        }

        private void btnLamLai_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void rdb_CheckChanged(object sender, EventArgs e)
        {
            changeMode();
        }

        private void btnGiaiPT_Click(object sender, EventArgs e)
        {
            if (rdbBacNhat.Checked)
            {
                if (validateTxt(txtNhapA, txtNhapB))
                    giaiPTBacNhat();
            }
            else if (rdbBacHai.Checked)
            {
                if (validateTxt(txtNhapA, txtNhapB, txtNhapC))
                    giaiPTBacHai();
            }
        }

        private bool validateTxt(params TextBox[] txts)
        {
            foreach (var txt in txts)
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    MessageBox.Show("Vui lòng nhập đủ dữ liệu!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt.Focus();
                    return false;
                }
                if (!double.TryParse(txt.Text, out _))
                {
                    MessageBox.Show("Dữ liệu phải là số!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt.Focus();
                    return false;
                }
            }
            return true;
        }

        private void giaiPTBacNhat()
        {
            double a = double.Parse(txtNhapA.Text);
            double b = double.Parse(txtNhapB.Text);

            if (a == 0)
            {
                if (b == 0)
                    txtKetQua.Text = "Phương trình vô số nghiệm";
                else
                    txtKetQua.Text = "Phương trình vô nghiệm";
            }
            else
            {
                double x = -b / a;
                txtKetQua.Text = $"x = {Math.Round(x, 2)}";
            }
        }

        private void giaiPTBacHai()
        {
            double a = double.Parse(txtNhapA.Text);
            double b = double.Parse(txtNhapB.Text);
            double c = double.Parse(txtNhapC.Text);

            if (a == 0)
            {
                double x = -c / b;
                txtKetQua.Text = $"(Về bậc nhất) x = {Math.Round(x, 2)}";
                return;
            }

            double delta = b * b - 4 * a * c;

            if (delta < 0)
            {
                txtKetQua.Text = "Phương trình vô nghiệm";
            }
            else if (delta == 0)
            {
                double x = -b / (2 * a);
                txtKetQua.Text = $"Nghiệm kép: x = {Math.Round(x, 2)}";
            }
            else
            {
                double x1 = (-b + Math.Sqrt(delta)) / (2 * a);
                double x2 = (-b - Math.Sqrt(delta)) / (2 * a);
                txtKetQua.Text = $"x1 = {Math.Round(x1, 2)}, x2 = {Math.Round(x2, 2)}";
            }
        }
    }
}
