using EmployeeManagement.DataAccess.Abstracs.Repository;

namespace EmployeeManagement.DataAccess.Abstracs.UnitOfWork;

public interface IUnitOfWork
{
    IEmployeeRepository EmployeeRepository { get; }
    IDepartmentRepository DepartmentRepository { get; }
    Task<int> SaveChangesAsync();
}
