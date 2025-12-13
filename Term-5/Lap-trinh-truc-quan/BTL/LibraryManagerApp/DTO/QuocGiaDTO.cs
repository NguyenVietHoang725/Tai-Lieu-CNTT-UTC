using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DTO
{
    public class QuocGiaDTO
    {
        public string MaQG { get; set; }
        public string TenQG { get; set; }
        public string HienThi { get { return $"{MaQG} - {TenQG}"; } } // Kết hợp để hiển thị trong ComboBox
    }
}
