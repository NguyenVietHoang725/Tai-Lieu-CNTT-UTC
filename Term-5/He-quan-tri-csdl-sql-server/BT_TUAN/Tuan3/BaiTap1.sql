USE QLHocSinh

-- 1. Viết một Trigger gắn với bảng DIEM dựa trên sự kiện Insert, Update để tự động cập nhật
-- điểm trung bình của học sinh khi thêm mới hay cập nhật bảng điểm 
-- Điểm trung bình= ((Toán +Văn)*2+Lý+Hóa)/6

CREATE TRIGGER trg_1
ON DIEM
AFTER INSERT, UPDATE
AS
BEGIN
	UPDATE D
	SET DTB = ((D.TOAN + D.VAN) * 2 + D.LY + D.HOA) / 6
	FROM DIEM D
	INNER JOIN inserted I ON D.MAHS = I.MAHS
END

UPDATE DIEM
SET Toan = 9,
    Van = 6,
    Ly = 7,
    Hoa = 10
WHERE MaHS = '00026';

SELECT DTB
FROM DIEM
WHERE MAHS = '00026'

-- 2. Viết một Trigger gắn với bảng DIEM dựa trên sự kiện Insert, Update để tự động xếp loại
-- học sinh, cách thức xếp loại như sau: Nếu Điểm trung bình>=5 là lên lớp, ngược lại là lưu ban
DROP TRIGGER trg_2
CREATE TRIGGER trg_2
ON DIEM
AFTER INSERT, UPDATE
AS 
BEGIN
	IF TRIGGER_NESTLEVEL() > 1 RETURN; -- Kiểm tra độ sâu gọi lặp, tránh Trigger tự gọi chính nó
	
	UPDATE D
	SET XEPLOAI = 
	CASE
		WHEN D.DTB >= 5 THEN N'Lên lớp'
		ELSE N'Lưu ban'
	END
	FROM DIEM D
	INNER JOIN inserted I ON D.MAHS = I.MAHS
END

UPDATE DIEM
SET Toan = 3,
    Van = 6,
    Ly = 7,
    Hoa = 4
WHERE MaHS = '00025';

SELECT DTB, XEPLOAI
FROM DIEM
WHERE MAHS = '00025'

-- 3. Viết một Trigger gắn với bảng DIEM dựa trên sự kiện Insert, Update để tự động xếp loại
-- học sinh, cách thức xếp loại như sau
-- - Xét điểm thấp nhất (DTN) của các 4 môn
-- - Nếu DTB>=5 và DTN>=4 là “Lên Lớp”, ngược lại là lưu ban
CREATE TRIGGER trg_3
ON DIEM
AFTER INSERT, UPDATE
AS
BEGIN
	IF TRIGGER_NESTLEVEL() > 1 RETURN

	UPDATE D
	SET XEPLOAI =
	CASE	
		WHEN D.DTB >= 5 AND LEAST(D.TOAN, D.VAN, D.LY, D.HOA) >= 4 
		THEN N'Lên lớp'
		ELSE N'Lưu ban'
	END
	FROM DIEM D
	INNER JOIN inserted I ON D.MAHS = I.MAHS
END

UPDATE DIEM
SET Toan = 10,
    Van = 10,
    Ly = 10,
    Hoa = 3
WHERE MaHS = '00026';

SELECT DTB, XEPLOAI
FROM DIEM
WHERE MAHS = '00026'

-- 4. Viết một trigger xóa tự động bản ghi về điểm học sinh khi xóa dữ liệu học sinh đó trong DSHS
CREATE TRIGGER trg_4
ON DSHS
AFTER DELETE
AS
BEGIN
	DELETE D

	FROM DIEM D
	INNER JOIN deleted de ON D.MAHS = de.MAHS
END

DELETE FROM DSHS WHERE MaHS = '00026';

SELECT * FROM DIEM WHERE MaHS = '00026';