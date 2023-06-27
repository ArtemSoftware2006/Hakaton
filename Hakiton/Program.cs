using DAL;
using DAL.Interfaces;
using DAL.Repository;
using Hakiton;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Service.Impl;
using Service.Interfaces;
using Services.Impl;
using Services.Interfaces;

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
builder.Services.AddScoped<IDealRepository, DealRepository>();
builder.Services.AddScoped<IDealService, DealService>();
builder.Services.AddScoped<IProposalRepository, ProposalRepository>();
builder.Services.AddScoped<IProposalService, ProposalService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<IApprovedDealService, ApprovedDealService>();

builder.Services.AddControllers();

var AllowAllOrigins = "AllowAll";

builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowAllOrigins, builder =>
    {
        builder.WithOrigins("http://localhost:3000")
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        SaveSigninToken = false,
        ValidateIssuer = false,

        ValidateAudience = false,
        ValidateLifetime = false,

        IssuerSigningKey = AuthTokenOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = false,
    };
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

app.UseCors(AllowAllOrigins);
// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
