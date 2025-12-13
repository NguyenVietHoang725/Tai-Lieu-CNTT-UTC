# Controller trong ASP.NET Core MVC

---

## 1. Controller là gì ?

Controller là lớp trung tâm trong mô hình MVC, có nhiệm vụ:
- Xử lý yêu cầu từ trình duyệt
- Lấy dữ liệu từ Model
- Gọi View template để trả về response cho người dùng

Ví dụ:
```csharp
public class HelloWorldController : Controller
{
    // GET: /HelloWorld/
    public IActionResult Index()
    {
        return View();
    }

    // GET: /HelloWorld/Welcome/
    public IActionResult Welcome(string name, int numTimes = 1)
    {
        ViewData["Message"] = "Xin chào " + name;
        ViewData["NumTimes"] = numTimes;
        return View();
    }
}
```

---

## 2. HTTP Endpoint & URL Structure

HTTP Endpoint là URL có thể truy cập được trong ứng dụng web:

```
https://localhost:5001/HelloWorld/Welcome?name=John&numtimes=3
```

Phân tích thành phần:
- Protocol: HTTPS
- Server location: localhost:5001
- Target URI: HelloWorld/Welcome
- Query string: name=John&numtimes=3

---

## 3. Các kiểu trả về phổ biến (IActionResult)

### 3.1 View Result

```csharp
public IActionResult Index()
{
    return View(); // Trả về View mặc định
}

public IActionResult About()
{
    return View("AboutUs"); // Trả về View cụ thể
}
```

### 3.2. Redirect Results

```csharp
public IActionResult Login()
{
    // Chuyển hướng đến action khác
    return RedirectToAction("Dashboard", "Home");
}

public IActionResult External()
{
    // Chuyển hướng đến URL bên ngoài
    return Redirect("https://google.com");
}
```

### 3.3. JSON Result

```csharp
public IActionResult GetUser()
{
    var user = new { Name = "John", Age = 30 };
    return Json(user); // Trả về dữ liệu JSON
}
```

### 3.4. Content Result

```csharp
public IActionResult PlainText()
{
    return Content("Đây là nội dung văn bản thuần túy");
}
```

### 3.5. File Result

```csharp
public IActionResult DownloadFile()
{
    var fileBytes = System.IO.File.ReadAllBytes("path/to/file.pdf");
    return File(fileBytes, "application/pdf", "document.pdf");
}
```

---

## 4. Routing - Định tuyến URL

### 4.1. Routing là gì?

Routing là cơ chế ánh xạ URL đến các Controller và Action tương ứng.

### 4.2. Các cách định tuyến phổ biến

Convention-based Routing (Trong Program.cs)

```csharp
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "blog",
    pattern: "blog/{action=Index}/{id?}",
    defaults: new { controller = "Blog" });
```

Attribute Routing

```csharp
[Route("products")]
public class ProductController : Controller
{
    [Route("")]
    [Route("index")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("details/{id:int}")]
    public IActionResult Details(int id)
    {
        return View();
    }

    [Route("search/{category?}")]
    public IActionResult Search(string category = "all")
    {
        return View();
    }
}
```

---

## 5. Truyền tham số từ URL đến Controller

### 5.1. Truyền tham số qua Query String

```csharp
// URL: /HelloWorld/Welcome?name=John&numtimes=3
public IActionResult Welcome(string name, int numTimes = 1)
{
    // Sử dụng HtmlEncoder để bảo vệ khỏi XSS
    var safeName = HtmlEncoder.Default.Encode(name);
    ViewData["Message"] = $"Xin chào {safeName}, Số lần: {numTimes}";
    return View();
}
```

### 5.2. Truyền tham số qua Route Data

```csharp
// URL: /HelloWorld/Welcome/John/3
[Route("HelloWorld/Welcome/{name}/{id?}")]
public IActionResult Welcome(string name, int id = 1)
{
    var safeName = HtmlEncoder.Default.Encode(name);
    return Content($"Xin chào {safeName}, ID: {id}");
}
```

### 5.3. Model Binding

```csharp
public class UserModel
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public IActionResult CreateUser(UserModel user)
{
    // ASP.NET Core tự động bind dữ liệu từ form/query vào object
    return Json(new { 
        message = $"Đã tạo user: {user.Name}, tuổi: {user.Age}" 
    });
}
```

---


