using System;
using System.Collections.Generic;

namespace qlsv.Models;
public class Diem
{
    public string IdLopHocPhan { get; set; } = null!;
    public string IdSinhVien { get; set; } = null!;
    public decimal? DiemQuaTrinh { get; set; }
    public decimal? DiemKetThuc { get; set; }
    public decimal? DiemTongKet { get; set; }

    public virtual LopHocPhan IdLopHocPhanNavigation { get; set; } = null!;
    public virtual SinhVien IdSinhVienNavigation { get; set; } = null!;
}

