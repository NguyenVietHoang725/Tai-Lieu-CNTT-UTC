using System;
using System.Collections.Generic;

namespace Library_Manager.Models;

public partial class TQuocGia
{
    public string MaQg { get; set; } = null!;

    public string TenQg { get; set; } = null!;

    public virtual ICollection<TNhaXuatBan> TNhaXuatBan { get; set; } = new List<TNhaXuatBan>();

    public virtual ICollection<TTacGia> TTacGia { get; set; } = new List<TTacGia>();
}
