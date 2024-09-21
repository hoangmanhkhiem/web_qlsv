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

    public IActionResult Index()
    {
        // Retrieve the cookie named 'jwtToken'
        var jwtToken = Request.Cookies["jwtToken"];

        if (string.IsNullOrEmpty(jwtToken))
        {
           return RedirectToAction("Index", "Login", new { area = "Identity"});
        }
        // TODO code

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
