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
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles="Root")]
public class SessionApiController : ControllerBase
{
    // Variables
    private readonly SessionDbContext _session;

    // Constructor
    public SessionApiController(
        SessionDbContext session
    )
    {
        _session = session;
    }

    // GET: Get Accsess token 
    [HttpGet("getAccessToken")]
    public ActionResult GetAccessToken()
    {   
        var listAccessToken = _session.AccessTokens.ToList();
        return Ok(listAccessToken);
    }

    // GET: Get Refresh Token
    [HttpGet("getRefreshToken")]
    public ActionResult GetRefreshToken()
    {
        var listRefreshToken = _session.RefreshTokens.ToList();
        return Ok(listRefreshToken);
    }

    // POST: Add Access Token
    [HttpPost("addAccessToken")]
    public ActionResult AddAccessToken([FromBody] qlsv.Models.AccessToken model)
    {
        var accessToken = new qlsv.Models.AccessToken
        {
            Token = model.Token,
            UserId = model.UserId,
            ExpiryDate = model.ExpiryDate
        };
        _session.AccessTokens.Add(accessToken);
        _session.SaveChanges();
        return Ok(accessToken);
    }

    // POST: Add Refresh Token
    [HttpPost("addRefreshToken")]
    public ActionResult AddRefreshToken([FromBody] qlsv.Models.RefreshToken model)
    {
        var refreshToken = new qlsv.Models.RefreshToken
        {
            Token = model.Token,
            UserId = model.UserId,
            ExpiryDate = model.ExpiryDate
        };
        _session.RefreshTokens.Add(refreshToken);
        _session.SaveChanges();
        return Ok(refreshToken);
    }

    // PUT: Update Access Token
    [HttpPut("updateAccessToken")]
    public ActionResult UpdateAccessToken([FromBody] qlsv.Models.AccessToken model)
    {
        var accessToken = _session.AccessTokens.FirstOrDefault(a => a.UserId == model.UserId);
        if (accessToken != null)
        {
            accessToken.Token = model.Token;
            accessToken.ExpiryDate = model.ExpiryDate;
            _session.SaveChanges();
            return Ok(accessToken);
        }
        return Ok("Access token not found");
    }

    // PUT: Update Refresh Token
    [HttpPut("updateRefreshToken")]
    public ActionResult UpdateRefreshToken([FromBody] qlsv.Models.RefreshToken model)
    {
        var refreshToken = _session.RefreshTokens.FirstOrDefault(r => r.UserId == model.UserId);
        if (refreshToken != null)
        {
            refreshToken.Token = model.Token;
            refreshToken.ExpiryDate = model.ExpiryDate;
            _session.SaveChanges();
            return Ok(refreshToken);
        }
        return Ok("Refresh token not found");
    }

    // DELETE: Delete Access Token
    [HttpDelete("deleteAccessToken")]
    public ActionResult DeleteAccessToken(string IdToken)
    {
        var accessToken = _session.AccessTokens.FirstOrDefault(a => a.Id == IdToken);
        if (accessToken != null)
        {
            _session.AccessTokens.Remove(accessToken);
            _session.SaveChanges();
            return Ok("Access token deleted");
        }
        return Ok("Access token not found");
    }

    // DELETE: Delete Refresh Token
    [HttpDelete("deleteRefreshToken")]
    public ActionResult DeleteRefreshToken(string IdToken)
    {
        var refreshToken = _session.RefreshTokens.FirstOrDefault(r => r.Id == IdToken);
        if (refreshToken != null)
        {
            _session.RefreshTokens.Remove(refreshToken);
            _session.SaveChanges();
            return Ok("Refresh token deleted");
        }
        return Ok("Refresh token not found");
    }
    
}

