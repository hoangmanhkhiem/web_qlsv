using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

//
using qlsv.Data;
using qlsv.Models;
using qlsv.ViewModels;

namespace qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CalendarController : ControllerBase
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;

    // Constructor
    public CalendarController(
        QuanLySinhVienDbContext context)
    {
        _context = context;
    }

    // GET: Get calendar from giao vien with id
    [HttpGet("giaovien/{id}")]
    public async Task<IActionResult> GetCalendarGiaoVien(string id)
    {
        // Variables
        List<CalendarEventObject> listEvent = new List<CalendarEventObject>();

        // Query database to get events for the teacher
        var events = await (from lhp in _context.LopHocPhans
                            join tglhp in _context.ThoiGianLopHocPhans on lhp.IdLopHocPhan equals tglhp.IdLopHocPhan
                            join tg in _context.ThoiGians on tglhp.IdThoiGian equals tg.IdThoiGian
                            where lhp.IdGiaoVien == id
                            select new CalendarEventObject
                            {
                                Id = tg.IdThoiGian,
                                GroupId = lhp.IdLopHocPhan,
                                Title = lhp.TenHocPhan,
                                Description = $"Giáo viên: {lhp.TenHocPhan}",
                                Start = tg.NgayBatDau,
                                End = tg.NgayKetThuc
                            }).ToListAsync();

        // Add results to listEvent
        listEvent.AddRange(events);

        if (events.Count == 0)
        {
            return NotFound("Không tìm thấy lịch học");
        }
        // To Json 
        var res = JsonSerializer.Serialize(listEvent.Select(mh => new
        {
            id = mh.Id,
            groupId = mh.GroupId,
            title = mh.Title,
            start = mh.Start?.ToString("yyyy-MM-ddTHH:mm:ss"),
            end = mh.End?.ToString("yyyy-MM-ddTHH:mm:ss"),
            description = mh.Description
        }).ToList());

        return Ok(
            res
        );
    }


    // GET: Get calendar from sinh vien with id
    [HttpGet("sinhvien/{id}")]
    public async Task<IActionResult> GetCalendarSinhVien(string id)
    {
        // Variables
        List<CalendarEventObject> listEvent = new List<CalendarEventObject>();

        // Query database to get events for the student
        var events = await (from svlhp in _context.SinhVienLopHocPhans
                            join lhp in _context.LopHocPhans on svlhp.IdLopHocPhan equals lhp.IdLopHocPhan
                            join tglhp in _context.ThoiGianLopHocPhans on lhp.IdLopHocPhan equals tglhp.IdLopHocPhan
                            join tg in _context.ThoiGians on tglhp.IdThoiGian equals tg.IdThoiGian
                            where svlhp.IdSinhVien == id
                            select new CalendarEventObject
                            {
                                Id = tg.IdThoiGian,
                                GroupId = lhp.IdLopHocPhan,
                                Title = lhp.TenHocPhan,
                                Description = $"Lớp: {lhp.TenHocPhan}",
                                Start = tg.NgayBatDau,
                                End = tg.NgayKetThuc
                            }).ToListAsync();

        // Add results to listEvent
        listEvent.AddRange(events);

        if (events.Count == 0)
        {
            return NotFound("Không tìm thấy lịch học");
        }
        // To Json 
        var res = JsonSerializer.Serialize(listEvent.Select(mh => new
        {
            id = mh.Id,
            groupId = mh.GroupId,
            title = mh.Title,
            start = mh.Start?.ToString("yyyy-MM-ddTHH:mm:ss"),
            end = mh.End?.ToString("yyyy-MM-ddTHH:mm:ss"),
            description = mh.Description
        }).ToList());

        return Ok(
            res
        );
    }

    /**
     * GET: api/calendar/lophocphan/{id}
     * Get calerdar lop hoc phan with id lop hoc pha
     */
    [HttpGet("lophocphan/{id}")]
    public async Task<IActionResult> GetCalendarLopHocPhan(string id)
    {
        List<CalendarEventObject> listEvents = await (
            from tg_lhp in _context.ThoiGianLopHocPhans
            where tg_lhp.IdLopHocPhan == id
            join lhp in _context.LopHocPhans on tg_lhp.IdLopHocPhan equals lhp.IdLopHocPhan
            join tg in _context.ThoiGians on tg_lhp.IdThoiGian equals tg.IdThoiGian
            select new CalendarEventObject {
                Id = tg.IdThoiGian,
                GroupId = tg_lhp.IdLopHocPhan,
                Title = lhp.TenHocPhan,
                Description = $"Lớp: {lhp.TenHocPhan}",
                Start = tg.NgayBatDau,
                End = tg.NgayKetThuc
            }
        ).ToListAsync();

        if (listEvents.Count < 0){
            return BadRequest("Không tìm thấy lịch học");
        }

        var res = JsonSerializer.Serialize(listEvents.Select(mh => new
        {
            id = mh.Id,
            groupId = mh.GroupId,
            title = mh.Title,
            start = mh.Start?.ToString("yyyy-MM-ddTHH:mm:ss"),
            end = mh.End?.ToString("yyyy-MM-ddTHH:mm:ss"),
            description = mh.Description.ToString()
        }).ToList());

        return Ok(res);
    }

}


