using System;
using System.Collections.Generic;

namespace qlsv.Models
{
    public class DiemDanh
    {
        public string IdDiemDanh { get; set; } = null!;
        public string? IdSinhVien { get; set; }
        public string? IdThoiGian { get; set; }
        public string? IdLopHocPhan { get; set; }
        public bool? DaDiemDanh { get; set; }

        public virtual LopHocPhan? IdLopHocPhanNavigation { get; set; }
        public virtual SinhVien? IdSinhVienNavigation { get; set; }
        public virtual ThoiGian? IdThoiGianNavigation { get; set; }
    }
}
