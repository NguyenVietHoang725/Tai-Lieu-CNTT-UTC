using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleMarket
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            addData();
        }

        private void addData()
        {
            lstDanhSachMatHang.Items.Add("Kỹ thuật lập trình C#");
            lstDanhSachMatHang.Items.Add("Tự học Visual C# trong 21 ngày");
            lstDanhSachMatHang.Items.Add("Bài tập C#");
            lstDanhSachMatHang.Items.Add(".NET toàn tập tập 1");
            lstDanhSachMatHang.Items.Add(".NET toàn tập tập 2");
            lstDanhSachMatHang.Items.Add(".NET toàn tập tập 3");
            lstDanhSachMatHang.Items.Add(".NET toàn tập tập 4");
            lstDanhSachMatHang.Items.Add("Tin học cơ bản");
            lstDanhSachMatHang.Items.Add("SQL Server");
            lstDanhSachMatHang.Items.Add("Cơ bản về XML");
            lstDanhSachMatHang.Items.Add("Phân tích thiết kế hệ thống");
            lstDanhSachMatHang.Items.Add("Sử dụng Word");
            lstDanhSachMatHang.Items.Add("Đến với Word 2003");
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (txtHoTen.Text.Equals("") || txtDienThoai.Text.Equals(""))
            {
                MessageBox.Show("Bạn cần nhập đủ thông tin", "Thông báo");
            }
            else
            {
                string sb = "";
                foreach (object item in lstHangDatMua.Items)
                {
                    sb += (item.ToString());
                    sb += ("\n");
                }
                DialogResult result = MessageBox.Show(
                    "Đặt hàng thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void lstDanhSachMatHang_MouseDoubleClick(object sender, EventArgs e)
        {
            string curItem = lstDanhSachMatHang.SelectedItem.ToString();

            // Tìm kiếm xem có trong hàng đặt mua
            int index = lstHangDatMua.FindString(curItem);
            // Nếu mà không có thì in thông báo và thêm vào hàng đặt mua ngược lại báo đã có
            if (index == -1)
            {
                lstHangDatMua.Items.Add(curItem);
            }
            else
            {
                MessageBox.Show("Hàng đặt mua đã có rồi!");
            }
        }

        private void lstHangDatMua_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            int index = lstHangDatMua.SelectedIndex;
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                lstHangDatMua.Items.RemoveAt(index);
        }

        private string HinhThucLL()
        {
            string a = "";
            if (chbDienThoai.Checked == true)
            {
                a += "    " + chbDienThoai.Text;
            }
            if (chbEmail.Checked == true)
            {
                a += "    " + chbEmail.Text;
            }
            if (chbFax.Checked == true)
            {
                a += "    " + chbFax.Text;
            }
            return a;
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string hinhThuc = HinhThucLL();
            MessageBox.Show($"Hình thức liên lạc: {hinhThuc}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string ThanhToan()
        {
            string s = "";
            if (rdbTienMat.Checked == true)
            {
                s += rdbTienMat.Text;
            }
            else if (rdbSec.Checked == true)
            {
                s += rdbSec.Text;
            }
            else if (rdbTheTinDung.Checked == true)
            {
                s += rdbTheTinDung.Text;
            }

            return s;
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rad = sender as RadioButton;
            if (rad != null && rad.Checked)
            {
                string phuongThuc = ThanhToan();
                MessageBox.Show($"Phương thức thanh toán: {phuongThuc}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
