
using BynetApi.Models;
using BynetApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCors();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy",
//        builder => builder.AllowAnyOrigin()
//        .AllowAnyMethod()
//        .AllowAnyHeader());
//});
var connectionString = builder.Configuration.GetConnectionString("BynetDb");
builder.Services.AddDbContext<BynetContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddScoped<IEmployeesService, EmployeesService>();

var app = builder.Build();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));


app.UseAuthorization();

app.MapControllers();
app.Run();