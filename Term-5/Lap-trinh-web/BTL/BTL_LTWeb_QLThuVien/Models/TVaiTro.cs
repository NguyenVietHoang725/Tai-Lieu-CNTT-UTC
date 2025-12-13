using System;
using System.Collections.Generic;

namespace Library_Manager.Models;

public partial class TVaiTro
{
    public string MaVt { get; set; } = null!;

    public string TenVt { get; set; } = null!;

    public virtual ICollection<TTaiKhoan> TTaiKhoan { get; set; } = new List<TTaiKhoan>();
}
