using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlsv.Data;
using qlsv.Dto;
using qlsv.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace qlsv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguyenVongThayDoiLichController : ControllerBase
    {
        private readonly QuanLySinhVienDbContext _context;

        public NguyenVongThayDoiLichController(QuanLySinhVienDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNguyenVong()
        {
            var requests = await _context.DangKyDoiLichs.ToListAsync();
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNguyenVongById(string id)
        {
            var request = await _context.DangKyDoiLichs
                .FirstOrDefaultAsync(nv => nv.IdDangKyDoiLich == id);

            return request == null ? NotFound("Nguyện Vọng Không Tìm Thấy") : Ok(request);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNguyenVong([FromBody] NguyenVongThayDoiLichDto nguyenVong)
        {
            if (await CheckIfAlreadyRegistered(nguyenVong.IdThoiGian))
                return BadRequest("Nguyện Vọng Đã Được Đăng Ký !!!");

            if (!IsValidTimeRange(nguyenVong.ThoiGianBatDauHienTai, nguyenVong.ThoiGianKetThucHienTai) ||
                !IsValidTimeRange(nguyenVong.ThoiGianBatDauMoi, nguyenVong.ThoiGianKetThucMoi))
                return BadRequest("Thời Gian Bắt Đầu Lớn Hơn Thời Gian Kết Thúc");

            if (nguyenVong.ThoiGianBatDauMoi < DateTime.Now)
                return BadRequest("Thời gian truyền vào ở quá khứ");

            if (await CheckIfTimeOverlap(nguyenVong.IdThoiGian, nguyenVong.ThoiGianBatDauMoi, nguyenVong.ThoiGianKetThucMoi))
                return BadRequest("Thời gian bị trùng lịch hiện tại");

            var newRequest = new DangKyDoiLich
            {
                IdDangKyDoiLich = Guid.NewGuid().ToString(),
                IdThoiGian = nguyenVong.IdThoiGian,
                ThoiGianBatDauHienTai = nguyenVong.ThoiGianBatDauHienTai,
                ThoiGianKetThucHienTai = nguyenVong.ThoiGianKetThucHienTai,
                ThoiGianBatDauMoi = nguyenVong.ThoiGianBatDauMoi,
                ThoiGianKetThucMoi = nguyenVong.ThoiGianKetThucMoi,
                TrangThai = -1
            };

            _context.DangKyDoiLichs.Add(newRequest);
            await _context.SaveChangesAsync();

            return Ok(new { statusCode = 200, message = "Tạo Nguyện Vọng Thành Công" });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNguyenVong([FromBody] NguyenVongThayDoiLichDto nguyenVong)
        {
            var existingRequest = await _context.DangKyDoiLichs
                .FirstOrDefaultAsync(nv => nv.IdDangKyDoiLich == nguyenVong.IdDangKyDoiLich);

            if (existingRequest == null)
                return NotFound("Không Tìm Thấy Nguyện Vọng");

            if (!IsValidTimeRange(nguyenVong.ThoiGianBatDauHienTai, nguyenVong.ThoiGianKetThucHienTai) ||
                !IsValidTimeRange(nguyenVong.ThoiGianBatDauMoi, nguyenVong.ThoiGianKetThucMoi))
                return BadRequest("Thời Gian Bắt Đầu Lớn Hơn Thời Gian Kết Thúc");

            if (nguyenVong.ThoiGianBatDauMoi < DateTime.Now)
                return BadRequest("Thời gian truyền vào ở quá khứ");

            if (await CheckIfTimeOverlap(nguyenVong.IdThoiGian, nguyenVong.ThoiGianBatDauMoi, nguyenVong.ThoiGianKetThucMoi))
                return BadRequest("Thời gian bị trùng lịch hiện tại");

            existingRequest.ThoiGianBatDauHienTai = nguyenVong.ThoiGianBatDauHienTai;
            existingRequest.ThoiGianKetThucHienTai = nguyenVong.ThoiGianKetThucHienTai;
            existingRequest.ThoiGianBatDauMoi = nguyenVong.ThoiGianBatDauMoi;
            existingRequest.ThoiGianKetThucMoi = nguyenVong.ThoiGianKetThucMoi;

            await _context.SaveChangesAsync();

            return Ok("Cập Nhật Nguyện Vọng Thành Công");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNguyenVong(string id)
        {
            var request = await _context.DangKyDoiLichs
                .FirstOrDefaultAsync(nv => nv.IdDangKyDoiLich == id);

            if (request == null)
                return NotFound("Không Tìm Thấy Nguyện Vọng");

            _context.DangKyDoiLichs.Remove(request);
            await _context.SaveChangesAsync();

            return Ok("Xoá Nguyện Vọng Thành Công");
        }

        // Helper method to check if already registered
        private async Task<bool> CheckIfAlreadyRegistered(string idThoiGian) =>
            await _context.DangKyDoiLichs.AnyAsync(t => t.IdThoiGian == idThoiGian);

        // Helper method to validate time range
        private bool IsValidTimeRange(DateTime start, DateTime end) => start < end;

        // Helper method to check for overlapping times
        private async Task<bool> CheckIfTimeOverlap(string idThoiGian, DateTime start, DateTime end)
        {
            var lhp = await _context.ThoiGianLopHocPhans
                .Where(l => l.IdThoiGian == idThoiGian)
                .Select(l => l.IdLopHocPhan)
                .FirstOrDefaultAsync();

            if (lhp == null) return true;

            return await (from tl in _context.ThoiGianLopHocPhans
                          join tgCheck in _context.ThoiGians on tl.IdThoiGian equals tgCheck.IdThoiGian
                          where tl.IdLopHocPhan == lhp &&
                                ((start < tgCheck.NgayKetThuc && start > tgCheck.NgayBatDau) ||
                                 (end < tgCheck.NgayKetThuc && end > tgCheck.NgayBatDau))
                          select tgCheck).AnyAsync();
        }
    }
}
