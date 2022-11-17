using EmployeeManagement.Data.Entities;
using EmployeeManagement.DataAccess.Abstracs.Repository;
using EmployeeManagement.DataAccess.Context;
using EmployeeManagement.Repository.Implementations.Base;

namespace EmployeeManagement.Business.Concrets.Repository;

public class DepartmentRepository : Repository<Department, string>, IDepartmentRepository
{
    public DepartmentRepository(AppDbContext dbContext) : base(dbContext)
    {

    }

}
