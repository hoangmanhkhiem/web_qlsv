using System;
using System.Collections.Generic;

namespace qlsv.Models;
public class DiemDanh
{
    public string IdSinhVien { get; set; } = null!;
    public string IdThoiGian { get; set; } = null!;
    public string IdLopHocPhan { get; set; } = null!;
    public bool? DaDiemDanh { get; set; }

    public virtual LopHocPhan IdLopHocPhanNavigation { get; set; } = null!;
    public virtual SinhVien IdSinhVienNavigation { get; set; } = null!;
    public virtual ThoiGian IdThoiGianNavigation { get; set; } = null!;
}

