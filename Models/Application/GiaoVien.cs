using System;
using System.Collections.Generic;

namespace qlsv.Models;
public class GiaoVien
{
    public GiaoVien()
    {
        LopHocPhans = new HashSet<LopHocPhan>();
    }

    public string IdGiaoVien { get; set; } = null!;
    public string TenGiaoVien { get; set; } = null!;
    public string? Email { get; set; }
    public string? SoDienThoai { get; set; }
    public string? ChuyenMon { get; set; }

    public virtual ICollection<LopHocPhan> LopHocPhans { get; set; }
}

