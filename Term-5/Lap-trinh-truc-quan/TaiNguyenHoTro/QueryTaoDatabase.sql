/*HƯỚNG DẪN
Bước 1: Chạy đoạn: CREATE DATABASE TenCSDL (bôi xanh đoạn này và ấn nút Execute)
Bước 2: Chạy đoạn: USE TenCSDL
Bước 3: Chạy cả file bằng cách nhấn nút Execute
*/

-- Tạo bảng cha
CREATE TABLE TenBang1 (
    Ma1 NVARCHAR(10) PRIMARY KEY,		-- Khóa chính
    Ten1 NVARCHAR(100) NOT NULL			-- Tên
);
GO

CREATE TABLE TenBang2 (
    Ma2 NVARCHAR(10) PRIMARY KEY,		-- Khóa chính                
    Ten2 NVARCHAR(100) NOT NULL,        -- Tên      
    GiaTien DECIMAL(10, 2) NULL,        -- Giá tiền    
    SoLuong INT NOT NULL,               -- Số lượng    
    DuongDanAnh NVARCHAR(255) NULL,     -- Đường dẫn ảnh
    Ngay DATE NOT NULL,				-- Ngày

    -- Khóa ngoại
    Ma1 NVARCHAR(10) NOT NULL, 
    FOREIGN KEY (Ma1) REFERENCES TenBang1(Ma1) 
);
GO

-- Thêm mới dữ liệu
INSERT INTO TenBang1 (Ma1, Ten1)
VALUES 
    (N'...', N'...'), 
    (N'...', N'...'), 
    (N'...', N'...');
GO

INSERT INTO TenBang2 (Ma2, Ten2, GiaTien, SoLuong, DuongDanAnh, Ngay, Ma1)
VALUES 
    (N'...', N'...', 28500000.00, 15, N'/images/dienthoai/ip15.jpg', '2025-10-30', N'...'),
    (N'...', N'...', 12000000.00, 25, N'/images/laptop/lx001.png', '2025-10-25', N'...'),    
    (N'...', N'...', 14000000.00, 50, N'/images/phukien/bp002.jpg', '2025-09-15', N'...'),
    (N'...', N'...', 2500000.00, 30, N'/images/phukien/bp002.jpg', '2025-10-29', N'...');
GO