using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
//
using qlsv.Data;
using qlsv.Models;
using Microsoft.AspNetCore.Authorization;

namespace qlsv.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ViewsApiController : ControllerBase
{
    // Variables
    private readonly ViewDbContext _context;

    // Constructor
    public ViewsApiController(
        ViewDbContext viewDbContext)
    {
        _context = viewDbContext;
    }

    // GET: Get Views
    [HttpGet("getListNavBar")]
    public ActionResult GetListNavBar()
    {
        var listNavBar = _context.NavBarPages.ToList();
        return Ok(listNavBar);
    }

    // POST: Create new Navbar Page
    [HttpPost("createNavBar")]
    public ActionResult CreateNavBar(ViewNavItem model)
    {
        var navBarPage = new ViewNavItem
        {
            Area = model.Area,
            Controller = model.Controller,
            Action = model.Action,
            Content = model.Content,
            Icon = model.Icon,
            IconClass = model.IconClass
        };

        _context.NavBarPages.Add(navBarPage);
        _context.SaveChanges();
        return Ok(navBarPage);
    }

    // PUT: Upgrade the navbar page
    [HttpPut("upgradeNavBar")]
    public ActionResult UpgradeNavBar(ViewNavItem model)
    {
        var navBarPage = _context.NavBarPages.Find(model.Id);
        if (navBarPage == null)
        {
            return NotFound();
        }

        navBarPage.Area = model.Area;
        navBarPage.Controller = model.Controller;
        navBarPage.Action = model.Action;
        navBarPage.Content = model.Content;
        navBarPage.Icon = model.Icon;
        navBarPage.IconClass = model.IconClass;

        _context.SaveChanges();
        return Ok(navBarPage);
    }

    // DELETE: Delete the navbar page
    [HttpDelete("deleteNavBar")]
    public ActionResult DeleteNavBar(int id)
    {
        var navBarPage = _context.NavBarPages.Find(id);
        if (navBarPage == null)
        {
            return NotFound();
        }

        _context.NavBarPages.Remove(navBarPage);
        _context.SaveChanges();
        return Ok(navBarPage);
    }
    
}

