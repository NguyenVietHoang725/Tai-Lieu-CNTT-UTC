using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiemTraGiuaKy
{
    public partial class Form1 : Form
    {
        ProcessDatabase db = new ProcessDatabase();
        private string trangThai = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TaiDuLieu();
            SetTrangThaiMacDinh();
        }

        private void TaiDuLieu()
        {
            try
            {
                string sql = "SELECT * FROM tThietBi";
                dgvKetQua.DataSource = db.DocBang(sql);
                dgvKetQua.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                if (dgvKetQua.Columns.Contains("MaTB"))
                    dgvKetQua.Columns["MaTB"].HeaderText = "Mã thiết bị";

                if (dgvKetQua.Columns.Contains("TenTB"))
                    dgvKetQua.Columns["TenTB"].HeaderText = "Tên thiết bị";

                if (dgvKetQua.Columns.Contains("TinhTrang"))
                    dgvKetQua.Columns["TinhTrang"].HeaderText = "Tình trạng";

                if (dgvKetQua.Columns.Contains("NgayNhan"))
                    dgvKetQua.Columns["NgayNhan"].HeaderText = "Ngày nhận";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaTrangInput()
        {
            txtMaTB.Clear();
            txtTenTB.Clear();
            cbTinhTrang.SelectedIndex = -1; 
            dtpNgayNhan.Value = DateTime.Now;
        }

        private void SetTrangThaiMacDinh()
        {
            trangThai = "";
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            dgvKetQua.Enabled = true;

            txtMaTB.Enabled = false;
            txtTenTB.Enabled = false;
            cbTinhTrang.Enabled = false;
            dtpNgayNhan.Enabled = false;
        }

        private void SetTrangThaiThem()
        {
            trangThai = "THEM";
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            dgvKetQua.Enabled = false;

            txtMaTB.Enabled = true;
            txtTenTB.Enabled = true;
            cbTinhTrang.Enabled = true;
            dtpNgayNhan.Enabled = true;

            XoaTrangInput();
            txtMaTB.Focus();
        }

        private void SetTrangThaiSua()
        {
            trangThai = "SUA";
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            dgvKetQua.Enabled = false;

            txtMaTB.Enabled = false; 
            txtTenTB.Enabled = true;
            cbTinhTrang.Enabled = true;
            dtpNgayNhan.Enabled = true;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            XoaTrangInput();
            TaiDuLieu();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            SetTrangThaiThem();
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaTB.Text))
            {
                MessageBox.Show("Vui lòng chọn thiết bị để sửa!");
                return;
            }
            SetTrangThaiSua();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTenTB.Text) || cbTinhTrang.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                    return;
                }

                if (trangThai == "THEM")
                {
                    if (string.IsNullOrWhiteSpace(txtMaTB.Text))
                    {
                        MessageBox.Show("Vui lòng nhập mã thiết bị!");
                        return;
                    }

                    string sql = $@"
                        INSERT INTO tThietBi (MaTB, TenTB, TinhTrang, NgayNhan)
                        VALUES (N'{txtMaTB.Text.Trim()}',
                                N'{txtTenTB.Text.Trim()}',
                                N'{cbTinhTrang.SelectedItem}',
                                '{dtpNgayNhan.Value:yyyy-MM-dd}')";

                    db.CapNhatDuLieu(sql);
                    MessageBox.Show("Thêm mới thành công!");
                }
                else if (trangThai == "SUA")
                {
                    string sql = $@"
                        UPDATE tThietBi
                        SET TenTB = N'{txtTenTB.Text.Trim()}',
                            TinhTrang = N'{cbTinhTrang.SelectedItem}',
                            NgayNhan = '{dtpNgayNhan.Value:yyyy-MM-dd}'
                        WHERE MaTB = N'{txtMaTB.Text.Trim()}'";

                    db.CapNhatDuLieu(sql);
                    MessageBox.Show("Cập nhật thành công!");
                }

                TaiDuLieu();
                SetTrangThaiMacDinh();
                XoaTrangInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn hủy thao tác này không?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                XoaTrangInput();
                SetTrangThaiMacDinh();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaTB.Text))
            {
                MessageBox.Show("Vui lòng chọn thiết bị cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa thiết bị này không?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    string sql = $"DELETE FROM tThietBi WHERE MaTB = N'{txtMaTB.Text.Trim()}'";
                    db.CapNhatDuLieu(sql);
                    MessageBox.Show("Xóa thành công!");
                    TaiDuLieu();
                    XoaTrangInput();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                }
            }
        }

        private void dgvKetQua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (trangThai != "") return; 

            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvKetQua.Rows[e.RowIndex];
                    txtMaTB.Text = row.Cells["MaTB"].Value.ToString();
                    txtTenTB.Text = row.Cells["TenTB"].Value.ToString();
                    cbTinhTrang.SelectedItem = row.Cells["TinhTrang"].Value.ToString();
                    dtpNgayNhan.Value = Convert.ToDateTime(row.Cells["NgayNhan"].Value);
                }
            }
            catch { }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM tThietBi WHERE TinhTrang = N'Bảo hành'";
                DataTable dt = db.DocBang(sql);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có thiết bị nào đang bảo hành.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvKetQua.DataSource = null;
                }
                else
                {
                    dgvKetQua.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
