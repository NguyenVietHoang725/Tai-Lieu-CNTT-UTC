using System;
using System.Collections.Generic;

namespace NguyenVietHoang_231230791.Models;

public partial class DatVe
{
    public string? MaKhach { get; set; }

    public int? MaChuyen { get; set; }

    public int? SoVe { get; set; }

    public double? GiaVe { get; set; }

    public string? DiemDon { get; set; }

    public virtual Chuyen? MaChuyenNavigation { get; set; }

    public virtual HanhKhach? MaKhachNavigation { get; set; }
}
