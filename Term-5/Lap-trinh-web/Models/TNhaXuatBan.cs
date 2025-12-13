using System;
using System.Collections.Generic;

namespace Library_Manager.Models;

public partial class TNhaXuatBan
{
    public string MaNxb { get; set; } = null!;

    public string TenNxb { get; set; } = null!;

    public string MaQg { get; set; } = null!;

    public virtual ICollection<TTaiLieu> TTaiLieus { get; set; } = new List<TTaiLieu>();

    public virtual TQuocGium MaQgNavigation { get; set; } = null!;
}
