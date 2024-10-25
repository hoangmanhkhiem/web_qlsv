
using Microsoft.AspNetCore.Mvc;

//
using qlsv.Helpers;

namespace qlsv.Components;

public class CalendarViewComponent : ViewComponent
{
    // Variables
    // TODO handle with database
    private readonly CalendarHelper _calendarHelper;
    // Constructor
    public CalendarViewComponent(
        CalendarHelper calendarHelper)
    {
        _calendarHelper = calendarHelper;
    }

    // Hanlders async
    public async Task<IViewComponentResult> InvokeAsync(string IdUser, string Type) 
    {
        switch (Type.ToUpper()) {
            case "SINHVIEN":
                var listEvent = await _calendarHelper.GetListEventStudent(IdUser);
                return View("SinhVien", listEvent);
            case "GIAOVIEN":
                var listEventTeacher = await _calendarHelper.GetListEventTeacher(IdUser);
                return View("GiaoVien", listEventTeacher);
            default:
                return View();
        }
    }
}