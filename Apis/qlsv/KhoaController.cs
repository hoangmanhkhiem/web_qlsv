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
public class KhoaController : ControllerBase
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public KhoaController(
        QuanLySinhVienDbContext quanLySinhVienDbContext)
    {
        _context = quanLySinhVienDbContext;
    }
    
    /**
     * GET: api/khoa
     * Get tat ca khoa
     */
    [HttpGet]
    public async Task<IActionResult> GetKhoas()
    {
        var khoas = (
            from k in _context.Khoas
            select new {
                IdKhoa = k.IdKhoa,
                TenKhoa = k.TenKhoa
            }
        ).ToList();

        if (khoas.Count == 0)
        {
            return NotFound("Không có khoa nào");
        }

        return Ok(khoas);
    }

    /**
     * GET: api/khoa/{id}
     * Get khoa by id
     */
    [HttpGet("{id}")]
    public async Task<IActionResult> GetKhoaById(string id)
    {
        var khoa = (
            from k in _context.Khoas
            where k.IdKhoa == id
            select new {
                IdKhoa = k.IdKhoa,
                TenKhoa = k.TenKhoa
            }
        ).FirstOrDefault();
    
        if (khoa == null)
        {
            return NotFound("Không tìm thấy khoa");
        }

        return Ok(khoa);
    }

    /**
     * POST: api/khoa/
     * Them khoa
     */
    [HttpPost]
    public async Task<IActionResult> CreateKhoa([FromBody] string tenKhoa)
    {
        var khoa = new Khoa
        {
            IdKhoa = Guid.NewGuid().ToString(),
            TenKhoa = tenKhoa
        };

        _context.Khoas.Add(khoa);
        _context.SaveChanges();

        return Ok("Thêm khoa thành công");
    }

    /**
     * PUT: api/khoa/{id}/
     * Sua ten khoa
     */
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateKhoa(string id, [FromBody] string tenKhoa)
    {
        var khoa = (
            from k in _context.Khoas
            where k.IdKhoa == id
            select k
        ).FirstOrDefault();

        if (khoa == null)
        {
            return NotFound("Không tìm thấy khoa");
        }

        khoa.TenKhoa = tenKhoa;
        _context.SaveChanges();

        return Ok("Sửa khoa thành công");
    }

    /**
     * DELETE: api/khoa/{id}
     * Xoa khoa
     */
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteKhoa(string id)
    {
        var khoa = (
            from k in _context.Khoas
            where k.IdKhoa == id
            select k
        ).FirstOrDefault();

        if (khoa == null)
        {
            return NotFound("Không tìm thấy khoa");
        }

        _context.Khoas.Remove(khoa);
        _context.SaveChanges();

        return Ok("Xóa khoa thành công");
    }
}

