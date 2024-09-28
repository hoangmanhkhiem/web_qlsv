using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
//
using qlsv.Data;
using qlsv.ViewModels;
using qlsv.Models;

namespace qlsv.Helpers;

public class JwtHelper
{
    // Variables
    private readonly IConfiguration _configuration;
    private readonly IdentityDbContext _context;
    private readonly SessionDbContext _session;
    private string _key;
    private string _expireMinutes;
    private string _refreshTokenExpireDays;

    // Constructor
    public JwtHelper(
        IConfiguration configuration,
        IdentityDbContext context,
        SessionDbContext session
    )
    {
        _configuration = configuration.GetSection("Jwt");
        _context = context;
        _session = session;
        GetValue();
    }

    // Get value
    private void GetValue()
    {
        _key = _configuration["Key"];
        _expireMinutes = _configuration["ExpireMinutes"];
        _refreshTokenExpireDays = _configuration["RefreshTokenExpireDays"];
    }

    // Generate Token 
    public Token GenerateToken(string userId)
    {
        return new Token
        {
            AccessToken = GenerateAccessToken(userId),
            RefreshToken = GenerateRefreshToken(userId)
        };
    }

    // Generate Access Token
    private string GenerateAccessToken(string userId)
    {
        // Generate Access Token (JWT Token)
        var key = Encoding.ASCII.GetBytes(_key);
        var tokenHandler = new JwtSecurityTokenHandler();

        // Get List Role With UserId
        var roles = (from role in _context.UserRoles
                     join user in _context.Users on role.UserId equals user.Id
                     join roleItem in _context.Roles on role.RoleId equals roleItem.Id
                     where user.Id == userId
                     select roleItem.Name).ToList();

        var claims = new List<Claim>
        {
            new Claim("idUser", userId),
        };

        // Add roles to claims
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(_expireMinutes)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        // Create JWT Access Token
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(token);

        // Add Access Token to Session Db
        var accessTokenDb = new AccessToken
        {
            Token = accessToken,
            UserId = userId,
            ExpiryDate = DateTime.UtcNow.AddMinutes(int.Parse(_expireMinutes))
        };
        _session.AccessTokens.Add(accessTokenDb);

        // Return the Access Token
        return accessToken;
    }

    // Generate Refresh Token
    private string GenerateRefreshToken(string userId)
    {
        var key = Encoding.ASCII.GetBytes(_key);
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("idUser", userId),
            }),
            Expires = DateTime.UtcNow.AddDays(int.Parse(_refreshTokenExpireDays)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        // Create JWT Refresh Token
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var refreshToken = tokenHandler.WriteToken(token);

        // Add Token to Session Db
        var refreshTokenDb = new RefreshToken
        {
            Token = refreshToken,
            UserId = userId,
            ExpiryDate = DateTime.UtcNow.AddDays(int.Parse(_refreshTokenExpireDays))
        };

        return refreshToken;
    }

    // Decode Token
    public JwtSecurityToken DecodeToken(string token)
    {
       return new JwtSecurityTokenHandler().ReadJwtToken(token);
    }
}
