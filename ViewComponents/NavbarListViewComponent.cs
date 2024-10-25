
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


//
using qlsv.Data;

namespace qlsv.Components;

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
                .Where(p => p.LocationNavItem.ToUpper() == "ADMIN")
                .ToListAsync();
            return View("AdminPage", listNavbar);
        }
        if (page.ToUpper() == "STUDENT")
        {
            var listNavbar = await _dbContext.NavBarPages
                .Where(p => p.LocationNavItem.ToUpper() == "STUDENT")
                .ToListAsync();
            return View("StudentPage", listNavbar);
        }
        if (page.ToUpper() == "TEACHER")
        {
            var listNavbar = await _dbContext.NavBarPages
                .Where(p => p.LocationNavItem.ToUpper() == "TEACHER")
                .ToListAsync();
            return View("TeacherPage", listNavbar);
        }
        return View("Default");
    }

}