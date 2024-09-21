
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace qlsv.ViewModels;

public class LoginViewModel {
    [Required]
    public string UserNameOrEmail { get; set; }

    [Required]
    public string Password { get; set; }
}