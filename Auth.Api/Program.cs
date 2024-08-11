using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Auth.Domain.Data;
using Auth.Application.Repository;
using Auth.Infrastructure.Repository;
using Auth.Application.Services;
using Auth.Infrastructure.Services;
using Auth.Domain.Entities;
using Auth.Api.Common;
using Auth.Api.Settings;
using Auth.Api.Extensions;
using Auth.Api.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
throw new InvalidOperationException("Invalid databse connection string");

builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    // assign null value for naming policy to make them lowercases.
    options.JsonSerializerOptions.PropertyNamingPolicy = new FlatCasePolicy();
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(connectionString, config =>
    {
        var isDevelopment = builder.Environment.IsDevelopment();
        if (isDevelopment) config.MigrationsAssembly("Auth.Api");
    });
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>().
AddDefaultTokenProviders();

// Bind jwt config from app-settings.json
builder.Services.Configure<JwtConfigSettings>(
    builder.Configuration.GetRequiredSection(nameof(JwtConfigSettings))
);
builder.Services.AddScoped<JwtService>();

// Add JwtServiceConfig 
builder.Services.AddJwtAuthConfig();
builder.Services.AddSingleton<JwtService>();

// Disable defautl model validation
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// user
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// role
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();

// product
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

// Login
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<ILoginService, LoginService>();

// add fluent vlaidtions to DI
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);


builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Permission based auth in ASP.NET Core",
        Contact = new OpenApiContact
        {
            Email = "youremail@email.com"
        }
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();