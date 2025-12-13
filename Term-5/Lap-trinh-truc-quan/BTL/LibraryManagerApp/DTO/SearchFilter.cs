using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.DTO
{
    public class SearchFilter
    {
        // Tên thuộc tính trong DTO (Ví dụ: "MaBD", "HoDem", "NgaySinh")
        public string FieldName { get; set; }

        // Tên hiển thị trên ComboBox (Ví dụ: "Mã Bạn Đọc")
        public string DisplayName { get; set; }

        // Toán tử được chọn (Ví dụ: "=", "LIKE", ">")
        public string Operator { get; set; }

        // Giá trị tìm kiếm (Luôn lưu dưới dạng string, sẽ chuyển đổi khi truy vấn)
        public string Value { get; set; }
        public string ValueTo { get; set; }

        // Loại dữ liệu của trường (Giúp biết cách áp dụng toán tử)
        public TypeCode DataType { get; set; }

        public override string ToString()
        {
            if (Operator == "Khoảng" || Operator == "Đoạn")
            {
                // Hiển thị tên toán tử chính xác
                return $"{DisplayName} {Operator} [{Value} đến {ValueTo}]";
            }
            return $"{DisplayName} {Operator} '{Value}'";
        }
    }
}
