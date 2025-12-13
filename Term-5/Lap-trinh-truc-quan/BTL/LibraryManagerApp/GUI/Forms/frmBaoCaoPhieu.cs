// File: LibraryManagerApp.GUI.Forms/frmBaoCaoPhieu.cs

using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace LibraryManagerApp.GUI.Forms
{
    public partial class frmBaoCaoPhieu : Form
    {
        private DataTable _dataSource;

        public frmBaoCaoPhieu(DataTable dataSource)
        {
            InitializeComponent();
            _dataSource = dataSource;
        }

        private void frmBaoCaoPhieu_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Reset
                reportViewer1.LocalReport.DataSources.Clear();

                // 2. Gán DataSource
                ReportDataSource rds = new ReportDataSource("DataSet1", _dataSource);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // 3. Đường dẫn Report
                string reportPath = Path.Combine(Application.StartupPath, "GUI", "Reports", "rptPhieuMuonTra.rdlc");
                if (!File.Exists(reportPath))
                {
                    reportPath = Path.Combine(Application.StartupPath, "rptPhieuMuonTra.rdlc");
                    // ... (Thêm logic tìm file như cũ nếu cần)
                }
                reportViewer1.LocalReport.ReportPath = reportPath;

                // 4. Đặt tên file mặc định khi lưu
                if (_dataSource.Rows.Count > 0)
                {
                    string maGD = _dataSource.Rows[0]["MaGD"].ToString();
                    reportViewer1.LocalReport.DisplayName = $"Phieu_{maGD}";
                }

                // 5. Refresh
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị báo cáo: " + ex.Message);
            }
        }
    }
}