use QLGiaiBongDa
-- Câu 2:
drop procedure sp2
create procedure sp2 
	@MaCLB char(20),
	@SoBanThangCLB int output
as begin
	set nocount on
	select @SoBanThangCLB = count(*)
	from TRANDAU_GHIBAN gb
	where gb.CauLacBoID = @MaCLB and (gb.VaoLuoiNha = 0 or gb.VaoLuoiNha is null)

	set @SoBanThangCLB = isnull(@SoBanThangCLB, 0)
end

declare @SL_BanThang int
declare @Ma_CLB char(20) = '101'

exec sp2
	@MaCLB = @Ma_CLB,
	@SoBanThangCLB = @SL_BanThang output

print N'Câu lạc bộ có mã ' + @Ma_CLB + N' đã ghi được: ' + cast(@SL_BanThang as nvarchar(10)) + N' bàn thắng.'

-- Câu 3:
create function fn2
(
	@TenCLB nvarchar(80)
)
returns table
as return 
(
	select 
		CauThuID,
		HoVaTen,
		ViTri,
		QuocTich,
		SoAo
	from CAUTHU ct
	join CAULACBO clb on ct.CauLacBoID = clb.CauLacBoID
	where @TenCLB = TenCLB
)

select * from fn2(N'Manchester United')

-- Câu 4: 
alter table CAUTHU
add SoBanGhi int default 0

create trigger tg4 on TRANDAU_GHIBAN
after insert, update, delete
as begin
	declare @Cauthu table (
		CauThuID char(20) PRIMARY KEY
	)

	insert into @Cauthu (CauThuID)
	select CauThuID from inserted
	union 
	select CauThuID from deleted

	update ct
	set SoBanGhi = (
		select count(*)
		from TRANDAU_GHIBAN gb
		where gb.CauThuID = ct.CauThuID
		and (gb.VaoLuoiNha = 0 or gb.VaoLuoiNha is null)
	)
	from CAUTHU ct
	join @Cauthu cauthu on ct.CauThuID = cauthu.CauThuID
end

select SoBanGhi from CAUTHU where CauThuID = N'1030'

update TRANDAU_GHIBAN
set ThoiDiemGhiBan = 12
where GhiBanID = '122'

-- Câu 5:
create view v5 as
select
	td.NgayThiDau as 'Ngày thi đấu',
	CLBNha.TenCLB as 'Tên CLB Nhà',
	CLBKhach.TenCLB as 'Tên CLB Khách',
	svd.TenSan as 'Tên sân'
from TRANDAU td
join CAULACBO CLBNha on td.CLBNha = CLBNha.CauLacBoID
join CAULACBO CLBKhach on td.CLBKhach = CLBKhach.CauLacBoID
join SANVANDONG svd on td.SanVanDongID = svd.SanVanDongID
where (year(td.NgayThiDau) = 2013) and (CLBNha.TenCLB = N'Manchester United' or CLBKhach.TenCLB = N'Manchester United')

select * from v5

-- Câu 6:
create login NguyenVietHoang with password = '123'
create user NguyenVietHoang for login NguyenVietHoang

grant select on CAULACBO to NguyenVietHoang

-- Câu 7:
drop function fn7
create function fn7()
returns @DanhSachTrongTai table (
	TrongTaiID char(20),
	HoVaTen nvarchar(80)
)
as begin
	declare @TranDauLienQuan table (
		TranDauID char(20) primary key
	)

	insert into @TranDauLienQuan (TranDauID)
	select distinct td.TranDauID
	from TRANDAU td
	where td.CLBNha = '104' or td.CLBKhach = '104'

	insert into @DanhSachTrongTai (TrongTaiID, HoVaTen)
	select distinct
		tt.TrongTaiID,
		tt.HoVaTen
	from TRONGTAI_TRANDAU tdtd
	join TRONGTAI tt on tdtd.TrongTaiID = tt.TrongTaiID
	join @TranDauLienQuan tdau on tdtd.TranDauID = tdau.TranDauID 
	
	return
end

select * from fn7()

select * from TRANDAU where CLBKhach = '104' or CLBNha = '104'