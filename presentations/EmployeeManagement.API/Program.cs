using EmployeeManagement.API.Middleware;
using EmployeeManagement.Business;
using EmployeeManagement.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddBusiness(builder.Configuration);

builder.Services.AddLogging();
builder.Logging.ClearProviders();

builder.Logging.AddDebug();
builder.Logging.AddConsole();

var app = builder.Build();

//app.UseMiddleware<ExceptionMIddlewware>();
app.UseMiddleware<AuthenticationMiddleware>();

//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Hello");
//});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.Use(async (context, next) =>
//{
//    //Request
//    try
//    {
//    await next(context);

//    }
//    catch (global::System.Exception )
//    {

//        throw;
//    }
//    //Response
//});

//app.Use(async (context, next) =>
//{
//    await next(context);
//});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
