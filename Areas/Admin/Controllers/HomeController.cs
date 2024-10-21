
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
//
using qlsv.Models;

namespace qlsv.Identity.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
    // Variable
    private readonly ILogger<HomeController> _logger;
    

    // Constructor
    public HomeController(
        ILogger<HomeController> logger
    ) {
        _logger = logger;
    }

    /**
     * GET: /Admin/Home 
     * Home Page
     */
    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
