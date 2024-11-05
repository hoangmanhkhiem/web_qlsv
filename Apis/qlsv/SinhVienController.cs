using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;

//
using qlsv.Helpers;
using qlsv.Data;
using qlsv.Models;
using Microsoft.EntityFrameworkCore;
using qlsv.Dto;

namespace qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SinhVienController : ControllerBase
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;
    private readonly IdentityDbContext _identityContext;
    private readonly SecurityHelper _securityHelper;

    // Constructor
    public SinhVienController(
        QuanLySinhVienDbContext quanLySinhVienDbContext,
        IdentityDbContext identityDbContext,
        SecurityHelper securityHelper)
    {
        _context = quanLySinhVienDbContext;
        _identityContext = identityDbContext;
        _securityHelper = securityHelper;
    }

    /**
     * GET: api/sinhvien/
     * get all sinh vien
     */
    [HttpGet]
    public async Task<IActionResult> GetSinhViens()
    {
        var sinhViens = (
            from sv in _context.SinhViens
            join khoa in _context.Khoas on sv.IdKhoa equals khoa.IdKhoa
            join cch in _context.ChuongTrinhHocs on sv.IdChuongTrinhHoc equals cch.IdChuongTrinhHoc
            select new
            {
                // List id
                IdSinhVien = sv.IdSinhVien,
                IdKhoa = sv.IdKhoa,
                IdChuongTrinhHoc = sv.IdChuongTrinhHoc,
                // list value
                TenSinhVien = sv.HoTen,
                Lop = sv.Lop,
                NgaySinh = sv.NgaySinh.HasValue ? sv.NgaySinh.Value.ToString("dd/MM/yyyy") : null,
                DiaChi = sv.DiaChi,
                TenKhoa = khoa.TenKhoa,
                TenChuongTrinhHoc = cch.TenChuongTrinhHoc,
            }
        ).ToList();
        return Ok(sinhViens);
    }

    /**
     * GET: api/sinhvien/{id}/
     * get sinh vien by id
     */
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSinhVienById(string id)
    {
        var sinhVien = (
            from sv in _context.SinhViens
            where sv.IdSinhVien == id
            join khoa in _context.Khoas on sv.IdKhoa equals khoa.IdKhoa
            join cch in _context.ChuongTrinhHocs on sv.IdChuongTrinhHoc equals cch.IdChuongTrinhHoc
            select new
            {
                // List id
                IdSinhVien = sv.IdSinhVien,
                IdKhoa = sv.IdKhoa,
                IdChuongTrinhHoc = sv.IdChuongTrinhHoc,
                // list value
                TenSinhVien = sv.HoTen,
                Lop = sv.Lop,
                NgaySinh = sv.NgaySinh.HasValue ? sv.NgaySinh.Value.ToString("dd/MM/yyyy") : null,
                DiaChi = sv.DiaChi,
                TenKhoa = khoa.TenKhoa,
                TenChuongTrinhHoc = cch.TenChuongTrinhHoc,
            }
        ).FirstOrDefault();
        return Ok(sinhVien);
    }

    /**
     * PUT: api/sinhvien/{id}/
     * sua thong tin sinh vien
     */
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSinhVien(string id, [FromBody] SinhVienDto sinhVien)
    {
        // Find the existing sinh vien
        var existingSinhVien = await _context.SinhViens.FindAsync(id);
        if (existingSinhVien == null)
        {
            return NotFound("Không tìm thấy sinh viên");
        }
        // Update the existing sinh vien
        existingSinhVien.HoTen = sinhVien.HoTen;
        existingSinhVien.Lop = sinhVien.Lop;
        existingSinhVien.NgaySinh = sinhVien.NgaySinh;
        existingSinhVien.DiaChi = sinhVien.DiaChi;
        // Save the changes
        await _context.SaveChangesAsync();
        return Ok(new
        {
            statusCode = 200,
            message = "Cập nhật sinh viên thành công",
            data = new
            {
                IdSinhVien = existingSinhVien.IdSinhVien,
                TenSinhVien = existingSinhVien.HoTen,
                Lop = existingSinhVien.Lop,
                NgaySinh = existingSinhVien.NgaySinh.HasValue ? existingSinhVien.NgaySinh.Value.ToString("dd/MM/yyyy") : null,
                DiaChi = existingSinhVien.DiaChi,
            }
        });
    }

    /**
     * POST: api/sinhvien/
     * them sinh vien
     */
    [HttpPost]
    public async Task<IActionResult> CreateSinhVien([FromBody] SinhVien sinhVien)
    {
        _context.SinhViens.Add(sinhVien);
        await _context.SaveChangesAsync();
        return Ok("Thêm sinh viên thành công");
    }

    // DELETE: api/sinhvien/{id}
    // delete sinh vien by id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSinhVien(string id)
    {
        var sinhVien = await _context.SinhViens.FindAsync(id);
        if (sinhVien == null)
        {
            return NotFound("Không tìm thấy sinh viên");
        }
        _context.SinhViens.Remove(sinhVien);
        await _context.SaveChangesAsync();
        return Ok("Xóa sinh viên thành công");
    }

    /**
     * GET: api/sinhvien/{id}/lophocphan
     * GET Sinh Vien Thuoc Lop Hoc Phan
     */
    [HttpGet("{id}/lophocphan")]
    public async Task<IActionResult> GetSinhVienThuocLopHocPhan(string id)
    {
        var qr = await (
            from lhp in _context.LopHocPhans
            join svlhp in _context.SinhVienLopHocPhans on lhp.IdLopHocPhan equals svlhp.IdLopHocPhan
            join sv in _context.SinhViens on svlhp.IdSinhVien equals sv.IdSinhVien
            join k in _context.Khoas on sv.IdKhoa equals k.IdKhoa
            join cth in _context.ChuongTrinhHocs on sv.IdChuongTrinhHoc equals cth.IdChuongTrinhHoc
            where lhp.IdLopHocPhan == id
            select new
            {
                IdSinhVien = sv.IdSinhVien,
                IdKhoa = sv.IdKhoa,
                IdChuongTrinhHoc = sv.IdChuongTrinhHoc,
                TenSinhVien = sv.HoTen,
                Lop = sv.Lop,
                NgaySinh = sv.NgaySinh.HasValue ? sv.NgaySinh.Value.ToString("dd/MM/yyyy") : null,
                DiaChi = sv.DiaChi,
                TenKhoa = k.TenKhoa,
                TenChuongTrinhHoc = cth.TenChuongTrinhHoc
            }
        ).ToListAsync();


        return Ok(qr);
    }

    // POST: api/sinhvien/adminupdatepassword
    [HttpPost("adminupdatepassword")]
    public IActionResult UpdatePassword([FromBody] AdminUpdatePasswordDto updatePasswordDto)
    {

        if (string.IsNullOrWhiteSpace(updatePasswordDto.NewPassword) ||
            updatePasswordDto.NewPassword != updatePasswordDto.ConfirmPassword ||
            updatePasswordDto.IdUser == null)
        {
            return BadRequest("Invalid data.");
        }

        try
        {
            // Find the user in the Identity system by teacher ID
            var user = _identityContext.Users.FirstOrDefault(u => u.IdClaim == updatePasswordDto.IdUser);
            if (user == null)
            {
                return NotFound("Giáo viên không tồn tại.");
            }

            // Hash the new password and update the user's PasswordHash field
            user.PasswordHash = _securityHelper.Hash(updatePasswordDto.NewPassword);

            // Save changes to the Identity database
            _identityContext.Users.Update(user);
            _identityContext.SaveChanges();

            return Ok("Password updated successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    // POST: api/sinhvien/updatepassword
    [HttpPost("updatepassword")]
    public IActionResult UpdatePassword([FromBody] UpdatePasswordDto updatePasswordDto)
    {
        if (string.IsNullOrWhiteSpace(updatePasswordDto.NewPassword) ||
        string.IsNullOrWhiteSpace(updatePasswordDto.OldPassword) ||
        updatePasswordDto.NewPassword != updatePasswordDto.ConfirmPassword ||
        updatePasswordDto.IdUser == null)
        {
            return BadRequest("Invalid data.");
        }

        try
        {
            // Find the user in the Identity system by teacher ID
            var user = _identityContext.Users.FirstOrDefault(u => u.Id == updatePasswordDto.IdUser);
            if (user == null)
            {
                return NotFound("Sinh viên không tồn tại.");
            }

            // Verify the old password
            var passwordHasher = user.PasswordHash;
            bool verificationResult = _securityHelper.ValidateHash(updatePasswordDto.OldPassword, passwordHasher);

            if (verificationResult == false)
            {
                return Unauthorized("Mật khẩu cũ không chính xác.");
            }

            // Hash the new password and update the user's PasswordHash field
            user.PasswordHash = _securityHelper.Hash(updatePasswordDto.NewPassword);

            // Save changes to the Identity database
            _identityContext.Users.Update(user);
            _identityContext.SaveChanges();

            return Ok("Password updated successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

}

