using System;
using System.Collections.Generic;

namespace NguyenVietHoang_231230791.Models;

public partial class HanhKhach
{
    public string MaKhach { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public string? Cccd { get; set; }

    public string? DienThoai { get; set; }

    public int? MaHang { get; set; }

    public string? Anh { get; set; }

    public double? DiemTichLuy { get; set; }

    public int? MaLoai { get; set; }

    public virtual PhanHang? MaHangNavigation { get; set; }

    public virtual LoaiKhach? MaLoaiNavigation { get; set; }
}
