
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


//
using qlsv.Data;
using qlsv.Models;
using qlsv.Helpers;

namespace qlsv.Components;

public class NavUserInformationViewComponent : ViewComponent
{
    // Variables
    private readonly IdentityDbContext _context;
    private readonly JwtHelper _jwt;

    // Constructor
    public NavUserInformationViewComponent(
        IdentityDbContext identityDbContext,
        JwtHelper jwtHelper)
    {
        _context = identityDbContext;
        _jwt = jwtHelper;
    }

    // Hanlders async
    public async Task<IViewComponentResult> InvokeAsync()
    {
        // Check if _context is null
        if (_context == null)
        {
            throw new NullReferenceException("The _context is null.");
        }

        // Get user information from cookie
        string accessToken = HttpContext.Request.Cookies["AccsessToken"];

        // Check if accessToken is null or empty
        if (string.IsNullOrEmpty(accessToken))
        {
            throw new NullReferenceException("The accessToken cookie is null or empty.");
        }

        // Decode the token
        var jwtToken = _jwt.DecodeToken(accessToken);

        // Check if jwtToken is null
        if (jwtToken == null)
        {
            throw new NullReferenceException("The decoded JWT token is null.");
        }

        // Extract idUser from the token claims
        string idUser = jwtToken.Claims.FirstOrDefault(c => c.Type == "idUser")?.Value;

        // Extract role name from the token claims
        string roleName = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

        // Check if idUser is null or empty
        if (string.IsNullOrEmpty(idUser))
        {
            throw new NullReferenceException("The UserId claim is null or empty.");
        }

        // Fetch user information from the database
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == idUser);

        // Check if user is null
        if (user == null)
        {
            throw new NullReferenceException("The user is not found in the database.");
        }

        return ViewHelp(user, roleName);
    }

    // Helpers return view
    private IViewComponentResult ViewHelp(UserCustom user, string roleName)
    {
        switch (roleName.ToUpper())
        {
            case "ADMIN":
                return View("Admin", user);
            case "SINHVIEN":
                return View("Student", user);
            case "GIAOVIEN":
                return View("Teacher", user);
            default:
                return View("Default", user);
        }
    }

}