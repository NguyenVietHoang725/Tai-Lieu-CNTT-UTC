using LibraryManagerApp.DTO;
using LibraryManagerApp.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using LibraryManagerApp.BLL;
using LibraryManagerApp.GUI.Forms; // Cần dùng cho frmTimKiem

namespace LibraryManagerApp.GUI.UserControls.QLPhanQuyen
{
    public partial class ucFrmThongTinNhanVien : UserControl
    {
        // Khai báo BLL và các biến trạng thái
        private NhanVienBLL _bll = new NhanVienBLL();
        private State _currentState;
        private string _selectedMaNV = string.Empty;
        private FrmTimKiem _searchForm;
        public event EventHandler<StatusRequestEventArgs> OnStatusRequest;

        private const string MODULE_NAME = "NhanVien";

        #region KHỞI TẠO VÀ CẤU HÌNH DGV

        public ucFrmThongTinNhanVien()
        {
            InitializeComponent();

            // Cấu hình ComboBox Giới Tính
            cboGioiTinh.Items.AddRange(new string[] { "Nam", "Nữ" });

            // TODO: (Tùy chọn) Load danh sách Phụ trách nếu bạn có bảng riêng.
            // Nếu không, bạn có thể chuyển cboPhuTrach thành TextBox hoặc load các giá trị DISTINCT từ DB.


            ConfigureDGV();
        }
        private void LoadPhuTrachComboBox()
        {
            try
            {
                cboPhuTrach.Items.Clear();
                List<string> danhSachPhuTrach = _bll.LayDanhSachPhuTrach();
                cboPhuTrach.Items.AddRange(danhSachPhuTrach.ToArray());
            }
            catch (Exception ex)
            {
                // Bỏ qua lỗi trong DesignMode
                if (!DesignMode)
                {
                    MessageBox.Show("Lỗi khi tải danh sách Phụ trách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ucFrmThongTinNhanVien_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            LoadPhuTrachComboBox(); // Gọi hàm này khi form load

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
                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã NV", DataPropertyName = "MaNV", Name = "MaNV" });
                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Họ Đệm", DataPropertyName = "HoDem", Name = "HoDem" });
                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên", DataPropertyName = "Ten", Name = "Ten" });
                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ngày Sinh", DataPropertyName = "NgaySinh", Name = "NgaySinh", DefaultCellStyle = { Format = "dd/MM/yyyy" } });
                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Giới Tính", DataPropertyName = "GioiTinhHienThi", Name = "GioiTinhHienThi" });

                // Cột Dài: Địa chỉ, Email, Phụ Trách (Fill)
                var colDiaChi = new DataGridViewTextBoxColumn { HeaderText = "Địa Chỉ", DataPropertyName = "DiaChi", Name = "DiaChi" };
                colDiaChi.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colDiaChi.MinimumWidth = 150;
                colDiaChi.FillWeight = 150;
                dgvDuLieu.Columns.Add(colDiaChi);

                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "SĐT", DataPropertyName = "SDT", Name = "SDT" });

                var colEmail = new DataGridViewTextBoxColumn { HeaderText = "Email", DataPropertyName = "Email", Name = "Email" };
                colEmail.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colEmail.MinimumWidth = 120;
                colEmail.FillWeight = 100;
                dgvDuLieu.Columns.Add(colEmail);

                var colPhuTrach = new DataGridViewTextBoxColumn { HeaderText = "Phụ Trách", DataPropertyName = "PhuTrach", Name = "PhuTrach" };
                colPhuTrach.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colPhuTrach.MinimumWidth = 120;
                colPhuTrach.FillWeight = 100;
                dgvDuLieu.Columns.Add(colPhuTrach);
            }
        }
        #endregion

        #region QUẢN LÝ TRẠNG THÁI (STATE)
        private void SetState(State state)
        {
            _currentState = state;

            bool isEditing = (state == State.CREATE || state == State.UPDATE);

            // Inputs
            // QUAN TRỌNG: txtMaNV luôn bị disabled vì mã được tự động sinh.
            txtMaNV.Enabled = false;
            txtHoDem.Enabled = isEditing;
            txtTen.Enabled = isEditing;
            dtpNgaySinh.Enabled = isEditing;
            cboGioiTinh.Enabled = isEditing;
            txtDiaChi.Enabled = isEditing;
            txtSDT.Enabled = isEditing;
            txtEmail.Enabled = isEditing;
            cboPhuTrach.Enabled = isEditing; // Thêm trường PhuTrach

            // Buttons
            // Nút Thêm: Chỉ hiện khi đang ở chế độ Đọc VÀ User có quyền CREATE
            btnThem.Enabled = (state == State.READ) && SessionManager.CanCreate(MODULE_NAME);

            // Nút Sửa: Chỉ hiện khi đang Đọc, đã chọn dòng VÀ User có quyền UPDATE
            btnSua.Enabled = (state == State.READ && !string.IsNullOrEmpty(txtMaNV.Text))
                             && SessionManager.CanUpdate(MODULE_NAME);

            // Nút Xóa: Chỉ hiện khi đang Đọc, đã chọn dòng VÀ User có quyền DELETE
            btnXoa.Enabled = (state == State.READ && !string.IsNullOrEmpty(txtMaNV.Text))
                             && SessionManager.CanDelete(MODULE_NAME);

            // Nút Tìm kiếm: Chỉ hiện khi đang Đọc VÀ User có quyền SEARCH
            btnTimKiem.Enabled = (state == State.READ) && SessionManager.CanSearch(MODULE_NAME);

            // (Lưu ý: Form Nhân Viên không có nút Xuất Excel, nếu có, bạn sẽ thêm tương tự)
            // btnXuatExcel.Enabled = (state == State.READ) && SessionManager.CanExport(MODULE_NAME);

            btnLuu.Enabled = isEditing;
            btnHuy.Enabled = isEditing;

            btnTimKiem.Enabled = (state == State.READ);

            TriggerStatusEvent(state);
        }
        #endregion

        #region CHỨC NĂNG READ
        private void LoadData()
        {
            try
            {
                dgvDuLieu.DataSource = null;
                List<NhanVienDTO> danhSach = _bll.LayThongTinNhanVien();
                dgvDuLieu.DataSource = danhSach;
                dgvDuLieu.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // === HÀM BỔ SUNG: Xử lý việc tải dữ liệu sau khi tìm kiếm ===
        private void LoadNhanVienData(List<SearchFilter> filters)
        {
            try
            {
                dgvDuLieu.DataSource = null;
                List<NhanVienDTO> danhSach;

                if (filters == null || filters.Count == 0)
                {
                    // Nếu không có bộ lọc, tải lại tất cả dữ liệu gốc
                    danhSach = _bll.LayThongTinNhanVien();
                }
                else
                {
                    // Sử dụng bộ lọc để tìm kiếm
                    danhSach = _bll.TimKiemNhanVien(filters); // Giả sử BLL có hàm SearchNhanVien
                }

                dgvDuLieu.DataSource = danhSach;
                dgvDuLieu.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                // Hiển thị thông báo (tùy chọn)
                if (filters != null && filters.Count > 0)
                {
                    MessageBox.Show($"Tìm thấy {danhSach.Count} kết quả khớp với bộ lọc.", "Thông báo Tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ClearInputs(); // Dọn dẹp inputs sau khi tải dữ liệu
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thực hiện tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadModelToInputs(NhanVienDTO model)
        {
            txtMaNV.Text = model.MaNV;
            txtHoDem.Text = model.HoDem;
            txtTen.Text = model.Ten;
            dtpNgaySinh.Value = model.NgaySinh;
            txtDiaChi.Text = model.DiaChi;
            txtSDT.Text = model.SDT;
            txtEmail.Text = model.Email;

            // Xử lý Giới Tính: Chuyển 'M'/'F' sang "Nam"/"Nữ"
            cboGioiTinh.SelectedItem = model.GioiTinh.Equals("M") ? "Nam" : "Nữ";

            // Xử lý Phụ Trách
            // Giả sử cboPhuTrach là một ComboBox (như trong Designer)
            cboPhuTrach.Text = model.PhuTrach;
        }

        private void dgvDuLieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvDuLieu.RowCount || _currentState != State.READ)
                return;

            string maNV = dgvDuLieu.Rows[e.RowIndex].Cells["MaNV"].Value.ToString();

            _selectedMaNV = maNV;

            NhanVienDTO model = _bll.LayChiTietNhanVien(maNV);

            if (model != null)
            {
                LoadModelToInputs(model);
            }

            // Cập nhật trạng thái nút sửa/xóa
            bool isRowSelected = !string.IsNullOrEmpty(txtMaNV.Text);
            btnSua.Enabled = isRowSelected && SessionManager.CanUpdate(MODULE_NAME);
            btnXoa.Enabled = isRowSelected && SessionManager.CanDelete(MODULE_NAME);
        }
        #endregion

        #region CHỨC NĂNG CREATE
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Create, "thêm nhân viên")) return;
            ClearInputs();
            SetState(State.CREATE);

            try
            {
                // Gọi BLL để sinh Mã NV mới
                string newMaNV = _bll.SinhMaNVMoi();
                txtMaNV.Text = newMaNV;
                txtHoDem.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sinh Mã Nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetState(State.READ); // Trở về trạng thái READ nếu sinh mã lỗi
            }
        }
        #endregion

        #region CHỨC NĂNG UPDATE
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Update, "sửa nhân viên")) return;
            if (_currentState == State.READ && !string.IsNullOrEmpty(txtMaNV.Text))
            {
                SetState(State.UPDATE);
                txtHoDem.Focus();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một bản ghi để chỉnh sửa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region CHỨC NĂNG DELETE
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Delete, "xóa nhân viên")) return;
            string maNV = txtMaNV.Text.Trim();
            string hoTen = txtHoDem.Text.Trim() + " " + txtTen.Text.Trim();

            if (string.IsNullOrEmpty(maNV))
            {
                MessageBox.Show("Vui lòng chọn một Nhân viên để xóa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa Nhân viên:\n[ {maNV} - {hoTen} ] không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (_bll.XoaNhanVien(maNV))
                    {
                        MessageBox.Show("Xóa Nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Xóa Nhân viên thất bại. Có thể Nhân viên này có dữ liệu liên quan (ví dụ: tài khoản, phụ trách đầu sách...).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void btnMoTimKiem_Click(object sender, EventArgs e)
        {
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Search, "tìm kiếm nhân viên")) return;
            // Lấy metadata cho Nhân Viên
            List<FieldMetadata> nvMetadata = SearchMetadata.GetNhanVienFields();

            // Đảm bảo không tạo nhiều instance của Form tìm kiếm VÀ CHỈ ĐĂNG KÝ EVENT MỘT LẦN
            if (_searchForm == null || _searchForm.IsDisposed)
            {
                // 1. Khởi tạo Form tìm kiếm mới, truyền metadata
                _searchForm = new FrmTimKiem(nvMetadata);

                // 2. Đăng ký Event để nhận bộ lọc khi nút "Tìm" được nhấn (CHỈ 1 LẦN)
                // Dùng tên rõ ràng hơn cho hàm xử lý Event này
                _searchForm.OnSearchApplied += HandleSearchAppliedNhanVien;

                // 3. Xử lý sự kiện FormClosed để gỡ đăng ký Event và dọn dẹp biến _searchForm
                _searchForm.FormClosed += SearchForm_FormClosed;
            }

            // 4. Hiển thị Form non-modal (Không chặn Form cha)
            _searchForm.Show();
            _searchForm.BringToFront(); // Đưa Form tìm kiếm lên trên
        }

        // Hàm xử lý Event khi người dùng nhấn nút "Tìm" trong FrmTimKiem
        // Đã đổi tên hàm thành HandleSearchAppliedNhanVien để rõ ràng hơn
        private void HandleSearchAppliedNhanVien(List<SearchFilter> filters)
        {
            try
            {
                dgvDuLieu.DataSource = null;

                if (filters == null || filters.Count == 0)
                {
                    // Nếu không có bộ lọc, tải lại toàn bộ dữ liệu (trạng thái READ mặc định)
                    LoadData();
                }
                else
                {
                    // Thực hiện tìm kiếm với bộ lọc nhận được
                    List<NhanVienDTO> danhSachTimKiem = _bll.TimKiemNhanVien(filters);
                    dgvDuLieu.DataSource = danhSachTimKiem;

                    MessageBox.Show($"Tìm thấy {danhSachTimKiem.Count} kết quả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                dgvDuLieu.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                ClearInputs();
                btnHuy.Enabled = true; // Kích hoạt nút hủy bộ lọc
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thực hiện tìm kiếm Nhân Viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm xử lý khi Form tìm kiếm bị đóng
        private void SearchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_searchForm != null)
            {
                // Gỡ đăng ký Event để tránh rò rỉ bộ nhớ (memory leak)
                _searchForm.OnSearchApplied -= HandleSearchAppliedNhanVien;
                // Tùy chọn: Gọi LoadData() nếu bạn muốn làm mới dữ liệu khi Form tìm kiếm đóng
                // LoadData(); 
            }

            // Đặt _searchForm về null để lần sau khi click sẽ tạo Form mới
            _searchForm = null;
        }
        #endregion

        #region XỬ LÝ SỰ KIỆN CÁC NÚT - LƯU - HỦY
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                return;
            }

            NhanVienDTO model = GetModelFromInputs();

            if (_currentState == Helpers.State.CREATE)
            {
                try
                {
                    if (_bll.ThemNhanVien(model))
                    {
                        MessageBox.Show("Thêm Nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        SetState(Helpers.State.READ);
                    }
                    else
                    {
                        MessageBox.Show("Thêm Nhân viên thất bại. (Mã NV có thể đã tồn tại hoặc lỗi khác)", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống khi thêm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (_currentState == Helpers.State.UPDATE)
            {
                try
                {
                    if (_bll.CapNhatNhanVien(model))
                    {
                        MessageBox.Show("Cập nhật Nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        SetState(Helpers.State.READ);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật Nhân viên thất bại. (Không tìm thấy Mã NV)", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                if (!string.IsNullOrEmpty(_selectedMaNV))
                {
                    NhanVienDTO model = _bll.LayChiTietNhanVien(_selectedMaNV);
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

        #region HÀM BỔ TRỢ VÀ VALIDATION
        private void TriggerStatusEvent(State state)
        {
            string title = "QUẢN LÝ NHÂN VIÊN";
            Color backColor = Color.FromArgb(32, 36, 104); // Xanh
            Color foreColor = Color.White;

            switch (state)
            {
                case State.CREATE:
                    title = "THÊM MỚI NHÂN VIÊN";
                    backColor = Color.SeaGreen;
                    break;
                case State.UPDATE:
                    title = "CẬP NHẬT NHÂN VIÊN";
                    backColor = Color.DarkOrange;
                    break;
                case State.READ:
                default:
                    title = "DANH SÁCH NHÂN VIÊN";
                    backColor = Color.FromArgb(32, 36, 104);
                    break;
            }

            OnStatusRequest?.Invoke(this, new StatusRequestEventArgs(title, backColor, foreColor));
        }

        private void ClearInputs()
        {
            txtMaNV.Text = string.Empty;
            txtHoDem.Text = string.Empty;
            txtTen.Text = string.Empty;
            dtpNgaySinh.Value = DateTime.Now;
            cboGioiTinh.SelectedIndex = -1;
            txtDiaChi.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtEmail.Text = string.Empty;
            cboPhuTrach.SelectedIndex = -1; // Xóa Phụ Trách

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private NhanVienDTO GetModelFromInputs()
        {
            string gioiTinhCode = cboGioiTinh.SelectedItem?.ToString().Equals("Nam") == true ? "M" : "F";
            // Xử lý cboPhuTrach (Nếu là ComboBox)
            string phuTrach = cboPhuTrach.SelectedItem?.ToString() ?? cboPhuTrach.Text.Trim();

            return new NhanVienDTO
            {
                MaNV = txtMaNV.Text.Trim(),
                HoDem = txtHoDem.Text.Trim(),
                Ten = txtTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value,
                GioiTinh = gioiTinhCode,
                DiaChi = txtDiaChi.Text.Trim(),
                SDT = txtSDT.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                PhuTrach = phuTrach
            };
        }

        private bool ValidateInputs()
        {
            string maNV = txtMaNV.Text.Trim();
            string hoDem = txtHoDem.Text.Trim();
            string ten = txtTen.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string email = txtEmail.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string phuTrach = cboPhuTrach.Text.Trim(); // Lấy từ Text nếu không có SelectedItem (mode nhập tự do)

            // 1. Kiểm tra trường BẮT BUỘC (MaNV, HoDem, Ten, NgaySinh, GioiTinh, SDT, Email)
            if (string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(hoDem) || string.IsNullOrEmpty(ten) ||
                string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(email) || cboGioiTinh.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã NV, Họ Đệm, Tên, Giới Tính, Số Điện Thoại và Email.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 2. Kiểm tra định dạng Mã Nhân viên (MaNV CHAR(7))
            if (maNV.Length != 7)
            {
                MessageBox.Show("Mã Nhân viên phải có chính xác 7 ký tự (NVYY-##).", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 3. Kiểm tra Ngày Sinh hợp lệ
            if (dtpNgaySinh.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Ngày Sinh không hợp lệ (Không được sau ngày hiện tại).", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaySinh.Focus();
                return false;
            }

            // 4. Kiểm tra Định dạng Số Điện Thoại (Tái sử dụng Regex của Bạn Đọc)
            string vnPhonePattern = @"^(?:\+84|0)(3[2-9]|5[6|8|9]|7[0|6-9]|8[1-6|8|9]|9[0-4|6-9])\d{7}$";
            if (!Regex.IsMatch(sdt, vnPhonePattern))
            {
                MessageBox.Show("Số Điện Thoại không hợp lệ. Vui lòng nhập số điện thoại 10 chữ số (bắt đầu bằng 0).", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }

            // 5. Kiểm tra Định dạng Email
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show("Địa chỉ Email không đúng định dạng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // 6. Kiểm tra giới hạn ký tự (theo độ dài NVARCHAR của DB)
            if (hoDem.Length > 50)
            {
                MessageBox.Show("Họ Đệm không được vượt quá 50 ký tự.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoDem.Focus();
                return false;
            }
            if (ten.Length > 30)
            {
                MessageBox.Show("Tên không được vượt quá 30 ký tự.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return false;
            }
            if (diaChi.Length > 200)
            {
                MessageBox.Show("Địa Chỉ không được vượt quá 200 ký tự.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return false;
            }
            if (email.Length > 200)
            {
                MessageBox.Show("Email không được vượt quá 200 ký tự.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            // Kiểm tra trường PhuTrach (NVARCHAR(100) NULL) - Nếu không NULL thì phải kiểm tra
            if (phuTrach.Length > 100)
            {
                MessageBox.Show("Nội dung Phụ Trách không được vượt quá 100 ký tự.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboPhuTrach.Focus();
                return false;
            }

            return true;
        }
        #endregion
    }
}