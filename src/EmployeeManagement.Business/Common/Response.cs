using System.Net;

namespace EmployeeManagement.Business.Common;

public class Response
{
    public HttpStatusCode StatusCodes { get; set; }
    public string? Message { get; set; } = null!;
    public object? Data { get; set; }
}
