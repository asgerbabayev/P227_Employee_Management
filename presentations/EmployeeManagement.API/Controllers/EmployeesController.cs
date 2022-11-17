using EmployeeManagement.API.Controllers.Base;
using EmployeeManagement.Business.Common;
using EmployeeManagement.Business.Exceptions.NotFoundExceptions;
using EmployeeManagement.Business.Services.Abstracts;
using EmployeeManagement.Business.Services.Concrets;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployeeManagement.API.Controllers;



public class EmployeesController : BaseApiController
{
    private readonly IEmployeesService _service;
    public EmployeesController(IEmployeesService service)
    {
        _service = service;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] string id)
    {
        var result = await _service.GetAsync(id);

        return StatusCode((int)result.StatusCodes, new { result.Data, message = result.Message });

    }
}
