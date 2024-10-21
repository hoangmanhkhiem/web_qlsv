
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace qlsv.ViewModels;

public class RegisterViewModel {
    [Required]
    [RegularExpression(@"^[a-zA-Z0-9._]{3,16}$",
        ErrorMessage = "Username must be 3-16 characters and contain only letters, numbers, periods, and underscores.")]
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