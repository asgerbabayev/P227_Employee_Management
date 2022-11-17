using EmployeeManagement.Business.Common;
using EmployeeManagement.Business.Constants;
using EmployeeManagement.Business.DTOs.AuthDtos;
using EmployeeManagement.Business.Services.Abstracts;
using EmployeeManagement.Data.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Business.Services.Concrets;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _accessor;

    private const string TOKEN = "token";

    public AuthService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration configuration, IHttpContextAccessor accessor)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _accessor = accessor;
    }

    public async Task CreateRoleAsync()
    {
        await _roleManager.CreateAsync(new AppRole { Name = "Admin" });
        await _roleManager.CreateAsync(new AppRole { Name = "Member" });
    }

    public async Task<Response> LoginAsync(LoginDto loginDto)
    {
        AppUser existUser = await _userManager.FindByEmailAsync(loginDto.Email);
        if (existUser is null) return new Response
        {
            Data = null,
            Message = Messages.UserNotFound,
            StatusCodes = HttpStatusCode.NotFound
        };

        IList<string> roles = await _userManager.GetRolesAsync(existUser);

        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, existUser.Id),
            new Claim(ClaimTypes.Name, existUser.UserName),
            new Claim(ClaimTypes.Email, existUser.Email)
        };

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["Jwt:securityKey"]));
        
        SigningCredentials signingCredentials = new(key, SecurityAlgorithms.HmacSha256);

        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        DateTime expires = DateTime.Now.AddMinutes(20);

        JwtSecurityToken securityToken = new(
            issuer: _configuration["Jwt:issuer"],
            audience: _configuration["Jwt:audience"],
            claims: claims,
            signingCredentials: signingCredentials,
            expires: expires
            );

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        _accessor?.HttpContext?.Response.Cookies.Append(TOKEN, token,new CookieOptions
        {
            HttpOnly = true,
            Secure = true
        });

        return new Response
        {
            Data = existUser.Id,
            Message = Messages.Successfully,
            StatusCodes = HttpStatusCode.OK,
        };
    }

    public async Task<Response> RegisterAsync(RegisterDto registerDto)
    {
        AppUser existUser = await _userManager.FindByEmailAsync(registerDto.Email);
        if (existUser is not null) return new Response
        {
            Data = null,
            Message = Messages.UserAlreadyExist,
            StatusCodes = HttpStatusCode.Forbidden
        };

        AppUser user = new()
        {
            Email = registerDto.Email,
            UserName = registerDto.Email.Substring(0, registerDto.Email.IndexOf("@")) + Guid.NewGuid()
        };

        IdentityResult resultCreateUser = await _userManager.CreateAsync(user, registerDto.Password);

        string? description = resultCreateUser.Errors.Select(d => d.Description).FirstOrDefault();
        if (!resultCreateUser.Succeeded) return new Response { Message = description, StatusCodes = HttpStatusCode.Forbidden };

        IdentityResult resultAddRole = await _userManager.AddToRoleAsync(user, "Member");
        if (!resultAddRole.Succeeded) return new Response { Message = description, StatusCodes = HttpStatusCode.Forbidden };

        return new Response { StatusCodes = HttpStatusCode.OK };
    }


}
