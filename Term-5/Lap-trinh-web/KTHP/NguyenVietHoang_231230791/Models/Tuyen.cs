using System;
using System.Collections.Generic;

namespace NguyenVietHoang_231230791.Models;

public partial class Tuyen
{
    public string MaTuyen { get; set; } = null!;

    public string? TenTuyen { get; set; }

    public string MaDiemDau { get; set; } = null!;

    public string MaDiemCuoi { get; set; } = null!;

    public bool? TrangThai { get; set; }

    public int? KhoangCach { get; set; }

    public virtual ICollection<Chuyen> Chuyens { get; set; } = new List<Chuyen>();

    public virtual TinhThanh MaDiemCuoiNavigation { get; set; } = null!;

    public virtual TinhThanh MaDiemDauNavigation { get; set; } = null!;
}
