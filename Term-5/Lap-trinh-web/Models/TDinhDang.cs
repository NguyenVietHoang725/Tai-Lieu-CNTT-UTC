using System;
using System.Collections.Generic;

namespace Library_Manager.Models;

public partial class TDinhDang
{
    public string MaDd { get; set; } = null!;

    public string TenDd { get; set; } = null!;

    public virtual ICollection<TTaiLieu> TTaiLieus { get; set; } = new List<TTaiLieu>();
}
