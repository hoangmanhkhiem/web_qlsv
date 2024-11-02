
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using qlsv.Helpers;

//
using qlsv.Models;
using qlsv.Data;
using Microsoft.EntityFrameworkCore;
namespace qlsv.Teacher.Controllers;

[Area("Teacher")]
public class DangKyNguyenVongController : Controller
{
    // Variable
    private readonly ILogger<DangKyNguyenVongController> _logger;
    private readonly JwtHelper _jwtHelper;
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public DangKyNguyenVongController(
        ILogger<DangKyNguyenVongController> logger,
        JwtHelper jwtHelper,
        QuanLySinhVienDbContext quanLySinhVienDbContext)
    {
        _logger = logger;
        _jwtHelper = jwtHelper;
        _context = quanLySinhVienDbContext;
    }

    /**
     * GET: /Teacher/DangKyNguyenVong 
     * Home Page
     */
    public async Task<IActionResult> Index() {
        // Get token from cookie
        var accessToken = HttpContext.Request.Cookies["AccsessToken"];
        if (accessToken == null)
        {
            return RedirectToAction("Index", "Login", new { area = "Identity" });
        }

        // Get iduser info from token
        var jwtToken = _jwtHelper.DecodeToken(accessToken);

        string idUser = jwtToken.Claims.FirstOrDefault(c => c.Type == "idClaim")?.Value;

        GiaoVien? gv = await _context.GiaoViens
            .FirstOrDefaultAsync(g => g.IdGiaoVien == idUser);

        if (gv == null)
        {
            return RedirectToAction("Index", "Login", new { area = "Identity"});
        }

        return View(gv);
    }

    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
