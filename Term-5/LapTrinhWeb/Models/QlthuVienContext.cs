using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Library_Manager.Models;

public partial class QlthuVienContext : DbContext
{
    public QlthuVienContext()
    {
    }

    public QlthuVienContext(DbContextOptions<QlthuVienContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TBanDoc> TBanDocs { get; set; }

    public virtual DbSet<TBanSao> TBanSaos { get; set; }

    public virtual DbSet<TDinhDang> TDinhDangs { get; set; }

    public virtual DbSet<TGiaoDichBanSao> TGiaoDichBanSaos { get; set; }

    public virtual DbSet<TGiaoDichMuonTra> TGiaoDichMuonTras { get; set; }

    public virtual DbSet<TNgonNgu> TNgonNgus { get; set; }

    public virtual DbSet<TNhaXuatBan> TNhaXuatBans { get; set; }

    public virtual DbSet<TNhanVien> TNhanViens { get; set; }

    public virtual DbSet<TTacGia> TTacGia { get; set; }

    public virtual DbSet<TTaiKhoan> TTaiKhoans { get; set; }

    public virtual DbSet<TTaiLieu> TTaiLieus { get; set; }

    public virtual DbSet<TTaiLieuTacGia> TTaiLieuTacGia { get; set; }

    public virtual DbSet<TTheBanDoc> TTheBanDocs { get; set; }

    public virtual DbSet<TTheLoai> TTheLoais { get; set; }

    public virtual DbSet<TVaiTro> TVaiTros { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-EQ8Q1MJ\\SQLEXPRESS;Database=QLThuVien;Trusted_Connection=True;TrustServerCertificate=True;Command Timeout=120");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TBanDoc>(entity =>
        {
            entity.HasKey(e => e.MaBd).HasName("PK__tBanDoc__272475A710261635");

            entity.ToTable("tBanDoc");

            entity.Property(e => e.MaBd)
                .HasMaxLength(10)
                .HasColumnName("MaBD");
            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.HoDem).HasMaxLength(50);
            entity.Property(e => e.Sdt)
                .HasMaxLength(15)
                .HasColumnName("SDT");
            entity.Property(e => e.Ten).HasMaxLength(30);
        });

        modelBuilder.Entity<TBanSao>(entity =>
        {
            entity.HasKey(e => e.MaBs).HasName("PK__tBanSao__2724759610ACCE09");

            entity.ToTable("tBanSao");

            entity.Property(e => e.MaBs)
                .HasMaxLength(10)
                .HasColumnName("MaBS");
            entity.Property(e => e.MaTl)
                .HasMaxLength(10)
                .HasColumnName("MaTL");
            entity.Property(e => e.TinhTrang).HasMaxLength(30);

            entity.HasOne(d => d.MaTlNavigation).WithMany(p => p.TBanSaos)
                .HasForeignKey(d => d.MaTl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tBanSao__MaTL__60A75C0F");
        });

        modelBuilder.Entity<TDinhDang>(entity =>
        {
            entity.HasKey(e => e.MaDd).HasName("PK__tDinhDan__272586658B70631A");

            entity.ToTable("tDinhDang");

            entity.HasIndex(e => e.TenDd, "UQ__tDinhDan__4CF9655254F8CC1D").IsUnique();

            entity.Property(e => e.MaDd)
                .HasMaxLength(10)
                .HasColumnName("MaDD");
            entity.Property(e => e.TenDd)
                .HasMaxLength(50)
                .HasColumnName("TenDD");
        });

        modelBuilder.Entity<TGiaoDichBanSao>(entity =>
        {
            entity.HasKey(e => new { e.MaGd, e.MaBs }).HasName("PK__tGiaoDic__5557E9D89B499743");

            entity.ToTable("tGiaoDich_BanSao");

            entity.Property(e => e.MaGd)
                .HasMaxLength(10)
                .HasColumnName("MaGD");
            entity.Property(e => e.MaBs)
                .HasMaxLength(10)
                .HasColumnName("MaBS");
            entity.Property(e => e.TinhTrangMuon).HasMaxLength(30);
            entity.Property(e => e.TinhTrangTra).HasMaxLength(30);

            entity.HasOne(d => d.MaBsNavigation).WithMany(p => p.TGiaoDichBanSaos)
                .HasForeignKey(d => d.MaBs)
                .HasConstraintName("FK__tGiaoDich___MaBS__75A278F5");

            entity.HasOne(d => d.MaGdNavigation).WithMany(p => p.TGiaoDichBanSaos)
                .HasForeignKey(d => d.MaGd)
                .HasConstraintName("FK__tGiaoDich___MaGD__74AE54BC");
        });

        modelBuilder.Entity<TGiaoDichMuonTra>(entity =>
        {
            entity.HasKey(e => e.MaGd).HasName("PK__tGiaoDic__2725AE8101E364E2");

            entity.ToTable("tGiaoDichMuonTra");

            entity.Property(e => e.MaGd)
                .HasMaxLength(10)
                .HasColumnName("MaGD");
            entity.Property(e => e.MaTbd)
                .HasMaxLength(10)
                .HasColumnName("MaTBD");
            entity.Property(e => e.MaTk)
                .HasMaxLength(10)
                .HasColumnName("MaTK");
            entity.Property(e => e.NgayHenTra).HasColumnType("datetime");
            entity.Property(e => e.NgayMuon).HasColumnType("datetime");
            entity.Property(e => e.NgayTra).HasColumnType("datetime");
            entity.Property(e => e.TrangThai).HasMaxLength(30);

            entity.HasOne(d => d.MaTbdNavigation).WithMany(p => p.TGiaoDichMuonTras)
                .HasForeignKey(d => d.MaTbd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tGiaoDich__MaTBD__70DDC3D8");

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.TGiaoDichMuonTras)
                .HasForeignKey(d => d.MaTk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tGiaoDichM__MaTK__71D1E811");
        });

        modelBuilder.Entity<TNgonNgu>(entity =>
        {
            entity.HasKey(e => e.MaNn).HasName("PK__tNgonNgu__2725D732C2D0663F");

            entity.ToTable("tNgonNgu");

            entity.HasIndex(e => e.TenNn, "UQ__tNgonNgu__4CF9B4ACC1D2A4F5").IsUnique();

            entity.Property(e => e.MaNn)
                .HasMaxLength(10)
                .HasColumnName("MaNN");
            entity.Property(e => e.TenNn)
                .HasMaxLength(50)
                .HasColumnName("TenNN");
        });

        modelBuilder.Entity<TNhaXuatBan>(entity =>
        {
            entity.HasKey(e => e.MaNxb).HasName("PK__tNhaXuat__3A19482CA1C3E6D0");

            entity.ToTable("tNhaXuatBan");

            entity.HasIndex(e => e.TenNxb, "UQ__tNhaXuat__CCE3868D031A483E").IsUnique();

            entity.Property(e => e.MaNxb)
                .HasMaxLength(10)
                .HasColumnName("MaNXB");
            entity.Property(e => e.TenNxb)
                .HasMaxLength(50)
                .HasColumnName("TenNXB");
        });

        modelBuilder.Entity<TNhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__tNhanVie__2725D70AE1A40245");

            entity.ToTable("tNhanVien");

            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .HasColumnName("MaNV");
            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.HoDem).HasMaxLength(50);
            entity.Property(e => e.NgaySinh).HasColumnType("datetime");
            entity.Property(e => e.PhuTrach).HasMaxLength(100);
            entity.Property(e => e.Sdt)
                .HasMaxLength(15)
                .HasColumnName("SDT");
            entity.Property(e => e.Ten).HasMaxLength(30);
        });

        modelBuilder.Entity<TTacGia>(entity =>
        {
            entity.HasKey(e => e.MaTg).HasName("PK__tTacGia__2725007436A5F170");

            entity.ToTable("tTacGia");

            entity.Property(e => e.MaTg)
                .HasMaxLength(10)
                .HasColumnName("MaTG");
            entity.Property(e => e.HoDem).HasMaxLength(50);
            entity.Property(e => e.Ten).HasMaxLength(30);
        });

        modelBuilder.Entity<TTaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaTk).HasName("PK__tTaiKhoa__27250070B0E4D0E3");

            entity.ToTable("tTaiKhoan");

            entity.Property(e => e.MaTk)
                .HasMaxLength(10)
                .HasColumnName("MaTK");
            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .HasColumnName("MaNV");
            entity.Property(e => e.MaVt)
                .HasMaxLength(10)
                .HasColumnName("MaVT");
            entity.Property(e => e.MatKhau).HasMaxLength(30);
            entity.Property(e => e.TenDangNhap).HasMaxLength(30);
            entity.Property(e => e.TrangThai).HasMaxLength(30);

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.TTaiKhoans)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tTaiKhoan__MaNV__6D0D32F4");

            entity.HasOne(d => d.MaVtNavigation).WithMany(p => p.TTaiKhoans)
                .HasForeignKey(d => d.MaVt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tTaiKhoan__MaVT__6E01572D");
        });

        modelBuilder.Entity<TTaiLieu>(entity =>
        {
            entity.HasKey(e => e.MaTl).HasName("PK__tTaiLieu__272500715EC9DBF2");

            entity.ToTable("tTaiLieu");

            entity.Property(e => e.MaTl)
                .HasMaxLength(10)
                .HasColumnName("MaTL");
            entity.Property(e => e.KhoCo).HasMaxLength(30);
            entity.Property(e => e.MaDd)
                .HasMaxLength(10)
                .HasColumnName("MaDD");
            entity.Property(e => e.MaNn)
                .HasMaxLength(10)
                .HasColumnName("MaNN");
            entity.Property(e => e.MaNxb)
                .HasMaxLength(10)
                .HasColumnName("MaNXB");
            entity.Property(e => e.MaThL).HasMaxLength(10);
            entity.Property(e => e.MaTk)
                .HasMaxLength(10)
                .HasDefaultValue("TK03")
                .HasColumnName("MaTK");
            entity.Property(e => e.TenTl)
                .HasMaxLength(100)
                .HasColumnName("TenTL");

            entity.HasOne(d => d.MaDdNavigation).WithMany(p => p.TTaiLieus)
                .HasForeignKey(d => d.MaDd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tTaiLieu__MaDD__59FA5E80");

            entity.HasOne(d => d.MaNnNavigation).WithMany(p => p.TTaiLieus)
                .HasForeignKey(d => d.MaNn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tTaiLieu__MaNN__5812160E");

            entity.HasOne(d => d.MaNxbNavigation).WithMany(p => p.TTaiLieus)
                .HasForeignKey(d => d.MaNxb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tTaiLieu__MaNXB__571DF1D5");

            entity.HasOne(d => d.MaThLNavigation).WithMany(p => p.TTaiLieus)
                .HasForeignKey(d => d.MaThL)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tTaiLieu__MaThL__59063A47");

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.TTaiLieus)
                .HasForeignKey(d => d.MaTk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tTaiLieu__MaTK__151B244E");
        });

        modelBuilder.Entity<TTaiLieuTacGia>(entity =>
        {
            entity.HasKey(e => new { e.MaTl, e.MaTg }).HasName("PK__tTaiLieu__755750768AB3B65E");

            entity.ToTable("tTaiLieu_TacGia");

            entity.Property(e => e.MaTl)
                .HasMaxLength(10)
                .HasColumnName("MaTL");
            entity.Property(e => e.MaTg)
                .HasMaxLength(10)
                .HasColumnName("MaTG");
            entity.Property(e => e.VaiTro).HasMaxLength(30);

            entity.HasOne(d => d.MaTgNavigation).WithMany(p => p.TTaiLieuTacGia)
                .HasForeignKey(d => d.MaTg)
                .HasConstraintName("FK__tTaiLieu_T__MaTG__5DCAEF64");

            entity.HasOne(d => d.MaTlNavigation).WithMany(p => p.TTaiLieuTacGia)
                .HasForeignKey(d => d.MaTl)
                .HasConstraintName("FK__tTaiLieu_T__MaTL__5CD6CB2B");
        });

        modelBuilder.Entity<TTheBanDoc>(entity =>
        {
            entity.HasKey(e => e.MaTbd).HasName("PK__tTheBanD__3149BE70516B65CF");

            entity.ToTable("tTheBanDoc");

            entity.Property(e => e.MaTbd)
                .HasMaxLength(10)
                .HasColumnName("MaTBD");
            entity.Property(e => e.MaBd)
                .HasMaxLength(10)
                .HasColumnName("MaBD");
            entity.Property(e => e.MaTk)
                .HasMaxLength(10)
                .HasDefaultValue("TK11")
                .HasColumnName("MaTK");
            entity.Property(e => e.TrangThai).HasMaxLength(30);

            entity.HasOne(d => d.MaBdNavigation).WithMany(p => p.TTheBanDocs)
                .HasForeignKey(d => d.MaBd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tTheBanDoc__MaBD__656C112C");

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.TTheBanDocs)
                .HasForeignKey(d => d.MaTk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tTheBanDoc__MaTK__17036CC0");
        });

        modelBuilder.Entity<TTheLoai>(entity =>
        {
            entity.HasKey(e => e.MaThL).HasName("PK__tTheLoai__314EEAB4AF189FB6");

            entity.ToTable("tTheLoai");

            entity.HasIndex(e => e.TenThL, "UQ__tTheLoai__CD4F500CE0D3A072").IsUnique();

            entity.Property(e => e.MaThL).HasMaxLength(10);
            entity.Property(e => e.TenThL).HasMaxLength(50);
        });

        modelBuilder.Entity<TVaiTro>(entity =>
        {
            entity.HasKey(e => e.MaVt).HasName("PK__tVaiTro__2725103EFF1651C9");

            entity.ToTable("tVaiTro");

            entity.HasIndex(e => e.TenVt, "UQ__tVaiTro__4CF9F7BC8EA7B111").IsUnique();

            entity.Property(e => e.MaVt)
                .HasMaxLength(10)
                .HasColumnName("MaVT");
            entity.Property(e => e.MoTa).HasMaxLength(100);
            entity.Property(e => e.TenVt)
                .HasMaxLength(30)
                .HasColumnName("TenVT");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
