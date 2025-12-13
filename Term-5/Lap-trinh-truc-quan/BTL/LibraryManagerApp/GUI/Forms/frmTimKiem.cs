// File: LibraryManagerApp.GUI.Forms/FrmTimKiem.cs

using LibraryManagerApp.DTO;
using LibraryManagerApp.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace LibraryManagerApp.GUI.Forms
{
    // === Đổi tên State thành FilterState để tránh xung đột ===
    public enum FilterState
    {
        READ,   // Chỉ xem, không chỉnh sửa
        CREATE, // Đang tạo mới bộ lọc
        UPDATE  // Đang sửa bộ lọc
    }

    public delegate void SearchAppliedHandler(List<SearchFilter> filters);

    // === Đổi tên lớp thành FrmTimKiem (PascalCase) ===
    public partial class FrmTimKiem : Form
    {
        private List<FieldMetadata> _metadata;
        private List<SearchFilter> _currentFilters = new List<SearchFilter>();
        public event SearchAppliedHandler OnSearchApplied;

        // Public Property để Form cha có thể lấy danh sách Filters sau khi đóng Form
        public List<SearchFilter> Filters
        {
            get { return _currentFilters; }
        }

        // Quản lý State
        private FilterState _currentState; // <-- Đã cập nhật kiểu dữ liệu
        private SearchFilter _selectedFilter = null;

        // === CONSTRUCTOR ĐÃ CHỈNH SỬA: Nhận metadata linh hoạt ===
        public FrmTimKiem(List<FieldMetadata> metadataList)
        {
            InitializeComponent();

            // Gán metadata được truyền vào (Nhân viên, Bạn Đọc, v.v.)
            _metadata = metadataList;

            ConfigureListView();
        }

        // Constructor mặc định (dùng cho Design Time hoặc fallback)
        public FrmTimKiem() : this(SearchMetadata.GetBanDocFields())
        {
        }

        #region KHỞI TẠO VÀ CẤU HÌNH GIAO DIỆN

        private void ConfigureListView()
        {
            // Cấu hình giao diện chung
            lsvBoLoc.View = View.Details;
            lsvBoLoc.FullRowSelect = true; // Chọn toàn bộ hàng
            lsvBoLoc.GridLines = true;     // Hiển thị đường kẻ lưới (cho giống DGV)
            lsvBoLoc.HeaderStyle = ColumnHeaderStyle.Nonclickable; // Header phẳng, không click sort
            lsvBoLoc.MultiSelect = false;  // Chỉ chọn 1 dòng

            lsvBoLoc.Font = new Font("Consolas", 10f, FontStyle.Regular);

            // Cấu hình Cột
            lsvBoLoc.Columns.Clear();
            // Đặt chiều rộng cột bằng chiều rộng ListView trừ đi một chút để không hiện thanh cuộn ngang
            // -25 là khoảng trừ hao cho thanh cuộn dọc và viền
            lsvBoLoc.Columns.Add("Điều kiện Tìm kiếm", lsvBoLoc.Width - 5);
        }

        private void FrmTimKiem_Load(object sender, EventArgs e) // <-- Đã sửa tên phương thức
        {
            LoadTimTheoComboBox();
            SetState(FilterState.READ); // <-- Đã cập nhật
        }

        #endregion

        // ---

        #region QUẢN LÝ STATE (CHO BỘ LỌC)

        private void SetState(FilterState state) // <-- Đã cập nhật kiểu dữ liệu
        {
            _currentState = state;
            bool isEditing = (state == FilterState.CREATE || state == FilterState.UPDATE); // <-- Đã cập nhật

            // INPUTS
            cboTimTheo.Enabled = isEditing;
            cboToanTu.Enabled = isEditing;
            txtGiaTri.Enabled = isEditing && txtGiaTri.Visible;
            txtTu.Enabled = isEditing && txtTu.Visible;
            txtDen.Enabled = isEditing && txtDen.Visible;

            // THAO TÁC TRỰC TIẾP
            btnThemBoLoc.Enabled = (state == FilterState.READ); // <-- Đã cập nhật
            btnSuaBoLoc.Enabled = (state == FilterState.READ && lsvBoLoc.SelectedItems.Count > 0); // <-- Đã cập nhật
            btnXoaBoLoc.Enabled = (state == FilterState.READ && lsvBoLoc.SelectedItems.Count > 0); // <-- Đã cập nhật
            btnTim.Enabled = (state == FilterState.READ); // <-- Đã cập nhật

            // THAO TÁC CRUD STATE
            btnLuu.Enabled = isEditing;
            btnHuy.Enabled = isEditing;

            lsvBoLoc.Enabled = (state == FilterState.READ); // <-- Đã cập nhật

            // Đảm bảo khi chuyển trạng thái, các trường INPUT được reset/cập nhật
            if (state == FilterState.READ) // <-- Đã cập nhật
            {
                ClearFilterInputs();
            }
            else if (state == FilterState.CREATE) // <-- Đã cập nhật
            {
                // Đặt focus vào ComboBox tìm kiếm
                cboTimTheo.Focus();
            }
        }

        private void LsvBoLoc_SelectedIndexChanged(object sender, EventArgs e) // <-- Đã sửa tên phương thức
        {
            if (_currentState == FilterState.READ) // <-- Đã cập nhật
            {
                bool isSelected = lsvBoLoc.SelectedItems.Count > 0;
                btnSuaBoLoc.Enabled = isSelected;
                btnXoaBoLoc.Enabled = isSelected;
            }
        }

        #endregion

        // ---

        #region QUẢN LÝ INPUTS (COMBOBOX VÀ INPUT FIELD)

        private void LoadTimTheoComboBox()
        {
            cboTimTheo.DataSource = _metadata;
            cboTimTheo.DisplayMember = "DisplayName";
            cboTimTheo.ValueMember = "FieldName";

            if (cboTimTheo.Items.Count > 0)
            {
                cboTimTheo.SelectedIndex = 0;
            }
        }

        private void CboTimTheo_SelectedIndexChanged(object sender, EventArgs e) // <-- Đã sửa tên phương thức
        {
            FieldMetadata selectedField = cboTimTheo.SelectedItem as FieldMetadata;

            if (selectedField != null)
            {
                cboToanTu.DataSource = selectedField.SupportedOperators;
                if (cboToanTu.Items.Count > 0) cboToanTu.SelectedIndex = 0;

                // Quản lý hiển thị Inputs
                bool isDateTime = selectedField.DataType == TypeCode.DateTime;

                // Nếu là DateTime, hiển thị txtTu, ẩn txtGiaTri
                txtGiaTri.Visible = !isDateTime;
                label2.Text = !isDateTime ? "Giá trị:" : "";

                label3.Visible = isDateTime;
                txtTu.Visible = isDateTime;

                // Cập nhật trạng thái Enabled cho các trường INPUT
                txtGiaTri.Enabled = _currentState != FilterState.READ && txtGiaTri.Visible; // <-- Đã cập nhật
                txtTu.Enabled = _currentState != FilterState.READ && txtTu.Visible; // <-- Đã cập nhật

                // Gọi hàm quản lý toán tử để xử lý trường 'Đến'
                CboToanTu_SelectedIndexChanged(null, null); // <-- Gọi hàm đã sửa tên
            }
        }

        private void CboToanTu_SelectedIndexChanged(object sender, EventArgs e) // <-- Đã sửa tên phương thức
        {
            FieldMetadata selectedField = cboTimTheo.SelectedItem as FieldMetadata;
            string selectedOperator = cboToanTu.SelectedItem?.ToString();

            if (selectedField != null && selectedField.DataType == TypeCode.DateTime)
            {
                bool isRange = selectedOperator == "Khoảng" || selectedOperator == "Đoạn";

                label4.Visible = isRange;
                txtDen.Visible = isRange;
                txtDen.Enabled = _currentState != FilterState.READ && txtDen.Visible; // <-- Đã cập nhật

                // Cập nhật nhãn "Từ"
                label3.Text = isRange ? "Từ:" : "Ngày:";
            }
            else
            {
                label4.Visible = false;
                txtDen.Visible = false;
                txtDen.Enabled = false;
            }
        }

        #endregion

        // ---

        #region XỬ LÝ NÚT CRUD STATE (LƯU - HỦY)

        private void BtnLuu_Click(object sender, EventArgs e) // <-- Đã sửa tên phương thức
        {
            FieldMetadata selectedField = cboTimTheo.SelectedItem as FieldMetadata;
            string selectedOperator = cboToanTu.SelectedItem?.ToString();

            // 1. Validation và Thu thập giá trị
            if (selectedField == null || string.IsNullOrEmpty(selectedOperator))
            {
                MessageBox.Show("Vui lòng chọn trường và toán tử.", "Lỗi Cấu Hình", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TryCollectAndValidateValue(selectedField, selectedOperator, out string value, out string valueTo))
            {
                return;
            }

            // 2. Tạo hoặc Cập nhật Filter Object
            SearchFilter filterToSave;

            if (_currentState == FilterState.CREATE) // <-- Đã cập nhật
            {
                filterToSave = new SearchFilter();
                _currentFilters.Add(filterToSave);
            }
            else // UPDATE
            {
                filterToSave = _selectedFilter;
            }

            // 3. Gán giá trị mới
            filterToSave.FieldName = selectedField.FieldName;
            filterToSave.DisplayName = selectedField.DisplayName;
            filterToSave.DataType = selectedField.DataType;
            filterToSave.Operator = selectedOperator;
            filterToSave.Value = value;
            filterToSave.ValueTo = valueTo;

            // 4. Hoàn tất
            UpdateBoLocListView();

            // Tự động chọn filter vừa lưu/sửa
            var item = lsvBoLoc.Items.Cast<ListViewItem>().FirstOrDefault(i => i.Tag == filterToSave);
            if (item != null)
            {
                item.Selected = true;
            }

            SetState(FilterState.READ); // <-- Đã cập nhật
        }

        private void BtnHuy_Click(object sender, EventArgs e) // <-- Đã sửa tên phương thức
        {
            SetState(FilterState.READ); // <-- Đã cập nhật
            // Nếu đang sửa, cần đảm bảo không có filter nào được chọn
            lsvBoLoc.SelectedItems.Clear();
        }

        #endregion

        // ---

        #region XỬ LÝ NÚT HÀNH ĐỘNG (THÊM, SỬA, XÓA, TÌM)

        private void BtnThemBoLoc_Click(object sender, EventArgs e) // <-- Đã sửa tên phương thức
        {
            ClearFilterInputs();
            _selectedFilter = null;
            SetState(FilterState.CREATE); // <-- Đã cập nhật
        }

        private void BtnSuaBoLoc_Click(object sender, EventArgs e) // <-- Đã sửa tên phương thức
        {
            if (lsvBoLoc.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một Bộ lọc để sửa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedFilter = lsvBoLoc.SelectedItems[0].Tag as SearchFilter;
            LoadFilterToInputs(_selectedFilter);

            SetState(FilterState.UPDATE); // <-- Đã cập nhật
        }

        private void BtnXoaBoLoc_Click(object sender, EventArgs e) // <-- Đã sửa tên phương thức
        {
            if (lsvBoLoc.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một Bộ lọc để xóa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa bộ lọc này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SearchFilter filterToDelete = lsvBoLoc.SelectedItems[0].Tag as SearchFilter;
                _currentFilters.Remove(filterToDelete);
                UpdateBoLocListView();

                SetState(FilterState.READ); // <-- Đã cập nhật

                // ***QUAN TRỌNG: Gọi Event Find ngay lập tức sau khi xóa***
                BtnTim_Click(sender, e); // <-- Gọi hàm đã sửa tên
            }
        }

        private void BtnTim_Click(object sender, EventArgs e) // <-- Đã sửa tên phương thức
        {
            if (_currentState != FilterState.READ) // <-- Đã cập nhật
            {
                MessageBox.Show("Vui lòng Lưu hoặc Hủy chỉnh sửa Bộ lọc trước khi Tìm kiếm.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Gọi Delegate (Tùy chọn, để Form cha xử lý ngay lập tức)
            OnSearchApplied?.Invoke(_currentFilters ?? new List<SearchFilter>());

            // 2. Đặt DialogResult và đóng Form
            this.DialogResult = DialogResult.OK;
            
        }

        #endregion

        // ---

        #region HÀM BỔ TRỢ VÀ VALIDATION

        private bool TryCollectAndValidateValue(FieldMetadata selectedField, string selectedOperator, out string value, out string valueTo)
        {
            value = "";
            valueTo = "";

            // 1. Xử lý trường Ngày tháng
            if (selectedField.DataType == TypeCode.DateTime)
            {
                value = txtTu.Text.Trim(); // Lấy giá trị 'Từ'
                bool isRange = selectedOperator == "Khoảng" || selectedOperator == "Đoạn";

                // Bắt buộc nhập Ngày nếu không phải tìm Khoảng/Đoạn và không có giá trị
                if (string.IsNullOrEmpty(value) && !isRange)
                {
                    MessageBox.Show("Vui lòng nhập Ngày tháng cho tìm kiếm.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Kiểm tra định dạng ngày tháng (Nếu có giá trị)
                if (!string.IsNullOrEmpty(value) && !DateTime.TryParse(value, out DateTime dtStart))
                {
                    MessageBox.Show("Ngày bắt đầu không hợp lệ (DD/MM/YYYY).", "Lỗi Định Dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (isRange)
                {
                    valueTo = txtDen.Text.Trim(); // Lấy giá trị 'Đến'

                    if (string.IsNullOrEmpty(valueTo))
                    {
                        MessageBox.Show("Vui lòng nhập Ngày kết thúc.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (!DateTime.TryParse(valueTo, out DateTime dtEnd))
                    {
                        MessageBox.Show("Ngày kết thúc không hợp lệ (DD/MM/YYYY).", "Lỗi Định Dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    // Kiểm tra logic ngày bắt đầu < ngày kết thúc
                    if (DateTime.TryParse(value, out dtStart) && dtStart > dtEnd)
                    {
                        MessageBox.Show("Ngày bắt đầu không được lớn hơn Ngày kết thúc.", "Lỗi Logic", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            // 2. Xử lý trường chuỗi/số
            else
            {
                value = txtGiaTri.Text.Trim();
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Vui lòng nhập Giá trị tìm kiếm.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        private void ClearFilterInputs()
        {
            txtGiaTri.Text = string.Empty;
            txtTu.Text = string.Empty;
            txtDen.Text = string.Empty;

            // Đặt lại ComboBox về item đầu tiên nếu có
            if (cboTimTheo.Items.Count > 0)
            {
                cboTimTheo.SelectedIndex = 0;
            }

            // Xóa lựa chọn trong ListView
            lsvBoLoc.SelectedItems.Clear();

            // Đảm bảo nút Sửa/Xóa bị tắt khi không có gì được chọn
            btnSuaBoLoc.Enabled = false;
            btnXoaBoLoc.Enabled = false;
        }

        private void LoadFilterToInputs(SearchFilter filter)
        {
            // Set SelectedValue sẽ kích hoạt sự kiện SelectedIndexChanged
            cboTimTheo.SelectedValue = filter.FieldName;
            // Cần đặt lại SelectedItem sau khi cboTimTheo đã load lại cboToanTu
            cboToanTu.SelectedItem = filter.Operator;

            if (filter.DataType == TypeCode.DateTime)
            {
                // Vì SelectedIndexChanged đã chạy, txtTu/txtDen đã Visible/Hidden đúng
                txtTu.Text = filter.Value;
                txtDen.Text = filter.ValueTo;
                txtGiaTri.Text = string.Empty;
            }
            else
            {
                txtGiaTri.Text = filter.Value;
                txtTu.Text = string.Empty;
                txtDen.Text = string.Empty;
            }
        }

        private void UpdateBoLocListView()
        {
            lsvBoLoc.Items.Clear();
            foreach (var filter in _currentFilters)
            {
                // Sử dụng hàm ToString() đã override trong SearchFilter để hiển thị
                ListViewItem item = new ListViewItem(filter.ToString());
                item.Tag = filter;
                lsvBoLoc.Items.Add(item);
            }
        }
        #endregion
    }
}