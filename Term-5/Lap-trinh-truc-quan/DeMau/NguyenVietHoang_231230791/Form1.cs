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

namespace NguyenVietHoang_231230791
{
    public partial class Form1 : Form
    {
        string connectionString =
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\00_UNIVERSITY\TERM_05\LapTrinhTrucQuan\DeMau\NguyenVietHoang_231230791\QLKH.mdf;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string query = "SELECT * FROM tKhachHang";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvData.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message);
            }
        }

        private void txtMaKH_Leave(object sender, EventArgs e)
        {
            ValidateNumericTextBox(txtMaKH, "Mã khách hàng");
        }

        private void txtSoTienGui_Leave(object sender, EventArgs e)
        {
            ValidateNumericTextBox(txtSoTienGui, "Số tiền gửi");
        }

        private void ValidateNumericTextBox(TextBox txt, string fieldName)
        {
            string text = txt.Text.Trim();

            if (string.IsNullOrEmpty(text))
                return;

            if (!decimal.TryParse(text, out decimal value) || value < 0)
            {
                MessageBox.Show($"{fieldName} phải là số dương hợp lệ!",
                    "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txt.Focus();
                txt.SelectAll();
            }
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvData.Rows[e.RowIndex];

                txtMaKH.Text = row.Cells["MaKH"].Value?.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value?.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString();
                txtSoTienGui.Text = row.Cells["SoTienGui"].Value?.ToString();

                cbThoiGianGui.Text = row.Cells["ThoiGianGui"].Value?.ToString();
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            txtMaKH.Clear();
            txtHoTen.Clear();
            txtDiaChi.Clear();
            txtSoTienGui.Clear();
            txtNgayGui.Clear();

            if (cbThoiGianGui.Items.Count > 0)
                cbThoiGianGui.SelectedIndex = -1;

            rdbLoaiThuong.Checked = false;
            rdbPhatLoc.Checked = false;

            txtMaKH.Focus();
        }

        private void btnThemDS_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string ngayGui = txtNgayGui.Text.Trim();
            DateTime ngayGuiValue;
            decimal soTienGui;
            int kyHan;

            if (string.IsNullOrEmpty(maKH))
            {
                MessageBox.Show("Mã khách hàng không được để trống!", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                return;
            }

            if (maKH.Length != 6 || !maKH.All(char.IsDigit))
            {
                MessageBox.Show("Mã khách hàng phải gồm đúng 6 chữ số!", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                return;
            }

            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Họ tên khách hàng không được để trống!", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            if (string.IsNullOrEmpty(diaChi))
            {
                MessageBox.Show("Địa chỉ khách hàng không được để trống!", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }

            if (!DateTime.TryParse(ngayGui, out ngayGuiValue))
            {
                MessageBox.Show("Ngày gửi không hợp lệ! Vui lòng nhập đúng định dạng ngày (vd: 24/10/2025)", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNgayGui.Focus();
                return;
            }

            if (cbThoiGianGui.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn kỳ hạn gửi!", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(cbThoiGianGui.SelectedItem.ToString(), out kyHan))
            {
                MessageBox.Show("Kỳ hạn không hợp lệ!", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtSoTienGui.Text.Trim(), out soTienGui) || soTienGui <= 0)
            {
                MessageBox.Show("Số tiền gửi phải là số dương!", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoTienGui.Focus();
                return;
            }

            decimal laiSuat = 0;
            switch (kyHan)
            {
                case 1: laiSuat = 0.03m; break;
                case 3: laiSuat = 0.045m; break;
                case 6: laiSuat = 0.06m; break;
                case 12: laiSuat = 0.07m; break;
                default: laiSuat = 0.02m; break; 
            }

            decimal tienLai = soTienGui * laiSuat * kyHan / 12;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO tKhachHang (MaKH, HoTen, DiaChi, ThoiGianGui, SoTienGui, LaiSuatTienGui)
                         VALUES (@MaKH, @HoTen, @DiaChi, @ThoiGianGui, @SoTienGui, @LaiSuatTienGui)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        cmd.Parameters.AddWithValue("@HoTen", hoTen);
                        cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                        cmd.Parameters.AddWithValue("@ThoiGianGui", kyHan);
                        cmd.Parameters.AddWithValue("@SoTienGui", soTienGui);
                        cmd.Parameters.AddWithValue("@LaiSuatTienGui", tienLai);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Đã thêm khách hàng và lưu vào CSDL!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();

                btnThemMoi_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu vào CSDL: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string maKH = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(maKH))
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng cần tìm!", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM tKhachHang WHERE MaKH = @MaKH";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            dgvData.DataSource = dt;
                            MessageBox.Show("Đã tìm thấy khách hàng!", "Kết quả",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy khách hàng có mã này!", "Kết quả",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            dgvData.DataSource = null;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm khách hàng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}