namespace EmployeeManagement.Data.Entities;

public class Department : BaseEntity
{
    public Department()
    {
        Employees = new HashSet<Employee>();
        ChildDepartments = new HashSet<Department>();
    }
    public string Name { get; set; } = null!;
    public string? ParentDepartmentId { get; set; }
    public Department? ParentDepartment { get; set; }
    public ICollection<Department> ChildDepartments { get; set; }
    public ICollection<Employee> Employees { get; set; }
}