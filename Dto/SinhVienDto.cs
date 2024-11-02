
using System.ComponentModel.DataAnnotations;

namespace qlsv.Dto
{
    public class SinhVienDto
    {

        [Required]
        public string? IdSinhVien { get; set; }
        
        [Required(ErrorMessage = "Không được để trống")]
        public string? IdKhoa { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public string? IdChuongTrinhHoc { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [RegularExpression(@"^[a-zA-ZàáảãạăằắẳẵặâầấẩẫậèéẻẽẹêềếểễệìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵđÀÁẢÃẠĂẰẮẲẴẶÂẦẤẨẪẬÈÉẺẼẸÊỀẾỂỄỆÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴĐ\s]*$"
            , ErrorMessage = "Tên không hợp lệ")]
        public string? HoTen { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public string? Lop { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public string? DiaChi { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public string? TenKhoa { get; set; }

        [Required(ErrorMessage = "Không được để trống")]

        public string? IdSinhVien { get; set; }
        public string? IdKhoa { get; set; }
        public string? IdChuongTrinhHoc { get; set; }
        //
        public string? HoTen { get; set; }
        public string? Lop { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? DiaChi { get; set; }
        public string? TenKhoa { get; set; }

        public string? TenChuongTrinhHoc { get; set; }
    }
}
