using OnlineBusTicketReservationSystem.Models;
using OnlineBusTicketReservationSystem.Interface;
using OnlineBusTicketReservationSystem.Services;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Session;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    //options.Cookie.HttpOnly = true;
    //options.Cookie.IsEssential = true;
});


builder.Services.AddAuthentication
    (
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Home/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });


builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddRazorPages();



var mycon = builder.Configuration.GetConnectionString("cs_Bus");
builder.Services.AddDbContext<MyContext>(options => options.UseSqlServer(mycon));
builder.Services.AddTransient<IRepository<tbl_bookedSeat>, Repository<tbl_bookedSeat>>();
builder.Services.AddTransient<IRepository<tbl_bus>, Repository<tbl_bus>>();
builder.Services.AddTransient<IRepository<tbl_busSeats>, Repository<tbl_busSeats>>();
builder.Services.AddTransient<IRepository<tbl_discount>, Repository<tbl_discount>>();
builder.Services.AddTransient<IRepository<tbl_sale>, Repository<tbl_sale>>();
builder.Services.AddTransient<IRepository<tbl_user>, Repository<tbl_user>>();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
