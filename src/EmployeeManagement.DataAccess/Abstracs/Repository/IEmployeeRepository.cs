using EmployeeManagement.Data.Entities;
using EmployeeManagement.Repository.Interfaces.Base;

namespace EmployeeManagement.DataAccess.Abstracs.Repository;

public interface IEmployeeRepository : IRepository<Employee, string>
{

}
