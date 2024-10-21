using System;
using System.Collections.Generic;

namespace qlsv.Models
{
    public class SinhVienLopHocPhan
    {
        public string IdSinhVienLopHocPhan { get; set; } = null!;
        public string? IdSinhVien { get; set; }
        public string? IdLopHocPhan { get; set; }

        public virtual LopHocPhan? IdLopHocPhanNavigation { get; set; }
        public virtual SinhVien? IdSinhVienNavigation { get; set; }
    }
}
