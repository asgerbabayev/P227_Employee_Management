using EmployeeManagement.Business.Common;
using EmployeeManagement.Business.DTOs.AuthDtos;

namespace EmployeeManagement.Business.Services.Abstracts;

public interface IAuthService
{
    Task<Response> RegisterAsync(RegisterDto registerDto);
    Task<Response> LoginAsync(LoginDto loginDto);
    Task CreateRoleAsync();
}
