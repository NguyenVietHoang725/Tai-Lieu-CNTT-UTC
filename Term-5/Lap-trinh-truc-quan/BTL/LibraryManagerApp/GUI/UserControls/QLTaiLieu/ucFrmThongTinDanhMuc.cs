using LibraryManagerApp.BLL;
using LibraryManagerApp.DAL;
using LibraryManagerApp.DTO;
using LibraryManagerApp.GUI.Forms;
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
using LibraryManagerApp.GUI.Forms;
using LibraryManagerApp.Helpers;

namespace LibraryManagerApp.GUI.UserControls.QLTaiLieu
{
    public partial class ucFrmThongTinDanhMuc : UserControl
    {
        // Khai báo BLL chuyên biệt cho các danh mục
        private TacGiaBLL _tacGiaBll = new TacGiaBLL();
        private NxbBLL _nxbBll = new NxbBLL();
        private TheLoaiBLL _thlBll = new TheLoaiBLL();
        private DinhDangBLL _ddBll = new DinhDangBLL();

        private State _currentState;
        private string _selectedMaDM = string.Empty; // Dùng MaTG
        private List<SearchFilter> _currentFilters;
        private FrmTimKiem _searchForm;
        // Khai báo Enum để quản lý các loại danh mục
        private enum LoaiDanhMuc { TacGia, TheLoai, DinhDang, NhaXuatBan }
        private LoaiDanhMuc _currentDanhMuc = LoaiDanhMuc.TacGia; // Mặc định là Tác giả
        public event EventHandler<StatusRequestEventArgs> OnStatusRequest;
        private const string MODULE_NAME = "DanhMuc";

        #region KHỞI TẠO VÀ CẤU HÌNH
        public ucFrmThongTinDanhMuc()
        {
            InitializeComponent();

            ConfigureDGV();
        }

        private void ucFrmThongTinDanhMuc_Load(object sender, EventArgs e)
        {
            // Load các danh mục chính vào cboDanhMuc
            LoadLoaiDanhMuc();

            // Load Quốc gia (cần cho cả NXB và Tác giả)
            LoadQuocGiaVaoCombo();

            // Thiết lập trạng thái READ ban đầu
            SetState(State.READ);
        }

        private void ConfigureDGV()
        {
            // Cấu hình Style chung
            dgvDuLieu.AutoGenerateColumns = false;
            dgvDuLieu.ReadOnly = true;
            dgvDuLieu.AllowUserToAddRows = false;
            dgvDuLieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDuLieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDuLieu.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgvDuLieu.ColumnHeadersDefaultCellStyle.Font = new Font(dgvDuLieu.Font.FontFamily, 10f, FontStyle.Bold);
            dgvDuLieu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDuLieu.ColumnHeadersHeight = 30;
            dgvDuLieu.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvDuLieu.DefaultCellStyle.Font = new Font(dgvDuLieu.Font.FontFamily, 10f, FontStyle.Regular);
            dgvDuLieu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Cấu hình ban đầu
            ConfigureDGVForTacGia();
        }

        private void ConfigureDGVForTacGia()
        {
            dgvDuLieu.Columns.Clear();
            dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã TG", DataPropertyName = "MaTG", Name = "MaTG" });

            // Tên Tác giả: Fill
            var colTen = new DataGridViewTextBoxColumn { HeaderText = "Họ Tên", DataPropertyName = "HoTen", Name = "HoTen" };
            colTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTen.MinimumWidth = 150;
            dgvDuLieu.Columns.Add(colTen);

            dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã QG", DataPropertyName = "MaQG", Name = "MaQG" });

            // Quốc Gia: Fill nhẹ
            var colQG = new DataGridViewTextBoxColumn { HeaderText = "Quốc Gia", DataPropertyName = "TenQG", Name = "TenQG" };
            colQG.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colQG.MinimumWidth = 100;
            dgvDuLieu.Columns.Add(colQG);
        }

        private void ConfigureDGVForNxb()
        {
            dgvDuLieu.Columns.Clear();
            dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã NXB", DataPropertyName = "MaNXB", Name = "MaNXB" });

            // Tên NXB: Fill
            var colTen = new DataGridViewTextBoxColumn { HeaderText = "Tên NXB", DataPropertyName = "TenNXB", Name = "TenNXB" };
            colTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTen.MinimumWidth = 200;
            dgvDuLieu.Columns.Add(colTen);

            var colQG = new DataGridViewTextBoxColumn { HeaderText = "Quốc Gia", DataPropertyName = "TenQG", Name = "TenQG" };
            colQG.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colQG.MinimumWidth = 100;
            dgvDuLieu.Columns.Add(colQG);
        }

        private void ConfigureDGVForDonGian(string maField, string tenField, string maHeader, string tenHeader)
        {
            dgvDuLieu.Columns.Clear();
            dgvDuLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = maHeader, DataPropertyName = maField, Name = maField });

            // Tên danh mục: Fill
            var colTen = new DataGridViewTextBoxColumn { HeaderText = tenHeader, DataPropertyName = tenField, Name = tenField };
            colTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTen.MinimumWidth = 200;
            dgvDuLieu.Columns.Add(colTen);
        }
        #endregion

        #region QUẢN LÝ TRẠNG THÁI (STATE)
        private void SetState(State state)
        {
            _currentState = state;
            bool isEditing = (state == State.CREATE || state == State.UPDATE);

            // 1. Quản lý Inputs
            cboDanhMuc.Enabled = (state == State.READ);
            txtMaDM.Enabled = false; // Mã DM luôn khóa

            // Bật/tắt Inputs linh hoạt dựa trên View (Hoặc theo Tác giả/NXB vì chúng phức tạp nhất)
            txtNhapLieu1.Enabled = isEditing;
            txtNhapLieu2.Enabled = isEditing && (_currentDanhMuc == LoaiDanhMuc.TacGia); // Chỉ Tác giả dùng NhapLieu2
            cboQuocGia.Enabled = isEditing && (_currentDanhMuc == LoaiDanhMuc.TacGia || _currentDanhMuc == LoaiDanhMuc.NhaXuatBan);

            // 2. Quản lý Buttons
            // Nút Thêm: Chỉ hiện khi đang ở chế độ Đọc VÀ User có quyền CREATE
            btnThem.Enabled = (state == State.READ) && SessionManager.CanCreate(MODULE_NAME);

            // Nút Sửa: Chỉ hiện khi đang Đọc, đã chọn dòng VÀ User có quyền UPDATE
            btnSua.Enabled = (state == State.READ && !string.IsNullOrEmpty(txtMaDM.Text))
                             && SessionManager.CanUpdate(MODULE_NAME);

            // Nút Xóa: Chỉ hiện khi đang Đọc, đã chọn dòng VÀ User có quyền DELETE
            btnXoa.Enabled = (state == State.READ && !string.IsNullOrEmpty(txtMaDM.Text))
                             && SessionManager.CanDelete(MODULE_NAME);

            // Nút Tìm kiếm: Chỉ hiện khi đang Đọc VÀ User có quyền SEARCH
            btnTimKiem.Enabled = (state == State.READ) && SessionManager.CanSearch(MODULE_NAME);

            // Nút Lưu/Hủy (Giữ nguyên)
            btnLuu.Enabled = isEditing;
            btnHuy.Enabled = isEditing;

            if (state == State.READ) ClearInputs();

            // 2. Gọi hàm cập nhật tiêu đề
            TriggerStatusEvent(state);
        }
        #endregion

        #region CHỨC NĂNG READ & CẤU HÌNH DGV
        private void LoadLoaiDanhMuc()
        {
            // Dữ liệu cho cboDanhMuc
            var danhMuc = new List<string> { "Tác giả", "Thể loại", "Định dạng", "Nhà xuất bản" };
            cboDanhMuc.DataSource = danhMuc;
            cboDanhMuc.SelectedIndex = 0; // Mặc định chọn Tác giả
        }

        private void LoadQuocGiaVaoCombo()
        {
            try
            {
                List<tQuocGia> danhSachQG = _tacGiaBll.LayTatCaQuocGia();

                // Chuyển sang DTO để hiển thị Mã và Tên
                var listQG = danhSachQG.Select(qg => new QuocGiaDTO
                {
                    MaQG = qg.MaQG,
                    TenQG = qg.TenQG
                }).ToList();

                cboQuocGia.DataSource = listQG;
                cboQuocGia.DisplayMember = "HienThi"; // Hiển thị MaQG - TenQG
                cboQuocGia.ValueMember = "MaQG";
                cboQuocGia.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách Quốc gia: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                dgvDuLieu.DataSource = null;
                object dataSource = null;

                if (_currentDanhMuc == LoaiDanhMuc.TacGia) dataSource = _tacGiaBll.LayTatCaTacGia();
                else if (_currentDanhMuc == LoaiDanhMuc.NhaXuatBan) dataSource = _nxbBll.LayTatCaNxb();
                else if (_currentDanhMuc == LoaiDanhMuc.TheLoai) dataSource = _thlBll.LayTatCaTheLoai();
                else if (_currentDanhMuc == LoaiDanhMuc.DinhDang) dataSource = _ddBll.LayTatCaDinhDang();

                dgvDuLieu.DataSource = dataSource;
                dgvDuLieu.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu {cboDanhMuc.SelectedItem}: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDuLieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvDuLieu.RowCount) return;

            string maDM = dgvDuLieu.Rows[e.RowIndex].Cells[0].Value.ToString();
            _selectedMaDM = maDM;

            object model = null;

            if (_currentDanhMuc == LoaiDanhMuc.TacGia) model = _tacGiaBll.LayChiTietTacGia(maDM);
            else if (_currentDanhMuc == LoaiDanhMuc.NhaXuatBan) model = _nxbBll.LayChiTietNxb(maDM);
            else if (_currentDanhMuc == LoaiDanhMuc.TheLoai) model = _thlBll.LayChiTietTheLoai(maDM);
            else if (_currentDanhMuc == LoaiDanhMuc.DinhDang) model = _ddBll.LayChiTietDinhDang(maDM);

            if (model != null) LoadModelToInputs(model);

            if (_currentState == State.READ)
            {
                bool isRowSelected = !string.IsNullOrEmpty(txtMaDM.Text);
                // Thêm kiểm tra quyền
                btnSua.Enabled = isRowSelected && SessionManager.CanUpdate(MODULE_NAME);
                btnXoa.Enabled = isRowSelected && SessionManager.CanDelete(MODULE_NAME);
            }
        }

        private void LoadModelToInputs(object model)
        {
            txtMaDM.Text = GetMaDMFromModel(model); // Lấy MaDM

            // Gán giá trị Inputs linh hoạt
            if (model is TacGiaDTO tg)
            {
                txtNhapLieu1.Text = tg.HoDem;
                txtNhapLieu2.Text = tg.Ten;
                cboQuocGia.SelectedValue = tg.MaQG;
            }
            else if (model is NxbDTO nxb)
            {
                txtNhapLieu1.Text = nxb.TenNXB;
                txtNhapLieu2.Text = string.Empty;
                cboQuocGia.SelectedValue = nxb.MaQG;
            }
            else if (model is TheLoaiDTO thl)
            {
                txtNhapLieu1.Text = thl.TenThL;
                txtNhapLieu2.Text = string.Empty;
                cboQuocGia.SelectedIndex = -1;
            }
            else if (model is DinhDangDTO dd)
            {
                txtNhapLieu1.Text = dd.TenDD;
                txtNhapLieu2.Text = string.Empty;
                cboQuocGia.SelectedIndex = -1;
            }
        }

        private string GetMaDMFromModel(object model)
        {
            if (model is TacGiaDTO tg) return tg.MaTG;
            if (model is NxbDTO nxb) return nxb.MaNXB;
            if (model is TheLoaiDTO thl) return thl.MaThL;
            if (model is DinhDangDTO dd) return dd.MaDD;
            return string.Empty;
        }

        // Cần xử lý sự kiện cboDanhMuc_SelectedIndexChanged để gọi ConfigureDGV và LoadData lại
        private void cboDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDanhMuc.SelectedItem == null) return;

            string selected = cboDanhMuc.SelectedItem.ToString();

            if (selected == "Tác giả")
            {
                _currentDanhMuc = LoaiDanhMuc.TacGia;
                ConfigureViewForTacGia();
            }
            else if (selected == "Nhà xuất bản")
            {
                _currentDanhMuc = LoaiDanhMuc.NhaXuatBan;
                ConfigureViewForNhaXuatBan();
            }
            else if (selected == "Thể loại")
            {
                _currentDanhMuc = LoaiDanhMuc.TheLoai;
                ConfigureViewForTheLoai();
            }
            else if (selected == "Định dạng")
            {
                _currentDanhMuc = LoaiDanhMuc.DinhDang;
                ConfigureViewForDinhDang();
            }

            SetState(State.READ); // Sẽ gọi ClearInputs
            LoadData();
        }

        private void ConfigureViewForTacGia()
        {
            ConfigureDGVForTacGia();
            label2.Text = "Họ Đệm:";
            label4.Text = "Tên:";
            label3.Text = "Quốc gia:";
            txtNhapLieu1.Visible = true;
            txtNhapLieu2.Visible = true;
            cboQuocGia.Visible = true;
        }

        private void ConfigureViewForNhaXuatBan()
        {
            ConfigureDGVForNxb();
            label2.Text = "Tên NXB:";
            label4.Text = ""; // Ẩn nhãn
            label3.Text = "Quốc gia:";
            txtNhapLieu1.Visible = true;
            txtNhapLieu2.Visible = false; // NXB chỉ dùng 1 TextBox
            cboQuocGia.Visible = true;
        }

        private void ConfigureViewForTheLoai()
        {
            ConfigureDGVForDonGian("MaThL", "TenThL", "Mã Thể loại", "Tên Thể loại");

            // Ẩn các control không cần thiết
            txtNhapLieu1.Visible = true;
            txtNhapLieu2.Visible = false;
            cboQuocGia.Visible = false;
            label2.Text = "Tên Thể loại:";
            label4.Text = "";
            label3.Text = "";
        }

        private void ConfigureViewForDinhDang()
        {
            ConfigureDGVForDonGian("MaDD", "TenDD", "Mã Định dạng", "Tên Định dạng");

            // Ẩn các control không cần thiết
            txtNhapLieu1.Visible = true;
            txtNhapLieu2.Visible = false;
            cboQuocGia.Visible = false;
            label2.Text = "Tên Định dạng:";
            label4.Text = "";
            label3.Text = "";
        }
        #endregion

        #region CHỨC NĂNG CREATE
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Create, "thêm danh mục")) return;
            // Kiểm tra xem danh mục có được hỗ trợ CREATE không
            if (_currentDanhMuc == LoaiDanhMuc.TheLoai || _currentDanhMuc == LoaiDanhMuc.DinhDang ||
                _currentDanhMuc == LoaiDanhMuc.TacGia || _currentDanhMuc == LoaiDanhMuc.NhaXuatBan)
            {
                ClearInputs();
                SetState(State.CREATE);
                // Đặt focus vào trường nhập liệu quan trọng đầu tiên
                if (_currentDanhMuc == LoaiDanhMuc.TacGia || _currentDanhMuc == LoaiDanhMuc.NhaXuatBan) cboQuocGia.Focus();
                else txtNhapLieu1.Focus();
            }
            else
            {
                MessageBox.Show("Chức năng Thêm mới không hỗ trợ cho danh mục này.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region CHỨC NĂNG UPDATE
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Update, "sửa danh mục")) return;
            // Kiểm tra xem danh mục có được hỗ trợ UPDATE không
            if (_currentDanhMuc != LoaiDanhMuc.TacGia && _currentDanhMuc != LoaiDanhMuc.NhaXuatBan &&
                _currentDanhMuc != LoaiDanhMuc.TheLoai && _currentDanhMuc != LoaiDanhMuc.DinhDang) return;

            if (_currentState == State.READ && !string.IsNullOrEmpty(txtMaDM.Text))
            {
                SetState(State.UPDATE);
                txtNhapLieu1.Focus();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một mục để chỉnh sửa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region CHỨC NĂNG DELETE
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Delete, "xóa danh mục")) return;
            if (_currentDanhMuc != LoaiDanhMuc.TacGia && _currentDanhMuc != LoaiDanhMuc.NhaXuatBan &&
                _currentDanhMuc != LoaiDanhMuc.TheLoai && _currentDanhMuc != LoaiDanhMuc.DinhDang) return;

            string maDM = txtMaDM.Text.Trim();
            string tenDM = GetTenDMFromInputs();

            if (string.IsNullOrEmpty(maDM))
            {
                MessageBox.Show("Vui lòng chọn một mục để xóa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa {cboDanhMuc.SelectedItem}:\n[ {maDM} - {tenDM} ] không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool success = false;

                    if (_currentDanhMuc == LoaiDanhMuc.TacGia) success = _tacGiaBll.XoaTacGia(maDM);
                    else if (_currentDanhMuc == LoaiDanhMuc.NhaXuatBan) success = _nxbBll.XoaNxb(maDM);
                    else if (_currentDanhMuc == LoaiDanhMuc.TheLoai) success = _thlBll.XoaTheLoai(maDM);
                    else if (_currentDanhMuc == LoaiDanhMuc.DinhDang) success = _ddBll.XoaDinhDang(maDM);

                    if (success)
                    {
                        MessageBox.Show($"Xóa {cboDanhMuc.SelectedItem} thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show($"{cboDanhMuc.SelectedItem} thất bại. Có thể có ràng buộc khóa ngoại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void BtnTimKiem_Click(object sender, EventArgs e) 
        {
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Search, "tìm kiếm danh mục")) return;
            if (_currentState != State.READ)
            {
                MessageBox.Show("Vui lòng Lưu hoặc Hủy chỉnh sửa trước khi Tìm kiếm.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<FieldMetadata> dmMetadata = null;

            // 1. Xác định Danh mục hiện tại và lấy Metadata tương ứng
            if (_currentDanhMuc == LoaiDanhMuc.TacGia)
                dmMetadata = _tacGiaBll.GetSearchFields();
            else if (_currentDanhMuc == LoaiDanhMuc.NhaXuatBan)
                dmMetadata = _nxbBll.GetSearchFields();
            else if (_currentDanhMuc == LoaiDanhMuc.TheLoai)
                dmMetadata = _thlBll.GetSearchFields();
            else if (_currentDanhMuc == LoaiDanhMuc.DinhDang)
                dmMetadata = _ddBll.GetSearchFields();
            else return;

            // --- Bắt đầu chuyển sang Non-modal và Event ---

            // Đảm bảo không tạo nhiều instance của Form tìm kiếm VÀ CHỈ ĐĂNG KÝ EVENT MỘT LẦN
            if (_searchForm == null || _searchForm.IsDisposed)
            {
                // Khởi tạo Form tìm kiếm
                _searchForm = new FrmTimKiem(dmMetadata);

                // Đăng ký Event để nhận bộ lọc khi nút "Tìm" được nhấn (CHỈ 1 LẦN)
                _searchForm.OnSearchApplied += HandleSearchAppliedDanhMuc;

                // Xử lý sự kiện FormClosed để gỡ đăng ký Event và dọn dẹp biến _searchForm
                _searchForm.FormClosed += SearchForm_FormClosed;
            }

            // Hiển thị Form non-modal
            _searchForm.Show();
            _searchForm.BringToFront();
        }

        // Hàm xử lý Event khi người dùng nhấn nút "Tìm" trong FrmTimKiem
        private void HandleSearchAppliedDanhMuc(List<SearchFilter> filters)
        {
            // Cập nhật bộ lọc hiện tại (dùng khi hủy tìm kiếm)
            _currentFilters = filters;

            // Gọi hàm tải dữ liệu mới với các bộ lọc nhận được
            LoadDataWithFilters(filters);
        }

        private void LoadDataWithFilters(List<SearchFilter> filters)
        {
            try
            {
                dgvDuLieu.DataSource = null;
                object dataSource = null;

                if (filters == null || filters.Count == 0)
                {
                    // Quay về hàm LoadData() gốc nếu không có bộ lọc
                    LoadData();
                    return;
                }

                // 1. Gọi hàm tìm kiếm tương ứng trong BLL
                if (_currentDanhMuc == LoaiDanhMuc.TacGia)
                    dataSource = _tacGiaBll.TimKiemTacGia(filters);
                else if (_currentDanhMuc == LoaiDanhMuc.NhaXuatBan)
                    dataSource = _nxbBll.TimKiemNxb(filters);
                else if (_currentDanhMuc == LoaiDanhMuc.TheLoai)
                    dataSource = _thlBll.TimKiemTheLoai(filters);
                else if (_currentDanhMuc == LoaiDanhMuc.DinhDang)
                    dataSource = _ddBll.TimKiemDinhDang(filters);

                // 2. Cập nhật DataGridView
                dgvDuLieu.DataSource = dataSource;
                dgvDuLieu.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                btnHuy.Enabled = true;

                // 3. Hiển thị thông báo kết quả
                int count = (dataSource as System.Collections.IList)?.Count ?? 0;
                MessageBox.Show($"Tìm thấy {count} kết quả khớp với bộ lọc.", "Thông báo Tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thực hiện tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm xử lý khi Form tìm kiếm bị đóng
        private void SearchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_searchForm != null)
            {
                // Gỡ đăng ký Event để tránh rò rỉ bộ nhớ (memory leak)
                _searchForm.OnSearchApplied -= HandleSearchAppliedDanhMuc;
            }

            // Đặt _searchForm về null để lần sau khi click sẽ tạo Form mới
            _searchForm = null;

            // Tùy chọn: Nếu bạn muốn dữ liệu trở về trạng thái toàn bộ khi Form tìm kiếm đóng
            // LoadData();
        }

        #endregion

        #region XỬ LÝ SỰ KIỆN CÁC NÚT - LƯU - HỦY
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                if (_currentState == State.CREATE)
                {
                    string newMaDM = HandleCreate();

                    if (newMaDM != null && newMaDM != string.Empty)
                    {
                        MessageBox.Show($"Tạo {cboDanhMuc.SelectedItem} thành công. Mã: {newMaDM}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        SetState(State.READ);
                        _selectedMaDM = newMaDM;
                    }
                    else if (newMaDM == null)
                    {
                        MessageBox.Show("Lỗi: Đã đạt giới hạn mã hoặc lỗi nghiệp vụ khác.", "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Thêm {cboDanhMuc.SelectedItem} thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (_currentState == State.UPDATE)
                {
                    if (HandleUpdate())
                    {
                        MessageBox.Show($"Cập nhật {cboDanhMuc.SelectedItem} thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        SetState(State.READ);
                    }
                    else
                    {
                        MessageBox.Show($"Cập nhật {cboDanhMuc.SelectedItem} thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (!string.IsNullOrEmpty(_selectedMaDM))
                {
                    object model = null;
                    if (_currentDanhMuc == LoaiDanhMuc.TacGia) model = _tacGiaBll.LayChiTietTacGia(_selectedMaDM);
                    else if (_currentDanhMuc == LoaiDanhMuc.NhaXuatBan) model = _nxbBll.LayChiTietNxb(_selectedMaDM);
                    else if (_currentDanhMuc == LoaiDanhMuc.TheLoai) model = _thlBll.LayChiTietTheLoai(_selectedMaDM);
                    else if (_currentDanhMuc == LoaiDanhMuc.DinhDang) model = _ddBll.LayChiTietDinhDang(_selectedMaDM);

                    if (model != null) LoadModelToInputs(model);
                    else ClearInputs();
                }
            }
            SetState(State.READ);
            LoadData();
        }
        #endregion

        #region HÀM BỔ TRỢ
        // Hàm hỗ trợ chọn màu và text dựa trên trạng thái
        private void TriggerStatusEvent(State state)
        {
            string title = "QUẢN LÝ DANH MỤC";
            Color backColor = Color.FromArgb(32, 36, 104); // Màu xanh mặc định
            Color foreColor = Color.White;

            switch (state)
            {
                case State.CREATE:
                    title = "THÊM DANH MỤC MỚI";
                    backColor = Color.SeaGreen;
                    break;
                case State.UPDATE:
                    title = "CẬP NHẬT DANH MỤC";
                    backColor = Color.DarkOrange;
                    break;
                case State.READ:
                default:
                    title = "DANH SÁCH DANH MỤC";
                    backColor = Color.FromArgb(32, 36, 104);
                    break;
            }

            // Bắn sự kiện ra ngoài cho Form cha bắt
            OnStatusRequest?.Invoke(this, new StatusRequestEventArgs(title, backColor, foreColor));
        }

        // --- XỬ LÝ CREATE (ĐỘNG) ---
        private string HandleCreate()
        {
            if (_currentDanhMuc == LoaiDanhMuc.TacGia)
            {
                TacGiaDTO tg = new TacGiaDTO { MaQG = cboQuocGia.SelectedValue?.ToString(), HoDem = txtNhapLieu1.Text.Trim(), Ten = txtNhapLieu2.Text.Trim() };
                return _tacGiaBll.ThemTacGia(tg); // Trả về MaTG hoặc null/string.Empty
            }
            else if (_currentDanhMuc == LoaiDanhMuc.NhaXuatBan)
            {
                NxbDTO nxb = new NxbDTO { MaQG = cboQuocGia.SelectedValue?.ToString(), TenNXB = txtNhapLieu1.Text.Trim() };
                return _nxbBll.ThemNxb(nxb);
            }
            else if (_currentDanhMuc == LoaiDanhMuc.TheLoai)
            {
                TheLoaiDTO thl = new TheLoaiDTO { TenThL = txtNhapLieu1.Text.Trim() };
                return _thlBll.ThemTheLoai(thl);
            }
            else if (_currentDanhMuc == LoaiDanhMuc.DinhDang)
            {
                DinhDangDTO dd = new DinhDangDTO { TenDD = txtNhapLieu1.Text.Trim() };
                return _ddBll.ThemDinhDang(dd);
            }
            return string.Empty;
        }

        // --- XỬ LÝ UPDATE (ĐỘNG) ---
        private bool HandleUpdate()
        {
            if (_currentDanhMuc == LoaiDanhMuc.TacGia)
            {
                TacGiaDTO tg = new TacGiaDTO { MaTG = _selectedMaDM, MaQG = cboQuocGia.SelectedValue?.ToString(), HoDem = txtNhapLieu1.Text.Trim(), Ten = txtNhapLieu2.Text.Trim() };
                return _tacGiaBll.CapNhatTacGia(tg);
            }
            else if (_currentDanhMuc == LoaiDanhMuc.NhaXuatBan)
            {
                NxbDTO nxb = new NxbDTO { MaNXB = _selectedMaDM, MaQG = cboQuocGia.SelectedValue?.ToString(), TenNXB = txtNhapLieu1.Text.Trim() };
                return _nxbBll.CapNhatNxb(nxb);
            }
            else if (_currentDanhMuc == LoaiDanhMuc.TheLoai)
            {
                TheLoaiDTO thl = new TheLoaiDTO { MaThL = _selectedMaDM, TenThL = txtNhapLieu1.Text.Trim() };
                return _thlBll.CapNhatTheLoai(thl);
            }
            else if (_currentDanhMuc == LoaiDanhMuc.DinhDang)
            {
                DinhDangDTO dd = new DinhDangDTO { MaDD = _selectedMaDM, TenDD = txtNhapLieu1.Text.Trim() };
                return _ddBll.CapNhatDinhDang(dd);
            }
            return false;
        }

        private string GetTenDMFromInputs()
        {
            if (_currentDanhMuc == LoaiDanhMuc.TacGia) return txtNhapLieu1.Text.Trim() + " " + txtNhapLieu2.Text.Trim();
            if (_currentDanhMuc == LoaiDanhMuc.NhaXuatBan) return txtNhapLieu1.Text.Trim();
            if (_currentDanhMuc == LoaiDanhMuc.TheLoai) return txtNhapLieu1.Text.Trim();
            if (_currentDanhMuc == LoaiDanhMuc.DinhDang) return txtNhapLieu1.Text.Trim();
            return string.Empty;
        }

        private void ClearInputs()
        {
            txtMaDM.Text = string.Empty;
            txtNhapLieu1.Text = string.Empty;
            txtNhapLieu2.Text = string.Empty;
            cboQuocGia.SelectedIndex = -1;

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        // Hàm mới: Thu thập Inputs và tạo DTO
        private TacGiaDTO GetModelFromInputs()
        {
            return new TacGiaDTO
            {
                MaTG = txtMaDM.Text.Trim(), // Sẽ rỗng khi CREATE, dùng khi UPDATE
                MaQG = cboQuocGia.SelectedValue?.ToString() ?? string.Empty,
                HoDem = txtNhapLieu1.Text.Trim(),
                Ten = txtNhapLieu2.Text.Trim()
            };
        }

        // Hàm mới: Kiểm tra tính hợp lệ
        private bool ValidateInputs()
        {
            // Kiểm tra trường bắt buộc chung cho tên
            if (string.IsNullOrEmpty(txtNhapLieu1.Text.Trim()))
            {
                MessageBox.Show($"Vui lòng nhập Tên/Thông tin cho {cboDanhMuc.SelectedItem}.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNhapLieu1.Focus();
                return false;
            }

            // Logic Validation cụ thể
            if (_currentDanhMuc == LoaiDanhMuc.TacGia)
            {
                if (string.IsNullOrEmpty(txtNhapLieu2.Text.Trim()) || cboQuocGia.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ Họ Đệm, Tên và chọn Quốc gia.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (txtNhapLieu1.Text.Trim().Length > 50 || txtNhapLieu2.Text.Trim().Length > 30)
                {
                    MessageBox.Show("Họ Đệm không được vượt quá 50 ký tự, Tên không quá 30 ký tự.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else if (_currentDanhMuc == LoaiDanhMuc.NhaXuatBan)
            {
                if (cboQuocGia.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn Quốc gia cho Nhà Xuất Bản.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (txtNhapLieu1.Text.Trim().Length > 100)
                {
                    MessageBox.Show("Tên Nhà Xuất Bản không được vượt quá 100 ký tự.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else if (_currentDanhMuc == LoaiDanhMuc.TheLoai || _currentDanhMuc == LoaiDanhMuc.DinhDang)
            {
                if (txtNhapLieu1.Text.Trim().Length > 50)
                {
                    MessageBox.Show("Tên không được vượt quá 50 ký tự.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }
        #endregion
    }
}
