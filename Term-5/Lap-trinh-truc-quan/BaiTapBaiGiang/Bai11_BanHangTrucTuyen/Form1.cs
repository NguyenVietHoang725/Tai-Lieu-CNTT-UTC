using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai11_BanHangTrucTuyen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            addData();
        }

        private void addData()
        {
            lstDanhSachMatHang.Items.Add("Kỹ thuật lập trình C#");
            lstDanhSachMatHang.Items.Add("Tự học Visual C# trong 21 ngày");
            lstDanhSachMatHang.Items.Add("Lập trình Web ASP.NET");
            lstDanhSachMatHang.Items.Add(".NET toàn tập - Tập 1");
            lstDanhSachMatHang.Items.Add(".NET toàn tập - Tập 2");
            lstDanhSachMatHang.Items.Add(".NET toàn tập - Tập 3");
            lstDanhSachMatHang.Items.Add(".NET toàn tập - Tập 4");
            lstDanhSachMatHang.Items.Add(".NET toàn tập - Tập 5");
            lstDanhSachMatHang.Items.Add(".NET toàn tập - Tập 6");
            lstDanhSachMatHang.Items.Add("Cơ bản về XML");
            lstDanhSachMatHang.Items.Add("Phân tích thiết kế hệ thống");
            lstDanhSachMatHang.Items.Add("Tin học căn bản SQL Server");
            lstDanhSachMatHang.Items.Add("Cấu trúc dữ liệu và giải thuật");
        }

        private void lstDanhSachMatHang_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string currentItem = lstDanhSachMatHang.SelectedItem.ToString();

            int index = lstHangDatMua.FindString(currentItem);

            if (index == -1)
            {
                lstHangDatMua.Items.Add(currentItem);
            }
            else
            {
                MessageBox.Show("Mặt hàng đã có trong giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lstHangDatMua_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = lstHangDatMua.SelectedIndex;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa mặt hàng này khỏi giỏ hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lstHangDatMua.Items.RemoveAt(index);
            }
        }

        private string HinhThucThanhToan()
        {
            string s = "";

            if (rdbTienMat.Checked)
            {
                s += rdbTienMat.Text;
            }
            else if (rdbSec.Checked)
            {
                s += rdbSec.Text;
            }
            else if (rdbTheTinDung.Checked)
            {
                s += rdbTheTinDung.Text;
            }

            return s;
        }

        private string HinhThucLienLac()
        {
            List<string> lienLac = new List<string>();
            
            if (chbDienThoai.Checked)
            {
                lienLac.Add(chbDienThoai.Text);
            }
            if (chbEmail.Checked)
            {
                lienLac.Add(chbEmail.Text);
            }
            if (chbFax.Checked)
            {
                lienLac.Add(chbFax.Text);
            }

            return string.Join(", ", lienLac);
        }

        private string DanhSachHangDatMua()
        {
            List<string> dsHang = new List<string>();
            foreach (var item in lstHangDatMua.Items)
            {
                dsHang.Add("- " + item.ToString());
            }
            return string.Join(Environment.NewLine, dsHang);
        }

        private void ResetForm()
        {
            txtHoTen.Clear();
            txtDienThoai.Clear();
            lstDanhSachMatHang.ClearSelected();
            lstHangDatMua.Items.Clear();
            rdbTienMat.Checked = false;
            rdbSec.Checked = false;
            rdbTheTinDung.Checked = false;
            chbDienThoai.Checked = false;
            chbEmail.Checked = false;
            chbFax.Checked = false;
            txtHoTen.Focus();
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDienThoai.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return;
            }

            string thanhToan = HinhThucThanhToan();
            if (string.IsNullOrEmpty(thanhToan))
            {
                MessageBox.Show("Vui lòng chọn hình thức thanh toán!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string lienLac = HinhThucLienLac();
            if (string.IsNullOrEmpty(lienLac))
            {
                MessageBox.Show("Vui lòng chọn hình thức liên lạc!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string thongBao = $"Tên khách: {txtHoTen.Text}\n" +
                              $"Điện thoại: {txtDienThoai.Text}\n" +
                              $"Mặt hàng:\n {DanhSachHangDatMua()}\n" +
                              $"Thanh toán: {thanhToan}\n" +
                              $"Liên lạc: {lienLac}";

            MessageBox.Show(thongBao, "Thông tin đơn hàng", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            ResetForm();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
