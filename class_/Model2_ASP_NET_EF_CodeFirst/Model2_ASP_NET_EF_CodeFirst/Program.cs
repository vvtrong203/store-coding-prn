using Microsoft.EntityFrameworkCore;
using Model2_ASP_NET_EF_CodeFirst.Models;
using Module2_ASP_NET_MVC_EF_CodeFirst.Models;

var builder = WebApplication.CreateBuilder(args);

// Bổ sung services vào web container
builder.Services.AddControllersWithViews();
// Thêm DBContext vào WebServer
builder.Services.AddDbContext<PRN222DbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("PRN222"));
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});


// Thêm Session
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddControllersWithViews();

var app = builder.Build();


// Thêm các middlewares cho web server kiểm soát các request (clients)
app.UseSession();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

// Xác định pattern cho phép định tuyến các request
app.MapControllerRoute
(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}"
);

app.Run();