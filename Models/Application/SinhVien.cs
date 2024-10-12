using System;
using System.Collections.Generic;

namespace qlsv.Models;
public class SinhVien
{
    public SinhVien()
    {
        DiemDanhs = new HashSet<DiemDanh>();
        Diems = new HashSet<Diem>();
        IdLopHocPhans = new HashSet<LopHocPhan>();
    }

    public string IdSinhVien { get; set; } = null!;
    public string HoTen { get; set; } = null!;
    public string Lop { get; set; } = null!;
    public string ChuyenNganh { get; set; } = null!;
    public DateTime? NgaySinh { get; set; }
    public string? DiaChi { get; set; }

    public virtual ICollection<DiemDanh> DiemDanhs { get; set; }
    public virtual ICollection<Diem> Diems { get; set; }

    public virtual ICollection<LopHocPhan> IdLopHocPhans { get; set; }
}

