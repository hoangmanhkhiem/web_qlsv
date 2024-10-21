
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace qlsv.ViewModels;

public class RegisterViewModel {
    [Required]
    [RegularExpression(@"^[A-Za-z][A-Za-z0-9_]{7,29}$")]
    public string UserName { get; set; }

    [Required]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",
        ErrorMessage = "Email must be a valid email address.")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", 
        ErrorMessage = "Password must be at least 8 characters and contain at least one uppercase letter, one lowercase letter, and one number.")]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}