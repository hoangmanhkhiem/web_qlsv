using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
//
using qlsv.Data;
using qlsv.Models;

namespace qlsv.Helpers;

public class JwtHelper
{
    // Variables
    private readonly IConfiguration _configuration;
    private readonly IdentityDbContext _context;
    private string _key;
    private string _expireMinutes;
    private string _refreshTokenExpireDays;

    // Constructor
    public JwtHelper(IConfiguration configuration, IdentityDbContext context)
    {
        _configuration = configuration.GetSection("Jwt");
        _context = context;
        GetValue();
    }

    // Get value
    private void GetValue()
    {
        _key = _configuration["Key"];
        _expireMinutes = _configuration["ExpireMinutes"];
        _refreshTokenExpireDays = _configuration["RefreshTokenExpireDays"];
    }

    // Create JWT token and embed Refresh Token in payload
    public string GenerateJwtToken(string userId, string username, string? _refreshToken)
    {
        // Generate Access Token (JWT Token)
        var key = Encoding.ASCII.GetBytes(_key);
        var tokenHandler = new JwtSecurityTokenHandler();

        // Generate a refresh token if not provided
        var refreshToken = _refreshToken ?? GenerateRefreshToken(userId);

        // List Role
        var roles = (from role in _context.UserRoles
                     join user in _context.Users on role.UserId equals user.Id
                     join roleItem in _context.Roles on role.RoleId equals roleItem.Id
                     where user.Id == userId
                     select roleItem.Name).ToList();

        var claims = new List<Claim>
        {
            new Claim("id", userId),
            new Claim("username", username),
            new Claim("refreshToken", refreshToken)
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

        // Return the Access Token
        return accessToken;
    }


    // Create refresh token
    public string GenerateRefreshToken(string UserId)
    {
        // Auto remove the refresh token from the session
        this.RevokeToken(UserId);
        var refreshToken = Guid.NewGuid().ToString();
        var expires = DateTime.UtcNow.AddDays(int.Parse(_refreshTokenExpireDays));

        // Add the refresh token to the session
        var refreshTokenModel = new RefreshToken
        {
            Token = refreshToken,
            UserId = UserId,
            ExpiryDate = expires
        };
        _context.RefreshTokens.Add(refreshTokenModel);
        _context.SaveChanges();

        return refreshToken;
    }

    // Check JWT token
    public (bool, string) CheckJwtToken(string token)
    {
        var key = Encoding.ASCII.GetBytes(_key);

        // Create token handler
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            // Validate token
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true, // Kiểm tra khóa ký
                IssuerSigningKey = new SymmetricSecurityKey(key), // Khóa ký đối với token
                ValidateIssuer = false, // Bạn có thể bật lên nếu cần kiểm tra issuer
                ValidateAudience = false, // Bạn có thể bật lên nếu cần kiểm tra audience
                ValidateLifetime = true, // Kiểm tra thời gian hết hạn của token
                ClockSkew = TimeSpan.Zero // Loại bỏ độ lệch thời gian mặc định
            }, out SecurityToken validatedToken);

            return (true, "");
        }
        catch (SecurityTokenExpiredException)
        {
            // Token hết hạn
            return (false, RefreshJwtToken(token));
        }
        catch (Exception)
        {
            // Các lỗi khác
            return (false, "Token is not valid");
        }
    }

    // Refresh JWT token
    public string RefreshJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_key);
        var jwtToken = tokenHandler.ReadJwtToken(token);
        var payload = jwtToken.Payload;

        if (!HelpperCheckTokenRefresh(token, tokenHandler, key))
        {
            return token;
        }

        var userId = payload["id"].ToString();
        var refreshTokenEntry = _context.RefreshTokens.FirstOrDefault(x => x.UserId == userId);

        if (refreshTokenEntry == null ||
            !refreshTokenEntry.Token.Equals(payload["refreshToken"].ToString()) ||
            refreshTokenEntry.ExpiryDate <= DateTime.UtcNow)
        {
            return token;
        }

        return GenerateJwtToken(userId, payload["username"].ToString(), refreshTokenEntry.Token);
    }

    // Revoke JWT token
    public bool RevokeToken(string userId)
    {
        var refreshToken = _context.RefreshTokens.FirstOrDefault(
            x => x.UserId == userId
        );

        if (refreshToken == null)
        {
            return false;
        }
        // Revoke the refresh token
        _context.RefreshTokens.Remove(refreshToken);
        _context.SaveChanges();

        return true;
    }

    // Helpper Refresh Token
    private bool HelpperCheckTokenRefresh(
        string token,
        JwtSecurityTokenHandler tokenHandler,
        byte[] key
    )
    {
        try
        {
            // Validate token
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true, // Kiểm tra khóa ký
                IssuerSigningKey = new SymmetricSecurityKey(key), // Khóa ký đối với token
                ValidateIssuer = false, // Bạn có thể bật lên nếu cần kiểm tra issuer
                ValidateAudience = false, // Bạn có thể bật lên nếu cần kiểm tra audience
                ValidateLifetime = false, // Kiểm tra thời gian hết hạn của token
            }, out SecurityToken validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }

}
