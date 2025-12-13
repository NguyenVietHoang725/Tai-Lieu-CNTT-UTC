use QLBanSach
-- View
-- 1.Tạo view in ra danh sách các sách của nhà xuất bản giáo dục nhập trong năm 2021
CREATE VIEW vCau1 AS
SELECT s.MaSach, s.TenSach
FROM tSach s
JOIN tChiTietHDN ct ON s.MaSach = ct.MaSach
JOIN tHoaDonNhap hdn ON ct.SoHDN = hdn.SoHDN
JOIN tNhaXuatBan nxb ON s.MaNXB = nxb.MaNXB
WHERE TenNXB = N'NXB Giáo Dục' AND YEAR(NgayNhap) = 2021

SELECT * FROM vCau1
-- 2.Tạo view thống kê các sách không bán được trong năm 2021
CREATE VIEW vCau2 AS
SELECT s.MaSach, s.TenSach
FROM tSach s
JOIN tChiTietHDB ct ON s.MaSach = ct.MaSach
JOIN tHoaDonBan hdb ON ct.SoHDB = hdb.SoHDB
WHERE NOT EXISTS (
	SELECT 1
	FROM tSach s
	JOIN tChiTietHDB ct ON s.MaSach = ct.MaSach
	JOIN tHoaDonBan hdb ON ct.SoHDB = hdb.SoHDB
	WHERE s.MaSach = ct.MaSach AND YEAR(NgayBan) = 2021
)

SELECT * FROM vCau2

-- 3.Tạo view thống kê 10 khách hàng có tổng tiền tiêu dùng cao nhất trong năm 2014
CREATE VIEW vCau3 AS
SELECT TOP 10
    kh.MaKH,
    kh.TenKH,
    SUM(ct.SLBan * s.DonGiaBan) AS TongTienTieuDung
FROM tKhachHang kh
JOIN tHoaDonBan hdb ON kh.MaKH = hdb.MaKH
JOIN tChiTietHDB ct ON hdb.SoHDB = ct.SoHDB
JOIN tSach s ON s.MaSach = ct.MaSach
WHERE YEAR(hdb.NgayBan) = 2014
GROUP BY kh.MaKH, kh.TenKH
ORDER BY TongTienTieuDung DESC;

SELECT * FROM vCau3


-- 4.Tạo view thống kê số lượng sách bán ra trong năm 2021 và số lượng sách nhập trong năm
-- 2021 và chênh lệch giữa hai số lượng này ứng với mỗi đầu sách
CREATE VIEW vCau4 AS
SELECT 
    s.MaSach,
    s.TenSach,
    ISNULL(SUM(DISTINCT ctn.SLNhap), 0) AS TongNhap2021,
    ISNULL(SUM(DISTINCT ctb.SLBan), 0) AS TongBan2021,
    ISNULL(SUM(DISTINCT ctn.SLNhap), 0) - ISNULL(SUM(DISTINCT ctb.SLBan), 0) AS ChenhLech
FROM tSach s
LEFT JOIN tChiTietHDN ctn ON s.MaSach = ctn.MaSach
LEFT JOIN tHoaDonNhap hdn ON ctn.SoHDN = hdn.SoHDN AND YEAR(hdn.NgayNhap) = 2021
LEFT JOIN tChiTietHDB ctb ON s.MaSach = ctb.MaSach
LEFT JOIN tHoaDonBan hdb ON ctb.SoHDB = hdb.SoHDB AND YEAR(hdb.NgayBan) = 2021
GROUP BY s.MaSach, s.TenSach;

SELECT * FROM vCau4

-- 5.Tạo view đưa ra những thông tin hóa đơn và tổng tiền của hóa đơn đó trong ngày 16/11/2021
CREATE VIEW vCau5 AS
SELECT 
    hdb.SoHDB,
    hdb.NgayBan,
    hdb.MaNV,
    hdb.MaKH,
    SUM(ct.SLBan * s.DonGiaBan) AS TongTien
FROM tHoaDonBan hdb
JOIN tChiTietHDB ct ON hdb.SoHDB = ct.SoHDB
JOIN tSach s ON ct.MaSach = s.MaSach
WHERE hdb.NgayBan = '2014-05-14'
GROUP BY hdb.SoHDB, hdb.NgayBan, hdb.MaNV, hdb.MaKH;

SELECT * FROM vCau5

-- 6. Tạo view đưa ra doanh thu bán hàng của từng tháng trong năm 2014, những tháng không
-- có doanh thu thì ghi là 0.
CREATE VIEW vCau6 AS
WITH Thang AS (
    SELECT 1 AS Thang UNION ALL SELECT 2 UNION ALL SELECT 3 UNION ALL SELECT 4
    UNION ALL SELECT 5 UNION ALL SELECT 6 UNION ALL SELECT 7 UNION ALL SELECT 8
    UNION ALL SELECT 9 UNION ALL SELECT 10 UNION ALL SELECT 11 UNION ALL SELECT 12
)
SELECT 
    t.Thang,
    ISNULL(SUM(ct.SLBan * s.DonGiaBan), 0) AS DoanhThu
FROM Thang t
LEFT JOIN tHoaDonBan hdb ON MONTH(hdb.NgayBan) = t.Thang AND YEAR(hdb.NgayBan) = 2014
LEFT JOIN tChiTietHDB ct ON hdb.SoHDB = ct.SoHDB
LEFT JOIN tSach s ON ct.MaSach = s.MaSach
GROUP BY t.Thang

SELECT * FROM vCau6
-- 7. Tạo view đưa ra doanh thu bán hàng theo ngày, và tổng doanh thu cho mỗi tháng (dùng roll up)
CREATE VIEW vCau7 AS
SELECT 
    MONTH(hdb.NgayBan) AS Thang,
    CAST(hdb.NgayBan AS DATE) AS Ngay,
    SUM(ct.SLBan * s.DonGiaBan) AS DoanhThu
FROM tHoaDonBan hdb
JOIN tChiTietHDB ct ON hdb.SoHDB = ct.SoHDB
JOIN tSach s ON ct.MaSach = s.MaSach
GROUP BY ROLLUP (MONTH(hdb.NgayBan), CAST(hdb.NgayBan AS DATE));

SELECT * FROM vCau7

-- 8.Tạo view đưa ra danh sách 3 khách hàng có tiền tiêu dùng cao nhất
CREATE VIEW vCau8 AS
SELECT TOP 3 
    kh.MaKH,
    kh.TenKH,
    SUM(ct.SLBan * s.DonGiaBan) AS TongTienTieuDung
FROM tKhachHang kh
JOIN tHoaDonBan hdb ON kh.MaKH = hdb.MaKH
JOIN tChiTietHDB ct ON hdb.SoHDB = ct.SoHDB
JOIN tSach s ON ct.MaSach = s.MaSach
GROUP BY kh.MaKH, kh.TenKH
ORDER BY TongTienTieuDung DESC;

SELECT * FROM vCau8

-- 9.Tạo view đưa ra 10 đầu sách được tiêu thụ nhiều nhất trong năm 2022 và số lượng tiêu thụ mỗi đầu sách.
CREATE VIEW vCau9 AS
SELECT TOP 10
    s.MaSach,
    s.TenSach,
    SUM(ct.SLBan) AS TongSLBan
FROM tSach s
JOIN tChiTietHDB ct ON s.MaSach = ct.MaSach
JOIN tHoaDonBan hdb ON ct.SoHDB = hdb.SoHDB
WHERE YEAR(hdb.NgayBan) = 2014
GROUP BY s.MaSach, s.TenSach
ORDER BY TongSLBan DESC;

SELECT * FROM vCau9

-- 10.Tạo view SachGD đưa ra danh sách các sách với các thông tin MaSach,TenSach, tên
-- thể loại, tổng số lượng nhập, tổng số lượng bán, số lượng tồn do Nhà xuất bản Giáo Dục xuất bản.
CREATE VIEW SachGD AS
SELECT 
    s.MaSach,
    s.TenSach,
    tl.TenTheLoai,
    ISNULL(SUM(DISTINCT ctn.SLNhap), 0) AS TongNhap,
    ISNULL(SUM(DISTINCT ctb.SLBan), 0) AS TongBan,
    ISNULL(SUM(DISTINCT ctn.SLNhap), 0) - ISNULL(SUM(DISTINCT ctb.SLBan), 0) AS SoLuongTon
FROM tSach s
JOIN tTheLoai tl ON s.MaTheLoai = tl.MaTheLoai
JOIN tNhaXuatBan nxb ON s.MaNXB = nxb.MaNXB
LEFT JOIN tChiTietHDN ctn ON s.MaSach = ctn.MaSach
LEFT JOIN tChiTietHDB ctb ON s.MaSach = ctb.MaSach
WHERE nxb.TenNXB = N'NXB Giáo Dục'
GROUP BY s.MaSach, s.TenSach, tl.TenTheLoai;

SELECT * FROM SachGD

-- 11.Tạo view KhachVip đưa ra khách hàng gồm thông tin MaKH, TenKH, địa chỉ, điện thoại cho những khách hàng 
-- đã mua hàng với tổng tất cả các trị giá hóa đơn trong năm hiện tại lớn hơn 30.000.000
CREATE VIEW KhachVip AS
SELECT 
    kh.MaKH,
    kh.TenKH,
    kh.DiaChi,
    kh.DienThoai,
    SUM(ct.SLBan * s.DonGiaBan) AS TongTienTieuDung
FROM tKhachHang kh
JOIN tHoaDonBan hdb ON kh.MaKH = hdb.MaKH
JOIN tChiTietHDB ct ON hdb.SoHDB = ct.SoHDB
JOIN tSach s ON s.MaSach = ct.MaSach
WHERE YEAR(hdb.NgayBan) = 2014
GROUP BY kh.MaKH, kh.TenKH, kh.DiaChi, kh.DienThoai
HAVING SUM(ct.SLBan * s.DonGiaBan) > 3000000;

SELECT * FROM KhachVip

-- 12.Tạo view đưa ra số hóa đơn, trị giá các hóa đơn và tổng toàn bộ trị giá của các hoa đơn do nhân viên có tên “Trần Huy” lập trong tháng hiện tại
CREATE VIEW vCau12 AS
SELECT 
    hdb.SoHDB,
    SUM(ct.SLBan * s.DonGiaBan) AS TriGiaHoaDon,
    (SELECT SUM(ct2.SLBan * s2.DonGiaBan)
     FROM tHoaDonBan hdb2
     JOIN tChiTietHDB ct2 ON hdb2.SoHDB = ct2.SoHDB
     JOIN tNhanVien nv2 ON hdb2.MaNV = nv2.MaNV
	 JOIN tSach s2 ON s2.MaSach = ct2.MaSach
     WHERE nv2.TenNV = N'Trần Huy'
       AND MONTH(hdb2.NgayBan) = 2
       AND YEAR(hdb2.NgayBan) = 2013
    ) AS TongTriGia
FROM tHoaDonBan hdb
JOIN tChiTietHDB ct ON hdb.SoHDB = ct.SoHDB
JOIN tNhanVien nv ON hdb.MaNV = nv.MaNV
JOIN tSach s ON ct.MaSach = s.MaSach
WHERE nv.TenNV = N'Trần Huy'
  AND MONTH(hdb.NgayBan) = 2
  AND YEAR(hdb.NgayBan) = 2013
GROUP BY hdb.SoHDB;

SELECT * FROM vCau12

-- 13.Tạo view đưa thông tin các các sách còn tồn
CREATE VIEW vCau13 AS
SELECT s.MaSach, s.TenSach
FROM tSach s
WHERE NOT EXISTS (
    SELECT 1
    FROM tChiTietHDB ct
    WHERE s.MaSach = ct.MaSach
)

SELECT * FROM vCau13

-- 14.Tạo view đưa ra danh sách các sách không bán được trong năm 2014.
CREATE VIEW vCau14 AS
SELECT 
    s.MaSach,
    s.TenSach
FROM tSach s
WHERE NOT EXISTS (
    SELECT 1
    FROM tChiTietHDB ct
    JOIN tHoaDonBan hdb ON ct.SoHDB = hdb.SoHDB
    WHERE s.MaSach = ct.MaSach
      AND YEAR(hdb.NgayBan) = 2014
);

SELECT * FROM vCau14
-- 15.Tạo view đưa ra danh sách các sách của NXB Trẻ không bán được trong năm 2014.
CREATE VIEW vCau15 AS
SELECT 
    s.MaSach,
    s.TenSach,
    nxb.TenNXB
FROM tSach s
JOIN tNhaXuatBan nxb ON s.MaNXB = nxb.MaNXB
WHERE nxb.TenNXB = N'NXB Trẻ'
AND NOT EXISTS (
    SELECT 1
    FROM tChiTietHDB ct
    JOIN tHoaDonBan hdb ON ct.SoHDB = hdb.SoHDB
    WHERE s.MaSach = ct.MaSach AND YEAR(hdb.NgayBan) = 2014
);

SELECT * FROM vCau15

-- 16.Tạo view đưa ra các thông tin về sách và số lượng từng sách được bán ra trong năm 2014.
CREATE VIEW vCau16 AS
SELECT 
    s.MaSach,
    s.TenSach,
    SUM(ct.SLBan) AS TongSoLuongBan
FROM tSach s
JOIN tChiTietHDB ct ON s.MaSach = ct.MaSach
JOIN tHoaDonBan hdb ON ct.SoHDB = hdb.SoHDB
WHERE YEAR(hdb.NgayBan) = 2014
GROUP BY s.MaSach, s.TenSach;

SELECT * FROM vCau16


-- 17.Tạo view đưa ra họ tên khách hàng đã mua hóa đơn có trị giá cao nhất trong năm 2014.
CREATE VIEW vCau17 AS
SELECT 
    kh.MaKH,
    kh.TenKH,
    hdb.SoHDB,
    SUM(ct.SLBan * s.DonGiaBan) AS TriGiaHoaDon
FROM tKhachHang kh
JOIN tHoaDonBan hdb ON kh.MaKH = hdb.MaKH
JOIN tChiTietHDB ct ON hdb.SoHDB = ct.SoHDB
JOIN tSach s ON ct.MaSach = s.MaSach
WHERE YEAR(hdb.NgayBan) = 2014
GROUP BY kh.MaKH, kh.TenKH, hdb.SoHDB
HAVING SUM(ct.SLBan * s.DonGiaBan) = (
    SELECT MAX(TriGia)
    FROM (
        SELECT SUM(ct2.SLBan * s2.DonGiaBan) AS TriGia
        FROM tHoaDonBan hdb2
        JOIN tChiTietHDB ct2 ON hdb2.SoHDB = ct2.SoHDB
		JOIN tSach s2 ON s2.MaSach = ct2.MaSach
        WHERE YEAR(hdb2.NgayBan) = 2014
        GROUP BY hdb2.SoHDB
    ) AS BangTriGia
)

SELECT * FROM vCau17

-- 18.Tạo view đưa ra danh sách những nhân viên (MaKH, TenKH) có doanh số nằm trong 3 doanh số cao nhất của năm hiện tại.
CREATE VIEW vCau18 AS
SELECT TOP 3 
    nv.MaNV,
    nv.TenNV,
    SUM(ct.SLBan * s.DonGiaBan) AS DoanhSo
FROM tNhanVien nv
JOIN tHoaDonBan hdb ON nv.MaNV = hdb.MaNV
JOIN tChiTietHDB ct ON hdb.SoHDB = ct.SoHDB
JOIN tSach s ON ct.MaSach = s.MaSach
GROUP BY nv.MaNV, nv.TenNV
ORDER BY DoanhSo DESC;

SELECT * FROM vCau18

-- 19.Tạo view đưa ra danh sách sách và số lượng nhập của mỗi nhà xuất bản trong năm hiện tại
CREATE VIEW vCau19 AS
SELECT 
    nxb.MaNXB,
    nxb.TenNXB,
    s.MaSach,
    s.TenSach,
    SUM(ct.SLNhap) AS TongSLNhap
FROM tSach s
JOIN tChiTietHDN ct ON s.MaSach = ct.MaSach
JOIN tHoaDonNhap hdn ON ct.SoHDN = hdn.SoHDN
JOIN tNhaXuatBan nxb ON s.MaNXB = nxb.MaNXB
WHERE YEAR(hdn.NgayNhap) = 2014
GROUP BY nxb.MaNXB, nxb.TenNXB, s.MaSach, s.TenSach;

SELECT * FROM vCau19

-- Thủ tục
-- 1. Tạo thủ tục có đầu vào là mã sách, đầu ra là số lượng sách đó được bán trong năm 2014
DROP PROCEDURE SP1
CREATE PROCEDURE SP1
   @MaSach NVARCHAR(10)
AS
BEGIN
    SELECT COUNT(SLBan) AS SoLuongSach
	FROM tSach t
	JOIN tChiTietHDB cthdb ON t.MaSach = cthdb.MaSach
	JOIN tHoaDonBan hdb ON cthdb.SoHDB = hdb.SoHDB
	WHERE @MaSach = t.MaSach AND YEAR(NgayBan) = 2014
END

EXEC SP1 N'S01'

-- 2. Tạo thủ tục có đầu vào là ngày, đầu ra là số lượng hóa đơn và số lượng tiền bán của sách trong ngày đó

CREATE PROCEDURE SP2 
	@Ngay DATETIME
AS
BEGIN
	SELECT 
		COUNT(DISTINCT hdb.SoHDB) AS 'SL Hóa Đơn',
		SUM(cthdb.SLBan * t.DonGiaBan * 
            ISNULL(CAST(cthdb.KhuyenMai AS FLOAT), 1)
        ) AS TongTienBan
	FROM tSach t
	JOIN tChiTietHDB cthdb ON t.MaSach = cthdb.MaSach
	JOIN tHoaDonBan hdb ON cthdb.SoHDB = hdb.SoHDB
	WHERE @Ngay = NgayBan
END

EXEC SP2 '2014-08-11'

-- 3. Tạo thủ tục có đầu vào là mã nhà cung cấp, đầu ra là số đầu sách và số tiền cửa hàng đã nhập của nhà cung cấp đó

CREATE PROCEDURE SP3 
	@MaNCC NVARCHAR(10)
AS
BEGIN
	SELECT 
		COUNT(DISTINCT cthdn.MaSach) AS 'SL Đầu Sách',
		SUM(cthdn.SLNhap * s.DonGiaNhap * 
            ISNULL(CAST(cthdn.KhuyenMai AS FLOAT), 1)
        ) AS TongTienNhap
	FROM tNhaCungCap ncc
	JOIN tHoaDonNhap hdn ON ncc.MaNCC = hdn.MaNCC
	JOIN tChiTietHDN cthdn ON hdn.SoHDN = cthdn.SoHDN
	JOIN tSach s ON cthdn.MaSach = s.MaSach
	WHERE @MaNCC = ncc.MaNCC
END

EXEC SP3 'NCC01'

-- 4.Tạo thủ tục có đầu vào là năm, đầu ra là số tiền nhập hàng, số tiền bán hàng của năm đó.

CREATE PROCEDURE SP4
	@Nam INT
AS
BEGIN
	SELECT 
		SUM(cthdb.SLBan * s.DonGiaBan * 
            ISNULL(CAST(cthdn.KhuyenMai AS FLOAT), 1)
        ) AS TongTienBan,
		SUM(cthdn.SLNhap * s.DonGiaNhap * 
            ISNULL(CAST(cthdn.KhuyenMai AS FLOAT), 1)
        ) AS TongTienNhap
	FROM tSach s
	JOIN tChiTietHDB cthdb ON s.MaSach = cthdb.MaSach
	JOIN tHoaDonBan hdb ON cthdb.SoHDB = hdb.SoHDB
	JOIN tChiTietHDN cthdn ON s.MaSach = cthdb.MaSach
	JOIN tHoaDonNhap hdn ON cthdn.SoHDN = hdn.SoHDN
	WHERE @Nam = YEAR(NgayNhap) AND @Nam = YEAR(NgayBan)
END

EXEC SP4 2014

-- 5. Tạo thủ tục có đầu vào là mã NXB, đầu ra là số lượng sách tồn của nhà xuất bản đó

CREATE PROCEDURE SP5 
	@MaNXB NVARCHAR(10)
AS
BEGIN
	SELECT
		COUNT (*) AS SoLuongSachTon
	FROM tSach s
	WHERE s.MaNXB = @MaNXB
	AND s.MaSach NOT IN (
		SELECT DISTINCT cthdb.MaSach
		FROM tChiTietHDB cthdb
	)
END

EXEC SP5 N'NXB10'

-- 6.Tạo thủ tục nhập dữ liệu cho bảng hóa đơn nhập và chi tiết hóa đơn nhập cùng lúc (sử dụng transaction)

CREATE TYPE ChiTietHDNType AS TABLE
(
    MaSach NVARCHAR(10),
	SLNhap INT,
	KhuyenMai NVARCHAR(100)
)

CREATE PROCEDURE SP6
	@SoHDN NVARCHAR(10),
	@MaNV NVARCHAR(10),
	@NgayNhap DATETIME,
	@MaNCC NVARCHAR(10),
	@ChiTiet ChiTietHDNType READONLY
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;
			INSERT INTO tHoaDonNhap(SoHDN, NgayNhap, MaNV, MaNCC)
			VALUES(@SoHDN, @NgayNhap, @MaNV, @MaNCC)

			INSERT INTO tChiTietHDN(SoHDN, MaSach, SLNhap, KhuyenMai)
			SELECT @SoHDN, MaSach, SLNhap, KhuyenMai
			FROM @ChiTiet
		COMMIT TRANSACTION;
		PRINT N'Them hoa don nhap thanh cong'
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		PRINT N'Loi, Rollback'
	END CATCH
END

DECLARE @ChiTiet ChiTietHDNType

INSERT INTO @ChiTiet VALUES 
(N'S01', 10, NULL),
(N'S02', 13, NULL)

EXEC SP6 @SoHDN = N'HDN06', @NgayNhap = '2025-10-07', @MaNV = N'NV01', @MaNCC = N'NCC01', @ChiTiet = @ChiTiet

SELECT * 
FROM tHoaDonNhap
WHERE SoHDN = 'HDN06';

SELECT * 
FROM tChiTietHDN
WHERE SoHDN = 'HDN06';


-- 7.Tạo thủ tục xóa đồng thời hóa đơn bán và chi tiết hóa đơn bán (dùng transaction)

CREATE PROCEDURE SP7
    @SoHDB NVARCHAR(10)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        DELETE FROM tChiTietHDB
        WHERE SoHDB = @SoHDB;

        DELETE FROM tHoaDonBan
        WHERE SoHDB = @SoHDB;

        COMMIT TRANSACTION;
        PRINT N'Da xoa thanh xong';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        PRINT N'Loi, rollback';
    END CATCH
END

EXEC SP7 N'HDB13'



-- 8.Tạo thủ tục có đầu vào là năm, đầu ra là số lượng sách nhập, sách bán của năm đó
DROP PROCEDURE SP8
GO
CREATE PROCEDURE SP8
	@Nam INT
AS
BEGIN
	SELECT 
		COUNT(SLBan) AS SoLuongSachBan,
		COUNT(SLNhap) AS SoLuongSachNhap
	FROM tSach s
	JOIN tChiTietHDB cthdb ON s.MaSach = cthdb.MaSach
	JOIN tHoaDonBan hdb ON cthdb.SoHDB = hdb.SoHDB
	JOIN tChiTietHDN cthdn ON s.MaSach = cthdb.MaSach
	JOIN tHoaDonNhap hdn ON cthdn.SoHDN = hdn.SoHDN
	WHERE @Nam = YEAR(NgayNhap) AND @Nam = YEAR(NgayBan)
END

EXEC SP8 2014

-- 9. Tạo thủ tục có đầu vào là mã sách, năm, đầu ra số lượng sách nhập, số lượng sách bán trong năm đó

CREATE PROCEDURE SP9
	@MaSach NVARCHAR(10),
	@Nam INT
AS
BEGIN
	SELECT 
		COUNT(SLBan) AS SoLuongSachBan,
		COUNT(SLNhap) AS SoLuongSachNhap
	FROM tSach s
	JOIN tChiTietHDB cthdb ON s.MaSach = cthdb.MaSach
	JOIN tHoaDonBan hdb ON cthdb.SoHDB = hdb.SoHDB
	JOIN tChiTietHDN cthdn ON s.MaSach = cthdb.MaSach
	JOIN tHoaDonNhap hdn ON cthdn.SoHDN = hdn.SoHDN
	WHERE @Nam = YEAR(NgayNhap) AND @Nam = YEAR(NgayBan) AND @MaSach = s.MaSach
END

EXEC SP9 'S02', 2014

-- 10. Tạo thủ tục có đầu vào là mã khách hàng, năm, đầu ra là số lượng sách đã mua và số lượng tiền tiêu dùng của khách hàng đó trong năm nhập vào.
DROP PROCEDURE SP10
GO
CREATE PROCEDURE SP10
	@MaKH NVARCHAR(10),
	@Nam INT
AS
BEGIN
	SELECT
		SUM(SLBan) AS SLSachDaMua,
		SUM(cthdb.SLBan * s.DonGiaBan * 
            ISNULL(CAST(cthdb.KhuyenMai AS FLOAT), 1)
        ) AS TongTienTieuDung
	FROM tKhachHang kh
	JOIN tHoaDonBan hdb ON kh.MaKH = hdb.MaKH
	JOIN tChiTietHDB cthdb ON hdb.SoHDB = cthdb.SoHDB
	JOIN tSach s ON cthdb.MaSach = s.MaSach
	WHERE @MaKH = kh.MaKH AND @Nam = YEAR(NgayBan)
END

EXEC SP10 N'KH01', 2014

-- 11.Tạo thủ tục có đầu vào là mã khách hàng, năm, đầu ra là số lượng hóa đơn đã mua và số lượng tiền tiêu dùng của khách hàng đó trong năm đó.
DROP PROCEDURE SP11
GO
CREATE PROCEDURE SP11
	@MaKH NVARCHAR(10),
	@Nam INT
AS
BEGIN
	SELECT
		COUNT(DISTINCT hdb.SoHDB) AS SLHoaDonMua,
		SUM(cthdb.SLBan * s.DonGiaBan * 
            ISNULL(CAST(cthdb.KhuyenMai AS FLOAT), 1)
        ) AS TongTienTieuDung
	FROM tKhachHang kh
	JOIN tHoaDonBan hdb ON kh.MaKH = hdb.MaKH
	JOIN tChiTietHDB cthdb ON hdb.SoHDB = cthdb.SoHDB
	JOIN tSach s ON cthdb.MaSach = s.MaSach
	WHERE kh.MaKH = @MaKH AND YEAR(NgayBan) = @Nam
END

EXEC SP11 N'KH01', 2014

-- Hàm
-- 1. Tạo hàm đưa ra tổng số tiền đã nhập sách trong một năm với tham số đầu vào là năm
CREATE FUNCTION fn1
(
	@Nam INT
)
RETURNS TABLE
AS 
RETURN
(
	SELECT SUM(cthdn.SLNhap * s.DonGiaNhap * 
            ISNULL(CAST(cthdn.KhuyenMai AS FLOAT), 1)
        ) AS TongTienNhap
	FROM tSach s
	JOIN tChiTietHDN cthdn ON s.MaSach = cthdn.MaSach
	JOIN tHoaDonNhap hdn ON cthdn.SoHDN = hdn.SoHDN
	WHERE YEAR(NgayNhap) = @Nam
)

SELECT * FROM fn1(2014)

-- 2. Tạo hàm đưa ra danh sách 5 đầu sách bán chạy nhất trong tháng nào đó (tháng là tham số đầu vào)

CREATE FUNCTION fn2
(
	@Thang INT
)
RETURNS TABLE
AS
RETURN
(
	SELECT TOP 5
		s.MaSach, s.TenSach,
		SUM(cthdb.SLBan) AS TongSoLuongBan
	FROM tSach s
	JOIN tChiTietHDB cthdb ON s.MaSach = cthdb.MaSach
	JOIN tHoaDonBan hdb ON cthdb.SoHDB = hdb.SoHDB
	WHERE MONTH(hdb.NgayBan) = @Thang
	GROUP BY s.MaSach, s.TenSach
	ORDER BY SUM(cthdb.SLBan) DESC
)

SELECT* FROM fn2(5)

-- 3. Tạo hàm đưa ra danh sách n nhân viên có doanh thu cao nhất trong một năm với n và năm là tham số đầu vào

CREATE FUNCTION fn3
(
	@SoLuongNV INT,
	@Nam INT
)
RETURNS TABLE
AS
RETURN
(
	SELECT MaNV, TenNV, TongDoanhThu
    FROM
    (
        SELECT 
            nv.MaNV,
            nv.TenNV,
            SUM(cthdb.SLBan * s.DonGiaBan * ISNULL(CAST(cthdb.KhuyenMai AS FLOAT), 1)) AS TongDoanhThu,
            ROW_NUMBER() OVER(ORDER BY SUM(cthdb.SLBan * s.DonGiaBan * ISNULL(CAST(cthdb.KhuyenMai AS FLOAT),1)) DESC) AS rn
        FROM tNhanVien nv
        JOIN tHoaDonBan hdb ON nv.MaNV = hdb.MaNV
        JOIN tChiTietHDB cthdb ON hdb.SoHDB = cthdb.SoHDB
        JOIN tSach s ON cthdb.MaSach = s.MaSach
        WHERE YEAR(hdb.NgayBan) = @Nam
        GROUP BY nv.MaNV, nv.TenNV
    ) AS T
    WHERE rn <= @SoLuongNV
)

SELECT * FROM fn3(5, 2014)

-- 4. Tạo hàm đưa ra thông tin Nhân viên sinh nhật trong ngày là tham số nhập vào

CREATE FUNCTION fn4 
(
	@Ngay DATETIME
)
RETURNS TABLE 
AS
RETURN
(
	SELECT 
        MaNV,
        TenNV,
        NgaySinh
    FROM tNhanVien
    WHERE DAY(NgaySinh) = DAY(@Ngay)
      AND MONTH(NgaySinh) = MONTH(@Ngay)
)

SELECT * FROM fn4('2025-09-11')

-- 5. Tạo hàm đưa ra danh sách tồn trong kho quá 2 năm (nhập nhưng không bán hết trong hai năm)

CREATE FUNCTION fn5
(
    @Nam INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        s.MaSach,
        s.TenSach,
        SUM(cthdn.SLNhap) AS TongNhap,
        ISNULL(SUM(cthdb.SLBan), 0) AS TongBan,
        SUM(cthdn.SLNhap) - ISNULL(SUM(cthdb.SLBan),0) AS SoLuongTon,
        MIN(hdn.NgayNhap) AS NgayNhapCaoNhat
    FROM tSach s
    JOIN tChiTietHDN cthdn ON s.MaSach = cthdn.MaSach
	JOIN tHoaDonNhap hdn ON cthdn.SoHDN = hdn.SoHDN
    LEFT JOIN tChiTietHDB cthdb ON s.MaSach = cthdb.MaSach
    GROUP BY s.MaSach, s.TenSach
    HAVING SUM(cthdn.SLNhap) - ISNULL(SUM(cthdb.SLBan),0) > 0
       AND MIN(hdn.NgayNhap) <= DATEFROMPARTS(@Nam - 2, 12, 31)
);

SELECT * FROM fn5(2016)

-- 6. Tạo hàm với đầu vào là ngày, đầu ra là thông tin các hóa đơn và trị giá của hóa đơn trong ngày đó

CREATE FUNCTION fn6
(
    @Ngay DATETIME
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        hdb.SoHDB,
        hdb.MaKH,
        hdb.MaNV,
        hdb.NgayBan,
        SUM(cthdb.SLBan * s.DonGiaBan * ISNULL(CAST(cthdb.KhuyenMai AS FLOAT), 1)) AS TriGiaHoaDon
    FROM tHoaDonBan hdb
    JOIN tChiTietHDB cthdb ON hdb.SoHDB = cthdb.SoHDB
    JOIN tSach s ON cthdb.MaSach = s.MaSach
    WHERE CAST(hdb.NgayBan AS DATE) = CAST(@Ngay AS DATE)
    GROUP BY hdb.SoHDB, hdb.MaKH, hdb.MaNV, hdb.NgayBan
);

SELECT * FROM fn6('2014-05-14')

-- 7. Tạo hàm có đầu vào là năm đầu ra là thống kê doanh thu theo từng tháng và cả năm (dùng roll up)

CREATE FUNCTION fn7
(
    @Nam INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        ISNULL(MONTH(hdb.NgayBan), 0) AS Thang,
        SUM(cthdb.SLBan * s.DonGiaBan * ISNULL(CAST(cthdb.KhuyenMai AS FLOAT), 1)) AS DoanhThu
    FROM tHoaDonBan hdb
    JOIN tChiTietHDB cthdb ON hdb.SoHDB = cthdb.SoHDB
    JOIN tSach s ON cthdb.MaSach = s.MaSach
    WHERE YEAR(hdb.NgayBan) = @Nam
    GROUP BY ROLLUP(MONTH(hdb.NgayBan))
)

SELECT * FROM fn7(2014) ORDER BY Thang


-- 8. Tạo hàm có đầu vào là mã sách, đầu ra là số lượng tồn của sách

CREATE FUNCTION fn8
(
	@MaSach NVARCHAR(10)
)
RETURNS TABLE
AS
RETURN
(
	SELECT
		s.MaSach, s.TenSach,
		ISNULL(SUM(cthdn.SLNhap), 0) - ISNULL(SUM(cthdb.SLBan), 0) AS SoLuongTon
	FROM tSach s
	LEFT JOIN tChiTietHDN cthdn ON s.MaSach = cthdn.MaSach
    LEFT JOIN tChiTietHDB cthdb ON s.MaSach = cthdb.MaSach
    WHERE s.MaSach = @MaSach
    GROUP BY s.MaSach, s.TenSach
)

SELECT * FROM fn8(N'S06')

-- 9. Tạo hàm có đầu vào là mã thể loại, đầu ra là thông tin sách, số lượng sách nhập, số lượng sách bán của mỗi sách thuộc mã loại đó

CREATE FUNCTION fn9
(
    @MaTheLoai NVARCHAR(10)
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        s.MaSach,
        s.TenSach,
        ISNULL(SUM(cthdn.SLNhap), 0) AS SoLuongNhap,
        ISNULL(SUM(cthdb.SLBan), 0) AS SoLuongBan
    FROM tSach s
    LEFT JOIN tChiTietHDN cthdn ON s.MaSach = cthdn.MaSach
    LEFT JOIN tChiTietHDB cthdb ON s.MaSach = cthdb.MaSach
    WHERE s.MaTheLoai = @MaTheLoai
    GROUP BY s.MaSach, s.TenSach
);

SELECT * FROM fn9(N'TL01')

-- Trigger
-- 1. Tạo trường thành tiền (ThanhTien) cho bảng tChitietHDB, tạo trigger cập nhật tự động cho trường này biết ThanhTien=SLBan*DonGiaBan
CREATE TRIGGER trg_1
ON tChiTietHDB
AFTER INSERT, UPDATE
AS
BEGIN
	UPDATE hdb
	SET hdb.ThanhToan = hdb.SLBan * DonGiaBan
	FROM tChiTietHDB hdb
	INNER JOIN inserted i ON hdb.SoHDB = i.SoHDB AND hdb.MaSach = i.MaSach
	INNER JOIN tSach s ON hdb.MaSach = s.MaSach
END

UPDATE tChiTietHDB
SET SLBan = 8
WHERE SoHDB = 'HDB01' AND MaSach = 'S01';

SELECT * 
FROM tChiTietHDB
WHERE SoHDB = 'HDB01' AND MaSach = 'S01';

-- 2. Thêm trường đơn giá (DonGia) cho bảng chi tiết hóa đơn bán, cập nhật dữ liệu cho trường này mỗi khi thêm, sửa bản ghi vào bảng chi tiết hóa đơn bán.
-- 3. Thêm trường số lượng hóa đơn vào bảng khách hàng và cập nhật tự động cho trường này mỗi khi thêm hóa đơn
-- 4. Thêm trường số sản phẩm vào bảng hóa đơn bán, cập nhật tự động cho trường này mỗi khi thêm chi tiết hóa đơn
-- 5.Thêm trường số sản phẩm vào bảng hóa đơn bán, cập nhật tự động cho trường này mỗi khi xóa chi tiết hóa đơn
-- 6.Thêm trường số sản phẩm vào bảng hóa đơn bán, cập nhật tự động cho trường này mỗi khi thêm, sửa, xóa chi tiết hóa đơn
-- 7. Thêm trường tổng tiền cho hóa đơn bán, cập nhật tự động cho trường này mỗi khi thêm chi tiết hóa đơn
-- 8. Thêm trường số lượng hóa đơn vào bảng khách hàng và cập nhật tự động cho trường này mỗi khi thêm, sửa, xóa hóa đơn
-- 9. Thêm trường tổng tiền cho hóa đơn bán, cập nhật tự động cho trường này mỗi khi thêm, xóa, sửa chi tiết hóa đơn
-- 10. Số lượng trong bảng Sách là số lượng tồn kho, cập nhật tự động dữ liệu cho trường này mỗi khi nhập hay bán sách
-- 11. Thêm trường Tổng tiền tiêu dùng cho bảng khách hàng, cập nhật thông tin cho trường này.
-- 12. Thêm trường Số đầu sách cho bảng nhà cung cấp, cập nhật tự động số đầu sách nhà cung cấp cung cấp cho cửa hàng
-- 13. Thêm trường Số lượng sách và Tổng tiền hàng vào bảng nhà cung cấp, cập nhật dữ liệu cho trường này mỗi khi nhập hàng.
-- 14.Tạo trigger trên bảng thoadonban thực hiện xóa các chi tiết hóa đơn mỗi khi xóa hóa đơn