using FluentValidation;
using Auth.Domain.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Auth.Application.Repository;
using Auth.Infrastructure.Repository;
using Auth.Application.Services;
using Auth.Infrastructure.Services;
using Auth.Domain.Entities;
using Auth.Api.Common;
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

builder.Services.Configure<ApiBehaviorOptions>(options=>{
    options.SuppressModelStateInvalidFilter=true;
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