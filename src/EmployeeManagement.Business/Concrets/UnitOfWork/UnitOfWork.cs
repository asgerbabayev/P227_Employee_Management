using EmployeeManagement.Business.Concrets.Repository;
using EmployeeManagement.DataAccess.Abstracs.Repository;
using EmployeeManagement.DataAccess.Abstracs.UnitOfWork;
using EmployeeManagement.DataAccess.Context;

namespace EmployeeManagement.Business.Concrets.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private IEmployeeRepository? _employeeRepository;
    private IDepartmentRepository? _departmentRepository;
    public IEmployeeRepository EmployeeRepository => _employeeRepository ??= new EmployeeRepository(_dbContext);
    public IDepartmentRepository DepartmentRepository => _departmentRepository ??= new DepartmentRepository(_dbContext);

    public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}
