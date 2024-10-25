
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
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var listEvents = _calendarHelper.ConvertIcsToCalendarEvents("wwwroot/resources/data_cadenlar.ics");

        // System.Console.Write(_calendarHelper.SerializeCalendarEvents(listEvents));

        return View();
    }

}