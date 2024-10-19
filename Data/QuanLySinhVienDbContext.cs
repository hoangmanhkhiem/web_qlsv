using Microsoft.EntityFrameworkCore;
using qlsv.Models;

namespace qlsv.Data;

public class QuanLySinhVienDbContext : DbContext
{
    // Variables
    public DbSet<Diem> Diems { get; set; }
    public DbSet<DiemDanh> DiemDanhs { get; set; }
    public DbSet<GiaoVien> GiaoViens { get; set; }
    public DbSet<LopHocPhan> LopHocPhans { get; set; }
    public DbSet<SinhVien> SinhViens { get; set; }
    public DbSet<ThoiGian> ThoiGians { get; set; }
    public DbSet<SinhVienLopHocPhan> SinhVienLopHocPhans { get; set; }
    public DbSet<ThoiGianLopHocPhan> ThoiGianLopHocPhans { get; set; }

    // Constructor
    public QuanLySinhVienDbContext(DbContextOptions<QuanLySinhVienDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Diem>(entity =>
            {
                entity.HasKey(e => e.IdDiem);

                entity.ToTable("Diem");

                entity.Property(e => e.IdDiem)
                    .HasMaxLength(36)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DiemKetThuc).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.DiemQuaTrinh).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.DiemTongKet).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.IdLopHocPhan).HasMaxLength(36);

                entity.Property(e => e.IdSinhVien).HasMaxLength(36);

                entity.HasOne(d => d.IdLopHocPhanNavigation)
                    .WithMany(p => p.Diems)
                    .HasForeignKey(d => d.IdLopHocPhan);

                entity.HasOne(d => d.IdSinhVienNavigation)
                    .WithMany(p => p.Diems)
                    .HasForeignKey(d => d.IdSinhVien);
            });

        modelBuilder.Entity<DiemDanh>(entity =>
        {
            entity.HasKey(e => e.IdDiemDanh);

            entity.ToTable("DiemDanh");

            entity.Property(e => e.IdDiemDanh)
                .HasMaxLength(36)
                .HasDefaultValueSql("(newid())");

            entity.Property(e => e.IdLopHocPhan).HasMaxLength(36);

            entity.Property(e => e.IdSinhVien).HasMaxLength(36);

            entity.Property(e => e.IdThoiGian).HasMaxLength(36);

            entity.HasOne(d => d.IdLopHocPhanNavigation)
                .WithMany(p => p.DiemDanhs)
                .HasForeignKey(d => d.IdLopHocPhan);

            entity.HasOne(d => d.IdSinhVienNavigation)
                .WithMany(p => p.DiemDanhs)
                .HasForeignKey(d => d.IdSinhVien);

            entity.HasOne(d => d.IdThoiGianNavigation)
                .WithMany(p => p.DiemDanhs)
                .HasForeignKey(d => d.IdThoiGian);
        });

        modelBuilder.Entity<GiaoVien>(entity =>
        {
            entity.HasKey(e => e.IdGiaoVien);

            entity.ToTable("GiaoVien");

            entity.Property(e => e.IdGiaoVien)
                .HasMaxLength(36)
                .HasDefaultValueSql("(newid())");

            entity.Property(e => e.ChuyenMon).HasMaxLength(100);

            entity.Property(e => e.Email).HasMaxLength(100);

            entity.Property(e => e.SoDienThoai).HasMaxLength(15);

            entity.Property(e => e.TenGiaoVien).HasMaxLength(100);
        });

        modelBuilder.Entity<LopHocPhan>(entity =>
        {
            entity.HasKey(e => e.IdHocPhan);

            entity.ToTable("LopHocPhan");

            entity.Property(e => e.IdHocPhan)
                .HasMaxLength(36)
                .HasDefaultValueSql("(newid())");

            entity.Property(e => e.IdGiaoVien).HasMaxLength(36);

            entity.Property(e => e.TenHocPhan).HasMaxLength(100);

            entity.HasOne(d => d.IdGiaoVienNavigation)
                .WithMany(p => p.LopHocPhans)
                .HasForeignKey(d => d.IdGiaoVien);
        });

        modelBuilder.Entity<SinhVien>(entity =>
        {
            entity.HasKey(e => e.IdSinhVien);

            entity.ToTable("SinhVien");

            entity.Property(e => e.IdSinhVien)
                .HasMaxLength(36)
                .HasDefaultValueSql("(newid())");

            entity.Property(e => e.ChuyenNganh).HasMaxLength(100);

            entity.Property(e => e.DiaChi).HasMaxLength(255);

            entity.Property(e => e.HoTen).HasMaxLength(100);

            entity.Property(e => e.Lop).HasMaxLength(50);

            entity.Property(e => e.NgaySinh).HasColumnType("date");
        });

        modelBuilder.Entity<SinhVienLopHocPhan>(entity =>
        {
            entity.HasKey(e => e.IdSinhVienLopHocPhan);

            entity.ToTable("SinhVien_LopHocPhan");

            entity.Property(e => e.IdSinhVienLopHocPhan)
                .HasMaxLength(36)
                .HasColumnName("IdSinhVien_LopHocPhan")
                .HasDefaultValueSql("(newid())");

            entity.Property(e => e.IdLopHocPhan).HasMaxLength(36);

            entity.Property(e => e.IdSinhVien).HasMaxLength(36);

            entity.HasOne(d => d.IdLopHocPhanNavigation)
                .WithMany(p => p.SinhVienLopHocPhans)
                .HasForeignKey(d => d.IdLopHocPhan);

            entity.HasOne(d => d.IdSinhVienNavigation)
                .WithMany(p => p.SinhVienLopHocPhans)
                .HasForeignKey(d => d.IdSinhVien);
        });

        modelBuilder.Entity<ThoiGian>(entity =>
        {
            entity.HasKey(e => e.IdThoiGian);

            entity.ToTable("ThoiGian");

            entity.Property(e => e.IdThoiGian)
                .HasMaxLength(36)
                .HasDefaultValueSql("(newid())");

            entity.Property(e => e.Ngay).HasColumnType("date");

            entity.Property(e => e.ThoiGianBd).HasColumnName("ThoiGianBD");

            entity.Property(e => e.ThoiGianKt).HasColumnName("ThoiGianKT");
        });

        modelBuilder.Entity<ThoiGianLopHocPhan>(entity =>
        {
            entity.HasKey(e => e.IdThoiGianLopHocPhan);

            entity.ToTable("ThoiGian_LopHocPhan");

            entity.Property(e => e.IdThoiGianLopHocPhan)
                .HasMaxLength(36)
                .HasColumnName("IdThoiGian_LopHocPhan")
                .HasDefaultValueSql("(newid())");

            entity.Property(e => e.IdLopHocPhan).HasMaxLength(36);

            entity.Property(e => e.IdThoiGian).HasMaxLength(36);

            entity.HasOne(d => d.IdLopHocPhanNavigation)
                .WithMany(p => p.ThoiGianLopHocPhans)
                .HasForeignKey(d => d.IdLopHocPhan);

            entity.HasOne(d => d.IdThoiGianNavigation)
                .WithMany(p => p.ThoiGianLopHocPhans)
                .HasForeignKey(d => d.IdThoiGian);
        });
    }
}