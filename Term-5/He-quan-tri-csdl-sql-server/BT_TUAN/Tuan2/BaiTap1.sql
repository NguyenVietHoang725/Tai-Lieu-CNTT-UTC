USE QLVanTai

-- 1. Tạo hàm có đầu vào là lộ trình, đầu ra là số xe, mã trọng tải, số lượng vận tải, ngày đi, ngày
-- đến (SoXe, MaTrongTai, SoLuongVT, NgayDi, NgayDen.)
CREATE FUNCTION fn1 
(
	@MaLoTrinh NVARCHAR(10)
)
RETURNS TABLE
AS
RETURN 
(
	SELECT SoXe, MaTrongTai, SoLuongVT, NgayDi, NgayDen
	FROM ChiTietVanTai
	WHERE MaLoTrinh = @MaLoTrinh
)

SELECT * FROM dbo.fn1(N'PK');

-- 2. Thiết lập hàm có đầu vào là số xe, đầu ra là thông tin về lộ trình
CREATE FUNCTION fn2
(
	@SoXe NVARCHAR(10)
)
RETURNS TABLE
AS
RETURN 
(
	SELECT lt.MaLoTrinh, TenLoTrinh, DonGia, ThoiGianQD
	FROM LoTrinh lt
	JOIN ChiTietVanTai ct ON ct.MaLoTrinh = lt.MaLoTrinh 
	WHERE ct.SoXe = @SoXe
)

SELECT * FROM dbo.fn2(N'333');

-- 3.Tạo hàm có đầu vào là trọng tải, đầu ra là các số xe có trọng tải quy định lớn hơn hoặc bằng trọng tải đó
CREATE FUNCTION fn3 
(
	@TrongTai INT
)
RETURNS TABLE 
AS 
RETURN 
(
	SELECT ct.SoXe
	FROM ChiTietVanTai ct
	JOIN TrongTai tt ON tt.MaTrongTai = ct.MaTrongTai
	WHERE TrongTaiQD >= @TrongTai
)

SELECT * FROM dbo.fn3(8);

-- 4. Tạo hàm có đầu vào là trọng tải và mã lộ trình, đầu ra là số lượng xe có trọng tải quy định
-- lớn hơn hoặc bằng trọng tải đó và thuộc lộ trình đó.
CREATE FUNCTION fn4 
(
	@TrongTai INT,
	@MaLoTrinh NVARCHAR(10)
)
RETURNS TABLE
AS
RETURN
(
	SELECT COUNT(DISTINCT ct.SoXe) AS SoLuongXe
	FROM ChiTietVanTai ct
	JOIN TrongTai tt ON tt.MaTrongTai = ct.MaTrongTai
	WHERE TrongTaiQD >= @TrongTai AND MaLoTrinh = @MaLoTrinh
)

SELECT * FROM dbo.fn4(8, N'HN');

-- 5. Tạo thủ tục có đầu vào Mã lộ trình đầu ra là số lượng xe thuộc lộ trình đó.
CREATE PROCEDURE sp5
    @MaLoTrinh NVARCHAR(10)
AS
BEGIN
    SELECT COUNT(DISTINCT ct.SoXe) AS SoLuongXe
    FROM ChiTietVanTai ct
    WHERE ct.MaLoTrinh = @MaLoTrinh;
END

EXEC sp5 N'HN';

-- 6. Tạo thủ tục có đầu vào là mã lộ trình, năm vận tải, đầu ra là số tiền theo mã lộ trình và năm vận tải đó
CREATE PROCEDURE sp6
	@MaLoTrinh NVARCHAR(10),
	@NamVanTai INT
AS
BEGIN
	SELECT 
		ct.MaLoTrinh,
		@NamVanTai AS NamVanTai,
		SUM(ct.SoLuongVT * lt.DonGia) AS TongSoTien
	FROM ChiTietVanTai ct
	JOIN LoTrinh lt ON lt.MaLoTrinh = ct.MaLoTrinh
	WHERE ct.MaLoTrinh = @MaLoTrinh
		AND YEAR(ct.NgayDi) = @NamVanTai
	GROUP BY ct.MaLoTrinh
END

EXEC sp6 N'HN', 2014;

-- 7. Tạo thủ tục có đầu vào là số xe, năm vận tải, đầu ra là số tiền theo số xe và năm vận tải đó đó
CREATE PROCEDURE sp7
	@SoXe NVARCHAR(10),
	@NamVanTai INT
AS
BEGIN
	SELECT 
		ct.SoXe,
		@NamVanTai AS NamVanTai,
		SUM(ct.SoLuongVT * lt.DonGia) AS TongSoTien
	FROM ChiTietVanTai ct
	JOIN LoTrinh lt ON lt.MaLoTrinh = ct.MaLoTrinh
	WHERE ct.SoXe = @SoXe
		AND YEAR(ct.NgayDi) = @NamVanTai
	GROUP BY ct.SoXe
END

EXEC sp7 N'333', 2014;

-- 8. Tạo thủ tục có đầu vào là mã trọng tải, đầu ra là số lượng xe vượt quá trọng tải quy định của mã trọng tải đó.
CREATE PROCEDURE sp8
	@MaTrongTai NVARCHAR(10)
AS
BEGIN
	SELECT
		COUNT(DISTINCT ct.SoXe) AS SoLuongXe
	FROM ChiTietVanTai ct
	JOIN TrongTai tt ON tt.MaTrongTai = ct.MaTrongTai
	WHERE tt.MaTrongTai = @MaTrongTai
		AND ct.SoLuongVT > TrongTaiQD
END

EXEC sp8 N'50';
