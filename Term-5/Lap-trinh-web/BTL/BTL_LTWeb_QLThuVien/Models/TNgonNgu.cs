using System;
using System.Collections.Generic;

namespace Library_Manager.Models;

public partial class TNgonNgu
{
    public string MaNn { get; set; } = null!;

    public string TenNn { get; set; } = null!;

    public virtual ICollection<TTaiLieu> TTaiLieu { get; set; } = new List<TTaiLieu>();
}
