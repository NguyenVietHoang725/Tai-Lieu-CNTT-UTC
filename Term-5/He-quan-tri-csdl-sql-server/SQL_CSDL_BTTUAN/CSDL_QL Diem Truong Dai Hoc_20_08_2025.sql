--CREATE DATABASE QuanLyDiemTruongDaiHoc
--USE QuanLyDiemTruongDaiHoc
GO
/****** Object:  Table [dbo].[BoMon]    Script Date: 20/08/2025 8:10:24 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BoMon](
	[MaBoMon] [varchar](10) NOT NULL,
	[TenBoMon] [nvarchar](200) NULL,
	[TenTiengAnh] [varchar](200) NULL,
	[MaKhoa] [varchar](10) NULL,
	[TruongBoMon] [varchar](10) NULL,
	[DiaChi] [nvarchar](50) NULL,
	[DienThoai] [varchar](15) NULL,
	[Email] [varchar](50) NULL,
 CONSTRAINT [pk_BoMon] PRIMARY KEY CLUSTERED 
(
	[MaBoMon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChuongTrinhDaoTao]    Script Date: 20/08/2025 8:10:24 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChuongTrinhDaoTao](
	[MaCTDT] [varchar](25) NOT NULL,
	[TenCTDT] [nvarchar](200) NULL,
	[TenCTDTTiengAnh] [varchar](200) NULL,
	[MaKhoa] [varchar](10) NULL,
	[NganhApDung] [nvarchar](200) NULL,
	[KhoaHocApDung] [varchar](10) NULL,
 CONSTRAINT [pk_ChuongTrinhDaoTao] PRIMARY KEY CLUSTERED 
(
	[MaCTDT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CTDT_HocPhan]    Script Date: 20/08/2025 8:10:24 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTDT_HocPhan](
	[MaCTDT] [varchar](25) NOT NULL,
	[MaHocPhan] [varchar](25) NOT NULL,
	[KyHoc] [int] NULL,
 CONSTRAINT [pk_CTDT_HocPhan] PRIMARY KEY CLUSTERED 
(
	[MaCTDT] ASC,
	[MaHocPhan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GiangVien]    Script Date: 20/08/2025 8:10:24 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiangVien](
	[MaGiangVien] [varchar](10) NOT NULL,
	[HoDem] [nvarchar](35) NULL,
	[Ten] [nvarchar](35) NULL,
	[MaBoMon] [varchar](10) NULL,
	[NgaySinh] [date] NULL,
	[GioiTinh] [bit] NULL,
	[HocHam] [nvarchar](20) NULL,
	[HocVi] [nvarchar](20) NULL,
	[ChucDanh] [nvarchar](20) NULL,
	[DienThoai] [varchar](15) NULL,
	[Email] [varchar](50) NULL,
 CONSTRAINT [pk_GiangVien] PRIMARY KEY CLUSTERED 
(
	[MaGiangVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HocPhan]    Script Date: 20/08/2025 8:10:24 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HocPhan](
	[MaHocPhan] [varchar](25) NOT NULL,
	[TenHocPhan] [nvarchar](200) NULL,
	[TenTiengAnh] [nvarchar](200) NULL,
	[MaBoMon] [varchar](10) NULL,
	[SoTinChi] [int] NULL,
	[TrongSoDiemQuaTrinh] [float] NULL,
	[TrongSoDiemThiKTHP] [float] NULL,
 CONSTRAINT [pk_HocPhan] PRIMARY KEY CLUSTERED 
(
	[MaHocPhan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HocPhanTienQuyet]    Script Date: 20/08/2025 8:10:24 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HocPhanTienQuyet](
	[MaHocPhan] [varchar](25) NOT NULL,
	[MaHocPhanTienQuyet] [varchar](25) NOT NULL,
	[RangBuoc] [bit] NULL,
 CONSTRAINT [pk_HocPhanTienQuyet] PRIMARY KEY CLUSTERED 
(
	[MaHocPhan] ASC,
	[MaHocPhanTienQuyet] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Khoa]    Script Date: 20/08/2025 8:10:24 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Khoa](
	[MaKhoa] [varchar](10) NOT NULL,
	[TenKhoa] [nvarchar](200) NULL,
	[TenTiengAnh] [nvarchar](200) NULL,
	[DiaChi] [nvarchar](50) NULL,
	[DienThoai] [varchar](15) NULL,
	[Email] [varchar](50) NULL,
	[Website] [ntext] NULL,
 CONSTRAINT [pk_Khoa] PRIMARY KEY CLUSTERED 
(
	[MaKhoa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lop]    Script Date: 20/08/2025 8:10:24 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lop](
	[MaLop] [varchar](25) NOT NULL,
	[TenLop] [nvarchar](200) NULL,
	[MaKhoa] [varchar](10) NULL,
	[KhoaHoc] [varchar](10) NULL,
 CONSTRAINT [pk_Lop] PRIMARY KEY CLUSTERED 
(
	[MaLop] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LopHocPhan]    Script Date: 20/08/2025 8:10:24 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LopHocPhan](
	[MaHocPhan] [varchar](25) NOT NULL,
	[MaLopHocPhan] [varchar](25) NOT NULL,
	[TenLopHocPhan] [nvarchar](200) NULL,
	[MaGiangVien] [varchar](10) NULL,
	[NamHoc] [varchar](20) NULL,
	[HocKy] [int] NULL,
	[DotHoc] [int] NULL,
 CONSTRAINT [pk_LopHocPhan] PRIMARY KEY CLUSTERED 
(
	[MaHocPhan] ASC,
	[MaLopHocPhan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LopHocPhan_SinhVien]    Script Date: 20/08/2025 8:10:24 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LopHocPhan_SinhVien](
	[MaHocPhan] [varchar](25) NOT NULL,
	[MaLopHocPhan] [varchar](25) NOT NULL,
	[MaSinhVien] [varchar](10) NOT NULL,
	[DiemQuaTrinh] [float] NULL,
	[DiemThiKTHP] [float] NULL,
	[DiemTKHP] [float] NULL,
	[DiemHe4] [float] NULL,
	[DiemHeChu] [varchar](10) NULL,
	[LanHoc] [int] NULL,
 CONSTRAINT [pk_LopHocPhan_SinhVien] PRIMARY KEY CLUSTERED 
(
	[MaHocPhan] ASC,
	[MaLopHocPhan] ASC,
	[MaSinhVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SinhVien]    Script Date: 20/08/2025 8:10:24 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SinhVien](
	[MaSinhVien] [varchar](10) NOT NULL,
	[HoDem] [nvarchar](35) NULL,
	[Ten] [nvarchar](35) NULL,
	[MaCTDT] [varchar](25) NULL,
	[MaLop] [varchar](25) NULL,
	[NgaySinh] [date] NULL,
	[GioiTinh] [bit] NULL,
	[DiaChi] [nvarchar](250) NULL,
	[DienThoai] [varchar](15) NULL,
	[Email] [varchar](50) NULL,
	[TinhTrang] [nvarchar](50) NULL,
 CONSTRAINT [pk_SinhVien] PRIMARY KEY CLUSTERED 
(
	[MaSinhVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[BoMon] ([MaBoMon], [TenBoMon], [TenTiengAnh], [MaKhoa], [TruongBoMon], [DiaChi], [DienThoai], [Email]) VALUES (N'CPM', N'CÔNG NGHỆ PHẦN MỀM', N'SOFTWARE TECHNOLOGY', N'CNTT', N'0172', N'310A9', N'0247899234', N'cpm@utc.edu.vn')
GO
INSERT [dbo].[BoMon] ([MaBoMon], [TenBoMon], [TenTiengAnh], [MaKhoa], [TruongBoMon], [DiaChi], [DienThoai], [Email]) VALUES (N'KHM', N'KHOA HỌC MÁY TÍNH', N'COMPUTER SCIENCE', N'CNTT', N'1680', N'309A9', N'0243344556', N'khm@utc.edu.vn')
GO
INSERT [dbo].[BoMon] ([MaBoMon], [TenBoMon], [TenTiengAnh], [MaKhoa], [TruongBoMon], [DiaChi], [DienThoai], [Email]) VALUES (N'MHT', N'MẠNG VÀ CÁC HỆ THỐNG THÔNG TIN', N'NETWORKS AND INFORMATION SYSTEMS', N'CNTT', N'1387', N'308A9', N'0248899889', N'mht@utc.edu.vn')
GO
INSERT [dbo].[ChuongTrinhDaoTao] ([MaCTDT], [TenCTDT], [TenCTDTTiengAnh], [MaKhoa], [NganhApDung], [KhoaHocApDung]) VALUES (N'7.48.01.01', N'KHOA HỌC MÁY TÍNH', NULL, NULL, NULL, N'63')
GO
INSERT [dbo].[ChuongTrinhDaoTao] ([MaCTDT], [TenCTDT], [TenCTDTTiengAnh], [MaKhoa], [NganhApDung], [KhoaHocApDung]) VALUES (N'7.48.02.01', N'CÔNG NGHỆ THÔNG TIN', N'INFORMATION TECHNOLOGY', N'CNTT', N'CNTT', N'tu khoa 61')
GO
INSERT [dbo].[ChuongTrinhDaoTao] ([MaCTDT], [TenCTDT], [TenCTDTTiengAnh], [MaKhoa], [NganhApDung], [KhoaHocApDung]) VALUES (N'7.48.02.01QT(IE3)', N'CHẤT LƯỢNG CAO CÔNG NGHỆ THÔNG TIN VIỆT ANH', N'HIGH QUALITY INFORMATION TECHNOLOGY', N'CNTT', N'CNTT', N'62')
GO
INSERT [dbo].[CTDT_HocPhan] ([MaCTDT], [MaHocPhan], [KyHoc]) VALUES (N'7.48.02.01', N'IT0.004.2', 1)
GO
INSERT [dbo].[CTDT_HocPhan] ([MaCTDT], [MaHocPhan], [KyHoc]) VALUES (N'7.48.02.01', N'IT1.106.3', 3)
GO
INSERT [dbo].[CTDT_HocPhan] ([MaCTDT], [MaHocPhan], [KyHoc]) VALUES (N'7.48.02.01', N'IT1.107.3', 3)
GO
INSERT [dbo].[CTDT_HocPhan] ([MaCTDT], [MaHocPhan], [KyHoc]) VALUES (N'7.48.02.01', N'IT1.110.3', 4)
GO
INSERT [dbo].[CTDT_HocPhan] ([MaCTDT], [MaHocPhan], [KyHoc]) VALUES (N'7.48.02.01', N'IT1.113.3', 3)
GO
INSERT [dbo].[CTDT_HocPhan] ([MaCTDT], [MaHocPhan], [KyHoc]) VALUES (N'7.48.02.01', N'IT1.217.3', 6)
GO
INSERT [dbo].[CTDT_HocPhan] ([MaCTDT], [MaHocPhan], [KyHoc]) VALUES (N'7.48.02.01', N'IT1.220.3', 7)
GO
INSERT [dbo].[CTDT_HocPhan] ([MaCTDT], [MaHocPhan], [KyHoc]) VALUES (N'7.48.02.01', N'IT1.223.2', 5)
GO
INSERT [dbo].[CTDT_HocPhan] ([MaCTDT], [MaHocPhan], [KyHoc]) VALUES (N'7.48.02.01QT(IE3)', N'IE3.002.3', 4)
GO
INSERT [dbo].[CTDT_HocPhan] ([MaCTDT], [MaHocPhan], [KyHoc]) VALUES (N'7.48.02.01QT(IE3)', N'IE3.003.3', 5)
GO
INSERT [dbo].[CTDT_HocPhan] ([MaCTDT], [MaHocPhan], [KyHoc]) VALUES (N'7.48.02.01QT(IE3)', N'IE3.004.3', 6)
GO
INSERT [dbo].[CTDT_HocPhan] ([MaCTDT], [MaHocPhan], [KyHoc]) VALUES (N'7.48.02.01QT(IE3)', N'IT1.110.3', 4)
GO
INSERT [dbo].[CTDT_HocPhan] ([MaCTDT], [MaHocPhan], [KyHoc]) VALUES (N'7.48.02.01QT(IE3)', N'IT1.115.3', 5)
GO
INSERT [dbo].[CTDT_HocPhan] ([MaCTDT], [MaHocPhan], [KyHoc]) VALUES (N'7.48.02.01QT(IE3)', N'IT1.223.2', 5)
GO
INSERT [dbo].[CTDT_HocPhan] ([MaCTDT], [MaHocPhan], [KyHoc]) VALUES (N'7.48.02.01QT(IE3)', N'IT1.227.2', 6)
GO
INSERT [dbo].[GiangVien] ([MaGiangVien], [HoDem], [Ten], [MaBoMon], [NgaySinh], [GioiTinh], [HocHam], [HocVi], [ChucDanh], [DienThoai], [Email]) VALUES (N'0172', N'Nguyễn Trọng', N'Phúc', N'CPM', CAST(N'1976-01-01' AS Date), 1, NULL, N'TS', N'GV', N'0888869999', N'NguyenTrongPhuc@utc.edu.vn')
GO
INSERT [dbo].[GiangVien] ([MaGiangVien], [HoDem], [Ten], [MaBoMon], [NgaySinh], [GioiTinh], [HocHam], [HocVi], [ChucDanh], [DienThoai], [Email]) VALUES (N'0175', N'Nguyễn Trần ', N'Hạnh', N'KHM', CAST(N'1979-05-05' AS Date), 1, NULL, N'ThS', N'GV', N'0967897777', N'NguyenTranHanh@utc.edu.vn')
GO
INSERT [dbo].[GiangVien] ([MaGiangVien], [HoDem], [Ten], [MaBoMon], [NgaySinh], [GioiTinh], [HocHam], [HocVi], [ChucDanh], [DienThoai], [Email]) VALUES (N'0849', N'Nguyễn Anh', N'Tâm', N'MHT', CAST(N'2001-03-03' AS Date), 0, NULL, N'KS', N'GV', N'0984000000', N'NguyenAnhTam@utc.edu.vn')
GO
INSERT [dbo].[GiangVien] ([MaGiangVien], [HoDem], [Ten], [MaBoMon], [NgaySinh], [GioiTinh], [HocHam], [HocVi], [ChucDanh], [DienThoai], [Email]) VALUES (N'0896', N'Bùi Ngọc', N'Quang', N'MHT', CAST(N'1977-02-02' AS Date), 1, NULL, N'TS', N'GVC', N'0919333999', N'BuiNgocQuang@utc.edu.vn')
GO
INSERT [dbo].[GiangVien] ([MaGiangVien], [HoDem], [Ten], [MaBoMon], [NgaySinh], [GioiTinh], [HocHam], [HocVi], [ChucDanh], [DienThoai], [Email]) VALUES (N'1207', N'Nguyễn Hữu', N'Thái', N'KHM', CAST(N'2000-07-07' AS Date), 1, NULL, N'KS', N'GV', N'0944998888', N'NguyenHuuThai@utc.edu.vn')
GO
INSERT [dbo].[GiangVien] ([MaGiangVien], [HoDem], [Ten], [MaBoMon], [NgaySinh], [GioiTinh], [HocHam], [HocVi], [ChucDanh], [DienThoai], [Email]) VALUES (N'1268', N'Lại Mạnh', N'Dũng', N'MHT', CAST(N'1981-01-01' AS Date), 1, NULL, N'TS', N'GV', N'0888888866', N'LaiManhDung@utc.edu.vn')
GO
INSERT [dbo].[GiangVien] ([MaGiangVien], [HoDem], [Ten], [MaBoMon], [NgaySinh], [GioiTinh], [HocHam], [HocVi], [ChucDanh], [DienThoai], [Email]) VALUES (N'1387', N'Nguyễn Quốc', N'Tuấn', N'CPM', CAST(N'1978-12-12' AS Date), 1, NULL, N'TS', N'GVC', N'0969969969', N'NguyenQuocTuan@utc.edu.vn')
GO
INSERT [dbo].[GiangVien] ([MaGiangVien], [HoDem], [Ten], [MaBoMon], [NgaySinh], [GioiTinh], [HocHam], [HocVi], [ChucDanh], [DienThoai], [Email]) VALUES (N'1406', N'Nguyễn Lê', N'Mạnh', N'KHM', CAST(N'1999-08-08' AS Date), 1, NULL, N'ThS', N'GV', N'0912777999', N'NguyenLeManh@utc.edu.vn')
GO
INSERT [dbo].[GiangVien] ([MaGiangVien], [HoDem], [Ten], [MaBoMon], [NgaySinh], [GioiTinh], [HocHam], [HocVi], [ChucDanh], [DienThoai], [Email]) VALUES (N'1614', N'Nguyễn Thị Hồng', N'Nhung', N'KHM', CAST(N'1998-06-06' AS Date), 0, NULL, N'ThS', N'GV', N'0983888888', N'NguyenThiHongNhung@utc.edu.vn')
GO
INSERT [dbo].[GiangVien] ([MaGiangVien], [HoDem], [Ten], [MaBoMon], [NgaySinh], [GioiTinh], [HocHam], [HocVi], [ChucDanh], [DienThoai], [Email]) VALUES (N'1673', N'Nguyễn Thanh ', N'Trà', N'CPM', CAST(N'1969-11-11' AS Date), 0, N'PGS', N'TS', N'GVCC', N'0839099999', N'NguyenThanhTra@utc.edu.vn')
GO
INSERT [dbo].[GiangVien] ([MaGiangVien], [HoDem], [Ten], [MaBoMon], [NgaySinh], [GioiTinh], [HocHam], [HocVi], [ChucDanh], [DienThoai], [Email]) VALUES (N'1674', N'Nguyễn Kim', N'Sao', N'CPM', CAST(N'1979-10-10' AS Date), 1, NULL, N'TS', N'GVC', N'0987988888', N'NguyenKimSao@utc.edu.vn')
GO
INSERT [dbo].[GiangVien] ([MaGiangVien], [HoDem], [Ten], [MaBoMon], [NgaySinh], [GioiTinh], [HocHam], [HocVi], [ChucDanh], [DienThoai], [Email]) VALUES (N'1679', N'Phạm Thanh', N'Hải', N'MHT', CAST(N'1971-04-04' AS Date), 0, N'PGS', N'TS', N'GVCC', N'0911222222', N'PhamThanhHai@utc.edu.vn')
GO
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [TenTiengAnh], [MaBoMon], [SoTinChi], [TrongSoDiemQuaTrinh], [TrongSoDiemThiKTHP]) VALUES (N'IE3.002.3', N'Công nghệ Java', N'Java Programming Language', N'CPM', 3, 0.5, 0.5)
GO
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [TenTiengAnh], [MaBoMon], [SoTinChi], [TrongSoDiemQuaTrinh], [TrongSoDiemThiKTHP]) VALUES (N'IE3.003.3', N'Lập trình web', N'Web Programming', N'MHT', 3, 0.5, 0.5)
GO
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [TenTiengAnh], [MaBoMon], [SoTinChi], [TrongSoDiemQuaTrinh], [TrongSoDiemThiKTHP]) VALUES (N'IE3.004.3', N'Lập trình thiết bị di động', N'Mobile Device Programming', N'MHT', 3, 0.5, 0.5)
GO
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [TenTiengAnh], [MaBoMon], [SoTinChi], [TrongSoDiemQuaTrinh], [TrongSoDiemThiKTHP]) VALUES (N'IT0.004.2', N'Tin học đại cương', N'Fundamental Informatics', N'CPM', 2, 0.4, 0.6)
GO
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [TenTiengAnh], [MaBoMon], [SoTinChi], [TrongSoDiemQuaTrinh], [TrongSoDiemThiKTHP]) VALUES (N'IT1.106.3', N'Thiết kế web', N'Front End Web Design', N'MHT', 3, 0.5, 0.5)
GO
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [TenTiengAnh], [MaBoMon], [SoTinChi], [TrongSoDiemQuaTrinh], [TrongSoDiemThiKTHP]) VALUES (N'IT1.107.3', N'Kiến trúc và tổ chức máy tính', N'Computer Organization and Architecture', N'MHT', 3, 0.4, 0.6)
GO
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [TenTiengAnh], [MaBoMon], [SoTinChi], [TrongSoDiemQuaTrinh], [TrongSoDiemThiKTHP]) VALUES (N'IT1.110.3', N'Cơ sở dữ liệu', N'Database', N'MHT', 3, 0.5, 0.5)
GO
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [TenTiengAnh], [MaBoMon], [SoTinChi], [TrongSoDiemQuaTrinh], [TrongSoDiemThiKTHP]) VALUES (N'IT1.113.3', N'Công nghệ Java', N'Java Programming Language', N'CPM', 3, 0.5, 0.5)
GO
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [TenTiengAnh], [MaBoMon], [SoTinChi], [TrongSoDiemQuaTrinh], [TrongSoDiemThiKTHP]) VALUES (N'IT1.115.3', N'Mạng máy tính', N'Computer Networking', N'MHT', 3, 0.4, 0.6)
GO
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [TenTiengAnh], [MaBoMon], [SoTinChi], [TrongSoDiemQuaTrinh], [TrongSoDiemThiKTHP]) VALUES (N'IT1.118.2', N'Thuật toán và ứng dụng', N'Algorithms and applications', N'KHM', 3, 0.4, 0.6)
GO
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [TenTiengAnh], [MaBoMon], [SoTinChi], [TrongSoDiemQuaTrinh], [TrongSoDiemThiKTHP]) VALUES (N'IT1.217.3', N'Lập trình Web', N'Web Programming  ', N'MHT', 3, 0.5, 0.5)
GO
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [TenTiengAnh], [MaBoMon], [SoTinChi], [TrongSoDiemQuaTrinh], [TrongSoDiemThiKTHP]) VALUES (N'IT1.220.3', N'Trí tuệ nhân tạo', N'Artificial Intelligence', N'MHT', 3, 0.5, 0.5)
GO
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [TenTiengAnh], [MaBoMon], [SoTinChi], [TrongSoDiemQuaTrinh], [TrongSoDiemThiKTHP]) VALUES (N'IT1.223.2', N'Hệ quản trị CSDL SQL Server', N'Microsoft SQL Server Management', N'KHM', 3, 0.4, 0.6)
GO
INSERT [dbo].[HocPhan] ([MaHocPhan], [TenHocPhan], [TenTiengAnh], [MaBoMon], [SoTinChi], [TrongSoDiemQuaTrinh], [TrongSoDiemThiKTHP]) VALUES (N'IT1.227.2', N'Hệ điều hành UNIX', N'Unix Operating System', N'KHM', 2, 0.4, 0.6)
GO
INSERT [dbo].[HocPhanTienQuyet] ([MaHocPhan], [MaHocPhanTienQuyet], [RangBuoc]) VALUES (N'IE3.003.3', N'IT0.004.2', 0)
GO
INSERT [dbo].[HocPhanTienQuyet] ([MaHocPhan], [MaHocPhanTienQuyet], [RangBuoc]) VALUES (N'IE3.003.3', N'IT1.106.3', 0)
GO
INSERT [dbo].[HocPhanTienQuyet] ([MaHocPhan], [MaHocPhanTienQuyet], [RangBuoc]) VALUES (N'IE3.003.3', N'IT1.110.3', 1)
GO
INSERT [dbo].[HocPhanTienQuyet] ([MaHocPhan], [MaHocPhanTienQuyet], [RangBuoc]) VALUES (N'IE3.003.3', N'IT1.115.3', 0)
GO
INSERT [dbo].[HocPhanTienQuyet] ([MaHocPhan], [MaHocPhanTienQuyet], [RangBuoc]) VALUES (N'IE3.004.3', N'IT0.004.2', 0)
GO
INSERT [dbo].[HocPhanTienQuyet] ([MaHocPhan], [MaHocPhanTienQuyet], [RangBuoc]) VALUES (N'IT1.107.3', N'IT0.004.2                ', 0)
GO
INSERT [dbo].[HocPhanTienQuyet] ([MaHocPhan], [MaHocPhanTienQuyet], [RangBuoc]) VALUES (N'IT1.110.3', N'IT0.004.2', 1)
GO
INSERT [dbo].[HocPhanTienQuyet] ([MaHocPhan], [MaHocPhanTienQuyet], [RangBuoc]) VALUES (N'IT1.217.3', N'IT0.004.2', 0)
GO
INSERT [dbo].[HocPhanTienQuyet] ([MaHocPhan], [MaHocPhanTienQuyet], [RangBuoc]) VALUES (N'IT1.217.3', N'IT1.106.3', 0)
GO
INSERT [dbo].[HocPhanTienQuyet] ([MaHocPhan], [MaHocPhanTienQuyet], [RangBuoc]) VALUES (N'IT1.217.3', N'IT1.110.3', 0)
GO
INSERT [dbo].[HocPhanTienQuyet] ([MaHocPhan], [MaHocPhanTienQuyet], [RangBuoc]) VALUES (N'IT1.223.2', N'IT1.110.3', 1)
GO
INSERT [dbo].[HocPhanTienQuyet] ([MaHocPhan], [MaHocPhanTienQuyet], [RangBuoc]) VALUES (N'IT1.227.2', N'IT0.004.2', 0)
GO
INSERT [dbo].[Khoa] ([MaKhoa], [TenKhoa], [TenTiengAnh], [DiaChi], [DienThoai], [Email], [Website]) VALUES (N'CK', N'CƠ KHÍ', N'MECHANICAL ENGINEERING', N'102A2', N'02435568670', N'ck@utc.edu.vn', N'fme.utc.vn')
GO
INSERT [dbo].[Khoa] ([MaKhoa], [TenKhoa], [TenTiengAnh], [DiaChi], [DienThoai], [Email], [Website]) VALUES (N'CNTT', N'CÔNG NGHỆ THÔNG TIN', N'INFORMATION TECHNOLOGY', N'306A9   ', N'02435568668', N'cntt@utc.edu.vn', N'fit.utc.vn')
GO
INSERT [dbo].[Khoa] ([MaKhoa], [TenKhoa], [TenTiengAnh], [DiaChi], [DienThoai], [Email], [Website]) VALUES (N'CT', N'CÔNG TRÌNH', N'CIVIL ENGINEERING', N'106A6', N'02435568669', N'ct@utc.edu.vn', N'fce.utc.vn')
GO
INSERT [dbo].[Khoa] ([MaKhoa], [TenKhoa], [TenTiengAnh], [DiaChi], [DienThoai], [Email], [Website]) VALUES (N'DDT', N'ĐIỆN - ĐIỆN TỬ', N'ELECTRICAL-ELECTRONIC ENGINEERING', N'305A4', N'02435568671', N'ddt@utc.edu.vn', N'fee.utc.vn')
GO
INSERT [dbo].[Lop] ([MaLop], [TenLop], [MaKhoa], [KhoaHoc]) VALUES (N'K61.CK1', N'CƠ KHÍ 1', N'CK', N'61')
GO
INSERT [dbo].[Lop] ([MaLop], [TenLop], [MaKhoa], [KhoaHoc]) VALUES (N'K61.CNTT2', N'CÔNG NGHỆ THÔNG TIN 2', N'CNTT', N'61  ')
GO
INSERT [dbo].[Lop] ([MaLop], [TenLop], [MaKhoa], [KhoaHoc]) VALUES (N'K61.CT1', N'CÔNG TRÌNH 1', N'CT', N'61')
GO
INSERT [dbo].[Lop] ([MaLop], [TenLop], [MaKhoa], [KhoaHoc]) VALUES (N'K61.DDT1', N'ĐIỆN - ĐIỆN TỬ 1', N'DDT', N'61')
GO
INSERT [dbo].[Lop] ([MaLop], [TenLop], [MaKhoa], [KhoaHoc]) VALUES (N'K62.CK2', N'CƠ KHÍ 2', N'CK', N'62')
GO
INSERT [dbo].[Lop] ([MaLop], [TenLop], [MaKhoa], [KhoaHoc]) VALUES (N'K62.CNTTVA2', N'CÔNG NGHỆ THÔNG TIN CHẤT LƯỢNG CAO VIỆT - ANH 2', N'CNTT', N'62  ')
GO
INSERT [dbo].[Lop] ([MaLop], [TenLop], [MaKhoa], [KhoaHoc]) VALUES (N'K62.CTVA1', N'CÔNG TRÌNH CHẤT LƯỢNG CAO VIỆT - ANH 1', N'CT', N'62')
GO
INSERT [dbo].[Lop] ([MaLop], [TenLop], [MaKhoa], [KhoaHoc]) VALUES (N'K62.DDT2', N'ĐIỆN - ĐIỆN TỬ 2', N'DDT', N'62')
GO
INSERT [dbo].[LopHocPhan] ([MaHocPhan], [MaLopHocPhan], [TenLopHocPhan], [MaGiangVien], [NamHoc], [HocKy], [DotHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'Công nghệ Java-1-2-22(QT02)', N'0172', N'2022-2023', 2, 1)
GO
INSERT [dbo].[LopHocPhan] ([MaHocPhan], [MaLopHocPhan], [TenLopHocPhan], [MaGiangVien], [NamHoc], [HocKy], [DotHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'Lập trình thiết bị di động-1-2-23(QT02)', N'1268', N'2023-2024', 2, 1)
GO
INSERT [dbo].[LopHocPhan] ([MaHocPhan], [MaLopHocPhan], [TenLopHocPhan], [MaGiangVien], [NamHoc], [HocKy], [DotHoc]) VALUES (N'IE3.004.3', N'1-2-24-QT02', N'Lập trình thiết bị di động-1-2-24(QT02)', N'1268', N'2024-2025', 2, 1)
GO
INSERT [dbo].[LopHocPhan] ([MaHocPhan], [MaLopHocPhan], [TenLopHocPhan], [MaGiangVien], [NamHoc], [HocKy], [DotHoc]) VALUES (N'IT1.110.3', N'1-2-22-N01', N'Cơ sở dữ liệu-1-2-22(N01)', N'1674', N'2022-2023', 2, 1)
GO
INSERT [dbo].[LopHocPhan] ([MaHocPhan], [MaLopHocPhan], [TenLopHocPhan], [MaGiangVien], [NamHoc], [HocKy], [DotHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'Cơ sở dữ liệu-1-2-22(QT08)', N'1674', N'2023-2024', 2, 1)
GO
INSERT [dbo].[LopHocPhan] ([MaHocPhan], [MaLopHocPhan], [TenLopHocPhan], [MaGiangVien], [NamHoc], [HocKy], [DotHoc]) VALUES (N'IT1.110.3', N'1-2-24-N01', N'Cơ sở dữ liệu-1-2-24(N01)', N'1674', N'2024-2025', 1, 1)
GO
INSERT [dbo].[LopHocPhan] ([MaHocPhan], [MaLopHocPhan], [TenLopHocPhan], [MaGiangVien], [NamHoc], [HocKy], [DotHoc]) VALUES (N'IT1.110.3', N'2-2-22-N01', N'Cơ sở dữ liệu-2-2-22(N01)', N'1674', N'2023-2024', 2, 2)
GO
INSERT [dbo].[LopHocPhan] ([MaHocPhan], [MaLopHocPhan], [TenLopHocPhan], [MaGiangVien], [NamHoc], [HocKy], [DotHoc]) VALUES (N'IT1.113.3', N'1-2-24-N02', N'Công nghệ Java-1-2-24(N02)', N'0172', N'2024-2025', 2, 1)
GO
INSERT [dbo].[LopHocPhan] ([MaHocPhan], [MaLopHocPhan], [TenLopHocPhan], [MaGiangVien], [NamHoc], [HocKy], [DotHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'Lập trình web-1-2-22(N02)', N'1674', N'2022-2023', 2, 1)
GO
INSERT [dbo].[LopHocPhan] ([MaHocPhan], [MaLopHocPhan], [TenLopHocPhan], [MaGiangVien], [NamHoc], [HocKy], [DotHoc]) VALUES (N'IT1.217.3', N'2-2-11-N01', N'Lập trình web-1-2-22(N02)', N'1387', N'2023-2024', 2, 2)
GO
INSERT [dbo].[LopHocPhan] ([MaHocPhan], [MaLopHocPhan], [TenLopHocPhan], [MaGiangVien], [NamHoc], [HocKy], [DotHoc]) VALUES (N'IT1.217.3', N'2-2-22-N01', N'Lập trình web-1-2-22(N02)', N'1387', N'2024-2025', 2, 1)
GO
INSERT [dbo].[LopHocPhan] ([MaHocPhan], [MaLopHocPhan], [TenLopHocPhan], [MaGiangVien], [NamHoc], [HocKy], [DotHoc]) VALUES (N'IT1.217.3', N'2-2-23-N01', N'Lập trình web-1-2-22(N02)', N'1387', N'2023-2024', 2, 2)
GO
INSERT [dbo].[LopHocPhan] ([MaHocPhan], [MaLopHocPhan], [TenLopHocPhan], [MaGiangVien], [NamHoc], [HocKy], [DotHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'Trí tuệ nhân tạo-1-2-23(N03)', N'1387', N'2023-2024', 2, 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'171202737', 10, 8, 9, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'183000703', 10, 8, 9, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'191200482', 8.5, 8, 8.3, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212600556', 6.5, 8.8, 7.7, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212601105', 8.5, 8.5, 8.5, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212602500', 8.5, 9.6, 9.1, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212602565', 9.2, 2.6, 5.9, 2, N'C', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212602719', 8, 2.8, 5.4, 1.5, N'D+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212602930', 8.6, 10, 9.3, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212603983', 8.5, 8.6, 8.6, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212604007', 6, 9.7, 7.8, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212606012', 6.2, 8.8, 7.5, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212606014', 8.5, 8.5, 8.5, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212606016', 9.1, 8.2, 8.6, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212606018', 5.6, 9.5, 7.5, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212610094', 6, 5.8, 5.9, 2, N'C', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212610442', 9, 8.8, 8.9, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212610443', 7.5, 9, 8.3, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212610781', 9.5, 8.6, 9.1, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212611377', 10, 9.6, 9.8, 4, N'A+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212612471', 9, 8.5, 8.8, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212612929', 8.1, 9.5, 8.8, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212613775', 7.2, 8.3, 7.8, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212616010', 10, 8.8, 9.4, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212630022', 7.5, 10, 8.8, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212630095', 7.5, 8.5, 8, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212630184', 8.3, 9, 8.7, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212630895', 7.6, 9.7, 8.6, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212630972', 8.3, 7.6, 8, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212631446', 7.6, 8.8, 8.2, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212632702', 9.5, 8.8, 9.2, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212633564', 7.3, 8.6, 7.9, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212633819', 6.8, 9.5, 8.2, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212633820', 9.3, 8.8, 9.1, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212633938', 7, 8.5, 7.8, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212634149', 8, 9.5, 8.8, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212640397', 10, 9.8, 9.9, 4, N'A+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212642248', 7.3, 7.5, 7.4, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'212643488', 9.6, 9.8, 9.7, 4, N'A+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'882126003', 4, 0, 2, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'882126009', 8.5, 7.8, 8.2, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'882126013', 7.3, 7.7, 7.5, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'882126017', 6.6, 8.7, 7.6, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'882126030', 7.8, 9, 8.4, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.002.3', N'1-2-22-QT02', N'882126033', 9, 7.2, 8.1, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212600556', 8.3, 8.6, 8.4, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212601105', 8.5, 10, 9.3, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212602500', 7.8, 9.6, 8.7, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212602565', 7.2, 8.6, 7.9, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212602719', 7.3, 9.5, 8.4, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212602930', 2.1, 8.5, 5.3, 1.5, N'D+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212603983', 8.5, 8.6, 8.6, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212604007', 7.3, 8, 7.7, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212606012', 0.6, 0, 0.3, 0, N'F', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212606014', 5.5, 9.2, 7.3, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212606016', 3, 1, 2, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212606018', 9.8, 9.5, 9.7, 4, N'A+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212610094', 6.5, 0.3, 3.4, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212610442', 9.8, 8.2, 9, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212610443', 9.2, 9.8, 9.5, 4, N'A+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212610781', 8.2, 8.5, 8.3, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212611377', 7.6, 7.8, 7.7, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212612471', 7.2, 8.6, 7.9, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212612929', 0.6, 9.6, 5.1, 1.5, N'D+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212613775', 8.7, 7.6, 8.1, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212616010', 8, 6.8, 7.4, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212630022', 6.8, 9.8, 8.3, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212630095', 10, 10, 10, 4, N'A+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212630184', 10, 10, 10, 4, N'A+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212630895', 6, 8.5, 7.3, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212630972', 9.1, 6.5, 7.8, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212631446', 1.5, 5.7, 3.6, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212632702', 8.1, 9.3, 8.7, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212633564', 7.6, 9.2, 8.4, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212633819', 0.2, 9.5, 4.8, 1.5, N'D+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212633820', 8.5, 8.2, 8.3, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212633938', 6.5, 9, 7.8, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212634149', 8, 9.7, 8.8, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212640397', 7.1, 8.8, 8, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212642248', 6.5, 7.6, 7, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'212643488', 7.7, 8.5, 8.1, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'882126003', 10, 8.8, 9.4, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'882126009', 7.3, 8.8, 8.1, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'882126013', 6.3, 10, 8.2, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'882126017', 6.3, 8.6, 7.4, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'882126030', 8.3, 9.5, 8.9, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-23-QT02', N'882126033', 6.7, 7, 6.8, 2.5, N'C+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IE3.004.3', N'1-2-24-QT02', N'212606016', 8, 8, 8, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-N01', N'183000703', 2, 3.5, 2.8, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-N01', N'201200161', 3, 4, 3.5, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-N01', N'201200168', 2.5, 4, 3.3, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-N01', N'201200177', 3.5, 4.5, 4, 1, N'D', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-N01', N'201200185', 1.5, 2.5, 2, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-N01', N'201200190', 4, 2, 3, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-N01', N'201200203', 4, 3, 3.5, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-N01', N'201200206', 4.5, 3, 3.8, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-N01', N'201200211', 3.5, 2.5, 3, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-N01', N'201200226', 2, 2, 2, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-N01', N'201200229', 3, 1.5, 2.3, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-N01', N'201200234', 1.5, 4, 2.8, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-N01', N'201200235', 3, 4, 3.5, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-N01', N'201200245', 3.5, 4.5, 4, 1, N'D', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-N01', N'201210225', 2.5, 3.5, 3, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212600556', 8.8, 4.7, 6.8, 2.5, N'C+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212601105', 8.5, 8, 8.3, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212602500', 0.6, 4.5, 2.5, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212602565', 7.6, 7.5, 7.5, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212602719', 8.1, 9, 8.6, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212602930', 9.2, 7.2, 8.2, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212603983', 8, 8.6, 8.3, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212604007', 8.7, 7.5, 8.1, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212606012', 8, 8, 8, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212606014', 7.7, 6.8, 7.3, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212606016', 9.8, 8.8, 9.3, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212606018', 8.8, 8, 8.4, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212610094', 8.2, 8.2, 8.2, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212610442', 3.3, 8, 5.7, 2, N'C', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212610443', 9.2, 7.1, 8.1, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212610781', 8, 3.8, 5.9, 2, N'C', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212611377', 9, 7.5, 8.3, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212612471', 7.5, 7, 7.3, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212612929', 8, 6.7, 7.3, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212613775', 8.8, 8.6, 8.7, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212616010', 8, 8.1, 8.1, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212630022', 8.1, 9.1, 8.6, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212630095', 9, 8.3, 8.7, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212630184', 9, 9, 9, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212630895', 9.8, 9.7, 9.8, 4, N'A+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212630972', 0, 7.2, 3.6, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212631446', 8, 0.3, 4.2, 1, N'D', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212632702', 9, 7.1, 8.1, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212633564', 8.6, 7.5, 8.1, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212633819', 8.1, 6.5, 7.3, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212633820', 7.7, 0.5, 4.1, 1, N'D', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212633938', 8.8, 8, 8.4, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212634149', 8, 9.5, 8.8, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212640397', 2, 2, 2, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212642248', 7.6, 8.5, 8.1, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'212643488', 8.1, 0, 4, 1, N'D', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'882126003', 7.6, 7.5, 7.5, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'882126009', 8.1, 7.3, 7.7, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'882126013', 8.6, 7.8, 8.2, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'882126017', 8.5, 8, 8.3, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'882126030', 8.8, 10, 9.4, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-22-QT08', N'882126033', 7.7, 9.5, 8.6, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'1-2-24-N01', N'201200026', 4, 3, 3.5, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'2-2-22-N01', N'183000703', 10, 9.8, 9.9, 4, N'A+', 2)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'2-2-22-N01', N'201200161', 10, 8.8, 9.4, 3.8, N'A', 2)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'2-2-22-N01', N'201200168', 8.3, 7.6, 8, 3.5, N'B+', 2)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'2-2-22-N01', N'201200177', 8.5, 7.8, 8.2, 3.5, N'B+', 2)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'2-2-22-N01', N'201200185', 6, 5.8, 5.9, 2, N'C', 2)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'2-2-22-N01', N'201200190', 7.3, 7.7, 7.5, 3, N'B', 2)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'2-2-22-N01', N'201200203', 7.3, 8.6, 7.9, 3, N'B', 2)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'2-2-22-N01', N'201200206', 9.6, 9.8, 9.7, 4, N'A+', 2)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'2-2-22-N01', N'201200211', 8, 2.8, 5.4, 1.5, N'D+', 2)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'2-2-22-N01', N'201200226', 8.5, 8.6, 8.6, 3.8, N'A', 2)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'2-2-22-N01', N'201200229', 4, 4, 4, 1, N'D', 2)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'2-2-22-N01', N'201200234', 4, 4.5, 4.3, 1, N'D', 2)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'2-2-22-N01', N'201200235', 4.5, 5, 4.8, 1.5, N'D+', 2)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'2-2-22-N01', N'201200245', 6, 4.5, 5.3, 1.5, N'D+', 2)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.110.3', N'2-2-22-N01', N'201210225', 9.5, 8.6, 9.1, 3.8, N'A', 2)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.113.3', N'1-2-24-N02', N'201200026', 7, 9, 8, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'171202737', 9.5, 7.5, 8.5, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'191200482', 9, 8.7, 8.8, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'191213486', 8.3, 8.5, 8.4, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200005', 7.5, 7.2, 7.3, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200014', 7.3, 7.5, 7.4, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200021', 8, 6.8, 7.4, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200026', 3, 2, 2.5, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200029', 9.1, 9.5, 9.3, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200043', 9.8, 6, 7.9, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200053', 9.3, 6.8, 8.1, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200057', 9.8, 8.7, 9.3, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200060', 9.1, 0.2, 4.6, 1.5, N'D+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200066', 8.6, 9.5, 9.1, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200085', 9.5, 8.8, 9.2, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200087', 8.5, 9.5, 9, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200102', 8, 9.3, 8.7, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200108', 8.2, 8.5, 8.3, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200116', 8.5, 8, 8.3, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200130', 7.3, 9.3, 8.3, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200137', 9.5, 8.1, 8.8, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200138', 8, 8, 8, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200160', 9, 7.5, 8.3, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200163', 7.8, 6.2, 7, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200173', 5, 9, 7, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200179', 7.6, 7.3, 7.4, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200188', 9.3, 9.5, 9.4, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200193', 9.2, 9.8, 9.5, 4, N'A+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200212', 8.6, 9.8, 9.2, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200217', 10, 7.6, 8.8, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200248', 2.7, 8.2, 5.4, 1.5, N'D+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200263', 8.8, 8.1, 8.4, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200265', 8.5, 8.3, 8.4, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200266', 8.1, 7.8, 7.9, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200267', 7.6, 7.6, 7.6, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200281', 3.8, 0, 1.9, 0, N'F', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200288', 4.2, 8.5, 6.3, 2.5, N'C+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200292', 4.8, 8.1, 6.4, 2.5, N'C+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200295', 8.8, 7.1, 8, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200302', 7.5, 7.8, 7.7, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200316', 8.3, 7.8, 8.1, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200322', 4, 2, 3, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200324', 8.6, 6.5, 7.5, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200341', 6.2, 1, 3.6, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200342', 9, 6.5, 7.8, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200346', 8.2, 3.8, 6, 2.5, N'C+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200361', 8.5, 8.1, 8.3, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200382', 10, 9.1, 9.6, 4, N'A+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200385', 9, 7.8, 8.4, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200392', 9, 6.6, 7.8, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200399', 7.3, 7, 7.2, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200412', 0, 8, 4, 1, N'D', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201200422', 6.5, 5.2, 5.8, 2, N'C', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201205542', 8, 7.5, 7.8, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201206522', 8.3, 6.3, 7.3, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201206931', 8.7, 9.3, 9, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201207282', 7.6, 0, 3.8, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201207326', 3.3, 7.2, 5.3, 1.5, N'D+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201210114', 9.3, 4.3, 6.8, 2.5, N'C+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201210159', 8.6, 6.8, 7.7, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201210176', 9.7, 7.5, 8.6, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201210256', 8.7, 2.1, 5.4, 1.5, N'D+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201210294', 9.6, 0.6, 5.1, 1.5, N'D+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201210305', 0, 8.5, 4.3, 1, N'D', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201210356', 7.1, 8.5, 7.8, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201210407', 9, 7.2, 8.1, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201216824', 9, 9, 9, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'201240414', 4, 1.5, 2.8, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'1-2-22-N02', N'991790002', 8.1, 7.8, 7.9, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'2-2-22-N01', N'201200026', 7.3, 7, 7.2, 3, N'B', 2)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'2-2-22-N01', N'201200322', 3, 4, 3.5, 0.5, N'F+', 2)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.217.3', N'2-2-23-N01', N'201200322', 8, 6, 7, 3, N'B', 3)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'171202737', 7.7, 9, 8.3, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'191200482', 6.6, 9.2, 7.9, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'191213486', 9.1, 8.8, 8.9, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200005', 7.6, 6.5, 7, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200014', 8.1, 7.6, 7.8, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200021', 7.5, 9.6, 8.6, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200026', 6.8, 8.8, 7.8, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200029', 7, 8.8, 7.9, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200043', 8.2, 9.7, 8.9, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200053', 10, 9.2, 9.6, 4, N'A+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200057', 8, 6, 7, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200060', 8.5, 8.7, 8.6, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200066', 8.7, 7.6, 8.1, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200085', 0, 9.2, 4.6, 1.5, N'D+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200087', 7.7, 7.1, 7.4, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200102', 7.6, 7.3, 7.4, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200108', 9, 6.8, 7.9, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200116', 9.5, 9.1, 9.3, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200130', 0, 6.6, 3.3, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200137', 8.5, 6.5, 7.5, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200138', 9.2, 6.7, 7.9, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200160', 9, 8.1, 8.6, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200163', 8.1, 8.2, 8.1, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200173', 8.3, 7.8, 8.1, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200179', 7.5, 9, 8.3, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200188', 5.5, 7.3, 6.4, 2.5, N'C+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200193', 9, 8, 8.5, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200212', 8.5, 7.5, 8, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200217', 7.8, 7.8, 7.8, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200248', 0, 6.5, 3.3, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200263', 8.8, 7.5, 8.2, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200265', 8.8, 7.3, 8.1, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200266', 9.2, 6.5, 7.8, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200267', 8.5, 9.5, 9, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200281', 7.8, 10, 8.9, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200288', 9.6, 8.6, 9.1, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200292', 8.8, 8.7, 8.8, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200295', 8.8, 9.6, 9.2, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200302', 10, 7.5, 8.8, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200316', 7.6, 8.5, 8.1, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200322', 6.8, 8.1, 7.4, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200324', 5.6, 10, 7.8, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200341', 0.6, 7.3, 3.9, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200342', 8.5, 6.8, 7.7, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200346', 6.8, 8, 7.4, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200361', 7.8, 4.6, 6.2, 2.5, N'C+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200382', 7, 8, 7.5, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200385', 7.2, 8.8, 8, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200392', 7.2, 9.5, 8.3, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200399', 9.1, 9.3, 9.2, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200412', 8.2, 6, 7.1, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201200422', 7.5, 0.3, 3.9, 0.5, N'F+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201205542', 9.5, 6.8, 8.2, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201206522', 9.2, 8.6, 8.9, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201206931', 8.8, 8.6, 8.7, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201207282', 7.8, 7.6, 7.7, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201207326', 2, 7.8, 4.9, 1.5, N'D+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201210114', 9.2, 7.8, 8.5, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201210159', 8.8, 6.8, 7.8, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201210176', 6.7, 8.5, 7.6, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201210256', 7.1, 10, 8.6, 3.8, N'A', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201210294', 5.1, 9.3, 7.2, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201210305', 6.5, 8.5, 7.5, 3, N'B', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201210356', 5.2, 8.2, 6.7, 2.5, N'C+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201210407', 5.6, 5, 5.3, 1.5, N'D+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201216824', 9.6, 7.1, 8.3, 3.5, N'B+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'201240414', 8.5, 1.3, 4.9, 1.5, N'D+', 1)
GO
INSERT [dbo].[LopHocPhan_SinhVien] ([MaHocPhan], [MaLopHocPhan], [MaSinhVien], [DiemQuaTrinh], [DiemThiKTHP], [DiemTKHP], [DiemHe4], [DiemHeChu], [LanHoc]) VALUES (N'IT1.220.3', N'1-2-23-N03', N'991790002', 9.6, 7.1, 8.3, 3.5, N'B+', 1)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'171202737', N'Lê Thiên', N'Khang', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-08-11' AS Date), 1, N'số 275, Vũ Tông Phan, P. Khương Trung, Q.Thanh Xuân, TP Hà Nội ', N'0968809252', N'LeThienKhang@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'183000703', N'Cao Hùng', N'Anh', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-10-03' AS Date), 1, N'Phường Láng, Hà Nội', N'0906789123', N'CaoHungAnh@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'191200482', N'Hoàng Kim', N'Dũng', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-04-21' AS Date), 1, N'Nhà số 2 , xóm Tây Phướn , phường Trang Hạ , thị xã Từ Sơn , tỉnh Bắc Ninh ', N'0966090629', N'HoangKimDung@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'191213486', N'Chu Mạnh', N'Quân', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-04-09' AS Date), 1, N'Số 5 tổ dân phố Tháp phường đại mỗ Nam từ liêm Hà nội', N'0982129703', N'ChuManhQuan@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200005', N'Nguyễn Văn', N'Thắng', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-10-24' AS Date), 1, N'Phường Láng, Hà Nội', N'0385548856', N'NguyenVanThang@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200014', N'Phạm Nguyễn Phúc', N'Thắng', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-10-25' AS Date), 1, N'số nhà 102 đường minh xuân, thôn trường an, xã an khánh, hoài đức, hà nội', N'0394049011', N'PhamNguyenPhucThang@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200021', N'Nguyễn Vũ', N'Thiện', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-01-18' AS Date), 1, N'Số 15/175 định công - thịnh liệt- hoàng mai', N'0914851967', N'NguyenVuThien@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200026', N'Nguyễn Đức', N'Thuận', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-09-09' AS Date), 1, N' 14A4 Khu tập Thể Pin Văn Điển, Thanh Trì, Hà Nội', N'0982791899', N'NguyenDucThuan@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200029', N'Nguyễn Văn', N'Tiến', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-03-24' AS Date), 1, N'0978238503 số 5 ngách 99/141 Định Công Hoàng Mai', N'0368754143', N'NguyenVanTien@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200043', N'Triệu Việt', N'Trung', N'7.48.02.01', N'K61.CNTT2', CAST(N'2001-12-27' AS Date), 1, N'Xã Hoằng Châu, Tỉnh Thanh Hóa', N'0948593044', N'TrieuVietTrung@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200053', N'Đỗ Anh', N'Tuấn', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-07-12' AS Date), 1, NULL, N'0917390319', N'DoAnhTuan@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200057', N'Nguyễn Bá', N'Tuấn', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-01-11' AS Date), 1, NULL, N'0397759286', N'NguyenBoTuan@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200060', N'Vũ Trung', N'Tuấn', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-04-05' AS Date), 1, NULL, N'0913010872', N'VuTrungTuan@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200066', N'Phạm Xuân', N'Tùng', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-04-24' AS Date), 1, NULL, N'0946279422', N'PhamXuonTung@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200085', N'Nguyễn Trọng', N'Văn', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-06-29' AS Date), 1, NULL, N'0979917356', N'NguyenTrongVan@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200087', N'Lê Quang', N'Việt', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-08-05' AS Date), 1, NULL, N'0946069818', N'LoQuangViet@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200102', N'Trần Đức', N'An', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-02-12' AS Date), 1, N'P.1505, tòa nhà Vinaconex 6, số 61, tổ 15, phường Cầu Diễn, quận Nam Từ Liêm, thành phố Hà Nội', N'0878758412', N'TranDucAn@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200108', N'Nguyễn Quỳnh', N'Anh', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-12-14' AS Date), 0, N'P306, 21B4 CT2 KĐT TPGL, 234 Phạm Văn Đồng, TDP Hoàng 22, Cổ Nhuế 1, Bắc Từ Liêm, Hà Nội', N'0965731615', N'NguyenQuynhAnh@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200116', N'Trịnh Xuân', N'Bách', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-02-11' AS Date), 1, N'Xóm mới - xã thuỵ lôi-kim bảng- hà nam', N'0932290479', N'TrinhXuonBoch@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200130', N'Nguyễn Phương', N'Bắc', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-03-20' AS Date), 1, N'Xóm Mỹ Hưng, thôn Quang Trung, xã Quỳnh Mỹ, huyện Quỳnh Phụ, tỉnh Thái Bình', N'0789377434', N'NguyenPhuongBac@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200137', N'Nguyễn Đăng Hoàng', N'Bình', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-10-08' AS Date), 1, NULL, N'0397615693', N'NguyenDangHoangBonh@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200138', N'Mai Duy', N'Chinh', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-08-02' AS Date), 1, N'P506-B3, Ngọc Khánh, Ba Đình, Hà Nội', N'0346954746', N'MaiDuyChinh@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200160', N'Phạm Tiến', N'Dũng', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-12-17' AS Date), 1, N'Số 915, tổ dân phố 9, Phú Lãm, Hà Đông, Hà Nội', N'0383645108', N'PhamTienDung@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200161', N'Nguyễn Bảo', N'Hồng', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-07-08' AS Date), 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200163', N'Tống Trung', N'Đoàn', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-01-29' AS Date), 1, N'Xóm 10 Xã Hải Trung Huyện Hải Hậu Tỉnh Nam Định', N'0376981003', N'TongTrungDoan@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200168', N'Nguyễn Đức', N'An', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-12-30' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200173', N'Hoàng Minh', N'Đức', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-03-27' AS Date), 1, N'Đội 5-thôn Phương La-xã Cẩm Chế-huyện Thanh Hà-tỉnh Hải Dương', N'0795255527', N'HoangMinhDuc@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200177', N'Nguyễn Hữu', N'An', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-04-26' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200179', N'Đỗ Quang', N'Giáp', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-03-29' AS Date), 1, N'Bãi Cả - Cúc Phương - Nho Quan - Ninh Bình', N'0962745975', N'DoQuangGiop@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200185', N'Đoàn Thị Mai', N'Anh', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-10-03' AS Date), 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200188', N'Vũ Đức', N'Hải', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-02-07' AS Date), 1, N'Thôn Kim Lũ - xã Phù Ủng - huyện Ân Thi - tỉnh Hưng Yên', N'0392049560', N'VuDucHai@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200190', N'Nguyễn Thị Phương', N'Anh', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-08-01' AS Date), 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200193', N'Lê Công', N'Hiệp', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-04-13' AS Date), 1, N'SN 263 - tổ 7- phường tân thanh -Tp.Điện Biên Phủ -tỉnh Điện Biên', N'0888457996', N'LoCungHiep@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200203', N'Nguyễn Việt', N'Anh', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-01-02' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200206', N'Vũ Hoàng', N'Anh', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-11-24' AS Date), 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200211', N'Phạm Khắc', N'Chiến', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-08-31' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200212', N'Đặng Thị', N'Hoa', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-08-22' AS Date), 0, N'C8, cầu thang 3, khu TT Kim Liên, Đống Đa, Hà Nội', N'0386204932', N'DangThiHoa@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200217', N'Nguyễn Huy', N'Hoàng', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-04-26' AS Date), 1, N'Chợ 3D Xã Tiến Thịnh Huyện Mê Linh Thành Phố Hà Nội', N'0969341115', N'NguyenHuyHoang@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200226', N'Đào Thị', N'Diễm', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-01-31' AS Date), 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200229', N'Hoàng Anh', N'Dũng', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-04-18' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200234', N'Nguyễn Công Tuấn', N'Dũng', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-09-01' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200235', N'Đinh Nguyễn Tùng', N'Duy', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-08-28' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200245', N'Đỗ Thành', N'Đạt', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-12-20' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200248', N'Nguyễn Văn', N'Hoàng', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-05-09' AS Date), 1, N'Số 16 Hàng Đậu, phường Đồng Xuân, quận Hoàn Kiếm, TP Hà Nội', N'0928492950', N'NguyenVanHoang@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200263', N'Đỗ Đức', N'Huy', N'7.48.02.01', N'K61.CNTT2', CAST(N'2001-01-26' AS Date), 1, N'số nhà 102 đường minh xuân, thôn trường an, xã an khánh, hoài đức, hà nội', N'0919952816', N'DoDucHuy@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200265', N'Nguyễn Đình An', N'Huy', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-03-18' AS Date), 1, N'Số 15/175 định công - thịnh liệt- hoàng mai', N'0789036150', N'NguyenDonhAnHuy@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200266', N'Nguyễn Quang', N'Huy', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-12-02' AS Date), 1, N'14A4 Khu tập Thể Pin Văn Điển, Thanh Trì, Hà Nội', N'0707871103', N'NguyenQuangHuy@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200267', N'Nguyễn Quang', N'Huy', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-06-21' AS Date), 1, N'số 5 ngách 99/141 Định Công Hoàng Mai', N'0345764892', N'NguyenQuangHuy@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200281', N'Nguyễn Tuấn', N'Hưng', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-05-11' AS Date), 1, N'Thái Khê Cấn Hữu Quốc Oai Hà Nội', N'0936114598', N'NguyenTuanHung@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200288', N'Trần Đức', N'Hữu', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-08-31' AS Date), 1, N'TDP 5 - Vân Gia - Trung Hưng - Sơn Tây - Hà Nội', N'0981356325', N'TranDucHuu@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200292', N'Lê Mạnh', N'Khánh', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-05-11' AS Date), 1, N' Địa chỉ : số nhà 12 đường chùa Tăng Phúc, Phường Đông Cương, Thành Phố Thanh Hóa', N'0989799065', N'LoManhKhonh@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200295', N'Vũ Trung', N'Kiên', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-03-10' AS Date), 1, NULL, N'0912538138', N'VuTrungKion@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200302', N'Nguyễn Đình', N'Lộc', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-05-05' AS Date), 1, N'thôn 2 Do Dạo xã Nhân Thịnh huyện Lý Nhân tỉnh Hà Nam', N'0857533287', N'NguyenDonhLoc@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200316', N'Nguyễn Huy', N'Nghĩa', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-06-28' AS Date), 1, N'thông Đông Thượng Liệt - xã Đông Tân - Đông Hưng- Thái Bình', N'0795298940', N'NguyenHuyNghia@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200322', N'Nguyễn Xuân', N'Ngọc', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-11-17' AS Date), 1, N'19 ngõ 74 An Trạch, Cát Linh, Đống Đa, Hà Nội', N'0325762795', N'NguyenXuonNgoc@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200324', N'Vũ Thị', N'Nhung', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-10-17' AS Date), NULL, N'Xóm Đường Bòng-xã Đông Tảo-huyện Khoái Châu-tỉnh Hưng Yên', N'0382090703', N'VuThiNhung@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200341', N'Trần Quang', N'Ninh', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-02-08' AS Date), 1, N'SN 09 - Ngõ 02 - Đường 03 - thôn Đạt Tài 2 - xã Hoằng Hà - huyện Hoằng Hóa -  tỉnh Thanh Hóa', N'0379457583', N'TranQuangNinh@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200342', N'Trần Văn', N'Ninh', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-02-14' AS Date), 1, N'A6 Chung cư An Bình City ,Phường Cổ Nhuế 1,Quận Bắc Từ Liêm ,Hà Nội', N'0382672329', N'TranVanNinh@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200346', N'Hà Hoàng', N'Oanh', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-10-14' AS Date), 0, NULL, N'0963025745', N'HaHoangOanh@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200361', N'Đào Mạnh', N'Quang', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-02-05' AS Date), 1, N'Số 3/26, ngõ 78, tổ 6 Ngọc Thụy, Long Biên, Hà Nội', N'0889762488', N'DaoManhQuang@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200382', N'Nguyễn Hoàng', N'Quân', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-11-23' AS Date), 1, N'Cầu cả,  xóm 14, xã Xuân Kiên, huyện Xuân Trường, tỉnh Nam Định', N'0383307634', N'NguyenHoangQuon@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200385', N'Nguyễn Ngọc', N'Quân', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-12-06' AS Date), 1, N' số 9 phú lãm hà đông hà nội', N'0383645108', N'NguyenNgocQuon@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200392', N'Vương Anh', N'Quân', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-11-22' AS Date), 1, N'Số 132 tổ 2 phường Giang Biên Quận Long Biên Hà Nội', N'0941895386', N'VuongAnhQuon@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200399', N'Trần Bảo', N'Quốc', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-11-05' AS Date), 1, N'19 ngõ 74 An Trạch, Cát Linh, Đống Đa, Hà Nội', N'0325762795', N'TranBaoQuoc@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200412', N'Đào Đức', N'Sơn', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-01-31' AS Date), 1, N'C8, cầu thang 3, khu TT Kim Liên, Đống Đa, Hà Nội', N'0912307158', N'DaoDucSon@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201200422', N'Ngô Đức', N'Tài', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-08-30' AS Date), 1, N' số 16 Hàng Đậu, phường Đồng Xuân, quận Hoàn Kiếm, TP Hà Nội', N'', N'NguDucTai@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201205542', N'Nguyễn Hoàng', N'Lan', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-07-05' AS Date), 0, N'SN 263 - tổ 7 - P. Tân Thanh - TP. Điện Biên Phủ - tỉnh Điện Biên ', N'0888457996', N'NguyenHoangLan@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201206522', N'Nguyễn Ngọc', N'Hiệp', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-08-05' AS Date), 1, N'10/82 ngõ Trung Tả, Phường Thổ Quan, Đống Đa, Hà Nội ', N'0966421283', N'NguyenNgocHiep@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201206931', N'Phùng Thảo', N'Trang', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-07-20' AS Date), 0, NULL, N'0945887961', N'PhungThaoTrang@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201207282', N'Nguyễn Văn', N'Vũ', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-08-17' AS Date), 1, NULL, N'0913098071', N'NguyenVanVu@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201207326', N'Trần Minh', N'Quang', N'7.48.02.01', N'K61.CNTT2', CAST(N'2000-01-16' AS Date), 1, N'Đội 8 - Tranh Xuyên - Đồng Tâm - Ninh Giang - Hải Dương ', N'0364518268', N'TranMinhQuang@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201210114', N'Trần Lan', N'Anh', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-09-06' AS Date), 0, N'Đội 8- Tranh Xuyên- Đồng Tâm- Ninh Giang- Hải Dương ', N'0364518268', N'TranLanAnh@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201210159', N'Nguyễn Quốc', N'Dũng', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-12-24' AS Date), 1, N'Thôn Nông Lục, Xã Hưng Vũ, huyện Bắc Sơn, tỉnh Lạng Sơn', N'0915649920', N'NguyenQuocDung@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201210176', N'Vũ Phạm Anh', N'Đức', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-03-08' AS Date), 1, N'62-đường Tân An-phố Tân An-phường Tân Thành-TP Ninh Bình', N'0919933413', N'VuPhamAnhDuc@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201210225', N'Lại Văn', N'Cương', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-02-11' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201210256', N'Chu Xuân', N'Hùng', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-11-11' AS Date), 1, NULL, N'0397189950', N'ChuXuonHung@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201210294', N'Ngô Trung', N'Kiên', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-08-15' AS Date), 1, N'Xóm 4, thôn Ngoại Hoàng, xã Lưu Hoàng, huyện Ứng Hoà, thành phố Hà Nội ', N'0961196480', N'NguTrungKion@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201210305', N'Tạ Quang', N'Lợi', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-12-21' AS Date), 1, N'Xóm 2 - Xuân Châu - Xuân Trường - Nam Định', N'0375139334', N'TaQuangLoi@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201210356', N'Lê Thị', N'Phương', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-10-08' AS Date), 1, N'Trung Chính-Hải Hoà-Nghi Sơn-Thanh Hoá', N'0912538138', N'LoThiPhuong@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201210407', N'Nguyễn Minh', N'Quý', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-12-04' AS Date), 1, N'10/82 ngõ Trung Tả, Phường Thổ Quan, Đống Đa, Hà Nội ', N'0983890905', N'NguyenMinhQuy@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201216824', N'Nguyễn Tuấn', N'Dương', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-11-04' AS Date), 1, N'Thôn 1 ,vân trình , thượng hoà , nho quan , ninh bình', N'0862357149', N'NguyenTuanDuong@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'201240414', N'Trần Hồng', N'Sơn', N'7.48.02.01', N'K61.CNTT2', CAST(N'2002-03-29' AS Date), 1, NULL, N'0903930392', N'TranHongSon@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212600556', N'Phạm Huy', N'Hoàng', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-07-09' AS Date), 1, N'số 10-ngõ 11- đường thôn 7-Yên Sở-Hoài Đức-Hà Nội', N'0969561907', N'PhamHuyHoang@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212601105', N'Lê Bùi Trường', N'Giang', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-10-14' AS Date), 1, N'Thôn Hiếu Thiện - Xã Vũ Hội - Huyện Vũ Thư - Tỉnh Thái Bình', N'0328709355', N'LoBuiTruongGiang@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212602500', N'Trần Đức', N'Hoàng', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-11-13' AS Date), 1, N'Số nhà 15, Tiểu khu 5, thị trấn Ít Ong, Mường La, Sơn La', N'0837361877', N'TranDucHoang@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212602565', N'Chu Thị Thu', N'Hà', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-08-10' AS Date), 0, N'Ngách 29/33 tổ 15 Hưng Phúc, phường Yên Sở, quận Hoàng Mai, Hà Nội.', N'0386839489', N'ChuThiThuHa@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212602719', N'Đào Quang', N'Tùng', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-02-07' AS Date), 1, N'A2712, KNOCC Viglacera TLNO1, Tòa, Cầu Giấy, Hà Nội', N'0904106878', N'DaoQuangTung@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212602930', N'Lê Trà', N'My', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-11-03' AS Date), 0, N'Thôn An Ấp, xã An Ấp, huyện Quỳnh Phụ, tỉnh Thái Bình', N'0975670766', N'LoTraMy@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212603983', N'Trần Hà', N'Vinh', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-10-23' AS Date), 1, N'Xóm Trù mét, xã Nghĩa Khánh, huyện Nghĩa Đàn, tỉnh Nghệ An', N'0327054171', N'TranHaVinh@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212604007', N'Lê Văn', N'Hải', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-12-22' AS Date), 1, N' Đồng Tiến - Hà Tiến - Hà Trung - Thanh Hoá', N'0983634115', N'LoVanHai@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212606012', N'Vũ Quang', N'Vinh', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-10-19' AS Date), 1, N'Số 77, đường Hàn Thượng, phường Bình Hàn, Thành phố Hải Dương, tỉnh Hải Dương', N'0585866191', N'VuQuangVinh@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212606014', N'Ngô Viết', N'Dũng', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-09-14' AS Date), 1, N'Thôn Ba- xã Tân Mỹ- TP Bắc Giang- Bắc Giang', N'0962766786', N'NguVietDung@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212606016', N'Bùi Đoàn Quang', N'Hưng', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-01-21' AS Date), 1, N'Nhà số 8 - ngõ 3 - xóm lý - thôn thanh thần - xã thanh cao-huyện thanh oai- hà nội', N'0983980202', N'BuiDoanQuangHung@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212606018', N'Nông Đức', N'Mạnh', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-10-04' AS Date), 1, N'Xóm 11 . Xã Hải Vân Huyện Hải Hậu Tỉnh Nam Định', N'0342673803', N'NungDucManh@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212610094', N'Đinh Thúy', N'Quỳnh', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-11-07' AS Date), 0, N'xóm Lâm Hoan - xã Giao Phong - huyện Giao Thuỷ - tỉnh Nam Định', N'0921426339', N'DinhThyyQuynh@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212610442', N'Nguyễn Mạnh', N'Hùng', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-11-07' AS Date), 1, N' Số 4 , Ngách 4 , Ngõ 8 , Đường Lê Trọng Tấn , Phường La Khê , Hà Đông , Hà Nội', N'0388369089', N'NguyenManhHung@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212610443', N'Vũ', N'Khánh', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-05-06' AS Date), 1, N'38,ngõ 2,tổ 1,Trưng Nhị, Phúc Yên, Vĩnh Phúc', N'0989357950', N'VuKhonh@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212610781', N'Hoàng Đình', N'Tùng', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-10-20' AS Date), 1, N'thôn An Khang, xã Yên Thạch, Sông Lô, Vĩnh Phúc', N'0983689634', N'HoangDonhTung@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212611377', N'Vũ Tuấn', N'Đạt', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-04-12' AS Date), 1, N'Thôn Chiêm- Liên Hiệp- Hưng Hà - Thái Bình ', N'0981791325', N'VuTuanDat@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212612471', N'Đồng Hà', N'Duy', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-01-16' AS Date), 1, N'Kim 3- xã Phượng Sơn- huyện Lục Ngạn- tỉnh Bắc Giang', N'0395238535', N'DongHaDuy@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212612929', N'Ngô Thị Hương', N'Giang', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-05-04' AS Date), 0, N'Xã Phú Lương huyện Đông Hưng tỉnh Thái Bình ', N'0332196131', N'NguThiHuongGiang@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212613775', N'Nguyễn Sơn', N'Anh', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-09-23' AS Date), 1, N'Lũng Kênh-Đức Giang-Hoài Đức-Hà Nội', N'0978871942', N'NguyenSonAnh@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212616010', N'Lê Xuân', N'Phúc', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-09-06' AS Date), 1, N'Thôn2 Đinh xá phủ lý Hà Nam', N'0387005637', N'LoXuonPhyc@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212630022', N'Chu Hải', N'Phong', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-11-28' AS Date), 1, N'Nhà số 8 -ngõ 3-xóm lý- thanh thần-thanh cao- thanh oai - hà nội', N'0382060143', N'ChuHaiPhong@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212630095', N'Trịnh Quang', N'Dương', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-10-28' AS Date), 1, N'Xóm 2- thôn Cụ Trì- xã Ngũ Hùng - huyện Thanh Miện- tỉnh Hải Dương', N'0975179251', N'TrinhQuangDuong@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212630184', N'Phạm Thanh', N'Hằng', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-08-19' AS Date), 0, N'Nhà số 7, ngách 197/12, TDP Trung 5, phường Tây Tựu, quận Bắc Từ Liêm, Hà Nội', N'0866438580', N'PhamThanhDuy@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212630895', N'Nguyễn Trung', N'Kiên', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-01-29' AS Date), 1, N'Số nhà 34 ngách 25 ngõ 1009 tdp giáp tiêu phường dĩnh kế thành phố bắc giang', N'0976937260', N'NguyenTrungKion@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212630972', N'Nguyễn Đắc', N'Phúc', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-12-06' AS Date), 1, N'Xóm Đông - xã Đồng Du - huyện Bình Lục - tỉnh Hà Nam', N'0388440736', N'NguyenDacPhyc@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212631446', N'Trịnh Đức', N'Lương', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-01-12' AS Date), 1, N'Địa chỉ :Minh Loan- xóm Trên- Cảnh Lâm- Tân việt- Yên mĩ- Hưng Yên', N'0988455749', N'TrinhDucLuong@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212632702', N'Nguyễn Thị Ninh', N'Giang', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-11-02' AS Date), 0, N'Khu 12, xã Vĩnh Lại, huyện Lâm Thao, tỉnh Phú Thọ', N'0398878408', N'NguyenThiNinhGiang@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212633564', N'Đỗ Đức', N'Thịnh', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-08-11' AS Date), 1, N'Thôn thành công - Xã Nhuế Dương -Huyện Khoái Châu - Tỉnh Hưng Yên', N'0353658184', N'DoDucThinh@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212633819', N'Trần Thế Anh', N'Dũng', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-10-05' AS Date), 1, N'Tam á gia đông thuânu thành bắc ninh', N'0385638359', N'TranTheAnhDung@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212633820', N'Nguyễn Trọng', N'Hưng', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-07-18' AS Date), 1, N'Tổ 13 cụm 4 phường đông khê- ngô quyền - hải phòng ', N'0834860039', N'NguyenTrongHung@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212633938', N'Nghiêm Quốc', N'Huy', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-02-09' AS Date), 1, N'Thôn Văn Mỹ, xã Hoàng Văn Thụ, huyện Chương Mỹ, TP.Hà Nội', N'0986177528', N'NghiomQuocHuy@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212634149', N'Trần Văn', N'Nhân', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-02-27' AS Date), 1, N'Ba hàng , thủ sĩ ,tiên lữ ,hưng yên', N'0973399912', N'TranVanNhon@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212640397', N'Kiều Kiên', N'Quyết', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-10-19' AS Date), 1, N'107- cụm6 - Tân Hội - Đan phượng - Hà Nội', N'0334841068', N'KieuKionQuyet@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212642248', N'Nguyễn Thị Hồng', N'Giang', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-06-02' AS Date), 0, N'số nhà 17 tổ 23 khu đô thị Trần Lãm thành phố Thái Bình', N'0333041194', N'NguyenThiHongGiang@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'212643488', N'Lý Xuân', N'Trường', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-07-17' AS Date), 1, N'Thôn Đông Duy - Yên Quang - Ý Yên - Nam Định', N'0346256817', N'LyXuonTruong@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'221230806', N'Đỗ Ngọc Trường', N'Sơn', NULL, N'K62.CTVA1', CAST(N'2002-10-03' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'223630711', N'Nguyễn Tiến', N'Mạnh', NULL, N'K61.DDT1', CAST(N'2002-09-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231210730', N'Vũ Tuấn', N'Phong', NULL, N'K61.CK1', CAST(N'2002-03-24' AS Date), 1, NULL, N'368754143', NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231210877', N'Đỗ Hữu', N'Toàn', NULL, N'K62.CTVA1', CAST(N'2002-09-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231210899', N'Nguyễn Đức', N'Trọng', NULL, N'K62.CK2', CAST(N'2002-04-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231220734', N'Phạm Danh', N'Phố', NULL, N'K62.CK2', CAST(N'2001-12-27' AS Date), 1, NULL, N'368654143', NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231220738', N'Bùi Huy', N'Phú', NULL, N'K61.DDT1', CAST(N'2002-07-12' AS Date), 1, NULL, N'917390319', NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231220893', N'Nguyễn Lâm', N'Tùng', NULL, N'K61.CT1', CAST(N'2002-04-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230703', N'Đinh Quang', N'Hưng', NULL, N'K61.CT1', CAST(N'2002-08-11' AS Date), 1, NULL, N'968809252', NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230714', N'Lê Bá', N'Hưng', NULL, N'K62.CTVA1', CAST(N'2002-10-03' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230716', N'Vũ Tuấn', N'Hưng', NULL, N'K61.CK1', CAST(N'2002-04-21' AS Date), 1, NULL, N'966090629', NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230724', N'Vũ Đình', N'Kiên', NULL, N'K62.CK2', CAST(N'2002-04-09' AS Date), 1, NULL, N'982129703', NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230725', N'Lê Xuân', N'Khánh', NULL, N'K61.DDT1', CAST(N'2002-10-24' AS Date), 1, NULL, N'385548856', NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230727', N'Nguyễn Ngọc Anh', N'Linh', NULL, N'K62.DDT2', CAST(N'2002-10-25' AS Date), 1, NULL, N'394049011', NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230729', N'Khúc Phương', N'Nam', NULL, N'K61.CT1', CAST(N'2002-01-18' AS Date), 1, NULL, N'914851967', NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230731', N'Trần Vệ', N'Nam', NULL, N'K62.CTVA1', CAST(N'2002-09-09' AS Date), 1, NULL, N'982791899', NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230744', N'Nguyễn Sĩ', N'Phúc', NULL, N'K62.DDT2', CAST(N'2002-01-11' AS Date), 1, NULL, N'397759286', NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230747', N'Đoàn Thái', N'Sơn', NULL, N'K61.CT1', CAST(N'2002-08-11' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230754', N'Lù Vũ Thái', N'Sơn', NULL, N'K61.CK1', CAST(N'2002-04-21' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230757', N'Trần Ngọc', N'Sơn', NULL, N'K62.CK2', CAST(N'2002-04-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230758', N'Trần Tiến', N'Sơn', NULL, N'K61.DDT1', CAST(N'2002-10-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230767', N'Vũ Việt', N'Tân', NULL, N'K62.DDT2', CAST(N'2002-10-25' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230776', N'Nguyễn Mạnh', N'Tiến', NULL, N'K61.CT1', CAST(N'2002-01-18' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230878', N'Phạm Quốc', N'Toản', NULL, N'K61.CK1', CAST(N'2002-03-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230879', N'Nguyễn Văn', N'Tú', NULL, N'K62.CK2', CAST(N'2002-08-11' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230882', N'Phạm Minh', N'Tuấn', NULL, N'K61.DDT1', CAST(N'2002-10-03' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230885', N'Hồ Việt', N'Tùng', NULL, N'K62.DDT2', CAST(N'2002-04-21' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230895', N'Đào Văn', N'Tuyến', NULL, N'K62.CTVA1', CAST(N'2002-10-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230900', N'Nguyễn Văn', N'Trưởng', NULL, N'K61.DDT1', CAST(N'2002-10-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230909', N'Phạm Văn', N'Trượng', NULL, N'K62.DDT2', CAST(N'2002-10-25' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230916', N'Nguyễn Đức Minh', N'Vũ', NULL, N'K62.CTVA1', CAST(N'2002-09-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230922', N'Nguyễn Khánh', N'Sơn', NULL, N'K61.CK1', CAST(N'2002-03-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230931', N'Phạm Văn', N'Tâm', NULL, N'K62.CK2', CAST(N'2001-12-27' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230940', N'Nguyễn Đức', N'Thạch', NULL, N'K61.CK1', CAST(N'2002-10-25' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230942', N'Ngô Thị', N'Thanh', NULL, N'K62.CK2', CAST(N'2002-01-18' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230943', N'Nguyễn Văn', N'Thanh', NULL, N'K61.DDT1', CAST(N'2002-09-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230947', N'Bùi Huy', N'Thành', NULL, N'K62.DDT2', CAST(N'2002-03-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230948', N'Đinh Xuân', N'Thành', NULL, N'K61.CT1', CAST(N'2002-08-11' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230954', N'Chu Nguyễn Khánh', N'Thiện', NULL, N'K62.CTVA1', CAST(N'2002-10-03' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230955', N'Lương Đức', N'Thoan', NULL, N'K61.CK1', CAST(N'2002-04-21' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230958', N'Doãn Đức', N'Toàn', NULL, N'K61.DDT1', CAST(N'2002-07-12' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231230959', N'Ngô Minh', N'Tuấn', NULL, N'K62.DDT2', CAST(N'2002-01-11' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'231234698', N'Phan Quốc', N'Uy', NULL, N'K61.CT1', CAST(N'2002-01-18' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'232611004', N'Trương Thị', N'Luyến', NULL, N'K62.CK2', CAST(N'2002-04-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'232630994', N'Nguyễn Hồng', N'Phú', NULL, N'K61.DDT1', CAST(N'2002-03-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'232631005', N'Nguyễn Đức', N'Mạnh', NULL, N'K61.DDT1', CAST(N'2002-10-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'232631006', N'Nguyễn Đức', N'Mạnh', NULL, N'K62.CK2', CAST(N'2002-04-21' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'232631009', N'Phạm Hồng', N'Minh', NULL, N'K61.DDT1', CAST(N'2002-04-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'232631010', N'Vũ Khánh', N'Minh', NULL, N'K62.CK2', CAST(N'2002-10-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'232631011', N'Đặng Phương', N'Nam', NULL, N'K61.DDT1', CAST(N'2002-10-25' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'232631019', N'Nguyễn Hoàng', N'Nam', NULL, N'K62.CK2', CAST(N'2002-01-18' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'232631020', N'Nguyễn Trọng', N'Nguyên', NULL, N'K62.CK2', CAST(N'2002-09-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233620650', N'Phạm Thái', N'Bình', NULL, N'K61.CT1', CAST(N'2002-08-11' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233620652', N'Đặng Thanh', N'Cao', NULL, N'K61.CK1', CAST(N'2002-04-21' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233620653', N'Bùi Thế', N'Duyệt', NULL, N'K62.CK2', CAST(N'2002-04-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233620654', N'Hoàng Tiến', N'Đạt', NULL, N'K61.DDT1', CAST(N'2002-10-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233620660', N'Phạm Thị Thu', N'Hằng', NULL, N'K61.DDT1', CAST(N'2002-07-12' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233620663', N'Nguyễn Phúc', N'Hiếu', NULL, N'K62.CTVA1', CAST(N'2002-10-03' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233620675', N'Hồ Nguyên', N'Long', NULL, N'K62.CTVA1', CAST(N'2002-10-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233620677', N'Lại Ngọc', N'Mai', NULL, N'K62.CK2', CAST(N'2002-01-18' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233620681', N'Phạm Anh', N'Phi', NULL, N'K61.CT1', CAST(N'2002-01-18' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630626', N'Nguyễn Hoàng', N'Tùng', NULL, N'K61.CT1', CAST(N'2002-08-11' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630627', N'Võ Hữu', N'Thái', NULL, N'K62.CTVA1', CAST(N'2002-10-03' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630628', N'Lê Xuân', N'Thành', NULL, N'K61.CK1', CAST(N'2002-04-21' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630629', N'Nguyễn Đông', N'Thành', NULL, N'K62.CK2', CAST(N'2002-04-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630631', N'Nguyễn Thị Phương', N'Thảo', NULL, N'K61.DDT1', CAST(N'2002-10-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630633', N'Đinh Khánh', N'Thiện', NULL, N'K62.DDT2', CAST(N'2002-10-25' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630634', N'Đinh Hải', N'Triều', NULL, N'K61.CT1', CAST(N'2002-01-18' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630635', N'Lò Văn', N'Trình', NULL, N'K62.CTVA1', CAST(N'2002-09-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630636', N'Nguyễn Đăng', N'Trung', NULL, N'K61.CK1', CAST(N'2002-03-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630637', N'Vũ Quốc', N'Trung', NULL, N'K62.CK2', CAST(N'2002-08-11' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630639', N'Đinh Văn', N'Trường', NULL, N'K61.DDT1', CAST(N'2002-10-03' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630640', N'Nguyễn Văn', N'Trường', NULL, N'K62.DDT2', CAST(N'2002-04-21' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630641', N'Trần Công', N'Việt', NULL, N'K61.CT1', CAST(N'2002-04-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630642', N'Lê Thanh', N'Vy', NULL, N'K62.CTVA1', CAST(N'2002-10-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630643', N'Đinh Thị Phương', N'Anh', NULL, N'K61.CK1', CAST(N'2002-10-25' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630645', N'Nguyễn Tuấn', N'Anh', NULL, N'K62.CK2', CAST(N'2002-01-18' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630646', N'Thạc Duy', N'Anh', NULL, N'K61.DDT1', CAST(N'2002-09-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630647', N'Trần Văn', N'Bắc', NULL, N'K62.DDT2', CAST(N'2002-03-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630649', N'Phạm Xuân', N'Bình', NULL, N'K62.CTVA1', CAST(N'2002-10-03' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630655', N'Nguyễn Hữu', N'Giáp', NULL, N'K61.CT1', CAST(N'2002-01-18' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630656', N'Trần Quang', N'Hải', NULL, N'K62.CTVA1', CAST(N'2002-09-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630657', N'Đinh Văn', N'Điệp', NULL, N'K62.DDT2', CAST(N'2002-10-25' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630658', N'Vũ Nguyễn Đức', N'Hải', NULL, N'K61.CK1', CAST(N'2002-03-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630659', N'Vũ Thanh', N'Hải', NULL, N'K62.CK2', CAST(N'2001-12-27' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630661', N'Đỗ Hoàng', N'Hiếu', NULL, N'K62.DDT2', CAST(N'2002-01-11' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630662', N'Nguyễn Đức', N'Hiếu', NULL, N'K61.CT1', CAST(N'2002-08-11' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630664', N'Phạm Minh', N'Hiếu', NULL, N'K61.CK1', CAST(N'2002-04-21' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630665', N'Trình Minh', N'Hiếu', NULL, N'K62.CK2', CAST(N'2002-04-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630666', N'Trần Văn', N'Hoan', NULL, N'K61.DDT1', CAST(N'2002-10-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630667', N'Bế Việt', N'Hoàng', NULL, N'K62.DDT2', CAST(N'2002-10-25' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630668', N'Nguyễn Việt', N'Hoàng', NULL, N'K61.CT1', CAST(N'2002-01-18' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630669', N'Nghiêm Thị', N'Huyền', NULL, N'K62.CTVA1', CAST(N'2002-09-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630670', N'Phạm Đình', N'Kiên', NULL, N'K61.CK1', CAST(N'2002-03-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630671', N'Trần Văn', N'Kiệt', NULL, N'K62.CK2', CAST(N'2002-08-11' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630672', N'Dương Đức', N'Khánh', NULL, N'K61.DDT1', CAST(N'2002-10-03' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630673', N'Vũ Đăng', N'Khoa', NULL, N'K62.DDT2', CAST(N'2002-04-21' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630674', N'Đỗ Hồng', N'Khôi', NULL, N'K61.CT1', CAST(N'2002-04-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630676', N'Ngô Xuân', N'Lộc', NULL, N'K61.CK1', CAST(N'2002-10-25' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630678', N'Đỗ Hoàng', N'Minh', NULL, N'K62.DDT2', CAST(N'2002-03-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630679', N'Hoàng Bá', N'Ngọc', NULL, N'K61.DDT1', CAST(N'2002-10-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630680', N'Nguyễn Long', N'Nhật', NULL, N'K62.DDT2', CAST(N'2002-10-25' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630683', N'Trần Quang', N'Phúc', NULL, N'K62.CTVA1', CAST(N'2002-09-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630684', N'Trịnh Tiến', N'Đạt', NULL, N'K61.CK1', CAST(N'2002-03-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630685', N'Nguyễn Anh', N'Đức', NULL, N'K62.CK2', CAST(N'2001-12-27' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630687', N'Nguyễn Trung', N'Đức', NULL, N'K61.DDT1', CAST(N'2002-07-12' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630688', N'Lại Trường', N'Giang', NULL, N'K62.DDT2', CAST(N'2002-01-11' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630689', N'Hà Văn', N'Hải', NULL, N'K61.CT1', CAST(N'2002-08-11' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630690', N'Vũ Quang', N'Minh', NULL, N'K61.CT1', CAST(N'2002-08-11' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630691', N'Dương Xuân', N'Nam', NULL, N'K62.CTVA1', CAST(N'2002-10-03' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630692', N'Nguyễn Hải', N'Nam', NULL, N'K61.CK1', CAST(N'2002-04-21' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630693', N'Phạm Duy', N'Nghĩa', NULL, N'K62.CK2', CAST(N'2002-04-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630694', N'Hoàng Đình', N'Hào', NULL, N'K62.CTVA1', CAST(N'2002-10-03' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630695', N'Vũ Thị Thanh', N'Hằng', NULL, N'K61.CK1', CAST(N'2002-04-21' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630696', N'Đàm Trọng', N'Hiển', NULL, N'K62.CK2', CAST(N'2002-04-09' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630697', N'Đồng Minh', N'Hiếu', NULL, N'K61.DDT1', CAST(N'2002-10-24' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'233630699', N'Nguyễn Hải', N'Hiếu', NULL, N'K62.DDT2', CAST(N'2002-10-25' AS Date), 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'882126003', N'Nguyễn Phú Thiện', N'Nhân', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-09-15' AS Date), 1, N'Đội 2 - vĩnh khúc - văn giang - hunge yên', N'0961356098', N'NguyenPhyThienNhon@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'882126009', N'Lê Đạt', N'Quý', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2002-12-24' AS Date), 1, N'Khu 5 đỗ xuyên thanh ba phú thọ', N'0836513357', N'LoDatQuy@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'882126013', N'Vũ Phúc', N'Thành', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-04-07' AS Date), 1, N'số nhà 89 - thôn 3 - xã Yên Sở - huyện Hoài Đức - thành phố Hà Nội', N'0384926612', N'VuPhycThanh@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'882126017', N'Nguyễn Thị Vân', N'Anh', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-10-07' AS Date), 0, N'Nhà số 6 tổ 18 khu 3 Cửa Ông Cẩm Phả Quảng Ninh', N'0362817808', N'NguyenThiVonAnh@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'882126030', N'Nông Bảo', N'Khanh', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-06-19' AS Date), 1, N'Số nhà 301- Tổ 10- Khu Nhuệ Hổ- Phường Kim Sơn- Thị xã Đông Triều- Tỉnh Quảng Ninh', N'0962411665', N'NungBaoKhanh@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'882126033', N'Trần Nguyễn Nam', N'Long', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2003-08-23' AS Date), 1, N'Khôn Thôn - Minh Cường - Thường Tín - Hà Nội', N'0359726417', N'TranNguyenNamLong@utc.edu.vn', NULL)
GO
INSERT [dbo].[SinhVien] ([MaSinhVien], [HoDem], [Ten], [MaCTDT], [MaLop], [NgaySinh], [GioiTinh], [DiaChi], [DienThoai], [Email], [TinhTrang]) VALUES (N'991790002', N'Trần Khoa', N'Trường', N'7.48.02.01QT(IE3)', N'K62.CNTTVA2', CAST(N'2001-11-17' AS Date), 1, NULL, N'0386', N'TranKhoaTruong@utc.edu.vn', NULL)
GO
ALTER TABLE [dbo].[BoMon]  WITH CHECK ADD  CONSTRAINT [FK_BoMon_Khoa] FOREIGN KEY([MaKhoa])
REFERENCES [dbo].[Khoa] ([MaKhoa])
GO
ALTER TABLE [dbo].[BoMon] CHECK CONSTRAINT [FK_BoMon_Khoa]
GO
ALTER TABLE [dbo].[ChuongTrinhDaoTao]  WITH CHECK ADD  CONSTRAINT [FK_ChuongTrinhDaoTao_Khoa] FOREIGN KEY([MaKhoa])
REFERENCES [dbo].[Khoa] ([MaKhoa])
GO
ALTER TABLE [dbo].[ChuongTrinhDaoTao] CHECK CONSTRAINT [FK_ChuongTrinhDaoTao_Khoa]
GO
ALTER TABLE [dbo].[CTDT_HocPhan]  WITH CHECK ADD  CONSTRAINT [FK_CTDT_HocPhan_ChuongTrinhDaoTao] FOREIGN KEY([MaCTDT])
REFERENCES [dbo].[ChuongTrinhDaoTao] ([MaCTDT])
GO
ALTER TABLE [dbo].[CTDT_HocPhan] CHECK CONSTRAINT [FK_CTDT_HocPhan_ChuongTrinhDaoTao]
GO
ALTER TABLE [dbo].[CTDT_HocPhan]  WITH CHECK ADD  CONSTRAINT [FK_CTDT_HocPhan_HocPhan] FOREIGN KEY([MaHocPhan])
REFERENCES [dbo].[HocPhan] ([MaHocPhan])
GO
ALTER TABLE [dbo].[CTDT_HocPhan] CHECK CONSTRAINT [FK_CTDT_HocPhan_HocPhan]
GO
ALTER TABLE [dbo].[GiangVien]  WITH CHECK ADD  CONSTRAINT [FK_GiangVien_BoMon] FOREIGN KEY([MaBoMon])
REFERENCES [dbo].[BoMon] ([MaBoMon])
GO
ALTER TABLE [dbo].[GiangVien] CHECK CONSTRAINT [FK_GiangVien_BoMon]
GO
ALTER TABLE [dbo].[HocPhan]  WITH CHECK ADD  CONSTRAINT [FK_HocPhan_BoMon] FOREIGN KEY([MaBoMon])
REFERENCES [dbo].[BoMon] ([MaBoMon])
GO
ALTER TABLE [dbo].[HocPhan] CHECK CONSTRAINT [FK_HocPhan_BoMon]
GO
ALTER TABLE [dbo].[HocPhanTienQuyet]  WITH CHECK ADD  CONSTRAINT [FK_HocPhanTienQuyet_HocPhan] FOREIGN KEY([MaHocPhan])
REFERENCES [dbo].[HocPhan] ([MaHocPhan])
GO
ALTER TABLE [dbo].[HocPhanTienQuyet] CHECK CONSTRAINT [FK_HocPhanTienQuyet_HocPhan]
GO
ALTER TABLE [dbo].[HocPhanTienQuyet]  WITH CHECK ADD  CONSTRAINT [FK_HocPhanTienQuyet_HocPhan1] FOREIGN KEY([MaHocPhanTienQuyet])
REFERENCES [dbo].[HocPhan] ([MaHocPhan])
GO
ALTER TABLE [dbo].[HocPhanTienQuyet] CHECK CONSTRAINT [FK_HocPhanTienQuyet_HocPhan1]
GO
ALTER TABLE [dbo].[Lop]  WITH CHECK ADD  CONSTRAINT [FK_Lop_Khoa] FOREIGN KEY([MaKhoa])
REFERENCES [dbo].[Khoa] ([MaKhoa])
GO
ALTER TABLE [dbo].[Lop] CHECK CONSTRAINT [FK_Lop_Khoa]
GO
ALTER TABLE [dbo].[LopHocPhan]  WITH CHECK ADD  CONSTRAINT [FK_LopHocPhan_GiangVien] FOREIGN KEY([MaGiangVien])
REFERENCES [dbo].[GiangVien] ([MaGiangVien])
GO
ALTER TABLE [dbo].[LopHocPhan] CHECK CONSTRAINT [FK_LopHocPhan_GiangVien]
GO
ALTER TABLE [dbo].[LopHocPhan]  WITH CHECK ADD  CONSTRAINT [FK_LopHocPhan_HocPhan] FOREIGN KEY([MaHocPhan])
REFERENCES [dbo].[HocPhan] ([MaHocPhan])
GO
ALTER TABLE [dbo].[LopHocPhan] CHECK CONSTRAINT [FK_LopHocPhan_HocPhan]
GO
ALTER TABLE [dbo].[LopHocPhan_SinhVien]  WITH CHECK ADD  CONSTRAINT [FK_LopHocPhan_SinhVien_LopHocPhan] FOREIGN KEY([MaHocPhan], [MaLopHocPhan])
REFERENCES [dbo].[LopHocPhan] ([MaHocPhan], [MaLopHocPhan])
GO
ALTER TABLE [dbo].[LopHocPhan_SinhVien] CHECK CONSTRAINT [FK_LopHocPhan_SinhVien_LopHocPhan]
GO
ALTER TABLE [dbo].[LopHocPhan_SinhVien]  WITH CHECK ADD  CONSTRAINT [FK_LopHocPhan_SinhVien_SinhVien] FOREIGN KEY([MaSinhVien])
REFERENCES [dbo].[SinhVien] ([MaSinhVien])
GO
ALTER TABLE [dbo].[LopHocPhan_SinhVien] CHECK CONSTRAINT [FK_LopHocPhan_SinhVien_SinhVien]
GO
ALTER TABLE [dbo].[SinhVien]  WITH CHECK ADD  CONSTRAINT [FK_SinhVien_ChuongTrinhDaoTao] FOREIGN KEY([MaCTDT])
REFERENCES [dbo].[ChuongTrinhDaoTao] ([MaCTDT])
GO
ALTER TABLE [dbo].[SinhVien] CHECK CONSTRAINT [FK_SinhVien_ChuongTrinhDaoTao]
GO
ALTER TABLE [dbo].[SinhVien]  WITH CHECK ADD  CONSTRAINT [FK_SinhVien_Lop] FOREIGN KEY([MaLop])
REFERENCES [dbo].[Lop] ([MaLop])
GO
ALTER TABLE [dbo].[SinhVien] CHECK CONSTRAINT [FK_SinhVien_Lop]
GO
