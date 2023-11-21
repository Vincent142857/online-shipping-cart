using Microsoft.EntityFrameworkCore;
using OnlineShoppingCart.Data;
using Microsoft.AspNetCore.Identity;
using OnlineShoppingCart.Data.Entities;
using OnlineShoppingCart.Core.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("AppConnectionString") ?? throw new InvalidOperationException("Connection string 'AppConnectionString' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();   // xac thuc thông tin đăng nhập
app.UseAuthorization();   // xac dinh thông tin về quyền của User

app.MapControllerRoute(
    name: "areas",
    pattern: "{areas:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();