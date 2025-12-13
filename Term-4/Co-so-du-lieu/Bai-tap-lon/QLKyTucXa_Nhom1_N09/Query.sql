--- 02 lệnh gồm select có điều kiện trên một bảng

--Liệt kê các sinh viên có giới tính nữ
select *
from tSinhVien
where GioiTinh = '0'

--Liệt kê nhân viên có chức vụ là quản lí
select *
from tNhanVien
where ChucVu like N'Quản lý%';


--- 03 lệnh gồm select có điều kiện trên hai bảng

--Thông tin phiếu thuê phòng kèm tên sinh viên đã hoàn thành đóng tiền
select HoTen, tPTTThuePhong.*
from tSinhVien
inner join tPTTThuePhong on tSinhVien.MaSV = tPTTThuePhong.MASV
where TrangThai = 1;

--Thông tin hợp đồng và tên phòng kết thúc vào tháng này
select TenPhong, tHopDong.*
from tPhong
inner join tHopDong on tPhong.MaPhong = tHopDong.MaPhong
where YEAR(NgayKetThuc) = YEAR(GETDATE()) and MONTH(NgayKetThuc) = MONTH(GETDATE());


--Thông tin phiếu thu điện nước chưa đóng tiền, kèm tên nhân viên thu điện nước
select HoTen, tPTTDienNuoc.*
from tPTTDienNuoc
inner join tNhanVien on tNhanVien.MaNV = tPTTDienNuoc.MaNV
where TrangThai = 0;

--- 02 câu lệnh gồm select có where và group by
--Tổng tiền theo từng phòng tổng tiền phiếu thu tiền phòng đã được thanh toán
select MaPhong, SUM(TongTien) as DaThanhToan
from tPTTDienNuoc
where TrangThai = 1
group by MaPhong

--Số lượng phòng từng loại phòng của nữ
select TenLoaiPhong, count(MaPhong) as SL
from tLoaiPhong
inner join tPhong on tLoaiPhong.MaLoaiPhong = tPhong.MaLoaiPhong
where Nam_Nu = 0
group by TenLoaiPhong

--- 03 câu lệnh gồm select có where, group by, having

--Tổng tiền theo từng phòng tổng tiền phiếu thu tiền phòng chưa được thanh toán nhỏ hơn 1 triệu
select MaPhong, SUM(TongTien) as ChuaThanhToan
from tPTTDienNuoc
where TrangThai = 0
group by MaPhong
having SUM(TongTien) < 1000000

--Loại phòng của nam mà có trên 3 phòng
select TenLoaiPhong, count(MaPhong) as SL
from tLoaiPhong
inner join tPhong on tLoaiPhong.MaLoaiPhong = tPhong.MaLoaiPhong
where Nam_Nu = 1
group by TenLoaiPhong
having count(MaPhong) > 3

--Liệt kê những phòng nữ chưa đủ người
select tPhong.MaPhong,	COUNT(MaSV) as SLHienTai, SoNguoi 
from tHopDong
right join tPhong on tHopDong.MaPhong = tPhong.MaPhong
right join tLoaiPhong on tPhong.MaLoaiPhong = tLoaiPhong.MaLoaiPhong
where Nam_Nu = 0
group by tPhong.MaPhong, SoNguoi
having COUNT(MaSV) < SoNguoi

--- 02 câu lệnh gồm select có where, group by, having và order by
--thứ tự tiền điện nước chưa thanh toán, lớn hơn 100000, giảm dần của các phòng
select MaPhong, SUM(TongTien) as ChuaThanhToan
from tPTTDienNuoc
where TrangThai = 0
group by MaPhong
having SUM(TongTien) > 100000
order by ChuaThanhToan desc

--tìm những loại phòng có hơn 1 phòng (chứa được ít nhất 4 người), sắp xếp tăng dần theo số lượng phòng
select TenLoaiPhong, count(MaPhong) as SL
from tLoaiPhong
inner join tPhong on tLoaiPhong.MaLoaiPhong = tPhong.MaLoaiPhong
where SoNguoi >= 4
group by TenLoaiPhong
having count(MaPhong) > 1
order by SL

--- 02 câu lệnh gồm select có where, group by, having và truy vấn con
--tính tổng tiền đã thanh toán của phòng có đủ số lượng sinh viên trong phòng

select A.MaPhong, A.DaThanhToan
from
(select MaPhong, SUM(TongTien) as DaThanhToan
from tPTTDienNuoc
where TrangThai = 1
group by MaPhong) A
inner join
(select tPhong.MaPhong, COUNT(MaSV) as SL, SoNguoi
from tHopDong
inner join tPhong on tHopDong.MaPhong = tPhong.MaPhong
inner join tLoaiPhong on tPhong.MaLoaiPhong = tLoaiPhong.MaLoaiPhong
group by tPhong.MaPhong, SoNguoi
having COUNT(MaSV) = SoNguoi) B on A.MaPhong = B.MaPhong


--Loại phòng nam, chưa đủ người, tăng dần
select TenLoaiPhong, count(MaPhong) as SL
from tLoaiPhong
inner join tPhong on tLoaiPhong.MaLoaiPhong = tPhong.MaLoaiPhong
where Nam_Nu = 1 
and exists (select tPhong.MaPhong,	COUNT(MaSV) as SLHienTai, SoNguoi 
from tHopDong
right join tPhong on tHopDong.MaPhong = tPhong.MaPhong
right join tLoaiPhong on tPhong.MaLoaiPhong = tLoaiPhong.MaLoaiPhong
where Nam_Nu = 1
group by tPhong.MaPhong, SoNguoi
having COUNT(MaSV) < SoNguoi) 
group by TenLoaiPhong
order by SL

--- 02 câu câu lệnh insert có điều kiện

--thêm sinh viên nếu chưa có
insert into tSinhVien(MaSV, HoTen, NgaySinh, GioiTinh, SDT)
select '231230884', N'Nguyễn Văn A', '09-09-2002', 1, '0126546478'
where not exists(
select 1
from tSinhVien sv
where sv.MaSV = '231230884')

--thêm những phiếu báo điện nước vào phiếu thu nếu chưa có phiếu thu
insert into tPTTDienNuoc(MaPT, NgayLapPhieu, HanThanhToan, TongTien, TrangThai)
select pb.MaPB, pb.NgayLapPhieu, pb.HanThanhToan, pb.TongTien, pb.TrangThai
from tPBTDienNuoc pb
where not exists(
select 1
from tPTTDienNuoc pt
where pb.MaPB = pt.MaPT)

--sinh viên nữ chưa có hợp đồng
select HoTen
from tSinhVien
where GioiTinh = 0 and not exists (
select 1
from tHopDong
where tHopDong.MaSV = tSinhVien.MaSV
)
--- 02 câu lệnh update có điều kiện --thêm thông tin cho phòng mới

--Cập nhật trạng thái phiếu thu tiền phòng đã thanh toán
update tPTTThuePhong
set TrangThai = 1
where NgayLapPhieu <= GETDATE()

--cập nhật chức vụ nhân viên cho những nhân viên chưa có chức vụ
update tNhanVien
set ChucVu = N'Nhân viên'
where ChucVu is null

--- 02 câu lệnh delete có điều kiện
--Xóa sinh viên không có hợp đồng nào
delete from tSinhVien
where not exists(
select 1
from tHopDong
where tHopDong.MaSV = tSinhVien.MaSV)

--Xóa phiếu thu điện nước chưa thanh toán và đã quá hạn
delete from tPTTDienNuoc
where TrangThai = 0 and HanThanhToan < GETDATE();