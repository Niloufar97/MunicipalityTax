using MunicipalityTax.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using MunicipalityTax.Services;
using MunicipalityTax.Repositories;
using MunicipalityTax.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddOpenApi();

builder.Services.AddControllers();

// The connection string is retrieved from the appsettings.json file under "ConnectionStrings:DefaultConnection"
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITaxRepository, TaxRepository>();
builder.Services.AddScoped<ITaxService, TaxService>();
builder.Services.AddScoped<MunicipalityService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
//Automatically redirects HTTP requests to HTTPS.
app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
