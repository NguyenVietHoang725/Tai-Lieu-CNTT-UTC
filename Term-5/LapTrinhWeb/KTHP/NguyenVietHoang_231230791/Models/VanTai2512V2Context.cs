using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NguyenVietHoang_231230791.Models;

public partial class VanTai2512V2Context : DbContext
{
    public VanTai2512V2Context()
    {
    }

    public VanTai2512V2Context(DbContextOptions<VanTai2512V2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Chuyen> Chuyens { get; set; }

    public virtual DbSet<CongTy> CongTies { get; set; }

    public virtual DbSet<DatVe> DatVes { get; set; }

    public virtual DbSet<HanhKhach> HanhKhaches { get; set; }

    public virtual DbSet<LaiXe> LaiXes { get; set; }

    public virtual DbSet<LoaiKhach> LoaiKhaches { get; set; }

    public virtual DbSet<LoaiXe> LoaiXes { get; set; }

    public virtual DbSet<NavItem> NavItems { get; set; }

    public virtual DbSet<PhanHang> PhanHangs { get; set; }

    public virtual DbSet<TinhThanh> TinhThanhs { get; set; }

    public virtual DbSet<Tuyen> Tuyens { get; set; }

    public virtual DbSet<Xe> Xes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=HOANGNGUYEN\\SQLEXPRESS;Database=VanTai2512V2;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chuyen>(entity =>
        {
            entity.HasKey(e => e.MaChuyen);

            entity.ToTable("Chuyen");

            entity.Property(e => e.MaTuyen).HasMaxLength(10);
            entity.Property(e => e.NgayGio).HasColumnType("datetime");
            entity.Property(e => e.SoXe).HasMaxLength(20);

            entity.HasOne(d => d.MaLaiXeNavigation).WithMany(p => p.Chuyens)
                .HasForeignKey(d => d.MaLaiXe)
                .HasConstraintName("FK_Chuyen_LaiXe");

            entity.HasOne(d => d.MaTuyenNavigation).WithMany(p => p.Chuyens)
                .HasForeignKey(d => d.MaTuyen)
                .HasConstraintName("FK_Chuyen_Tuyen");

            entity.HasOne(d => d.SoXeNavigation).WithMany(p => p.Chuyens)
                .HasForeignKey(d => d.SoXe)
                .HasConstraintName("FK_Chuyen_Xe");
        });

        modelBuilder.Entity<CongTy>(entity =>
        {
            entity.HasKey(e => e.MaCongTy).HasName("PK_NhaXe");

            entity.ToTable("CongTy");

            entity.Property(e => e.MaCongTy).HasMaxLength(10);
            entity.Property(e => e.Anh).HasMaxLength(50);
            entity.Property(e => e.DiaChi).HasMaxLength(250);
            entity.Property(e => e.DienThoai).HasMaxLength(30);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.TenCongTy).HasMaxLength(50);
        });

        modelBuilder.Entity<DatVe>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DatVe");

            entity.Property(e => e.DiemDon).HasMaxLength(50);
            entity.Property(e => e.MaKhach)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.MaChuyenNavigation).WithMany()
                .HasForeignKey(d => d.MaChuyen)
                .HasConstraintName("FK_DatVe_Chuyen");

            entity.HasOne(d => d.MaKhachNavigation).WithMany()
                .HasForeignKey(d => d.MaKhach)
                .HasConstraintName("FK_DatVe_KhachHang");
        });

        modelBuilder.Entity<HanhKhach>(entity =>
        {
            entity.HasKey(e => e.MaKhach).HasName("PK_KhachHang");

            entity.ToTable("HanhKhach");

            entity.Property(e => e.MaKhach)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Anh).HasMaxLength(50);
            entity.Property(e => e.Cccd)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CCCD");
            entity.Property(e => e.DienThoai)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.HoTen).HasMaxLength(50);

            entity.HasOne(d => d.MaHangNavigation).WithMany(p => p.HanhKhaches)
                .HasForeignKey(d => d.MaHang)
                .HasConstraintName("FK_HanhKhach_PhanHang");

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.HanhKhaches)
                .HasForeignKey(d => d.MaLoai)
                .HasConstraintName("FK_KhachHang_LoaiKhach");
        });

        modelBuilder.Entity<LaiXe>(entity =>
        {
            entity.HasKey(e => e.MaLaiXe);

            entity.ToTable("LaiXe");

            entity.Property(e => e.Anh).HasMaxLength(50);
            entity.Property(e => e.HoTen).HasMaxLength(50);
        });

        modelBuilder.Entity<LoaiKhach>(entity =>
        {
            entity.HasKey(e => e.MaLoai);

            entity.ToTable("LoaiKhach");

            entity.Property(e => e.TenLoai).HasMaxLength(100);
        });

        modelBuilder.Entity<LoaiXe>(entity =>
        {
            entity.HasKey(e => e.MaLoaiXe).HasName("PK_LoaiNhaXe");

            entity.ToTable("LoaiXe");

            entity.Property(e => e.TenLoaiXe).HasMaxLength(100);
        });

        modelBuilder.Entity<NavItem>(entity =>
        {
            entity.Property(e => e.Link).HasMaxLength(250);
            entity.Property(e => e.NavName).HasMaxLength(50);
            entity.Property(e => e.NavNameVn)
                .HasMaxLength(100)
                .HasColumnName("NavNameVN");
        });

        modelBuilder.Entity<PhanHang>(entity =>
        {
            entity.HasKey(e => e.MaHang);

            entity.ToTable("PhanHang");

            entity.Property(e => e.ChinhSach).HasMaxLength(100);
            entity.Property(e => e.TenHang).HasMaxLength(50);
        });

        modelBuilder.Entity<TinhThanh>(entity =>
        {
            entity.HasKey(e => e.MaTinhThanh);

            entity.ToTable("TinhThanh");

            entity.Property(e => e.MaTinhThanh).HasMaxLength(10);
            entity.Property(e => e.TenTinhThanh).HasMaxLength(100);
            entity.Property(e => e.VietTat).HasMaxLength(5);
        });

        modelBuilder.Entity<Tuyen>(entity =>
        {
            entity.HasKey(e => e.MaTuyen);

            entity.ToTable("Tuyen");

            entity.Property(e => e.MaTuyen).HasMaxLength(10);
            entity.Property(e => e.MaDiemCuoi).HasMaxLength(10);
            entity.Property(e => e.MaDiemDau).HasMaxLength(10);
            entity.Property(e => e.TenTuyen).HasMaxLength(50);

            entity.HasOne(d => d.MaDiemCuoiNavigation).WithMany(p => p.TuyenMaDiemCuoiNavigations)
                .HasForeignKey(d => d.MaDiemCuoi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tuyen_TinhThanh_DiemCuoi");

            entity.HasOne(d => d.MaDiemDauNavigation).WithMany(p => p.TuyenMaDiemDauNavigations)
                .HasForeignKey(d => d.MaDiemDau)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tuyen_TinhThanh_DiemDau");
        });

        modelBuilder.Entity<Xe>(entity =>
        {
            entity.HasKey(e => e.SoXe);

            entity.ToTable("Xe");

            entity.Property(e => e.SoXe).HasMaxLength(20);
            entity.Property(e => e.Anh).HasMaxLength(50);
            entity.Property(e => e.MaCongTy).HasMaxLength(10);
            entity.Property(e => e.MauXe).HasMaxLength(50);

            entity.HasOne(d => d.MaCongTyNavigation).WithMany(p => p.Xes)
                .HasForeignKey(d => d.MaCongTy)
                .HasConstraintName("FK_Xe_CongTy");

            entity.HasOne(d => d.MaLoaiXeNavigation).WithMany(p => p.Xes)
                .HasForeignKey(d => d.MaLoaiXe)
                .HasConstraintName("FK_Xe_LoaiXe");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
