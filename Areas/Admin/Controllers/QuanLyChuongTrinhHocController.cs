
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
//
using qlsv.Models;
using qlsv.Data;

namespace qlsv.Admin.Controllers;

[Area("Admin")]
public class QuanLyChuongTrinhHocController : Controller
{
    // Variable
    private readonly ILogger<QuanLyChuongTrinhHocController> _logger;
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public QuanLyChuongTrinhHocController(
        ILogger<QuanLyChuongTrinhHocController> logger,
        QuanLySinhVienDbContext context) 
    {
        _logger = logger;
        _context = context;
    }

    /**
     * GET: /Admin/QuanLyChuongTrinhHoc
     * Home Page
     */
    public IActionResult Index()
    {
        return View();
    }

    // GET: /Admin/QuanLyChuongTrinhHoc/Details
    public IActionResult Details(string IdChuongTrinhHoc)
    {
        List<MonHoc> list = (
            from mh in _context.MonHocs
            join cth in _context.ChuongTrinhHocMonHocs on mh.IdMonHoc equals cth.IdMonHoc
            where cth.IdChuongTrinhHoc == IdChuongTrinhHoc
            select new MonHoc{
                TenMonHoc = mh.TenMonHoc,
                SoTinChi = mh.SoTinChi,
                SoTietHoc = mh.SoTietHoc,
                Khoas = mh.Khoas
            }
        ).ToList();

        return View(list);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
