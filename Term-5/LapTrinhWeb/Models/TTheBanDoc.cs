using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Library_Manager.Models;

public partial class TTheBanDoc
{
    public string MaTbd { get; set; } = null!;

    public string MaBd { get; set; } = null!;

    public DateOnly NgayCap { get; set; }

    public DateOnly? NgayHetHan { get; set; }

    public string TrangThai { get; set; } = null!;

    public string MaTk { get; set; } = null!;

    [ValidateNever]
    public virtual TBanDoc MaBdNavigation { get; set; } = null!;

    [ValidateNever]
    public virtual TTaiKhoan MaTkNavigation { get; set; } = null!;

    public virtual ICollection<TGiaoDichMuonTra> TGiaoDichMuonTras { get; set; } = new List<TGiaoDichMuonTra>();

}
