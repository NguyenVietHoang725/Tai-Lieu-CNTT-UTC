use qlddh

-- Cau 2
create procedure sp1
	@Ngay datetime,
	@SLHoaDon int output
as begin
	set nocount on
	select @SLHoaDon = count(SoHoaDon)
	from DONDATHANG
	where convert(date, NgayDatHang) = convert(date, @Ngay)
	set @SLHoaDon = isnull(@SLHoaDon, 0)
end

declare @NgayTim datetime = '09/20/2016'
declare @SLHD int

exec sp1 @Ngay = @NgayTim, @SLHoaDon = @SLHD output

print N'Ngày ' + convert(nvarchar, convert(date, @NgayTim)) + N' có tất cả ' + cast(@SLHD as nvarchar) + N' hóa đơn.'

-- Cau 3
create function fn1( @MaNhanVien char(4) )
returns table as return
(
	select 
		year(ddh.NgayDatHang) as Nam,
		sum(ctdh.GiaBan * ctdh.SoLuong - ctdh.MucGiamGia) as DoanhThu
	from DONDATHANG ddh
	join CHITIETDATHANG ctdh on ddh.SoHoaDOn = ctdh.SoHoaDon
	where ddh.MaNhanVien = @MaNhanVien
	group by year(ddh.NgayDatHang)
)

select * from fn1('A001')

-- Cau 4
create view v4 as 
select 
	ncc.MaCongTy as 'Mã NCC',
	ncc.TenCongTy as 'Tên Nhà cung cấp',
	count(mh.MaHang) as 'Số mặt hàng cung cấp'
from NHACUNGCAP ncc
left join MATHANG mh on ncc.MaCongTy = mh.MaCongTy
group by ncc.MaCongTy, ncc.TenCongTy

select * from v4
-- Câu 5
alter table CHITIETDATHANG
add TenHang_LuuTru nvarchar(30);

alter table CHITIETDATHANG
add ThanhTien numeric(18, 2);

create trigger trg5 on CHITIETDATHANG
for insert, update as begin
	set nocount on

	update CHITIETDATHANG
	set 
		TenHang_LuuTru = (
			select TenHang
			from MATHANG
			where MaHang = CHITIETDATHANG.MaHang
		),
		ThanhTien = (
			select SoLuong * GiaBan - MucGiamGia
			from inserted
			where inserted.SoHoaDon = CHITIETDATHANG.SoHoaDon
				and inserted.MaHang = CHITIETDATHANG.MaHang
		)
	where exists (
		select 1
		from inserted
		where inserted.SoHoaDon = CHITIETDATHANG.SoHoaDon
			and inserted.MaHang = CHITIETDATHANG.MaHang
	)
end	

declare @SoHD int = 11;
declare @MaH char(4) = 'DC05';
declare @GiaBan numeric(10,2) = 3000;
declare @SoLuong int = 500;
declare @GiamGia numeric(10,2) = 0;

-- insert into CHITIETDATHANG (SoHoaDon, MaHang, GiaBan, SoLuong, MucGiamGia)
-- calues (@SoHD, @MaH, @GiaBan, @SoLuong, @GiamGia);

select
    SoHoaDon, MaHang, GiaBan, SoLuong, MucGiamGia,
    TenHang_LuuTru, ThanhTien
from CHITIETDATHANG
where SoHoaDon = @SoHD AND MaHang = @MaH;

-- Cau 6
create login NguyenVietHoang with password = '123'
create user NguyenVietHoang for login NguyenVietHoang

grant select on v4 to NguyenVietHoang
-- Cau 7
select 
	mh.TenHang as 'Tên hàng',
	ct.TongSoLuong as 'Tổng số lượng' 
from MATHANG mh
join (
	select MaHang, sum(SoLuong) as TongSoLuong
	from CHITIETDATHANG
	group by MaHang
) as ct on mh.MaHang = ct.MaHang
where ct.TongSoLuong = (
	select max(Tong)
	from (
		select sum(SoLuong) as Tong
		from CHITIETDATHANG
		group by MaHang
	) as MaxTieuThu
)

