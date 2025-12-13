-- use QLThuVien

-- =========================================================================
-- Tên: SP_GenerateNewMaTg
-- Mục đích: Sinh mã Tác giả mới (MaTg) theo format: TG[MaQg][YY]-[###]
-- Ví dụ: TGVN25-001
-- =========================================================================
DROP PROCEDURE SP_GenerateNewMaTg
CREATE PROCEDURE SP_GenerateNewMaTg
    @MaQg NVARCHAR(10), -- Mã Quốc gia (tham số đầu vào)
    @NewMaTg NVARCHAR(50) OUTPUT -- Mã Tác giả mới (tham số đầu ra)
AS
BEGIN
    -- Đảm bảo không có người dùng nào khác đọc/ghi trong quá trình sinh mã
    SET NOCOUNT ON;
    
    -- Khởi tạo Transaction để đảm bảo tính nguyên tử (Atomic)
    BEGIN TRANSACTION
    
    DECLARE @YearSuffix CHAR(2);
    DECLARE @Prefix NVARCHAR(20);
    DECLARE @LatestMaTg NVARCHAR(50);
    DECLARE @Sequence INT;

    -- 1. Tính toán Prefix (TL + MaGQ + YY + "-")
    SET @YearSuffix = FORMAT(GETDATE(), 'yy');
    SET @Prefix = 'TG' + @MaQg + @YearSuffix + '-';
    
    -- 2. Đọc mã lớn nhất hiện tại (sử dụng WITH (UPDLOCK, HOLDLOCK) để khóa hàng đọc)
    -- Lệnh này sẽ khóa các hàng có MaTg bắt đầu bằng @Prefix cho đến khi Transaction kết thúc
    SELECT TOP 1 @LatestMaTg = MaTg
    FROM TTacGia WITH (UPDLOCK, HOLDLOCK) -- **Khóa hàng quan trọng để tránh trùng lặp**
    WHERE MaTg LIKE @Prefix + '[0-9][0-9][0-9]'
    ORDER BY MaTg DESC;

    -- 3. Tính toán số thứ tự
    IF @LatestMaTg IS NULL
    BEGIN
        -- Đây là mã đầu tiên của prefix này
        SET @Sequence = 1;
    END
    ELSE
    BEGIN
        -- Trích xuất số thứ tự từ mã lớn nhất
        SET @Sequence = CAST(RIGHT(@LatestMaTg, 3) AS INT) + 1;
    END

    -- 4. Kiểm tra giới hạn (tùy chọn)
    IF @Sequence > 999
    BEGIN
        -- Rollback và báo lỗi nếu vượt quá giới hạn
        ROLLBACK TRANSACTION
        RAISERROR('Không thể tạo thêm mã tác giả cho Quốc gia này trong năm nay. Đã đạt giới hạn 999.', 16, 1)
        RETURN
    END

    -- 5. Định dạng và gán Mã mới
    SET @NewMaTg = @Prefix + RIGHT('00' + CAST(@Sequence AS NVARCHAR(3)), 3);

    COMMIT TRANSACTION -- Kết thúc giao dịch thành công
END

-- =========================================================================
-- Tên: SP_GenerateNewMaNxb
-- Mục đích: Sinh mã Nhà Xuất Bản mới (MaNXB) theo format: NXB[MaQg]-[###]
-- Ví dụ: NXBVN-001 (9 ký tự)
-- =========================================================================
DROP PROCEDURE IF EXISTS SP_GenerateNewMaNxb
GO
CREATE PROCEDURE SP_GenerateNewMaNxb
    @MaQg NVARCHAR(10), -- Mã Quốc gia (tham số đầu vào)
    @NewMaNxb NVARCHAR(50) OUTPUT -- Mã Nhà Xuất Bản mới (tham số đầu ra)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRANSACTION
    
    DECLARE @Prefix NVARCHAR(20);
    DECLARE @LatestMaNxb NVARCHAR(50);
    DECLARE @Sequence INT;

    -- 1. Tính toán Prefix (NXB + MaGQ + "-")
    -- Format mới: NXB[MaQg]- => 3 + 2 + 1 = 6 ký tự
    SET @Prefix = 'NXB' + @MaQg + '-';
    
    -- 2. Đọc mã lớn nhất hiện tại (sử dụng TÊN CỘT ĐÚNG: MaNXB)
    SELECT TOP 1 @LatestMaNxb = MaNXB
    FROM TNhaXuatBan WITH (UPDLOCK, HOLDLOCK)
    -- Lọc theo prefix cũ (NXBVN-001) VÀ prefix mới (NXBVN25-001)
    WHERE MaNXB LIKE @Prefix + '[0-9][0-9][0-9]'
    OR MaNXB LIKE 'NXB' + @MaQg + '[0-9][0-9]-' + '[0-9][0-9][0-9]' -- Kích hoạt lại nếu bạn có thể có dữ liệu cũ theo format có năm
    ORDER BY MaNXB DESC;

    -- 3. Tính toán số thứ tự
    IF @LatestMaNxb IS NULL OR LEFT(@LatestMaNxb, 6) <> @Prefix
    BEGIN
        -- Nếu không tìm thấy mã hoặc mã tìm thấy không khớp prefix (có thể là mã cũ có năm)
        SET @Sequence = 1;
    END
    ELSE
    BEGIN
        -- Trích xuất số thứ tự từ mã lớn nhất (3 ký tự cuối)
        SET @Sequence = CAST(RIGHT(@LatestMaNxb, 3) AS INT) + 1;
    END

    -- 4. Kiểm tra giới hạn
    IF @Sequence > 999
    BEGIN
        ROLLBACK TRANSACTION
        RAISERROR('Không thể tạo thêm mã Nhà Xuất Bản cho Quốc gia này. Đã đạt giới hạn 999.', 16, 1)
        RETURN
    END

    -- 5. Định dạng và gán Mã mới (9 ký tự)
    SET @NewMaNxb = @Prefix + RIGHT('00' + CAST(@Sequence AS NVARCHAR(3)), 3);

    COMMIT TRANSACTION -- Kết thúc giao dịch thành công
END
GO

-- =========================================================================
-- Tên: SP_GenerateNewMaThL
-- Mục đích: Sinh mã Thể loại mới (MaThL) theo format: THL[###]
-- Ví dụ: THL011 (6 ký tự)
-- =========================================================================
DROP PROCEDURE IF EXISTS SP_GenerateNewMaThL
GO
CREATE PROCEDURE SP_GenerateNewMaThL
    @NewMaThL CHAR(6) OUTPUT -- Mã Thể loại mới (tham số đầu ra)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRANSACTION
    
    DECLARE @Prefix CHAR(3) = 'THL';
    DECLARE @LatestMaThL CHAR(6);
    DECLARE @Sequence INT;

    -- 1. Đọc mã lớn nhất hiện tại
    SELECT TOP 1 @LatestMaThL = MaThL
    FROM TTheLoai WITH (UPDLOCK, HOLDLOCK) 
    WHERE MaThL LIKE @Prefix + '[0-9][0-9][0-9]'
    ORDER BY MaThL DESC;

    -- 2. Tính toán số thứ tự
    IF @LatestMaThL IS NULL
    BEGIN
        SET @Sequence = 1;
    END
    ELSE
    BEGIN
        -- Trích xuất số thứ tự từ mã lớn nhất (3 ký tự cuối)
        SET @Sequence = CAST(RIGHT(@LatestMaThL, 3) AS INT) + 1;
    END

    -- 3. Kiểm tra giới hạn (tùy chọn)
    IF @Sequence > 999
    BEGIN
        ROLLBACK TRANSACTION
        RAISERROR('Đã đạt giới hạn 999 cho Mã Thể loại.', 16, 1)
        RETURN
    END

    -- 4. Định dạng và gán Mã mới (THL + 3 số)
    SET @NewMaThL = @Prefix + RIGHT('00' + CAST(@Sequence AS NVARCHAR(3)), 3);

    COMMIT TRANSACTION -- Kết thúc giao dịch thành công
END
GO

-- =========================================================================
-- Tên: SP_GenerateNewMaDD
-- Mục đích: Sinh mã Định dạng mới (MaDD) theo format: DD[##]
-- Ví dụ: DD011 (5 ký tự)
-- =========================================================================
DROP PROCEDURE IF EXISTS SP_GenerateNewMaDD
GO
CREATE PROCEDURE SP_GenerateNewMaDD
    @NewMaDD CHAR(5) OUTPUT -- Mã Định dạng mới (tham số đầu ra)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRANSACTION
    
    DECLARE @Prefix CHAR(2) = 'DD';
    DECLARE @LatestMaDD CHAR(5);
    DECLARE @Sequence INT;

    -- 1. Đọc mã lớn nhất hiện tại
    -- Sử dụng UPDLOCK, HOLDLOCK để khóa hàng/trang trong giao dịch, đảm bảo tính tuần tự
    SELECT TOP 1 @LatestMaDD = MaDD
    FROM dbo.tDinhDang WITH (UPDLOCK, HOLDLOCK) 
    -- Kiểm tra format: DD theo sau bởi 3 chữ số
    WHERE MaDD LIKE @Prefix + '[0-9][0-9][0-9]'
    ORDER BY MaDD DESC;

    -- 2. Tính toán số thứ tự
    IF @LatestMaDD IS NULL
    BEGIN
        SET @Sequence = 1;
    END
    ELSE
    BEGIN
        -- Trích xuất số thứ tự từ mã lớn nhất (3 ký tự cuối)
        SET @Sequence = CAST(RIGHT(@LatestMaDD, 3) AS INT) + 1;
    END

    -- 3. Kiểm tra giới hạn (DD001 -> DD999)
    IF @Sequence > 999
    BEGIN
        ROLLBACK TRANSACTION
        RAISERROR('Đã đạt giới hạn 999 cho Mã Định dạng (DD).', 16, 1)
        RETURN
    END

    -- 4. Định dạng và gán Mã mới (DD + 3 số)
    -- Sử dụng hàm RIGHT để đảm bảo luôn có 3 chữ số (VD: 1 -> 001, 10 -> 010)
    SET @NewMaDD = @Prefix + RIGHT('00' + CAST(@Sequence AS NVARCHAR(3)), 3);

    COMMIT TRANSACTION -- Kết thúc giao dịch thành công
END
GO

-- =========================================================================
-- Tên: SP_GenerateNewMaTBD
-- Mục đích: Sinh mã Thẻ Bạn đọc mới (MaTBD) theo format: TBD + MaBD
-- Ví dụ: TBD231230821 (12 ký tự)
-- Kiểm tra: Đảm bảo MaBD tồn tại trong tBanDoc và chưa có thẻ.
-- =========================================================================
DROP PROCEDURE IF EXISTS SP_GenerateNewMaTBD
GO
CREATE PROCEDURE SP_GenerateNewMaTBD
    @MaBD CHAR(9),             -- Mã Bạn đọc (tham số đầu vào)
    @NewMaTBD CHAR(12) OUTPUT,  -- Mã Thẻ Bạn đọc mới (tham số đầu ra)
    @ErrorStatus INT OUTPUT     -- 0: Thành công, 1: Mã BD không tồn tại, 2: Bạn đọc đã có thẻ
AS
BEGIN
    SET NOCOUNT ON;
    SET @ErrorStatus = 0;
    SET @NewMaTBD = NULL;

    -- 1. KIỂM TRA MÃ BẠN ĐỌC TỒN TẠI TRONG tBanDoc
    IF NOT EXISTS (SELECT 1 FROM dbo.tBanDoc WHERE MaBD = @MaBD)
    BEGIN
        SET @ErrorStatus = 1; -- Mã BD không tồn tại
        RETURN;
    END

    -- 2. KIỂM TRA BẠN ĐỌC ĐÃ CÓ THẺ CHƯA (MaBD là UNIQUE/PK thứ cấp trong tTheBanDoc)
    IF EXISTS (SELECT 1 FROM dbo.tTheBanDoc WHERE MaBD = @MaBD)
    BEGIN
        SET @ErrorStatus = 2; -- Bạn đọc đã có thẻ
        RETURN;
    END

    -- 3. SINH MÃ (TBD + MaBD)
    SET @NewMaTBD = 'TBD' + @MaBD;

    -- 4. KIỂM TRA CHIỀU DÀI MÃ (Đảm bảo 12 ký tự)
    IF LEN(@NewMaTBD) <> 12
    BEGIN
        SET @ErrorStatus = 3; 
        SET @NewMaTBD = NULL;
    END
END
GO

-- =========================================================================
-- Tên: SP_AutoLockExpiredTheBanDoc
-- Mục đích: Tự động cập nhật trạng thái các thẻ bạn đọc đã hết hạn (NgayHetHan < GETDATE())
-- =========================================================================
DROP PROCEDURE IF EXISTS SP_AutoLockExpiredTheBanDoc
GO
CREATE PROCEDURE SP_AutoLockExpiredTheBanDoc
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @CurrentDate DATE = GETDATE();

    BEGIN TRANSACTION
    
    -- Khóa các thẻ có ngày hết hạn trong quá khứ và chưa bị khóa
    UPDATE dbo.tTheBanDoc
    SET TrangThai = N'Khóa'
    WHERE NgayHetHan < @CurrentDate
      AND TrangThai NOT IN (N'Khóa', N'Đã khóa'); 

    COMMIT TRANSACTION
END
GO

-- =======================================================
-- Tên: SP_GenerateNewMaNV (Mã Nhân viên)
-- Mã CHAR(7) format: NV[YY]-[##]
-- Chỉnh sửa: Dùng VARCHAR cho biến trung gian để tránh cắt/đệm chuỗi
-- =======================================================
DROP PROCEDURE IF EXISTS SP_GenerateNewMaNV
GO
CREATE PROCEDURE SP_GenerateNewMaNV
    @NewMaNV CHAR(7) OUTPUT 
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRANSACTION
    
    -- Dùng VARCHAR thay vì CHAR để đảm bảo dấu '-' không bị cắt
    DECLARE @YearCode VARCHAR(2) = RIGHT(CAST(YEAR(GETDATE()) AS NVARCHAR(4)), 2); 
    DECLARE @Prefix VARCHAR(5) = 'NV' + @YearCode + '-';  -- Prefix: NVYY- (5 ký tự)
    DECLARE @LatestMaNV CHAR(7);
    DECLARE @Sequence INT;

    -- Chú ý: Pattern tìm kiếm cần khớp với định dạng CHAR(7)
    SELECT TOP 1 @LatestMaNV = MaNV
    FROM dbo.tNhanVien WITH (UPDLOCK, HOLDLOCK) 
    WHERE MaNV LIKE @Prefix + '[0-9][0-9]'
    ORDER BY MaNV DESC;

    IF @LatestMaNV IS NULL
    BEGIN
        SET @Sequence = 1;
    END
    ELSE
    BEGIN
        -- Lấy 2 ký tự cuối (số thứ tự)
        SET @Sequence = CAST(RIGHT(@LatestMaNV, 2) AS INT) + 1;
    END

    IF @Sequence > 99
    BEGIN
        ROLLBACK TRANSACTION
        RAISERROR('Đã đạt giới hạn 99 cho Mã Nhân viên trong năm %s.', 16, 1, @YearCode)
        RETURN
    END

    -- Mã mới sẽ là 7 ký tự (NVYY-##)
    SET @NewMaNV = @Prefix + RIGHT('0' + CAST(@Sequence AS NVARCHAR(2)), 2);

    COMMIT TRANSACTION 
END
GO

-- =======================================================
-- Tên: SP_GenerateNewMaTK (Mã Tài khoản)
-- Mã CHAR(7) format: TK[YY]-[##]
-- Chỉnh sửa: Dùng VARCHAR cho biến trung gian để tránh cắt/đệm chuỗi
-- =======================================================
DROP PROCEDURE IF EXISTS SP_GenerateNewMaTK
GO
CREATE PROCEDURE SP_GenerateNewMaTK
    @NewMaTK CHAR(7) OUTPUT 
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRANSACTION
    
    -- SỬ DỤNG VARCHAR ĐỂ KHÔNG BỊ CẮT CHUỖI
    DECLARE @YearCode VARCHAR(2) = RIGHT(CAST(YEAR(GETDATE()) AS NVARCHAR(4)), 2); 
    DECLARE @Prefix VARCHAR(5) = 'TK' + @YearCode + '-';  -- Prefix: TKYY- (5 ký tự)
    DECLARE @LatestMaTK CHAR(7);
    DECLARE @Sequence INT;

    SELECT TOP 1 @LatestMaTK = MaTK
    FROM dbo.tTaiKhoan WITH (UPDLOCK, HOLDLOCK) 
    -- Sử dụng @Prefix (đã là VARCHAR(5) = TKYY-) để tìm kiếm đúng
    WHERE MaTK LIKE @Prefix + '[0-9][0-9]'
    ORDER BY MaTK DESC;

    IF @LatestMaTK IS NULL
    BEGIN
        SET @Sequence = 1;
    END
    ELSE
    BEGIN
        SET @Sequence = CAST(RIGHT(@LatestMaTK, 2) AS INT) + 1;
    END

    IF @Sequence > 99
    BEGIN
        ROLLBACK TRANSACTION
        RAISERROR('Đã đạt giới hạn 99 cho Mã Tài khoản trong năm %s.', 16, 1, @YearCode)
        RETURN
    END

    -- Mã mới sẽ là 7 ký tự (TKYY-##). @NewMaTK là CHAR(7) nên chuỗi kết quả phải đủ 7 ký tự
    SET @NewMaTK = @Prefix + RIGHT('0' + CAST(@Sequence AS NVARCHAR(2)), 2);

    COMMIT TRANSACTION 
END
GO

-- =======================================================
-- Tên: SP_GenerateNewMaGD (Mã Giao dịch)
-- Mã CHAR(12) format: GD[YY][MM][####]
-- Chỉnh sửa: Dùng VARCHAR cho biến trung gian để tránh cắt/đệm chuỗi
-- =======================================================
DROP PROCEDURE IF EXISTS SP_GenerateNewMaGD
GO
CREATE PROCEDURE SP_GenerateNewMaGD
    @NewMaGD CHAR(12) OUTPUT 
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRANSACTION
    
    -- Lấy năm (YY) và tháng (MM) hiện tại
    DECLARE @YearCode VARCHAR(2) = RIGHT(CAST(YEAR(GETDATE()) AS NVARCHAR(4)), 2); 
    DECLARE @MonthCode VARCHAR(2) = RIGHT('0' + CAST(MONTH(GETDATE()) AS NVARCHAR(2)), 2);
    DECLARE @Prefix VARCHAR(7) = 'GD' + @YearCode + @MonthCode;  -- Prefix: GDYYMM (6 ký tự)

    DECLARE @LatestMaGD CHAR(12);
    DECLARE @Sequence INT;

    -- Tìm Mã GD lớn nhất trong tháng hiện tại
    SELECT TOP 1 @LatestMaGD = MaGD
    FROM dbo.tGiaoDichMuonTra WITH (UPDLOCK, HOLDLOCK) 
    -- Sử dụng @Prefix (GDYYMM) để tìm kiếm các mã có cùng tháng
    WHERE MaGD LIKE @Prefix + '[0-9][0-9][0-9][0-9]'
    ORDER BY MaGD DESC;

    IF @LatestMaGD IS NULL
    BEGIN
        SET @Sequence = 1; -- Bắt đầu từ 1 (0001)
    END
    ELSE
    BEGIN
        -- Lấy 4 ký tự cuối và tăng lên 1
        SET @Sequence = CAST(RIGHT(@LatestMaGD, 4) AS INT) + 1;
    END

    IF @Sequence > 9999
    BEGIN
        ROLLBACK TRANSACTION
        RAISERROR('Đã đạt giới hạn 9999 cho Mã Giao dịch trong tháng %s/%s.', 16, 1, @MonthCode, @YearCode)
        RETURN
    END

    -- Mã mới sẽ là 12 ký tự (GDYYMM####). Dùng RIGHT('000' + CAST(@Sequence...) để thêm số 0 ở đầu
    SET @NewMaGD = @Prefix + RIGHT('000' + CAST(@Sequence AS NVARCHAR(4)), 4);

    COMMIT TRANSACTION 
END
GO

-- =========================================================================
-- Tên: SP_GenerateNewMaTl
-- Mục đích: Sinh mã Tài liệu mới (MaTL) theo format: TL[MaNN][YY]-[###]
-- Ví dụ: TLVI25-001 (Tài liệu Ngôn ngữ Việt, năm 2025, số 001)
-- =========================================================================
IF OBJECT_ID('SP_GenerateNewMaTl', 'P') IS NOT NULL
    DROP PROCEDURE SP_GenerateNewMaTl
GO

CREATE PROCEDURE SP_GenerateNewMaTl
    @MaNN CHAR(2),                           -- Mã Ngôn ngữ (VI, EN, RU,...) (tham số đầu vào)
    @NewMaTl CHAR(10) OUTPUT                   -- Mã Tài liệu mới (tham số đầu ra)
AS
BEGIN
    -- Đảm bảo không có người dùng nào khác đọc/ghi trong quá trình sinh mã
    SET NOCOUNT ON;
    
    -- Khởi tạo Transaction để đảm bảo tính nguyên tử (Atomic)
    BEGIN TRANSACTION
    
    DECLARE @YearSuffix CHAR(2);
    DECLARE @Prefix CHAR(6); -- 'TL' + MaNN + YY (6 ký tự)
    DECLARE @LatestMaTl CHAR(10);
    DECLARE @Sequence INT;

    -- 1. Tính toán Prefix (TL + MaNN + YY)
    SET @YearSuffix = FORMAT(GETDATE(), 'yy');
    SET @Prefix = 'TL' + @MaNN + @YearSuffix; -- Ví dụ: TLVI25
    
    -- 2. Đọc mã lớn nhất hiện tại (sử dụng WITH (UPDLOCK, HOLDLOCK) để khóa hàng đọc)
    -- Lệnh này sẽ khóa các hàng có MaTL bắt đầu bằng @Prefix cho đến khi Transaction kết thúc
    SELECT TOP 1 @LatestMaTl = MaTL
    FROM dbo.tTaiLieu WITH (UPDLOCK, HOLDLOCK) -- **Khóa hàng quan trọng để tránh trùng lặp**
    -- MaTL dài 10 ký tự: Prefix (6) + "-" (1) + Số thứ tự (3)
    WHERE MaTL LIKE @Prefix + '-[0-9][0-9][0-9]'
    ORDER BY MaTL DESC;

    -- 3. Tính toán số thứ tự
    IF @LatestMaTl IS NULL
    BEGIN
        -- Đây là mã đầu tiên của prefix này
        SET @Sequence = 1;
    END
    ELSE
    BEGIN
        -- Trích xuất số thứ tự (3 ký tự cuối) từ mã lớn nhất
        SET @Sequence = CAST(RIGHT(@LatestMaTl, 3) AS INT) + 1;
    END

    -- 4. Kiểm tra giới hạn
    IF @Sequence > 999
    BEGIN
        -- Rollback và báo lỗi nếu vượt quá giới hạn
        ROLLBACK TRANSACTION
        RAISERROR('Không thể tạo thêm mã tài liệu cho Ngôn ngữ này trong năm nay. Đã đạt giới hạn 999.', 16, 1)
        RETURN
    END

    -- 5. Định dạng và gán Mã mới (Prefix + "-" + Số thứ tự 3 chữ số)
    SET @NewMaTl = @Prefix + '-' + RIGHT('00' + CAST(@Sequence AS NVARCHAR(3)), 3);

    COMMIT TRANSACTION -- Kết thúc giao dịch thành công
END
GO

-- =========================================================================
-- Tên: SP_GenerateNewMaBS
-- Mục đích: Sinh mã Bản sao mới (MaBS) theo format: BS + MãTL (bỏ TL) + - + đánh số
-- Ví dụ: BSVN25-001-001 (Bản sao của Tài liệu VN25-001, số thứ tự 001)
-- =========================================================================
sql-- =========================================================================
-- Tên: SP_GenerateNewMaBS
-- Mục đích: Sinh mã Bản sao mới (MaBS) theo format: BS + MãTL (bỏ TL) + - + số thứ tự
-- Ví dụ: 
--   Input: TLBI25-001 → Output: BSBI25-001-001, BSBI25-001-002, ...
--   Input: TLVN25-005 → Output: BSVN25-005-001, BSVN25-005-002, ...
-- =========================================================================
IF OBJECT_ID('SP_GenerateNewMaBS', 'P') IS NOT NULL
    DROP PROCEDURE SP_GenerateNewMaBS
GO

CREATE PROCEDURE SP_GenerateNewMaBS
    @MaTL CHAR(10),              -- Mã Tài liệu gốc (Ví dụ: 'TLVN25-001')
    @NewMaBS CHAR(14) OUTPUT     -- Mã Bản sao mới (Ví dụ: 'BSVN25-001-001')
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Khởi tạo Transaction để đảm bảo tính nguyên tử
    BEGIN TRANSACTION;
    
    BEGIN TRY
        DECLARE @MaTL_Root CHAR(8);      -- Phần sau 'TL' (Ví dụ: 'VN25-001')
        DECLARE @Prefix CHAR(11);        -- BS + Root + - (Ví dụ: 'BSVN25-001-')
        DECLARE @LatestMaBS CHAR(14);
        DECLARE @Sequence INT;
        
        -- 1. KIỂM TRA ĐỊNH DẠNG MaTL
        SET @MaTL = RTRIM(LTRIM(@MaTL)); -- Loại bỏ khoảng trắng
        
        IF LEN(@MaTL) != 10 OR LEFT(@MaTL, 2) != 'TL'
        BEGIN
            RAISERROR('MaTL không hợp lệ. Định dạng yêu cầu: TLXXXX-### (10 ký tự).', 16, 1);
            RETURN;
        END
        
        -- 2. TRÍCH XUẤT PHẦN ROOT (8 ký tự sau 'TL')
        -- Ví dụ: TLBI25-001 → BI25-001
        SET @MaTL_Root = SUBSTRING(@MaTL, 3, 8);
        
        -- 3. TẠO PREFIX ĐẦY ĐỦ (11 ký tự)
        -- Ví dụ: BS + BI25-001 + - → BSBI25-001-
        SET @Prefix = 'BS' + @MaTL_Root + '-';
        
        -- Kiểm tra độ dài Prefix
        IF LEN(@Prefix) != 11
        BEGIN
            RAISERROR('Lỗi tạo Prefix. Độ dài không hợp lệ.', 16, 1);
            RETURN;
        END
        
        -- 4. TÌM MÃ LỚN NHẤT HIỆN TẠI (với khóa UPDLOCK, HOLDLOCK)
        -- Pattern: BSBI25-001-### (14 ký tự)
        SELECT TOP 1 @LatestMaBS = MaBS
        FROM dbo.tBanSao WITH (UPDLOCK, HOLDLOCK)
        WHERE MaBS LIKE @Prefix + '[0-9][0-9][0-9]'
          AND MaTL = @MaTL
          AND LEN(MaBS) = 14  -- Đảm bảo đúng độ dài
        ORDER BY MaBS DESC;
        
        -- 5. TÍNH SỐ THỨ TỰ
        IF @LatestMaBS IS NULL
        BEGIN
            -- Bản sao đầu tiên
            SET @Sequence = 1;
        END
        ELSE
        BEGIN
            -- Trích xuất 3 ký tự cuối
            DECLARE @LastThreeChars CHAR(3);
            SET @LastThreeChars = RIGHT(@LatestMaBS, 3);
            
            -- Kiểm tra có phải số không
            IF ISNUMERIC(@LastThreeChars) = 1
            BEGIN
                SET @Sequence = CAST(@LastThreeChars AS INT) + 1;
            END
            ELSE
            BEGIN
                RAISERROR('Mã bản sao hiện tại không hợp lệ. Không thể trích xuất số thứ tự.', 16, 1);
                RETURN;
            END
        END
        
        -- 6. KIỂM TRA GIỚI HẠN
        IF @Sequence > 999
        BEGIN
            RAISERROR('Không thể tạo thêm bản sao. Đã đạt giới hạn 999 bản sao cho tài liệu này.', 16, 1);
            RETURN;
        END
        
        -- 7. TẠO MÃ MỚI (Prefix + Số thứ tự 3 chữ số)
        -- Ví dụ: BSBI25-001- + 001 → BSBI25-001-001
        SET @NewMaBS = @Prefix + RIGHT('000' + CAST(@Sequence AS VARCHAR(3)), 3);
        
        -- 8. KIỂM TRA KẾT QUẢ
        IF LEN(@NewMaBS) != 14
        BEGIN
            RAISERROR('Mã bản sao được tạo không hợp lệ. Độ dài phải là 14 ký tự.', 16, 1);
            RETURN;
        END
        
        -- 9. KIỂM TRA TRÙNG LẶP (Phòng ngừa)
        IF EXISTS (SELECT 1 FROM dbo.tBanSao WHERE MaBS = @NewMaBS)
        BEGIN
            RAISERROR('Mã bản sao đã tồn tại. Vui lòng thử lại.', 16, 1);
            RETURN;
        END
        
        COMMIT TRANSACTION;
        
    END TRY
    BEGIN CATCH
        -- Rollback nếu có lỗi
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        -- Ném lỗi ra ngoài
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO