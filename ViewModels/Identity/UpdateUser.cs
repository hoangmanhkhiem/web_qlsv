


using System.ComponentModel.DataAnnotations;

namespace qlsv.ViewModels;

public class UpdateUser {
    // Variables
    [Required]
    public string Id { get; set; }    
    
    [Required(ErrorMessage = "Không được để trống")]
    [RegularExpression(@"^[a-zA-ZàáảãạăằắẳẵặâầấẩẫậèéẻẽẹêềếểễệìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵđÀÁẢÃẠĂẰẮẲẴẶÂẦẤẨẪẬÈÉẺẼẸÊỀẾỂỄỆÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴĐ\s]*$"
        , ErrorMessage = "Tên không hợp lệ")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Không được để trống")]
    [RegularExpression(@"^[a-zA-ZàáảãạăằắẳẵặâầấẩẫậèéẻẽẹêềếểễệìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵđÀÁẢÃẠĂẰẮẲẴẶÂẦẤẨẪẬÈÉẺẼẸÊỀẾỂỄỆÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴĐ\s]*$"
        , ErrorMessage = "Tên không hợp lệ")]
    public string? LastName { get; set; }
    
    [Required(ErrorMessage = "Email không được để trống")]
    [EmailAddress(ErrorMessage = "Vui lòng nhập địa chỉ email hợp lệ")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Email không hợp lệ")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Số điện thoại không được để trống")]
    [RegularExpression(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$", ErrorMessage = "Số điện thoại không hợp lệ")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Địa chỉ không được để trống")]
    [DataType(DataType.MultilineText, ErrorMessage = "Địa chỉ không hợp lệ")]
    public string? Address { get; set; }

    public byte[]? ProfilePicture { get; set; }
}