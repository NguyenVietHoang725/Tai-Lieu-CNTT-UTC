using System;
using System.Collections.Generic;

namespace Library_Manager.Models;

public partial class TBanDoc
{
    public string MaBd { get; set; } = null!;

    public string HoDem { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public DateOnly NgaySinh { get; set; }

    public string GioiTinh { get; set; } = null!;

    public string GioiTinhHienThi
    {
        get
        {
            return GioiTinh switch
            {
                "M" => "Nam",
                "F" => "Nữ",
                _ => "Khác"
            };
        }
    }

    public string? DiaChi { get; set; }

    public string Sdt { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<TTheBanDoc> TTheBanDocs { get; set; } = new List<TTheBanDoc>();
}
