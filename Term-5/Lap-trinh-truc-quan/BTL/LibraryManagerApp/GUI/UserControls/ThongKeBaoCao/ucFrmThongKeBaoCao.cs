// File: LibraryManagerApp.GUI.UserControls.ThongKeBaoCao/ucFrmThongKeBaoCao.cs

using LibraryManagerApp.BLL;
using LibraryManagerApp.DTO;
using LibraryManagerApp.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; // << Rất quan trọng

namespace LibraryManagerApp.GUI.UserControls.ThongKeBaoCao
{
    public partial class ucFrmThongKeBaoCao : UserControl
    {
        private ThongKeBLL _bll = new ThongKeBLL();
        private ThongKeMetadata _currentBaoCao; // Lưu báo cáo đang chọn

        #region KHỞI TẠO VÀ CẤU HÌNH
        public ucFrmThongKeBaoCao()
        {
            InitializeComponent();

            // Liên kết sự kiện (Quan trọng)
            this.Load += ucFrmThongKeBaoCao_Load;
            cboLoaiThongKe.SelectedIndexChanged += cboLoaiThongKe_SelectedIndexChanged;
            btnThem.Click += btnThem_Click;
            btnXoa.Click += btnXoa_Click;
            btnXemBieuDo.Click += btnXemBieuDo_Click;
        }

        private void ucFrmThongKeBaoCao_Load(object sender, EventArgs e)
        {
            // 1. Cấu hình Chart
            ConfigureChart();

            // 2. Tải "Từ điển" vào ComboBox
            LoadComboBoxThongKe(); // Hàm này sẽ kích hoạt SelectedIndexChanged

            // 3. >>> ĐIỀU CHỈNH: Tải mặc định năm hiện tại
            string namHienTai = nudChonNam.Value.ToString();

            if (!lsvNam.Items.Cast<ListViewItem>().Any(item => item.Text == namHienTai))
            {
                lsvNam.Items.Add(new ListViewItem(namHienTai));
            }

            // 4. Tải biểu đồ mặc định (Báo cáo đầu tiên, năm hiện tại)
            btnXemBieuDo_Click(sender, e);
        }

        private void LoadComboBoxThongKe()
        {
            cboLoaiThongKe.DataSource = ThongKeRepository.GetDanhSachBaoCao();
            cboLoaiThongKe.DisplayMember = "TenHienThi";
            cboLoaiThongKe.ValueMember = "MaBaoCao";

            cboLoaiThongKe.SelectedIndex = 0;
        }

        private void ConfigureChart()
        {
            chartThongKe.Series.Clear();
            chartThongKe.Legends.Clear();

            nudChonNam.Maximum = DateTime.Now.Year;
            nudChonNam.Value = DateTime.Now.Year;
        }
        #endregion

        #region QUẢN LÝ BỘ LỌC (EVENTS)

        // Sự kiện này kích hoạt khi người dùng chọn báo cáo khác
        private void cboLoaiThongKe_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentBaoCao = cboLoaiThongKe.SelectedItem as ThongKeMetadata;
            if (_currentBaoCao == null) return;

            // Kiểm tra xem báo cáo này có cần bộ lọc Năm hay không
            if (_currentBaoCao.YeuCauBoLoc == FilterType.ChonNhieuNam)
            {
                // Hiển thị panel bộ lọc
                tableLayoutPanel6.Visible = true;
            }
            else // (VD: FilterType.KhongCanBoLoc cho biểu đồ Tròn)
            {
                // Ẩn panel bộ lọc
                tableLayoutPanel6.Visible = false;
            }

            // >>> ĐIỀU CHỈNH: Không tự động tải lại biểu đồ khi chỉ đổi ComboBox
            // Người dùng phải nhấn "Xem Biểu đồ"
        }

        // Thêm Năm vào ListView
        private void btnThem_Click(object sender, EventArgs e)
        {
            string nam = nudChonNam.Value.ToString();

            if (lsvNam.Items.Cast<ListViewItem>().Any(item => item.Text == nam))
            {
                MessageBox.Show("Năm này đã được thêm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            lsvNam.Items.Add(new ListViewItem(nam));
        }

        // Xóa Năm khỏi ListView
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lsvNam.SelectedItems.Count > 0)
            {
                lsvNam.Items.Remove(lsvNam.SelectedItems[0]);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một năm để xóa.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region XỬ LÝ BIỂU ĐỒ (LOAD & BIND)

        // Nút "Xem Biểu đồ"
        private void btnXemBieuDo_Click(object sender, EventArgs e)
        {
            if (_currentBaoCao == null) return;

            try
            {
                List<int> listNam = new List<int>();

                // 1. Thu thập Bộ lọc (Filters)
                if (_currentBaoCao.YeuCauBoLoc == FilterType.ChonNhieuNam)
                {
                    listNam = lsvNam.Items.Cast<ListViewItem>().Select(item => int.Parse(item.Text)).ToList();
                    if (listNam.Count == 0)
                    {
                        // Nếu là báo cáo cần năm nhưng list rỗng, reset biểu đồ
                        chartThongKe.Series.Clear();
                        chartThongKe.Titles.Clear();
                        MessageBox.Show("Vui lòng thêm ít nhất một năm vào danh sách để xem.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // 2. Gọi BLL
                ChartDataDTO data = _bll.GetChartData(_currentBaoCao, listNam);

                // 3. Vẽ Biểu đồ
                BindDataToChart(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu thống kê: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm nội bộ để vẽ biểu đồ
        private void BindDataToChart(ChartDataDTO data)
        {
            chartThongKe.Series.Clear();
            chartThongKe.Legends.Clear();
            chartThongKe.Titles.Clear(); // Xóa tiêu đề cũ

            // Đặt tiêu đề (nếu có)
            chartThongKe.Titles.Add(new Title(data.TieuDe, Docking.Top, new Font("Consolas", 14F, FontStyle.Bold), Color.Black));

            // Set loại biểu đồ
            SeriesChartType chartType;
            switch (data.LoaiBieuDo)
            {
                case ChartTypeEnum.Pie:
                    chartType = SeriesChartType.Pie;
                    break;
                case ChartTypeEnum.Bar:
                    chartType = SeriesChartType.Column;
                    break;
                case ChartTypeEnum.Line:
                default:
                    // >>> ĐIỀU CHỈNH: Đổi từ Line sang Spline
                    chartType = SeriesChartType.Spline;
                    break;
            }


            // Lặp qua từng Series (VD: "Năm 2024", "Năm 2025")
            foreach (var seriesData in data.DuLieu)
            {
                string seriesName = seriesData.Key;
                Series series = chartThongKe.Series.Add(seriesName);
                series.ChartType = chartType;

                // Tùy chỉnh cho biểu đồ Line/Spline
                if (chartType == SeriesChartType.Spline)
                {
                    series.BorderWidth = 3;
                    series.MarkerStyle = MarkerStyle.Circle;
                    series.MarkerSize = 8;
                }

                // Tùy chỉnh cho biểu đồ Pie
                if (chartType == SeriesChartType.Pie)
                {
                    series.IsValueShownAsLabel = true;
                    series.LabelFormat = "#.##%"; // Hiển thị %
                }

                // Thêm Legend (chú thích)
                chartThongKe.Legends.Add(new Legend(seriesName) { Docking = Docking.Bottom });
                series.Legend = seriesName;

                // Lặp qua từng điểm dữ liệu (VD: "Tháng 1" -> 10)
                foreach (var dataPoint in seriesData.Value)
                {
                    string labelX = dataPoint.Key;
                    double valueY = dataPoint.Value;

                    DataPoint point = new DataPoint();
                    point.SetValueXY(labelX, valueY);

                    if (chartType == SeriesChartType.Pie)
                    {
                        point.LegendText = labelX; // Cho Pie: "QTV"
                        // Calculate percent manually since DataPoint does not have Percent property
                        double total = series.Points.Sum(p => p.YValues[0]) + valueY;
                        double percent = total > 0 ? valueY / total : 0;
                        point.Label = $"{valueY} ({percent:P0})"; // Hiển thị "10 (20%)"
                    }
                    else
                    {
                        point.Label = valueY.ToString(); // Cho Line: Hiển thị giá trị
                    }

                    series.Points.Add(point);
                }
            }
        }
        #endregion
    }
}