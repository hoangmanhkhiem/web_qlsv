


using System.ComponentModel.DataAnnotations;

namespace qlsv.ViewModels;

public class UpdateUser {
    // Variables
    [Required]
    public string Id { get; set; }    
    
    [Required(ErrorMessage = "Không được để trống")]
    [RegularExpression(@"^[a-zA-ZàáảãạăằắẳẵặâầấẩẫậèéẻẽẹêềếểễệìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵđÀÁẢÃẠĂẰẮẲẴẶÂẦẤẨẪẬÈÉẺẼẸÊỀẾỂỄỆÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴĐ\s]*$"
        , ErrorMessage = "Tên không hợp lệ")]
    public string? FullName { get; set; }

    [Required(ErrorMessage = "Không được để trống")]
    public string? Lop { get; set; }

    [Required(ErrorMessage = "Ngày sinh không được để trống")]
    [DataType(DataType.Date, ErrorMessage = "Ngày sinh không hợp lệ")]
    public DateTime? NgaySinh { get; set; }
    
    [Required(ErrorMessage = "Địa chỉ không được để trống")]
    [DataType(DataType.MultilineText, ErrorMessage = "Địa chỉ không hợp lệ")]
    public string? Address { get; set; }

    public byte[]? ProfilePicture { get; set; }
}