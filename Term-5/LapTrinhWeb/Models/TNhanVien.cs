using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Manager.Models;

public partial class TNhanVien
{
    public string MaNv { get; set; } = null!;

    public string HoDem { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public DateTime NgaySinh { get; set; }

    public string GioiTinh { get; set; } = null!;

    public string? DiaChi { get; set; }

    public string Sdt { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhuTrach { get; set; } = null!;

    public virtual ICollection<TTaiKhoan> TTaiKhoans { get; set; } = new List<TTaiKhoan>();

}
