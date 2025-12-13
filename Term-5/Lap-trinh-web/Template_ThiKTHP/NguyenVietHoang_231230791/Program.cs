using Microsoft.EntityFrameworkCore;
using NguyenVietHoang_231230791.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// * Lệnh Scaffold-DbContext để tạo DbContext và các entity từ cơ sở dữ liệu có thể trông như sau:
//Tool > Connect to Database; 
//Tool > Nuget Package Manager Console rồi chạy dòng lệnh sau:
//Scaffold-DbContext "Server=HOANGNGUYEN\SQLEXPRESS;Database=Tên_Database;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force

// * Điều chỉnh lệnh đăng ký DbContext phù hợp với tên context và chuỗi kết nối của bạn
//builder.Services.AddDbContext<Tên_Context>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
