using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TravelAgencyBackend.Application.Interfaces.Services;
using TravelAgencyBackend.Application.UseCases.Auth.Login;
using TravelAgencyBackend.Application.UseCases.Auth.Register;
using TravelAgencyBackend.Application.Validators;
using TravelAgencyBackend.Domain.Entities;
using TravelAgencyBackend.Infrastructure.Persistence;
using TravelAgencyBackend.Infrastructure.Services;
using TravelAgencyBackend.Presentation.Controllers;
using TravelAgencyBackend.Presentation.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Inject Fluent Validation for LoginModel
builder.Services.AddScoped<IValidator<LoginModel>, LoginValidator>();

// Inject Fluent Validation for RegisterModel
builder.Services.AddScoped<IValidator<RegisterModel>, RegisterValidator>();

// Add application services
builder.Services.AddScoped<IAuthService, AuthService>();

// Add authorization services
builder.Services.AddAuthorization();

// Add Use Case implementations
builder.Services.AddScoped<ILoginUseCase, LoginUseCase>(); 
builder.Services.AddScoped<IRegisterUseCase, RegisterUseCase>(); 

// Add controller services
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Travel Agency API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Travel Agency API v1"));
}

app.UseHttpsRedirection();

// Enable authorization middleware
app.UseAuthorization();

app.MapControllers();

app.Run();
