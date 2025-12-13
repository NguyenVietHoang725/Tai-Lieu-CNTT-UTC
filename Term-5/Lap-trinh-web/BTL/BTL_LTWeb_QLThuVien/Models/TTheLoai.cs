using System;
using System.Collections.Generic;

namespace Library_Manager.Models;

public partial class TTheLoai
{
    public string MaThL { get; set; } = null!;

    public string TenThL { get; set; } = null!;

    public virtual ICollection<TTaiLieu> TTaiLieu { get; set; } = new List<TTaiLieu>();
}
