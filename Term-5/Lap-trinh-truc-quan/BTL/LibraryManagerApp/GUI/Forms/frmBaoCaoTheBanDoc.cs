using LibraryManagerApp.DTO;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagerApp.GUI.Forms
{
    public partial class frmBaoCaoTheBanDoc : Form
    {
        private List<TheBanDocDTO> _dataList;

        // Constructor nhận danh sách thẻ cần in
        public frmBaoCaoTheBanDoc(List<TheBanDocDTO> dataList)
        {
            InitializeComponent();
            _dataList = dataList;
        }

        private void frmBaoCaoTheBanDoc_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Tạo DataTable với các cột TRÙNG KHỚP 100% với Dataset trong file .rdlc
                DataTable dt = new DataTable();
                dt.Columns.Add("MaTBD", typeof(string));
                dt.Columns.Add("HoTen", typeof(string));
                dt.Columns.Add("NgaySinh", typeof(string));
                dt.Columns.Add("GioiTinh", typeof(string));
                dt.Columns.Add("DiaChi", typeof(string));
                dt.Columns.Add("SDT", typeof(string));
                dt.Columns.Add("NgayCap", typeof(string));
                dt.Columns.Add("NgayHetHan", typeof(string));

                // 2. Đổ dữ liệu từ List DTO vào DataTable
                if (_dataList != null)
                {
                    foreach (var item in _dataList)
                    {
                        // Format từng trường ngày tháng
                        string strNgaySinh = item.NgaySinh.ToString("dd/MM/yyyy");
                        string strNgayCap = item.NgayCap.ToString("dd/MM/yyyy");

                        // Xử lý Null cho Ngày hết hạn (nếu có)
                        string strNgayHetHan = item.NgayHetHan.HasValue
                                               ? item.NgayHetHan.Value.ToString("dd/MM/yyyy")
                                               : ""; // Hoặc "Vĩnh viễn"

                        dt.Rows.Add(
                            item.MaTBD,
                            item.HoTenBD,
                            strNgaySinh,
                            item.GioiTinh,
                            item.DiaChi,
                            item.SDT,
                            strNgayCap,
                            strNgayHetHan
                        );
                    }
                }

                // 3. Reset ReportViewer
                this.reportViewer1.LocalReport.DataSources.Clear();

                // 4. Tạo ReportDataSource
                // "rptTheBanDoc" là tên DataSet bạn đã đặt trong file RDLC (dòng 72 file xml bạn gửi)
                ReportDataSource rds = new ReportDataSource("rptTheBanDoc", dt);
                this.reportViewer1.LocalReport.DataSources.Add(rds);

                // 5. Đường dẫn file báo cáo
                // Cách 1: Đường dẫn tương đối (khi debug)
                // string reportPath = "../../GUI/Reports/rptTheBanDoc.rdlc";

                // Cách 2: Copy to Output Directory (Khuyên dùng khi chạy thật)
                string reportPath = Path.Combine(Application.StartupPath, "GUI", "Reports", "rptTheBanDoc.rdlc");

                // Kiểm tra file tồn tại để tránh lỗi crash
                if (!File.Exists(reportPath))
                {
                    // Thử tìm ở thư mục gốc nếu cấu trúc thư mục khác
                    reportPath = Path.Combine(Application.StartupPath, "rptTheBanDoc.rdlc");
                    if (!File.Exists(reportPath))
                    {
                        // Thử tìm ngược lại thư mục project (chỉ dùng khi debug)
                        reportPath = @"..\..\GUI\Reports\rptTheBanDoc.rdlc";
                    }
                }

                this.reportViewer1.LocalReport.ReportPath = reportPath;

                if (_dataList != null && _dataList.Count > 0)
                {
                    // Lấy mã thẻ đầu tiên làm tên (Ví dụ: TheBanDoc_TBD231230821)
                    string tenFileMacDinh = $"TheBanDoc_{_dataList[0].MaTBD}";
                    this.reportViewer1.LocalReport.DisplayName = tenFileMacDinh;
                }
                else
                {
                    this.reportViewer1.LocalReport.DisplayName = "TheBanDoc_Report";
                }

                this.reportViewer1.LocalReport.ReportPath = reportPath;

                // 6. Refresh
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
