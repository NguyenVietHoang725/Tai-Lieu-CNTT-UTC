use QuanLyDiemTruongDaiHoc

-- 1. Đưa ra danh sách sinh viên gồm mã sinh viên, họ và tên của lớp có tên “Công nghệ thông tin 2” khóa 61.
select	
	MaSinhVien as "Mã sinh viên",
	HoDem + ' ' + Ten as "Họ và tên",
	TenLop as "Lớp",
	KhoaHoc as "Khóa"
from Lop 
inner join SinhVien on SinhVien.MaLop = Lop.MaLop
where TenLop like N'%2' and KhoaHoc = 61

-- 2. Đưa ra danh sách các học phần và kỳ học của chương trình đào tạo có tên “CHẤT LƯỢNG CAO CÔNG NGHỆ THÔNG TIN VIỆT ANH” cho khóa 62.
select 
	TenHocPhan as "Tên học phần",
	KyHoc as "Kỳ học",
	TenCTDT as "Tên CTDT"
from ChuongTrinhDaoTao
inner join CTDT_HocPhan on ChuongTrinhDaoTao.MaCTDT = CTDT_HocPhan.MaCTDT
inner join SinhVien on ChuongTrinhDaoTao.MaCTDT = SinhVien.MaCTDT
inner join Lop on SinhVien.MaLop = Lop.MaLop
inner join HocPhan on CTDT_HocPhan.MaHocPhan = HocPhan.MaHocPhan
where TenCTDT like N'%CHẤT LƯỢNG CAO CÔNG NGHỆ THÔNG TIN VIỆT ANH' and KhoaHoc = 62

-- 3. Đưa ra danh sách các bộ môn của Khoa Công nghệ thông tin. 
select 
	TenBoMon as "Tên bộ môn",
	BoMon.TenTiengAnh as "Tên tiếng Anh"
from Khoa
inner join BoMon on Khoa.MaKhoa = BoMon.MaKhoa
where TenKhoa like N'Công nghệ thông tin%'

-- 4. Đưa ra danh sách giảng viên của Khoa Công nghệ thông tin. 
select
	MaGiangVien as "Mã giảng viên",
	HoDem + ' ' + Ten as "Họ và tên",
	NgaySinh as "Ngày sinh",
	GioiTinh as "Giới tính",
	HocHam as "Học hàm", 
	GiangVien.DienThoai as "SĐT",
	GiangVien.Email as "Email"
from Khoa
inner join BoMon on Khoa.MaKhoa = BoMon.MaKhoa
inner join GiangVien on BoMon.MaBoMon = GiangVien.MaBoMon
where TenKhoa like N'Công nghệ thông tin%'

-- 5. Đưa ra danh sách các lớp học phần của học phần Cơ sở dữ liệu trong năm học 2023 - 2024.
select 
	TenLopHocPhan as "Tên lớp học phần",
	HoDem + ' ' + Ten as "Giảng viên", 
	NamHoc as "Năm học", 
	HocKy as "Học kỳ",
	DotHoc as "Đợt học"
from HocPhan
inner join LopHocPhan on HocPhan.MaHocPhan = LopHocPhan.MaHocPhan
inner join GiangVien on LopHocPhan.MaGiangVien = GiangVien.MaGiangVien
where TenHocPhan like N'Cơ sở dữ liệu%' and NamHoc like '2023-2024%'

-- 6. Đưa ra số học phần mà mỗi bộ môn của Khoa Công nghệ thông tin phụ trách. 
select
	TenBoMon as "Tên bộ môn",
	COUNT(HocPhan.MaHocPhan) as "Số học phần phụ trách"
from Khoa
inner join BoMon on Khoa.MaKhoa = BoMon.MaKhoa
inner join HocPhan on BoMon.MaBoMon = HocPhan.MaBoMon
group by BoMon.MaBoMon, BoMon.TenBoMon

-- 7. Đưa ra thông tin chi tiết của một sinh viên có mã sinh viên là '201200085'.
select 
	MaSinhVien as "Mã sinh viên",
	HoDem + ' ' + Ten as "Họ và tên", 
	MaLop as "Lớp",
	NgaySinh as "Ngày sinh",
	GioiTinh as "Giới tính",
	DiaChi as "Địa chỉ",
	DienThoai as "SĐT",
	Email as "Email"
from SinhVien
where MaSinhVien = '201200085'

-- 8. Đưa ra danh sách các giảng viên trong một bộ môn “Mạng và các hệ thống thông tin” 
select 
	MaGiangVien as "Mã giảng viên",
	HoDem + ' ' + Ten as "Họ và tên",
	NgaySinh as "Ngày sinh",
	GioiTinh as "Giới tính",
	HocHam as "Học hàm", 
	GiangVien.DienThoai as "SĐT",
	GiangVien.Email as "Email"	
from GiangVien
inner join BoMon on GiangVien.MaBoMon = BoMon.MaBoMon
where TenBoMon like N'Mạng và các hệ thống thông tin%'

-- 9. Đưa ra thông tin của các chương trình đào tạo của khoa Công nghệ thông tin.
select 
	MaCTDT as "Mã CTDT",
	TenCTDT as "Tên CTDT", 
	TenCTDTTiengAnh as "Tên CTDT Tiếng Anh",
	ChuongTrinhDaoTao.MaKhoa as "Mã khoa",
	NganhApDung as "Ngành áp dụng",
	KhoaHocApDung as "Khóa học áp dụng"
from ChuongTrinhDaoTao 
inner join Khoa on ChuongTrinhDaoTao.MaKhoa = Khoa.MaKhoa
where TenKhoa like N'Công nghệ thông tin%'

-- 10. Đưa ra thông tin điểm số học phần cơ sở dữ liệu của sinh viên “Nguyễn Hoàng Lan”
select
	DiemQuaTrinh as "Điểm quá trình",
	DiemThiKTHP as "Điểm thi KTHP",
	DiemTKHP as "Điểm TKHP",
	DiemHeChu as "Điểm hệ chữ",
	DiemHe4 as "Điểm hệ 4",
	LanHoc,
	TenHocPhan
from SinhVien
inner join LopHocPhan_SinhVien on SinhVien.MaSinhVien = LopHocPhan_SinhVien.MaSinhVien
inner join LopHocPhan on LopHocPhan_SinhVien.MaHocPhan = LopHocPhan.MaHocPhan
inner join HocPhan on LopHocPhan.MaHocPhan = HocPhan.MaHocPhan
where HoDem like N'Nguyễn Hoàng%' and Ten like N'Lan%' and TenLopHocPhan like N'Lập trình Web%'
group by LopHocPhan_SinhVien.LanHoc

-- 11. Đưa ra danh sách các lớp học phần của giảng viên “Nguyễn Kim Sao” đã giảng dạy trong học kỳ 2, 3 của năm học 2023-2024.
select 
	TenLopHocPhan as "Tên lớp học phần",
	NamHoc as "Năm học",
	HocKy as "Học kỳ"
from GiangVien
inner join LopHocPhan on GiangVien.MaGiangVien = LopHocPhan.MaGiangVien
where
	HoDem like N'Nguyễn Kim' and Ten like N'Sao%' 
	and HocKy between 2 and 3
	and NamHoc like '2023-2024%'

-- 12. Đưa ra danh sách mã sinh viên, họ tên, lớp, ngày sinh, điểm thành phần, điểm thi kết thúc học phần, điểm tổng kết học phần của các sinh viên 
-- đang theo học mã học phần ‘IT1.110.3’ mã lớp học phần ‘1-2-22-QT08’
select 
	SinhVien.MaSinhVien as "MSV",
	HoDem + ' ' + Ten as "Họ và tên",
	MaLop as "Lớp",
	NgaySinh as "Ngày sinh",
	DiemQuaTrinh as "Điểm quá trình",
	DiemThiKTHP as "Điểm thi KTHP",
	DiemTKHP as "Điểm TKHP"
from LopHocPhan_SinhVien
inner join LopHocPhan on LopHocPhan_SinhVien.MaLopHocPhan = LopHocPhan.MaLopHocPhan
inner join SinhVien on LopHocPhan_SinhVien.MaSinhVien = SinhVien.MaSinhVien
where LopHocPhan.MaHocPhan = 'IT1.110.3' and LopHocPhan.MaLopHocPhan = '1-2-22-QT08'

-- 13. Đưa ra thông tin của các giảng viên là trưởng bộ môn (chủ nhiệm bộ môn) cùng với thông tin về bộ môn mà họ quản lý. 
select
	MaGiangVien as "Mã giảng viên",
	HoDem + ' ' + Ten as "Họ và tên",
	NgaySinh as "Ngày sinh",
	GioiTinh as "Giới tính",
	HocHam as "Học hàm", 
	GiangVien.DienThoai as "SĐT",
	GiangVien.Email as "Email", 
	TenBoMon as "Trưởng bộ môn",
	MaKhoa as "Mã khoa"
from GiangVien
inner join BoMon on GiangVien.MaGiangVien = BoMon.TruongBoMon

-- 14. Cập nhật cột điểm tổng kết học phần cho bảng LopHocPhan_SinhVien. 
update LopHocPhan_SinhVien
set 
	DiemTKHP = DiemQuaTrinh*TrongSoDiemQuaTrinh + DiemThiKTHP*TrongSoDiemThiKTHP
from LopHocPhan_SinhVien
inner join LopHocPhan on LopHocPhan_SinhVien.MaHocPhan = LopHocPhan.MaHocPhan
inner join HocPhan on LopHocPhan.MaHocPhan = HocPhan.MaHocPhan

select
	DiemTKHP as "Điểm TKHP"
from LopHocPhan_SinhVien
inner join LopHocPhan on LopHocPhan_SinhVien.MaHocPhan = LopHocPhan.MaHocPhan
inner join HocPhan on LopHocPhan.MaHocPhan = HocPhan.MaHocPhan

-- 15. Cập nhật cột điểm hệ 4, điểm hệ chữ cho bảng LopHocPhan_SinhVien.
update LopHocPhan_SinhVien
set 
	DiemHeChu = case
		when DiemTKHP >= 0.0 and DiemTKHP < 2.0 then 'F'
		when DiemTKHP >= 2.0 and DiemTKHP < 4.0 then 'F+'
		when DiemTKHP >= 4.0 and DiemTKHP < 4.5 then 'D'
		when DiemTKHP >= 4.5 and DiemTKHP < 5.5 then 'D+'
		when DiemTKHP >= 5.5 and DiemTKHP < 6.0 then 'C'
		when DiemTKHP >= 6.0 and DiemTKHP < 7.0 then 'C+'
		when DiemTKHP >= 7.0 and DiemTKHP < 8.0 then 'B'
		when DiemTKHP >= 8.0 and DiemTKHP < 8.5 then 'B+'
		when DiemTKHP >= 8.5 and DiemTKHP < 9.5 then 'A'
		when DiemTKHP >= 9.5 and DiemTKHP <= 10 then 'A+'
	end,
	DiemHe4 = case
		when DiemTKHP >= 0.0 and DiemTKHP < 2.0 then 0
		when DiemTKHP >= 2.0 and DiemTKHP < 4.0 then 0.5
		when DiemTKHP >= 4.0 and DiemTKHP < 4.5 then 1.0
		when DiemTKHP >= 4.5 and DiemTKHP < 5.5 then 1.5
		when DiemTKHP >= 5.5 and DiemTKHP < 6.0 then 2.0
		when DiemTKHP >= 6.0 and DiemTKHP < 7.0 then 2.5
		when DiemTKHP >= 7.0 and DiemTKHP < 8.0 then 3.0
		when DiemTKHP >= 8.0 and DiemTKHP < 8.5 then 3.5
		when DiemTKHP >= 8.5 and DiemTKHP < 9.5 then 3.8
		when DiemTKHP >= 9.5 and DiemTKHP <= 10 then 4.0
	end
from LopHocPhan_SinhVien
inner join LopHocPhan on LopHocPhan_SinhVien.MaHocPhan = LopHocPhan.MaHocPhan
inner join HocPhan on LopHocPhan.MaHocPhan = HocPhan.MaHocPhan

select
	DiemTKHP as "Điểm TKHP",
	DiemHeChu as "Điểm hệ chữ",
	DiemHe4 as "Điểm hệ 4"
from LopHocPhan_SinhVien
inner join LopHocPhan on LopHocPhan_SinhVien.MaHocPhan = LopHocPhan.MaHocPhan
inner join HocPhan on LopHocPhan.MaHocPhan = HocPhan.MaHocPhan

-- 16. Thêm một sinh viên mới cho bảng sinh viên.
insert into SinhVien(MaSinhVien, HoDem, Ten, MaLop, NgaySinh, GioiTinh, DiaChi, DienThoai, Email)
values ('231230791', 'Nguyễn Việt', 'Hoàng', 'K61.CNTT2', '2005-02-07', 1, 'Hà Nội', '0388577967', 'hoangnguyen72.personal@gmail.com')

select 
	MaSinhVien as "Mã sinh viên",
	HoDem + ' ' + Ten as "Họ và tên", 
	MaLop as "Lớp",
	NgaySinh as "Ngày sinh",
	GioiTinh as "Giới tính",
	DiaChi as "Địa chỉ",
	DienThoai as "SĐT",
	Email as "Email"
from SinhVien
where MaSinhVien = '231230791'

-- 17. Xóa những lớp học phần không có sinh viên.
delete from LopHocPhan
where not exists (
	select 1
	from LopHocPhan_SinhVien
	where LopHocPhan.MaLopHocPhan = LopHocPhan_SinhVien.MaLopHocPhan
)

select
	LopHocPhan.MaLopHocPhan as "Mã lớp học phần",
	TenLopHocPhan as "Tên lớp học phần",
	count(SinhVien.MaSinhVien) as "Số lượng sinh viên"
from SinhVien
inner join LopHocPhan_SinhVien on SinhVien.MaSinhVien = LopHocPhan_SinhVien.MaSinhVien
inner join LopHocPhan on LopHocPhan_SinhVien.MaHocPhan = LopHocPhan.MaHocPhan
inner join HocPhan on LopHocPhan.MaHocPhan = HocPhan.MaHocPhan
group by LopHocPhan.MaLopHocPhan, LopHocPhan.TenLopHocPhan

-- 18. Cập nhật địa chỉ và số điện thoại cho SV có mã ‘201200043’.
update SinhVien
set 
	DiaChi = 'Hà Nội',
	DienThoai = '0123456789'
where MaSinhVien = '201200043'

select
	MaSinhVien as "MSV",
	HoDem + ' ' + Ten as "Họ và tên",
	DiaChi as "Địa chỉ",
	DienThoai as "SĐT"
from SinhVien
where MaSinhVien = '201200043'

-- 19. Đưa ra thông tin chi tiết về các lớp học phần mà sinh viên có mã sinh viên là '212606016' đang học, bao gồm cả điểm số của sinh viên đó 
-- (mã học phần, tên học phần, mã lớp học phần, điểm quá trình, điểm thi kết thúc học phần, điểm tổng kết học phần).
select
	HocPhan.MaHocPhan as "Mã học phần",
	TenHocPhan as "Tên học phần",
	LopHocPhan.MaLopHocPhan as "Mã lớp học phần",
	DiemQuaTrinh as "Điểm QT",
	DiemThiKTHP as "Điểm thi KTHP",
	DiemTKHP as "Điểm TKHP"
from SinhVien
inner join LopHocPhan_SinhVien on SinhVien.MaSinhVien = LopHocPhan_SinhVien.MaSinhVien
inner join LopHocPhan on LopHocPhan_SinhVien.MaHocPhan = LopHocPhan.MaHocPhan and LopHocPhan.MaLopHocPhan = LopHocPhan_SinhVien.MaLopHocPhan
inner join HocPhan on LopHocPhan.MaHocPhan = HocPhan.MaHocPhan
where SinhVien.MaSinhVien = '212606016'

-- 20. Tính tổng số tín chỉ đã tích lũy của sinh viên viên có mã sinh viên là '212606016' dựa trên các học phần đã hoàn thành và điểm số.
select
	SinhVien.MaSinhVien as "Mã sinh viên",
	HoDem + ' ' + Ten as "Họ và tên",
	sum(SoTinChi) as "Số tín chỉ tích lũy"
from SinhVien
inner join LopHocPhan_SinhVien on SinhVien.MaSinhVien = LopHocPhan_SinhVien.MaSinhVien
inner join LopHocPhan on LopHocPhan_SinhVien.MaHocPhan = LopHocPhan.MaHocPhan and LopHocPhan_SinhVien.MaLopHocPhan = LopHocPhan.MaLopHocPhan
inner join HocPhan on LopHocPhan.MaHocPhan = HocPhan.MaHocPhan
where SinhVien.MaSinhVien = '212606016' and LopHocPhan_SinhVien.DiemTKHP >= 4
group by SinhVien.MaSinhVien, HoDem, Ten
-- 21. Xác định xem sinh viên có mã sinh viên là '212606016' đã học các học phần tiên quyết của học phần có mã ‘IE3.003.3’ chưa. 
select 
	RangBuoc as "Xác nhận"
from SinhVien
inner join ChuongTrinhDaoTao on SinhVien.MaCTDT = ChuongTrinhDaoTao.MaCTDT
inner join CTDT_HocPhan on ChuongTrinhDaoTao.MaCTDT = CTDT_HocPhan.MaCTDT
inner join HocPhan on CTDT_HocPhan.MaHocPhan = HocPhan.MaHocPhan
inner join HocPhanTienQuyet on HocPhan.MaHocPhan = HocPhanTienQuyet.MaHocPhan
where MaHocPhanTienQuyet = 'IE3.003.3'

-- 22. Đếm số lượng sinh viên đã đăng ký theo từng học phần trong năm học 2023-2024, học kỳ 2, sắp xếp theo số lượng sinh viên giảm dần.
select
	HocPhan.MaHocPhan as "Mã học phần",
	TenHocPhan as "Tên học phần",
	count(SinhVien.MaSinhVien) as "Số lượng đăng ký"
from SinhVien
inner join LopHocPhan_SinhVien on SinhVien.MaSinhVien = LopHocPhan_SinhVien.MaSinhVien
inner join LopHocPhan on LopHocPhan_SinhVien.MaHocPhan = LopHocPhan.MaHocPhan
inner join HocPhan on LopHocPhan.MaHocPhan = HocPhan.MaHocPhan
where NamHoc = '2023-2024' and HocKy = '2'
group by HocPhan.MaHocPhan, HocPhan.TenHocPhan
order by "Số lượng đăng ký" desc

-- 23. Tính điểm trung bình của các sinh viên khoa Công nghệ thông tin theo từng chương trình đào tạo.
select 
	TenCTDT as "Tên CTDT",
	avg(DiemTKHP) as "Điểm trung bình"
from ChuongTrinhDaoTao
inner join Khoa on ChuongTrinhDaoTao.MaKhoa = Khoa.MaKhoa
inner join CTDT_HocPhan on ChuongTrinhDaoTao.MaCTDT = CTDT_HocPhan.MaCTDT
inner join LopHocPhan on CTDT_HocPhan.MaHocPhan = LopHocPhan.MaHocPhan
inner join LopHocPhan_SinhVien on LopHocPhan.MaLopHocPhan = LopHocPhan_SinhVien.MaLopHocPhan
where TenKhoa like N'Công nghệ thông tin%'
group by TenCTDT

-- 24. Đưa ra danh sách các học phần mà sinh viên có mã sinh viên là '212606016' chưa hoàn thành (có điểm dưới tổng kết học phần dưới 4 hoặc chưa đăng ký học) theo một chương trình đào tạo.
select distinct
	HP.MaHocPhan,
	TenHocPhan
from ChuongTrinhDaoTao CTDT
join CTDT_HocPhan on CTDT_HocPhan.MaCTDT = CTDT.MaCTDT
join LopHocPhan LHP on CTDT_HocPhan.MaHocPhan = LHP.MaHocPhan
left join LopHocPhan_SinhVien LHP_SV on LHP.MaLopHocPhan = LHP_SV.MaLopHocPhan and LHP.MaHocPhan = LHP_SV.MaHocPhan and LHP_SV.MaSinhVien = N'212606016'
join HocPhan HP on LHP.MaHocPhan = HP.MaHocPhan
where DiemTKHP < 4 or LHP_SV.MaSinhVien is null

-- 25. Đưa ra thông tin về các sinh viên và giảng viên của khoa Công nghệ thông tin có sinh nhật trong tháng hiện tại.

-- 26. Tính tổng số tín chỉ của các học phần đã hoàn thành bởi sinh viên có mã sinh viên là '212606016'  dựa trên các học phần  đã học có điểm tổng kết học phần từ 4 trở lên. 
select sum(SoTinChi) as "Tổng tín chỉ"
from LopHocPhan_SinhVien
inner join LopHocPhan on LopHocPhan_SinhVien.MaLopHocPhan = LopHocPhan.MaLopHocPhan
inner join HocPhan on LopHocPhan.MaHocPhan = HocPhan.MaHocPhan
where MaSinhVien = '212606016'
and LopHocPhan_SinhVien.DiemTKHP >= 4

-- 27. Liệt kê các giảng viên chưa được phân công lớp học phần trong năm học 2023-2024, học kỳ 2. 

-- 28. Liệt kê các sinh viên có điểm trung bình dưới điểm trung bình của lớp học phần có mã học phần ‘IT1.110.3’ mã lớp học phần ‘1-2-22-QT08’ 

-- 29. Đưa ra số phần trăm của các sinh viên có điểm trên 8,5; trên 7 và dưới 8,5; dưới 7 và trên 5,5; dưới 5,5 và trên 4; dưới 4 của mỗi lớp học phần của Khoa Công nghệ thông tin của học kỳ 2 năm học 2023-2024. 

-- 30. Đưa ra danh sách các sinh viên có điểm bằng điểm số cao nhất của lớp học phần có mã học phần ‘IT1.110.3’ mã lớp học phần ‘1-2-22-QT08’. 
select
	SinhVien.MaSinhVien as "MSV",
	HoDem + ' ' + Ten as "Họ và tên",
	MaLop as "Mã lớp",
	DiemTKHP as "Điểm TKHP"
from SinhVien
inner join LopHocPhan_SinhVien on SinhVien.MaSinhVien = LopHocPhan_SinhVien.MaSinhVien
inner join LopHocPhan on LopHocPhan_SinhVien.MaLopHocPhan = LopHocPhan.MaLopHocPhan
where DiemTKHP = (
	select max(DiemTKHP) 
	from LopHocPhan_SinhVien 
	where MaHocPhan = 'IT1.110.3' and MaLopHocPhan = '1-2-22-QT08'
)

-- 31. Đưa ra danh sách các giảng viên và số lớp học phần giảng viên đang đảm nhiệm của học kỳ 2 năm học 2023-2024.  
select
	GiangVien.MaGiangVien as "MGV",
	HoDem + ' ' + Ten as "Họ và tên",
	count(LopHocPhan.MaHocPhan) as "Số lớp học phần đang đảm nhiệm"
from GiangVien
inner join LopHocPhan on GiangVien.MaGiangVien = LopHocPhan.MaGiangVien
where HocKy = 2 and NamHoc = '2023-2024'
group by GiangVien.MaGiangVien, HoDem, Ten

-- 32. Danh sách các sinh viên có điểm trung bình cao hơn điểm trung bình của lớp học phần có mã học phần ‘IT1.110.3’ mã lớp học phần ‘1-2-22-QT08’. 

-- 33. Đưa ra danh sách các giảng viên không có sinh viên nào đăng ký trong các lớp học phần do họ giảng dạy. 

-- 34. Đưa ra danh sách các sinh viên đạt điểm cao nhất trong từng lớp học phần. 
select
	SinhVien.MaSinhVien as "MSV",
	HoDem + ' ' + Ten as "Họ và tên",
	TenLopHocPhan as "Tên lớp học phần",
	DiemTKHP as "Điểm TKHP"
from LopHocPhan_SinhVien
inner join LopHocPhan on LopHocPhan_SinhVien.MaLopHocPhan = LopHocPhan.MaLopHocPhan
inner join SinhVien on LopHocPhan_SinhVien.MaSinhVien = SinhVien.MaSinhVien
inner join (
	select MaLopHocPhan, max(DiemTKHP) as DiemCaoNhat
	from LopHocPhan_SinhVien
	group by MaLopHocPhan
) as MaxDiem on LopHocPhan_SinhVien.MaLopHocPhan = MaxDiem.MaLopHocPhan
	and LopHocPhan_SinhVien.DiemTKHP = MaxDiem.DiemCaoNhat

-- 35. Đưa ra danh sách các sinh viên có điểm dưới 4 của các lớp học phần có mã học phần ‘IT1.110.3’ mã lớp học phần ‘1-2-22-QT08’. 

-- 36. Đưa ra danh sách các học phần mà chưa có sinh viên chưa đăng ký. 

-- 37. Đưa ra danh sách các học phần và học phần tiên quyết của nó (nếu có). 
+

-- 38. Tính điểm trung bình của các sinh viên trong từng lớp học phần. 
select 
	TenLopHocPhan as "Tên lớp học phần",
	avg(DiemTKHP) as "Điểm trung bình"
from LopHocPhan_SinhVien
inner join LopHocPhan on LopHocPhan_SinhVien.MaLopHocPhan = LopHocPhan.MaLopHocPhan
group by TenLopHocPhan

-- 39. Truy vấn để tính tổng số tín chỉ đã tích lũy của từng sinh viên. 

-- 40. Đưa ra các giảng viên dạy nhiều lớp học phần nhiều nhất trong học kỳ 2 năm học 2023-2024. 
select 
	GiangVien.MaGiangVien as "MGV",
	HoDem + ' ' + Ten as "Họ và tên",
	count(LopHocPhan.MaLopHocPhan) as "Số lớp học phần"
from GiangVien
inner join LopHocPhan on GiangVien.MaGiangVien = LopHocPhan.MaGiangVien
where HocKy = 2 and NamHoc = '2023-2024'
group by GiangVien.MaGiangVien, GiangVien.HoDem, GiangVien.Ten
having count(LopHocPhan.MaLopHocPhan) = (
	select max("Số lớp học phần")
	from (
		select count(LopHocPhan.MaLopHocPhan) as "Số lớp học phần"
		from GiangVien
		inner join LopHocPhan on GiangVien.MaGiangVien = LopHocPhan.MaGiangVien
		where LopHocPhan.HocKy = 2 and LopHocPhan.NamHoc = '2023-2024'
		group by GiangVien.MaGiangVien
	) as subquery
) 

-- 41. Liệt kê tổng số lượng sinh viên đăng ký học tại các lớp học phần theo từng năm học và học kỳ. 

-- 42. Đưa ra danh sách các khoa có số lượng sinh viên lớn hơn 2000. 
select 
	TenKhoa as "Tên khoa",
	count(MaSinhVien) as "Số lượng sinh viên"
from Khoa
inner join Lop on Khoa.MaKhoa = Lop.MaKhoa
inner join SinhVien on Lop.MaLop = SinhVien.MaLop
group by TenKhoa

-- 43. Tìm các học phần của Khoa Công nghệ thông tin có điểm trung bình cao hơn các điểm trung bình của các học phần khác của khoa Công nghệ thông tin trong học kỳ 2 năm học 2023-2024 

-- 44. Tìm các học phần có sinh viên nào đạt điểm dưới 4 nhiều nhất. 

-- 45. Đưa ra danh sách các học phần đã học có điểm tổng kết học phần >= 4 của sinh viên có mã '201200085'. 

-- 46. Đưa ra danh sách các học phần đã học có điểm tổng kết học phần < 4 và các học phần chưa học của sinh viên có mã '201200085'.