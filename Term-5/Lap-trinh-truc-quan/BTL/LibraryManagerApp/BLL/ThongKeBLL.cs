// File: LibraryManagerApp.BLL/ThongKeBLL.cs

using LibraryManagerApp.DAL;
using LibraryManagerApp.DTO;
using LibraryManagerApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagerApp.BLL
{
    internal class ThongKeBLL
    {
        private ThongKeDAL _dal = new ThongKeDAL();

        // --- Hàm điều phối (Dispatcher) ---
        // Hàm này được gọi từ GUI (ucFrmThongKeBaoCao)
        public ChartDataDTO GetChartData(ThongKeMetadata metadata, List<int> listNam)
        {
            switch (metadata.MaBaoCao)
            {
                case "GetTheBanDocTheoThang":
                    return GetTheBanDocTheoThang(listNam, metadata);

                case "GetTaiKhoanTheoVaiTro":
                    return GetTaiKhoanTheoVaiTro(metadata);

                case "GetGiaoDichTheoThang":
                    return GetGiaoDichTheoThang(listNam, metadata);

                case "GetBanSaoTheoTrangThai":
                    return GetBanSaoTheoTrangThai(metadata);

                case "GetTaiLieuTheoTheLoai":
                    return GetTaiLieuTheoTheLoai(metadata);

                default:
                    throw new Exception("Mã báo cáo không hợp lệ: " + metadata.MaBaoCao);
            }
        }

        // --- Logic chi tiết cho từng Báo cáo ---

        // 1. Logic Biểu đồ Đường (Line): Thẻ Bạn Đọc
        private ChartDataDTO GetTheBanDocTheoThang(List<int> listNam, ThongKeMetadata metadata)
        {
            ChartDataDTO chartData = new ChartDataDTO { TieuDe = metadata.TenHienThi, LoaiBieuDo = metadata.LoaiBieuDo };

            foreach (int nam in listNam)
            {
                List<MonthlyStatRaw> rawData = _dal.GetTheBanDocStats(nam);
                var dataPoints = new Dictionary<string, double>();
                for (int i = 1; i <= 12; i++)
                {
                    var entry = rawData.FirstOrDefault(q => q.Thang == i);
                    dataPoints.Add($"T{i}", entry?.SoLuong ?? 0);
                }
                chartData.DuLieu.Add($"Năm {nam}", dataPoints);
            }
            return chartData;
        }

        // 2. Logic Biểu đồ Đường (Line): Giao dịch
        private ChartDataDTO GetGiaoDichTheoThang(List<int> listNam, ThongKeMetadata metadata)
        {
            ChartDataDTO chartData = new ChartDataDTO { TieuDe = metadata.TenHienThi, LoaiBieuDo = metadata.LoaiBieuDo };

            foreach (int nam in listNam)
            {
                List<MonthlyStatRaw> rawData = _dal.GetGiaoDichStats(nam);
                var dataPoints = new Dictionary<string, double>();
                for (int i = 1; i <= 12; i++)
                {
                    var entry = rawData.FirstOrDefault(q => q.Thang == i);
                    dataPoints.Add($"T{i}", entry?.SoLuong ?? 0);
                }
                chartData.DuLieu.Add($"Năm {nam}", dataPoints);
            }
            return chartData;
        }

        // 3. Logic Biểu đồ Tròn (Pie): Vai Trò
        private ChartDataDTO GetTaiKhoanTheoVaiTro(ThongKeMetadata metadata)
        {
            ChartDataDTO chartData = new ChartDataDTO { TieuDe = metadata.TenHienThi, LoaiBieuDo = metadata.LoaiBieuDo };
            List<CategoryStatRaw> rawData = _dal.GetVaiTroStats();
            var dataPoints = new Dictionary<string, double>();
            foreach (var item in rawData) { dataPoints.Add(item.TenNhom, item.SoLuong); }
            chartData.DuLieu.Add("Vai trò", dataPoints);
            return chartData;
        }

        // 4. Logic Biểu đồ Tròn (Pie): Bản sao
        private ChartDataDTO GetBanSaoTheoTrangThai(ThongKeMetadata metadata)
        {
            ChartDataDTO chartData = new ChartDataDTO { TieuDe = metadata.TenHienThi, LoaiBieuDo = metadata.LoaiBieuDo };
            List<CategoryStatRaw> rawData = _dal.GetBanSaoTrangThaiStats();
            var dataPoints = new Dictionary<string, double>();
            foreach (var item in rawData) { dataPoints.Add(item.TenNhom, item.SoLuong); }
            chartData.DuLieu.Add("Trạng thái", dataPoints);
            return chartData;
        }

        // 5. Logic Biểu đồ Tròn (Pie): Thể loại
        private ChartDataDTO GetTaiLieuTheoTheLoai(ThongKeMetadata metadata)
        {
            ChartDataDTO chartData = new ChartDataDTO { TieuDe = metadata.TenHienThi, LoaiBieuDo = metadata.LoaiBieuDo };
            List<CategoryStatRaw> rawData = _dal.GetTaiLieuTheLoaiStats();
            var dataPoints = new Dictionary<string, double>();
            foreach (var item in rawData) { dataPoints.Add(item.TenNhom, item.SoLuong); }
            chartData.DuLieu.Add("Thể loại", dataPoints);
            return chartData;
        }
    }
}