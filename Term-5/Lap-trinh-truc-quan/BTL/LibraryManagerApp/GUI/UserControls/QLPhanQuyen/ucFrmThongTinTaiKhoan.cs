using LibraryManagerApp.BLL;
using LibraryManagerApp.DTO;
using LibraryManagerApp.GUI.Forms;
using LibraryManagerApp.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LibraryManagerApp.GUI.UserControls.QLPhanQuyen
{
    public partial class ucFrmThongTinTaiKhoan : UserControl
    {
        private TaiKhoanBLL _bll = new BLL.TaiKhoanBLL();
        private State _currentState;
        private string _selectedMaTK = string.Empty;
        private FrmTimKiem _searchForm;
        public event EventHandler<StatusRequestEventArgs> OnStatusRequest;
        private const string MODULE_NAME = "TaiKhoan";

        #region KHỞI TẠO VÀ CẤU HÌNH
        public ucFrmThongTinTaiKhoan()
        {
            InitializeComponent();

            cboTrangThai.Items.AddRange(new string[] { "Hoạt động", "Khóa" });

            ConfigureDGV();

            // Thiết lập TextBox Mật khẩu là PasswordChar
            txtMatKhau.PasswordChar = '•';
            txtNhacLaiMK.PasswordChar = '•';
        }

        private void ucFrmThongTinTaiKhoan_Load(object sender, EventArgs e)
        {
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
            dgvDuLieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDuLieu.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Font Header
            dgvDuLieu.ColumnHeadersDefaultCellStyle.Font = new Font(dgvDuLieu.Font.FontFamily, 10f, FontStyle.Bold);
            dgvDuLieu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDuLieu.ColumnHeadersHeight = 30;
            dgvDuLieu.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Font Cell
            dgvDuLieu.DefaultCellStyle.Font = new Font(dgvDuLieu.Font.FontFamily, 10f, FontStyle.Regular);
            dgvDuLieu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            if (dgvDuLieu.Columns.Count == 0)
            {
                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã TK", DataPropertyName = "MaTK", Name = "MaTK" });
                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã NV", DataPropertyName = "MaNV", Name = "MaNV" });

                // Cột Dài: Họ Tên NV (Fill)
                var colHoTen = new DataGridViewTextBoxColumn { HeaderText = "Họ Tên NV", DataPropertyName = "HoTenNV", Name = "HoTenNV" };
                colHoTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colHoTen.MinimumWidth = 150;
                colHoTen.FillWeight = 150;
                dgvDuLieu.Columns.Add(colHoTen);

                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Vai Trò", DataPropertyName = "TenVaiTro", Name = "TenVaiTro" });

                // Cột Dài: Tên ĐN (Fill)
                var colTenDN = new DataGridViewTextBoxColumn { HeaderText = "Tên ĐN", DataPropertyName = "TenDangNhap", Name = "TenDangNhap" };
                colTenDN.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colTenDN.MinimumWidth = 120;
                colTenDN.FillWeight = 100;
                dgvDuLieu.Columns.Add(colTenDN);

                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Trạng Thái", DataPropertyName = "TrangThai", Name = "TrangThai" });
                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ngày Tạo", DataPropertyName = "NgayTaoHienThi", Name = "NgayTaoHienThi", DefaultCellStyle = { Format = "dd/MM/yyyy" } });
            }
        }
        #endregion

        #region QUẢN LÝ TRẠNG THÁI (STATE)
        private void SetState(State state)
        {
            _currentState = state;

            bool isEditing = (state == State.CREATE || state == State.UPDATE);
            bool isCreating = (state == State.CREATE);

            // Inputs
            txtMaTK.Enabled = false;        // Luôn disable

            // THAY ĐỔI: cboNhanVien chỉ Enable khi CREATE
            cboNhanVien.Enabled = isCreating;

            cboVaiTro.Enabled = isEditing;
            txtTenDangNhap.Enabled = isEditing;
            txtMatKhau.Enabled = isEditing;
            txtNhacLaiMK.Enabled = isEditing;
            cboTrangThai.Enabled = isEditing;
            dtpNgayTao.Enabled = false;     // Ngày tạo thường lấy theo DB/Hệ thống

            // Buttons
            // Nút Thêm: Chỉ hiện khi đang ở chế độ Đọc VÀ User có quyền CREATE
            btnThem.Enabled = (state == State.READ) && SessionManager.CanCreate(MODULE_NAME);

            // Nút Sửa: Chỉ hiện khi đang Đọc, đã chọn dòng VÀ User có quyền UPDATE
            btnSua.Enabled = (state == State.READ && !string.IsNullOrEmpty(txtMaTK.Text))
                             && SessionManager.CanUpdate(MODULE_NAME);

            // Nút Xóa: Chỉ hiện khi đang Đọc, đã chọn dòng VÀ User có quyền DELETE
            btnXoa.Enabled = (state == State.READ && !string.IsNullOrEmpty(txtMaTK.Text))
                             && SessionManager.CanDelete(MODULE_NAME);

            // Nút Tìm kiếm: Chỉ hiện khi đang Đọc VÀ User có quyền SEARCH
            btnTimKiem.Enabled = (state == State.READ) && SessionManager.CanSearch(MODULE_NAME);

            // Các nút Lưu/Hủy giữ nguyên logic
            btnLuu.Enabled = isEditing;
            btnHuy.Enabled = isEditing;

            // Logic đặc biệt: Tải Combo chỉ khi chuyển sang CREATE/UPDATE
            if (isEditing)
            {
                LoadVaiTroVaoCombo();
            }
            if (isCreating)
            {
                LoadNhanVienVaoCombo();
            }
            else if (state == State.READ)
            {
                // Xóa inputs mật khẩu và cboNhanVien
                txtMatKhau.Clear();
                txtNhacLaiMK.Clear();
                cboNhanVien.DataSource = null;
                cboNhanVien.Text = string.Empty;
            }

            TriggerStatusEvent(state);
        }
        #endregion

        #region CHỨC NĂNG READ
        private void LoadData()
        {
            try
            {
                dgvDuLieu.DataSource = null;
                List<TaiKhoanDTO> danhSach = _bll.LayDanhSachTaiKhoan();
                dgvDuLieu.DataSource = danhSach;

                dgvDuLieu.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Tài Khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadVaiTroVaoCombo()
        {
            try
            {
                List<VaiTroDTO> danhSach = _bll.LayTatCaVaiTro();
                cboVaiTro.DataSource = danhSach;

                cboVaiTro.DisplayMember = "TenVT";
                cboVaiTro.ValueMember = "MaVT";

                // Giữ lại giá trị nếu đang UPDATE
                if (_currentState == State.CREATE)
                    cboVaiTro.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách Vai Trò: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm mới: Tải danh sách Nhân Viên chưa có Tài Khoản vào ComboBox
        private void LoadNhanVienVaoCombo()
        {
            try
            {
                List<NhanVienChuaCoTaiKhoanDTO> danhSach = _bll.LayNhanVienChuaCoTaiKhoan();

                var danhSachCombo = danhSach.Select(nv => new
                {
                    MaNV = nv.MaNV,
                    HoTenVaMa = $"{nv.MaNV} - {nv.HoTen}" // Trường hiển thị mới
                }).ToList();

                cboNhanVien.DataSource = danhSachCombo;
                cboNhanVien.DisplayMember = "HoTenVaMa";
                cboNhanVien.ValueMember = "MaNV";
                cboNhanVien.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách Nhân Viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadModelToInputs(TaiKhoanDTO model)
        {
            txtMaTK.Text = model.MaTK;

            // THAY ĐỔI: Hiển thị MaNV và HoTenNV của tài khoản đã tồn tại
            cboNhanVien.Enabled = false;
            cboNhanVien.Text = $"{model.MaNV} - {model.HoTenNV}";

            cboVaiTro.SelectedValue = model.MaVT;

            txtTenDangNhap.Text = model.TenDangNhap;

            txtMatKhau.Clear();
            txtNhacLaiMK.Clear();

            cboTrangThai.SelectedItem = model.TrangThai;
            dtpNgayTao.Value = model.NgayTao;
        }

        private void dgvDuLieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvDuLieu.RowCount)
                return;

            string maTK = dgvDuLieu.Rows[e.RowIndex].Cells["MaTK"].Value.ToString();

            _selectedMaTK = maTK;

            TaiKhoanDTO model = _bll.LayChiTietTaiKhoan(maTK);

            if (model != null)
            {
                LoadModelToInputs(model);
            }

            if (_currentState == State.READ)
            {
                bool isRowSelected = !string.IsNullOrEmpty(txtMaTK.Text);
                // Thêm kiểm tra quyền
                btnSua.Enabled = isRowSelected && SessionManager.CanUpdate(MODULE_NAME);
                btnXoa.Enabled = isRowSelected && SessionManager.CanDelete(MODULE_NAME);
            }
        }

        #endregion

        #region CHỨC NĂNG CREATE
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Create, "thêm tài khoản")) return;
            ClearInputs();

            SetState(State.CREATE);
            dtpNgayTao.Value = DateTime.Now.Date;
            cboTrangThai.SelectedItem = "Hoạt động";
            cboNhanVien.Focus();
        }
        #endregion

        #region CHỨC NĂNG UPDATE
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Update, "sửa tài khoản")) return;
            if (_currentState == State.READ && !string.IsNullOrEmpty(txtMaTK.Text))
            {
                SetState(State.UPDATE);
                txtTenDangNhap.Focus();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một Tài Khoản để chỉnh sửa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region CHỨC NĂNG DELETE
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Delete, "xóa tài khoản")) return;
            
            string maTK = txtMaTK.Text.Trim();
            string hoTenNV = cboNhanVien.Text.Trim(); // Lấy tên hiển thị khi ở trạng thái READ

            if (string.IsNullOrEmpty(maTK))
            {
                MessageBox.Show("Vui lòng chọn một Tài Khoản để xóa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa Tài Khoản:\n[ {maTK} - NV: {hoTenNV} ] không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (_bll.XoaTaiKhoan(maTK))
                    {
                        MessageBox.Show("Xóa Tài Khoản thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                        SetState(State.READ);
                    }
                    else
                    {
                        MessageBox.Show("Xóa Tài Khoản thất bại. Có thể Tài Khoản này đang có ràng buộc dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region CHỨC NĂNG TÌM KIẾM

        // private FrmTimKiem _searchForm; // Biến này phải được khai báo ở cấp độ Class/Field

        private void btnMoTimKiem_Click(object sender, EventArgs e)
        {
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Search, "tìm kiếm tài khoản")) return;
            // Lấy metadata cho Tài Khoản
            List<FieldMetadata> tkMetadata = _bll.GetSearchFields();

            // Đảm bảo không tạo nhiều instance của Form tìm kiếm VÀ CHỈ ĐĂNG KÝ EVENT MỘT LẦN
            if (_searchForm == null || _searchForm.IsDisposed)
            {
                // 1. Khởi tạo Form tìm kiếm mới, truyền metadata
                _searchForm = new FrmTimKiem(tkMetadata);

                // 2. Đăng ký Event để nhận bộ lọc khi nút "Tìm" được nhấn (CHỈ 1 LẦN)
                _searchForm.OnSearchApplied += HandleSearchAppliedTaiKhoan;
            }

            // 3. Hiển thị Form non-modal (Không chặn Form cha)
            _searchForm.Show();
            _searchForm.BringToFront(); // Đưa Form tìm kiếm lên trên
        }

        // Hàm xử lý Event khi người dùng nhấn nút "Tìm" trong FrmTimKiem
        // Event này được gọi (Raise) từ FrmTimKiem sau khi thu thập xong filters VÀ KHÔNG ĐÓNG FORM
        private void HandleSearchAppliedTaiKhoan(List<SearchFilter> filters)
        {
            try
            {
                // Gọi hàm tải dữ liệu mới với các bộ lọc nhận được
                LoadTaiKhoanData(filters);

                // Cấu hình hiển thị và thông báo có thể được thực hiện trong LoadTaiKhoanData
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thực hiện tìm kiếm Tài Khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTaiKhoanData(List<SearchFilter> filters)
        {
            try
            {
                dgvDuLieu.DataSource = null;
                List<TaiKhoanDTO> danhSach;

                if (filters == null || filters.Count == 0)
                {
                    // Tải lại toàn bộ nếu không có bộ lọc
                    danhSach = _bll.LayDanhSachTaiKhoan();
                }
                else
                {
                    // Thực hiện tìm kiếm với bộ lọc nhận được
                    danhSach = _bll.TimKiemTaiKhoan(filters);
                }

                dgvDuLieu.DataSource = danhSach;
                // ... (Cấu hình hiển thị)
                MessageBox.Show($"Tìm thấy {danhSach.Count} Tài Khoản khớp với bộ lọc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnHuy.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Tài Khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region XỬ LÝ SỰ KIỆN CÁC NÚT - LƯU - HỦY
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                return;
            }

            TaiKhoanDTO model = GetModelFromInputs();
            string rawPassword = txtMatKhau.Text.Trim();
            int errorStatus = 0;

            try
            {
                if (_currentState == State.CREATE)
                {
                    string newMaTK = _bll.ThemTaiKhoan(model, rawPassword, out errorStatus);

                    if (errorStatus == 0)
                    {
                        MessageBox.Show("Tạo Tài Khoản thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        SetState(State.READ);
                        _selectedMaTK = newMaTK;
                    }
                    else
                    {
                        string errorMessage = _bll.GetErrorMessage(errorStatus);
                        MessageBox.Show(errorMessage, "Lỗi Nghiệp Vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (_currentState == State.UPDATE)
                {
                    model.MaTK = _selectedMaTK;

                    // Chỉ truyền mật khẩu nếu người dùng đã nhập giá trị mới
                    string newPassword = string.IsNullOrEmpty(rawPassword) ? null : rawPassword;

                    if (_bll.CapNhatTaiKhoan(model, newPassword))
                    {
                        MessageBox.Show("Cập nhật Tài Khoản thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        SetState(State.READ);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật Tài Khoản thất bại. (Có thể do trùng Tên Đăng Nhập hoặc lỗi DB).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (_currentState == State.CREATE)
            {
                ClearInputs();
            }
            else if (_currentState == State.UPDATE)
            {
                if (!string.IsNullOrEmpty(_selectedMaTK))
                {
                    try
                    {
                        TaiKhoanDTO model = _bll.LayChiTietTaiKhoan(_selectedMaTK);
                        if (model != null)
                        {
                            LoadModelToInputs(model);
                        }
                        else
                        {
                            ClearInputs();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi hệ thống khi tải lại dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ClearInputs();
                    }
                }
            }
            SetState(State.READ);
            LoadData();
        }
        #endregion

        #region HÀM BỔ TRỢ
        private void TriggerStatusEvent(State state)
        {
            string title = "QUẢN LÝ TÀI KHOẢN";
            Color backColor = Color.FromArgb(32, 36, 104); // Xanh
            Color foreColor = Color.White;

            switch (state)
            {
                case State.CREATE:
                    title = "THÊM MỚI TÀI KHOẢN";
                    backColor = Color.SeaGreen;
                    break;
                case State.UPDATE:
                    title = "CẬP NHẬT TÀI KHOẢN";
                    backColor = Color.DarkOrange;
                    break;
                case State.READ:
                default:
                    title = "DANH SÁCH TÀI KHOẢN";
                    backColor = Color.FromArgb(32, 36, 104);
                    break;
            }

            OnStatusRequest?.Invoke(this, new StatusRequestEventArgs(title, backColor, foreColor));
        }
        private void ClearInputs()
        {
            txtMaTK.Clear();

            cboNhanVien.DataSource = null;
            cboNhanVien.Text = string.Empty;

            cboVaiTro.SelectedIndex = -1;
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            txtNhacLaiMK.Clear();
            cboTrangThai.SelectedIndex = -1;
            dtpNgayTao.Value = DateTime.Now.Date;

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private TaiKhoanDTO GetModelFromInputs()
        {
            string maNVHienTai;
            if (_currentState == State.CREATE)
            {
                // Lấy MaNV từ SelectedValue của ComboBox
                maNVHienTai = cboNhanVien.SelectedValue?.ToString() ?? string.Empty;
            }
            else // READ hoặc UPDATE
            {
                // Lấy MaNV từ chuỗi hiển thị khi không có DataSource
                string text = cboNhanVien.Text.Trim();
                // Giả định format là "MaNV - HoTenNV"
                if (!string.IsNullOrEmpty(text) && text.Contains(" - "))
                {
                    maNVHienTai = text.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                }
                else
                {
                    // Trường hợp dự phòng: lấy từ DB dựa trên MaTK đang chọn
                    TaiKhoanDTO selectedModel = _bll.LayChiTietTaiKhoan(_selectedMaTK);
                    maNVHienTai = selectedModel?.MaNV ?? "UNKNOWN";
                }
            }

            return new TaiKhoanDTO
            {
                MaTK = txtMaTK.Text.Trim(),
                MaNV = maNVHienTai,
                MaVT = cboVaiTro.SelectedValue?.ToString() ?? string.Empty,
                TenDangNhap = txtTenDangNhap.Text.Trim(),
                // MatKhau được truyền riêng và mã hóa ở BLL
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Hoạt động",
                NgayTao = dtpNgayTao.Value.Date,
            };
        }

        private bool ValidateInputs()
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string nhacLaiMK = txtNhacLaiMK.Text.Trim();

            // 1. Kiểm tra trường BẮT BUỘC (MaNV, MaVT, TenDangNhap, TrangThai)

            // THAY ĐỔI: Kiểm tra cboNhanVien chỉ khi CREATE
            if (_currentState == State.CREATE && (cboNhanVien.SelectedIndex == -1 || string.IsNullOrEmpty(cboNhanVien.SelectedValue?.ToString())))
            {
                MessageBox.Show("Vui lòng chọn Mã/Tên Nhân Viên chưa có Tài Khoản.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhanVien.Focus();
                return false;
            }

            if (cboVaiTro.SelectedIndex == -1 || string.IsNullOrEmpty(tenDangNhap) || cboTrangThai.SelectedIndex == -1)
            {
                MessageBox.Show("Vai Trò, Tên Đăng Nhập và Trạng Thái không được rỗng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 2. Kiểm tra Mật khẩu:
            if (_currentState == State.CREATE || !string.IsNullOrEmpty(matKhau) || !string.IsNullOrEmpty(nhacLaiMK))
            {
                // 2a. Nếu là CREATE: bắt buộc phải nhập MK
                if (_currentState == State.CREATE && (string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(nhacLaiMK)))
                {
                    MessageBox.Show("Vui lòng nhập Mật Khẩu và Nhắc lại Mật Khẩu.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhau.Focus();
                    return false;
                }
                // 2b. Nếu là UPDATE và có nhập MK: phải khớp
                else if (!string.IsNullOrEmpty(matKhau) && matKhau != nhacLaiMK)
                {
                    MessageBox.Show("Mật Khẩu và Nhắc lại Mật Khẩu không khớp.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNhacLaiMK.Focus();
                    return false;
                }

                // 2c. Kiểm tra độ dài MK (Chỉ kiểm tra nếu có nhập)
                if (!string.IsNullOrEmpty(matKhau) && matKhau.Length < 6)
                {
                    MessageBox.Show("Mật Khẩu phải có ít nhất 6 ký tự.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhau.Focus();
                    return false;
                }
            }

            // 3. Kiểm tra trạng thái (nếu là CREATE, chỉ cho phép "Hoạt động")
            if (_currentState == State.CREATE && cboTrangThai.SelectedItem.ToString() != "Hoạt động")
            {
                MessageBox.Show("Khi tạo mới, trạng thái mặc định phải là 'Hoạt động'.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        #endregion
    }
}