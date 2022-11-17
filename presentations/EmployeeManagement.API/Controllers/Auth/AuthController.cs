using EmployeeManagement.API.Controllers.Base;
using EmployeeManagement.Business.DTOs.AuthDtos;
using EmployeeManagement.Business.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers.Auth
{
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) =>
            _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            return StatusCode((int)result.StatusCodes, new { result.Message, result.Data });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            return StatusCode((int)result.StatusCodes, new { result.Message, result.Data });
        }

        [HttpGet("add-role")]
        public async Task CreateRole()
        {
            await _authService.CreateRoleAsync();
        }
    }
}
