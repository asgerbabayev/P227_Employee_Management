using EmployeeManagement.Data.Entities;

namespace EmployeeManagement.Business.DTOs.EmployeeDtos;

public class EmployeeGetDto
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public byte Age { get; set; }
    public Department Department { get; set; } = null!;
}
