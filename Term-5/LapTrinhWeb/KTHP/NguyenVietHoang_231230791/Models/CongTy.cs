using System;
using System.Collections.Generic;

namespace NguyenVietHoang_231230791.Models;

public partial class CongTy
{
    public string MaCongTy { get; set; } = null!;

    public string TenCongTy { get; set; } = null!;

    public string? DiaChi { get; set; }

    public string? Email { get; set; }

    public string? DienThoai { get; set; }

    public string? Anh { get; set; }

    public virtual ICollection<Xe> Xes { get; set; } = new List<Xe>();
}
