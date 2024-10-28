using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
//
using qlsv.ViewModels;
using qlsv.Helpers;
using qlsv.Models;
using qlsv.Data;

namespace qlsv.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Root")]
public class IdentityApiController : ControllerBase
{
    // Variables
    private readonly qlsv.Data.IdentityDbContext _context;
    private readonly SecurityHelper _securityHelper;

    // Constructor
    public IdentityApiController(
        qlsv.Data.IdentityDbContext context,
        SecurityHelper securityHelper)
    {
        _context = context;
        _securityHelper = securityHelper;
    }

    // GET: Users
    [Authorize(Roles = "Root")]
    [HttpGet("users")]
    public IActionResult GetUsers()
    {
        var listUsers = _context.Users.ToList();
        var userDtos = listUsers.Select(user => new UserCustom
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Address = user.Address,
            Phone = user.Phone,
            ProfilePicture = user.ProfilePicture
        }).ToList();
        return Ok(userDtos);
    }

    // GET: Roles
    [HttpGet("roles")]
    public IActionResult GetRoles()
    {
        var listRole = _context.Roles.ToList();
        var roleDtos = listRole.Select(role => new IdentityRole
        {
            Id = role.Id,
            Name = role.Name,
            NormalizedName = role.NormalizedName,
            ConcurrencyStamp = role.ConcurrencyStamp
        }).ToList();
        return Ok(roleDtos);
    }

    // GET: UserRoles
    [HttpGet("userroles")]
    public IActionResult GetUserRoles()
    {
        var listUserRoles = _context.UserRoles.ToList();
        var userRoles = listUserRoles.Select(userRole => new IdentityUserRole<string>
        {
            UserId = userRole.UserId,
            RoleId = userRole.RoleId,
        }).ToList();
        return Ok(userRoles);
    }

    // GET: RoleClaims
    [HttpGet("roleclaims")]
    public IActionResult GetRoleClaims()
    {
        var listRoleClaims = _context.RoleClaims.ToList();
        var roleClaims = listRoleClaims.Select(roleClaim => new IdentityRoleClaim<string>
        {
            Id = roleClaim.Id,
            RoleId = roleClaim.RoleId,
            ClaimType = roleClaim.ClaimType,
            ClaimValue = roleClaim.ClaimValue,
        }).ToList();
        return Ok(roleClaims);
    }

    // GET: UserClaims
    [HttpGet("userclaims")]
    public IActionResult GetUserClaims()
    {
        var listUserClaims = _context.UserClaims.ToList();
        var userClaims = listUserClaims.Select(userClaim => new IdentityUserClaim<string>
        {
            Id = userClaim.Id,
            UserId = userClaim.UserId,
            ClaimType = userClaim.ClaimType,
            ClaimValue = userClaim.ClaimValue,
        }).ToList();
        return Ok(userClaims);
    }

    // GET: UserLogins
    [HttpGet("userlogins")]
    public IActionResult GetUserLogins()
    {
        var listUserLogins = _context.UserLogins.ToList();
        var userLogins = listUserLogins.Select(userLogin => new IdentityUserLogin<string>
        {
            LoginProvider = userLogin.LoginProvider,
            ProviderKey = userLogin.ProviderKey,
            UserId = userLogin.UserId,
        }).ToList();
        return Ok(userLogins);
    }

    // GET: UserTokens
    [HttpGet("usertokens")]
    public IActionResult GetUserTokens()
    {
        var listUserTokens = _context.UserTokens.ToList();
        var userTokens = listUserTokens.Select(userToken => new IdentityUserToken<string>
        {
            UserId = userToken.UserId,
            LoginProvider = userToken.LoginProvider,
            Name = userToken.Name,
            Value = userToken.Value,
        }).ToList();
        return Ok(userTokens);
    }

    // PUT: UpdateUser
    [HttpPut("updateuser")]
    public IActionResult UpdateUser(UserCutomInputJson model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = _context.Users.FirstOrDefault(user => user.Id == model.Id);
        if (user == null)
        {
            return NotFound("User not found");
        }
        user.UserName = model.UserName;
        user.Email = model.Email;
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.Address = model.Address;
        user.Phone = model.Phone;
        user.PasswordHash = model.PasswordHash;
        _context.SaveChanges();
        return Ok();
    }

    // PUT: Upgrade Roles
    [HttpPut("updaterole")]
    public IActionResult UpdateRole(IdentityRole model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var role = _context.Roles.FirstOrDefault(r => r.Id == model.Id);
        if (role == null)
        {
            return NotFound("Role not found");
        }

        role.Name = model.Name;
        role.NormalizedName = model.NormalizedName;
        _context.SaveChanges();
        return Ok($"Role {role.Name}, {role.NormalizedName} updated");
    }

    // PUT: Upgrade User Role
    [HttpPut("updateuserrole")]
    public IActionResult UpdateUserRole(string userId, string roleId)
    {
        var userRole = _context.UserRoles.FirstOrDefault(ur => ur.UserId == userId && ur.RoleId == roleId);
        if (userRole == null)
        {
            return NotFound("UserRole not found");
        }

        userRole.RoleId = roleId;
        _context.SaveChanges();
        return Ok($"UserRole {userRole.RoleId} updated");
    }

    // PUT: Upgrade Role Claims
    [HttpPut("updateroleclaim")]
    public IActionResult UpdateRoleClaim(IdentityRoleClaim<string> model)
    {
        var roleClaim = _context.RoleClaims.FirstOrDefault(rc => rc.Id == model.Id);
        if (roleClaim == null)
        {
            return NotFound("RoleClaim not found");
        }

        roleClaim.ClaimType = model.ClaimType;
        roleClaim.ClaimValue = model.ClaimValue;
        _context.SaveChanges();
        return Ok($"RoleClaim {roleClaim.ClaimType}, {roleClaim.ClaimValue} updated");
    }

    // PUT: Upgrade User Claims
    [HttpPut("updateuserclaim")]
    public IActionResult UpdateUserClaim(IdentityUserClaim<string> model)
    {
        var userClaim = _context.UserClaims.FirstOrDefault(uc => uc.Id == model.Id);
        if (userClaim == null)
        {
            return NotFound("UserClaim not found");
        }

        userClaim.ClaimType = model.ClaimType;
        userClaim.ClaimValue = model.ClaimValue;
        _context.SaveChanges();
        return Ok($"UserClaim {userClaim.ClaimType}, {userClaim.ClaimValue} updated");
    }

    // PUT: Upgrade User Logins
    [HttpPut("updateuserlogin")]
    public IActionResult UpdateUserLogin(IdentityUserLogin<string> model)
    {
        var userLogin = _context.UserLogins.FirstOrDefault(ul => ul.LoginProvider == model.LoginProvider && ul.ProviderKey == model.ProviderKey);
        if (userLogin == null)
        {
            return NotFound("UserLogin not found");
        }

        userLogin.UserId = model.UserId;
        _context.SaveChanges();
        return Ok($"User {userLogin.UserId}");
    }

    // PUT: Upgrade User Tokens
    [HttpPut("updateusertoken")]
    public IActionResult UpdateUserToken(IdentityUserToken<string> model)
    {
        var userToken = _context.UserTokens.FirstOrDefault(ut => ut.UserId == model.UserId && ut.LoginProvider == model.LoginProvider && ut.Name == model.Name);
        if (userToken == null)
        {
            return NotFound("UserToken not found");
        }

        userToken.Value = model.Value;
        _context.SaveChanges();
        return Ok($"UserToken {userToken.Value}");
    }

    // PUT: Upgrade password user
    // TODO: Handle Update password when {token, new password} is valid
    [HttpPut("updatepassword/{id}")]
    public IActionResult UpdatePassword(string id, string newPassword)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound("User not found");
        }

        user.PasswordHash = _securityHelper.Hash(newPassword);
        _context.SaveChanges();
        return Ok($"User {user.UserName} updated");
    }

    // DELETE: Delete User
    [HttpDelete("deleteuser/{id}")]
    public IActionResult DeleteUser(string id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound("User not found");
        }

        _context.Users.Remove(user);
        _context.SaveChanges();
        return Ok($"User {user.UserName} deleted");
    }

    // DELETE: Delete Role
    [HttpDelete("deleterole/{id}")]
    public IActionResult DeleteRole(string id)
    {
        var role = _context.Roles.FirstOrDefault(r => r.Id == id);
        if (role == null)
        {
            return NotFound("Role not found");
        }

        _context.Roles.Remove(role);
        _context.SaveChanges();
        return Ok($"Role {role.Name} deleted");
    }

    // DELETE: Delete User Role
    [HttpDelete("deleteuserrole/{userId}/{roleId}")]
    public IActionResult DeleteUserRole(string userId, string roleId)
    {
        var userRole = _context.UserRoles.FirstOrDefault(ur => ur.UserId == userId && ur.RoleId == roleId);
        if (userRole == null)
        {
            return NotFound("UserRole not found");
        }

        _context.UserRoles.Remove(userRole);
        _context.SaveChanges();
        return Ok($"UserRole {userRole.RoleId} deleted");
    }

    // DELETE: Delete Role Claims
    [HttpDelete("deleteroleclaim/{id}")]
    public IActionResult DeleteRoleClaim(int id)
    {
        var roleClaim = _context.RoleClaims.FirstOrDefault(rc => rc.Id == id);
        if (roleClaim == null)
        {
            return NotFound("RoleClaim not found");
        }

        _context.RoleClaims.Remove(roleClaim);
        _context.SaveChanges();
        return Ok($"RoleClaim {roleClaim.ClaimType}, {roleClaim.ClaimValue} deleted");
    }

    // DELETE: Delete User Claims
    [HttpDelete("deleteuserclaim/{id}")]
    public IActionResult DeleteUserClaim(int id)
    {
        var userClaim = _context.UserClaims.FirstOrDefault(uc => uc.Id == id);
        if (userClaim == null)
        {
            return NotFound("UserClaim not found");
        }

        _context.UserClaims.Remove(userClaim);
        _context.SaveChanges();
        return Ok($"UserClaim {userClaim.ClaimType}, {userClaim.ClaimValue} deleted");
    }

    // DELETE: Delete User Logins
    [HttpDelete("deleteuserlogin/{loginProvider}/{providerKey}")]
    public IActionResult DeleteUserLogin(string loginProvider, string providerKey)
    {
        var userLogin = _context.UserLogins.FirstOrDefault(ul => ul.LoginProvider == loginProvider && ul.ProviderKey == providerKey);
        if (userLogin == null)
        {
            return NotFound("UserLogin not found");
        }

        _context.UserLogins.Remove(userLogin);
        _context.SaveChanges();
        return Ok($"User {userLogin.UserId} deleted");
    }

    // DELETE: Delete User Tokens
    [HttpDelete("deleteusertoken/{userId}/{loginProvider}/{name}")]
    public IActionResult DeleteUserToken(string userId, string loginProvider, string name)
    {
        var userToken = _context.UserTokens.FirstOrDefault(ut => ut.UserId == userId && ut.LoginProvider == loginProvider && ut.Name == name);
        if (userToken == null)
        {
            return NotFound("UserToken not found");
        }

        _context.UserTokens.Remove(userToken);
        _context.SaveChanges();
        return Ok($"UserToken {userToken.Value} deleted");
    }

    // POST: Create User
    [HttpPost("createuser")]
    public IActionResult CreateUser(UserCutomInputJson model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!CheckDuplicateCreateUser(model)) {
            return BadRequest("UserName, Email, Phone is duplicated");
        }

        var user = new UserCustom
        {
            UserName = model.UserName,
            Email = model.Email,
            FullName = model.FullName,
            Address = model.Address,
            Phone = model.Phone,
        };

        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok($"{model.UserName} created");
    }

    // POST: Create Role
    [HttpPost("createrole")]
    public IActionResult CreateRole(IdentityRole model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!CheckDuplicateCreateRole(model)) {
            return BadRequest("Name is duplicated");
        }

        var role = new IdentityRole
        {
            Name = model.Name,
            NormalizedName = model.NormalizedName
        };

        _context.Roles.Add(role);
        _context.SaveChanges();
        return Ok($"Role {role.Name} created");
    }

    // POST: Create User Role
    [HttpPost("createuserrole")]
    public IActionResult CreateUserRole(string userId, string roleId)
    {
        // Check user is already
        if (!_context.Users.Any(u => u.Id == userId))
        {
            return NotFound("User not found");
        }

        // Check role is already
        if(!_context.Roles.Any(r => r.Id == roleId))
        {
            return NotFound("Role not found");
        }

        var userRole = new IdentityUserRole<string>
        {
            UserId = userId,
            RoleId = roleId
        };

        _context.UserRoles.Add(userRole);
        _context.SaveChanges();
        return Ok($"UserRole {userRole.RoleId} created");
    }

    // POST: Create Role Claims
    [HttpPost("createroleclaim")]
    public IActionResult CreateRoleClaim(IdentityRoleClaim<string> model)
    {
        if (!_context.Roles.Any(r => r.Id == model.RoleId))
        {
            return NotFound("Role not found");
        }
        var roleClaim = new IdentityRoleClaim<string>
        {
            RoleId = model.RoleId,
            ClaimType = model.ClaimType,
            ClaimValue = model.ClaimValue
        };

        _context.RoleClaims.Add(roleClaim);
        _context.SaveChanges();
        return Ok($"Role {roleClaim.RoleId} created");
    }

    // POST: Create User Claims
    [HttpPost("createuserclaim")]
    public IActionResult CreateUserClaim(IdentityUserClaim<string> model)
    {
        if (!_context.Users.Any(u => u.Id == model.UserId))
        {
            return NotFound("User not found");
        }
        var userClaim = new IdentityUserClaim<string>
        {
            UserId = model.UserId,
            ClaimType = model.ClaimType,
            ClaimValue = model.ClaimValue
        };

        _context.UserClaims.Add(userClaim);
        _context.SaveChanges();
        return Ok($"User {userClaim.UserId} created");
    }

    // POST: Create User Logins
    [HttpPost("createuserlogin")]
    public IActionResult CreateUserLogin(IdentityUserLogin<string> model)
    {
        if (!_context.Users.Any(u => u.Id == model.UserId))
        {
            return NotFound("User not found");
        }
        var userLogin = new IdentityUserLogin<string>
        {
            UserId = model.UserId,
            LoginProvider = model.LoginProvider,
            ProviderKey = model.ProviderKey
        };

        _context.UserLogins.Add(userLogin);
        _context.SaveChanges();
        return Ok($"User {userLogin.UserId} created");
    }

    // POST: Create User Tokens
    [HttpPost("createusertoken")]
    public IActionResult CreateUserToken(IdentityUserToken<string> model)
    {
        if (!_context.Users.Any(u => u.Id == model.UserId))
        {
            return NotFound("User not found");
        }
        var userToken = new IdentityUserToken<string>
        {
            UserId = model.UserId,
            LoginProvider = model.LoginProvider,
            Name = model.Name,
            Value = model.Value
        };

        _context.UserTokens.Add(userToken);
        _context.SaveChanges();
        return Ok($"UserToken {userToken.Value} created");
    }

    // Helpper Checker Infomation duplication: UserName, Phone, Email
    private bool CheckDuplicateCreateUser(UserCustom model)
    {
        return _context.Users.Any(u => u.UserName == model.UserName) ||
               _context.Users.Any(u => u.Email == model.Email) ||
               _context.Users.Any(u => u.Phone == model.Phone);
    }

    // Helpper Checker Infomation duplication: Role Name
    private bool CheckDuplicateCreateRole(IdentityRole model)
    {
        return _context.Roles.Any(r => r.Name == model.Name);
    }
}

