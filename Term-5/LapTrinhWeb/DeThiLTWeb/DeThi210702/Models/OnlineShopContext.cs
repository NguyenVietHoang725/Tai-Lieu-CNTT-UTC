using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DeThi210702.Models;

public partial class OnlineShopContext : DbContext
{
    public OnlineShopContext()
    {
    }

    public OnlineShopContext(DbContextOptions<OnlineShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<NavItem> NavItems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=HOANGNGUYEN\\SQLEXPRESS;Database=OnlineShop;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Categories");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NameVn)
                .HasMaxLength(50)
                .HasColumnName("NameVN");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Customers");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Fullname).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.RePassword).HasMaxLength(50);
        });

        modelBuilder.Entity<NavItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.NavItems");

            entity.Property(e => e.NavName).HasMaxLength(50);
            entity.Property(e => e.NavNameVn)
                .HasMaxLength(50)
                .HasColumnName("NavNameVN");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Orders");

            entity.Property(e => e.OrderDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.Orders_dbo.Customers_CustomerId");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.OrderDetails");

            entity.Property(e => e.ProductId).HasMaxLength(10);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_dbo.OrderDetails_dbo.Orders_OrderId");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_dbo.OrderDetails_dbo.Products_ProductId");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Products");

            entity.Property(e => e.Id).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_dbo.Products_dbo.Categories_CategoryId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
