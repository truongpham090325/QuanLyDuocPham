using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OnTapCuoiKy_De1.Models;

public partial class QlduocPhamContext : DbContext
{
    public QlduocPhamContext()
    {
    }

    public QlduocPhamContext(DbContextOptions<QlduocPhamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DanhMucThuoc> DanhMucThuocs { get; set; }

    public virtual DbSet<Thuoc> Thuocs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = LAPTOP-V0468VFD; Database=QLDuocPham; Integrated Security=True; TrustServerCertificate= True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DanhMucThuoc>(entity =>
        {
            entity.HasKey(e => e.MaDm).HasName("PK__DanhMucT__2725866EF5AB88F4");

            entity.ToTable("DanhMucThuoc");

            entity.Property(e => e.MaDm)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaDM");
            entity.Property(e => e.TenDm)
                .HasMaxLength(50)
                .HasColumnName("TenDM");
        });

        modelBuilder.Entity<Thuoc>(entity =>
        {
            entity.HasKey(e => e.MaThuoc).HasName("PK__Thuoc__4BB1F620F75A7B18");

            entity.ToTable("Thuoc");

            entity.Property(e => e.MaThuoc)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaDm)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaDM");
            entity.Property(e => e.TenThuoc).HasMaxLength(30);

            entity.HasOne(d => d.MaDmNavigation).WithMany(p => p.Thuocs)
                .HasForeignKey(d => d.MaDm)
                .HasConstraintName("FK__Thuoc__MaDM__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
