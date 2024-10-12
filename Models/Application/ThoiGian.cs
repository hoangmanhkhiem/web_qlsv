using System;
using System.Collections.Generic;

namespace qlsv.Models;

public partial class ThoiGian
{
    public ThoiGian()
    {
        DiemDanhs = new HashSet<DiemDanh>();
        IdLopHocPhans = new HashSet<LopHocPhan>();
    }

    public string IdThoiGian { get; set; } = null!;
    public DateTime Ngay { get; set; }
    public TimeSpan ThoiGianBd { get; set; }
    public TimeSpan ThoiGianKt { get; set; }

    public virtual ICollection<DiemDanh> DiemDanhs { get; set; }

    public virtual ICollection<LopHocPhan> IdLopHocPhans { get; set; }
}

