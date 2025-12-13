using System;
using System.Collections.Generic;

namespace Library_Manager.Models;

public partial class TTaiLieu
{
    public string MaTl { get; set; } = null!;

    public string MaNxb { get; set; } = null!;

    public string MaNn { get; set; } = null!;

    public string MaThL { get; set; } = null!;

    public string MaDd { get; set; } = null!;

    public string TenTl { get; set; } = null!;

    public int? LanXuatBan { get; set; }

    public int? NamXuatBan { get; set; }

    public int? SoTrang { get; set; }

    public string? KhoCo { get; set; }

    public string MaTk { get; set; } = null!;

    public virtual TDinhDang MaDdNavigation { get; set; } = null!;

    public virtual TNgonNgu MaNnNavigation { get; set; } = null!;

    public virtual TNhaXuatBan MaNxbNavigation { get; set; } = null!;

    public virtual TTheLoai MaThLNavigation { get; set; } = null!;

    public virtual TTaiKhoan MaTkNavigation { get; set; } = null!;

    public virtual ICollection<TBanSao> TBanSaos { get; set; } = new List<TBanSao>();

    public virtual ICollection<TTaiLieuTacGia> TTaiLieuTacGia { get; set; } = new List<TTaiLieuTacGia>();
}
