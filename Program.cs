using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TravelAgencyBackend.Data;
using TravelAgencyBackend.Models;
using TravelAgencyBackend.validators;
using TravelAgencyBackend.Validators; // Added namespace for validators

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Inject Fluent Validation for LoginModel
builder.Services.AddScoped<IValidator<LoginModel>, LoginValidator>();

// Inject Fluent Validation for RegisterModel
builder.Services.AddScoped<IValidator<RegisterModel>, RegisterValidator>();

// Add authorization services
builder.Services.AddAuthorization();

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
