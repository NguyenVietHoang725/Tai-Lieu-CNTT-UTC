USE QLBanSach

-- 1. In ra danh sách các sách chỉ lấy (MaSach,TenSach) do Nhà xuất bản Giáo Dục xuất bản.
select
	MaSach,
	TenSach
from tSach S
join tNhaXuatBan NXB on S.MaNXB = NXB.MaNXB
where TenNXB = N'NXB Giáo dục'

-- 2. In ra danh sách các sách có tên bắt đầu là “Ngày”.
select
	TenSach
from tSach S
where TenSach like N'Ngày%'

-- 3. In ra danh sách các sách (MaSach,TenSach) do Nhà xuất bản Giáo Dục có giá từ 100.000 đến 150.000.
select
	MaSach,
	TenSach
from tSach S
join tNhaXuatBan NXB on S.MaNXB = NXB.MaNXB
where TenNXB = N'NXB Giáo Dục' and DonGiaBan >= 100000 and DonGiaBan <= 150000

-- 4. In ra danh sách các các sách (MaSach,TenSach) do Nhà xuất bản Giáo Dục hoặc Nhà Xuất Bản Trẻ sản xuất có giá từ 90.000 đến 140.000.
select 
	MaSach,
	TenSach
from tSach S
join tNhaXuatBan NXB on S.MaNXB = NXB.MaNXB
where (TenNXB = N'NXB Giáo dục' or TenNXB = N'NXB Trẻ')
	and DonGiaBan >= 90000 and DonGiaBan <= 140000

-- 5. In ra các số hóa đơn, trị giá hóa đơn bán ra trong ngày 1/1/2014 và ngày 31/12/2014.
select 
	HDB.SoHDB,
	SUM(CTHDB.SLBan * S.DonGiaBan) as TriGia
from tHoaDonBan HDB
join tChiTietHDB CTHDB on HDB.SoHDB = CTHDB.SoHDB
join tSach S on CTHDB.MaSach = S.MaSach
where HDB.NgayBan between '2014-01-01' and '2014-12-31'
group by HDB.SoHDB

-- 6. In ra các số hóa đơn, trị giá hóa đơn trong tháng 4/2014, sắp xếp theo ngày (tăng dần) và trị giá của hóa đơn (giảm dần).
select 
	HDB.SoHDB,
	SUM(CTHDB.SLBan * S.DonGiaBan * (1 - ISNULL(CTHDB.KhuyenMai,0))) as TriGia
from tHoaDonBan HDB
join tChiTietHDB CTHDB on HDB.SoHDB = CTHDB.SoHDB
join tSach S on CTHDB.MaSach = S.MaSach
where HDB.NgayBan between '2014-04-01' and '2014-04-30'
group by HDB.SoHDB, HDB.NgayBan
order by HDB.NgayBan asc, TriGia desc

-- 7. In ra danh sách các khách hàng (MaKH, TenKH) đã mua hàng trong ngày 10/4/2014.
select
	KH.MaKH,
	TenKH
from tKhachHang KH
join tHoaDonBan HDB on KH.MaKH = HDB.MaKH 
where NgayBan = '2014-04-10'

-- 8. In ra số hóa đơn, trị giá các hóa đơn do nhân viên có tên “Trần Huy” lập trong ngày “11/8/2014”
select 
	HDB.SoHDB,
	SUM(CTHDB.SLBan * S.DonGiaBan) as TriGia
from tHoaDonBan HDB
join tChiTietHDB CTHDB on HDB.SoHDB = CTHDB.SoHDB
join tSach S on CTHDB.MaSach = S.MaSach
join tNhanVien NV on HDB.MaNV = NV.MaNV
where HDB.NgayBan = '2014-08-11' and TenNV = N'Trần Huy'
group by HDB.SoHDB

-- 9. In ra danh sách các sách (MaSach,TenSach) được khách hàng có tên “Nguyễn Đình Sơn” mua trong tháng 8/2014.
select 
	S.MaSach,
	S.TenSach
from tSach S
join tChiTietHDB CTHDB on S.MaSach = CTHDB.MaSach
join tHoaDonBan HDB on CTHDB.SoHDB = HDB.SoHDB
join tKhachHang KH on HDB.MaKH = KH.MaKH
where TenKH = N'Nguyễn Đình Sơn' 
	and NgayBan between '2014-08-01' and '2014-08-31'

-- 10. Tìm các số hóa đơn đã mua sách “Cấu trúc dữ liệu và giải thuật”
select
	CTHDB.SoHDB
from tChiTietHDB CTHDB
join tSach S on CTHDB.MaSach = S.MaSach
where S.TenSach = N'Cấu trúc dữ liệu và giải thuật'

-- 11. Tìm các số hóa đơn đã mua sản phẩm có mã số “S01” hoặc “S02”, mỗi sản phẩm mua với số lượng từ 10 đến 20.
select
	CTHDB.SoHDB
from tChiTietHDB CTHDB
join tSach S on CTHDB.MaSach = S.MaSach
where (S.MaSach = N'S01' or S.MaSach = N'S02')
	and SLBan between 10 and 20

-- 12. Tìm các số hóa đơn mua cùng lúc 2 sản phẩm có mã số “S10” và “S11”, mỗi sản phẩm mua với số lượng từ 5 đến 10.

-- 13. In ra danh sách các sách không bán được.

-- 14. In ra danh sách các sách không bán được trong năm 2014.

-- 15. In ra danh sách các sách của NXB Giáo Dục không bán được trong năm 2014.

-- 16. Tìm số hóa đơn đã mua tất cả các sách của NXB Giáo Dục.

-- 17. Có bao nhiêu đầu sách khác nhau được bán ra trong năm 2014.

-- 18. Cho biết trị giá hóa đơn cao nhất, thấp nhất là bao nhiêu?

-- 19. Trị giá trung bình của tất cả các hóa đơn được bán ra trong năm 2014 là bao nhiêu?

-- 20. Tính doanh thu bán hàng trong năm 2014.

-- 21. Tìm số hóa đơn có trị giá cao nhất trong năm 2014.

-- 22. Tìm họ tên khách hàng đã mua hóa đơn có trị giá cao nhất trong năm 2014.

-- 23. In ra danh sách 3 khách hàng (MaKH, TenKH) có doanh số cao nhất.

-- 24. In ra danh sách các sách có giá bán bằng 1 trong 3 mức giá cao nhất.

-- 25. In ra danh sách các sách do NXB Giáo Dục sản xuất có giá bằng 1 trong 3 mức giá cao nhất (của tất cả các sản phẩm).

-- 26. Tính tổng số đầu sách do NXB Giáo Dục xuất bản.

-- 27. Tính tổng số sách của từng NXB.

-- 28. Với từng NXB, tìm giá bán cao nhất, thấp nhất, trung bình của các sản phẩm.

-- 29. Tính doanh thu bán hàng mỗi ngày.

-- 30. Tính tổng số lượng của từng sách bán ra trong tháng 10/2014.

-- 31. Tính doanh thu bán hàng của từng tháng trong năm 2014 (kể cả những tháng không có doanh thu, VD: Tháng 1: 0; Tháng 2: 12000000; Tháng 3: 0; Tháng ....)

-- 32. Tìm hóa đơn có mua ít nhất 4 sản phẩm khác nhau.

-- 33. Tìm hóa đơn có mua 3 sách do NXB Giáo Dục xuất bản (3 sách khác nhau).

-- 34. Tìm khách hàng (MaKH, TenKH) có số lần mua hàng nhiều nhất.

-- 35. Tháng mấy trong năm 2014, doanh số bán hàng cao nhất ?

-- 36. Tìm sách có tổng số lượng bán ra thấp nhất trong năm 2014.

-- 37. Mỗi NXB, tìm sách (MaSach,TenSach) có giá bán cao nhất.

-- 38. Giảm giá sách 10% cho các sách của NXB Giáo Dục

-- 39. Thêm trưởng tổng tiền cho bảng tHoaDonBan rồi cập nhật tổng tiền của hóa đơn cho trường này.

-- 40. Giảm 10% trên tổng hóa đơn cho các hóa đơn có trị giá trên 500.000 trong tháng 9/2014

-- 41. Tính tổng số lượng sách nhập trong năm 2014

-- 42. Tính tổng số lượng sách bán trong năm 2014

-- 43. Tính tổng tiền đã nhập trong năm 2014

-- 44. Xóa những hóa đơn do nhân viên "Trần Huy" lập (lưu ý xóa chi tiết hóa đơn trước)

-- 45. Đổi tên "NXB Thăng Long" thành "NXB Văn học

-- 46. Đưa ra thông tin toàn bộ sách, nếu sách được bán trong năm 2014 thì đưa ra SL bán
