using EmployeeManagement.Business.Common;
using EmployeeManagement.Business.DTOs.EmployeeDtos;

namespace EmployeeManagement.Business.Services.Abstracts;

public interface IEmployeesService
{
    Task<Response> GetAsync(string id);
}
