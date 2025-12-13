use qlks

-- Câu 2
create procedure sp2
	@MaKH nvarchar(10),
	@NgayDen date,
	@SLPhong int output
as
begin
	set nocount on;
	select @SLPhong = isnull(sum(ct.SLPhong), 0)
	from PHIEUDAT pd
	inner join CHITIETPHONGDAT ct on pd.MaBooking = ct.MaBooking
	where pd.MaKH = @MaKH and cast(pd.NgayDenDuKien as date) = @NgayDen
end

declare @SL int

exec sp2 @MaKH = N'KH0001', @NgayDen = '2022-01-09', @SLPhong = @SL output
print N'Số lượng phòng thuê: ' + cast(@SL as nvarchar(10))

-- Câu 3
create function fn3
(
	@MaBooking nvarchar(10)
)
returns table
as return 
(
	select 
		pt.MaBooking as 'Mã Booking',
		MaKH as 'Mã khách hàng',
		Kieuphong as 'Kiểu phòng',
		p.Maphong as 'Mã phòng',
		Thoigiancheckin as 'Thời gian check in',
		Thoigiancheckout as 'Thời gian check out'
	from PHIEUTHUE pt
	join PHIEUDAT pd on pt.MaBooking = pd.MaBooking
	join Phong p on pt.Maphong = p.Maphong
	join LOAIPHONG lp on p.MaLP = lp.MaLP
	where pt.MaBooking = @MaBooking
)

SELECT * FROM fn3(N'PD0001');

-- câu 4
alter table PHIEUDAT
add SoLuongPhongThue int default 0

create trigger trg4 on PHIEUTHUE
after insert, update, delete
as
begin
	set nocount on

	update PHIEUDAT
	set SoLuongPhongThue = (
		select count(*)
		from PHIEUTHUE pt
		where pt.MaBooking = PHIEUDAT.MaBooking
	)
	where PHIEUDAT.MaBooking in (
		select MaBooking from inserted
		union
		select MaBooking from deleted
	)
end

update PHIEUTHUE
set KMPhong = 0.15, Thoigiancheckout = '2022-01-10 00:00:00.000'
where MaPT = 'PT0001'

select MaBooking, SoLuongPhongThue
from PHIEUDAT
where MaBooking = 'PD0001'

-- Câu 5
create view v5 as
select
	pd.MaBooking,
	Tiendatcoc,
	p.Maphong,
	Kieuphong,
	NgayDenDukien,
	Thoigiancheckin,
	Thoigiancheckout
from PHIEUDAT pd
join CHITIETPHONGDAT ct on pd.MaBooking = ct.MaBooking
join LOAIPHONG lp on ct.MaLP = lp.MaLP
join PHONG p on lp.MaLP = p.MaLP
join PHIEUTHUE pt on pd.MaBooking = pt.MaBooking
where NgayDenDukien between '2022-12-12' and '2022-12-19'

select * from v5

-- Câu 6
create login NgoTrungKien with password = '123'
create user NgoTrungKien for login NgoTrungKien

grant select, insert, update on NHANVIEN to NgoTrungKien with grant option

create login NguyenXuanNgoc with password = '123'
create user NguyenXuanNgoc for login NguyenXuanNgoc

grant select, update on NHANVIEN to NguyenXuanNgoc;

DROP USER IF EXISTS NguyenXuanNgoc;
DROP LOGIN NguyenXuanNgoc;

select * from NHANVIEN

insert into NHANVIEN (MaNV, TenNV, SoCCCD, SDT, NgaySinh, Gioitinh, ChucVu)
values (N'NV006', N'Nguyễn Văn A', N'012345678901', N'0912345678', null, 1, null);

delete from NHANVIEN
where MaNV = N'NV006'

-- Câu 7
drop view v7
create view v7 as
select top 3
	kh.MaKH,
	kh.TenKH,
	sum(
		case 
			when pd.NgayDenDuKien <> pd.NgayDiDuKien
				then datediff(day, pd.NgayDenDuKien, pd.NgayDiDuKien)
			else 1
		end
	) as TongSoNgayDatStandard
from KHACHHANG kh
join PHIEUDAT pd on kh.MaKH = pd.MaKH
join CHITIETPHONGDAT ct on pd.MaBooking = ct.MaBooking
join LOAIPHONG lp on ct.MaLP = lp.MaLP
where lp.MaLP like N'Standard%'
group by kh.MaKH, kh.TenKH
order by TongSoNgayDatStandard desc

select * from v7