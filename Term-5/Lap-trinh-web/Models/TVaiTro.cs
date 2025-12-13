using System;
using System.Collections.Generic;

namespace Library_Manager.Models;

public partial class TVaiTro
{
    public string MaVt { get; set; } = null!;

    public string TenVt { get; set; } = null!;

    public string? MoTa { get; set; }

    public virtual ICollection<TTaiKhoan> TTaiKhoans { get; set; } = new List<TTaiKhoan>();
}
