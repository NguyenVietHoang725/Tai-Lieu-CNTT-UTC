using LibraryManagerApp.DTO;
using System.Collections.Generic;

namespace LibraryManagerApp.Helpers
{
    // 1. Enum định nghĩa các BỘ LỌC CẦN HIỂN THỊ
    public enum FilterType
    {
        KhongCanBoLoc, // Không cần lọc (VD: Biểu đồ Tròn)
        ChonNhieuNam    // Cần 1 hoặc nhiều năm (VD: Biểu đồ Đường)
    }

    // 2. Class Metadata (Từ điển)
    public class ThongKeMetadata
    {
        public string TenHienThi { get; set; } // Hiển thị trên ComboBox
        public string MaBaoCao { get; set; }   // Key để gọi BLL
        public ChartTypeEnum LoaiBieuDo { get; set; }
        public FilterType YeuCauBoLoc { get; set; }
    }

    // 3. Kho chứa "Từ điển" (Danh sách các báo cáo)
    public static class ThongKeRepository
    {
        public static List<ThongKeMetadata> GetDanhSachBaoCao()
        {
            return new List<ThongKeMetadata>
            {
                // Báo cáo 1 (Đã có)
                new ThongKeMetadata
                {
                    TenHienThi = "Thẻ bạn đọc mới (theo tháng)",
                    MaBaoCao = "GetTheBanDocTheoThang",
                    LoaiBieuDo = ChartTypeEnum.Line, // Sẽ được GUI vẽ thành Spline
                    YeuCauBoLoc = FilterType.ChonNhieuNam
                },
                
                // >>> BÁO CÁO MỚI 1 (Giao dịch)
                new ThongKeMetadata
                {
                    TenHienThi = "Giao dịch mượn mới (theo tháng)",
                    MaBaoCao = "GetGiaoDichTheoThang",
                    LoaiBieuDo = ChartTypeEnum.Line,
                    YeuCauBoLoc = FilterType.ChonNhieuNam
                },
                
                // Báo cáo 2 (Đã có)
                new ThongKeMetadata
                {
                    TenHienThi = "Cơ cấu Tài khoản (theo Vai trò)",
                    MaBaoCao = "GetTaiKhoanTheoVaiTro",
                    LoaiBieuDo = ChartTypeEnum.Pie,
                    YeuCauBoLoc = FilterType.KhongCanBoLoc
                },

                // >>> BÁO CÁO MỚI 2 (Bản sao)
                new ThongKeMetadata
                {
                    TenHienThi = "Tình trạng Kho Bản sao",
                    MaBaoCao = "GetBanSaoTheoTrangThai",
                    LoaiBieuDo = ChartTypeEnum.Pie,
                    YeuCauBoLoc = FilterType.KhongCanBoLoc
                },

                // >>> BÁO CÁO MỚI 3 (Thể loại)
                new ThongKeMetadata
                {
                    TenHienThi = "Cơ cấu Tài liệu (theo Thể loại)",
                    MaBaoCao = "GetTaiLieuTheoTheLoai",
                    LoaiBieuDo = ChartTypeEnum.Pie,
                    YeuCauBoLoc = FilterType.KhongCanBoLoc
                }
            };
        }
    }
}