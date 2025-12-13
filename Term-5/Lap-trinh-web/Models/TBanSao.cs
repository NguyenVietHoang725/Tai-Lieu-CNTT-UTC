using System;
using System.Collections.Generic;

namespace Library_Manager.Models;

public partial class TBanSao
{
    public string MaBs { get; set; } = null!;

    public string MaTl { get; set; } = null!;

    public string? TinhTrang { get; set; }

    public virtual TTaiLieu MaTlNavigation { get; set; } = null!;

    public virtual ICollection<TGiaoDichBanSao> TGiaoDichBanSaos { get; set; } = new List<TGiaoDichBanSao>();
}
