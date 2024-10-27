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
public class NguyenVongController : ControllerBase
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public NguyenVongController(
        QuanLySinhVienDbContext quanLySinhVienDbContext)
    {
        _context = quanLySinhVienDbContext;
    }

    /**
     * GET: api/nguyenvong/
     * Get all nguyen vong 
     */
    [HttpGet]
    public async Task<IActionResult> GetNguyenVongs()
    {
        var nguyenVongs = (
            from nv in _context.DangKyNguyenVongs
            join sv in _context.SinhViens on nv.IdSinhVien equals sv.IdSinhVien
            join mh in _context.MonHocs on nv.IdMonHoc equals mh.IdMonHoc
            select new
            {
                IdDangKyNguyenVong = nv.IdDangKyNguyenVong,
                IdSinhVien = nv.IdSinhVien,
                IdMonHoc = mh.IdMonHoc,
                TenSinhVien = sv.HoTen,
                TenMonHoc = mh.TenMonHoc,
            }
        ).ToList();

        return Ok(nguyenVongs);
    }

    /**
     * GET: api/nguyenvong/{id}/
     * get nguyen vong by id
     */
    [HttpGet("{id}")]
    public async Task<IActionResult> GetNguyenVongById(string id)
    {
        var nguyenVongs = (
            from nv in _context.DangKyNguyenVongs
            where nv.IdDangKyNguyenVong == id
            join sv in _context.SinhViens on nv.IdSinhVien equals sv.IdSinhVien
            join mh in _context.MonHocs on nv.IdMonHoc equals mh.IdMonHoc
            select new
            {
                IdDangKyNguyenVong = nv.IdDangKyNguyenVong,
                IdSinhVien = nv.IdSinhVien,
                IdMonHoc = mh.IdMonHoc,
                TenSinhVien = sv.HoTen,
                TenMonHoc = mh.TenMonHoc,
            }
        ).ToList();

        return Ok(nguyenVongs);
    }

    /**
     * GET: api/nguyenvong/sinhvien/{id}/
     * get nguyen vong by sinh vien id
     */
    [HttpGet("sinhvien/{id}")]
    public async Task<IActionResult> GetNguyenVongBySinhVien(string id)
    {
        var nguyenVongs = (
            from nv in _context.DangKyNguyenVongs
            where nv.IdSinhVien == id
            join sv in _context.SinhViens on nv.IdSinhVien equals sv.IdSinhVien
            join mh in _context.MonHocs on nv.IdMonHoc equals mh.IdMonHoc
            select new
            {
                IdDangKyNguyenVong = nv.IdDangKyNguyenVong,
                IdSinhVien = nv.IdSinhVien,
                IdMonHoc = mh.IdMonHoc,
                TenSinhVien = sv.HoTen,
                TenMonHoc = mh.TenMonHoc,
            }
        ).ToList();

        return Ok(nguyenVongs);
    }
}

