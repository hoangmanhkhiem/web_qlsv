using System;
using System.Collections.Generic;

namespace qlsv.Models
{
    public class ThoiGianLopHocPhan
    {
        public string IdThoiGianLopHocPhan { get; set; } = null!;
        public string? IdLopHocPhan { get; set; }
        public string? IdThoiGian { get; set; }

        public virtual LopHocPhan? IdLopHocPhanNavigation { get; set; }
        public virtual ThoiGian? IdThoiGianNavigation { get; set; }
    }
}
