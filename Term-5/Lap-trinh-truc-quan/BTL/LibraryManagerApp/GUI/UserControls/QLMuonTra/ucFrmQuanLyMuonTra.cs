// File: LibraryManagerApp.GUI.UserControls.QLMuonTra/ucFrmQuanLyMuonTra.cs

using LibraryManagerApp.BLL;
using LibraryManagerApp.DTO;
using LibraryManagerApp.GUI.Forms;
using LibraryManagerApp.GUI.UserControls.QLBanDoc;
using LibraryManagerApp.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LibraryManagerApp.GUI.UserControls.QLMuonTra
{
    public partial class ucFrmQuanLyMuonTra : UserControl
    {
        private GiaoDichBLL _bll = new GiaoDichBLL();

        // Enum nội bộ để quản lý 3 chế độ
        private enum MuonTraState { READ, CREATE_GIAODICH, UPDATE_GIAODICH }
        private MuonTraState _currentState;

        // Biến lưu trữ thông tin đang thao tác
        private string _selectedMaGD; // Giao dịch đang chọn (cho chế độ Trả)
        private TheBanDocDTO _theBanDocHopLe = null; // Thẻ bạn đọc đã được xác thực
        private List<GiaoDich_BanSaoDTO> _danhSachBanSaoTam = new List<GiaoDich_BanSaoDTO>(); // "Giỏ hàng"
        public event EventHandler<StatusRequestEventArgs> OnStatusRequest;

        #region KHỞI TẠO VÀ CẤU HÌNH
        public ucFrmQuanLyMuonTra()
        {
            InitializeComponent();
            ConfigureDGV();

            // Gọi trực tiếp delegate nếu muốn cập nhật label khi khởi tạo
            this.OnStatusRequest += UcMuonTra_OnStatusRequest;
        }

        // Hàm xử lý event
        private void UcMuonTra_OnStatusRequest(object sender, StatusRequestEventArgs e)
        {
            label1.Text = e.TitleText;
            label1.BackColor = e.BackColor;
            label1.ForeColor = e.ForeColor;
        }

        private void ucFrmQuanLyMuonTra_Load(object sender, EventArgs e)
        {
            // Tải trạng thái mặc định
            LoadData(); // Tải DGV Master
            SetState(MuonTraState.READ);

            // Cấu hình ComboBox
            cboTrangThaiGD.Items.AddRange(new string[] { "Đang mượn", "Đã trả", "Trễ hạn" });

            LoadAutoCompleteSources();
        }

        private void LoadAutoCompleteSources()
        {
            try
            {
                // 1. Gợi ý Mã Thẻ Bạn Đọc
                // (Giả sử bạn đã thêm hàm LayDanhSachMaTBD() vào BLL)
                List<string> listMaTBD = _bll.LayDanhSachMaTBD();
                SetupAutoComplete(txtMaTBD, listMaTBD);

                // 2. Gợi ý Mã Bản Sao (Chỉ lấy những cuốn "Có sẵn")
                List<string> listMaBS = _bll.LayDanhSachMaBSCoSan();
                SetupAutoComplete(txtMaBS, listMaBS);
            }
            catch (Exception ex)
            {
                // Không hiện lỗi để tránh phiền người dùng, chỉ log
                Console.WriteLine("Lỗi load Autocomplete: " + ex.Message);
            }
        }

        private void SetupAutoComplete(TextBox txt, List<string> source)
        {
            if (source != null && source.Count > 0)
            {
                txt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                col.AddRange(source.ToArray());
                txt.AutoCompleteCustomSource = col;
            }
        }

        private void ConfigureDGV()
        {
            // Cấu hình chung cho cả 2 DGV
            ConfigureDGVStyle(dgvDuLieuPhieuGD);
            ConfigureDGVStyle(dgvDuLieuBanSao);

            ConfigureDGVForGiaoDich();
            ConfigureDGVForBanSao();
        }

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

        private void ConfigureDGVForGiaoDich()
        {
            dgvDuLieuPhieuGD.Columns.Clear();
            dgvDuLieuPhieuGD.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã GD", DataPropertyName = "MaGD", Name = "MaGD", Width = 100 });

            // Tên Bạn Đọc: Fill
            var colTenBD = new DataGridViewTextBoxColumn { HeaderText = "Tên Bạn Đọc", DataPropertyName = "HoTenBD", Name = "HoTenBD" };
            colTenBD.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTenBD.MinimumWidth = 150;
            dgvDuLieuPhieuGD.Columns.Add(colTenBD);

            dgvDuLieuPhieuGD.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ngày Mượn", DataPropertyName = "NgayMuon", Name = "NgayMuon", DefaultCellStyle = { Format = "dd/MM/yyyy", Alignment = DataGridViewContentAlignment.MiddleCenter }, Width = 100 });
            dgvDuLieuPhieuGD.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ngày Hẹn Trả", DataPropertyName = "NgayHenTra", Name = "NgayHenTra", DefaultCellStyle = { Format = "dd/MM/yyyy", Alignment = DataGridViewContentAlignment.MiddleCenter }, Width = 100 });
            dgvDuLieuPhieuGD.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Trạng Thái", DataPropertyName = "TrangThai", Name = "TrangThai", Width = 100 });
        }

        private void ConfigureDGVForBanSao()
        {
            dgvDuLieuBanSao.Columns.Clear();
            dgvDuLieuBanSao.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã Bản Sao", DataPropertyName = "MaBS", Name = "MaBS", Width = 120 });

            // Tên Tài Liệu: Fill
            var colTenTL = new DataGridViewTextBoxColumn { HeaderText = "Tên Tài Liệu", DataPropertyName = "TenTL", Name = "TenTL" };
            colTenTL.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTenTL.MinimumWidth = 200;
            dgvDuLieuBanSao.Columns.Add(colTenTL);
        }
        #endregion

        #region QUẢN LÝ TRẠNG THÁI (STATE)
        private void SetState(MuonTraState state)
        {
            _currentState = state;
            ClearInputs();

            bool isReading = (state == MuonTraState.READ);
            bool isCreating = (state == MuonTraState.CREATE_GIAODICH);
            bool isUpdating = (state == MuonTraState.UPDATE_GIAODICH);

            // 1. Inputs Giao dịch (groupBox5)
            txtMaGD.Enabled = false;
            txtNhanVien.Enabled = false;
            dtpNgayMuon.Enabled = false;
            dtpNgayHenTra.Enabled = isCreating;
            dtpNgayTra.Enabled = isUpdating;
            cboTrangThaiGD.Enabled = isUpdating;

            // 2. Inputs Bạn Đọc (groupBox1)
            txtMaTBD.Enabled = isCreating;
            btnKiemTraThe.Enabled = isCreating;

            // 3. Inputs Bản Sao (groupBox7)
            txtMaBS.Enabled = isCreating;
            btnThemBanSao.Enabled = isCreating;
            txtTenTL.Enabled = false;
            textBox1.Enabled = false;

            // 4. DGV
            dgvDuLieuPhieuGD.Enabled = isReading;
            dgvDuLieuBanSao.Enabled = true;

            // --- SỬA LỖI CHECKBOX: Cấu hình cột ---
            if (isUpdating)
            {
                // Phải cho phép sửa lưới để click được Checkbox
                dgvDuLieuBanSao.ReadOnly = false;

                // Thêm cột CheckBox nếu chưa có
                if (!dgvDuLieuBanSao.Columns.Contains("ChonTra"))
                {
                    DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn
                    {
                        Name = "ChonTra",
                        HeaderText = "Chọn Trả",
                        Width = 60,
                        ReadOnly = false // Cột này cho phép sửa
                    };
                    dgvDuLieuBanSao.Columns.Add(chkCol);
                }
                else
                {
                    dgvDuLieuBanSao.Columns["ChonTra"].Visible = true;
                    dgvDuLieuBanSao.Columns["ChonTra"].ReadOnly = false;
                }

                // KHÓA các cột thông tin khác (không cho sửa text)
                foreach (DataGridViewColumn col in dgvDuLieuBanSao.Columns)
                {
                    if (col.Name != "ChonTra") col.ReadOnly = true;
                }
            }
            else // READ hoặc CREATE
            {
                // Ẩn cột CheckBox
                if (dgvDuLieuBanSao.Columns.Contains("ChonTra"))
                {
                    dgvDuLieuBanSao.Columns["ChonTra"].Visible = false;
                }
                // Khóa toàn bộ bảng
                dgvDuLieuBanSao.ReadOnly = true;
            }

            // 5. Nút Thao tác (groupBox3)
            btnLapPhieuMuon.Enabled = isReading;
            btnXacNhanPhieuTra.Enabled = isReading && !string.IsNullOrEmpty(_selectedMaGD);
            btnTimPhieuGiaoDich.Enabled = isReading;
            btnXoaPhieuGD.Enabled = isReading && !string.IsNullOrEmpty(_selectedMaGD);

            btnLuuThaoTac.Enabled = isCreating || isUpdating;
            btnHuyThaoTac.Enabled = isCreating || isUpdating;

            // 6. Nút Vùng nhớ (groupBox2)
            btnXoaBanSao.Enabled = isCreating;

            // 7. Xử lý logic riêng khi vào State
            if (isCreating)
            {
                txtMaGD.Text = "(Mã GD Mới)";
                txtNhanVien.Text = SessionManager.CurrentUser.HoTenNV;
                dtpNgayMuon.Value = DateTime.Now;
                dtpNgayHenTra.Value = DateTime.Now.AddDays(14);
                cboTrangThaiGD.SelectedItem = "Đang mượn";
                _danhSachBanSaoTam.Clear();
                RefreshDgvBanSaoTam();
            }

            State mappedState = State.READ; // mặc định

            switch (state)
            {
                case MuonTraState.CREATE_GIAODICH:
                    mappedState = State.CREATE;
                    break;
                case MuonTraState.UPDATE_GIAODICH:
                    mappedState = State.UPDATE;
                    break;
                case MuonTraState.READ:
                default:
                    mappedState = State.READ;
                    break;
            }

            TriggerStatusEvent(mappedState);
        }
        #endregion

        #region CHỨC NĂNG READ (LOAD)
        private void LoadData()
        {
            try
            {
                dgvDuLieuPhieuGD.DataSource = null;
                List<GiaoDichDTO> danhSach = _bll.LayTatCaGiaoDich();
                dgvDuLieuPhieuGD.DataSource = danhSach;
                dgvDuLieuPhieuGD.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Giao dịch: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDuLieuPhieuGD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvDuLieuPhieuGD.RowCount) return;
            if (_currentState != MuonTraState.READ) return;

            _selectedMaGD = dgvDuLieuPhieuGD.Rows[e.RowIndex].Cells["MaGD"].Value.ToString();
            GiaoDichDTO gd = _bll.LayTatCaGiaoDich().FirstOrDefault(g => g.MaGD == _selectedMaGD);
            if (gd == null) return;

            // Load Inputs Giao dịch (Khu vực 5)
            txtMaGD.Text = gd.MaGD;
            txtNhanVien.Text = gd.HoTenNV;
            dtpNgayMuon.Value = gd.NgayMuon;
            dtpNgayHenTra.Value = gd.NgayHenTra;
            dtpNgayTra.Value = gd.NgayTra ?? DateTime.Now;
            dtpNgayTra.Visible = gd.NgayTra.HasValue;
            cboTrangThaiGD.SelectedItem = gd.TrangThai;

            // Load Inputs Bạn Đọc (Khu vực 1)
            txtMaTBD.Text = gd.MaTBD;
            lblHoTenBD.Text = gd.HoTenBD;
            lblTrangThaiThe.Text = "";

            // Load DGV Chi tiết (Khu vực 2)
            LoadChiTietGiaoDich(_selectedMaGD);

            // Cập nhật trạng thái nút khi ở READ
            if (_currentState == MuonTraState.READ)
            {
                bool isDangMuon = (gd.TrangThai == "Đang mượn");
                btnXacNhanPhieuTra.Enabled = isDangMuon;
                btnXoaPhieuGD.Enabled = true;
            }
        }

        private void LoadChiTietGiaoDich(string maGD)
        {
            try
            {
                _danhSachBanSaoTam = _bll.LayChiTietGiaoDich(maGD);
                RefreshDgvBanSaoTam();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết giao dịch: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDuLieuBanSao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvDuLieuBanSao.RowCount) return;

            // Lấy đối tượng từ hàng được chọn
            GiaoDich_BanSaoDTO item = dgvDuLieuBanSao.Rows[e.RowIndex].DataBoundItem as GiaoDich_BanSaoDTO;

            if (item != null)
            {
                // Hiển thị lên các Input (Khu vực 3 - Thêm Bản Sao)
                // Dù đang ở chế độ Trả, việc hiển thị này giúp người dùng biết mình đang chọn sách gì
                txtMaBS.Text = item.MaBS;
                txtTenTL.Text = item.TenTL;

                // Hiển thị trạng thái (Dựa trên TinhTrang boolean)
                textBox1.Text = item.TinhTrang ? "Đã trả" : "Đang mượn";
            }
        }
        #endregion

        #region CHỨC NĂNG CREATE (LẬP PHIẾU MƯỢN)

        // 1. Kích hoạt chế độ CREATE
        private void btnLapPhieuMuon_Click(object sender, EventArgs e)
        {
            SetState(MuonTraState.CREATE_GIAODICH);
        }

        // 2. Xác thực Bạn đọc
        private void btnKiemTraThe_Click(object sender, EventArgs e)
        {
            try
            {
                string maTBD = txtMaTBD.Text.Trim();
                _theBanDocHopLe = _bll.KiemTraTheBanDoc(maTBD);

                lblHoTenBD.Text = _theBanDocHopLe.HoTenBD;
                lblTrangThaiThe.Text = _theBanDocHopLe.TrangThai;
                lblTrangThaiThe.ForeColor = Color.Green;
                txtMaBS.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Xác thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblHoTenBD.Text = "Họ tên BD:";
                lblTrangThaiThe.Text = "Không hợp lệ";
                lblTrangThaiThe.ForeColor = Color.Red;
                _theBanDocHopLe = null;
            }
        }

        // 3. Thêm Bản sao vào "Giỏ hàng" (Vùng nhớ)
        private void btnThemBanSao_Click(object sender, EventArgs e)
        {
            try
            {
                string maBS = txtMaBS.Text.Trim();

                // 1. Kiểm tra Input rỗng
                if (string.IsNullOrEmpty(maBS))
                {
                    MessageBox.Show("Vui lòng nhập Mã Bản Sao.", "Thông báo");
                    return;
                }

                // 2. Kiểm tra Bạn đọc
                if (_theBanDocHopLe == null)
                {
                    MessageBox.Show("Vui lòng xác thực Bạn Đọc trước.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Kiểm tra trùng trong "Giỏ hàng"
                if (_danhSachBanSaoTam.Any(bs => bs.MaBS == maBS))
                {
                    MessageBox.Show($"Bản sao {maBS} đã có trong danh sách mượn.", "Thông báo");
                    txtMaBS.Clear();
                    return;
                }

                // 4. Gọi BLL Kiểm tra (Có thể hàm này đang ném exception khi không tìm thấy?)
                // Hãy đảm bảo KiemTraBanSao ném Exception cụ thể nếu lỗi
                BanSaoKiemTraDTO banSao = _bll.KiemTraBanSao(maBS);

                // Nếu chạy đến đây nghĩa là KHÔNG CÓ LỖI từ BLL

                // Hiển thị chi tiết
                txtTenTL.Text = banSao.TenTL;
                textBox1.Text = banSao.TrangThai;

                // 5. Thêm vào Vùng nhớ
                _danhSachBanSaoTam.Add(new GiaoDich_BanSaoDTO
                {
                    MaBS = banSao.MaBS,
                    TenTL = banSao.TenTL,
                    TinhTrang = false
                });

                RefreshDgvBanSaoTam();
                txtMaBS.Clear();
                txtMaBS.Focus();
            }
            catch (Exception ex)
            {
                // Đây là nơi hứng lỗi từ BLL (VD: "Mã bản sao không tồn tại" hoặc "Đã được mượn")
                MessageBox.Show(ex.Message, "Không thể thêm bản sao", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Reset text hiển thị lỗi
                txtTenTL.Text = "Không tìm thấy";
                textBox1.Text = "";
                txtMaBS.SelectAll();
                txtMaBS.Focus();
            }
        }

        // 4. Xóa Bản sao khỏi "Giỏ hàng"
        private void btnXoaBanSao_Click(object sender, EventArgs e)
        {
            if (_currentState != MuonTraState.CREATE_GIAODICH) return;
            if (dgvDuLieuBanSao.SelectedRows.Count == 0) return;

            string maBSCanXoa = dgvDuLieuBanSao.SelectedRows[0].Cells["MaBS"].Value.ToString();

            GiaoDich_BanSaoDTO item = _danhSachBanSaoTam.FirstOrDefault(i => i.MaBS == maBSCanXoa);
            if (item != null)
            {
                _danhSachBanSaoTam.Remove(item);
                RefreshDgvBanSaoTam();
            }
        }

        #endregion

        #region CHỨC NĂNG UPDATE (TRẢ SÁCH)

        // 1. Kích hoạt chế độ UPDATE (Trả sách)
        private void btnXacNhanPhieuTra_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedMaGD))
            {
                MessageBox.Show("Vui lòng chọn một Giao dịch 'Đang mượn' từ danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SetState(MuonTraState.UPDATE_GIAODICH);

            // Load lại chi tiết (để đảm bảo DGV Chi tiết có thể Check)
            LoadChiTietGiaoDich(_selectedMaGD);

            // Cập nhật các trường cho chế độ Trả
            dtpNgayTra.Value = DateTime.Now;
            cboTrangThaiGD.SelectedItem = "Đã trả";
        }

        private void dgvDuLieuBanSao_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Chỉ xử lý khi đang ở chế độ Trả và click vào đúng cột CheckBox
            if (_currentState == MuonTraState.UPDATE_GIAODICH &&
                dgvDuLieuBanSao.Columns[e.ColumnIndex].Name == "ChonTra" &&
                e.RowIndex >= 0)
            {
                // Cam kết thay đổi ngay lập tức (để code phía sau đọc được value mới nhất)
                dgvDuLieuBanSao.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        #endregion

        #region CHỨC NĂNG DELETE (XÓA PHIẾU)
        private void btnXoaPhieuGD_Click(object sender, EventArgs e)
        {
            if (_currentState != MuonTraState.READ || string.IsNullOrEmpty(_selectedMaGD))
            {
                MessageBox.Show("Vui lòng chọn một Giao dịch từ danh sách để xóa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy thông tin để hiển thị xác nhận
            string maGD = _selectedMaGD;
            string hoTenBD = lblHoTenBD.Text;

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa Giao dịch:\n[ {maGD} - {hoTenBD} ] không?\n\n(Hệ thống sẽ tự động cập nhật lại trạng thái 'Có sẵn' cho các bản sao liên quan)",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (_bll.XoaGiaoDich(maGD))
                    {
                        MessageBox.Show("Xóa Giao dịch thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                        SetState(MuonTraState.READ); // Đảm bảo các nút được reset
                    }
                    else
                    {
                        MessageBox.Show("Xóa Giao dịch thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region XUẤT PHIẾU
        private void btnXuatPhieu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedMaGD))
            {
                MessageBox.Show("Vui lòng chọn một Giao dịch để in phiếu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // 1. Lấy dữ liệu in từ BLL
                System.Data.DataTable dt = _bll.LayDuLieuInPhieu(_selectedMaGD);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // 2. Mở Form báo cáo
                    frmBaoCaoPhieu frm = new frmBaoCaoPhieu(dt);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để in.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất phiếu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region XỬ LÝ SỰ KIỆN CÁC NÚT - LƯU - HỦY
        private void btnLuuThaoTac_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentState == MuonTraState.CREATE_GIAODICH)
                {
                    // --- LOGIC LƯU PHIẾU MƯỢN ---
                    if (!ValidateInputs_Create()) return;

                    GiaoDichDTO model = GetModelFromInputs_Create();
                    string newMaGD = _bll.LapPhieuMuon(model, _danhSachBanSaoTam);

                    if (!string.IsNullOrEmpty(newMaGD))
                    {
                        MessageBox.Show($"Lập Phiếu Mượn thành công. Mã GD: {newMaGD}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        SetState(MuonTraState.READ);
                    }
                    else
                    {
                        MessageBox.Show("Lập Phiếu Mượn thất bại. (Lỗi CSDL hoặc SP).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (_currentState == MuonTraState.UPDATE_GIAODICH)
                {
                    // --- LOGIC LƯU PHIẾU TRẢ ---
                    HandleLuuPhieuTra();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống khi Lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuyThaoTac_Click(object sender, EventArgs e)
        {
            SetState(MuonTraState.READ);
        }
        #endregion

        #region HÀM BỔ TRỢ
        // Hàm hỗ trợ chọn màu và text dựa trên trạng thái
        private void TriggerStatusEvent(State state)
        {
            string title = "QUẢN LÝ GIAO DỊCH MƯỢN TRẢ";
            Color backColor = Color.FromArgb(32, 36, 104); // Màu xanh mặc định
            Color foreColor = Color.White;

            switch (state)
            {
                case State.CREATE:
                    title = "THÊM GIAO DỊCH MỚI";
                    backColor = Color.SeaGreen;
                    break;
                case State.UPDATE:
                    title = "CẬP NHẬT GIAO DỊCH";
                    backColor = Color.DarkOrange;
                    break;
                case State.READ:
                default:
                    title = "DANH SÁCH LỊCH SỬ GIAO DỊCH";
                    backColor = Color.FromArgb(32, 36, 104);
                    break;
            }

            // Bắn sự kiện ra ngoài cho Form cha bắt
            OnStatusRequest?.Invoke(this, new StatusRequestEventArgs(title, backColor, foreColor));
        }
        private void ClearInputs()
        {
            // Khu vực 5 (Giao dịch)
            txtMaGD.Text = string.Empty;
            txtNhanVien.Text = string.Empty;
            dtpNgayMuon.Value = DateTime.Now;
            dtpNgayHenTra.Value = DateTime.Now;
            dtpNgayTra.Value = DateTime.Now;
            cboTrangThaiGD.SelectedIndex = -1;

            // Khu vực 1 (Bạn đọc)
            txtMaTBD.Text = string.Empty;
            lblHoTenBD.Text = "Họ tên BD:";
            lblTrangThaiThe.Text = "Trạng thái thẻ:";
            lblTrangThaiThe.ForeColor = SystemColors.ControlText;
            _theBanDocHopLe = null;

            // Khu vực 7 (Bản sao)
            txtMaBS.Text = string.Empty;
            txtTenTL.Text = string.Empty;
            textBox1.Text = string.Empty;

            // Khu vực 2 (DGV Chi tiết)
            _danhSachBanSaoTam.Clear();
            RefreshDgvBanSaoTam();

            // Tắt nút khi Clear
            btnXacNhanPhieuTra.Enabled = false;
            btnXoaPhieuGD.Enabled = false;
        }

        private void RefreshDgvBanSaoTam()
        {
            dgvDuLieuBanSao.DataSource = null;
            if (_danhSachBanSaoTam.Count > 0)
            {
                dgvDuLieuBanSao.DataSource = _danhSachBanSaoTam;
            }
            dgvDuLieuBanSao.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private GiaoDichDTO GetModelFromInputs_Create()
        {
            return new GiaoDichDTO
            {
                MaTBD = _theBanDocHopLe.MaTBD,
                MaTK = SessionManager.GetMaTaiKhoan(),
                NgayMuon = dtpNgayMuon.Value.Date,
                NgayHenTra = dtpNgayHenTra.Value.Date,
                TrangThai = "Đang mượn"
            };
        }

        private bool ValidateInputs_Create()
        {
            if (_theBanDocHopLe == null)
            {
                MessageBox.Show("Vui lòng xác thực Thẻ Bạn Đọc hợp lệ.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtpNgayHenTra.Value.Date < dtpNgayMuon.Value.Date)
            {
                MessageBox.Show("Ngày hẹn trả phải lớn hơn hoặc bằng ngày mượn.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (_danhSachBanSaoTam.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một Bản sao vào phiếu mượn.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // Hàm mới: Xử lý logic Lưu Phiếu Trả
        private void HandleLuuPhieuTra()
        {
            // 1. Lấy danh sách Mã Bản Sao được chọn trả
            List<string> danhSachMaBSTra = new List<string>();
            int soLuongChuaTra = 0;

            foreach (DataGridViewRow row in dgvDuLieuBanSao.Rows)
            {
                GiaoDich_BanSaoDTO chiTiet = row.DataBoundItem as GiaoDich_BanSaoDTO;

                DataGridViewCheckBoxCell chk = row.Cells["ChonTra"] as DataGridViewCheckBoxCell;
                bool isChecked = (chk != null && Convert.ToBoolean(chk.Value));

                if (isChecked && chiTiet.TinhTrang == false) // Đang mượn VÀ được chọn trả
                {
                    danhSachMaBSTra.Add(chiTiet.MaBS);
                }
                else if (chiTiet.TinhTrang == false) // Đang mượn VÀ KHÔNG được chọn trả
                {
                    soLuongChuaTra++;
                }
            }

            // 2. Validation
            if (danhSachMaBSTra.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một Bản sao để ghi nhận trả.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Xác định xem có trả hết không
            bool traHet = (soLuongChuaTra == 0);

            // 4. Gọi BLL
            if (_bll.GhiNhanTra(_selectedMaGD, danhSachMaBSTra, traHet))
            {
                MessageBox.Show("Ghi nhận trả sách thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                SetState(MuonTraState.READ);
            }
            else
            {
                MessageBox.Show("Ghi nhận trả sách thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}