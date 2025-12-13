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
using System.Text.RegularExpressions;
using System.Windows.Forms;
using LibraryManagerApp.GUI.Forms;

namespace LibraryManagerApp.GUI.UserControls.QLBanDoc
{
    public partial class ucFrmThongTinBanDoc : UserControl
    {
        private BLL.BanDocBLL _bll = new BLL.BanDocBLL();

        private State _currentState;
        private string _selectedMaBD = string.Empty;
        private FrmTimKiem _searchForm;
        public event EventHandler<StatusRequestEventArgs> OnStatusRequest;
        private const string MODULE_NAME = "BanDoc";

        #region KHỞI TẠO VÀ CẤU HÌNH
        public ucFrmThongTinBanDoc()
        {
            InitializeComponent();

            cboGioiTinh.Items.AddRange(new string[] { "Nam", "Nữ" });

            ConfigureDGV();
        }

        private void ucFrmThongTinBanDoc_Load(object sender, EventArgs e)
        {
            SetState(State.READ);

            LoadData();
        }

        private void ConfigureDGV()
        {
            // Cấu hình chung cho DataGridView
            dgvDuLieu.AutoGenerateColumns = false; // Tắt tự động sinh cột từ DataSource
            dgvDuLieu.ReadOnly = true;            // Chế độ chỉ đọc
            dgvDuLieu.AllowUserToAddRows = false; // Ẩn hàng trống cuối cùng
            dgvDuLieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Chọn toàn bộ hàng khi click

            // Tự động co giãn chiều rộng cột theo nội dung (Áp dụng cho các cột ngắn mặc định)
            dgvDuLieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDuLieu.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Cấu hình Font chữ Header (In đậm)
            dgvDuLieu.ColumnHeadersDefaultCellStyle.Font = new Font(dgvDuLieu.Font.FontFamily, 10f, FontStyle.Bold);
            dgvDuLieu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDuLieu.ColumnHeadersHeight = 30;
            dgvDuLieu.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Cấu hình Font chữ Cell (Thường)
            dgvDuLieu.DefaultCellStyle.Font = new Font(dgvDuLieu.Font.FontFamily, 10f, FontStyle.Regular);
            dgvDuLieu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Định nghĩa các cột
            if (dgvDuLieu.Columns.Count == 0)
            {
                // Các cột ngắn: Để mặc định (sẽ tự co giãn theo nội dung nhờ AutoSizeColumnsMode.AllCells ở trên)
                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã BD", DataPropertyName = "MaBD", Name = "MaBD" });
                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Họ Đệm", DataPropertyName = "HoDem", Name = "HoDem" });
                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên", DataPropertyName = "Ten", Name = "Ten" });
                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ngày Sinh", DataPropertyName = "NgaySinh", Name = "NgaySinh", DefaultCellStyle = { Format = "dd/MM/yyyy" } });
                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Giới Tính", DataPropertyName = "GioiTinhHienThi", Name = "GioiTinhHienThi" });

                // --- CỘT DÀI 1: ĐỊA CHỈ ---
                var colDiaChi = new DataGridViewTextBoxColumn { HeaderText = "Địa Chỉ", DataPropertyName = "DiaChi", Name = "DiaChi" };
                colDiaChi.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Tự động giãn để lấp đầy
                colDiaChi.FillWeight = 150; // Chiếm tỉ trọng lớn hơn
                colDiaChi.MinimumWidth = 200; // [QUAN TRỌNG] Đặt chiều rộng tối thiểu. Nếu form nhỏ hơn mức này, thanh cuộn sẽ hiện ra.
                dgvDuLieu.Columns.Add(colDiaChi);

                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "SĐT", DataPropertyName = "SDT", Name = "SDT" });

                // --- CỘT DÀI 2: EMAIL ---
                var colEmail = new DataGridViewTextBoxColumn { HeaderText = "Email", DataPropertyName = "Email", Name = "Email" };
                colEmail.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Tự động giãn để lấp đầy
                colEmail.FillWeight = 100;
                colEmail.MinimumWidth = 150; // [QUAN TRỌNG] Đặt chiều rộng tối thiểu để không bị ép quá nhỏ.
                dgvDuLieu.Columns.Add(colEmail);
            }
        }
        #endregion

        #region QUẢN LÝ TRẠNG THÁI (STATE)
        private void SetState (State state)
        {
            _currentState = state;

            bool isEditing = (state == State.CREATE || state == State.UPDATE);

            // Inputs
            txtMaBD.Enabled = (state == State.CREATE); // Chỉ cho phép nhập Mã BD khi tạo mới
            txtHoDem.Enabled = isEditing;
            txtTen.Enabled = isEditing;
            dtpNgaySinh.Enabled = isEditing;
            cboGioiTinh.Enabled = isEditing;
            txtDiaChi.Enabled = isEditing;
            txtSDT.Enabled = isEditing;
            txtEmail.Enabled = isEditing;

            // --- Buttons: Kết hợp Logic trạng thái VÀ Phân quyền ---

            // Nút Thêm: Chỉ hiện khi đang ở chế độ Đọc VÀ User có quyền CREATE
            btnThem.Enabled = (state == State.READ) && SessionManager.CanCreate(MODULE_NAME);

            // Nút Sửa: Chỉ hiện khi đang Đọc, đã chọn dòng VÀ User có quyền UPDATE
            btnSua.Enabled = (state == State.READ && !string.IsNullOrEmpty(txtMaBD.Text))
                             && SessionManager.CanUpdate(MODULE_NAME);

            // Nút Xóa: Chỉ hiện khi đang Đọc, đã chọn dòng VÀ User có quyền DELETE
            btnXoa.Enabled = (state == State.READ && !string.IsNullOrEmpty(txtMaBD.Text))
                             && SessionManager.CanDelete(MODULE_NAME);

            // Nút Tìm kiếm: Chỉ hiện khi đang Đọc VÀ User có quyền SEARCH
            btnTimKiem.Enabled = (state == State.READ) && SessionManager.CanSearch(MODULE_NAME);

            // Nút Xuất Excel: Chỉ hiện khi đang Đọc VÀ User có quyền EXPORT
            btnXuatExcel.Enabled = (state == State.READ) && SessionManager.CanExport(MODULE_NAME);

            btnLuu.Enabled = isEditing;
            btnHuy.Enabled = isEditing;

            btnTimKiem.Enabled = (state == State.READ);

            if (isEditing)
            {
                // TO DO: Ẩn DGV khi thêm/sửa nếu cần
            }

            TriggerStatusEvent(state);
        }
        #endregion

        #region CHỨC NĂNG READ
        private void LoadData()
        {
            try
            {
                // Lấy dữ liệu từ BLL
                dgvDuLieu.DataSource = null;
                List<BanDocDTO> danhSach = _bll.LayThongTinBanDoc();
                dgvDuLieu.DataSource = danhSach;

                // Tự động điều chỉnh kích thước cột
                dgvDuLieu.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadModelToInputs(BanDocDTO model)
        {
            txtMaBD.Text = model.MaBD;
            txtHoDem.Text = model.HoDem;
            txtTen.Text = model.Ten;
            dtpNgaySinh.Value = model.NgaySinh;
            txtDiaChi.Text = model.DiaChi;
            txtSDT.Text = model.SDT;
            txtEmail.Text = model.Email;

            // Xử lý Giới Tính: Chuyển 'M'/'F' sang "Nam"/"Nữ"
            cboGioiTinh.SelectedItem = model.GioiTinh.Equals("M") ? "Nam" : "Nữ";
        }

        private void dgvDuLieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra hàng hợp lệ
            if (e.RowIndex < 0 || e.RowIndex >= dgvDuLieu.RowCount)
                return;

            // Lấy MaBD từ hàng được chọn
            string maBD = dgvDuLieu.Rows[e.RowIndex].Cells["MaBD"].Value.ToString();

            _selectedMaBD = maBD;

            // Gọi BLL để lấy thông tin chi tiết đầy đủ (BanDocModel)
            BanDocDTO model = _bll.LayChiTietBanDoc(maBD);

            if (model != null)
            {
                LoadModelToInputs(model); 
            }

            // Cập nhật trạng thái nút sửa/xóa
            if (_currentState == State.READ)
            {
                bool isRowSelected = !string.IsNullOrEmpty(txtMaBD.Text);

                // [CHỈNH SỬA] Kiểm tra thêm quyền trước khi bật nút
                btnSua.Enabled = isRowSelected && SessionManager.CanUpdate(MODULE_NAME);
                btnXoa.Enabled = isRowSelected && SessionManager.CanDelete(MODULE_NAME);

            }
        }
        #endregion

        #region CHỨC NĂNG CREATE
        private void btnThem_Click(object sender, EventArgs e)
        {
            // [THÊM] Chặn nếu không có quyền
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Create)) return;

            ClearInputs();
            SetState(State.CREATE);
            txtMaBD.Focus();
        }
        #endregion

        #region CHỨC NĂNG UPDATE
        private void btnSua_Click(object sender, EventArgs e)
        {
            // [THÊM] Chặn nếu không có quyền
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Update)) return;

            // Đảm bảo có bản ghi được chọn và đang ở trạng thái READ
            if (_currentState == State.READ && !string.IsNullOrEmpty(txtMaBD.Text))
            {
                SetState(State.UPDATE);
                txtHoDem.Focus(); // Bắt đầu chỉnh sửa từ trường Họ Đệm
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
            // [THÊM] Chặn nếu không có quyền
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Delete)) return;

            // 1. Kiểm tra xem có bản ghi nào được chọn chưa
            string maBD = txtMaBD.Text.Trim();
            string hoTen = txtHoDem.Text.Trim() + " " + txtTen.Text.Trim();

            if (string.IsNullOrEmpty(maBD))
            {
                MessageBox.Show("Vui lòng chọn một Bạn Đọc để xóa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa Bạn Đọc:\n[ {maBD} - {hoTen} ] không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // 3. Xử lý kết quả xác nhận
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (_bll.XoaBanDoc(maBD))
                    {
                        MessageBox.Show("Xóa Bạn Đọc thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Tải lại danh sách
                        ClearInputs(); // Xóa dữ liệu trên Inputs
                                       // Không cần SetState vì đang ở State.READ
                    }
                    else
                    {
                        // Thông báo lỗi chung (có thể do khóa ngoại)
                        MessageBox.Show("Xóa Bạn Đọc thất bại. Có thể Bạn Đọc này có dữ liệu liên quan (ví dụ: đang mượn sách).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // [THÊM] Chặn nếu không có quyền
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Search)) return;

            // Đảm bảo không tạo nhiều instance của Form tìm kiếm VÀ CHỈ ĐĂNG KÝ EVENT MỘT LẦN
            if (_searchForm == null || _searchForm.IsDisposed)
            {
                _searchForm = new LibraryManagerApp.GUI.Forms.FrmTimKiem();

                // 1. Đăng ký Event (CHỈ 1 LẦN)
                _searchForm.OnSearchApplied += HandleSearchApplied;

                // 2. Xử lý khi Form tìm kiếm bị đóng
                _searchForm.FormClosed += SearchForm_FormClosed;
            }

            // 3. Hiển thị Form non-modal
            _searchForm.Show();
            _searchForm.BringToFront(); // Đưa lên trên
        }

        // Hàm xử lý Event khi người dùng nhấn nút "Tìm" trong frmTimKiem
        private void HandleSearchApplied(List<SearchFilter> filters)
        {
            try
            {
                dgvDuLieu.DataSource = null;

                // Load lại data gốc nếu không có bộ lọc
                if (filters == null || filters.Count == 0)
                {
                    // Nếu không có bộ lọc, tải lại toàn bộ dữ liệu (trạng thái READ mặc định)
                    LoadData();
                }
                else
                {
                    // Gọi BLL để thực hiện tìm kiếm với các bộ lọc
                    List<BanDocDTO> danhSachTimKiem = _bll.TimKiemBanDoc(filters);
                    dgvDuLieu.DataSource = danhSachTimKiem;
                    //Kích hoạt nút để hủy bộ lọc tìm kiếm
                    btnHuy.Enabled = true;

                    MessageBox.Show($"Tìm thấy {danhSachTimKiem.Count} kết quả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Điều chỉnh kích thước cột
                dgvDuLieu.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                // Xóa Inputs của Form cha để tập trung vào kết quả tìm kiếm
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thực hiện tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

   //      Hàm xử lý khi Form tìm kiếm bị đóng
        private void SearchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Hủy đăng ký Event để tránh rò rỉ bộ nhớ
            if (_searchForm != null)
            {
                _searchForm.OnSearchApplied -= HandleSearchApplied;
            }

            // Khôi phục DGV về trạng thái mặc định (Load lại toàn bộ dữ liệu)
            //LoadData();
            _searchForm = null;
        }


        #endregion

        #region CHỨC NĂNG XUẤT EXCEL

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            // [THÊM] Chặn nếu không có quyền
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Export)) return;

            try
            {
                // 1. Chuẩn bị dữ liệu nguồn (Lấy tất cả danh sách bạn đọc hiện có)
                // (Lưu ý: Nếu bạn muốn xuất danh sách đang tìm kiếm, hãy lưu danh sách đó vào một biến toàn cục khi tìm kiếm)
                List<BanDocDTO> dataList = _bll.LayThongTinBanDoc();

                if (dataList == null || dataList.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 2. Định nghĩa danh sách TẤT CẢ các cột có thể xuất
                // Key: Tên thuộc tính trong BanDocDTO
                // Value: Tên hiển thị trên Header Excel
                Dictionary<string, string> allColumns = new Dictionary<string, string>
                {
                    { "MaBD", "Mã Bạn Đọc" },
                    { "HoTen", "Họ và Tên" }, // Thuộc tính tính toán gộp Họ + Tên
                    { "NgaySinh", "Ngày Sinh" },
                    { "GioiTinhHienThi", "Giới Tính" }, // Hiển thị Nam/Nữ thay vì M/F
                    { "DiaChi", "Địa Chỉ" },
                    { "SDT", "Số Điện Thoại" },
                    { "Email", "Email" },
                    { "HoDem", "Họ Đệm" }, // Thêm tùy chọn tách riêng nếu cần
                    { "Ten", "Tên" }       // Thêm tùy chọn tách riêng nếu cần
                };

                // 3. Mở Form chọn cột
                frmChonCotXuatExcel frm = new frmChonCotXuatExcel(allColumns);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // 4. Lấy các cột người dùng đã chọn
                    Dictionary<string, string> selectedColumns = frm.SelectedColumns;

                    // 5. Mở hộp thoại chọn nơi lưu file
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                        sfd.FileName = $"DanhSachBanDoc_{DateTime.Now:ddMMyyyy}.xlsx"; // Tên file mặc định

                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            // 6. Gọi Helper để xuất file
                            bool success = ExcelHelper.ExportToExcel(dataList, selectedColumns, sfd.FileName);

                            if (success)
                            {
                                MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Tùy chọn: Mở file vừa xuất
                                // System.Diagnostics.Process.Start(sfd.FileName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            BanDocDTO model = GetModelFromInputs();

            // 1. Xử lý logic theo State CREATE (Đã có từ Bước 2)
            if (_currentState == Helpers.State.CREATE)
            {
                try
                {
                    if (_bll.ThemBanDoc(model))
                    {
                        MessageBox.Show("Thêm Bạn Đọc thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        SetState(Helpers.State.READ);
                    }
                    else
                    {
                        MessageBox.Show("Thêm Bạn Đọc thất bại. (Mã BD đã tồn tại)", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống khi thêm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            // 2. Xử lý logic theo State UPDATE
            else if (_currentState == Helpers.State.UPDATE)
            {
                // Không cần kiểm tra Mã BD tồn tại vì đang cập nhật bản ghi đã có
                try
                {
                    if (_bll.CapNhatBanDoc(model))
                    {
                        MessageBox.Show("Cập nhật Bạn Đọc thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Tải lại danh sách
                        SetState(Helpers.State.READ); // Chuyển về trạng thái READ
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật Bạn Đọc thất bại. (Không tìm thấy Mã BD)", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // 1. Nếu đang ở trạng thái TẠO MỚI: Chỉ cần xóa hết dữ liệu trên Inputs
                ClearInputs();
            }
            else if (_currentState == State.UPDATE)
            {
                // 2. Nếu đang ở trạng thái CẬP NHẬT: Khôi phục lại dữ liệu gốc của bản ghi đã chọn
                if (!string.IsNullOrEmpty(_selectedMaBD))
                {
                    // Tải lại dữ liệu chi tiết của bản ghi đang sửa
                    BanDocDTO model = _bll.LayChiTietBanDoc(_selectedMaBD);
                    if (model != null)
                    {
                        LoadModelToInputs(model); // Gọi hàm mới để tải dữ liệu (sẽ được tạo ở bước 3)
                    }
                    else
                    {
                        // Trường hợp ngoại lệ: Bản ghi gốc đã bị xóa bởi người dùng khác, ta chỉ cần Clear
                        ClearInputs();
                    }
                }
            }

            // Luôn luôn chuyển về trạng thái READ sau khi Hủy
            SetState(State.READ);
            LoadData(); // Tải lại dữ liệu để đồng bộ DGV
        }
        #endregion

        #region HÀM BỔ TRỢ
        private void TriggerStatusEvent(State state)
        {
            string title = "THÔNG TIN BẠN ĐỌC";
            Color backColor = Color.FromArgb(32, 36, 104); // Màu xanh mặc định (như logo)
            Color foreColor = Color.White;

            switch (state)
            {
                case State.CREATE:
                    title = "THÊM MỚI BẠN ĐỌC";
                    backColor = Color.SeaGreen; // Màu xanh lá cho thêm mới
                    break;
                case State.UPDATE:
                    title = "CẬP NHẬT BẠN ĐỌC";
                    backColor = Color.DarkOrange; // Màu cam cho chỉnh sửa
                    break;
                case State.READ:
                default:
                    title = "XEM THÔNG TIN BẠN ĐỌC";
                    backColor = Color.FromArgb(32, 36, 104);
                    break;
            }

            // Bắn sự kiện ra ngoài cho Form cha bắt
            OnStatusRequest?.Invoke(this, new StatusRequestEventArgs(title, backColor, foreColor));
        }

        private void ClearInputs()
        {
            txtMaBD.Text = string.Empty;
            txtHoDem.Text = string.Empty;
            txtTen.Text = string.Empty;
            dtpNgaySinh.Value = DateTime.Now;
            cboGioiTinh.SelectedIndex = -1;
            txtDiaChi.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtEmail.Text = string.Empty;

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private BanDocDTO GetModelFromInputs()
        {
            // Chuyển "Nam"/"Nữ" sang 'M'/'F' để lưu vào DB
            string gioiTinhCode = cboGioiTinh.SelectedItem.ToString().Equals("Nam") ? "M" : "F";

            return new BanDocDTO
            {
                MaBD = txtMaBD.Text.Trim(),
                HoDem = txtHoDem.Text.Trim(),
                Ten = txtTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value,
                GioiTinh = gioiTinhCode,
                DiaChi = txtDiaChi.Text.Trim(),
                SDT = txtSDT.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };
        }

        private bool ValidateInputs()
        {
            // Lấy dữ liệu đã Trim() để kiểm tra
            string maBD = txtMaBD.Text.Trim();
            string hoDem = txtHoDem.Text.Trim();
            string ten = txtTen.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string email = txtEmail.Text.Trim();

            // 1. Kiểm tra trường BẮT BUỘC (theo NOT NULL của DB)
            if (string.IsNullOrEmpty(maBD) || string.IsNullOrEmpty(hoDem) || string.IsNullOrEmpty(ten) ||
                string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(email) || cboGioiTinh.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã BD, Họ Đệm, Tên, Giới Tính, Số Điện Thoại và Email.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 2. Kiểm tra định dạng Mã Bạn Đọc (MaBD CHAR(9))
            if (maBD.Length != 9)
            {
                MessageBox.Show("Mã Bạn Đọc phải có chính xác 9 ký tự.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaBD.Focus();
                return false;
            }

            // (ĐÃ THÊM Ở BLL - Chú ý: Nếu đây là trạng thái CREATE, bạn cần kiểm tra MaBD đã tồn tại chưa ở lớp BLL.)

            // 3. Kiểm tra Ngày Sinh hợp lệ (DATE NOT NULL)
            // Ngày sinh không được sau ngày hiện tại
            if (dtpNgaySinh.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Ngày Sinh không hợp lệ (Không được sau ngày hiện tại).", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaySinh.Focus();
                return false;
            }

            // 4. Kiểm tra Định dạng Số Điện Thoại
            // Regex này chấp nhận đầu số di động Việt Nam: 03, 05, 07, 08, 09 hoặc +84...
            string vnPhonePattern = @"^(?:\+84|0)(3[2-9]|5[6|8|9]|7[0|6-9]|8[1-6|8|9]|9[0-4|6-9])\d{7}$";

            if (!Regex.IsMatch(sdt, vnPhonePattern))
            {
                // Bổ sung ví dụ cụ thể vào thông báo
                MessageBox.Show("Số Điện Thoại không hợp lệ.\n" +
                                "Vui lòng nhập đúng số di động Việt Nam (10 số).\n" +
                                "Ví dụ: 0912345678 hoặc +84912345678",
                                "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }

            // 5. Kiểm tra Định dạng Email
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$";

            if (!Regex.IsMatch(email, emailPattern))
            {
                // Bổ sung ví dụ cụ thể vào thông báo
                MessageBox.Show("Địa chỉ Email không đúng định dạng.\n" +
                                "Vui lòng nhập theo mẫu: ten_tai_khoan@ten_mien\n" +
                                "Ví dụ: example@gmail.com",
                                "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            return true;
        }
        #endregion
       
    }
}
