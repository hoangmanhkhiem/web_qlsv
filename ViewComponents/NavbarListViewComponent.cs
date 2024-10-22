
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


//
using qlsv.Data;

namespace qlsvm.Components;

public class NavbarListViewComponent : ViewComponent
{
    // Variables
    private readonly ViewDbContext _dbContext;
    // Constructor
    public NavbarListViewComponent(
        ViewDbContext viewDbContext)
    {
        _dbContext = viewDbContext;
    }

    // Hanlders async
    public async Task<IViewComponentResult> InvokeAsync(string page, params object[] args)
    {
        if (page.ToUpper() == "ADMIN")
        {
            var listNavbar = await _dbContext.NavBarPages
                .Where(p => p.Area == "ADMIN")
                .ToListAsync();
            return View("AdminPage", listNavbar);
        }
        if (page.ToUpper() == "STUDENT")
        {
            return View("StudentPage");
        }
        if (page.ToUpper() == "TEACHER")
        {
            return View("TeacherPage");
        }
        return View("Default");
    }

}