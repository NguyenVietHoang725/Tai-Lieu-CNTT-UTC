using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DTO
{
    // Enum định nghĩa các loại biểu đồ
    public enum ChartTypeEnum
    {
        Line, // Đường
        Pie,  // Tròn
        Bar   // Cột
    }

    public class ChartDataDTO
    {
        public string TieuDe { get; set; }

        public ChartTypeEnum LoaiBieuDo { get; set; }

        /// <summary>
        /// Dữ liệu để vẽ.
        /// Key (string): Tên Series (VD: "Năm 2024", "Năm 2025")
        /// Value (Dictionary): 
        ///     Key (string): Nhãn trục X (VD: "Tháng 1", "Vai trò QTV")
        ///     Value (double): Giá trị trục Y (VD: 10, 50)
        /// </summary>
        public Dictionary<string, Dictionary<string, double>> DuLieu { get; set; }

        public ChartDataDTO()
        {
            DuLieu = new Dictionary<string, Dictionary<string, double>>();
        }
    }
}
