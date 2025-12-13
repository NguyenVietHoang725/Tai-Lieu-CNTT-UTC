using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai14
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLamLai_Click(object sender, EventArgs e)
        {
            resetThongTin();
        }

        private void resetThongTin()
        {
            txtHoTen.Text = "";
            cbKhoa.SelectedIndex = -1;
            cbKhoas.SelectedIndex = -1;
            lstDSGiaoTrinh.ClearSelected();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (txtHoTen.Text != "" && cbKhoa.SelectedIndex != -1 && cbKhoas.SelectedIndex != -1 && lstDSGiaoTrinh.SelectedItems.Count > 0)
            {
                string hoTen = txtHoTen.Text;
                string khoa = cbKhoa.SelectedItem.ToString();
                string khoas = cbKhoas.SelectedItem.ToString();
                string giaoTrinh = string.Join(", ", lstDSGiaoTrinh.SelectedItems.Cast<string>());

                string thongTin = $"{lstDSHocSinh.Items.Count + 1}. {hoTen} {khoa} K{khoas} ĐK: {giaoTrinh}";
                lstDSHocSinh.Items.Add(thongTin);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lstDSHocSinh.SelectedIndex != -1)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa học sinh này không?", "Xóa Học Sinh", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }

                lstDSHocSinh.Items.RemoveAt(lstDSHocSinh.SelectedIndex);
                // Cập nhật lại số thứ tự
                for (int i = 0; i < lstDSHocSinh.Items.Count; i++)
                {
                    string item = lstDSHocSinh.Items[i].ToString();
                    int dotIndex = item.IndexOf('.');
                    if (dotIndex != -1)
                    {
                        string newItem = $"{i + 1}{item.Substring(dotIndex)}";
                        lstDSHocSinh.Items[i] = newItem;
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn học sinh để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
