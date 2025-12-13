using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagerApp.GUI.Forms
{
    public partial class frmChonCotXuatExcel : Form
    {
        // Dictionary chứa TẤT CẢ các cột (Nhận từ form cha)
        private Dictionary<string, string> _allColumns;

        // Dictionary KẾT QUẢ (Trả về cho form cha)
        public Dictionary<string, string> SelectedColumns { get; private set; }

        public frmChonCotXuatExcel(Dictionary<string, string> allColumns)
        {
            InitializeComponent();
            _allColumns = allColumns;

            ConfigureListView();

            // Liên kết sự kiện
            this.Load += frmChonCotXuatExcel_Load;
            btnThemCot.Click += btnThemCot_Click;
            btnXoaCot.Click += btnXoaCot_Click;
            btnXacNhan.Click += btnXacNhan_Click;

            // Sự kiện để bật/tắt nút
            lsvBoLocCot.SelectedIndexChanged += (s, e) => UpdateButtonState();
            cboChonCot.SelectedIndexChanged += (s, e) => UpdateButtonState();
        }

        // Hàm cấu hình riêng
        private void ConfigureListView()
        {
            lsvBoLocCot.View = View.Details;
            lsvBoLocCot.FullRowSelect = true;
            lsvBoLocCot.GridLines = true;     // Có dòng kẻ
            lsvBoLocCot.HeaderStyle = ColumnHeaderStyle.Nonclickable;

            lsvBoLocCot.Font = new Font("Consolas", 10f, FontStyle.Regular);

            lsvBoLocCot.Columns.Clear();
            // Tự động giãn cột theo chiều rộng form (-5 để trừ viền)
            lsvBoLocCot.Columns.Add("Tên cột", lsvBoLocCot.Width - 5);
        }

        private void frmChonCotXuatExcel_Load(object sender, EventArgs e)
        {
            // Mặc định: Thêm TẤT CẢ các cột vào ListView
            foreach (var kvp in _allColumns)
            {
                AddColumnToListView(kvp.Key, kvp.Value);
            }

            // ComboBox sẽ rỗng ban đầu
            UpdateButtonState();
        }

        #region LOGIC THÊM / XÓA

        // Thêm cột vào ListView (Từ ComboBox)
        private void btnThemCot_Click(object sender, EventArgs e)
        {
            ColumnInfo selectedCol = cboChonCot.SelectedItem as ColumnInfo;
            if (selectedCol == null) return;

            // 1. Thêm vào ListView
            AddColumnToListView(selectedCol.PropertyName, selectedCol.DisplayName);

            // 2. Xóa khỏi ComboBox
            cboChonCot.Items.Remove(selectedCol);

            UpdateButtonState();
        }

        // Xóa cột khỏi ListView (Đưa về ComboBox)
        private void btnXoaCot_Click(object sender, EventArgs e)
        {
            if (lsvBoLocCot.SelectedItems.Count == 0) return;

            // Lấy item đang chọn
            ListViewItem item = lsvBoLocCot.SelectedItems[0];
            ColumnInfo colInfo = item.Tag as ColumnInfo;

            // 1. Thêm lại vào ComboBox
            cboChonCot.Items.Add(colInfo);

            // 2. Xóa khỏi ListView
            lsvBoLocCot.Items.Remove(item);

            UpdateButtonState();
        }

        #endregion

        #region HÀM BỔ TRỢ

        private void AddColumnToListView(string key, string value)
        {
            ListViewItem item = new ListViewItem(value); // Hiển thị DisplayName
            item.Tag = new ColumnInfo { PropertyName = key, DisplayName = value }; // Lưu object gốc
            lsvBoLocCot.Items.Add(item);
        }

        private void UpdateButtonState()
        {
            // Nút Thêm: Chỉ bật khi ComboBox có item và đã chọn 1 item
            btnThemCot.Enabled = (cboChonCot.Items.Count > 0 && cboChonCot.SelectedIndex != -1);

            // Nút Xóa: Chỉ bật khi ListView có item được chọn
            btnXoaCot.Enabled = (lsvBoLocCot.SelectedItems.Count > 0);

            // Nút Xác nhận: Chỉ bật khi ListView có ít nhất 1 cột
            btnXacNhan.Enabled = (lsvBoLocCot.Items.Count > 0);
        }

        #endregion

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            // Tạo Dictionary kết quả từ ListView (theo thứ tự người dùng đã sắp xếp/giữ lại)
            SelectedColumns = new Dictionary<string, string>();

            foreach (ListViewItem item in lsvBoLocCot.Items)
            {
                ColumnInfo col = item.Tag as ColumnInfo;
                SelectedColumns.Add(col.PropertyName, col.DisplayName);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }

    // Class phụ để lưu trữ thông tin cột (cho ComboBox và ListView Tag)
    public class ColumnInfo
    {
        public string PropertyName { get; set; } // Key (VD: MaBD)
        public string DisplayName { get; set; }  // Value (VD: Mã Bạn Đọc)

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
