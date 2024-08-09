using Microsoft.EntityFrameworkCore;
using WebApiProject.Context;
using WebApiProject.Middleware;
using AutoMapper;
using System.Reflection;
using WebApiProject.Interface;
using WebApiProject.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Autofac.Core;
using Microsoft.Extensions.Configuration;
using WebApiProject.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var smtpSettings = builder.Configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
builder.Services.AddSingleton(smtpSettings);
builder.Services.AddTransient<MailService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().WithExposedHeaders("WWW-Authenticate").AllowAnyHeader();
}));

builder.Services.AddSwaggerGen(c =>

{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
     {
         {
             new OpenApiSecurityScheme
             {
                 Reference = new OpenApiReference
                 {
                     Type=ReferenceType.SecurityScheme,
                     Id="Bearer"
                 }
             },
             new string[]{
         }
        } });
});
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
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<WebContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IProductServices, ProductService>();
builder.Services.AddScoped<IAdressServices, AdressService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["AppSettings:ValidIssuer"],
        ValidAudience = builder.Configuration["AppSettings:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Secret"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Manager", policy => policy.RequireRole("Manager"));
    options.AddPolicy("Member", policy => policy.RequireRole("Member"));
    // Aktif kullanýcýlar için policy
    options.AddPolicy("ActiveUserOnly", policy => policy.RequireAssertion(context =>
        context.User.HasClaim(c => c.Type == "IsActive" && c.Value == "1")));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAuthorization();


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

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
