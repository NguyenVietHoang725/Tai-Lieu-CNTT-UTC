using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryManagerApp.DTO;
using LibraryManagerApp.DAL;
using static LibraryManagerApp.DAL.TrangChuDAL;

namespace LibraryManagerApp.GUI.UserControls.TrangChu
{
    public partial class ucFrmTrangChu : UserControl
    {
        #region Fields & Constants

        private Timer timerDateTime;
        private LoginSessionDTO _currentUser;
        private readonly TrangChuDAL _dal;

        // Constants cho dễ cấu hình
        private const int TOP_BOOKS_COUNT = 10;           // Số lượng sách top
        private const int RECENT_ACTIVITIES_COUNT = 10;    // Số lượng hoạt động gần đây
        private const int TIMER_INTERVAL = 1000;           // 1 giây

        // Color constants
        private static readonly Color PRIMARY_COLOR = Color.FromArgb(48, 52, 129);
        private static readonly Color SECONDARY_COLOR = Color.FromArgb(214, 230, 242);
        private static readonly Color WARNING_COLOR = Color.FromArgb(220, 53, 69);
        private static readonly Color WHITE_COLOR = Color.White;
        private static readonly Color ALTERNATE_ROW_COLOR = Color.FromArgb(245, 245, 245);

        #endregion

        #region Constructor & Initialization

        public ucFrmTrangChu()
        {
            InitializeComponent();
            _dal = new TrangChuDAL();
            InitializeTimer();
            InitializeDataGridViews();
            this.Load += UcFrmTrangChu_Load;
        }

        private void UcFrmTrangChu_Load(object sender, EventArgs e)
        {
            LoadUserInfo();
            LoadAllData();
        }

        /// <summary>
        /// Load tất cả dữ liệu một lần
        /// </summary>
        private void LoadAllData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                LoadStatistics();
                LoadRecentActivity();
                LoadTopBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Timer - Cập nhật ngày giờ

        private void InitializeTimer()
        {
            timerDateTime = new Timer
            {
                Interval = TIMER_INTERVAL
            };
            timerDateTime.Tick += TimerDateTime_Tick;
            timerDateTime.Start();

            UpdateDateTime(); // Update ngay lập tức
        }

        private void TimerDateTime_Tick(object sender, EventArgs e)
        {
            UpdateDateTime();
        }

        private void UpdateDateTime()
        {
            DateTime now = DateTime.Now;
            string dayOfWeek = GetVietnameseDayOfWeek(now.DayOfWeek);
            lblDateTime.Text = $"🕐 {dayOfWeek}\n{now:dd/MM/yyyy}\n{now:HH:mm:ss}";
        }

        private string GetVietnameseDayOfWeek(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday:
                    return "Thứ Hai";
                case DayOfWeek.Tuesday:
                    return "Thứ Ba";
                case DayOfWeek.Wednesday:
                    return "Thứ Tư";
                case DayOfWeek.Thursday:
                    return "Thứ Năm";
                case DayOfWeek.Friday:
                    return "Thứ Sáu";
                case DayOfWeek.Saturday:
                    return "Thứ Bảy";
                case DayOfWeek.Sunday:
                    return "Chủ Nhật";
                default:
                    return "";
            }
        }

        #endregion

        #region Thông tin người dùng

        private void LoadUserInfo()
        {
            if (_currentUser != null)
            {
                lblWelcome.Text = $"Xin chào, {_currentUser.HoTenNV}";
                lblRole.Text = $"Vai trò: {GetRoleName(_currentUser.MaVT)}";
            }
            else
            {
                lblWelcome.Text = "Xin chào!";
                lblRole.Text = "Vai trò: Chưa đăng nhập";
            }
        }

        private string GetRoleName(string maVT)
        {
            if (string.IsNullOrEmpty(maVT))
                return "Nhân viên";

            switch (maVT.ToUpper())
            {
                case "QTV":
                    return "Quản trị viên";
                case "QLB":
                    return "Quản lý bạn đọc";
                case "QLT":
                    return "Quản lý tài liệu";
                case "QLM":
                    return "Quản lý mượn trả";
                default:
                    return "Nhân viên";
            }
        }

        #endregion

        #region Khởi tạo DataGridView

        private void InitializeDataGridViews()
        {
            ConfigureDataGridView(dgvActivity);
            ConfigureDataGridView(dgvTopBooks);
        }

        private void ConfigureDataGridView(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.RowTemplate.Height = 35;

            // Header style
            dgv.ColumnHeadersDefaultCellStyle.BackColor = PRIMARY_COLOR;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = WHITE_COLOR;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Consolas", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Cell style
            dgv.DefaultCellStyle.Font = new Font("Consolas", 9F);
            dgv.DefaultCellStyle.SelectionBackColor = SECONDARY_COLOR;
            dgv.DefaultCellStyle.SelectionForeColor = PRIMARY_COLOR;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = ALTERNATE_ROW_COLOR;
        }

        #endregion

        #region Load dữ liệu Thống kê

        private void LoadStatistics()
        {
            try
            {
                var stats = _dal.GetStatistics();

                lblCountTaiLieu.Text = stats["TongTaiLieu"].ToString("N0");
                lblCountBanDoc.Text = stats["TongBanDoc"].ToString("N0");
                lblCountDangMuon.Text = stats["DangMuon"].ToString("N0");
                lblCountQuaHan.Text = stats["QuaHan"].ToString("N0");
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tải thống kê", ex);
                ResetStatistics();
            }
        }

        private void ResetStatistics()
        {
            lblCountTaiLieu.Text = "0";
            lblCountBanDoc.Text = "0";
            lblCountDangMuon.Text = "0";
            lblCountQuaHan.Text = "0";
        }

        #endregion

        #region Load Hoạt động gần đây

        private void LoadRecentActivity()
        {
            try
            {
                var activities = _dal.GetRecentActivities(RECENT_ACTIVITIES_COUNT);

                // Tạo DataTable
                DataTable dt = CreateActivityDataTable(activities);

                // Setup columns
                SetupActivityColumns();

                // Bind data
                dgvActivity.DataSource = dt;

                // Update title
                UpdateActivityTitle(activities.Count);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tải hoạt động", ex);
                lblActivityTitle.Text = "📋 Hoạt động gần đây (Lỗi)";
            }
        }

        private DataTable CreateActivityDataTable(List<ActivityDTO> activities)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaGD", typeof(string));
            dt.Columns.Add("BanDoc", typeof(string));
            dt.Columns.Add("TaiLieu", typeof(string));
            dt.Columns.Add("LoaiGD", typeof(string));
            dt.Columns.Add("NgayGD", typeof(DateTime));

            foreach (var activity in activities)
            {
                dt.Rows.Add(
                    activity.MaGD,
                    activity.HoTenBD,
                    activity.TenTL,
                    activity.LoaiGD,
                    activity.NgayGD
                );
            }

            return dt;
        }

        private void SetupActivityColumns()
        {
            dgvActivity.Columns.Clear();

            dgvActivity.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaGD",
                HeaderText = "Mã GD",
                Width = 80
            });

            dgvActivity.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BanDoc",
                HeaderText = "Bạn đọc",
                Width = 120
            });

            dgvActivity.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TaiLieu",
                HeaderText = "Tài liệu",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvActivity.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LoaiGD",
                HeaderText = "Loại",
                Width = 60,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dgvActivity.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NgayGD",
                HeaderText = "Thời gian",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd/MM/yyyy HH:mm"
                }
            });
        }

        private void UpdateActivityTitle(int count)
        {
            lblActivityTitle.Text = count == 0
                ? "📋 Hoạt động gần đây (Không có dữ liệu)"
                : $"📋 Hoạt động gần đây ({count} giao dịch)";
        }

        #endregion

        #region Load Top sách được mượn

        private void LoadTopBooks()
        {
            try
            {
                var topBooks = _dal.GetTopBorrowedBooks(TOP_BOOKS_COUNT);

                // Tạo DataTable
                DataTable dt = CreateTopBooksDataTable(topBooks);

                // Setup columns
                SetupTopBooksColumns();

                // Bind data
                dgvTopBooks.DataSource = dt;

                // Update title
                UpdateTopBooksTitle(topBooks.Count);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tải top sách", ex);
                lblTopBooksTitle.Text = "⭐ Top sách được mượn (Lỗi)";
            }
        }

        private DataTable CreateTopBooksDataTable(List<TopBookDTO> topBooks)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("TenTL", typeof(string));
            dt.Columns.Add("SoLuot", typeof(int));

            foreach (var book in topBooks)
            {
                dt.Rows.Add(book.STT, book.TenTL, book.SoLuotMuon);
            }

            return dt;
        }

        private void SetupTopBooksColumns()
        {
            dgvTopBooks.Columns.Clear();

            dgvTopBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "STT",
                HeaderText = "Top",
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Consolas", 10F, FontStyle.Bold),
                    ForeColor = PRIMARY_COLOR
                }
            });

            dgvTopBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenTL",
                HeaderText = "Tên sách",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvTopBooks.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoLuot",
                HeaderText = "Lượt",
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Consolas", 10F, FontStyle.Bold),
                    ForeColor = WARNING_COLOR
                }
            });
        }

        private void UpdateTopBooksTitle(int count)
        {
            lblTopBooksTitle.Text = count == 0
                ? "⭐ Top sách được mượn (Không có dữ liệu)"
                : $"⭐ Top {count} sách được mượn nhiều nhất";
        }

        #endregion

        #region Helper Methods

        private void ShowError(string title, Exception ex)
        {
            MessageBox.Show($"{title}: {ex.Message}",
                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set thông tin người dùng từ frmMain
        /// </summary>
        public void SetUserInfo(LoginSessionDTO userSession)
        {
            _currentUser = userSession;
            LoadUserInfo();
        }

        /// <summary>
        /// Refresh toàn bộ dữ liệu
        /// </summary>
        public void RefreshData()
        {
            LoadAllData();
        }

        /// <summary>
        /// Refresh riêng phần thống kê
        /// </summary>
        public void RefreshStatistics()
        {
            LoadStatistics();
        }

        /// <summary>
        /// Refresh riêng phần hoạt động
        /// </summary>
        public void RefreshActivities()
        {
            LoadRecentActivity();
        }

        /// <summary>
        /// Refresh riêng phần top sách
        /// </summary>
        public void RefreshTopBooks()
        {
            LoadTopBooks();
        }

        #endregion
    }
}