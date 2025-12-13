USE QLKhachSan

-- 2. Tạo Trigger tính tiền và điền tự động vào bảng tDoanhThu như sau:
-- Các trường lấy thông tin từ các bảng và các thông tin sau:
-- Trong đó:
-- (a) Số Ngày Ở= Ngày Ra – Ngày Vào
-- (b) ThucThu: Tính theo yêu cầu sau:
-- Nếu Số Ngày ở <10 Thành tiền = Đơn Giá * Số ngày ở
-- Nếu 10 <=Số Ngày ở <30 Thành Tiền = Đơn Giá* Số Ngày ở * 0.95 (Giảm5%)
-- Nếu Số ngày ở >= 30 Thành Tiền = Đơn Giá* Số Ngày ở * 0.9 (Giảm10%)

CREATE TRIGGER trg_1
ON tDoanhThu
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dt
    SET 
        SoNgayO = DATEDIFF(DAY, dk.NgayVao, dk.NgayRa),
        ThucThu = CASE 
            WHEN DATEDIFF(DAY, dk.NgayVao, dk.NgayRa) < 10 
                THEN lp.DonGia * DATEDIFF(DAY, dk.NgayVao, dk.NgayRa)
            WHEN DATEDIFF(DAY, dk.NgayVao, dk.NgayRa) < 30 
                THEN lp.DonGia * DATEDIFF(DAY, dk.NgayVao, dk.NgayRa) * 0.95
            ELSE 
                lp.DonGia * DATEDIFF(DAY, dk.NgayVao, dk.NgayRa) * 0.9
        END
    FROM tDoanhThu dt
    INNER JOIN inserted i ON dt.MaDK = i.MaDK
    INNER JOIN tDangKy dk ON dk.MaDK = i.MaDK
    INNER JOIN tLoaiPhong lp ON lp.LoaiPhong = dk.LoaiPhong;
END;

INSERT INTO tDoanhThu (MaDK, LoaiPhong, SoNgayO, ThucThu)
VALUES
('001', 'A', NULL, NULL)

SELECT 
    dt.MaDK,
    dt.LoaiPhong,
    dk.NgayVao,
    dk.NgayRa,
    dt.SoNgayO,
    dt.ThucThu
FROM tDoanhThu dt
JOIN tDangKy dk ON dt.MaDK = dk.MaDK;

-- 3. Thêm trường DonGia vào bảng tDangKy, tạo trigger cập nhật tự động cho trường này.
ALTER TABLE tDangKy
ADD DonGia INT NULL

CREATE TRIGGER trg_2
ON tDangKy
AFTER INSERT, UPDATE
AS
BEGIN
	SET NOCOUNT ON;

    UPDATE dk
    SET dk.DonGia = lp.DonGia
    FROM tDangKy dk
    INNER JOIN inserted i ON dk.MaDK = i.MaDK
    INNER JOIN tLoaiPhong lp ON lp.LoaiPhong = dk.LoaiPhong;
END

UPDATE tDangKy 
SET LoaiPhong = 'B'
WHERE MaDK = '001'

SELECT MaDK, LoaiPhong, DonGia FROM tDangKy;

-- 4. Thêm trường tổng tiêu dùng (TongTieuDung) và bảng khách hàng và tính tự động tổng
-- tiền khách hàng đã trả cho khách sạn mỗi khi thêm, sửa, xóa bản tDangKy

ALTER TABLE tChiTietKH
ADD TongTieuDung INT NULL

CREATE TRIGGER trg_3
ON tDangKY
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON;

	;WITH ChangedKH AS (
        SELECT MaDK FROM inserted
        UNION
        SELECT MaDK FROM deleted
    )
    UPDATE kh
    SET kh.TongTieuDung = ISNULL(
        (
            SELECT SUM(ISNULL(dt.ThucThu, 0))
            FROM tDoanhThu dt
            WHERE dt.MaDK = kh.MaDK
        ), 0
    )
    FROM tChiTietKH kh
    INNER JOIN ChangedKH c ON kh.MaDK = c.MaDK;
END