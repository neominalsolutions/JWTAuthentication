using JWTAuthentication.Data;
using JWTAuthentication.Data.Identity;
using JWTAuthentication.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddMediatR(

  opt => { 
    opt.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
  }

);


var key = Encoding.ASCII.GetBytes(JwtSettings.SecretKey);


builder.Services.AddAuthentication(x =>
{
  x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,opt =>
{
  opt.RequireHttpsMetadata = true;
  opt.SaveToken = true;
  opt.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = false,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ValidateLifetime = true, // 1 saat boyunca sadece validate et, expire olmuþ tokenlarý validate etmez
    ValidateAudience = false
  };
});

builder.Services.AddDbContext<AppIdentityDbContext>(opt =>
{
  opt.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDb"));
});

// identity özellikleri
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
{
  opt.Lockout.MaxFailedAccessAttempts = 10;
  opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30); // Deafult 5
  opt.SignIn.RequireConfirmedEmail = true;
  opt.Password.RequireDigit = true;
  opt.Password.RequireLowercase = true;
  opt.Password.RequireUppercase = true;
  opt.Password.RequiredLength = 8;

}).AddEntityFrameworkStores<AppIdentityDbContext>();

builder.Services.AddAuthorization();

builder.Services.AddScoped<IJwtService, MicrosoftJwtBearerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
