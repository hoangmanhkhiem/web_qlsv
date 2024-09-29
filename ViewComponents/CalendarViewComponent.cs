
using Microsoft.AspNetCore.Mvc;

namespace qlsvm.Components;

public class CalendarViewComponent : ViewComponent
{
    // Variables
    // TODO handle with database
    // Constructor
    public CalendarViewComponent()
    {
    }

    // Hanlders async
    public async Task<IViewComponentResult> InvokeAsync()
    {
        await Task.CompletedTask;
        return View();
    }

}