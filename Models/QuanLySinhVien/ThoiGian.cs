using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace qlsv.Models
{
    public class ThoiGian
    {
        // Variables
        public string IdThoiGian { get; set; } = null!;

        [DataType(DataType.DateTime)] 
        public DateTime NgayBatDau { get; set; }

        [DataType(DataType.DateTime)] 
        public DateTime NgayKetThuc { get; set; }

        public string? DiaDiem { get; set; }

        // Variables linked to another table
        public virtual ICollection<ThoiGianLopHocPhan> ThoiGianLopHocPhans { get; set; }
    
        // Constructor
        public ThoiGian()
        {
            ThoiGianLopHocPhans = new HashSet<ThoiGianLopHocPhan>();
        }
    }
}
