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

        // Generate a refresh token
        var refreshToken = _refreshToken ?? GenerateRefreshToken(userId);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", userId),
                new Claim("username", username),
                new Claim("refreshToken", refreshToken)
            }),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(_expireMinutes)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        // Create JWT Access Token with Refresh Token embedded in payload
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(token);

        // Return the Access Token (with embedded refresh token in payload)
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
    public bool CheckJwtToken(string token)
    {
        var key = Encoding.ASCII.GetBytes(_key);

        // Create token handler
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            // Validate token
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true, // Bật xác thực thời gian sống của token
                ClockSkew = TimeSpan.Zero // Tùy chọn: thiết lập độ lệch thời gian (thời gian mà token có thể hợp lệ sau khi hết hạn)
            }, out SecurityToken validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }

    // Refresh JWT token
    public string RefreshJwtToken(string token)
    {
        // Create token handler
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_key);

        // Read payload
        var payload = tokenHandler.ReadJwtToken(token).Payload;

        // Validate token
        if (!HelpperCheckTokenRefresh(token, tokenHandler, key))
        {
            return token;
        }

        // Check date refresh token
        var refreshTokens = _context.RefreshTokens.FirstOrDefault(
            x => x.UserId == payload["userId"].ToString()
        );

        if (refreshTokens == null)
        {
            return token;
        }

        if (refreshTokens.Token.Equals(payload["refreshToken"].ToString())
            && refreshTokens.ExpiryDate > DateTime.UtcNow)
        {
            return GenerateJwtToken(
                payload["userId"].ToString(),
                payload["username"].ToString(),
                refreshTokens.Token
            );
        }

        return token;
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
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
            }, out SecurityToken validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }

}
