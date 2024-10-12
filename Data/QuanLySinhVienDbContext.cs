using Microsoft.EntityFrameworkCore;
using qlsv.Models;

namespace qlsv.Data;

public class QuanLySinhVienDbContext : DbContext
{
    // Variables
    public virtual DbSet<Diem> Diems { get; set; } = null!;
    public virtual DbSet<DiemDanh> DiemDanhs { get; set; } = null!;
    public virtual DbSet<GiaoVien> GiaoViens { get; set; } = null!;
    public virtual DbSet<LopHocPhan> LopHocPhans { get; set; } = null!;
    public virtual DbSet<SinhVien> SinhViens { get; set; } = null!;
    public virtual DbSet<ThoiGian> ThoiGians { get; set; } = null!;

    // Constructor
    public QuanLySinhVienDbContext()
    {
    }

    public QuanLySinhVienDbContext(DbContextOptions<QuanLySinhVienDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Diem>(entity =>
        {
            entity.HasKey(e => new { e.IdLopHocPhan, e.IdSinhVien })
                .HasName("PK__Diem__72255A5ACAFEA388");

            entity.ToTable("Diem");

            entity.Property(e => e.IdLopHocPhan).HasMaxLength(36);

            entity.Property(e => e.IdSinhVien).HasMaxLength(36);

            entity.Property(e => e.DiemKetThuc).HasColumnType("decimal(5, 2)");

            entity.Property(e => e.DiemQuaTrinh).HasColumnType("decimal(5, 2)");

            entity.Property(e => e.DiemTongKet).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.IdLopHocPhanNavigation)
                .WithMany(p => p.Diems)
                .HasForeignKey(d => d.IdLopHocPhan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Diem__IdLopHocPh__5EBF139D");

            entity.HasOne(d => d.IdSinhVienNavigation)
                .WithMany(p => p.Diems)
                .HasForeignKey(d => d.IdSinhVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Diem__IdSinhVien__5FB337D6");
        });

        modelBuilder.Entity<DiemDanh>(entity =>
        {
            entity.HasKey(e => new { e.IdSinhVien, e.IdThoiGian, e.IdLopHocPhan })
                .HasName("PK__DiemDanh__817E0446A874C894");

            entity.ToTable("DiemDanh");

            entity.Property(e => e.IdSinhVien).HasMaxLength(36);

            entity.Property(e => e.IdThoiGian).HasMaxLength(36);

            entity.Property(e => e.IdLopHocPhan).HasMaxLength(36);

            entity.HasOne(d => d.IdLopHocPhanNavigation)
                .WithMany(p => p.DiemDanhs)
                .HasForeignKey(d => d.IdLopHocPhan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DiemDanh__IdLopH__5812160E");

            entity.HasOne(d => d.IdSinhVienNavigation)
                .WithMany(p => p.DiemDanhs)
                .HasForeignKey(d => d.IdSinhVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DiemDanh__IdSinh__5629CD9C");

            entity.HasOne(d => d.IdThoiGianNavigation)
                .WithMany(p => p.DiemDanhs)
                .HasForeignKey(d => d.IdThoiGian)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DiemDanh__IdThoi__571DF1D5");
        });

        modelBuilder.Entity<GiaoVien>(entity =>
        {
            entity.HasKey(e => e.IdGiaoVien)
                .HasName("PK__GiaoVien__F4372AE328C6DBED");

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
            entity.HasKey(e => e.IdHocPhan)
                .HasName("PK__LopHocPh__6A35A4EC70344C38");

            entity.ToTable("LopHocPhan");

            entity.Property(e => e.IdHocPhan)
                .HasMaxLength(36)
                .HasDefaultValueSql("(newid())");

            entity.Property(e => e.IdGiaoVien).HasMaxLength(36);

            entity.Property(e => e.TenHocPhan).HasMaxLength(100);

            entity.HasOne(d => d.IdGiaoVienNavigation)
                .WithMany(p => p.LopHocPhans)
                .HasForeignKey(d => d.IdGiaoVien)
                .HasConstraintName("FK__LopHocPha__IdGia__4D94879B");

            entity.HasMany(d => d.IdThoiGians)
                .WithMany(p => p.IdLopHocPhans)
                .UsingEntity<Dictionary<string, object>>(
                    "ThoiGianLopHocPhan",
                    l => l.HasOne<ThoiGian>().WithMany().HasForeignKey("IdThoiGian").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__ThoiGian___IdTho__5BE2A6F2"),
                    r => r.HasOne<LopHocPhan>().WithMany().HasForeignKey("IdLopHocPhan").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__ThoiGian___IdLop__5AEE82B9"),
                    j =>
                    {
                        j.HasKey("IdLopHocPhan", "IdThoiGian").HasName("PK__ThoiGian__15C11AD9B63A9B11");

                        j.ToTable("ThoiGian_LopHocPhan");

                        j.IndexerProperty<string>("IdLopHocPhan").HasMaxLength(36);

                        j.IndexerProperty<string>("IdThoiGian").HasMaxLength(36);
                    });
        });

        modelBuilder.Entity<SinhVien>(entity =>
        {
            entity.HasKey(e => e.IdSinhVien)
                .HasName("PK__SinhVien__93ABE5766A04A8ED");

            entity.ToTable("SinhVien");

            entity.Property(e => e.IdSinhVien)
                .HasMaxLength(36)
                .HasDefaultValueSql("(newid())");

            entity.Property(e => e.ChuyenNganh).HasMaxLength(100);

            entity.Property(e => e.DiaChi).HasMaxLength(255);

            entity.Property(e => e.HoTen).HasMaxLength(100);

            entity.Property(e => e.Lop).HasMaxLength(50);

            entity.Property(e => e.NgaySinh).HasColumnType("date");

            entity.HasMany(d => d.IdLopHocPhans)
                .WithMany(p => p.IdSinhViens)
                .UsingEntity<Dictionary<string, object>>(
                    "SinhVienLopHocPhan",
                    l => l.HasOne<LopHocPhan>().WithMany().HasForeignKey("IdLopHocPhan").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__SinhVien___IdLop__6383C8BA"),
                    r => r.HasOne<SinhVien>().WithMany().HasForeignKey("IdSinhVien").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__SinhVien___IdSin__628FA481"),
                    j =>
                    {
                        j.HasKey("IdSinhVien", "IdLopHocPhan").HasName("PK__SinhVien__531A1B36CDEDAB4C");

                        j.ToTable("SinhVien_LopHocPhan");

                        j.IndexerProperty<string>("IdSinhVien").HasMaxLength(36);

                        j.IndexerProperty<string>("IdLopHocPhan").HasMaxLength(36);
                    });
        });

        modelBuilder.Entity<ThoiGian>(entity =>
        {
            entity.HasKey(e => e.IdThoiGian)
                .HasName("PK__ThoiGian__EDEFED40D5785B54");

            entity.ToTable("ThoiGian");

            entity.Property(e => e.IdThoiGian)
                .HasMaxLength(36)
                .HasDefaultValueSql("(newid())");

            entity.Property(e => e.Ngay).HasColumnType("date");

            entity.Property(e => e.ThoiGianBd).HasColumnName("ThoiGianBD");

            entity.Property(e => e.ThoiGianKt).HasColumnName("ThoiGianKT");
        });

    }
}