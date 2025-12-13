using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!validateNhapSo())
            {
                return;
            }

            lstDaySo.Items.Add(txtNhapSo.Text);
            txtNhapSo.Clear();
            txtNhapSo.Focus();
        }

        private bool validateNhapSo()
        {
            if (string.IsNullOrEmpty(txtNhapSo.Text))
            {
                MessageBox.Show("Vui lòng nhập số nguyên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNhapSo.Clear();
                txtNhapSo.Focus();
                return false;
            }

            if (!int.TryParse(txtNhapSo.Text, out int number))
            {
                MessageBox.Show("Vui lòng nhập số nguyên hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNhapSo.Clear();
                txtNhapSo.Focus();
                return false;
            }

            return true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lstDaySo.SelectedItem != null)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa số đã chọn không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    lstDaySo.Items.Remove(lstDaySo.SelectedItem);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn số cần xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTang3_Click(object sender, EventArgs e)
        {
            if (lstDaySo.Items.Count == 0)
            {
                MessageBox.Show("Danh sách rỗng. Vui lòng thêm số trước khi tăng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < lstDaySo.Items.Count; i++)
            {
                if (int.TryParse(lstDaySo.Items[i].ToString(), out int number))
                {
                    number += 3;
                    lstDaySo.Items[i] = number.ToString();
                }
            }
        }

        private void btnChanDau_Click(object sender, EventArgs e)
        {
            int number;
            
            if (lstDaySo.Items.Count == 0)
            {
                MessageBox.Show("Danh sách rỗng. Vui lòng thêm số trước khi tìm số chẵn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < lstDaySo.Items.Count; i++)
            {
                if (int.TryParse(lstDaySo.Items[i].ToString(), out number) && number % 2 == 0)
                {
                    MessageBox.Show("Số chẵn đầu tiên trong danh sách là: " + number, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }
    }
}
