
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//
using qlsv.Models;
using qlsv.Data;
using qlsv.Dto;

namespace qlsv.Admin.Controllers;

[Area("Admin")]
public class QuanLyLopHocPhanController : Controller
{
    // Variable
    private readonly ILogger<QuanLyLopHocPhanController> _logger;
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public QuanLyLopHocPhanController(
        ILogger<QuanLyLopHocPhanController> logger,
        QuanLySinhVienDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    /**
     * GET: /Admin/QuanLyLopHocPhan
     * Home Page
     */
    public IActionResult Index()
    {
        return View();
    }

    // GET: /Admin/QuanLyLopHocPhan/Details?IdLopHocPhan={id}
    public IActionResult Details(string IdLopHocPhan)
    {  
        
        var lhp = _context.LopHocPhans
            .Include(x => x.MonHocs)
            .Include(x => x.GiaoViens)
            .FirstOrDefault(x => x.IdLopHocPhan == IdLopHocPhan);

        return View(lhp);
    }


    private StatusUploadFileDto SinhVienExists(LopHocPhanDto _lopHocPhan)
    {
        // Check id
        var id = _lopHocPhan.IdLopHocPhan;
        var lopHp = _context.LopHocPhans.FirstOrDefault(x => x.IdLopHocPhan == id);
        if (lopHp != null)
        {
            return new StatusUploadFileDto
            {
                Status = false,
                Message = "Id already exists"
            };
        }
        var tenLopHP = _lopHocPhan.TenLopHocPhan;
        var tenLopHp = _context.LopHocPhans.FirstOrDefault(x => x.TenHocPhan == tenLopHP);
        if (tenLopHp != null)
        {
            return new StatusUploadFileDto
            {
                Status = false,
                Message = "Ten lop hoc phan already exists"
            };
        }

        

        
        return new StatusUploadFileDto
        {
            Status = true,
            Message = "Success"
        };
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
