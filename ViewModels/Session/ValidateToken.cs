
namespace qlsv.ViewModels;

public class ValidateToken
{
    public bool IsValid { get; set; }   // Trạng thái token có hợp lệ hay không
    public string Message { get; set; } // Thông báo chi tiết cho kết quả kiểm tra
    public int Status { get; set; }     // Mã trạng thái hoặc mã lỗi (ví dụ: 200, 401, 403)
}
