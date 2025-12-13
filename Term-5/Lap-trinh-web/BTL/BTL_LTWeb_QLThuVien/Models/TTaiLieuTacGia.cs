using System;
using System.Collections.Generic;

namespace Library_Manager.Models;

public partial class TTaiLieuTacGia
{
    public string MaTl { get; set; } = null!;

    public string MaTg { get; set; } = null!;

    public string? VaiTro { get; set; }

    public virtual TTacGia MaTgNavigation { get; set; } = null!;

    public virtual TTaiLieu MaTlNavigation { get; set; } = null!;
}
