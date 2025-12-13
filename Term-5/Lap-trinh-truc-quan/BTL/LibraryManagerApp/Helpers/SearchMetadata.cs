using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagerApp.Helpers
{
    // Lớp mô hình hóa Metadata của một trường tìm kiếm
    public class FieldMetadata
    {
        public string FieldName { get; set; }     // MaBD, HoDem, Ten, NgaySinh, v.v.
        public string DisplayName { get; set; }   // Mã Bạn Đọc, Họ Đệm, Tên, v.v.
        public TypeCode DataType { get; set; }    // String, DateTime, etc.
        public List<string> SupportedOperators { get; set; } // =, LIKE, >, <, v.v.
    }

    public static class SearchMetadata
    {
        // Khởi tạo các cấu hình tìm kiếm cho bảng Bạn Đọc
        public static List<FieldMetadata> GetBanDocFields()
        {
            return new List<FieldMetadata>
            {
                new FieldMetadata
                {
                    FieldName = "MaBD",
                    DisplayName = "Mã Bạn Đọc",
                    DataType = TypeCode.String,
                    SupportedOperators = new List<string> { "=", "LIKE", "Bắt đầu bằng" }
                },
                new FieldMetadata
                {
                    FieldName = "HoTen", // Kết hợp HoDem và Ten cho tìm kiếm tiện lợi
                    DisplayName = "Họ Tên",
                    DataType = TypeCode.String,
                    SupportedOperators = new List<string> { "LIKE", "Bắt đầu bằng" }
                },
                new FieldMetadata
                {
                    FieldName = "Email",
                    DisplayName = "Email",
                    DataType = TypeCode.String,
                    SupportedOperators = new List<string> { "=", "LIKE" }
                },
                new FieldMetadata
                {
                    FieldName = "DiaChi",
                    DisplayName = "Địa Chỉ",
                    DataType = TypeCode.String,
                    SupportedOperators = new List<string> { "=", "LIKE" }
                },
                new FieldMetadata
                {
                    FieldName = "SDT",
                    DisplayName = "Số Điện Thoại",
                    DataType = TypeCode.String,
                    SupportedOperators = new List<string> { "=" }
                },
                new FieldMetadata
                {
                    FieldName = "NgaySinh",
                    DisplayName = "Ngày Sinh",
                    DataType = TypeCode.DateTime,
                    SupportedOperators = new List<string> { "=", ">", "<", ">=", "<=", "Khoảng", "Đoạn" }
                }
            };
        }

        // Khởi tạo các cấu hình tìm kiếm cho bảng Thẻ Bạn Đọc
        public static List<FieldMetadata> GetTheBanDocFields()
        {
            return new List<FieldMetadata>
    {
        new FieldMetadata
        {
            FieldName = "MaTBD",
            DisplayName = "Mã Thẻ BĐ",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "=", "LIKE", "Bắt đầu bằng" }
        },
        new FieldMetadata
        {
            FieldName = "MaBD",
            DisplayName = "Mã Bạn Đọc",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "=", "LIKE", "Bắt đầu bằng" }
        },
        new FieldMetadata
        {
            FieldName = "HoTenBD",
            DisplayName = "Tên Bạn Đọc",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "LIKE", "Bắt đầu bằng" }
        },
        new FieldMetadata
        {
            FieldName = "MaTK",
            DisplayName = "Mã Tài Khoản",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "=" }
        },
        new FieldMetadata
        {
            FieldName = "HoTenNV",
            DisplayName = "Tên Nhân Viên",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "LIKE", "Bắt đầu bằng" }
        },
        new FieldMetadata
        {
            FieldName = "NgayCap",
            DisplayName = "Ngày Cấp",
            DataType = TypeCode.DateTime,
            SupportedOperators = new List<string> { "=", ">", "<", ">=", "<=", "Khoảng", "Đoạn" }
        },
        new FieldMetadata
        {
            FieldName = "NgayHetHan",
            DisplayName = "Ngày Hết Hạn",
            DataType = TypeCode.DateTime,
            SupportedOperators = new List<string> { "=", ">", "<", ">=", "<=", "Khoảng", "Đoạn" }
        },
        new FieldMetadata
        {
            FieldName = "TrangThai",
            DisplayName = "Trạng Thái",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "=" } // Ví dụ: Hoạt động, Khóa
        }
    };
        }

        // Khởi tạo các cấu hình tìm kiếm cho bảng Nhân Viên
        public static List<FieldMetadata> GetNhanVienFields()
        {
            return new List<FieldMetadata>
            {
                new FieldMetadata
                {
                    FieldName = "MaNV",
                    DisplayName = "Mã Nhân Viên",
                    DataType = TypeCode.String,
                    SupportedOperators = new List<string> { "=", "LIKE", "Bắt đầu bằng" }
                },
                new FieldMetadata
                {
                    FieldName = "HoTen", // Kết hợp HoDem và Ten
                    DisplayName = "Họ Tên",
                    DataType = TypeCode.String,
                    SupportedOperators = new List<string> { "LIKE", "Bắt đầu bằng" }
                },
                new FieldMetadata
                {
                    FieldName = "NgaySinh",
                    DisplayName = "Ngày Sinh",
                    DataType = TypeCode.DateTime,
                    // Dựa trên logic đã triển khai trong hàm SearchNhanVien
                    SupportedOperators = new List<string> { "=", ">", "<", ">=", "<=", "Khoảng", "Đoạn" }
                },
                new FieldMetadata
                {
                    FieldName = "GioiTinh",
                    DisplayName = "Giới Tính",
                    DataType = TypeCode.Char, // Hoặc TypeCode.String
                    SupportedOperators = new List<string> { "=" } // Chỉ cần so sánh bằng ('M'/'F')
                },
                new FieldMetadata
                {
                    FieldName = "DiaChi",
                    DisplayName = "Địa Chỉ",
                    DataType = TypeCode.String,
                    SupportedOperators = new List<string> { "=", "LIKE" }
                },
                new FieldMetadata
                {
                    FieldName = "SDT",
                    DisplayName = "Số Điện Thoại",
                    DataType = TypeCode.String,
                    SupportedOperators = new List<string> { "=" }
                },
                new FieldMetadata
                {
                    FieldName = "Email",
                    DisplayName = "Email",
                    DataType = TypeCode.String,
                    SupportedOperators = new List<string> { "=", "LIKE" }
                },
                new FieldMetadata
                {
                    FieldName = "PhuTrach",
                    DisplayName = "Phụ Trách",
                    DataType = TypeCode.String,
                    SupportedOperators = new List<string> { "=", "LIKE" }
                }
            };
        }
        // Khởi tạo các cấu hình tìm kiếm cho bảng Tài Khoản
        public static List<FieldMetadata> GetTaiKhoanFields()
        {
            return new List<FieldMetadata>
            {
                new FieldMetadata
                {
                    FieldName = "MaTK",
                    DisplayName = "Mã Tài Khoản",
                    DataType = TypeCode.String,
                    SupportedOperators = new List<string> { "=", "LIKE", "Bắt đầu bằng" }
                },
                new FieldMetadata
                {
                    FieldName = "MaNV",
                    DisplayName = "Mã Nhân Viên",
                    DataType = TypeCode.String,
                    SupportedOperators = new List<string> { "=", "LIKE", "Bắt đầu bằng" }
                },
                new FieldMetadata
                {
                    FieldName = "MaVT",
                    DisplayName = "Mã Vai Trò",
                    DataType = TypeCode.String,
                    SupportedOperators = new List<string> { "=" }
                },
                new FieldMetadata
                {
                    FieldName = "TenDangNhap",
                    DisplayName = "Tên Đăng Nhập",
                    DataType = TypeCode.String,
                    SupportedOperators = new List<string> { "=", "LIKE", "Bắt đầu bằng" }
                },
                new FieldMetadata
                {
                    FieldName = "TrangThai",
                    DisplayName = "Trạng Thái",
                    DataType = TypeCode.String,
                    SupportedOperators = new List<string> { "=" } // Ví dụ: Hoạt động, Bị khóa
                },
                new FieldMetadata
                {
                    FieldName = "NgayTao",
                    DisplayName = "Ngày Tạo",
                    DataType = TypeCode.DateTime,
                    SupportedOperators = new List<string> { "=", ">", "<", ">=", "<=", "Khoảng", "Đoạn" }
                }
            };
        }
        // Khởi tạo các cấu hình tìm kiếm cho bảng Tài Liệu
        public static List<FieldMetadata> GetTaiLieuFields()
        {
            return new List<FieldMetadata>
    {
        new FieldMetadata
        {
            FieldName = "MaTL",
            DisplayName = "Mã Tài Liệu",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "=", "LIKE", "Bắt đầu bằng" }
        },
        new FieldMetadata
        {
            FieldName = "TenTL",
            DisplayName = "Tên Tài Liệu",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "LIKE", "Bắt đầu bằng" }
        },
        new FieldMetadata
        {
            FieldName = "MaNXB",
            DisplayName = "Mã NXB",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "=", "LIKE" }
        },
        new FieldMetadata
        {
            FieldName = "MaNN",
            DisplayName = "Mã Ngôn Ngữ",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "=" }
        },
        new FieldMetadata
        {
            FieldName = "MaThL",
            DisplayName = "Mã Thể Loại",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "=" }
        },
        new FieldMetadata
        {
            FieldName = "MaDD",
            DisplayName = "Mã Định Dạng",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "=" }
        },
        new FieldMetadata
        {
            FieldName = "NamXuatBan",
            DisplayName = "Năm Xuất Bản",
            DataType = TypeCode.Int32, // Xử lý như số nguyên
            SupportedOperators = new List<string> { "=", ">", "<", ">=", "<=", "Khoảng", "Đoạn" }
        },
        new FieldMetadata
        {
            FieldName = "SoTrang",
            DisplayName = "Số Trang",
            DataType = TypeCode.Int32,
            SupportedOperators = new List<string> { "=", ">", "<", ">=", "<=", "Khoảng", "Đoạn" }
        }
    };
        }
        // Khởi tạo các cấu hình tìm kiếm cho bảng Tác Giả (Cần thiết cho UI)
        public static List<FieldMetadata> GetTacGiaFields()
        {
            return new List<FieldMetadata>
    {
        new FieldMetadata
        {
            FieldName = "MaTG",
            DisplayName = "Mã Tác giả",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "=", "LIKE", "Bắt đầu bằng" }
        },
        new FieldMetadata
        {
            FieldName = "HoTen", // Kết hợp HoDem và Ten
            DisplayName = "Họ Tên",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "LIKE", "Bắt đầu bằng" }
        },
        new FieldMetadata
        {
            FieldName = "MaQG",
            DisplayName = "Mã Quốc gia",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "=" }
        },
        new FieldMetadata
        {
            FieldName = "TenQG",
            DisplayName = "Tên Quốc gia",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "LIKE" }
        }
    };
        }

        // Khởi tạo các cấu hình tìm kiếm cho bảng Nhà Xuất Bản (Cần thiết cho UI)
        public static List<FieldMetadata> GetNxbFields()
        {
            return new List<FieldMetadata>
    {
        new FieldMetadata
        {
            FieldName = "MaNXB",
            DisplayName = "Mã NXB",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "=", "LIKE", "Bắt đầu bằng" }
        },
        new FieldMetadata
        {
            FieldName = "TenNXB",
            DisplayName = "Tên NXB",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "LIKE", "Bắt đầu bằng" }
        },
        new FieldMetadata
        {
            FieldName = "MaQG",
            DisplayName = "Mã Quốc gia",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "=" }
        },
        new FieldMetadata
        {
            FieldName = "TenQG",
            DisplayName = "Tên Quốc gia",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "LIKE" }
        }
    };
        }

        // Khởi tạo các cấu hình tìm kiếm cho bảng Thể Loại
        public static List<FieldMetadata> GetTheLoaiFields()
        {
            return new List<FieldMetadata>
    {
        new FieldMetadata
        {
            FieldName = "MaThL",
            DisplayName = "Mã Thể loại",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "=", "LIKE", "Bắt đầu bằng" }
        },
        new FieldMetadata
        {
            FieldName = "TenThL",
            DisplayName = "Tên Thể loại",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "LIKE", "Bắt đầu bằng" }
        }
    };
        }

        // Khởi tạo các cấu hình tìm kiếm cho bảng Định Dạng
        public static List<FieldMetadata> GetDinhDangFields()
        {
            return new List<FieldMetadata>
    {
        new FieldMetadata
        {
            FieldName = "MaDD",
            DisplayName = "Mã Định dạng",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "=", "LIKE", "Bắt đầu bằng" }
        },
        new FieldMetadata
        {
            FieldName = "TenDD",
            DisplayName = "Tên Định dạng",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "LIKE", "Bắt đầu bằng" }
        }
    };
        }
        // Khởi tạo các cấu hình tìm kiếm cho bảng Bản Sao
        public static List<FieldMetadata> GetBanSaoFields()
        {
            return new List<FieldMetadata>
    {
        new FieldMetadata
        {
            FieldName = "MaBS",
            DisplayName = "Mã Bản Sao",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "=", "LIKE", "Bắt đầu bằng" }
        },
        new FieldMetadata
        {
            FieldName = "TrangThai",
            DisplayName = "Trạng Thái",
            DataType = TypeCode.String,
            SupportedOperators = new List<string> { "=" } // Có sẵn/Không có sẵn/Ngưng sử dụng
        }
    };
        }
    }
}
 
