using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using qlsv.Models;

namespace qlsv.Identity.Controllers;

[Area("Identity")]
public class RegisterController : Controller
{
    // Variable
    private readonly ILogger<RegisterController> _logger;

    // Constructor
    public RegisterController(ILogger<RegisterController> logger)
    {
        _logger = logger;
    }
    // TODO 
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
