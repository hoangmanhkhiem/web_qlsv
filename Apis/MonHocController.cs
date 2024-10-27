using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;

//
using qlsv.Helpers;
using qlsv.Data;
using qlsv.Models;

namespace qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MonHocController : ControllerBase
{
    // Variables
    private readonly qlsv.Data.IdentityDbContext _context;
    private readonly QuanLySinhVienDbContext _quanLySinhVienDbContext;

    // Constructor
    public MonHocController(
        qlsv.Data.IdentityDbContext context,
        QuanLySinhVienDbContext quanLySinhVienDbContext)
    {
        _context = context;
        _quanLySinhVienDbContext = quanLySinhVienDbContext;
    }

    // GET: Get data mon hoc for giao vien with id giao vien
    [HttpGet("giaovien/{IdGiaoVien}")]
    public async Task<IActionResult> GetMonHoc(string IdGiaoVien)
    {
        List<MonHoc> listMH = (
            from gv in _quanLySinhVienDbContext.GiaoViens
            where gv.IdGiaoVien == IdGiaoVien
            join khoa in _quanLySinhVienDbContext.Khoas on gv.IdKhoa equals khoa.IdKhoa
            join mh in _quanLySinhVienDbContext.MonHocs on khoa.IdKhoa equals mh.IdKhoa
            select mh
        ).ToList();

        var transformedList = listMH.Select(mh => new
        {
            IdMonHoc = mh.IdMonHoc,
            TenMonHoc = mh.TenMonHoc?.ToString(),
            SoTinChi = mh.SoTinChi,
            SoTiet = mh.SoTietHoc,
        }).ToList();

        // To Json
        string json = JsonSerializer.Serialize(transformedList);

        return Ok(json);
    }
}

