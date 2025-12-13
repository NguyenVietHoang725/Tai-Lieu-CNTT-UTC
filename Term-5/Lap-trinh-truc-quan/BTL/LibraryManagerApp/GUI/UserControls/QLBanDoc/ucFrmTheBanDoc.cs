// File: LibraryManagerApp.GUI.UserControls.QLBanDoc/ucFrmTheBanDoc.cs

using LibraryManagerApp.BLL;
using LibraryManagerApp.DAL; // Cần thiết cho BanDocChuaCoTheDTO
using LibraryManagerApp.DTO;
using LibraryManagerApp.GUI.Forms;
using LibraryManagerApp.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LibraryManagerApp.GUI.UserControls.QLBanDoc
{
    public partial class ucFrmTheBanDoc : UserControl
    {
        private TheBanDocBLL _bll = new BLL.TheBanDocBLL();
        private State _currentState;
        private string _selectedMaTBD = string.Empty;
        private FrmTimKiem _searchForm;
        public event EventHandler<StatusRequestEventArgs> OnStatusRequest;
        private const string MODULE_NAME = "TheBanDoc";

        #region KHỞI TẠO VÀ CẤU HÌNH
        public ucFrmTheBanDoc()
        {
            InitializeComponent();

            cboTrangThai.Items.AddRange(new string[] { "Hoạt động", "Khóa" });

            ConfigureDGV();
        }
        
        private void ucFrmTheBanDoc_Load(object sender, EventArgs e)
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

            // Tự động co giãn cột ngắn, dòng tự động cao
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
                // Các cột ngắn (Mã, Ngày, Trạng thái)
                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã Thẻ", DataPropertyName = "MaTBD", Name = "MaTBD" });
                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã BĐ", DataPropertyName = "MaBD", Name = "MaBD" });

                // Cột Dài 1: Họ Tên Bạn Đọc (Fill)
                var colTenBD = new DataGridViewTextBoxColumn { HeaderText = "Họ Tên Bạn Đọc", DataPropertyName = "HoTenBD", Name = "HoTenBD" };
                colTenBD.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colTenBD.FillWeight = 150;
                colTenBD.MinimumWidth = 150;
                dgvDuLieu.Columns.Add(colTenBD);

                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã TK", DataPropertyName = "MaTK", Name = "MaTK" });

                // Cột Dài 2: Nhân Viên Cấp (Fill)
                var colTenNV = new DataGridViewTextBoxColumn { HeaderText = "Nhân Viên Cấp", DataPropertyName = "HoTenNV", Name = "HoTenNV" };
                colTenNV.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colTenNV.FillWeight = 120;
                colTenNV.MinimumWidth = 120;
                dgvDuLieu.Columns.Add(colTenNV);

                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ngày Cấp", DataPropertyName = "NgayCap", Name = "NgayCap", DefaultCellStyle = { Format = "dd/MM/yyyy", Alignment = DataGridViewContentAlignment.MiddleCenter } });
                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Hết Hạn", DataPropertyName = "NgayHetHanHienThi", Name = "NgayHetHanHienThi", DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
                dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Trạng Thái", DataPropertyName = "TrangThai", Name = "TrangThai" });
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
            txtMaTBD.Enabled = false;
            cboBanDoc.Enabled = isCreating;
            txtMaTK.Enabled = false;
            txtHoTenNV.Enabled = false;

            if (isCreating)
            {
                if (SessionManager.IsLoggedIn)
                {
                    txtMaTK.Text = SessionManager.GetMaTaiKhoan();
                    txtHoTenNV.Text = SessionManager.CurrentUser.HoTenNV;
                }
                else
                {
                    txtMaTK.Text = string.Empty;
                    txtHoTenNV.Text = string.Empty;
                }
            }
            else if (state == State.READ)
            {
                cboBanDoc.DataSource = null;
                cboBanDoc.Text = string.Empty;
                txtMaTK.Text = string.Empty;
                txtHoTenNV.Text = string.Empty;
            }

            dtpNgayCap.Enabled = isEditing;
            dtpNgayHetHan.Enabled = isEditing;
            cboTrangThai.Enabled = isEditing;

            // Buttons
            // Nút Thêm: Chỉ hiện khi đang ở chế độ Đọc VÀ User có quyền CREATE
            btnThem.Enabled = (state == State.READ) && SessionManager.CanCreate(MODULE_NAME);

            // Nút Sửa: Chỉ hiện khi đang Đọc, đã chọn dòng VÀ User có quyền UPDATE
            btnSua.Enabled = (state == State.READ && !string.IsNullOrEmpty(txtMaTBD.Text))
                             && SessionManager.CanUpdate(MODULE_NAME);

            // Nút Xóa: Chỉ hiện khi đang Đọc, đã chọn dòng VÀ User có quyền DELETE
            btnXoa.Enabled = (state == State.READ && !string.IsNullOrEmpty(txtMaTBD.Text))
                             && SessionManager.CanDelete(MODULE_NAME);

            // Nút Xuất Thẻ (Print): Chỉ hiện khi đang Đọc, đã chọn dòng VÀ User có quyền PRINT
            btnXuatThe.Enabled = (state == State.READ && !string.IsNullOrEmpty(txtMaTBD.Text))
                                 && SessionManager.CanPrint(MODULE_NAME);

            // Nút Xuất Excel: Chỉ hiện khi đang Đọc VÀ User có quyền EXPORT
            btnXuatExcel.Enabled = (state == State.READ) && SessionManager.CanExport(MODULE_NAME);

            // Nút Tìm kiếm: Chỉ hiện khi đang Đọc VÀ User có quyền SEARCH
            btnTimKiem.Enabled = (state == State.READ) && SessionManager.CanSearch(MODULE_NAME);

            // Nút Lưu/Hủy
            btnLuu.Enabled = isEditing;
            btnHuy.Enabled = isEditing;

            if (isCreating)
            {
                LoadBanDocVaoCombo();
            }

            // 2. Gọi hàm cập nhật tiêu đề
            TriggerStatusEvent(state);
        }
        #endregion

        #region CHỨC NĂNG READ
        private void LoadData()
        {
            try
            {
                dgvDuLieu.DataSource = null;
                List<TheBanDocDTO> danhSach = _bll.LayThongTinTheBanDoc();
                dgvDuLieu.DataSource = danhSach;

                dgvDuLieu.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Thẻ Bạn Đọc: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBanDocVaoCombo()
        {
            try
            {
                // Sử dụng DTO đã định nghĩa trong BanDocDAL
                List<BanDocChuaCoTheDTO> danhSach = _bll.LayBanDocChuaCoThe();

                // Tạo danh sách Anonymous/DTO mới để hiển thị Mã BD
                var danhSachCombo = danhSach.Select(bd => new
                {
                    MaBD = bd.MaBD,
                    HoTenVaMa = $"{bd.MaBD} - {bd.HoTen}" // Trường hiển thị mới
                }).ToList();

                cboBanDoc.DataSource = danhSachCombo;

                // Cập nhật DisplayMember và ValueMember
                cboBanDoc.DisplayMember = "HoTenVaMa"; // <<< Hiển thị: MaBD - HoTen
                cboBanDoc.ValueMember = "MaBD";        // <<< Giá trị lấy: MaBD

                cboBanDoc.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách Bạn Đọc: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadModelToInputs(TheBanDocDTO model)
        {
            txtMaTBD.Text = model.MaTBD;

            // Hiển thị MaBD và HoTenBD của thẻ đã tồn tại
            cboBanDoc.Enabled = false;
            cboBanDoc.Text = $"{model.MaBD} - {model.HoTenBD}";

            txtMaTK.Text = model.MaTK;
            txtHoTenNV.Text = model.HoTenNV;
            dtpNgayCap.Value = model.NgayCap;
            dtpNgayHetHan.Value = model.NgayHetHan ?? model.NgayCap.AddYears(4); // Nếu DB trả về NULL (rất hiếm), tính lại
            cboTrangThai.SelectedItem = model.TrangThai;
        }

        private void dgvDuLieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvDuLieu.RowCount)
                return;

            string maTBD = dgvDuLieu.Rows[e.RowIndex].Cells["MaTBD"].Value.ToString();

            _selectedMaTBD = maTBD;

            TheBanDocDTO model = _bll.LayChiTietTheBanDoc(maTBD);

            if (model != null)
            {
                LoadModelToInputs(model);
            }

            if (_currentState == State.READ)
            {
                bool isRowSelected = !string.IsNullOrEmpty(txtMaTBD.Text);
                // Thêm kiểm tra quyền
                btnSua.Enabled = isRowSelected && SessionManager.CanUpdate(MODULE_NAME);
                btnXoa.Enabled = isRowSelected && SessionManager.CanDelete(MODULE_NAME);
                btnXuatThe.Enabled = isRowSelected && SessionManager.CanPrint(MODULE_NAME);
            }

        }
        #endregion

        #region CHỨC NĂNG CREATE
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Create, "cấp thẻ bạn đọc")) return;
            ClearInputs();

            dtpNgayCap.Value = DateTime.Now.Date;
            dtpNgayHetHan.Value = DateTime.Now.Date.AddYears(4);

            SetState(State.CREATE);
            cboBanDoc.Focus();
        }
        #endregion

        #region CHỨC NĂNG UPDATE
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Update, "sửa thẻ bạn đọc")) return;
            if (_currentState == State.READ && !string.IsNullOrEmpty(txtMaTBD.Text))
            {
                SetState(State.UPDATE);
                dtpNgayCap.Focus();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một Thẻ Bạn Đọc để chỉnh sửa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region CHỨC NĂNG DELETE
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Delete, "xóa thẻ bạn đọc")) return;
            // 1. Kiểm tra xem có bản ghi nào được chọn chưa
            string maTBD = txtMaTBD.Text.Trim();
            string hoTenBD = cboBanDoc.Text.Trim(); // Lấy tên hiển thị khi ở trạng thái READ

            if (string.IsNullOrEmpty(maTBD))
            {
                MessageBox.Show("Vui lòng chọn một Thẻ Bạn Đọc để xóa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa Thẻ Bạn Đọc:\n[ {maTBD} - {hoTenBD} ] không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // 3. Xử lý kết quả xác nhận
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (_bll.XoaTheBanDoc(maTBD))
                    {
                        MessageBox.Show("Xóa Thẻ Bạn Đọc thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Tải lại danh sách
                        ClearInputs(); // Xóa dữ liệu trên Inputs
                        // SetState(State.READ) đã được thực hiện bởi ClearInputs
                    }
                    else
                    {
                        // Thông báo lỗi chung (có thể do khóa ngoại, thẻ đang hoạt động,...)
                        MessageBox.Show("Xóa Thẻ Bạn Đọc thất bại. Có thể Thẻ đang được sử dụng hoặc có ràng buộc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Search, "tìm kiếm thẻ bạn đọc")) return;
            // Lấy metadata cần thiết (chỉ làm 1 lần)
            List<FieldMetadata> tbdMetadata = _bll.GetSearchFields();

            // Đảm bảo không tạo nhiều instance của Form tìm kiếm VÀ CHỈ ĐĂNG KÝ EVENT MỘT LẦN
            if (_searchForm == null || _searchForm.IsDisposed)
            {
                // Truyền metadata vào constructor của Form tìm kiếm
                _searchForm = new FrmTimKiem(tbdMetadata);

                // 1. Đăng ký Event để nhận bộ lọc (CHỈ 1 LẦN)
                _searchForm.OnSearchApplied += HandleSearchAppliedTheBanDoc;

                // Tùy chọn: Xử lý sự kiện FormClosed nếu bạn muốn giải phóng tài nguyên hoặc 
                // thực hiện hành động nào đó khi người dùng đóng Form tìm kiếm.
                _searchForm.FormClosed += SearchForm_FormClosed;
            }

            // 2. Hiển thị Form non-modal
            _searchForm.Show();
            _searchForm.BringToFront(); // Đưa Form tìm kiếm lên trên
        }

        // Hàm xử lý Event khi người dùng nhấn nút "Tìm" trong frmTimKiem
        private void HandleSearchAppliedTheBanDoc(List<SearchFilter> filters)
        {
            try
            {           
                LoadDataWithFilters(filters);

                // Điều chỉnh kích thước cột (Nếu cần)
                dgvDuLieu.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                // Xóa Inputs của Form cha để tập trung vào kết quả tìm kiếm (Nếu có)
                // ClearInputs(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thực hiện tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Phương thức này giữ nguyên nhưng đã được gọi từ Event Handler mới
        private void LoadDataWithFilters(List<SearchFilter> filters)
        {
            if (filters == null || filters.Count == 0)
            {
                LoadData();
            }
            else
            {
                List<TheBanDocDTO> danhSach = _bll.TimKiemTheBanDoc(filters);
                dgvDuLieu.DataSource = danhSach;
                MessageBox.Show($"Tìm thấy {danhSach.Count} kết quả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            btnHuy.Enabled = true;
        }
        private void SearchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_searchForm != null)
            {
                // 1. Gỡ đăng ký Event OnSearchApplied để tránh memory leak
                _searchForm.OnSearchApplied -= HandleSearchAppliedTheBanDoc;

                // 2. Gỡ đăng ký Event FormClosed (tùy chọn, nhưng là thực hành tốt)
                _searchForm.FormClosed -= SearchForm_FormClosed;
            }

            // Đặt biến tham chiếu về null để lần sau click nút "Tìm Kiếm" sẽ tạo Form mới
            _searchForm = null;

            // Tùy chọn: Gọi LoadData() nếu bạn muốn dữ liệu hiển thị toàn bộ ngay khi Form tìm kiếm đóng
            // LoadData(); 
        }
        #endregion

        #region CHỨC NĂNG IN ẤN / BÁO CÁO

        private void btnXuatThe_Click(object sender, EventArgs e)
        {
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Print, "in thẻ bạn đọc")) return;
            // 1. Kiểm tra xem đã chọn thẻ nào chưa (dựa vào biến _selectedMaTBD được gán khi Click DGV)
            if (string.IsNullOrEmpty(_selectedMaTBD))
            {
                MessageBox.Show("Vui lòng chọn một Thẻ Bạn Đọc từ danh sách để in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // 2. Lấy thông tin chi tiết của thẻ (bao gồm cả thông tin cá nhân bạn đọc mới thêm vào DTO)
                TheBanDocDTO theBanDoc = _bll.LayChiTietTheBanDoc(_selectedMaTBD);

                if (theBanDoc != null)
                {
                    // 3. Form báo cáo nhận vào một List, nên ta tạo List chứa 1 phần tử
                    List<TheBanDocDTO> listData = new List<TheBanDocDTO>();
                    listData.Add(theBanDoc);

                    // 4. Khởi tạo và hiển thị Form Báo cáo
                    frmBaoCaoTheBanDoc frm = new frmBaoCaoTheBanDoc(listData);
                    frm.ShowDialog(); // Hiện dưới dạng popup
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin chi tiết của thẻ này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chuẩn bị dữ liệu in: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region CHỨC NĂNG XUẤT EXCEL

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Export, "xuất excel thẻ bạn đọc")) return;
            try
            {
                // 1. Chuẩn bị dữ liệu nguồn (Lấy tất cả hoặc lấy theo tìm kiếm hiện tại)
                // Ở đây ta lấy tất cả danh sách hiện có
                List<TheBanDocDTO> dataList = _bll.LayThongTinTheBanDoc();

                if (dataList == null || dataList.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 2. Định nghĩa danh sách TẤT CẢ các cột có thể xuất
                // Key: Tên thuộc tính trong DTO
                // Value: Tên hiển thị trên Header Excel
                Dictionary<string, string> allColumns = new Dictionary<string, string>
                {
                    { "MaTBD", "Mã Thẻ" },
                    { "MaBD", "Mã Bạn Đọc" },
                    { "HoTenBD", "Họ và Tên" },
                    { "NgaySinh", "Ngày Sinh" },
                    { "GioiTinh", "Giới Tính" },
                    { "SDT", "Số Điện Thoại" },
                    { "DiaChi", "Địa Chỉ" },
                    { "NgayCap", "Ngày Cấp Thẻ" },
                    { "NgayHetHanHienThi", "Ngày Hết Hạn" }, // Dùng thuộc tính hiển thị đã format sẵn
                    { "TrangThai", "Trạng Thái" },
                    { "HoTenNV", "Nhân Viên Cấp" }
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
                        sfd.FileName = $"DanhSachTheBanDoc_{DateTime.Now:ddMMyyyy}.xlsx"; // Tên mặc định

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

            TheBanDocDTO model = GetModelFromInputs();
            int errorStatus;

            // Bắt đầu khối try-catch bao quanh toàn bộ thao tác DB
            try
            {
                if (_currentState == State.CREATE)
                {
                    // --- LOGIC CREATE ---
                    string newMaTBD = _bll.ThemTheBanDoc(model, out errorStatus);

                    if (errorStatus == 0)
                    {
                        MessageBox.Show("Tạo Thẻ Bạn Đọc thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        SetState(State.READ);
                        _selectedMaTBD = newMaTBD;
                    }
                    else
                    {
                        // Xử lý lỗi nghiệp vụ từ BLL/SP
                        string errorMessage = GetErrorMessage(errorStatus);
                        MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (_currentState == State.UPDATE)
                {
                    // --- LOGIC UPDATE ---
                    model.MaTBD = _selectedMaTBD;

                    if (_bll.CapNhatTheBanDoc(model))
                    {
                        MessageBox.Show("Cập nhật Thẻ Bạn Đọc thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        SetState(State.READ);
                    }
                    else
                    {
                        // Lỗi nghiệp vụ (ví dụ: MaTK không tồn tại, ràng buộc NgayHetHan)
                        MessageBox.Show("Cập nhật Thẻ Bạn Đọc thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Bắt lỗi hệ thống/lỗi ngoại lệ không mong muốn
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (!string.IsNullOrEmpty(_selectedMaTBD))
                {
                    try
                    {
                        // Tải lại dữ liệu chi tiết của bản ghi đang sửa
                        TheBanDocDTO model = _bll.LayChiTietTheBanDoc(_selectedMaTBD);
                        if (model != null)
                        {
                            LoadModelToInputs(model);
                        }
                        else
                        {
                            // Trường hợp ngoại lệ: Bản ghi gốc đã bị xóa bởi người dùng khác
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

            // Luôn luôn chuyển về trạng thái READ sau khi Hủy
            SetState(State.READ);
            LoadData();
        }

        #endregion

        #region HÀM BỔ TRỢ
        // Hàm hỗ trợ chọn màu và text dựa trên trạng thái
        private void TriggerStatusEvent(State state)
        {
            string title = "QUẢN LÝ THẺ BẠN ĐỌC";
            Color backColor = Color.FromArgb(32, 36, 104); // Màu xanh mặc định
            Color foreColor = Color.White;

            switch (state)
            {
                case State.CREATE:
                    title = "CẤP THẺ BẠN ĐỌC MỚI";
                    backColor = Color.SeaGreen;
                    break;
                case State.UPDATE:
                    title = "CẬP NHẬT TRẠNG THÁI THẺ";
                    backColor = Color.DarkOrange;
                    break;
                case State.READ:
                default:
                    title = "DANH SÁCH THẺ BẠN ĐỌC";
                    backColor = Color.FromArgb(32, 36, 104);
                    break;
            }

            // Bắn sự kiện ra ngoài cho Form cha bắt
            OnStatusRequest?.Invoke(this, new StatusRequestEventArgs(title, backColor, foreColor));
        }

        private void ClearInputs()
        {
            txtMaTBD.Text = string.Empty;
            cboBanDoc.DataSource = null;
            cboBanDoc.Text = string.Empty;

            txtMaTK.Text = string.Empty;
            txtHoTenNV.Text = string.Empty;

            dtpNgayCap.Value = DateTime.Now.Date;
            dtpNgayHetHan.Value = DateTime.Now.Date.AddYears(4);
            cboTrangThai.SelectedIndex = cboTrangThai.Items.IndexOf("Hoạt động"); // Đặt về "Hoạt động"

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private TheBanDocDTO GetModelFromInputs()
        {
            // Lấy Mã BD cho CREATE/UPDATE
            string maBDHienTai;
            if (_currentState == State.CREATE)
            {
                // Lấy MaBD từ SelectedValue của ComboBox
                maBDHienTai = cboBanDoc.SelectedValue?.ToString() ?? string.Empty;
            }
            else // READ hoặc UPDATE
            {
                // Lấy MaBD bằng cách cắt chuỗi MaTBD (vì MaTBD = TBD + MaBD)
                // Cần đảm bảo txtMaTBD.Text có đủ 12 ký tự và bắt đầu bằng "TBD"
                if (txtMaTBD.Text.Length == 12 && txtMaTBD.Text.StartsWith("TBD"))
                {
                    maBDHienTai = txtMaTBD.Text.Substring(3);
                }
                else
                {
                    maBDHienTai = "UNKNOWN"; // Giá trị mặc định nếu không hợp lệ
                }
            }

            return new TheBanDocDTO
            {
                MaTBD = txtMaTBD.Text.Trim(),
                MaBD = maBDHienTai,
                MaTK = txtMaTK.Text.Trim(),
                NgayCap = dtpNgayCap.Value.Date,
                NgayHetHan = dtpNgayHetHan.Value.Date,
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Hoạt động"
            };
        }

        private bool ValidateInputs()
        {
            // Lấy dữ liệu đã Trim() để kiểm tra
            string maTK = txtMaTK.Text.Trim();

            // 1. Kiểm tra trường BẮT BUỘC (theo NOT NULL của DB)
            // Các trường NOT NULL: MaBD (kiểm tra qua cbo), MaTK, NgayCap, TrangThai (kiểm tra qua cbo)

            // Kiểm tra cboBanDoc chỉ cần thiết khi CREATE
            if (_currentState == State.CREATE && (cboBanDoc.SelectedIndex == -1 || string.IsNullOrEmpty(cboBanDoc.SelectedValue?.ToString())))
            {
                MessageBox.Show("Vui lòng chọn Mã/Tên Bạn Đọc chưa có thẻ.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboBanDoc.Focus();
                return false;
            }

            // Kiểm tra MaTK và cboTrangThai (NOT NULL)
            if (string.IsNullOrEmpty(maTK) || cboTrangThai.SelectedIndex == -1)
            {
                MessageBox.Show("Mã Tài Khoản và Trạng Thái không được rỗng.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 2. Kiểm tra định dạng Mã Tài Khoản (MaTK CHAR(7))
            if (maTK.Length != 7)
            {
                MessageBox.Show("Mã Tài Khoản phải có chính xác 7 ký tự.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaTK.Focus();
                return false;
            }

            // 3. Kiểm tra Ngày Cấp hợp lệ (DATE NOT NULL)
            // Ngày cấp không được sau ngày hiện tại
            if (dtpNgayCap.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Ngày Cấp Thẻ không hợp lệ (Không được sau ngày hiện tại).", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayCap.Focus();
                return false;
            }

            // 4. Kiểm tra Ngày Hết Hạn >= Ngày Cấp (Theo ràng buộc CHK)
            if (dtpNgayHetHan.Value.Date < dtpNgayCap.Value.Date)
            {
                MessageBox.Show("Ngày Hết Hạn phải lớn hơn hoặc bằng Ngày Cấp.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayHetHan.Focus();
                return false;
            }

            // 5. Kiểm tra trạng thái (nếu là CREATE, chỉ cho phép "Hoạt động")
            if (_currentState == State.CREATE && cboTrangThai.SelectedItem.ToString() != "Hoạt động")
            {
                MessageBox.Show("Khi tạo mới, trạng thái mặc định phải là 'Hoạt động'.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Không cần kiểm tra giới hạn ký tự cho các trường khác vì chúng là kết quả JOIN hoặc DatePicker đã kiểm soát.

            return true;
        }

        private string GetErrorMessage(int status)
        {
            switch (status)
            {
                case 1: return "Lỗi: Mã Bạn Đọc không tồn tại trong hệ thống.";
                case 2: return "Lỗi: Bạn Đọc này đã được cấp Thẻ.";
                case 3: return "Lỗi: Chiều dài Mã Thẻ không hợp lệ (Liên hệ quản trị).";
                case 4: return "Lỗi nghiệp vụ: Ngày Cấp Thẻ không hợp lệ.";
                case 99: return "Lỗi hệ thống: Không thể lưu Thẻ Bạn Đọc vào CSDL.";
                default: return "Lỗi không xác định.";
            }
        }
        #endregion
    }
}