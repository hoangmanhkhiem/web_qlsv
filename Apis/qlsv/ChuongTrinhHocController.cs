using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;

//
using qlsv.Data;
using qlsv.Models;
using Microsoft.EntityFrameworkCore;

namespace qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChuongTrinhHocController : ControllerBase
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;
    // Constructor
    public ChuongTrinhHocController(
        QuanLySinhVienDbContext context)
    {
        _context = context;
    }

    /** 
     * GET: api/chuongtrinhhoc/
     * Get tat ca chuong trinh hoc
     */
    [HttpGet]
    public async Task<IActionResult> GetChuongTrinhHocs()
    {
        // Query
        var query = await (
            from chh in _context.ChuongTrinhHocs
            select new {
                IdChuongTrinhHoc = chh.IdChuongTrinhHoc,
                TenChuongTrinhHoc = chh.TenChuongTrinhHoc
            }
        ).ToListAsync();

        if (query == null || !query.Any()) // Ensure there's data
        {
            return NotFound("Không tìm thấy chương trình học");
        }

        // Directly return the JSON result
        return Ok(query);
    }

    /** 
     * GET: api/chuongtrinhhoc/sinhvien/{id}
     * Get chuong trinh hoc cua sinh vien tu id
     */
    [HttpGet("sinhvien/{id}")]
    public async Task<IActionResult> GetchuongtrinhhocBySinhVien(string id)
    {
        // Query
        var query = await (
            from sv in _context.SinhViens
            where sv.IdSinhVien == id
            join cch in _context.ChuongTrinhHocs on sv.IdChuongTrinhHoc equals cch.IdChuongTrinhHoc 
            select new {
                IdChuongTrinhHoc = cch.IdChuongTrinhHoc,
                TenChuongTrinhHoc = cch.TenChuongTrinhHoc
            }
        ).ToListAsync();

        if (query == null || !query.Any()) // Ensure there's data
        {
            return NotFound("Không tìm thấy chương trình học");
        }

        // Directly return the JSON result
        return Ok(query);
    }

}

