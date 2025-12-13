using System;
using System.Collections.Generic;

namespace Library_Manager.Models;

public partial class TGiaoDichMuonTra
{
    public string MaGd { get; set; } = null!;

    public string MaTbd { get; set; } = null!;

    public string MaTk { get; set; } = null!;

    public DateOnly NgayMuon { get; set; }

    public DateOnly NgayHenTra { get; set; }

    public DateOnly? NgayTra { get; set; }

    public string TrangThai { get; set; } = null!;

    public virtual TTheBanDoc MaTbdNavigation { get; set; } = null!;

    public virtual TTaiKhoan MaTkNavigation { get; set; } = null!;

    public virtual ICollection<TGiaoDichBanSao> TGiaoDichBanSao { get; set; } = new List<TGiaoDichBanSao>();
}
