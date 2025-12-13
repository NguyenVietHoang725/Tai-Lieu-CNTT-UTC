using LibraryManagerApp.BLL;
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

namespace LibraryManagerApp.GUI.UserControls.QLTaiLieu
{
    public partial class ucFrmThongTinTaiLieu : UserControl
    {
        private TaiLieuBLL _bll = new TaiLieuBLL();
        private State _currentState;
        private string _selectedMaTL = string.Empty;
        private FrmTimKiem _searchForm;


        // Vùng nhớ (in-memory list) cho Tác giả
        private List<TL_TGDTO> _danhSachTacGiaTam = new List<TL_TGDTO>();

        // Biến tạm lưu Tác giả đang chọn trong DGV (khi EDITING)
        private TL_TGDTO _selectedTacGiaTam = null;

        // Khai báo các giá trị cố định
        private List<string> _listKhoCo = new List<string> { "A3", "A4", "A5", "B5", "Khác" };
        private List<string> _listVaiTro = new List<string> { "Tác giả", "Đồng tác giả", "Chủ biên", "Biên soạn", "Hướng dẫn khoa học" };
        public event EventHandler<StatusRequestEventArgs> OnStatusRequest;
        private const string MODULE_NAME = "TaiLieu";

        #region KHỞI TẠO VÀ CẤU HÌNH
        public ucFrmThongTinTaiLieu()
        {
            InitializeComponent();

            ConfigureDGV();
        }

        private void ucFrmThongTinTaiLieu_Load(object sender, EventArgs e)
        {
            LoadControlsData(); // Tải dữ liệu ComboBox
            SetState(State.READ);
            LoadData();
        }

        private void ConfigureDGV()
        {
            // Cấu hình chung cho cả 2 DGV
            ConfigureDGVStyle(dgvDuLieuTaiLieu);
            ConfigureDGVStyle(dgvDuLieuTacGia);

            ConfigureDGVForTaiLieu();
            ConfigureDGVForTacGia();
        }

        // Hàm cấu hình Style chung (Tái sử dụng)
        private void ConfigureDGVStyle(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(dgv.Font.FontFamily, 10f, FontStyle.Bold);
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 30;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.DefaultCellStyle.Font = new Font(dgv.Font.FontFamily, 10f, FontStyle.Regular);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        private void ConfigureDGVForTaiLieu()
        {
            dgvDuLieuTaiLieu.Columns.Clear();
            dgvDuLieuTaiLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã TL", DataPropertyName = "MaTL", Name = "MaTL" });

            // Cột Tên Tài liệu: Dài -> Fill
            var colTenTL = new DataGridViewTextBoxColumn { HeaderText = "Tên Tài liệu", DataPropertyName = "TenTL", Name = "TenTL" };
            colTenTL.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTenTL.MinimumWidth = 200;
            dgvDuLieuTaiLieu.Columns.Add(colTenTL);

            // Cột NXB: Dài -> Fill
            var colNXB = new DataGridViewTextBoxColumn { HeaderText = "NXB", DataPropertyName = "TenNXB", Name = "TenNXB" };
            colNXB.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colNXB.MinimumWidth = 150;
            dgvDuLieuTaiLieu.Columns.Add(colNXB);

            dgvDuLieuTaiLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Thể loại", DataPropertyName = "TenThL", Name = "TenThL" });
            dgvDuLieuTaiLieu.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ngôn ngữ", DataPropertyName = "TenNN", Name = "TenNN" });
        }

        private void ConfigureDGVForTacGia()
        {
            dgvDuLieuTacGia.Columns.Clear();
            dgvDuLieuTacGia.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã TG", DataPropertyName = "MaTG", Name = "MaTG" });

            // Cột Tên Tác giả: Dài -> Fill
            var colTenTG = new DataGridViewTextBoxColumn { HeaderText = "Họ Tên Tác giả", DataPropertyName = "HoTenTG", Name = "HoTenTG" };
            colTenTG.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTenTG.MinimumWidth = 150;
            dgvDuLieuTacGia.Columns.Add(colTenTG);

            dgvDuLieuTacGia.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Vai trò", DataPropertyName = "VaiTro", Name = "VaiTro" });
        }

        // Tải dữ liệu cho tất cả ComboBox
        private void LoadControlsData()
        {
            // Khổ Cỡ và Vai trò (Dữ liệu cố định)
            cboKhoCo.DataSource = _listKhoCo;
            cboVaiTro.DataSource = _listVaiTro;

            // Ngôn Ngữ, Thể loại, Định dạng (Danh mục đơn giản)
            LoadDanhMucDonGian(cboNgonNgu, _bll.LayTatCaNgonNgu(), "MaNN", "TenNN");
            LoadDanhMucDonGian(cboTheLoai, _bll.LayTatCaTheLoai(), "MaThL", "TenThL");
            LoadDanhMucDonGian(cboDinhDang, _bll.LayTatCaDinhDang(), "MaDD", "TenDD");

            // NXB và Tác giả (Danh mục phức tạp hơn)
            LoadDanhMucPhucTap(cboNhaXuatBan, _bll.LayTatCaNxb().Cast<object>().ToList(), "MaNXB", "TenNXB");
            LoadDanhMucPhucTap(cboTacGia, _bll.LayTatCaTacGia().Cast<object>().ToList(), "MaTG", "HoTen");

            // Đặt SelectedIndex = -1 sau khi load
            cboNgonNgu.SelectedIndex = -1;
            cboTheLoai.SelectedIndex = -1;
            cboDinhDang.SelectedIndex = -1;
            cboNhaXuatBan.SelectedIndex = -1;
            cboTacGia.SelectedIndex = -1;
            cboVaiTro.SelectedIndex = -1;
        }

        // Hàm hỗ trợ Load ComboBox (Danh mục đơn giản)
        private void LoadDanhMucDonGian<T>(ComboBox cbo, List<T> dataSource, string maField, string tenField)
        {
            var data = dataSource.Select(item => {
                var ma = item.GetType().GetProperty(maField).GetValue(item).ToString();
                var ten = item.GetType().GetProperty(tenField).GetValue(item).ToString();
                return new DanhMucDonGianDTO { Ma = ma, Ten = ten };
            }).ToList();

            cbo.DataSource = data;
            cbo.DisplayMember = "HienThi";
            cbo.ValueMember = "Ma";
        }

        // Hàm hỗ trợ Load ComboBox (NXB/TG)
        private void LoadDanhMucPhucTap<T>(ComboBox cbo, List<T> dataSource, string maField, string tenField)
        {
            var data = dataSource.Select(item => {
                var ma = item.GetType().GetProperty(maField).GetValue(item).ToString();
                var ten = item.GetType().GetProperty(tenField).GetValue(item).ToString();
                return new { Ma = ma, HienThi = $"{ma} - {ten}" };
            }).ToList();

            cbo.DataSource = data;
            cbo.DisplayMember = "HienThi";
            cbo.ValueMember = "Ma";
        }
        #endregion

        #region QUẢN LÝ TRẠNG THÁI (STATE)
        private void SetState(State state)
        {
            _currentState = state;
            bool isEditing = (state == State.CREATE || state == State.UPDATE);

            // Inputs Tài liệu chính
            txtMaTL.Enabled = false; // Mã TL sinh tự động
            txtTenTL.Enabled = isEditing;
            txtNamXuatBan.Enabled = isEditing;
            nudLanXuatBan.Enabled = isEditing;
            nudSoTrang.Enabled = isEditing;
            cboKhoCo.Enabled = isEditing;

            cboNhaXuatBan.Enabled = isEditing;
            cboNgonNgu.Enabled = isEditing;
            cboTheLoai.Enabled = isEditing;
            cboDinhDang.Enabled = isEditing;

            // Inputs Tác giả đính kèm (chỉ cần thiết khi có Tài liệu được chọn/đang tạo)
            cboTacGia.Enabled = isEditing; // Chỉ có thể thêm tác giả khi CREATE/UPDATE Tài liệu
            cboVaiTro.Enabled = isEditing;

            // DGV
            dgvDuLieuTaiLieu.Enabled = (state == State.READ);
            dgvDuLieuTacGia.Enabled = true; // Luôn bật để chọn (kể cả READ và EDIT)

            // --- ĐIỀU CHỈNH LOGIC NÚT BẤM ---
            if (state == State.READ)
            {
                // Kiểm tra quyền khi ở trạng thái READ
                btnThem.Enabled = SessionManager.CanCreate(MODULE_NAME);
                btnThem.Text = "Thêm"; // Thêm Tài liệu

                bool isRowSelected = !string.IsNullOrEmpty(txtMaTL.Text);
                btnSua.Enabled = isRowSelected && SessionManager.CanUpdate(MODULE_NAME);
                btnSua.Text = "Sửa"; // Sửa Tài liệu

                btnXoa.Enabled = isRowSelected && SessionManager.CanDelete(MODULE_NAME);
                btnXoa.Text = "Xóa"; // Xóa Tài liệu

                btnTimKiem.Enabled = SessionManager.CanSearch(MODULE_NAME);

                // Bổ sung logic cho btnXuatExcel (dựa theo hàm btnXuatExcel_Click đã có)
                btnXuatExcel.Enabled = SessionManager.CanExport(MODULE_NAME);

                // Các nút Xem (View) không cần quyền đặc biệt (chỉ cần View)
                btnXemHinhAnh.Enabled = isRowSelected;
                btnXemBanSao.Enabled = isRowSelected;
            }
            else // CREATE hoặc UPDATE
            {
                // Repurpose btnThem
                btnThem.Enabled = true;
                btnThem.Text = "Thêm Tác giả"; // Thêm Tác giả vào danh sách

                // Repurpose btnSua (Yêu cầu 1)
                btnSua.Enabled = false; // Bị tắt, chỉ bật khi chọn Tác giả trong DGV
                btnSua.Text = "Sửa Tác giả";

                // Repurpose btnXoa
                btnXoa.Enabled = false; // Bị tắt, chỉ bật khi chọn Tác giả trong DGV
                btnXoa.Text = "Xóa Tác giả";

                btnTimKiem.Enabled = false; // Tắt khi đang edit
                btnXuatExcel.Enabled = false; // Tắt khi đang edit
                btnXemHinhAnh.Enabled = false; // Tắt khi đang edit
                btnXemBanSao.Enabled = false; // Tắt khi đang edit
            }

            btnLuu.Enabled = isEditing;
            btnHuy.Enabled = isEditing;
            //btnTimKiem.Enabled = (state == State.READ);
            //btnXemHinhAnh.Enabled = (state == State.READ && !string.IsNullOrEmpty(txtMaTL.Text));
            //btnXemBanSao.Enabled = (state == State.READ && !string.IsNullOrEmpty(txtMaTL.Text));

            if (state == State.READ)
            {
                ClearInputs();
                _danhSachTacGiaTam.Clear(); // Xóa vùng nhớ
            }
            else if (isEditing)
            {
                // Yêu cầu 4: Clear combos khi bắt đầu Sửa/Thêm Tài liệu
                ClearTacGiaCombos();
            }

            TriggerStatusEvent(state);
        }
        #endregion

        #region CHỨC NĂNG READ
        private void LoadData()
        {
            try
            {
                dgvDuLieuTaiLieu.DataSource = null;
                List<TaiLieuDTO> danhSach = _bll.LayTatCaTaiLieu();
                dgvDuLieuTaiLieu.DataSource = danhSach;

                dgvDuLieuTaiLieu.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Tài liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDuLieuTaiLieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvDuLieuTaiLieu.RowCount) return;

            string maTL = dgvDuLieuTaiLieu.Rows[e.RowIndex].Cells["MaTL"].Value.ToString();
            _selectedMaTL = maTL;

            TaiLieuDTO model = _bll.LayChiTietTaiLieu(maTL);

            if (model != null)
            {
                LoadModelToInputs(model);
                LoadTacGiaDinhKem(model.DanhSachTacGia);

                if (model.DanhSachTacGia.Count > 0)
                {
                    // Lấy Tác giả đầu tiên
                    TL_TGDTO firstAuthor = model.DanhSachTacGia[0];
                    // Tải thông tin lên ComboBox
                    LoadTacGiaDetailToCombos(firstAuthor);

                    // Chọn hàng đầu tiên trong dgvDuLieuTacGia
                    dgvDuLieuTacGia.Rows[0].Selected = true;
                }
                else
                {
                    // Nếu không có tác giả, xóa ComboBox
                    ClearTacGiaCombos();
                }
            }

            if (_currentState == State.READ)
            {
                bool isRowSelected = !string.IsNullOrEmpty(txtMaTL.Text);
                btnSua.Enabled = isRowSelected && SessionManager.CanUpdate(MODULE_NAME);
                btnXoa.Enabled = isRowSelected && SessionManager.CanDelete(MODULE_NAME);
                btnXemHinhAnh.Enabled = isRowSelected;
                btnXemBanSao.Enabled = isRowSelected;
            }
        }

        private void dgvDuLieuTacGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvDuLieuTacGia.RowCount) return;

            TL_TGDTO selectedAuthor = dgvDuLieuTacGia.Rows[e.RowIndex].DataBoundItem as TL_TGDTO;
            if (selectedAuthor == null) return;

            // Lưu Tác giả đang chọn vào biến tạm
            _selectedTacGiaTam = selectedAuthor;

            if (_currentState == State.READ)
            {
                // Khi READ: Chỉ load thông tin
                LoadTacGiaDetailToCombos(selectedAuthor);
            }
            else // CREATE hoặc UPDATE
            {
                // Khi EDITING: Load thông tin VÀ kích hoạt nút
                LoadTacGiaDetailToCombos(selectedAuthor);
                btnSua.Enabled = true; // Bật nút "Sửa Tác giả"
                btnXoa.Enabled = true; // Bật nút "Xóa Tác giả"
            }
        }

        private void LoadTacGiaDinhKem(List<TL_TGDTO> danhSachTG)
        {
            _danhSachTacGiaTam = new List<TL_TGDTO>(danhSachTG);
            RefreshDgvTacGia();
        }

        private void LoadTacGiaDetailToCombos(TL_TGDTO tacGia)
        {
            // cboTacGia (đã load toàn bộ tác giả)
            cboTacGia.SelectedValue = tacGia.MaTG;

            // cboVaiTro (đã load danh sách vai trò)
            cboVaiTro.SelectedItem = tacGia.VaiTro;
        }

        private void LoadModelToInputs(TaiLieuDTO model)
        {
            txtMaTL.Text = model.MaTL;
            txtTenTL.Text = model.TenTL;
            txtNamXuatBan.Text = model.NamXuatBan?.ToString() ?? string.Empty;

            nudLanXuatBan.Value = model.LanXuatBan ?? 1;
            nudSoTrang.Value = model.SoTrang ?? 0;

            // Load ComboBox (cần gán ValueMember)
            cboNhaXuatBan.SelectedValue = model.MaNXB;
            cboNgonNgu.SelectedValue = model.MaNN;
            cboTheLoai.SelectedValue = model.MaThL;
            cboDinhDang.SelectedValue = model.MaDD;
            cboKhoCo.SelectedItem = model.KhoCo;
        }
        #endregion

        #region CHỨC NĂNG CREATE / THÊM TÁC GIẢ
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (_currentState == State.READ)
            {
                if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Create, "thêm tài liệu")) return;
                // 1. CHỨC NĂNG: THÊM TÀI LIỆU
                ClearInputs();
                SetState(State.CREATE);
                txtTenTL.Focus();
                _danhSachTacGiaTam.Clear();
                LoadTacGiaDinhKem(_danhSachTacGiaTam);
            }
            else // CREATE hoặc UPDATE
            {
                // 2. CHỨC NĂNG: THÊM TÁC GIẢ (vào vùng nhớ)
                HandleThemTacGiaVaoList();
            }
        }
        #endregion

        #region CHỨC NĂNG UPDATE / SỬA TÁC GIẢ
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_currentState == State.READ)
            {
                if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Update, "sửa tài liệu")) return;
                // 1. CHỨC NĂNG: SỬA TÀI LIỆU
                if (!string.IsNullOrEmpty(txtMaTL.Text))
                {
                    SetState(State.UPDATE);
                    txtTenTL.Focus();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một Tài liệu để chỉnh sửa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else // CREATE hoặc UPDATE
            {
                // 2. CHỨC NĂNG: SỬA VAI TRÒ TÁC GIẢ (trong vùng nhớ)
                HandleSuaTacGiaTrongList();
            }
        }
        #endregion

        #region CHỨC NĂNG DELETE / XÓA TÁC GIẢ
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_currentState == State.READ)
            {
                if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Delete, "xóa tài liệu")) return;
                // 1. CHỨC NĂNG: XÓA TÀI LIỆU
                HandleXoaTaiLieuChinh();
            }
            else // CREATE hoặc UPDATE
            {
                // 2. CHỨC NĂNG: XÓA TÁC GIẢ (khỏi vùng nhớ)
                HandleXoaTacGiaKhoiList();
            }
        }

        // Hàm mới: Xử lý logic Xóa Tài liệu chính
        private void HandleXoaTaiLieuChinh()
        {
            string maTL = txtMaTL.Text.Trim();
            string tenTL = txtTenTL.Text.Trim();

            if (string.IsNullOrEmpty(maTL))
            {
                MessageBox.Show("Vui lòng chọn một Tài liệu để xóa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa Tài liệu:\n[ {maTL} - {tenTL} ] không?\n(Mọi Tác giả đính kèm cũng sẽ bị xóa)",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning // Cảnh báo cao hơn Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (_bll.XoaTaiLieu(maTL))
                    {
                        MessageBox.Show("Xóa Tài liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Xóa Tài liệu thất bại. Có thể Tài liệu đang có Bản sao hoặc đang được mượn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống khi xóa Tài liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region CHỨC NĂNG TÌM KIẾM
        private void btnMoTimKiem_Click(object sender, EventArgs e)
        {
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Search, "tìm kiếm tài liệu")) return;
            // Lấy metadata cho Tài Liệu
            List<FieldMetadata> tlMetadata = _bll.GetSearchFields();

            // Đảm bảo không tạo nhiều instance của Form tìm kiếm VÀ CHỈ ĐĂNG KÝ EVENT MỘT LẦN
            if (_searchForm == null || _searchForm.IsDisposed)
            {
                // 1. Khởi tạo Form tìm kiếm mới
                _searchForm = new FrmTimKiem(tlMetadata);

                // 2. Đăng ký Event để nhận bộ lọc khi nút "Tìm" được nhấn (CHỈ 1 LẦN)
                _searchForm.OnSearchApplied += HandleSearchAppliedTaiLieu;

                // Tùy chọn: Đăng ký sự kiện đóng Form để giải phóng tài nguyên
                // _searchForm.FormClosed += SearchForm_FormClosed; 
            }

            // 3. Hiển thị Form non-modal (Không chặn Form cha)
            _searchForm.Show();
            _searchForm.BringToFront(); // Đưa Form tìm kiếm lên trên
        }

        // Hàm xử lý Event khi người dùng nhấn nút "Tìm" trong FrmTimKiem
        // Event này được gọi từ FrmTimKiem sau khi thu thập xong filters VÀ KHÔNG ĐÓNG FORM
        private void HandleSearchAppliedTaiLieu(List<SearchFilter> filters)
        {
            try
            {
                // Gọi hàm tải dữ liệu mới với các bộ lọc nhận được
                LoadTaiLieuData(filters);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thực hiện tìm kiếm Tài Liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTaiLieuData(List<SearchFilter> filters)
        {
            // TaiLieuBLL bll = new TaiLieuBLL(); // Không cần tạo lại BLL nếu đã có _bll ở cấp class
            List<TaiLieuDTO> danhSach;

            if (filters == null || filters.Count == 0)
            {
                danhSach = _bll.LayTatCaTaiLieu();
                MessageBox.Show("Hiển thị toàn bộ Tài Liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                danhSach = _bll.TimKiemTaiLieu(filters);
                MessageBox.Show($"Tìm thấy {danhSach.Count} Tài Liệu khớp với bộ lọc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            dgvDuLieuTaiLieu.DataSource = null;
            dgvDuLieuTaiLieu.DataSource = danhSach;

            // Cấu hình lại cột nếu cần
            dgvDuLieuTaiLieu.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            // Xóa các ô input chi tiết sau khi tìm kiếm
            ClearInputs();
            btnHuy.Enabled = true;
        }

        // Tùy chọn: Hàm xử lý khi Form tìm kiếm bị đóng
        private void SearchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_searchForm != null)
            {
                _searchForm.OnSearchApplied -= HandleSearchAppliedTaiLieu;
            }
            _searchForm = null;
        }
        #endregion

        #region CHỨC NĂNG PHỤ (BẢN SAO, HÌNH ẢNH)

        private void btnXemBanSao_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem đã chọn Tài liệu chưa
            if (string.IsNullOrEmpty(_selectedMaTL))
            {
                MessageBox.Show("Vui lòng chọn một Tài liệu từ danh sách chính.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2. Lấy thông tin Tài liệu cha
            string maTL = _selectedMaTL;
            string tenTL = txtTenTL.Text;

            // 3. Khởi tạo và mở Form Bản sao (dưới dạng Dialog)
            // Truyền MaTL và TenTL vào constructor
            frmBanSao formBanSao = new frmBanSao(maTL, tenTL);

            // Mở form dưới dạng Dialog để chặn tương tác với form cha
            formBanSao.ShowDialog();

            // 4. (Tùy chọn) Tải lại dữ liệu sau khi form đóng
            // (Không bắt buộc, vì form bản sao không ảnh hưởng đến DGV chính)
            // LoadData(); 
        }

        private void btnXemHinhAnh_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem đã chọn Tài liệu chưa
            if (string.IsNullOrEmpty(_selectedMaTL))
            {
                MessageBox.Show("Vui lòng chọn một Tài liệu từ danh sách chính.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2. Lấy thông tin chi tiết của Tài liệu để lấy đường dẫn ảnh hiện tại
            TaiLieuDTO model = _bll.LayChiTietTaiLieu(_selectedMaTL);
            if (model == null)
            {
                MessageBox.Show("Không thể tải chi tiết tài liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Khởi tạo và mở Form Hình ảnh
            // Truyền MaTL và Đường dẫn ảnh hiện tại (model.Anh)
            frmHinhAnh formHinhAnh = new frmHinhAnh(model.MaTL, model.Anh);

            DialogResult result = formHinhAnh.ShowDialog();

            // 4. (Tùy chọn) Cập nhật lại thông tin nếu ảnh đã thay đổi
            if (result == DialogResult.OK)
            {
                // Tải lại chi tiết model (không bắt buộc, nhưng tốt để làm mới dữ liệu)
                dgvDuLieuTaiLieu_CellClick(dgvDuLieuTaiLieu,
                    new DataGridViewCellEventArgs(dgvDuLieuTaiLieu.CurrentCell.ColumnIndex, dgvDuLieuTaiLieu.CurrentRow.Index));
            }
        }

        #endregion

        #region CHỨC NĂNG XUẤT EXCEL

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (!SessionManager.RequirePermission(MODULE_NAME, Permission.Export, "xuất excel tài liệu")) return;
            try
            {
                // 1. Lấy dữ liệu (Đã bao gồm các trường chi tiết và chuỗi tác giả gộp)
                List<TaiLieuDTO> dataList = _bll.LayDuLieuXuatExcel();

                if (dataList == null || dataList.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo");
                    return;
                }

                // 2. Cấu hình các cột xuất Excel
                // Key = Tên Property trong DTO
                // Value = Tên Header trên Excel
                Dictionary<string, string> allColumns = new Dictionary<string, string>
                {
                    { "MaTL", "Mã Tài Liệu" },
                    { "TenTL", "Tên Tài Liệu" },
                    { "TacGiaExcel", "Tác Giả & Vai Trò" }, // Cột gộp tác giả
                    { "TenNXB", "Nhà Xuất Bản" },
                    { "TenThL", "Thể Loại" }, // Chú ý: Property là TenThL (không phải TenTheLoai)
                    { "TenDD", "Định Dạng" },
                    { "TenNN", "Ngôn Ngữ" },
                    
                    // >>> CÁC CỘT MỚI BỔ SUNG <<<
                    { "NamXuatBan", "Năm XB" },
                    { "LanXuatBan", "Lần XB" },
                    { "SoTrang", "Số Trang" },
                    { "KhoCo", "Khổ Cỡ" }
                };

                // 3. Mở Form chọn cột
                frmChonCotXuatExcel frm = new frmChonCotXuatExcel(allColumns);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Dictionary<string, string> selectedColumns = frm.SelectedColumns;

                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                        sfd.FileName = $"DanhSachTaiLieu_{DateTime.Now:ddMMyyyy}.xlsx";

                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            // Gọi Helper xuất file
                            bool success = ExcelHelper.ExportToExcel(dataList, selectedColumns, sfd.FileName);
                            if (success) MessageBox.Show("Xuất file Excel thành công!", "Thông báo");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi");
            }
        }

        #endregion

        #region XỬ LÝ SỰ KIỆN CÁC NÚT - LƯU - HỦY
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            // 1. Thu thập Model
            TaiLieuDTO model = GetModelFromInputs();

            // 2. Gán MaTK từ SessionManager (Quan trọng)
            model.MaTK = Helpers.SessionManager.GetMaTaiKhoan();

            try
            {
                if (_currentState == State.CREATE)
                {
                    // --- LOGIC CREATE ---
                    string newMaTL = _bll.ThemTaiLieu(model);

                    if (newMaTL != null && newMaTL != string.Empty)
                    {
                        MessageBox.Show($"Tạo Tài liệu thành công. Mã TL: {newMaTL}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // LƯU DANH SÁCH TÁC GIẢ (Vùng nhớ)
                        _bll.LuuDanhSachTacGia(newMaTL, _danhSachTacGiaTam);

                        MessageBox.Show($"Tạo Tài liệu và Tác giả thành công. Mã TL: {newMaTL}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        SetState(State.READ);
                        _selectedMaTL = newMaTL;
                    }
                    else if (newMaTL == null)
                    {
                        MessageBox.Show("Lỗi hệ thống/nghiệp vụ: Không thể sinh mã (có thể do lỗi SP hoặc NXB không hợp lệ).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Thêm Tài liệu thất bại (Lỗi CSDL).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (_currentState == State.UPDATE)
                {
                    // --- LOGIC UPDATE ---
                    model.MaTL = _selectedMaTL; // Đảm bảo gán Mã TL đang sửa

                    // Gọi BLL (bao gồm cả đồng bộ Tác giả)
                    if (_bll.CapNhatTaiLieu(model, _danhSachTacGiaTam))
                    {
                        MessageBox.Show("Cập nhật Tài liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        SetState(State.READ);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật Tài liệu thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (!string.IsNullOrEmpty(_selectedMaTL))
                {
                    try
                    {
                        TaiLieuDTO model = _bll.LayChiTietTaiLieu(_selectedMaTL);
                        if (model != null)
                        {
                            LoadModelToInputs(model);
                            // Khôi phục DGV Tác giả (và vùng nhớ _danhSachTacGiaTam)
                            LoadTacGiaDinhKem(model.DanhSachTacGia);
                        }
                        else
                        {
                            ClearInputs();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi tải lại dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ClearInputs();
                    }
                }
            }
            // Nếu là CREATE, chỉ cần SetState(READ) (vì nó sẽ gọi ClearInputs)
            SetState(State.READ);
            LoadData();
        }
        #endregion

        #region HÀM BỔ TRỢ
        // Hàm hỗ trợ chọn màu và text dựa trên trạng thái
        private void TriggerStatusEvent(State state)
        {
            string title = "QUẢN LÝ TÀI LIỆU";
            Color backColor = Color.FromArgb(32, 36, 104); // Màu xanh mặc định
            Color foreColor = Color.White;

            switch (state)
            {
                case State.CREATE:
                    title = "THÊM TÀI LIỆU MỚI";
                    backColor = Color.SeaGreen;
                    break;
                case State.UPDATE:
                    title = "CẬP NHẬT TÀI LIỆU";
                    backColor = Color.DarkOrange;
                    break;
                case State.READ:
                default:
                    title = "DANH SÁCH TÀI LIỆU";
                    backColor = Color.FromArgb(32, 36, 104);
                    break;
            }

            // Bắn sự kiện ra ngoài cho Form cha bắt
            OnStatusRequest?.Invoke(this, new StatusRequestEventArgs(title, backColor, foreColor));
        }
        // Hàm xử lý logic Thêm Tác giả vào vùng nhớ
        private void HandleThemTacGiaVaoList()
        {
            var tacGiaDaChon = (dynamic)cboTacGia.SelectedItem;
            string vaiTro = cboVaiTro.SelectedItem?.ToString();

            if (tacGiaDaChon == null || string.IsNullOrEmpty(vaiTro))
            {
                MessageBox.Show("Vui lòng chọn Tác giả và Vai trò.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maTG = tacGiaDaChon.Ma;

            // Kiểm tra trùng lặp
            if (_danhSachTacGiaTam.Any(tg => tg.MaTG == maTG))
            {
                MessageBox.Show("Tác giả này đã có trong danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Thêm vào vùng nhớ
            TL_TGDTO tacGiaMoi = new TL_TGDTO
            {
                MaTG = maTG,
                VaiTro = vaiTro,
                // Lấy HoTenTG từ BLL (vì cboTacGia.HienThi là Ma-Ten)
                HoTenTG = _bll.LayChiTietTacGia(maTG).HoTen
            };
            _danhSachTacGiaTam.Add(tacGiaMoi);

            // Cập nhật DGV (Chỉ Refresh, không xóa)
            RefreshDgvTacGia();
            ClearTacGiaCombos();
        }

        // Hàm xử lý Sửa Vai trò Tác giả trong vùng nhớ
        private void HandleSuaTacGiaTrongList()
        {
            if (_selectedTacGiaTam == null)
            {
                MessageBox.Show("Vui lòng chọn một tác giả từ danh sách để sửa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy vai trò mới từ ComboBox
            string vaiTroMoi = cboVaiTro.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(vaiTroMoi))
            {
                MessageBox.Show("Vui lòng chọn Vai trò mới.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cập nhật Vai trò trong vùng nhớ
            _selectedTacGiaTam.VaiTro = vaiTroMoi;

            // Cập nhật DGV
            RefreshDgvTacGia();
            ClearTacGiaCombos();

            // Tắt nút Sửa/Xóa (bắt buộc chọn lại)
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            _selectedTacGiaTam = null;
        }

        // Hàm xử lý logic Xóa Tác giả khỏi vùng nhớ
        private void HandleXoaTacGiaKhoiList()
        {
            if (_selectedTacGiaTam == null)
            {
                MessageBox.Show("Vui lòng chọn Tác giả cần xóa khỏi danh sách.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _danhSachTacGiaTam.Remove(_selectedTacGiaTam);

            RefreshDgvTacGia();
            ClearTacGiaCombos();

            // Tắt nút Sửa/Xóa (bắt buộc chọn lại)
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            _selectedTacGiaTam = null;
        }

        // Hàm mới để Refresh DGV (tránh load lại toàn bộ)
        private void RefreshDgvTacGia()
        {
            // Tắt và bật lại DataSource để refresh DGV
            dgvDuLieuTacGia.DataSource = null;
            dgvDuLieuTacGia.DataSource = _danhSachTacGiaTam;
            dgvDuLieuTacGia.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void ClearTacGiaCombos()
        {
            cboTacGia.SelectedIndex = -1;
            cboVaiTro.SelectedIndex = -1;
        }

        private void ClearInputs()
        {
            txtMaTL.Text = string.Empty;
            txtTenTL.Text = string.Empty;
            txtNamXuatBan.Text = string.Empty;

            nudLanXuatBan.Value = 1;
            nudSoTrang.Value = 0;

            cboNhaXuatBan.SelectedIndex = -1;
            cboNgonNgu.SelectedIndex = -1;
            cboTheLoai.SelectedIndex = -1;
            cboDinhDang.SelectedIndex = -1;
            cboKhoCo.SelectedIndex = -1;

            ClearTacGiaCombos();

            dgvDuLieuTacGia.DataSource = null;

            // Đặt trạng thái nút
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnXemHinhAnh.Enabled = false;
            btnXemBanSao.Enabled = false;
        }

        private TaiLieuDTO GetModelFromInputs()
        {
            return new TaiLieuDTO
            {
                // MaTL sẽ được sinh trong BLL
                TenTL = txtTenTL.Text.Trim(),
                MaNXB = cboNhaXuatBan.SelectedValue?.ToString(),
                MaNN = cboNgonNgu.SelectedValue?.ToString(),
                MaThL = cboTheLoai.SelectedValue?.ToString(),
                MaDD = cboDinhDang.SelectedValue?.ToString(),

                // Convert giá trị số
                LanXuatBan = (int)nudLanXuatBan.Value,
                NamXuatBan = int.TryParse(txtNamXuatBan.Text, out int namXB) ? (int?)namXB : null,
                SoTrang = (int)nudSoTrang.Value,

                KhoCo = cboKhoCo.SelectedItem?.ToString(), // Lấy SelectedItem vì DataSource là List<string>
                Anh = null // Chưa hỗ trợ ảnh
            };
        }

        private bool ValidateInputs()
        {
            // 1. Kiểm tra trường bắt buộc (Tên và các ComboBox)
            if (string.IsNullOrEmpty(txtTenTL.Text.Trim()) ||
                cboNhaXuatBan.SelectedIndex == -1 ||
                cboNgonNgu.SelectedIndex == -1 ||
                cboTheLoai.SelectedIndex == -1 ||
                cboDinhDang.SelectedIndex == -1 ||
                cboKhoCo.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên Tài liệu và chọn các danh mục bắt buộc (NXB, Ngôn ngữ, Thể loại, Định dạng, Khổ cỡ).", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 2. Kiểm tra năm xuất bản (có thể rỗng, nhưng nếu nhập phải hợp lệ)
            if (!string.IsNullOrEmpty(txtNamXuatBan.Text))
            {
                if (!int.TryParse(txtNamXuatBan.Text, out int namXB) || namXB < 1000 || namXB > DateTime.Now.Year)
                {
                    MessageBox.Show("Năm Xuất Bản không hợp lệ (phải là số, không lớn hơn năm hiện tại).", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNamXuatBan.Focus();
                    return false;
                }
            }

            // 3. Kiểm tra các NumericUpDown (cho phép bằng 0)
            if (nudLanXuatBan.Value <= 0)
            {
                MessageBox.Show("Lần Xuất Bản phải lớn hơn 0.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudLanXuatBan.Focus();
                return false;
            }

            // SoTrang có thể bằng 0 (ví dụ: tài liệu video)

            return true;
        }
        #endregion
    }
}
