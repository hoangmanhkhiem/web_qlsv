using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
//
using qlsv.Models;
using qlsv.ViewModels;
using qlsv.Data;
using qlsv.Helpers;

namespace qlsv.Identity.Controllers;

[Area("Identity")]
public class RegisterController : Controller
{
    // Variable
    private readonly ILogger<RegisterController> _logger;
    private readonly IdentityDbContext _context;
    private readonly SecurityHelper _securityHelper;

    // Constructor
    public RegisterController(
        ILogger<RegisterController> logger,
        IdentityDbContext context,
        SecurityHelper securityHelper)
    {
        _logger = logger;
        _context = context;
        _securityHelper = securityHelper;
    }
    // TODO 
    public IActionResult Index()
    {
        return View();
    }

    /**
     * Register - Basic 
     */
    [HttpPost]
    public ActionResult Index(RegisterViewModel registerViewModel) {
        if (!ModelState.IsValid) {
            return View();
        }
        // Check if user name already exists
        var user = _context.Users.FirstOrDefault(u => u.UserName == registerViewModel.UserName);
        if (user != null) {
            ModelState.AddModelError("UserName", "User name already exists");
            return View();
        }

        // Check if email already exists
        user = _context.Users.FirstOrDefault(u => u.Email == registerViewModel.Email);
        if (user != null) {
            ModelState.AddModelError("Email", "Email already exists");
            return View();
        }

        // Create new user
        var newUser = new UserCustom {
            UserName = registerViewModel.UserName,
            Email = registerViewModel.Email,
            PasswordHash = _securityHelper.Hash(registerViewModel.Password)
        };
        _context.Users.Add(newUser);
        _context.SaveChanges();
        return RedirectToAction("Index", "Login", new { area = "Identity" });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
