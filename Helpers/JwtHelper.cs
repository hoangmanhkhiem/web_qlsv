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
    private string _accessTokenExpireDays;
    private string _refreshTokenExpireDays;
    private string _audience;
    private string _issuer;

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
        _accessTokenExpireDays = _configuration["AccessTokenExpireDays"];
        _refreshTokenExpireDays = _configuration["RefreshTokenExpireDays"];
        _audience = _configuration["Audience"];
        _issuer = _configuration["Issuer"];
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
            Audience = _audience,
            Issuer = _issuer,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(int.Parse(_accessTokenExpireDays)),
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
            ExpiryDate = DateTime.UtcNow.AddDays(int.Parse(_accessTokenExpireDays)),
        };
        _session.AccessTokens.Add(accessTokenDb);
        _session.SaveChanges();

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
            Audience = _audience,
            Issuer = _issuer,
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
        _session.RefreshTokens.Add(refreshTokenDb);
        _session.SaveChanges();

        return refreshToken;
    }

    // Decode Token
    public JwtSecurityToken DecodeToken(string token)
    {
        return new JwtSecurityTokenHandler().ReadJwtToken(token);
    }

    // Validate Token
    public ValidateToken ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_key);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = true,
                ValidAudience = _audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return new ValidateToken
            {
                IsValid = true,
                Message = "Token is valid.",
                Status = 200
            };
        }
        catch (SecurityTokenExpiredException ex)
        {
            return new ValidateToken
            {
                IsValid = false,
                Message = "Token has expired.",
                Status = 401
            };
        }
        catch (Exception ex)
        {
            return new ValidateToken
            {
                IsValid = false,
                Message = "Invalid token.",
                Status = 403
            };
        }
    }

    // Refresh Token
    public Token RefreshToken(string accessToken, string refreshToken)
    {
        // Validate tokens
        var validateAccessToken = ValidateToken(accessToken);
        var validateRefreshToken = ValidateToken(refreshToken);
        // If either token validation fails, return immediately
        if (validateAccessToken.Status == 403 || 
            !validateRefreshToken.IsValid)
        {
            return new Token
            {
                AccessToken = "",
                RefreshToken = ""
            };
        }

        // Decode tokens
        var decodedAccessToken = DecodeToken(accessToken);
        var decodedRefreshToken = DecodeToken(refreshToken);

        var accessUserId = decodedAccessToken.Claims.FirstOrDefault(x => x.Type == "idUser")?.Value;
        var refreshUserId = decodedRefreshToken.Claims.FirstOrDefault(x => x.Type == "idUser")?.Value;

        // Check user IDs after decoding tokens
        if (string.IsNullOrEmpty(accessUserId)
            || string.IsNullOrEmpty(refreshUserId)
            || accessUserId != refreshUserId)
        {
            checkedHackToken(accessUserId);
            checkedHackToken(refreshUserId);
            // TODO: Send notification/email to the user about potential security concerns
            return new Token
            {
                AccessToken = "",
                RefreshToken = ""
            };
        }

        // Check the refresh token in the database
        var refreshTokenDb = _session.RefreshTokens.FirstOrDefault(x => x.Token == refreshToken);
        if (refreshTokenDb == null || !refreshTokenDb.IsActive)
        {
            checkedHackToken(refreshUserId);
            // TODO: Send notification/email to the user about compromised account
            return new Token
            {
                AccessToken = "",
                RefreshToken = ""
            };
        }
        // Check User ID in the database
        if (refreshTokenDb.UserId != refreshUserId)
        {
            checkedHackToken(refreshUserId);
            checkedHackToken(refreshTokenDb.UserId);
            // TODO: Send notification/email to the user about compromised account
            return new Token
            {
                AccessToken = "",
                RefreshToken = ""
            };
        }

        refreshTokenDb.IsActive = false;
        _session.RefreshTokens.Update(refreshTokenDb);
        _session.SaveChanges();

        return GenerateToken(refreshUserId);
    }

    //  
    private void checkedHackToken(string userId)
    {
        // TODO: use black list block access token - When use redis db
        // Remove all access tokens or active is disabled
        var accessTokens = _session.AccessTokens
            .Where(x => x.UserId == userId)
            .ToList();
        _session.AccessTokens.RemoveRange(accessTokens);

        // Remove all refresh tokens or active is disabled
        var refreshTokens = _session.RefreshTokens
            .Where(x => x.UserId == userId)
            .ToList();
        _session.RefreshTokens.RemoveRange(refreshTokens);

        _session.SaveChanges();
    }

}