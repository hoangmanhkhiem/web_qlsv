using System;
using System.Collections.Generic;

namespace qlsv.Models
{
    public class ThoiGian
    {
        // Variables
        public string IdThoiGian { get; set; } = null!;
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }

        // Variables linked to another table
        public virtual ICollection<ThoiGianLopHocPhan> ThoiGianLopHocPhans { get; set; }
    
        // Constructor
        public ThoiGian()
        {
            ThoiGianLopHocPhans = new HashSet<ThoiGianLopHocPhan>();
        }
    }
}
