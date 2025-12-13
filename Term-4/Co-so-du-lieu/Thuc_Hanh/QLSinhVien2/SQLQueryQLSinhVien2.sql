use QLSinhVien2

-- 1. Cho biết những sinh viên thi lại trên 2 lần.
SELECT 
	DS.MaSV as "MSV",
	HoSV + ' ' + TenSV as "Họ và tên"
FROM DSSinhVien DS
JOIN KetQua KQ on DS.MaSV = KQ.MaSV 
WHERE LanThi >= 2

-- 2. Cho biết những sinh viên nam có điểm trung bình lần 1 trên 7.0
SELECT 
	DS.MaSV AS "MSV",
	HoSV + ' ' + TenSV AS "Họ và tên",
	AVG(CASE WHEN LanThi = 1 THEN Diem END) AS "Điểm TB"
FROM DSSinhVien DS
JOIN KetQua KQ ON DS.MaSV = KQ.MaSV 
WHERE KQ.Diem IS NOT NULL
GROUP BY DS.MaSV, HoSV, TenSV
HAVING AVG(CASE WHEN LanThi = 1 THEN Diem END) > 7

-- 3. Cho biết danh sách các sinh viên rớt trên 2 môn ở lần thi 1.
SELECT 
	DS.MaSV AS "MSV",
	HoSV + ' ' + TenSV AS "Họ và tên"
FROM DSSinhVien DS
JOIN KetQua KQ ON DS.MaSV = KQ.MaSV 
WHERE KQ.Diem IS NOT NULL
GROUP BY DS.MaSV, HoSV, TenSV
HAVING COUNT(CASE WHEN LanThi = 1 AND Diem < 4 THEN 1 END) >= 2

-- 4. Cho biết danh sách những khoa có nhiều hơn 2 sinh viên nam
SELECT 
	TenKhoa AS "Tên Khoa"
FROM DMKhoa K
JOIN DSSinhVien SV ON K.MaKhoa = SV.MaKhoa
GROUP BY TenKhoa
HAVING COUNT(CASE WHEN Phai = 'Nam' THEN 1 END) >= 2

-- 5. Cho biết những khoa có 2 sinh đạt học bổng từ 200.000 đến 300.000.
SELECT
	TenKhoa
FROM DMKhoa K
JOIN DSSinhVien SV ON K.MaKhoa = SV.MaKhoa
GROUP BY TenKhoa
HAVING COUNT(CASE WHEN HocBong BETWEEN 200000 AND 300000 THEN 1 END) >= 2

-- 6. Cho biết số lượng sinh viên đậu và số lượng sinh viên rớt của từng môn trong lần thi 1.
SELECT
	TenMH AS "Tên Môn học",
	COUNT(CASE WHEN LanThi = 1 AND Diem >= 4 THEN 1 END) AS "QUAMON",
	COUNT(CASE WHEN LanThi = 1 AND Diem < 4 THEN 1 END) AS "HOCLAI"
FROM DMMonHoc MH
JOIN KetQua KQ ON KQ.MaMH = MH.MaMH
GROUP BY TenMH

-- 7. Cho biết sinh viên nào có học bổng cao nhất.
SELECT 
	MaSV as "MSV",
	HoSV + ' ' + TenSV as "Họ và tên",
	HocBong as "Học bổng"
FROM DSSinhVien
WHERE HocBong = (SELECT MAX(HocBong) FROM DSSinhVien)

-- 8. Cho biết sinh viên nào có điểm thi lần 1 môn cơ sở dữ liệu cao nhất.
SELECT 
	DS.MaSV AS "MSV",
	HoSV + ' ' + TenSV AS "Họ và tên",
	Diem AS "Điểm"
FROM DSSinhVien DS
JOIN KetQua KQ ON DS.MaSV = KQ.MaSV
WHERE Diem = (
	SELECT MAX(Diem)
	FROM KetQua KQ
	JOIN DMMonHoc MH ON KQ.MaMH = MH.MaMH
	WHERE TenMH = N'Cơ sở dữ liệu' AND LanThi = 1
)

-- 9. Cho biết sinh viên khoa anh văn có tuổi lớn nhất.
SELECT
	DS.MaSV AS "MSV",
	HoSV + ' ' + TenSV AS "Họ và tên",
	DATEDIFF(YEAR, NgaySinh, GETDATE()) AS "Tuổi"
FROM DSSinhVien DS
JOIN DMKhoa K ON DS.MaKhoa = K.MaKhoa
WHERE DATEDIFF(YEAR, NgaySinh, GETDATE()) = (
	SELECT MAX(DATEDIFF(YEAR, NgaySinh, GETDATE()))
	FROM DSSinhVien
)

-- 10. Cho biết khoa nào có đông sinh viên nhất.
SELECT
    TenKhoa AS "Khoa",
    COUNT(MaSV) AS "Tổng SV"
FROM DMKhoa K
JOIN DSSinhVien DS ON K.MaKhoa = DS.MaKhoa
GROUP BY TenKhoa
HAVING COUNT(MaSV) = (
    SELECT MAX(SL)
    FROM (
        SELECT COUNT(MaSV) AS SL
        FROM DSSinhVien
        GROUP BY MaKhoa
    ) AS SubQuery
)

-- 11. Cho biết khoa nào có đông nữ nhất.
select 
	TenKhoa as "Tên khoa",
	count(MaSV) as "Tổng SV nữ"
from DMKhoa K
join DSSinhVien SV on K.maKhoa = SV.maKhoa
where Phai = N'Nữ'
group by TenKhoa
having count(MaSV) = (
	select max(SL)
	from (
		select count(MaSV) as SL
		from DSSinhVien
		where Phai = N'Nữ'
		group by MaKhoa
	) as Subquery
)

-- 12. Cho biết môn nào có nhiều sinh viên rớt lần 1 nhiều nhất.
select top 1 with ties
	TenMH as "Tên môn học",
	count(KQ.MaSV) as "Tổng SL"
from DMMonHoc MH
join KetQua KQ on MH.MaMH = KQ.MaMH
where KQ.LanThi = 1 and KQ.Diem < 5
group by MH.TenMH
order by count(KQ.MaSV) desc

-- 13. Cho biết sinh viên không học khoa anh văn có điểm thi môn phạm lớn hơn điểm thi văn phạm của sinh viên học khoa anh văn.
select 
	HoSV + ' ' + TenSV,
	Diem
from DSSinhVien SV
join KetQua KQ on SV.MaSV = KQ.MaSV
join DMMonHoc MH on KQ.MaMH = MH.MaMH 
join DMKhoa K on SV.MaKhoa = K.MaKhoa
where TenKhoa != N'Anh Văn' and TenMH = N'Văn Phạm' and Diem > (
	select max(Diem)
	from DSSinhVien SV2
	join KetQua KQ2 on SV2.MaSV = KQ2.MaSV
	join DMMonHoc MH2 on KQ2.MaMH = MH2.MaMH 
	join DMKhoa K2 on SV2.MaKhoa = K2.MaKhoa
	where K2.TenKhoa = N'Anh Văn' and MH2.TenMH = N'Văn Phạm'
)

-- 14. Cho biết sinh viên có nơi sinh cùng với Hải.
select NoiSinh
from DSSinhVien
where TenSV like N'%Hải'

select
	HoSV + ' ' + TenSV,
	NoiSinh
from DSSinhVien
where TenSV not like N'%Hải' and NoiSinh = (
	select NoiSinh
	from DSSinhVien
	where TenSV like N'%Hải'
)

-- 15. Cho biết những sinh viên nào có học bổng lớn hơn tất cả học bổng của sinh viên thuộc khoa anh văn
select 
	HoSV + ' ' + TenSV as "Họ và tên",
	HocBong as "Học bổng"
from DSSinhVien
where HocBong > (
	select MAX(HocBong)
	from DSSinhVien SV
	join DMKhoa K on SV.MaKhoa = K.MaKhoa
	where TenKhoa = N'Anh Văn'
)

-- 16. Cho biết những sinh viên có học bổng lớn hơn bất kỳ học bổng của sinh viên học khóa anh văn
select 
	HoSV + ' ' + TenSV as "Họ và tên",
	HocBong as "Học bổng"
from DSSinhVien
where HocBong > (
	select MIN(HocBong)
	from DSSinhVien SV
	join DMKhoa K on SV.MaKhoa = K.MaKhoa
	where TenKhoa = N'Anh Văn'
)

-- 17. Cho biết sinh viên nào có điểm thi môn cơ sở dữ liệu lần 2 lớn hơn tất cả điểm thi lần 1 môn cơ sở dữ liệu của những sinh viên khác.
select 
	HoSV + ' ' + TenSV as "Họ và tên",
	Diem as "Điểm"
from DSSinhVien SV
join KetQua KQ on SV.MaSV = KQ.MaSV
join DMMonHoc MH on KQ.MaMH = MH.MaMH
where TenMH = N'Cơ sở dữ liệu' and LanThi = 2
and Diem >= (
	select MAX(Diem)
	from KetQua KQ
	join DMMonHoc MH on KQ.MaMH = MH.MaMH
	where TenMH = N'Cơ sở dữ liệu' and LanThi = 1
)

-- 18. Cho biết những sinh viên đạt điểm cao nhất trong từng môn.

-- 19. Cho biết những khoa không có sinh viên học.
select
	TenKhoa as "Tên khoa",
	COUNT(MaSV) as "Tổng SL"
from DMKhoa K
join DSSinhVien SV on K.MaKhoa = SV.MaKhoa
group by TenKhoa

select
	TenKhoa as "Tên khoa"
from DMKhoa K
left join DSSinhVien SV on K.MaKhoa = SV.MaKhoa
where MaSV is null

-- 20. Cho biết sinh viên chưa thi môn cơ sở dữ liệu.
select 
	SV.MaSV
from DSSinhVien SV
left join KetQua KQ on SV.MaSV = KQ.MaSV
left join DMMonHoc MH on KQ.MaMH = MH.MaMH and MH.TenMH = N'Cơ sở dữ liệu'
where KQ.MaMH is null

-- 21. Cho biết sinh viên nào không thi lần 1 mà có dự thi lần 2.
select distinct
	SV.MaSV as "Mã sinh viên",
	SV.TenSV as "Tên sinh viên"
from DSSinhVien SV
join KetQua KQ on SV.maSV = KQ.MaSV
where KQ.LanThi = 2
and SV.MaSV not in (
	select MaSV
	from KetQua
	where LanThi = 1
)

-- 22. Cho biết môn nào không có sinh viên khoa anh văn học.
select 
	MH.MaMH as "Mã môn học",
	MH.TenMH as "Tên môn học"
from DMMonHoc MH
where MH.MaMH not in (
	select distinct KQ.MaMH
	from KetQua KQ
	join DSSinhVien SV on KQ.MaSV = SV.MaSV
	join DMKhoa K on SV.MaKhoa = K.MaKhoa
	where K.TenKhoa = N'Anh Văn'
)

-- 23. Cho biết những sinh viên khoa anh văn chưa học môn văn phạm.
select distinct
	SV.MaSV as "Mã sinh viên",
	SV.TenSV as "Tên sinh viên"
from DSSinhVien SV
join KetQua KQ on SV.MaSV = KQ.MaSV
where KQ.MaMH not in (
	select MaMH
	from DMMonHoc
	where TenMH = N'Văn Phạm'
)

-- 24. Cho biết những sinh viên không rớt môn nào.
select distinct
	SV.MaSV as "Mã sinh viên",
	SV.TenSV as "Tên sinh viên"
from DSSinhVien SV
where not exists (
	select 1
	from KetQua KQ
	where KQ.MaSV = SV.MaSV
	group by KQ.MaMH
	having max(KQ.Diem) < 4
)

-- 25. Cho biết những sinh viên học khoa anh văn có học bổng và những sinh viên chưa bao giờ rớt.
SELECT DISTINCT 
    SV.MaSV AS "Mã sinh viên",
    SV.TenSV AS "Tên sinh viên"
FROM DSSinhVien SV
JOIN DMKhoa K ON SV.MaKhoa = K.MaKhoa
WHERE K.TenKhoa = N'Anh Văn' AND SV.HocBong IS NOT NULL

UNION

SELECT DISTINCT 
    SV.MaSV AS "Mã sinh viên",
    SV.TenSV AS "Tên sinh viên"
FROM DSSinhVien SV
WHERE NOT EXISTS (
    SELECT 1
    FROM KetQua KQ
    WHERE KQ.MaSV = SV.MaSV
    GROUP BY KQ.MaMH
    HAVING MAX(KQ.Diem) < 4
)

-- 26. Cho biết khoa nào có đông sinh viên nhận học bổng nhất và khoa nào khoa nào có ít sinh viên nhận học bổng nhất.
WITH SoLuongHocBongTheoKhoa AS (
    SELECT 
        K.MaKhoa,
        K.TenKhoa,
        COUNT(*) AS SoSinhVienHocBong
    FROM DSSinhVien SV
    JOIN DMKhoa K ON SV.MaKhoa = K.MaKhoa
    WHERE SV.HocBong IS NOT NULL
    GROUP BY K.MaKhoa, K.TenKhoa
)

-- Lấy khoa có nhiều và ít sinh viên học bổng nhất
SELECT *
FROM SoLuongHocBongTheoKhoa
WHERE SoSinhVienHocBong = (
    SELECT MAX(SoSinhVienHocBong) FROM SoLuongHocBongTheoKhoa
)
OR SoSinhVienHocBong = (
    SELECT MIN(SoSinhVienHocBong) FROM SoLuongHocBongTheoKhoa
);

-- 27. Cho biết 3 sinh viên có học nhiều môn nhất.
-- 28. Cho biết những môn được tất cả các sinh viên theo học.
-- 29. Cho biết những sinh viên học những môn giống sinh viên có mã số A02 học.
-- 30. Cho biết những sinh viên học những môn bằng đúng những môn mà sinh viên A02 học.
-- 31. Tạo một bảng mới tên sinhvien-ketqua: gồm: MASV, HoSV, TenSV, SoMonHoc. Sau đó Thêm dữ liệu vào bảng này dựa vào dữ liệu đã có.
-- 32. Thêm vào bảng khoa cột Siso, cập nhật sỉ số vào khoa từ dữ liệu sinh viên.
-- 33. Tăng thêm 1 điểm cho các sinh viên vớt lần 2. Nhưng chỉ tăng tối đa là 5 điểm
-- 34. Tăng học bổng lên 100000 cho những sinh viên có điểm trung bình là 6.5 trở lên
-- 35. Thiết lập học bổng bằng 0 cho những sinh viên thi hai môn rớt ở lần 1
-- 36. Xoá tất cả những sinh viên chưa dự thi môn nào.
-- 37. Xóa những môn mà không có sinh viên học.
-- 38. Danh sách sinh viên không bi rớt môn nào
-- 39. Danh sách sinh viên học môn văn phạm và môn cơ sở dữ liệu
-- 40. Trong mỗi sinh viên cho biết môn có điểm thi lớn nhất. Thông tin gồm: mã sinh viên, tên sinh viên, tên môn, điểm.
-- 41. Danh sách sinh viên: Không rớt lần 1 hoặc ,Không học môn văn phạm
-- 42. Danh sách những sinh viên khoa có 2 sinh viên nữ trở lên
-- 43. Cho biết những nơi nào có hơn 2 sinh viên đang theo học tại trường.
-- 44. Cho biết những môn nào có trên 3 sinh viên dự thi.