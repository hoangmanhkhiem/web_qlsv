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
public class GiaoVienController : ControllerBase
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;
    // Constructor
    public GiaoVienController(
        QuanLySinhVienDbContext context)
    {
        _context = context;
    }

    /** 
     * GET: api/giaovien/
     * Get tat ca giao vien
     */
    [HttpGet]
    public async Task<IActionResult> GetGiaoViens()
    {
        // Query
        var query = await (
            from gv in _context.GiaoViens
            join k in _context.Khoas on gv.IdKhoa equals k.IdKhoa
            select new {
                IdGiaoVien = gv.IdGiaoVien,
                TenGiaoVien = gv.TenGiaoVien,
                Email = gv.Email,
                SoDienThoai = gv.SoDienThoai,
                IdKhoa = gv.IdKhoa,
                TenKhoa = k.TenKhoa
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
     * GET: api/giaovien/{id}
     * Get giao vien tu id
     */
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGiaoVienById(string id)
    {
        // Query
        var query = await (
            from gv in _context.GiaoViens
            where gv.IdGiaoVien == id
            join k in _context.Khoas on gv.IdKhoa equals k.IdKhoa
            select new {
                IdGiaoVien = gv.IdGiaoVien,
                TenGiaoVien = gv.TenGiaoVien,
                Email = gv.Email,
                SoDienThoai = gv.SoDienThoai,
                IdKhoa = gv.IdKhoa,
                TenKhoa = k.TenKhoa
            }
        ).ToListAsync();

        if (query == null || !query.Any()) // Ensure there's data
        {
            return NotFound("Không tìm thấy chương trình học");
        }

        // Directly return the JSON result
        return Ok(query);
    }

    // POST: api/giaovien/
    [HttpPost]
    public async Task<IActionResult> CreateGiaoVien([FromBody] GiaoVien giaoVien)
    {
        // Add to database
        _context.GiaoViens.Add(giaoVien);
        await _context.SaveChangesAsync();

        // Return the result
        return Ok(giaoVien);
    }

    // PUT /api/giaovien/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGiaoVien(string id, [FromBody] GiaoVien giaoVien)
    {
        // Find the existing giao vien
        var existingGiaoVien = await _context.GiaoViens.FindAsync(id);
        if (existingGiaoVien == null)
        {
            return NotFound("Không tìm thấy giáo viên");
        }

        // Update the existing giao vien
        existingGiaoVien.TenGiaoVien = giaoVien.TenGiaoVien;
        existingGiaoVien.Email = giaoVien.Email;
        existingGiaoVien.SoDienThoai = giaoVien.SoDienThoai;
        existingGiaoVien.IdKhoa = giaoVien.IdKhoa;

        // Save changes
        await _context.SaveChangesAsync();

        // Return the updated giao vien
        return Ok(existingGiaoVien);
    }
}

