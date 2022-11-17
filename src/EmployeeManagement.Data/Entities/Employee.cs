namespace EmployeeManagement.Data.Entities;

public class Employee : BaseAuditable
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public byte Age { get; set; }
    public string DepartmentId { get; set; } = null!;
    public Department Department { get; set; } = null!;
}