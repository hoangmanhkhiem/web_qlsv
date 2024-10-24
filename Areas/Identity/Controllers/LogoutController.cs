using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using qlsv.Models;

namespace qlsv.Identity.Controllers;

[Area("Identity")]
public class LogoutController : Controller
{
    // Variable
    private readonly ILogger<LogoutController> _logger;

    // Constructor
    public LogoutController(ILogger<LogoutController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult Index()
    {   
        // Remove Token
        HttpContext.Response.Cookies.Delete("AccsessToken");
        HttpContext.Response.Cookies.Delete("RefreshToken");

        return RedirectToAction("Index", "Login", new {  Area = "Identity" });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
