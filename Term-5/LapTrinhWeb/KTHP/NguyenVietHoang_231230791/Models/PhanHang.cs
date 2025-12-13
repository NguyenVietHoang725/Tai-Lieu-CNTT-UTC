using System;
using System.Collections.Generic;

namespace NguyenVietHoang_231230791.Models;

public partial class PhanHang
{
    public int MaHang { get; set; }

    public string TenHang { get; set; } = null!;

    public string? ChinhSach { get; set; }

    public virtual ICollection<HanhKhach> HanhKhaches { get; set; } = new List<HanhKhach>();
}
