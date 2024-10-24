using System;
using System.Collections.Generic;

namespace qlsv.Models
{
    public class SinhVien
    {
        // Variables
        public string IdSinhVien { get; set; } = null!;
        public string HoTen { get; set; } = null!;
        public string Lop { get; set; } = null!;
        public DateTime? NgaySinh { get; set; }
        public string? DiaChi { get; set; }
        public string? IdChuongTrinhHoc { get; set; }

        // Variables linked to another table    
        public virtual ChuongTrinhHoc? ChuongTrinhHocs { get; set; }
        public virtual ICollection<Diem> Diems { get; set; }
        public virtual ICollection<SinhVienLopHocPhan> SinhVienLopHocPhans { get; set; }

        // Constructor
        public SinhVien()
        {
            Diems = new HashSet<Diem>();
            SinhVienLopHocPhans = new HashSet<SinhVienLopHocPhan>();
        }
    }
}
