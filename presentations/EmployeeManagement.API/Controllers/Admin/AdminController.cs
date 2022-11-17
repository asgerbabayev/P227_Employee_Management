using EmployeeManagement.API.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers.Admin
{
    public class AdminController : BaseApiController
    {
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello world");
        }
    }
}
