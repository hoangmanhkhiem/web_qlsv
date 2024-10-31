using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

//
using qlsv.Data;
using qlsv.Models;

namespace qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiemController : ControllerBase
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;
    // Constructor
    public DiemController(
        QuanLySinhVienDbContext context)
    {
        _context = context;
    }

    /** 
     * GET: api/diem/
     * Get tat ca diem
     */
    [HttpGet]
    public async Task<IActionResult> GetDiems(){
        // Query
        var query = await (
            from d in _context.Diems
            select new
            {
                IdDiem = d.IdDiem,
                IdSinhVien = d.IdSinhVien,
                IdLopHocPhan = d.IdLopHocPhan,

                DiemQuaTrinh = d.DiemQuaTrinh,
                DiemKetThuc = d.DiemKetThuc,
                DiemTongKet = d.DiemTongKet,
                LanHoc = d.LanHoc,
            }
        ).ToListAsync();

        if (query == null || !query.Any()) // Ensure there's data
        {
            return NotFound("Không tìm thấy điểm");
        }

        // Directly return the JSON result
        return Ok(query);
    }

    /** 
     * GET: api/diem/{id}
     * Get diem tu id
     */
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDiemById(string id){
        // Query
        var query = await (
            from d in _context.Diems
            where d.IdDiem == id
            select new
            {
                IdDiem = d.IdDiem,
                IdSinhVien = d.IdSinhVien,
                IdLopHocPhan = d.IdLopHocPhan,

                DiemQuaTrinh = d.DiemQuaTrinh,
                DiemKetThuc = d.DiemKetThuc,
                DiemTongKet = d.DiemTongKet,
                LanHoc = d.LanHoc,
            }
        ).ToListAsync();

        if (query == null || !query.Any()) // Ensure there's data
        {
            return NotFound("Không tìm thấy điểm");
        }

        // Directly return the JSON result
        return Ok(query);
    }

    /**
     * GET: 
}

