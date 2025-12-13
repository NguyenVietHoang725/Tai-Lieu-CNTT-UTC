USE QLSinhVien

-- 1. Tạo login Login1, tạo User1 cho Login1
CREATE LOGIN Login1 WITH PASSWORD = '123456'
CREATE USER User1 FOR LOGIN Login1

-- 2. Phân quyền Select trên bảng DSSinhVien cho User1
GRANT SELECT ON DSSinhVien TO User1

-- 3. Đăng nhập để kiểm tra
SELECT * FROM DSSinhVien
UPDATE DSSinhVien
SET HocBong = HocBong + 20000
WHERE MaSV = 'A01';

-- 4. Tạo login Login2, tạo User2 cho Login2
CREATE LOGIN Login2 WITH PASSWORD = '123456'
CREATE USER User2 FOR LOGIN Login2

-- 5. Phân quyền Update trên bảng DSSinhVien cho User2, người này có thể cho phép người khác sử dụng quyền này
GRANT UPDATE ON DSSinhVien TO User2 WITH GRANT OPTION

-- 6. Đăng nhập dưới Login2 và trao quyền Update trên bảng DSSinhVien cho User 1
GRANT UPDATE ON DSSinhVien TO User1

-- 7. Đăng nhập Login 1 để kiểm tra
UPDATE DSSinhVien
SET HocBong = HocBong + 20000
WHERE MaSV = 'A01';
