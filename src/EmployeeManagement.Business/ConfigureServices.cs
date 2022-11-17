using EmployeeManagement.Business.Concrets.UnitOfWork;
using EmployeeManagement.Business.Services.Abstracts;
using EmployeeManagement.Business.Services.Concrets;
using EmployeeManagement.Data.Identity;
using EmployeeManagement.DataAccess.Abstracs.UnitOfWork;
using EmployeeManagement.DataAccess.Context;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace EmployeeManagement.Business;

public static class ConfigureServices
{
    public static IServiceCollection AddBusiness(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddIdentity<AppUser, AppRole>(x =>{
            x.Password.RequireUppercase = false;
            x.Password.RequireLowercase = false;
            x.Password.RequireDigit = false;
            x.Password.RequiredLength = 2;
            x.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<AppDbContext>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidIssuer = configuration["Jwt:issuer"],
                ValidAudience = configuration["Jwt:audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:securityKey"]))
            };
        });

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IEmployeesService, EmployeesService>();
        return services;
    }
}
