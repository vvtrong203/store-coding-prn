var builder = WebApplication.CreateBuilder(args);

//Bổ sung các service vào web controller
builder.Services.AddControllersWithViews();

var app = builder.Build();
//Thêm các middleware cho web server kiểm soát các request cho client
app.UseStaticFiles();
app.UseRouting();

//Xác định pattern cho phép định tuyến các request

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );



app.Run();
