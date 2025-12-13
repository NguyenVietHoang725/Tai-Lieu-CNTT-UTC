USE QLHocSinh

-- 1. Tạo view DSHS10A1 gồm thông tin Mã học sinh, họ tên, giới tính (là “Nữ” nếu Nu=1,
-- ngược lại là “Nam”), các điểm Toán, Lý, Hóa, Văn của các học sinh lớp 10A1
CREATE VIEW DSHS10A1 AS
SELECT 
	hs.MaHS, 
	Ho + Ten AS HoTen, 
	CASE 
		WHEN Nu = 1 THEN 'Nữ'
		ELSE 'Nam'
	END AS GioiTinh, 
	Toan, Ly, Hoa, Van
FROM DSHS hs
JOIN DIEM d ON d.MaHS = hs.MaHS
WHERE MaLop = N'10A1'

SELECT * FROM DSHS10A1

-- 2. Tạo login TranThanhPhong, tạo user TranThanhPhong cho TranThanhPhong trên CSDL QLHocSinh
CREATE LOGIN TranThanhPhong WITH PASSWORD = '123456'
CREATE USER TranThanhPhong FOR LOGIN TranThanhPhong

-- 2.1. Phân quyền Select trên view DSHS10A1 cho TranThanhPhong
GRANT SELECT ON DSHS10A1 TO TranThanhPhong

-- 2.2. Đăng nhập TranThanhPhong để kiểm tra
SELECT * FROM DSHS10A1
DELETE FROM DSHS10A1

-- 2.3. Tạo login PhamVanNam, tạo PhamVanNam cho PhamVanNam trên CSDL QLHocSinh
CREATE LOGIN PhamVanNam WITH PASSWORD = '123456'
CREATE USER PhamVanNam FOR LOGIN PhamVanNam

-- 2.4. Đăng nhập PhamVanNam để kiểm tra
SELECT * FROM DSHS10A1

-- 2.5. Tạo view DSHS10A2 tương tự như câu 1
CREATE VIEW DSHS10A2 AS
SELECT 
	hs.MaHS, 
	Ho + Ten AS HoTen, 
	CASE 
		WHEN Nu = 1 THEN 'Nữ'
		ELSE 'Nam'
	END AS GioiTinh, 
	Toan, Ly, Hoa, Van
FROM DSHS hs
JOIN DIEM d ON d.MaHS = hs.MaHS
WHERE MaLop = N'10A2'

SELECT * FROM DSHS10A2

-- 2.7. Phân quyền Select trên view DSHS10A2 cho PhamVanNam
GRANT SELECT ON DSHS10A2 TO PhamVanNam

-- 2.8. Đăng nhập PhamVanNam để kiểm tra
SELECT * FROM DSHS10A2

-- 3. Tạo view báo cáo Kết thúc năm học gồm các thông tin: Mã học sinh, Họ và tên, Ngày sinh,
-- Giới tính, Điểm Toán, Lý, Hóa, Văn, Điểm Trung bình, Xếp loại, Sắp xếp theo xếp loại (chọn
-- 1000 bản ghi đầu). Trong đó:
-- Điểm trung bình (DTB) = ((Toán + Văn)*2 + Lý + Hóa)/6)
-- Cách thức xếp loại như sau:
-- - Xét điểm thấp nhất (DTN) của các 4 môn
-- - Nếu DTB>5 và DTN>4 là “Lên Lớp”, ngược lại là lưu ban
CREATE VIEW BaoCao_KTNH AS
SELECT TOP 1000
	hs.MaHS, 
	HO + ' ' + TEN AS HoTen, 
	NGAYSINH,
	CASE 
		WHEN Nu = 1 THEN 'Nữ'
		ELSE 'Nam'
	END AS GioiTinh, 
	Toan, Ly, Hoa, Van,
	(( (d.Toan + d.Van)*2.0 + d.Ly + d.Hoa ) / 6.0) AS DiemTB,
	CASE	
		WHEN (( (d.Toan + d.Van)*2.0 + d.Ly + d.Hoa ) / 6.0) > 5 
			AND (SELECT MIN(v)
					FROM (VALUES (Toan), (Van), (Ly), (Hoa)) AS DiemTN(v)) > 4 
		THEN N'Lên lớp'
		ELSE N'Lưu ban'
	END AS XepLoai
FROM DSHS hs
JOIN DIEM d ON d.MaHS = hs.MaHS
ORDER BY XepLoai

SELECT * FROM BaoCao_KTNH

-- 4. Tạo view danh sách HOC SINH XUAT SAC bao gồm các học sinh có DTB>=8.5 và DTN>=8 
-- với các trường: Lop, Mahs, Hoten, Namsinh (năm sinh), Nu, Toan, Ly, Hoa, Van, DTN, DTB
CREATE VIEW HSXS AS
SELECT
	MALOP,
	hs.MAHS,
	HO + ' ' + TEN AS HOTEN,
	YEAR(NGAYSINH) AS NAMSINH, 
	NU,
	TOAN, LY, HOA, VAN,
	(SELECT MIN(v) FROM (VALUES (Toan), (Van), (Ly), (Hoa)) AS DiemTN(v)) AS DTN,
	(( (d.Toan + d.Van)*2.0 + d.Ly + d.Hoa ) / 6.0) AS DTB
FROM DSHS hs
JOIN DIEM d ON d.MaHS = hs.MaHS

SELECT * FROM HSXS

-- 5. Tạo view danh sách HOC SINH DAT THU KHOA KY THI bao gồm các học sinh xuất sắc 
-- có DTB lớn nhất với các trường: Lop, Mahs, Hoten, Namsinh, Nu, Toan, Ly, Hoa, Van, DTB

CREATE VIEW HSTKKT AS
SELECT
	MALOP,
	hs.MAHS,
	HO + ' ' + TEN AS HOTEN,
	YEAR(NGAYSINH) AS NAMSINH, 
	NU,
	TOAN, LY, HOA, VAN,
	(( (d.Toan + d.Van)*2.0 + d.Ly + d.Hoa ) / 6.0) AS DTB
FROM DSHS hs
JOIN DIEM d ON d.MaHS = hs.MaHS
WHERE (( (d.Toan + d.Van)*2.0 + d.Ly + d.Hoa ) / 6.0) = 
      (
        SELECT MAX(( (d2.Toan + d2.Van)*2.0 + d2.Ly + d2.Hoa ) / 6.0)
        FROM DIEM d2
      );

SELECT * FROM HSTKKT