
using Ical.Net;
using System.Net.Http;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//
using qlsv.Models;
using qlsv.Data;
using Microsoft.EntityFrameworkCore;

namespace qlsv.Helpers;

public class CalendarHelper
{
    // Variables
    private readonly QuanLySinhVienDbContext _context;

    // Constructors
    public CalendarHelper(
        QuanLySinhVienDbContext context)
    {
        _context = context;
    }

    // Return list event for student
    public async Task<List<CalendarEventObject>> GetListEventStudent(string IdUser)
    {
        // Variables
        List<CalendarEventObject> listEvent = new List<CalendarEventObject>();

        // Query database to get events for the student
        var events = await (from svlhp in _context.SinhVienLopHocPhans
                            join lhp in _context.LopHocPhans on svlhp.IdLopHocPhan equals lhp.IdLopHocPhan
                            join tglhp in _context.ThoiGianLopHocPhans on lhp.IdLopHocPhan equals tglhp.IdLopHocPhan
                            join tg in _context.ThoiGians on tglhp.IdThoiGian equals tg.IdThoiGian
                            where svlhp.IdSinhVien == IdUser
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

        return listEvent;
    }


    // Return list event for teacher
    public async Task<List<CalendarEventObject>> GetListEventTeacher(string IdUser)
    {
        // Variables
        List<CalendarEventObject> listEvent = new List<CalendarEventObject>();

        // Query database to get events for the teacher
        var events = await (from lhp in _context.LopHocPhans
                            join tglhp in _context.ThoiGianLopHocPhans on lhp.IdLopHocPhan equals tglhp.IdLopHocPhan
                            join tg in _context.ThoiGians on tglhp.IdThoiGian equals tg.IdThoiGian
                            where lhp.IdGiaoVien == IdUser
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
        return listEvent;
    }
}