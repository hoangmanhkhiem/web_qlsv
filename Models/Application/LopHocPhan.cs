using System;
using System.Collections.Generic;

namespace qlsv.Models;
public class LopHocPhan
{
    public LopHocPhan()
    {
        DiemDanhs = new HashSet<DiemDanh>();
        Diems = new HashSet<Diem>();
        IdSinhViens = new HashSet<SinhVien>();
        IdThoiGians = new HashSet<ThoiGian>();
    }

    public string IdHocPhan { get; set; } = null!;
    public string TenHocPhan { get; set; } = null!;
    public string? IdGiaoVien { get; set; }

    public virtual GiaoVien? IdGiaoVienNavigation { get; set; }
    public virtual ICollection<DiemDanh> DiemDanhs { get; set; }
    public virtual ICollection<Diem> Diems { get; set; }

    public virtual ICollection<SinhVien> IdSinhViens { get; set; }
    public virtual ICollection<ThoiGian> IdThoiGians { get; set; }
}

