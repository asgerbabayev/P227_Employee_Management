using AutoMapper;
using EmployeeManagement.Business.DTOs.DepartmentDtos;
using EmployeeManagement.Business.DTOs.EmployeeDtos;
using EmployeeManagement.Data.Entities;

namespace EmployeeManagement.Business.Profiles;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Department, DepartmentGetDto>();

        CreateMap<Employee, EmployeeGetDto>();
        CreateMap<EmployeeCreateDto, Employee>();
    }
}
