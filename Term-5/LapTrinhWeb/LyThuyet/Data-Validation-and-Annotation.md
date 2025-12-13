# Data Validation and Annotation
---
## 1. Data Validation (Kiểm tra dữ liệu) trong ASP.NET MVC
Đây là quy trình bắt buộc để kiểm tra xem dữ liệu người dùng nhập vào (qua URL, form, link...) có hợp lệ và an toàn trước khi ứng dụng xử lý.

### Mục đích chính

- **Đảm bảo an toàn**: Ngăn chặn các cuộc tấn công (như Cross-Site Scripting).
- **Đảm bảo tính chính xác**: Dữ liệu phải đúng định dạng và quy tắc mà ứng dụng yêu cầu (ví dụ: email phải có ký tự `@`, mật khẩu phải đủ dài).
- **Cung cấp phản hồi**: Thông báo lỗi rõ ràng để người dùng biết và sửa lại trước khi gửi lại.

### Quy trình Validation tiêu chuẩn
Khi dữ liệu từ người dùng gửi đến server, nó sẽ trải qua các bước kiểm tra tự động:
1. **Kiểm tra bảo mật**: Hệ thống đầu tiên quét để phát hiện các dấu hiệu tấn công. (Nếu là tấn công => Ném ra lỗi (exception)).
2. **Kiểm tra hợp lệ**: Nếu an toàn, dữ liệu sẽ được so sánh với các quy tắc validation của ứng dụng.
- Nếu tất cả dữ liệu hợp lệ → Request được chuyển để xử lý tiếp.
- Nếu có bất kỳ dữ liệu nào không hợp lệ → Trả về thông báo lỗi cho người dùng.
3.  **Manual Validation** (Kiểm tra thủ công)
Đôi khi bạn cần kiểm tra những điều kiện phức tạp, không có sẵn. Khi đó, bạn có thể tự viết code validation trực tiếp trong **Controller**.

Ví dụ 1: Kiểm tra độ dài mật khẩu
```csharp
[HttpPost]
public ActionResult Index(User model) {
    // Lấy mật khẩu từ model
    string modelPassword = model.password; 
    
    // Validation thủ công: Kiểm tra nếu mật khẩu ngắn hơn 7 ký tự
    if (modelPassword.Length < 7) {
        // Trả về lại view để hiển thị lỗi
        return View(); 
    }
    else {
        // Nếu hợp lệ, tiến hành đăng ký
        return Content("Đăng ký thành công!");
    }
}
```

Ví dụ 2: Kiểm tra định dạng Email bằng Biểu thức chính quy (Regex)
```csharp
[HttpPost]
public ActionResult Index(User model) {
    string modelEmail = model.email;
    // Mẫu regex để check định dạng email
    string pattern = @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"; 
    
    // Kiểm tra xem chuỗi email có khớp với pattern không
    if (Regex.IsMatch(modelEmail, pattern)) { 
        // Email hợp lệ, tiến hành xử lý
        return Content("Đăng ký thành công!");
    }
    else {
        // Email không hợp lệ, trả về view
        return View(); 
    }
}
```

---
## 2. Data Annotations là gì ?

Data Annotations là các thuộc tính (attributes) mà bạn có thể đặt trực tiếp lên các property của Model để khai báo các quy tắc validation và cách thức hiển thị một cách tự động. Chúng giúp việc kiểm tra dữ liệu trở nên dễ dàng, gọn gàng và nhất quán.

### Các Annotation Validation phổ biến

