using System;
using System.Collections.Generic;

namespace Library_Manager.Models;

public partial class TNhanVien
{
    public string MaNv { get; set; } = null!;

    public string HoDem { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public DateOnly NgaySinh { get; set; }

    public string GioiTinh { get; set; } = null!;

    public string? DiaChi { get; set; }

    public string Sdt { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhuTrach { get; set; }

    public virtual ICollection<TTaiKhoan> TTaiKhoan { get; set; } = new List<TTaiKhoan>();
}
