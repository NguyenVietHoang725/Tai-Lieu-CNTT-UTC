using System;
using System.Collections.Generic;

namespace Library_Manager.Models;

public partial class TGiaoDichBanSao
{
    public string MaGd { get; set; } = null!;

    public string MaBs { get; set; } = null!;

    public string? TinhTrangMuon { get; set; }

    public string? TinhTrangTra { get; set; }

    public virtual TBanSao MaBsNavigation { get; set; } = null!;

    public virtual TGiaoDichMuonTra MaGdNavigation { get; set; } = null!;
}
