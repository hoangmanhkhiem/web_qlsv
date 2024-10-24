using System;
using System.Collections.Generic;

namespace qlsv.Models
{
    public class ChuongTrinhHocMonHoc
    {
        // Variables
        public string IdCthmonHoc { get; set; } = null!;
        public string? IdChuongTrinhHoc { get; set; }
        public string? IdMonHoc { get; set; }
        
        // Variables linked to another table
        public virtual ChuongTrinhHoc? ChuongTrinhHocs { get; set; }
        public virtual MonHoc? MonHocs { get; set; }
    }
}
