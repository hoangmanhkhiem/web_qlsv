using System.Diagnostics;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
//
using qlsv.Models;
using qlsv.ViewModels;
using qlsv.Helpers;
using qlsv.Data;
using Microsoft.VisualBasic;


namespace qlsv.Identity.Controllers;

[Area("Identity")]
public class LoginController : Controller
{
    // Variable
    private readonly ILogger<LoginController> _logger;
    private readonly qlsv.Data.IdentityDbContext _context;
    private readonly JwtHelper _jwtHelper;
    private readonly SecurityHelper _securityHelper;

    // Constructor
    public LoginController(
        ILogger<LoginController> logger,
        qlsv.Data.IdentityDbContext context,
        JwtHelper jwtHelper,
        SecurityHelper securityHelper
    ) {
        _logger = logger;
        _context = context;
        _jwtHelper = jwtHelper;
        _securityHelper = securityHelper;
    }

    /**
     * Get Login basic
     */
    public IActionResult Index()
    {
        return View();
    }

    /**
     * Post Login basic
     */
    [HttpPost]
    public IActionResult Index(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            string passwordHash = _securityHelper.Hash(model.Password);
            var user = _context.Users.FirstOrDefault(
                u => 
                    u.UserName.ToUpper() == model.UserNameOrEmail.ToUpper() ||
                        u.Email.ToUpper() == model.UserNameOrEmail.ToUpper()
            );
            if (user==null)
            {
                ModelState.AddModelError("UserNameOrEmail", "Tài khoản không tồn tại");
                return View();
            }
            if (user.PasswordHash != passwordHash)
            {
                ModelState.AddModelError("Password", "Mật khẩu không đúng");
                return View();
            }

            var token = _jwtHelper.GenerateToken(user.Id);
            Response.Cookies.Append("AccsessToken", token.AccessToken);
            Response.Cookies.Append("RefreshToken", token.RefreshToken);
            return RedirectToHomeWithRole(user.Id);
        }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    /**
     * Help Redriect to home with role user
     */
    private IActionResult RedirectToHomeWithRole(string IdUser)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == IdUser);
        ViewBag.UserInformation = user;

        string roleId = _context.UserRoles
                        .FirstOrDefault(ur => ur.UserId == IdUser)
                        .RoleId;
        string roleName = _context.Roles
                        .FirstOrDefault(r => r.Id == roleId)
                        .Name;

        if (roleName.ToUpper() == "SINHVIEN")
        {
            return RedirectToAction("Index", "Home", new { area = "SINHVIEN"});
        }
        if (roleName.ToUpper() == "GIAOVIEN")
        {
            return RedirectToAction("Index", "Home", new { area = "GIAOVIEN"});
        }
        if (roleName.ToUpper() == "ADMIN")
        {
            return RedirectToAction("Index", "Home", new { area = "Admin"});
        }

        return RedirectToAction("Index", "Home", new { area = ""});
    }
}
