USE QLBanSach
-- 1.Tạo view in ra danh sách các sách của nhà xuất bản giáo dục nhập trong năm 2021
CREATE VIEW vSachNXBGiaoDuc2021 AS
SELECT s.MaSach, s.TenSach
FROM tSach AS s
JOIN tNhaXuatBan AS nxb ON nxb.MaNXB = s.MaNXB
JOIN tChiTietHDN AS ctd ON ctd.MaSach = s.MaSach
JOIN tHoaDonNhap AS hdn ON hdn.SoHDN = ctd.SoHDN
WHERE nxb.TenNXB = N'NXB Giáo Dục'
  AND YEAR(hdn.NgayNhap) = 2021;

-- 2.Tạo view thống kê các sách không bán được trong năm 2021
CREATE VIEW vSachKhongBan2021 AS
SELECT s.MaSach, s.TenSach
FROM tSach AS s
WHERE s.MaSach NOT IN (
    SELECT ctd.MaSach
    FROM tChiTietHDB AS ctd
    JOIN tHoaDonBan AS hdb ON hdb.SoHDB = ctd.SoHDB
    WHERE YEAR(hdb.NgayBan) = 2021
);

-- 3.Tạo view thống kê 10 khách hàng có tổng tiền tiêu dùng cao nhất trong năm 2021
CREATE VIEW vTop10KhachHang2021 AS
SELECT TOP 10 
    kh.MaKH,
    kh.TenKH,
    SUM(ctd.SLBan * s.DonGiaBan * (1 - ISNULL(ctd.KhuyenMai, 0))) AS TongTien
FROM tKhachHang AS kh
JOIN tHoaDonBan AS hdb ON kh.MaKH = hdb.MaKH
JOIN tChiTietHDB AS ctd ON hdb.SoHDB = ctd.SoHDB
JOIN tSach AS s ON ctd.MaSach = s.MaSach
WHERE YEAR(hdb.NgayBan) = 2021
GROUP BY kh.MaKH, kh.TenKH
ORDER BY TongTien DESC;

-- 4.Tạo view thống kê số lượng sách bán ra trong năm 2021 và số lượng sách nhập 
-- trong năm 2021 và chênh lệch giữa hai số lượng này ứng với mỗi đầu sách