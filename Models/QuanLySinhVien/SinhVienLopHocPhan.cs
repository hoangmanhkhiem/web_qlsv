using System;
using System.Collections.Generic;

namespace qlsv.Models
{
    public class SinhVienLopHocPhan
    {
        // Variables
        public string IdSinhVienLopHp { get; set; } = null!;
        public string? IdSinhVien { get; set; }
        public string? IdLopHocPhan { get; set; }

        // Variables linked to another table
        public virtual LopHocPhan? IdLopHocPhanNavigation { get; set; }
        public virtual SinhVien? IdSinhVienNavigation { get; set; }
    }
}
