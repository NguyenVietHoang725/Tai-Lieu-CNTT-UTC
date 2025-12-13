USE QLSinhVien

-- 1. Tạo View danh sách sinh viên, gồm các thông tin sau: Mã sinh viên, Họ sinh viên, Tên sinh viên, Học bổng.
CREATE VIEW vTHONGTINSINHVIEN AS
SELECT MaSV, HoSV, TenSV, HocBong
FROM dbo.DSSinhVien sv

SELECT * FROM vTHONGTINSINHVIEN

-- 2. Tạo view Liệt kê các sinh viên có học bổng từ 150,000 trở lên và sinh ở Hà Nội,
-- gồm các thông tin: Họ tên sinh viên, Mã khoa, Nơi sinh, Học bổng.
CREATE VIEW vCau2 AS
SELECT HoSV + TenSV as HoTenSV, MaKhoa, NoiSinh, HocBong
FROM DSSinhVien sv
WHERE HocBong >= 150000 AND NoiSinh = N'Hà Nội'

SELECT * FROM vCau2

-- 3. Tạo view liệt kê những sinh viên nam của khoa Anh văn và khoa tin học,
-- gồm các thông tin Mã sinh viên, Họ tên sinh viên, tên khoa, Phái.
CREATE VIEW vCau3 AS
SELECT MaSV, HoSV + TenSV as HoTenSV, TenKhoa, Phai
FROM DSSinhVien sv
JOIN DMKhoa k ON k.MaKhoa = sv.MaKhoa
WHERE Phai = 'Nam' AND (TenKhoa = N'Anh Văn' OR TenKhoa = N'Tin học')

SELECT * FROM vCau3

-- 4. Tạo view gồm những sinh viên có tuổi từ 20 đến 25, thông tin gồm: Họ tên sinh viên, Tuổi, Tên khoa.
CREATE VIEW vCau4 AS
SELECT HoSV + TenSV as HoTenSV, DATEDIFF(YEAR, sv.NgaySinh, GETDATE()) AS Tuoi, TenKhoa
FROM DSSinhVien sv
JOIN DMKhoa k ON k.MaKhoa = sv.MaKhoa
WHERE DATEDIFF(YEAR, sv.NgaySinh, GETDATE()) BETWEEN 20 AND 25

SELECT * FROM vCau4

-- 5. Tạo view cho biết thông tin về mức học bổng của các sinh viên, gồm: 
-- Mã sinh viên, Phái, Mã khoa, Mức học bổng. Trong đó, mức học bổng sẽ hiển thị là “Học bổng cao”
-- nếu giá trị của field học bổng lớn hơn 500,000 và ngược lại hiển thị là “Mức trung bình”
CREATE VIEW vCau5 AS
SELECT MaSV, Phai, MaKhoa,
	CASE 
		WHEN sv.HocBong > 500000 THEN N'Học bổng cao'
		ELSE N'Mức trung bình'
	END AS MucHocBong
FROM DSSinhVien sv

SELECT * FROM vCau5

-- 6. Tạo view đưa ra thông tin những sinh viên có học bổng lớn hơn bất kỳ học bổng của sinh viên học khóa anh văn
CREATE VIEW vCau6 AS
SELECT MaSV, HoSV + TenSV as HoTenSV
FROM DSSinhVien sv
WHERE HocBong > (
	SELECT MIN(HocBong)
	FROM DSSinhVien sv
	WHERE MaKhoa = 'AV'
)

SELECT * FROM vCau6

-- 7. Tạo view đưa ra thông tin những sinh viên đạt điểm cao nhất trong từng môn.
CREATE VIEW vCau7 AS
SELECT sv.MaSV, HoSV + TenSV as HoTenSV, mh.MaMH, mh.TenMH, kq.Diem
FROM DSSinhVien sv
JOIN KetQua kq ON kq.MaSV = sv.MaSV
JOIN DMMonHoc mh ON mh.MaMH = kq.MaMH
WHERE kq.Diem = (
	SELECT MAX(kq2.Diem)
	FROM KetQua kq2
	WHERE kq2.MaMH = mh.MaMH
)

SELECT * FROM vCau7

-- 8. Tạo view đưa ra những sinh viên chưa thi môn cơ sở dữ liệu.
CREATE VIEW vCau8 AS
SELECT sv.MaSV, HoSV + TenSV as HoTenSV
FROM DSSinhVien sv
WHERE NOT EXISTS (
	SELECT 1
	FROM KetQua kq
	JOIN DMMonHoc mh ON mh.MaMH = kq.MaMH
	WHERE kq.MaSV = sv.MaSV AND mh.TenMH = N'Cơ sở dữ liệu'
)

SELECT * FROM vCau8

-- 9. Tạo view đưa ra thông tin những sinh viên không trượt môn nào.
CREATE VIEW vCau9 AS
SELECT sv.MaSV, HoSV + TenSV as HoTenSV
FROM DSSinhVien sv
WHERE NOT EXISTS (
	SELECT 1
	FROM KetQua kq
	WHERE kq.MaSV = sv.MaSV AND kq.LanThi = 1 AND kq.Diem <= 4
)

SELECT * FROM vCau9