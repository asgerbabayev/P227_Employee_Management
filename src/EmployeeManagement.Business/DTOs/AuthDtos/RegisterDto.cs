using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Business.DTOs.AuthDtos;

public class RegisterDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;
}
