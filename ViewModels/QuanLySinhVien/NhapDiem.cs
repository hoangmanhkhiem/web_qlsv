using System.ComponentModel.DataAnnotations;

namespace qlsv.ViewModels;

public class NhapDiem
{
    // Variables
    [Required]
    public string? IdDiem { get; set; }

    [Required]
    [RegularExpression(@"^(10(\.0{1,2})?|[0-9](\.[0-9]{1,2})?)$", ErrorMessage = "Chỉ được nhập số từ 0 đến 10, với tối đa 2 chữ số thập phân")]
    public string? DiemQuaTrinh { get; set; }

    [Required]
    [RegularExpression(@"^(10(\.0{1,2})?|[0-9](\.[0-9]{1,2})?)$", ErrorMessage = "Chỉ được nhập số từ 0 đến 10, với tối đa 2 chữ số thập phân")]
    public string? DiemKetThuc { get; set; }

    [Required]
    [RegularExpression(@"^(10(\.0{1,2})?|[0-9](\.[0-9]{1,2})?)$", ErrorMessage = "Chỉ được nhập số từ 0 đến 10, với tối đa 2 chữ số thập phân")]
    public string? DiemTongKet { get; set; }
}