using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<MonHoc> dsMonHoc = new List<MonHoc>()
            {
                new MonHoc { tenMon = "Tin đại cương", soTinChi = 2 },
                new MonHoc { tenMon = "Giải tích F1", soTinChi = 3 },
                new MonHoc { tenMon = "Tiếng Anh A0", soTinChi = 3 },
                new MonHoc { tenMon = "Triết học Mac - Lenin", soTinChi = 2 },
                new MonHoc { tenMon = "Vật lý F1", soTinChi = 3 }
            };

            cbTenMH.DataSource = dsMonHoc;
            cbTenMH.DisplayMember = "tenMon";
            cbTenMH.SelectedIndex = -1;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void cbTenMH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTenMH.SelectedIndex == -1)
            {
                txtSoTC.Text = "";
                return;
            }

            txtSoTC.Text = (cbTenMH.SelectedItem as MonHoc).soTinChi.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!validateNhap())
                return;

            MonHoc mh = cbTenMH.SelectedItem as MonHoc;
            int soTC = mh.soTinChi;
            double diem = double.Parse(txtDiem.Text);
            
            string thongTin = $"{mh.tenMon}-{soTC}-{diem}";

            lstDSMH.Items.Add(thongTin);
        }

        private bool validateNhap()
        {
            if (cbTenMH.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn môn học!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbTenMH.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDiem.Text))
            {
                MessageBox.Show("Bạn chưa nhập điểm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDiem.Focus();
                return false;
            }

            if (!double.TryParse(txtDiem.Text, out double diem) || diem < 0 || diem > 10)
            {
                MessageBox.Show("Điểm phải là số thực trong khoảng từ 0 đến 10!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDiem.Focus();
                return false;
            }

            return true;
        }

        private void btnTinh_Click(object sender, EventArgs e)
        {
            if (!validateDSMH())
                return;

            double tongDiem = 0;
            double tongSoTC = 0;

            foreach (var item in lstDSMH.Items)
            {
                string[] parts = item.ToString().Split('-');
                string tenMon = parts[0];
                int soTC = int.Parse(parts[1]);
                double diem = double.Parse(parts[2]);
                tongDiem += diem * soTC;
                tongSoTC += soTC;
            }

            double diemTB = tongDiem / tongSoTC;

            txtTongDiem.Text = tongDiem.ToString("0.00");
            txtTongSoTC.Text = tongSoTC.ToString();
            txtDiemTB.Text = diemTB.ToString("0.00");
        }

        private bool validateDSMH()
        {
            if (lstDSMH.Items.Count == 0)
            {
                MessageBox.Show("Danh sách môn học rỗng! Hãy nhập thông tin môn học.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
