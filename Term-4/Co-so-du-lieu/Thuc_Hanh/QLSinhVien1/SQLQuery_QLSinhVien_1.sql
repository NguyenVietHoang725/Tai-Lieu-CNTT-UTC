-- 1. Liệt kê danh sách sinh viên, gồm các thông tin sau: 
-- Mã sinh viên, Họ sinh viên, Tên sinh viên, Học bổng. 
-- Danh sách sẽ được sắp xếp theo thứ tự Mã sinh viên tăng dần.

select
	maSV as "Mã sinh viên",
	HoSV as "Họ sinh viên",
	TenSV as "Tên sinh viên",
	HocBong as "Học bổng"
from DSSinhVien
order by maSV asc

-- 2. Danh sách các sinh viên gồm thông tin sau: 
-- Mã sinh viên, họ tên sinh viên, Phái, Ngày sinh.
-- Danh sách sẽ được sắp xếp theo thứ tự Nam/Nữ.

select 
	maSV as "Mã sinh viên",
	HoSV + ' ' + TenSV as "Họ và tên sinh viên",
	Phai as "Phái"
from DSSinhVien
order by Phai asc

-- 3. Thông tin các sinh viên gồm: Họ tên sinh viên, Ngày sinh, Học bổng. 
-- Thông tin sẽ được sắp xếp theo thứ tự Ngày sinh tăng dần và Học bổng giảm dần.

select 
	HoSV + ' ' + TenSV as "Họ và tên sinh viên",
	NgaySinh as "Ngày sinh",
	HocBong as "Học bổng"
from DSSinhVien
order by NgaySinh asc, HocBong desc

-- 4. Liệt kê các sinh viên có học bổng từ 150,000 trở lên và sinh ở Hà Nội, 
-- gồm các thông tin: Họ tên sinh viên, Mã khoa, Nơi sinh, Học bổng.

select 
	HoSV + ' ' + TenSV as "Họ và tên sinh viên",
	MaKhoa as "Mã khóa",
	NoiSinh as "Nơi sinh",
	HocBong as "Học bổng"
from DSSinhVien
where HocBong >= 150000 and NoiSinh like N'Hà Nội'

-- 5. Danh sách những sinh viên có học bổng từ 80.000 đến 150.000, 
-- gồm các thông tin: Mã sinh viên, Ngày sinh, Phái, Mã khoa.

select 
	MaSV as "Mã sinh viên",
	NgaySinh as "Ngày sinh",
	Phai as "Phái",
	MaKhoa as "Mã khoa"
from DSSinhVien
where HocBong between 80000 and 150000

-- Cho biết những môn học có số tiết lớn hơn 30 và nhỏ hơn 45, 
-- gồm các thông tin: Mã môn học, Tên môn học, Số tiết.

select
	MaMH as "Mã môn học",
	TenMH as "Tên môn học",
	SoTiet as "Số tiết"
from DMMonHoc
where SoTiet between 30 and 45

-- 7. Danh sách những sinh viên có tuổi từ 20 đến 25, thông tin gồm: 
-- Họ tên sinh viên, Tuổi, Tên khoa.

select 
	HoSV + ' ' + TenSV as "Họ và tên sinh viên",
	DATEDIFF(YEAR, NgaySinh, GETDATE())
	- case
		when format(NgaySinh, 'MM-dd') > format(GETDATE(), 'MM-dd') then 1
		else 0
	end as "Tuổi"
from DSSinhVien
where DATEDIFF(YEAR, NgaySinh, GETDATE()) between 20 and 25

-- 8. Cho biết thông tin về mức học bổng của các sinh viên, gồm: 
-- Mã sinh viên, Phái, Mã khoa, Mức học bổng. Trong đó, mức học bổng 
-- sẽ hiển thị là “Học bổng cao” nếu giá trị của field học bổng 
-- lớn hơn 500,000 và ngược lại hiển thị là “Mức trung bình”

select
	MaSV as "Mã sinh viên",
	Phai as "Phái",
	MaKhoa as "Mã khoa",
	HocBong,
	case
		when HocBong > 500000 then 'Học bổng cao'
		else 'Mức trung bình'
	end as "Mức học bổng"
from DSSinhVien

-- 9. Cho biết tổng số sinh viên của toàn trường

select 
	count(*) as "Tổng số sinh viên"
from DSSinhVien

-- 10. Cho biết tổng sinh viên và tổng sinh viên nữ.

select
	count(*) as "Tổng số sinh viên",
	count(case when RTRIM(Phai) = N'Nữ' then 1 end) as Nu
from DSSinhVien

-- 11. Cho biết tổng số sinh viên của từng khoa.

select 
	TenKhoa as "Khoa",
	count(MaSV) as "Tổng số sinh viên"
from DSSinhVien
inner join DMKhoa on DSSinhVien.MaKhoa = DMKhoa.MaKhoa
group by TenKhoa

-- 12. Cho biết số lượng sinh viên học từng môn.
select
	TenMH as "Tên môn học",
	count(DSSinhVien.MaSV) as "Tổng số sinh viên học"
from DSSinhVien
inner join KetQua on DSSinhVien.maSV = KetQua.maSV
inner join DMMonHoc on KetQua.maMH = DMMonHoc.maMH
group by TenMH

-- 13. Cho biết số lượng môn học mà sinh viên đã học(tức tổng số môn học có trong bảng kq)

select
	count(distinct MaMH) as "Tổng số lượng môn học mà sinh viên đã học"
from KetQua 

-- 14. Cho biết tổng số học bổng của mỗi khoa.

select
	TenKhoa as "Tên khoa",
	count(case when HocBong > 0 then 1 end) as "Tổng số học bổng"
from DSSinhVien
inner join DMKhoa on DSSinhVien.MaKhoa = DMKhoa.MaKhoa
group by TenKhoa

-- 15. Cho biết học bổng cao nhất của mỗi khoa.

select 
	TenKhoa as "Tên khoa",
	max(HocBong) as "Học bổng cao nhất"
from DSSinhVien
inner join DMKhoa on DSSinhVien.MaKhoa = DMKhoa.MaKhoa
group by TenKhoa

-- 16. Cho biết tổng số sinh viên nam và tổng số sinh viên nữ của mỗi khoa.

select
	TenKhoa as "Tên khoa",
	count(case when RTRIM(Phai) = N'Nam' then 1 end) as "Tổng số sinh viên nam",
	count(case when RTRIM(Phai) = N'Nữ' then 1 end) as "Tổng số sinh viên nữ"
from DSSinhVien
inner join DMKhoa on DSSinhVien.MaKhoa = DMKhoa.MaKhoa
group by TenKhoa

-- 17. Cho biết những năm sinh nào có 2 sinh viên đang theo học tại trường.

select
	YEAR(NgaySinh) as "Năm sinh"
from DSSinhVien
group by YEAR(NgaySinh)
having count(*) = 2

-- 18. Cho biết những sinh viên thi lại trên 2 lần.

select 
	distinct HoSV + ' ' + TenSV as "Họ tên SV thi lại trên 2 lần"
from DSSinhVien
inner join KetQua on DSSinhVien.MaSV = KetQua.MaSV
where LanThi >= 2

-- 19. Đưa ra điểm trung bình của sinh viên có mã ‘A06’

select
	avg(Diem) as "Điểm trung bình"
from KetQua
where MaSV = 'A02'

-- 20. Thống kê số học sinh học cho mỗi môn học

select
	TenMH as "Tên môn học",
	count(distinct MaSV) as "Tổng số sinh viên"
from DMMonHoc
inner join KetQua on DMMonHoc.MaMH = KetQua.MaMH
group by TenMH

-- 21. Đưa ra danh sách sinh viên gồm mã sinh viên, họ và tên, ngày sinh, 
-- tên khoa học, điểm trung bình

select
	DSSinhVien.MaSV as "Mã sinh viên",
	HoSV + ' ' + TenSV as "Họ và tên",
	NgaySinh as "Ngày sinh",
	TenKhoa as "Khoa",
	avg(Diem) as "Điểm TB"
from DSSinhVien
inner join DMKhoa on DSSinhVien.MaKhoa = DMKhoa.MaKhoa
inner join KetQua on DSSinhVien.MaSV = KetQua.MaSV
group by DSSinhVien.MaSV, HoSV, TenSV, NgaySinh, TenKhoa

-- 22. Đưa ra danh sách sinh viên xuất sắc gồm mã sinh viên, họ và tên, ngày sinh,
-- tên khoa học, điểm trung bình với điểm trunh bình >= 9.0

select
	DSSinhVien.MaSV as "Mã sinh viên",
	HoSV + ' ' + TenSV as "Họ và tên",
	NgaySinh as "Ngày sinh",
	TenKhoa as "Khoa",
	avg(Diem) as "Điểm TB"
from DSSinhVien
inner join DMKhoa on DSSinhVien.MaKhoa = DMKhoa.MaKhoa
inner join KetQua on DSSinhVien.MaSV = KetQua.MaSV
group by DSSinhVien.MaSV, HoSV, TenSV, NgaySinh, TenKhoa
having avg(Diem) >= 9.0

-- 23. Cho biết thông tin của các sinh viên, gồm: Mã sinh viên,tên sinh viên, 
-- Phái, Mã khoa, Điểm lần 1 môn có mã 01 (nếu có).

select
	DSSinhVien.MaSV as "Mã sinh viên",
	HoSV + ' ' + TenSV as "Họ và tên",
	NgaySinh as "Ngày sinh",
	TenKhoa as "Khoa",
	Diem as "Điểm"
from DSSinhVien
inner join DMKhoa on DSSinhVien.MaKhoa = DMKhoa.MaKhoa
inner join KetQua on DSSinhVien.MaSV = KetQua.MaSV
where MaMH = '01' and LanThi = 1

-- 24. Thêm trường TinhTrang (tình trạng) vào bảng kết quả. 
-- Cập nhật dữ liệu cho trường này biết rằng nếu điểm trung bình 
-- (điểm trung bình được tính như câu 2.3) < 4 ghi 0, từ 4 đến dưới 5.5 ghi 1,
-- còn lại không ghi (null).

alter table KetQua
add TinhTrang tinyint;

update KetQua
set TinhTrang = case
    when DiemTB < 4 then 0
    when DiemTB between 4 and 5.49 then 1
    else null
end
from KetQua
join (
    select MaSV, avg(Diem) as DiemTB
    from KetQua
    group by MaSV
) as TB on KetQua.MaSV = TB.MaSV;

select MaSV, avg(Diem) as DiemTB, TinhTrang
from KetQua
group by MaSV, TinhTrang
order by MaSV;

-- 25. Xoá tất cả những sinh viên chưa dự thi môn nào.

select * 
from DSSinhVien
where MaSV not in (select distinct MaSV from KetQua)

delete from DSSinhVien
where MaSV not in (select distinct MaSV from KetQua)

-- 26. Xóa những môn mà không có sinh viên học.

select * from DMMonHoc
where MaMH not in (select distinct MaMH from KetQua);

delete from DMMonHoc
where MaMH not in (select distinct MaMH from KetQua);

-- 27. Thêm vào bảng khoa cột Siso, cập nhật sỉ số vào khoa từ dữ liệu sinh viên.

alter table DMKhoa
add Siso int

update DMKhoa  
set Siso = sv_count.SL  
from DMKhoa  
join (  
    select MaKhoa, count(MaSV) as SL  
    from DSSinhVien  
    group by MaKhoa  
) as sv_count  
on DMKhoa.MaKhoa = sv_count.MaKhoa;

-- 28. Tăng thêm 1 điểm cho các sinh viên vớt lần 2. Nhưng chỉ tăng tối đa là 5 điểm

update KetQua
set Diem = Diem + 1
where LanThi = 2 and Diem <= 5

-- 29. Tăng học bổng lên 100000 cho những sinh viên có điểm trung bình là 6.5 trở lên

update DSSinhVien
set HocBong = HocBong + 100000
where MaSV in (
	select MaSV
	from KetQua
	group by MaSV
	having avg(Diem) >= 6.5
)

-- 30. Thiết lập học bổng bằng 0 cho những sinh viên thi hai môn rớt ở lần 1

update DSSinhVien
set HocBong = 0
where MaSV in (
	select MaSV
	from KetQua
	where LanThi = 1 and TinhTrang = 0
	group by MaSV
	having count(MaMH) >= 2
)