using System;
using System.Collections.Generic;

namespace qlsv.Models
{
    public class Diem
    {
        public string IdDiem { get; set; } = null!;
        public string? IdLopHocPhan { get; set; }
        public string? IdSinhVien { get; set; }
        public decimal? DiemQuaTrinh { get; set; }
        public decimal? DiemKetThuc { get; set; }
        public decimal? DiemTongKet { get; set; }

        public virtual LopHocPhan? IdLopHocPhanNavigation { get; set; }
        public virtual SinhVien? IdSinhVienNavigation { get; set; }
    }
}
