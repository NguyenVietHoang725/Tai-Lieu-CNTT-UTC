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

    public virtual DbSet<TBanDoc> TBanDoc { get; set; }

    public virtual DbSet<TBanSao> TBanSao { get; set; }

    public virtual DbSet<TDinhDang> TDinhDang { get; set; }

    public virtual DbSet<TGiaoDichBanSao> TGiaoDichBanSao { get; set; }

    public virtual DbSet<TGiaoDichMuonTra> TGiaoDichMuonTra { get; set; }

    public virtual DbSet<TNgonNgu> TNgonNgu { get; set; }

    public virtual DbSet<TNhaXuatBan> TNhaXuatBan { get; set; }

    public virtual DbSet<TNhanVien> TNhanVien { get; set; }

    public virtual DbSet<TQuocGia> TQuocGia { get; set; }

    public virtual DbSet<TTacGia> TTacGia { get; set; }

    public virtual DbSet<TTaiKhoan> TTaiKhoan { get; set; }

    public virtual DbSet<TTaiLieu> TTaiLieu { get; set; }

    public virtual DbSet<TTaiLieuTacGia> TTaiLieuTacGia { get; set; }

    public virtual DbSet<TTheBanDoc> TTheBanDoc { get; set; }

    public virtual DbSet<TTheLoai> TTheLoai { get; set; }

    public virtual DbSet<TVaiTro> TVaiTro { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=HOANGNGUYEN\\SQLEXPRESS;Database=QLThuVien;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TBanDoc>(entity =>
        {
            entity.HasKey(e => e.MaBd).HasName("PK__tBanDoc__272475A7FF9B5EE3");

            entity.ToTable("tBanDoc");

            entity.Property(e => e.MaBd)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaBD");
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.HoDem).HasMaxLength(50);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("SDT");
            entity.Property(e => e.Ten).HasMaxLength(30);
        });

        modelBuilder.Entity<TBanSao>(entity =>
        {
            entity.HasKey(e => e.MaBs).HasName("PK__tBanSao__27247596286DE968");

            entity.ToTable("tBanSao");

            entity.Property(e => e.MaBs)
                .HasMaxLength(14)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaBS");
            entity.Property(e => e.MaTl)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaTL");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(30)
                .HasDefaultValue("Có sẵn");

            entity.HasOne(d => d.MaTlNavigation).WithMany(p => p.TBanSao)
                .HasForeignKey(d => d.MaTl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BanSao_TaiLieu");
        });

        modelBuilder.Entity<TDinhDang>(entity =>
        {
            entity.HasKey(e => e.MaDd).HasName("PK__tDinhDan__27258665190F2871");

            entity.ToTable("tDinhDang");

            entity.HasIndex(e => e.TenDd, "UQ__tDinhDan__4CF96552C1C4EC11").IsUnique();

            entity.Property(e => e.MaDd)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaDD");
            entity.Property(e => e.TenDd)
                .HasMaxLength(50)
                .HasColumnName("TenDD");
        });

        modelBuilder.Entity<TGiaoDichBanSao>(entity =>
        {
            entity.HasKey(e => new { e.MaGd, e.MaBs }).HasName("PK__tGiaoDic__5557E9D8E3758997");

            entity.ToTable("tGiaoDich_BanSao");

            entity.Property(e => e.MaGd)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaGD");
            entity.Property(e => e.MaBs)
                .HasMaxLength(14)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaBS");

            entity.HasOne(d => d.MaBsNavigation).WithMany(p => p.TGiaoDichBanSao)
                .HasForeignKey(d => d.MaBs)
                .HasConstraintName("FK_GDBS_BS");

            entity.HasOne(d => d.MaGdNavigation).WithMany(p => p.TGiaoDichBanSao)
                .HasForeignKey(d => d.MaGd)
                .HasConstraintName("FK_GDBS_GD");
        });

        modelBuilder.Entity<TGiaoDichMuonTra>(entity =>
        {
            entity.HasKey(e => e.MaGd).HasName("PK__tGiaoDic__2725AE81038FF307");

            entity.ToTable("tGiaoDichMuonTra");

            entity.Property(e => e.MaGd)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaGD");
            entity.Property(e => e.MaTbd)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaTBD");
            entity.Property(e => e.MaTk)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaTK");
            entity.Property(e => e.NgayMuon).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(30)
                .HasDefaultValue("Ðang mu?n");

            entity.HasOne(d => d.MaTbdNavigation).WithMany(p => p.TGiaoDichMuonTra)
                .HasForeignKey(d => d.MaTbd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GD_TheBanDoc");

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.TGiaoDichMuonTra)
                .HasForeignKey(d => d.MaTk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GD_TaiKhoan");
        });

        modelBuilder.Entity<TNgonNgu>(entity =>
        {
            entity.HasKey(e => e.MaNn).HasName("PK__tNgonNgu__2725D73289C5A1D2");

            entity.ToTable("tNgonNgu");

            entity.HasIndex(e => e.TenNn, "UQ__tNgonNgu__4CF9B4AC1CF79F53").IsUnique();

            entity.Property(e => e.MaNn)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNN");
            entity.Property(e => e.TenNn)
                .HasMaxLength(50)
                .HasColumnName("TenNN");
        });

        modelBuilder.Entity<TNhaXuatBan>(entity =>
        {
            entity.HasKey(e => e.MaNxb).HasName("PK__tNhaXuat__3A19482C76939C06");

            entity.ToTable("tNhaXuatBan");

            entity.HasIndex(e => e.TenNxb, "UQ__tNhaXuat__CCE3868DD1E3E531").IsUnique();

            entity.Property(e => e.MaNxb)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNXB");
            entity.Property(e => e.MaQg)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaQG");
            entity.Property(e => e.TenNxb)
                .HasMaxLength(100)
                .HasColumnName("TenNXB");

            entity.HasOne(d => d.MaQgNavigation).WithMany(p => p.TNhaXuatBan)
                .HasForeignKey(d => d.MaQg)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NXB_QuocGia");
        });

        modelBuilder.Entity<TNhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__tNhanVie__2725D70A6DCE2E88");

            entity.ToTable("tNhanVien");

            entity.Property(e => e.MaNv)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNV");
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.HoDem).HasMaxLength(50);
            entity.Property(e => e.PhuTrach).HasMaxLength(100);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("SDT");
            entity.Property(e => e.Ten).HasMaxLength(30);
        });

        modelBuilder.Entity<TQuocGia>(entity =>
        {
            entity.HasKey(e => e.MaQg).HasName("PK__tQuocGia__2725F8574EB79A63");

            entity.ToTable("tQuocGia");

            entity.Property(e => e.MaQg)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaQG");
            entity.Property(e => e.TenQg)
                .HasMaxLength(100)
                .HasColumnName("TenQG");
        });

        modelBuilder.Entity<TTacGia>(entity =>
        {
            entity.HasKey(e => e.MaTg).HasName("PK__tTacGia__272500747CF8339B");

            entity.ToTable("tTacGia");

            entity.Property(e => e.MaTg)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaTG");
            entity.Property(e => e.HoDem).HasMaxLength(50);
            entity.Property(e => e.MaQg)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaQG");
            entity.Property(e => e.Ten).HasMaxLength(30);

            entity.HasOne(d => d.MaQgNavigation).WithMany(p => p.TTacGia)
                .HasForeignKey(d => d.MaQg)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TacGia_QuocGia");
        });

        modelBuilder.Entity<TTaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaTk).HasName("PK__tTaiKhoa__2725007004E57011");

            entity.ToTable("tTaiKhoan");

            entity.HasIndex(e => e.TenDangNhap, "UQ__tTaiKhoa__55F68FC03EA073CB").IsUnique();

            entity.Property(e => e.MaTk)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaTK");
            entity.Property(e => e.MaNv)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNV");
            entity.Property(e => e.MaVt)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaVT");
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.NgayTao).HasDefaultValueSql("(CONVERT([date],getdate()))");
            entity.Property(e => e.TenDangNhap).HasMaxLength(255);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(30)
                .HasDefaultValue("Hoạt động");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.TTaiKhoan)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaiKhoan_NhanVien");

            entity.HasOne(d => d.MaVtNavigation).WithMany(p => p.TTaiKhoan)
                .HasForeignKey(d => d.MaVt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaiKhoan_VaiTro");
        });

        modelBuilder.Entity<TTaiLieu>(entity =>
        {
            entity.HasKey(e => e.MaTl).HasName("PK__tTaiLieu__272500712B76AB56");

            entity.ToTable("tTaiLieu");

            entity.Property(e => e.MaTl)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaTL");
            entity.Property(e => e.Anh).HasMaxLength(255);
            entity.Property(e => e.KhoCo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaDd)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaDD");
            entity.Property(e => e.MaNn)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNN");
            entity.Property(e => e.MaNxb)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNXB");
            entity.Property(e => e.MaThL)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaTk)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaTK");
            entity.Property(e => e.TenTl)
                .HasMaxLength(200)
                .HasColumnName("TenTL");

            entity.HasOne(d => d.MaDdNavigation).WithMany(p => p.TTaiLieu)
                .HasForeignKey(d => d.MaDd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaiLieu_DinhDang");

            entity.HasOne(d => d.MaNnNavigation).WithMany(p => p.TTaiLieu)
                .HasForeignKey(d => d.MaNn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaiLieu_NgonNgu");

            entity.HasOne(d => d.MaNxbNavigation).WithMany(p => p.TTaiLieu)
                .HasForeignKey(d => d.MaNxb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaiLieu_NXB");

            entity.HasOne(d => d.MaThLNavigation).WithMany(p => p.TTaiLieu)
                .HasForeignKey(d => d.MaThL)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaiLieu_TheLoai");

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.TTaiLieu)
                .HasForeignKey(d => d.MaTk)
                .HasConstraintName("FK_TaiLieu_TaiKhoan");
        });

        modelBuilder.Entity<TTaiLieuTacGia>(entity =>
        {
            entity.HasKey(e => new { e.MaTl, e.MaTg }).HasName("PK__tTaiLieu__75575076ECC5B7BD");

            entity.ToTable("tTaiLieu_TacGia");

            entity.Property(e => e.MaTl)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaTL");
            entity.Property(e => e.MaTg)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaTG");
            entity.Property(e => e.VaiTro).HasMaxLength(50);

            entity.HasOne(d => d.MaTgNavigation).WithMany(p => p.TTaiLieuTacGia)
                .HasForeignKey(d => d.MaTg)
                .HasConstraintName("FK_TL_TG_TacGia");

            entity.HasOne(d => d.MaTlNavigation).WithMany(p => p.TTaiLieuTacGia)
                .HasForeignKey(d => d.MaTl)
                .HasConstraintName("FK_TL_TG_TaiLieu");
        });

        modelBuilder.Entity<TTheBanDoc>(entity =>
        {
            entity.HasKey(e => e.MaTbd).HasName("PK__tTheBanD__3149BE709AA3492D");

            entity.ToTable("tTheBanDoc");

            entity.Property(e => e.MaTbd)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaTBD");
            entity.Property(e => e.MaBd)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaBD");
            entity.Property(e => e.MaTk)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaTK");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(30)
                .HasDefaultValue("Ho?t d?ng")
                .ValueGeneratedNever(); 

            // NgayCap và NgayHetHan không cần ValueGeneratedNever nếu không có default trong DB

            entity.HasOne(d => d.MaBdNavigation).WithMany(p => p.TTheBanDoc)
                .HasForeignKey(d => d.MaBd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TheBanDoc_BanDoc");

            entity.HasOne(d => d.MaTkNavigation).WithMany(p => p.TTheBanDoc)
                .HasForeignKey(d => d.MaTk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TheBanDoc_TaiKhoan");
        });

        modelBuilder.Entity<TTheLoai>(entity =>
        {
            entity.HasKey(e => e.MaThL).HasName("PK__tTheLoai__314EEAB4CE3F7DD6");

            entity.ToTable("tTheLoai");

            entity.HasIndex(e => e.TenThL, "UQ__tTheLoai__CD4F500CD735555C").IsUnique();

            entity.Property(e => e.MaThL)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenThL).HasMaxLength(50);
        });

        modelBuilder.Entity<TVaiTro>(entity =>
        {
            entity.HasKey(e => e.MaVt).HasName("PK__tVaiTro__2725103E418EA7A2");

            entity.ToTable("tVaiTro");

            entity.HasIndex(e => e.TenVt, "UQ__tVaiTro__4CF9F7BC19028B7E").IsUnique();

            entity.Property(e => e.MaVt)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaVT");
            entity.Property(e => e.TenVt)
                .HasMaxLength(50)
                .HasColumnName("TenVT");
        });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
