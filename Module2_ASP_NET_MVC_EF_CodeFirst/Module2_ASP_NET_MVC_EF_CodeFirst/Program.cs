using Microsoft.EntityFrameworkCore;
using Module2_ASP_NET_MVC_EF_CodeFirst.Models;

var builder = WebApplication.CreateBuilder(args);

// Bổ sung services vào web container
builder.Services.AddControllersWithViews();
// Thêm DBContext vào WebServer
builder.Services.AddDbContext<PRN222DBContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("PRN222"));
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

// Thêm các middlewares cho web server kiểm soát các request (clients)
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

// Xác định pattern cho phép định tuyến các request
app.MapControllerRoute
    (
        name: "default",
        pattern: "{controller=Product}/{action=Index}/{id?}"
    );

app.Run();
