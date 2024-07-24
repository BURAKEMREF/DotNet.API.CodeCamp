using Microsoft.EntityFrameworkCore;
using WebApiProject.Context;
using WebApiProject.Middleware;
using AutoMapper;
using System.Reflection;
using WebApiProject.Interface;
using WebApiProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WebContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("ContentCreatorConnection"), option =>
    {
        option.CommandTimeout(18000); //5 Saat Timeout
        option.EnableRetryOnFailure(
            maxRetryCount: 15,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null
            );
    });
});
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IProductServices, ProductService>();
// Add  This to in the Program.cs file
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());  // Add this line

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
