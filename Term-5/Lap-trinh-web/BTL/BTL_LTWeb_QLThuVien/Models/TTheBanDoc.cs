using System;
using System.Collections.Generic;

namespace Library_Manager.Models;

public partial class TTheBanDoc
{
    public string MaTbd { get; set; } = null!;

    public string MaBd { get; set; } = null!;

    public string MaTk { get; set; } = null!;

    public DateOnly NgayCap { get; set; }

    public DateOnly? NgayHetHan { get; set; }

    public string TrangThai { get; set; } = null!;

    public virtual TBanDoc MaBdNavigation { get; set; } = null!;

    public virtual TTaiKhoan MaTkNavigation { get; set; } = null!;

    public virtual ICollection<TGiaoDichMuonTra> TGiaoDichMuonTra { get; set; } = new List<TGiaoDichMuonTra>();
}
