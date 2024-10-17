using System;
using System.Collections.Generic;

namespace qlsv.Models
{
    public class LopHocPhan
    {
        public LopHocPhan()
        {
            DiemDanhs = new HashSet<DiemDanh>();
            Diems = new HashSet<Diem>();
            SinhVienLopHocPhans = new HashSet<SinhVienLopHocPhan>();
            ThoiGianLopHocPhans = new HashSet<ThoiGianLopHocPhan>();
        }

        public string IdHocPhan { get; set; } = null!;
        public string TenHocPhan { get; set; } = null!;
        public string? IdGiaoVien { get; set; }

        public virtual GiaoVien? IdGiaoVienNavigation { get; set; }
        public virtual ICollection<DiemDanh> DiemDanhs { get; set; }
        public virtual ICollection<Diem> Diems { get; set; }
        public virtual ICollection<SinhVienLopHocPhan> SinhVienLopHocPhans { get; set; }
        public virtual ICollection<ThoiGianLopHocPhan> ThoiGianLopHocPhans { get; set; }
    }
}
