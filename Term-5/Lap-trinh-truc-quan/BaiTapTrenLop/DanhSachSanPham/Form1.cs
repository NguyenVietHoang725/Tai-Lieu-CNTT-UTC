using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DanhSachSanPham
{
    public partial class Form1 : Form
    {
        List<string> dsMatHang = new List<string>();

        public Form1()
        {
            InitializeComponent();
            dsMatHang.Add("Mã 1");
            dsMatHang.Add("Mã 2");
            dsMatHang.Add("Mã 3");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (validate() == false)
            {
                return;
            }

            if (dsMatHang.Contains(txtMaHang.Text.Trim()) == true)
            {
                MessageBox.Show("Mã hàng đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                ListViewItem newItem = new ListViewItem();
                newItem.Text = txtMaHang.Text;
                newItem.SubItems.Add(txtTenHang.Text);
                newItem.SubItems.Add(txtSoLuong.Text);

                lstViewDS.Items.Add(newItem);
                dsMatHang.Add(txtMaHang.Text.Trim());
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (lstViewDS.SelectedItems.Count > 0)
            {
                int i = lstViewDS.SelectedItems[0].Index;

                lstViewDS.Items[i].Text = txtMaHang.Text;
                lstViewDS.Items[i].SubItems[1].Text = txtTenHang.Text;
                lstViewDS.Items[i].SubItems[2].Text = txtSoLuong.Text;
            }
            else
            {
                MessageBox.Show("Hãy chọn sản phẩm cần sửa!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lstViewDS.SelectedItems.Count > 0)
            {
                if (DialogResult.Yes == MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    int i = lstViewDS.SelectedItems[0].Index;
                    lstViewDS.Items.RemoveAt(i);
                }
            }
        }

        private bool validate()
        {
            bool check = true;
            if (txtMaHang.Text.Trim() == "" || txtTenHang.Text.Trim() == "" || txtSoLuong.Text.Trim() == "")  
            {
                MessageBox.Show("Hãy điền đủ thông tin sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                check = false;
            }

            return check;
        }

        private void lstViewDS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstViewDS.SelectedItems.Count > 0)
            {
                ListViewItem ls = lstViewDS.SelectedItems[0];
                txtMaHang.Text = ls.Text;
                txtTenHang.Text = ls.SubItems[1].Text;
                txtSoLuong.Text = ls.SubItems[2].Text;
            }
            else
            {
                txtMaHang.Clear();
                txtTenHang.Clear();
                txtSoLuong.Clear();
            }
        }
    }
}
