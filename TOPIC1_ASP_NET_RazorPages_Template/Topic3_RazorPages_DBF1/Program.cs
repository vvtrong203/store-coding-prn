using Microsoft.EntityFrameworkCore;
using Topic3_RazorPages_DBF1.Hubs;
using Topic3_RazorPages_DBF1.Modelss;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Prn222dbFirstContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DBConnect"))
);

// Cấu hình Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian timeout của session
    options.Cookie.HttpOnly = true;  // Bảo mật cookie session
    options.Cookie.IsEssential = true; // Đảm bảo session không bị chặn bởi GDPR consent
});

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // ✅ Bắt buộc để kích hoạt session middleware

app.UseAuthorization();

app.MapRazorPages();

// Redirect root to a specific Razor page:
app.MapGet("/", context =>
{
    context.Response.Redirect("/HomePage/Login");
    return Task.CompletedTask;
});

app.MapHub<SignalRServer>("/hub");

app.Run();