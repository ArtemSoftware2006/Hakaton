using DAL;
using DAL.Interfaces;
using DAL.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Service.Impl;
using Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var conf_builder = new ConfigurationBuilder();

conf_builder.SetBasePath(Directory.GetCurrentDirectory());
conf_builder.AddJsonFile("appsettings.json");
var config = conf_builder.Build();

var connection = config.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 31))));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Index";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
    {
        policy.RequireRole("Admin");
    });
    options.AddPolicy("Employer", policy =>
    {
        policy.RequireRole("Employer");
    }); 
    options.AddPolicy("Creator", policy =>
    {
        policy.RequireRole("Creator");
    }); 
    options.AddPolicy("Executor", policy =>
    {
        policy.RequireRole("Executor");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
