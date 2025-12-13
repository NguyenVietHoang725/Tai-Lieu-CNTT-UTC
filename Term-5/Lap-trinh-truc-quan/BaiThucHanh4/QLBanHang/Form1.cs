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

namespace QLBanHang
{
    public partial class frmMatHang : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter dap;
        DataSet ds;

        public frmMatHang()
        {
            InitializeComponent();
        }

        private void frmMatHang_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection();

            conn.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                        "AttachDbFilename=E:\\00_UNIVERSITY\\TERM_05\\LapTrinhTrucQuan\\BaiThucHanh4\\QLBanHang\\QLBanHang.mdf;" +
                        "Integrated Security=True;Connect Timeout=30";


            LoadDuLieu("SELECT * FROM tblMatHang");

            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            HienChiTiet(false);
        }

        private void LoadDuLieu(String sql)
        {
            ds = new DataSet();
            dap = new SqlDataAdapter(sql, conn);
            dap.Fill(ds); 
            dgvKetQua.DataSource = ds.Tables[0];
        }

        private void HienChiTiet(Boolean hien)
        {
            txtMaSP.Enabled = hien;
            txtTenSP.Enabled = hien;
            dtpNgayHH.Enabled = hien;
            dtpNgaySX.Enabled = hien;
            txtDonVi.Enabled = hien;
            txtDonGia.Enabled = hien;
            txtGhiChu.Enabled = hien;
            //Ẩn hiện 2 nút Lưu và Hủy
            btnLuu.Enabled = hien;
            btnHuy.Enabled = hien;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            lblTieuDe.Text = "TÌM KIẾM MẶT HÀNG";
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            String sql = "SELECT * FROM tblMatHang";
            String dk = "";

            if (txtTKMaSP.Text.Trim() != "")
            {
                dk += " MaSP like '%" + txtTKMaSP.Text + "%'";
            }
            //kiem tra TenSP va MaSP khac rong
            if (txtTKTenSP.Text.Trim() != "" && dk != "")
            {
                dk += " AND TenSP like N'%" + txtTKTenSP.Text + "%'";
            }
            //Tim kiem theo TenSP khi MaSP la rong
            if (txtTKTenSP.Text.Trim() != "" && dk == "")
            {
                dk += " TenSP like N'%" + txtTKTenSP.Text + "%'";
            }
            //Ket hoi dk
            if (dk != "")
            {
                sql += " WHERE" + dk;
            }
            //Goi phương thức Load dữ liệu kết hợp điều kiện tìm kiếm
            LoadDuLieu(sql);

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            lblTieuDe.Text = "THÊM MẶT HÀNG";
            //Xoa trang GroupBox chi tiết sản phẩm
            XoaTrangChiTiet();
            //Cam nut sua xoa
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            //Hiện GroupBox Chi tiết
            HienChiTiet(true);

        }

        private void dgvKetQua_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Hien thi nut sua
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            //Bắt lỗi khi người sử dụng kích linh tinh lên datagrid
            try
            {
                txtMaSP.Text = dgvKetQua[0, e.RowIndex].Value.ToString();
                txtTenSP.Text = dgvKetQua[1, e.RowIndex].Value.ToString();
                dtpNgaySX.Value = (DateTime)dgvKetQua[2, e.RowIndex].Value;
                dtpNgayHH.Value = (DateTime)dgvKetQua[3, e.RowIndex].Value;
                txtDonVi.Text = dgvKetQua[4, e.RowIndex].Value.ToString();
                txtDonGia.Text = dgvKetQua[5, e.RowIndex].Value.ToString();
                txtGhiChu.Text = dgvKetQua[6, e.RowIndex].Value.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            //Cập nhật tiêu đề
            lblTieuDe.Text = "CẬP NHẬT MẶT HÀNG";
            //Ẩn hai nút Thêm và Sửa
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            //Hiện gropbox chi tiết
            HienChiTiet(true);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {   
            if (MessageBox.Show("Bạn có chắc chắn xóa mã mặt hàng ?" + txtMaSP.Text + " không ? Nếu có ấn nút Lưu, không thì ấn nút Hủy", "Xóa sản phẩm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lblTieuDe.Text = "XÓA MẶT HÀNG";
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                //Hiện gropbox chi tiết
                HienChiTiet(true);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql = "";

            // Kiểm tra nếu kết nối chưa mở thì thực hiện mở kết nối
            if (conn.State != ConnectionState.Open)
                conn.Open();

            // --- Kiểm tra lỗi nhập liệu ---
            // Kiểm tra tên sản phẩm
            if (txtTenSP.Text.Trim() == "")
            {
                errChiTiet.SetError(txtTenSP, "Bạn không để trống tên sản phẩm!");
                return;
            }
            else
            {
                errChiTiet.Clear();
            }

            // Kiểm tra ngày sản xuất
            if (dtpNgaySX.Value > DateTime.Now)
            {
                errChiTiet.SetError(dtpNgaySX, "Ngày sản xuất không hợp lệ!");
                return;
            }
            else
            {
                errChiTiet.Clear();
            }

            // Kiểm tra ngày hết hạn
            if (dtpNgayHH.Value < dtpNgaySX.Value)
            {
                errChiTiet.SetError(dtpNgayHH, "Ngày hết hạn nhỏ hơn ngày sản xuất!");
                return;
            }
            else
            {
                errChiTiet.Clear();
            }

            // Kiểm tra đơn vị
            if (txtDonVi.Text.Trim() == "")
            {
                errChiTiet.SetError(txtDonVi, "Bạn không để trống đơn vị!");
                return;
            }
            else
            {
                errChiTiet.Clear();
            }

            // Kiểm tra đơn giá
            if (txtDonGia.Text.Trim() == "")
            {
                errChiTiet.SetError(txtDonGia, "Bạn không để trống đơn giá!");
                return;
            }
            else
            {
                errChiTiet.Clear();
            }

            // --- Xử lý khi thêm ---
            if (btnThem.Enabled == true)
            {
                if (txtMaSP.Text.Trim() == "")
                {
                    errChiTiet.SetError(txtMaSP, "Bạn không để trống mã sản phẩm!");
                    return;
                }

                sql = "SELECT COUNT(*) FROM tblMatHang WHERE MaSP = '" + txtMaSP.Text + "'";
                cmd = new SqlCommand(sql, conn);
                int val = (int)cmd.ExecuteScalar();
                if (val > 0)
                {
                    errChiTiet.SetError(txtMaSP, "Mã sản phẩm đã tồn tại!");
                    return;
                }

                sql = "INSERT INTO tblMatHang (MaSP, TenSP, NgaySX, NgayHH, DonVi, DonGia, GhiChu) VALUES (";
                sql += "N'" + txtMaSP.Text + "',";
                sql += "N'" + txtTenSP.Text + "',";
                sql += "'" + dtpNgaySX.Value.Date + "',";
                sql += "'" + dtpNgayHH.Value.Date + "',";
                sql += "N'" + txtDonVi.Text + "',";
                sql += "N'" + txtDonGia.Text + "',";
                sql += "N'" + txtGhiChu.Text + "')";
            }

            // --- Xử lý khi sửa ---
            if (btnSua.Enabled == true)
            {
                sql = "UPDATE tblMatHang SET ";
                sql += "TenSP = N'" + txtTenSP.Text + "',";
                sql += "NgaySX = '" + dtpNgaySX.Value.Date + "',";
                sql += "NgayHH = '" + dtpNgayHH.Value.Date + "',";
                sql += "DonVi = N'" + txtDonVi.Text + "',";
                sql += "DonGia = '" + txtDonGia.Text + "',";
                sql += "GhiChu = N'" + txtGhiChu.Text + "' ";
                sql += "WHERE MaSP = N'" + txtMaSP.Text + "'";
            }

            // --- Xử lý khi xóa ---
            if (btnXoa.Enabled == true)
            {
                sql = "DELETE FROM tblMatHang WHERE MaSP = N'" + txtMaSP.Text + "'";
            }

            // --- Thực thi SQL ---
            if (sql != "")
            {
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                LoadDuLieu("SELECT * FROM tblMatHang");
            }

            conn.Close();

            HienChiTiet(false);
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = true;
        }



        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            //xoa trang
            XoaTrangChiTiet();
            //Cam nhap
            HienChiTiet(false);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void XoaTrangChiTiet()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            txtDonVi.Clear();
            txtDonGia.Clear();
            txtGhiChu.Clear();

            // Đặt lại ngày mặc định (hôm nay)
            dtpNgaySX.Value = DateTime.Now;
            dtpNgayHH.Value = DateTime.Now;

            // Xóa lỗi đang hiển thị (nếu có)
            errChiTiet.Clear();
        }

    }
}
