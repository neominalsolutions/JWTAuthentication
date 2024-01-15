using JWTMvcClient.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// birden fazla api varsa tek tek sisteme tanıtılır.
builder.Services.AddHttpClient("IdentityService", opt =>
{
  opt.BaseAddress = new Uri("https://localhost:7093/api/");
});

builder.Services.AddHttpClient("BService", opt =>
{

});

var key = Encoding.ASCII.GetBytes(JwtSettings.SecretKey); // api ile aynı key


builder.Services.AddAuthentication(x =>
{
  // Cookies denlen bir þema ile 
  x.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
  x.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(x => {

  x.LoginPath = "/Account/Login";
  x.LogoutPath = "/Account/LogOut";
  x.SlidingExpiration = true;
  x.ExpireTimeSpan = TimeSpan.FromDays(1);
});


builder.Services.AddAuthorization(opt =>
{
  opt.AddPolicy("OnlyAdminManager",policy =>
  {
    policy.RequireAuthenticatedUser();
    policy.RequireRole("Manager");
    policy.RequireRole("Admin");
  });
}); // [Authorize attributelar çalışsın diye açtık]

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); // kimlik doğrulamayı aktif hale getir.
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
  endpoints.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
  );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
