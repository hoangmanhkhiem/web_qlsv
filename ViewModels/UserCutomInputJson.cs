
using qlsv.Models;

namespace qlsv.ViewModels;

public class UserCutomInputJson : UserCustom {
    public string? ProfilePicture { get; set; } // Override base 64 to string
    // TODO: handle base64 to byte[] and byte[] to string base 64
}