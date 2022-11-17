using AutoMapper;
using EmployeeManagement.Business.Common;
using EmployeeManagement.Business.DTOs.EmployeeDtos;
using EmployeeManagement.Business.Exceptions.NotFoundExceptions;
using EmployeeManagement.Business.Services.Abstracts;
using EmployeeManagement.Data.Entities;
using EmployeeManagement.DataAccess.Abstracs.UnitOfWork;
using System.Net;

namespace EmployeeManagement.Business.Services.Concrets;

public class EmployeesService : IEmployeesService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public EmployeesService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Response> GetAsync(string id)
    {
        Employee? employee = await _unitOfWork.EmployeeRepository.GetAsync(id);
        if (employee is null)
        {
            return new Response
            {
                Data = null,
                Message = "Not found",
                StatusCodes = HttpStatusCode.NotFound
            };
        }
        return new Response
        {
            Data = _mapper.Map<EmployeeGetDto>(employee),
            StatusCodes = HttpStatusCode.OK
        };
    }
}
