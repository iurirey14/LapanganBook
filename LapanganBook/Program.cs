// ===== ORANG 1: Konfigurasi =====
using Microsoft.AspNetCore.Identity; using Microsoft.EntityFrameworkCore;
var b = WebApplication.CreateBuilder(args);
b.Services.AddControllersWithViews();
b.Services.AddDbContext<AppDbContext>(o => o.UseSqlite("Data Source=app.db"));
b.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
b.Services.AddAuthentication("Cookies").AddCookie("Cookies", o => { o.LoginPath="/Account/Login"; o.AccessDeniedPath="/Account/Login"; });
var app = b.Build();
using(var s = app.Services.CreateScope()){ var db=s.ServiceProvider.GetRequiredService<AppDbContext>(); db.Database.EnsureCreated(); Seed.Run(db, s.ServiceProvider.GetRequiredService<IPasswordHasher<User>>()); }
app.UseStaticFiles(); app.UseRouting(); app.UseAuthentication(); app.UseAuthorization();
app.MapDefaultControllerRoute(); app.Run();
