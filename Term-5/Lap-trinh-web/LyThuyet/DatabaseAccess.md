# Data Access với Entity Framework trong ASP.NET Core MVC

---

## 1. Entity Framework là gì?

Entity Framework (EF) là một ORM (Object-Relational Mapping) framework giúp:
- Đơn giản hóa việc truy cập dữ liệu từ ứng dụng
- Thực hiện chuyển đổi giữa hệ thống kiểu quan hệ (database) và lập trình hướng đối tượng (C#)
- Loại bỏ việc phải viết nhiều code data-access thủ công

Kiến trúc Entity Framework

![Hình ảnh](./src/entityframework.png)

---

## 2. Các tiếp cận trong Entity Framework

### 2.1. Database-First Approach

Áp dụng khi: Đã có sẵn database

Quy trình:
```
Database có sẵn → Tạo Model classes → Viết Code
```

Ưu điểm:
- Phù hợp với hệ thống legacy
- Database được thiết kế trước bởi DBA

### 2.2. Code-First Approach

Áp dụng khi: Phát triển ứng dụng mới

Quy trình:
```
Code classes → Entity Framework tạo Database → Migration
```


Ưu điểm:
- Tập trung vào business logic
- Dễ dàng version control database
- Phổ biến trong ASP.NET Core MVC

---

## 3. Thực hành với Code-First Approach

