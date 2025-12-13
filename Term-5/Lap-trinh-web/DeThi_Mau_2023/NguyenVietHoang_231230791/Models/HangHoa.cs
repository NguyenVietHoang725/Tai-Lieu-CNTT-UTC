using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // 1. Thêm thư viện này

namespace NguyenVietHoang_231230791.Models;

public partial class HangHoa
{
    public int MaHang { get; set; }

    public int MaLoai { get; set; }

    [Required(ErrorMessage = "Tên hàng không được để trống")]
    public string TenHang { get; set; } = null!;

    // 2. Validate Giá: Từ 100 đến 5000
    [Range(100, 5000, ErrorMessage = "Giá phải nằm trong khoảng từ 100 đến 5000")]
    public decimal? Gia { get; set; }

    // 3. Validate Ảnh: Sử dụng Regex để kiểm tra đuôi file
    // Pattern giải thích: Kết thúc bằng (.) sau đó là jpg/png/gif/tiff
    [RegularExpression(@"^.*\.(jpg|png|gif|tiff)$", ErrorMessage = "Tên file ảnh phải có đuôi: .jpg, .png, .gif, .tiff")]
    public string? Anh { get; set; }

    [ValidateNever]
    public virtual LoaiHang MaLoaiNavigation { get; set; } = null!;
}
