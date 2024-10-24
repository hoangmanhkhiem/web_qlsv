using System;
using System.Collections.Generic;

namespace qlsv.Models
{
    public class GiaoVien
    {
        // Variables
        public string IdGiaoVien { get; set; } = null!;
        public string TenGiaoVien { get; set; } = null!;
        public string? Email { get; set; }
        public string? SoDienThoai { get; set; }
        public string? IdKhoa { get; set; }

        // Variables linked to another table
        public virtual Khoa? IdKhoaNavigation { get; set; }
        public virtual ICollection<LopHocPhan> LopHocPhans { get; set; }

        // Constructor
        public GiaoVien()
        {
            LopHocPhans = new HashSet<LopHocPhan>();
        }
    }
}
