namespace EmployeeManagement.API.Middleware
{
    public class ExceptionMIddlewware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMIddlewware> _logger;

        public ExceptionMIddlewware(RequestDelegate next, ILogger<ExceptionMIddlewware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //Request
                await _next(context);
                //Response
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
