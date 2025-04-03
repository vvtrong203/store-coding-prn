using Microsoft.EntityFrameworkCore;
using Topic_3_ASP_DOT_NET_RAZOR_WORKEF_DBFRIST.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<Prn22dbfContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DBConnect"))
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();