using Library_Manager.Interfaces;
using Library_Manager.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//lấy chuỗi kết nối(connectionstring) từ file appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//đăng ký DbContext với chuỗi kết nối đã lấy được
builder.Services.AddDbContext<Library_Manager.Models.QlthuVienContext>(options =>
    options.UseSqlServer(connectionString));

//thêm dịch vụ session
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache(); // Thêm dịch vụ lưu cache session trong bộ nhớ
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Đăng ký Service với Lifetime phù hợp (Singleton, Scoped, Transient)
// Transient thường phù hợp cho các service xử lý tác vụ ngắn như upload file
builder.Services.AddTransient<IBufferedFileUploadService, BufferedFileUploadLocalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Tong-quan/Loi");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
//kích hoạt sử dụng session
app.UseSession();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
//pattern: "{controller=Home}/{action=Index}/{id?}");
//phải đăng nhập trước khi vào trang Home
pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
