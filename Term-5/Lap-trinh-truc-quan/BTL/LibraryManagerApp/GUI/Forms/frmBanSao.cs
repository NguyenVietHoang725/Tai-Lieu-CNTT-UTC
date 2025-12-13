using LibraryManagerApp.BLL;
using LibraryManagerApp.DTO;
using LibraryManagerApp.Helpers;
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

namespace LibraryManagerApp.GUI.Forms
{
    public partial class frmBanSao : Form
    {
        // Khai báo BLL và các biến trạng thái
        private BanSaoBLL _bll = new BanSaoBLL();
        private string _maTaiLieuHienTai;
        private State _currentState;
        private FrmTimKiem _searchForm;

        // Biến lưu thông tin Tài liệu cha
        private string _maTL;
        private string _tenTL;

        // Biến lưu bản sao đang chọn
        private string _selectedMaBS;

        #region KHỞI TẠO VÀ CẤU HÌNH

        // 1. Sửa Constructor để nhận MaTL và TenTL
        public frmBanSao(string maTL, string tenTL)
        {
            InitializeComponent();

            _maTaiLieuHienTai = maTL;
            _maTL = maTL;
            _tenTL = tenTL;

            // Khởi tạo ComboBox Trạng thái
            cboTrangThai.Items.AddRange(new string[] { "Có sẵn", "Không có sẵn", "Ngưng sử dụng" });

            ConfigureDGV();
        }

        private void frmBanSao_Load(object sender, EventArgs e)
        {
            // Hiển thị thông tin Tài liệu cha (không cho sửa)
            txtMaTL.Text = _maTL;
            txtTenTL.Text = _tenTL;

            SetState(State.READ);
            LoadData();
        }

        private void ConfigureDGV()
        {
            // Cấu hình chung
            dgvDuLieu.AutoGenerateColumns = false;
            dgvDuLieu.ReadOnly = true;
            dgvDuLieu.AllowUserToAddRows = false;
            dgvDuLieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Tự động co giãn
            dgvDuLieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDuLieu.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Font Header (Đậm)
            dgvDuLieu.ColumnHeadersDefaultCellStyle.Font = new Font(dgvDuLieu.Font.FontFamily, 10f, FontStyle.Bold);
            dgvDuLieu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDuLieu.ColumnHeadersHeight = 30;
            dgvDuLieu.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Font Cell (Thường)
            dgvDuLieu.DefaultCellStyle.Font = new Font(dgvDuLieu.Font.FontFamily, 10f, FontStyle.Regular);
            dgvDuLieu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            if (dgvDuLieu.Columns.Count == 0)
            {
                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã Bản Sao", DataPropertyName = "MaBS", Name = "MaBS", Width = 150 });

                // Cột Trạng Thái: Fill
                var colTrangThai = new DataGridViewTextBoxColumn { HeaderText = "Trạng Thái", DataPropertyName = "TrangThai", Name = "TrangThai" };
                colTrangThai.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colTrangThai.MinimumWidth = 150;
                dgvDuLieu.Columns.Add(colTrangThai);
            }
        }
        #endregion

        #region QUẢN LÝ TRẠNG THÁI (STATE)
        private void SetState(State state)
        {
            _currentState = state;
            bool isEditing = (state == State.CREATE || state == State.UPDATE);

            // Inputs
            txtMaTL.Enabled = false;
            txtTenTL.Enabled = false;

            // Mã Bản sao (textBox1) luôn khóa vì sinh tự động
            txtMaBanSao.Enabled = false;

            cboTrangThai.Enabled = isEditing;

            // Buttons
            btnThem.Enabled = (state == State.READ);
            btnSua.Enabled = (state == State.READ && !string.IsNullOrEmpty(txtMaBanSao.Text));
            btnXoa.Enabled = (state == State.READ && !string.IsNullOrEmpty(txtMaBanSao.Text));

            btnLuu.Enabled = isEditing;
            btnHuy.Enabled = isEditing;

            btnTimKiem.Enabled = (state == State.READ); // (Nếu có chức năng tìm kiếm)

            dgvDuLieu.Enabled = (state == State.READ);

            if (state == State.READ)
            {
                ClearInputs();
            }
        }
        #endregion

        #region CHỨC NĂNG READ
        private void LoadData()
        {
            try
            {
                dgvDuLieu.DataSource = null;
                // Chỉ tải các bản sao của Tài liệu cha (_maTL)
                List<BanSaoDTO> danhSach = _bll.LayDanhSachBanSao(_maTL);
                dgvDuLieu.DataSource = danhSach;

                dgvDuLieu.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Bản sao: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDuLieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvDuLieu.RowCount) return;

            string maBS = dgvDuLieu.Rows[e.RowIndex].Cells["MaBS"].Value.ToString();
            _selectedMaBS = maBS;

            BanSaoDTO model = _bll.LayChiTietBanSao(maBS);

            if (model != null)
            {
                LoadModelToInputs(model);
            }

            if (_currentState == State.READ)
            {
                bool isRowSelected = !string.IsNullOrEmpty(txtMaBanSao.Text);
                btnSua.Enabled = isRowSelected;
                btnXoa.Enabled = isRowSelected;
            }
        }

        private void LoadModelToInputs(BanSaoDTO model)
        {
            txtMaBanSao.Text = model.MaBS;
            cboTrangThai.SelectedItem = model.TrangThai;
        }
        #endregion

        #region CHỨC NĂNG CREATE
        private void btnThem_Click(object sender, EventArgs e)
        {
            ClearInputs();
            SetState(State.CREATE);
            cboTrangThai.Focus();
        }
        #endregion

        #region CHỨC NĂNG UPDATE
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_currentState == State.READ && !string.IsNullOrEmpty(txtMaBanSao.Text))
            {
                SetState(State.UPDATE);
                cboTrangThai.Focus();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một Bản sao để chỉnh sửa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region CHỨC NĂNG DELETE
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maBS = txtMaBanSao.Text.Trim();

            if (string.IsNullOrEmpty(maBS))
            {
                MessageBox.Show("Vui lòng chọn một Bản sao để xóa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa Bản sao:\n[ {maBS} ] không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (_bll.XoaBanSao(maBS))
                    {
                        MessageBox.Show("Xóa Bản sao thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Xóa Bản sao thất bại. Có thể Bản sao đang được mượn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region CHỨC NĂNG READ VÀ TÌM KIẾM

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            // Kiểm tra trạng thái và MaTL
            if (string.IsNullOrEmpty(_maTaiLieuHienTai))
            {
                MessageBox.Show("Không thể tìm kiếm vì Mã Tài Liệu chưa được xác định.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Đảm bảo không tạo nhiều instance của Form tìm kiếm VÀ CHỈ ĐĂNG KÝ EVENT MỘT LẦN
            if (_searchForm == null || _searchForm.IsDisposed)
            {
                // 1. Lấy metadata (MaBS, TrangThai)
                List<FieldMetadata> bsMetadata = _bll.GetSearchFields();

                // Khởi tạo Form tìm kiếm mới, truyền metadata vào constructor
                _searchForm = new FrmTimKiem(bsMetadata);

                // 2. Đăng ký Event để nhận bộ lọc từ Form tìm kiếm (CHỈ 1 LẦN)
                _searchForm.OnSearchApplied += HandleSearchAppliedBanSao;
            }
            // 3. Đăng ký sự kiện đóng Form để giải phóng tài nguyên
            _searchForm.FormClosed += SearchForm_FormClosed;

            // 4. Hiển thị Form non-modal
            _searchForm.Show();
            _searchForm.BringToFront(); // Đưa Form tìm kiếm lên trên
        }

        // Hàm xử lý Event khi người dùng nhấn nút "Tìm" trong FrmTimKiem
        // Event này được kích hoạt (Raise) từ FrmTimKiem sau khi thu thập filters
        private void HandleSearchAppliedBanSao(List<SearchFilter> filters)
        {
            try
            {
                // Kiểm tra lại MaTL trước khi gọi hàm tìm kiếm
                if (string.IsNullOrEmpty(_maTaiLieuHienTai))
                {
                    MessageBox.Show("Mã Tài Liệu hiện tại bị mất. Không thể tìm kiếm Bản Sao.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Gọi hàm tải dữ liệu, truyền MaTL cố định và các bộ lọc bổ sung
                LoadDataWithFilters(_maTaiLieuHienTai, filters);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thực hiện tìm kiếm Bản Sao: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataWithFilters(string maTL, List<SearchFilter> filters)
        {
            try
            {
                dgvDuLieu.DataSource = null;
                List<BanSaoDTO> danhSach;

                if (filters == null || filters.Count == 0)
                {
                    // Nếu không có bộ lọc bổ sung, tải lại tất cả Bản Sao của MaTL hiện tại
                    danhSach = _bll.LayDanhSachBanSao(maTL);
                }
                else
                {
                    // GỌI HÀM TÌM KIẾM CÓ LỌC KÉP
                    // Hàm này sẽ lọc trong MaTL cố định VÀ áp dụng thêm filters (MaBS, TrangThai,...)
                    danhSach = _bll.TimKiemBanSao(maTL, filters);
                }

                dgvDuLieu.DataSource = danhSach;
                dgvDuLieu.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                int count = danhSach.Count;
                MessageBox.Show($"Tìm thấy {count} bản sao khớp với bộ lọc trong Mã TL [{maTL}].", "Thông báo Tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thực hiện tìm kiếm Bản Sao: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnHuy.Enabled = true;
        }
        // Hàm xử lý sự kiện đóng Form tìm kiếm để giải phóng tài nguyên
        private void SearchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_searchForm != null)
            {
                // QUAN TRỌNG: Gỡ đăng ký Event OnSearchApplied để tránh rò rỉ bộ nhớ
                _searchForm.OnSearchApplied -= HandleSearchAppliedBanSao;
                // Gỡ đăng ký Event FormClosed (best practice)
                _searchForm.FormClosed -= SearchForm_FormClosed;
            }
            // Đặt biến tham chiếu về null để lần sau click nút "Tìm Kiếm" sẽ tạo Form mới
            _searchForm = null;
        }

        #endregion

        #region XỬ LÝ SỰ KIỆN CÁC NÚT - LƯU - HỦY
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            // 1. Thu thập Model
            BanSaoDTO model = GetModelFromInputs();

            try
            {
                if (_currentState == State.CREATE)
                {
                    // --- LOGIC CREATE ---
                    // MaTL đã được gán khi khởi tạo form
                    model.MaTL = _maTL;

                    string newMaBS = _bll.ThemBanSao(model);

                    if (newMaBS != null && newMaBS != string.Empty)
                    {
                        MessageBox.Show($"Tạo Bản sao thành công. Mã BS: {newMaBS}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        SetState(State.READ);
                        _selectedMaBS = newMaBS;
                    }
                    else if (newMaBS == null)
                    {
                        MessageBox.Show("Lỗi: Đã đạt giới hạn 999 bản sao cho tài liệu này.", "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Thêm Bản sao thất bại (Lỗi CSDL).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (_currentState == State.UPDATE)
                {
                    // --- LOGIC UPDATE ---
                    model.MaBS = _selectedMaBS; // Gán Mã BS đang sửa

                    if (_bll.CapNhatBanSao(model))
                    {
                        MessageBox.Show("Cập nhật Bản sao thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        SetState(State.READ);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật Bản sao thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống khi thực hiện Lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (_currentState == State.UPDATE)
            {
                // Khôi phục dữ liệu gốc
                if (!string.IsNullOrEmpty(_selectedMaBS))
                {
                    BanSaoDTO model = _bll.LayChiTietBanSao(_selectedMaBS);
                    if (model != null)
                    {
                        LoadModelToInputs(model);
                    }
                    else
                    {
                        ClearInputs();
                    }
                }
            }

            SetState(State.READ);
            LoadData();
        }
        #endregion

        #region HÀM BỔ TRỢ
        private BanSaoDTO GetModelFromInputs()
        {
            return new BanSaoDTO
            {
                MaBS = txtMaBanSao.Text.Trim(), // Sẽ rỗng khi CREATE
                MaTL = _maTL, // Lấy từ biến thành viên
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Có sẵn"
            };
        }

        private void ClearInputs()
        {
            txtMaBanSao.Text = string.Empty;
            cboTrangThai.SelectedIndex = 0; // Mặc định "Có sẵn"

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private bool ValidateInputs()
        {
            // 1. Kiểm tra trạng thái (NOT NULL)
            if (cboTrangThai.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Trạng thái cho Bản sao.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTrangThai.Focus();
                return false;
            }

            // 2. Khi CREATE, Trạng thái mặc định nên là "Có sẵn"
            if (_currentState == State.CREATE && cboTrangThai.SelectedItem.ToString() != "Có sẵn")
            {
                MessageBox.Show("Khi tạo mới, trạng thái mặc định phải là 'Có sẵn'.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        #endregion
    }
}
