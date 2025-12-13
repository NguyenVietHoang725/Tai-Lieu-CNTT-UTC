using Test;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        // Khai báo đối tượng ProcessDataBase
        ProcessDataBase pd = new ProcessDataBase();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TaiDuLieu();
        }

        // --- HÀM HỖ TRỢ DỮ LIỆU ---

        private void TaiDuLieu()
        {
            try
            {
                string sql = "SELECT * FROM KH";
                dgvKetQua.DataSource = pd.DocBang(sql);
                dgvKetQua.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- HÀM VALIDATE VÀ KIỂM TRA TRÙNG MÃ ---

        // Hàm kiểm tra trùng mã (trả về true nếu MaKH đã tồn tại)
        private bool KiemTraTrungMa(string maKH)
        {
            if (string.IsNullOrEmpty(maKH)) return false;

            // Lệnh SQL kiểm tra số lượng bản ghi có cùng MaKH
            string sql = $"SELECT COUNT(*) FROM KH WHERE MaKH = N'{maKH}'";

            try
            {
                DataTable dt = pd.DocBang(sql);
                int count = Convert.ToInt32(dt.Rows[0][0]);
                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi CSDL khi kiểm tra trùng mã: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true; // Giả định trùng để ngăn chặn thao tác
            }
        }

        // --- HÀM LÀM MỚI INPUT ---

        // Hàm làm mới (ResetInput)
        private void ResetInput()
        {
            // Reset TextBox và ComboBox
            txt_MaKh.Text = "";
            txt_Ten.Text = "";
            txt_DiaChi.Text = "";
            txtSotien.Text = "";

            cb_tgiangui.SelectedIndex = -1; // Reset ComboBox về trạng thái không chọn
            txt_LaiSuat.Text = "";

            // Mẫu Reset RadioButton và CheckBox (Thay thế tên control thực tế)
            // if (rb_Nam.Checked) rb_Nam.Checked = false;
            // chk_KichHoat.Checked = false; 

            txt_MaKh.Focus();
        }


        // --- SỰ KIỆN DATAGRIDVIEW ---

        private void dgv_Ketqua_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dgvKetQua.Rows[e.RowIndex];

                    // Gán giá trị từ từng cột sang textbox
                    txt_MaKh.Text = row.Cells["MaKH"].Value.ToString();
                    txt_Ten.Text = row.Cells["TenKH"].Value.ToString();
                    txt_DiaChi.Text = row.Cells["DiaChi"].Value.ToString();
                    txtSotien.Text = row.Cells["TienGui"].Value.ToString();
                    cb_tgiangui.Text = row.Cells["ThoiGianGui"].Value.ToString();
                    txt_LaiSuat.Text = row.Cells["LaiSuat"].Value.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi chọn dòng: " + ex.Message);
                }
            }
        }

        // --- SỰ KIỆN BUTTONS ---

        // Thêm mới (INSERT)
        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. VALIDATE: Kiểm tra thiếu dữ liệu bắt buộc
                if (string.IsNullOrEmpty(txt_MaKh.Text) || string.IsNullOrEmpty(txt_Ten.Text) || string.IsNullOrEmpty(txtSotien.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ Mã, Tên và Số Tiền!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. VALIDATE: Kiểm tra trùng mã
                if (KiemTraTrungMa(txt_MaKh.Text))
                {
                    MessageBox.Show("Mã khách hàng này đã tồn tại. Vui lòng nhập mã khác!", "Lỗi trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_MaKh.Focus();
                    return;
                }

                // 3. Thực thi INSERT
                string sql = $"INSERT INTO KH (MaKH, HoTen, DiaChi, TienGui, ThoiGian, LaiSuat) " +
                             $"VALUES (N'{txt_MaKh.Text}', N'{txt_Ten.Text}', N'{txt_DiaChi.Text}', '{txtSotien.Text}', '{cb_tgiangui.Text}', '{txt_LaiSuat.Text}')";

                pd.CapNhatDuLieu(sql);
                TaiDuLieu();
                ResetInput();

                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm thất bại. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa (DELETE)
        private void btn_Del_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_MaKh.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(
                "Bạn có chắc chắn muốn xóa khách hàng có Mã: " + txt_MaKh.Text + " không?",
                "Xác Nhận Xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string sql = $"DELETE FROM KH WHERE MaKH = N'{txt_MaKh.Text}'";

                    pd.CapNhatDuLieu(sql);
                    TaiDuLieu();
                    ResetInput();

                    MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xóa thất bại. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Sửa (UPDATE) - Mẫu
        private void btn_Edit_Click(object sender, EventArgs e)
        {
            // Kiểm tra đã chọn một MaKH để sửa hay chưa
            if (string.IsNullOrEmpty(txt_MaKh.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // VALIDATE: Kiểm tra thiếu dữ liệu
            if (string.IsNullOrEmpty(txt_Ten.Text) || string.IsNullOrEmpty(txtSotien.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên và Số Tiền!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sql = $"UPDATE KH SET " +
                             $"HoTen = N'{txt_Ten.Text}', " +
                             $"DiaChi = N'{txt_DiaChi.Text}', " +
                             $"TienGui = '{txtSotien.Text}', " +
                             $"ThoiGian = '{cb_tgiangui.Text}', " +
                             $"LaiSuat = '{txt_LaiSuat.Text}' " +
                             $"WHERE MaKH = N'{txt_MaKh.Text}'";

                pd.CapNhatDuLieu(sql);
                TaiDuLieu();
                ResetInput();

                MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật thất bại. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Làm mới (Refresh)
        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            ResetInput();
            MessageBox.Show("Đã làm mới các trường nhập liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Thoát (Exit)
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Bạn có chắc chắn muốn thoát không?\nNếu có, hãy nhấn Yes; nếu không, chọn No.",
                "Xác Nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}