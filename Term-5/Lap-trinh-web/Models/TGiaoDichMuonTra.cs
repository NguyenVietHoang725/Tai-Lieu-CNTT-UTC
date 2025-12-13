using System;
using System.Collections.Generic;

namespace Library_Manager.Models;

public partial class TGiaoDichMuonTra
{
    public string MaGd { get; set; } = null!;

    public string MaTbd { get; set; } = null!;

    public string MaTk { get; set; } = null!;

    public DateTime NgayMuon { get; set; }

    public DateTime NgayHenTra { get; set; }

    public DateTime? NgayTra { get; set; }

    public string TrangThai { get; set; } = null!;

    public virtual TTheBanDoc MaTbdNavigation { get; set; } = null!;

    public virtual TTaiKhoan MaTkNavigation { get; set; } = null!;

    public virtual ICollection<TGiaoDichBanSao> TGiaoDichBanSaos { get; set; } = new List<TGiaoDichBanSao>();
}
