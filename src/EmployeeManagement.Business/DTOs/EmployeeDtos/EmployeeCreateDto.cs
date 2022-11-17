namespace EmployeeManagement.Business.DTOs.EmployeeDtos;

public class EmployeeCreateDto
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public byte Age { get; set; }
    public string DepartmentId { get; set; } = null!;
}
