using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using qlsv.Models;

namespace qlsv.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // public IActionResult Index()
    // {
    //     // Retrieve the cookie named 'jwtToken'
    //     var jwtAccsessToken = Request.Cookies["AccsessToken"];
    //     var jwtRefreshToken = Request.Cookies["RefreshToken"];

    //     if (string.IsNullOrEmpty(jwtAccsessToken))
    //     {
    //        return RedirectToAction("Index", "Login", new { area = "Identity"});
    //     }
    //     // TODO code

    //     return View();
    // }

    // public IActionResult Privacy()
    // {
    //     return View();
    // }

    // // TODO - rm - Test viewcomponent calendar
    // public IActionResult Calendar()
    // {
    //     return View();
    // }

    // public IActionResult DataTableEdit()
    // {
    //     return View();
    // }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
