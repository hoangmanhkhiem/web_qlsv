
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using qlsv.Helpers;

//
using qlsv.Models;

namespace qlsv.Teacher.Controllers;

[Area("Teacher")]
public class HomeController : Controller
{
    // Variable
    private readonly ILogger<HomeController> _logger;
    private readonly JwtHelper _jwtHelper;

    // Constructor
    public HomeController(
        ILogger<HomeController> logger,
        JwtHelper jwtHelper)
    {
        _logger = logger;
        _jwtHelper = jwtHelper;
    }

    /**
     * GET: /Teacher/Home 
     * Home Page
     */
    public IActionResult Index()
    {
        // Get token from cookie
        var accessToken = HttpContext.Request.Cookies["AccsessToken"];
        if (accessToken == null)
        {
            return RedirectToAction("Index", "Login", new { area = "Identity" });
        }

        // Get iduser info from token
        var jwtToken = _jwtHelper.DecodeToken(accessToken);

        string idUser = jwtToken.Claims.FirstOrDefault(c => c.Type == "idClaim")?.Value;

        ViewBag.idUser = idUser;

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
