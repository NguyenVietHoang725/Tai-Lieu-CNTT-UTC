using System;
using System.Collections.Generic;

namespace Library_Manager.Models;

public partial class TTaiKhoan
{
    public string MaTk { get; set; } = null!;

    public string MaNv { get; set; } = null!;

    public string MaVt { get; set; } = null!;

    public string TenDangNhap { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string TrangThai { get; set; } = null!;

    public DateOnly NgayTao { get; set; }

    public virtual TNhanVien MaNvNavigation { get; set; } = null!;

    public virtual TVaiTro MaVtNavigation { get; set; } = null!;

    public virtual ICollection<TGiaoDichMuonTra> TGiaoDichMuonTras { get; set; } = new List<TGiaoDichMuonTra>();

    public virtual ICollection<TTaiLieu> TTaiLieus { get; set; } = new List<TTaiLieu>();

    public virtual ICollection<TTheBanDoc> TTheBanDocs { get; set; } = new List<TTheBanDoc>();
}
