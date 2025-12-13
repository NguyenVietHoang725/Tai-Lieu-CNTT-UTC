USE QLNhanVien

-- 1. Tạo hàm với đầu vào là năm, đầu ra là danh sách nhân viên sinh vào năm đó
CREATE FUNCTION fn1
(
	@Nam INT
)
RETURNS TABLE
AS 
RETURN
(
	SELECT MaNV, HO + ' ' + TEN AS HoTen, NTNS
	FROM tNhanVien
	WHERE YEAR(NTNS) = @Nam
)

SELECT * FROM fn1(1962)

-- 2. Tạo hàm với đầu vào là số thâm niên (số năm làm việc) đầu ra là danh sách nhân viên có thâm niên đó
CREATE FUNCTION fn2 
(
	@SoThamNien INT
)
RETURNS TABLE 
AS
RETURN
(
	SELECT 
		MaNV, 
		HO + ' ' + TEN AS HoTen, 
		NgayBD, 
		DATEDIFF(YEAR, NgayBD, GETDATE()) AS ThamNien
	FROM tNhanVien
	WHERE DATEDIFF(YEAR, NgayBD, GETDATE()) = @SoThamNien
)

SELECT * FROM fn2(35)

-- 3. Tạo hàm đầu vào là chức vụ đầu ra là những nhân viên cùng chức vụ đó
CREATE FUNCTION fn3
(
	@ChucVu NVARCHAR(10)
)
RETURNS TABLE
AS
RETURN
(
	SELECT 
		MaNV, 
		HO + ' ' + TEN AS HoTen
	FROM tNhanVien
	WHERE MaPB = @ChucVu
)

SELECT * FROM fn3(N'VP')

-- 4. Tạo hàm đưa ra thông tin về nhân viên được tăng lương của ngày hôm nay (giả sử 3 năm lên lương 1 lần)
CREATE FUNCTION fn4 
(
	@Ngay DATE
)
RETURNS TABLE
AS
RETURN
(
	SELECT
		MaNV, 
		HO + ' ' + TEN AS HoTen
	FROM tNhanVien
	WHERE @Ngay = DATEADD(YEAR, 3 * DATEDIFF(YEAR, NgayBD, @Ngay) / 3 , NgayBD) -- Tính ngày kì hạn tăng lương thứ k (3 năm, 6 năm, 9 năm, ...)
		AND DATEDIFF(YEAR, NgayBD, @Ngay) % 3 = 0
)

SELECT * FROM fn4('05/05/2004')

-- 5. Tạo Hàm xây dựng bảng lương của nhân viên
CREATE FUNCTION fn_TinhLuong
(
	@LuongCoBan DECIMAL(18, 2),
	@HSLuong TINYINT,
	@MucDoCV NVARCHAR(2)
)
RETURNS DECIMAL(18, 2)
AS
BEGIN
	RETURN (
		ISNULL(@LuongCoBan,0) * ISNULL(@HSLuong,0) + 
		CASE
			WHEN ISNULL(@MucDoCV,'') LIKE '%A' THEN 10
            WHEN ISNULL(@MucDoCV,'') LIKE '%B' THEN 8
            WHEN ISNULL(@MucDoCV,'') LIKE '%C' THEN 5
            ELSE 0
        END
	)
END

CREATE FUNCTION fn_TinhBaoHiem
(
	@LuongCoBan DECIMAL(18, 2),
    @HSLuong TINYINT,
    @MucDoCV NVARCHAR(2)
)
RETURNS DECIMAL(18, 2)
AS
BEGIN
    RETURN (
        dbo.fn_TinhLuong(ISNULL(@LuongCoBan,0), ISNULL(@HSLuong,0), @MucDoCV) * 0.105
    )
END

CREATE FUNCTION fn_TinhThuNhap
(
	@LuongCoBan DECIMAL(18, 2), 
    @HSLuong TINYINT,
    @MucDoCV NVARCHAR(2),
    @GTGC TINYINT
)
RETURNS DECIMAL(18, 2)
AS
BEGIN
    DECLARE @Luong DECIMAL(18, 2) = dbo.fn_TinhLuong(ISNULL(@LuongCoBan,0), ISNULL(@HSLuong,0), @MucDoCV);
    DECLARE @BaoHiem DECIMAL(18, 2) = dbo.fn_TinhBaoHiem(ISNULL(@LuongCoBan,0), ISNULL(@HSLuong,0), @MucDoCV);
    DECLARE @ThuNhap DECIMAL(18, 2);

    SET @ThuNhap = @Luong - @BaoHiem - 11 - (ISNULL(@GTGC,0) * 4.4);

    IF @ThuNhap < 0 
        SET @ThuNhap = 0;

    RETURN @ThuNhap;
END

CREATE FUNCTION fn_TinhThueTNCN
(
	@LuongCoBan DECIMAL(18, 2), 
    @HSLuong TINYINT,
    @MucDoCV NVARCHAR(2),
    @GTGC TINYINT
)
RETURNS DECIMAL(18, 2)
AS
BEGIN
    DECLARE @ThuNhap DECIMAL(18, 2) = dbo.fn_TinhThuNhap(ISNULL(@LuongCoBan,0), ISNULL(@HSLuong,0), @MucDoCV, ISNULL(@GTGC,0));
    DECLARE @Thue DECIMAL(18, 2);

    SET @Thue =
        CASE 
            WHEN @ThuNhap <= 5  THEN @ThuNhap * 0.05
            WHEN @ThuNhap <= 10 THEN @ThuNhap * 0.10 - 0.25
            WHEN @ThuNhap <= 18 THEN @ThuNhap * 0.15 - 0.75
            WHEN @ThuNhap <= 32 THEN @ThuNhap * 0.20 - 1.65
            WHEN @ThuNhap <= 52 THEN @ThuNhap * 0.25 - 3.25
            WHEN @ThuNhap <= 80 THEN @ThuNhap * 0.30 - 5.85
            ELSE @ThuNhap * 0.35 - 9.85
        END;

    IF @Thue < 0 
        SET @Thue = 0;

    RETURN @Thue
END

CREATE FUNCTION fn_TinhThucLinh
(
    @LuongCoBan DECIMAL(18, 2), 
    @HSLuong TINYINT,
    @MucDoCV NVARCHAR(2),
    @GTGC TINYINT
)
RETURNS DECIMAL(18, 2)
AS
BEGIN
    DECLARE @Luong DECIMAL(18, 2) = dbo.fn_TinhLuong(ISNULL(@LuongCoBan,0), ISNULL(@HSLuong,0), @MucDoCV);
    DECLARE @BaoHiem DECIMAL(18, 2) = dbo.fn_TinhBaoHiem(ISNULL(@LuongCoBan,0), ISNULL(@HSLuong,0), @MucDoCV);
    DECLARE @Thue DECIMAL(18, 2) = dbo.fn_TinhThueTNCN(ISNULL(@LuongCoBan,0), ISNULL(@HSLuong,0), @MucDoCV, ISNULL(@GTGC,0));
    DECLARE @ThucLinh DECIMAL(18, 2);

    SET @ThucLinh = @Luong - (@BaoHiem + @Thue);

    RETURN @ThucLinh;
END

CREATE FUNCTION fn_DanhSachLuong()
RETURNS @Result TABLE 
(
    MaNV INT,
    HoTen NVARCHAR(100),
    Luong DECIMAL(18, 2),
    BaoHiem DECIMAL(18, 2),
    ThueTNCN DECIMAL(18, 2),
    ThucLinh DECIMAL(18, 2)
)
AS
BEGIN
    INSERT INTO @Result
    SELECT 
        nv.MaNV,
        nv.HO + ' ' + nv.TEN AS HoTen,
        dbo.fn_TinhLuong(1.49, ISNULL(ct.HSLuong,0), ct.MucDoCV) AS Luong,
        dbo.fn_TinhBaoHiem(1.49, ISNULL(ct.HSLuong,0), ct.MucDoCV) AS BaoHiem,
        dbo.fn_TinhThueTNCN(1.49, ISNULL(ct.HSLuong,0), ct.MucDoCV, ISNULL(ct.GTGC,0)) AS ThueTNCN,
        dbo.fn_TinhThucLinh(1.49, ISNULL(ct.HSLuong,0), ct.MucDoCV, ISNULL(ct.GTGC,0)) AS ThucLinh
    FROM tNhanVien nv
    JOIN tChiTietNhanVien ct ON nv.MaNV = ct.MaNV;

    RETURN;
END

SELECT * FROM dbo.fn_DanhSachLuong();


-- 6. Tạo thủ tục có đầu vào là mã phòng, đầu ra là số nhân viên của phòng đó và tên trưởng phòng
CREATE PROCEDURE sp6
(
    @MaPhong NVARCHAR(10)
)
AS
BEGIN
    SELECT 
        COUNT(DISTINCT nv.MaNV) AS SoNhanVien,
        tp.HO + ' ' + tp.TEN AS TruongPhong
    FROM tNhanVien nv
    JOIN tPhongBan pb ON pb.MaPB = nv.MaPB
    JOIN tNhanVien tp ON tp.MaNV = pb.TruongPhong   -- lấy thông tin trưởng phòng
    WHERE pb.MaPB = @MaPhong
    GROUP BY tp.HO, tp.TEN;
END


EXEC sp6 N'KH'

-- 7. Tạo thủ tục có đầu vào là mã phòng, tháng, năm, đầu ra là số tiền lương của phòng đó

