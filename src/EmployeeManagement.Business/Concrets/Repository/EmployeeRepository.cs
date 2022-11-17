using EmployeeManagement.Data.Entities;
using EmployeeManagement.DataAccess.Abstracs.Repository;
using EmployeeManagement.DataAccess.Context;
using EmployeeManagement.Repository.Implementations.Base;

namespace EmployeeManagement.Business.Concrets.Repository;

public class EmployeeRepository : Repository<Employee, string>, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext dbContext) : base(dbContext)
    {

    }
}
