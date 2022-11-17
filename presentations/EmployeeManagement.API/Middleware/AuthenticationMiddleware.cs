namespace EmployeeManagement.API.Middleware;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private const string TOKEN = "token";
    private const string AUTH = "Authorization";
    public AuthenticationMiddleware(RequestDelegate next)
    {
        this._next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        bool header = context.Request.Headers.Any(x => x.Key.Equals(AUTH));
        string? token = context.Request.Cookies[TOKEN];
        if (!header)
        {
            context.Request.Headers.Add("Authorization", "Bearer " + token);
        }
        await _next(context);
    }
}
