-- CREATE DATABASE QLThuVien
-- GO
-- USE QLThuVien

-- (Tuỳ chọn) Xoá các bảng nếu đã tồn tại (dễ chạy lại khi dev)
IF OBJECT_ID('dbo.tViPham') IS NOT NULL DROP TABLE dbo.tViPham;
IF OBJECT_ID('dbo.tGiaoDich_BanSao') IS NOT NULL DROP TABLE dbo.tGiaoDich_BanSao;
IF OBJECT_ID('dbo.tGiaoDichMuonTra') IS NOT NULL DROP TABLE dbo.tGiaoDichMuonTra;
IF OBJECT_ID('dbo.tTheBanDoc') IS NOT NULL DROP TABLE dbo.tTheBanDoc;
IF OBJECT_ID('dbo.tBanDoc') IS NOT NULL DROP TABLE dbo.tBanDoc;
IF OBJECT_ID('dbo.tBanSao') IS NOT NULL DROP TABLE dbo.tBanSao;
IF OBJECT_ID('dbo.tTaiLieu_TacGia') IS NOT NULL DROP TABLE dbo.tTaiLieu_TacGia;
IF OBJECT_ID('dbo.tTaiLieu') IS NOT NULL DROP TABLE dbo.tTaiLieu;
IF OBJECT_ID('dbo.tTaiKhoan') IS NOT NULL DROP TABLE dbo.tTaiKhoan;
IF OBJECT_ID('dbo.tNhanVien') IS NOT NULL DROP TABLE dbo.tNhanVien;
IF OBJECT_ID('dbo.tVaiTro') IS NOT NULL DROP TABLE dbo.tVaiTro;
IF OBJECT_ID('dbo.tTacGia') IS NOT NULL DROP TABLE dbo.tTacGia;
IF OBJECT_ID('dbo.tNhaXuatBan') IS NOT NULL DROP TABLE dbo.tNhaXuatBan;
IF OBJECT_ID('dbo.tNgonNgu') IS NOT NULL DROP TABLE dbo.tNgonNgu;
IF OBJECT_ID('dbo.tTheLoai') IS NOT NULL DROP TABLE dbo.tTheLoai;
IF OBJECT_ID('dbo.tDinhDang') IS NOT NULL DROP TABLE dbo.tDinhDang;
IF OBJECT_ID('dbo.tQuocGia') IS NOT NULL DROP TABLE dbo.tQuocGia;
GO

-- =========================================
-- BẢNG QUỐC GIA
-- =========================================

CREATE TABLE dbo.tQuocGia (
    MaQG CHAR(2) NOT NULL PRIMARY KEY, -- ISO Alpha-2
    TenQG NVARCHAR(100) NOT NULL
);
GO

INSERT INTO tQuocGia (MaQG, TenQG) VALUES
('VN', N'Việt Nam'),
('US', N'Hoa Kỳ'),
('CN', N'Trung Quốc'),
('JP', N'Nhật Bản'),
('KR', N'Hàn Quốc'),
('FR', N'Pháp'),
('DE', N'Đức'),
('GB', N'Vương quốc Anh'),
('IT', N'Ý'),
('ES', N'Tây Ban Nha'),
('RU', N'Nga'),
('CA', N'Canada'),
('AU', N'Úc'),
('IN', N'Ấn Độ'),
('ID', N'Indonesia'),
('TH', N'Thái Lan'),
('PH', N'Philippines'),
('MY', N'Malaysia'),
('SG', N'Singapore'),
('LA', N'Lào'),
('KH', N'Campuchia'),
('MM', N'Myanmar'),
('BR', N'Braxin'),
('AR', N'Argentina'),
('MX', N'Mexico'),
('ZA', N'Nam Phi'),
('EG', N'Ai Cập'),
('TR', N'Thổ Nhĩ Kỳ'),
('SA', N'Ả Rập Xê Út'),
('AE', N'Các Tiểu vương quốc Ả Rập Thống nhất'),
('PK', N'Pakistan'),
('BD', N'Bangladesh'),
('NP', N'Nepal'),
('CH', N'Thụy Sĩ'),
('SE', N'Thụy Điển'),
('NO', N'Na Uy'),
('FI', N'Phần Lan'),
('DK', N'Đan Mạch'),
('NL', N'Hà Lan'),
('BE', N'Bỉ'),
('PL', N'Ba Lan'),
('CZ', N'Cộng hòa Séc'),
('AT', N'Áo'),
('HU', N'Hungary'),
('GR', N'Hy Lạp'),
('PT', N'Bồ Đào Nha'),
('IE', N'Ai-len'),
('IS', N'Iceland'),  
('RO', N'Rumani'),
('BG', N'Bungari'),
('UA', N'Ukraina'),
('BY', N'Belarus'),
('KZ', N'Kazakhstan'),
('UZ', N'Uzbekistan'),
('IR', N'Iran'),
('IQ', N'Iraq'),
('IL', N'Israel'),
('JO', N'Jordan'),
('QA', N'Qatar'),
('KW', N'Kuwait'),
('OM', N'Oman'),
('YE', N'Yemen'),
('SY', N'Syria'),
('LB', N'Liban'),
('AF', N'Afghanistan'),
('LK', N'Sri Lanka'),
('MV', N'Maldives'),
('BT', N'Bhutan'),
('MN', N'Mông Cổ'),
('HK', N'Hồng Kông'),
('TW', N'Đài Loan'),
('NZ', N'New Zealand'),
('PG', N'Papua New Guinea'),
('FJ', N'Fiji'),
('SB', N'Solomon'),
('VU', N'Vanuatu'),
('TO', N'Tonga'),
('WS', N'Samoa'),
('CL', N'Chile'),
('CO', N'Colombia'),
('PE', N'Peru'),
('VE', N'Venezuela'),
('CU', N'Cuba'),
('DO', N'Cộng hòa Dominica'),
('JM', N'Jamaica'),
('HT', N'Haiti'),
('CR', N'Costa Rica'),
('PA', N'Panama'),
('GT', N'Guatemala'),
('HN', N'Honduras'),
('SV', N'El Salvador'),
('NI', N'Nicaragua'),
('UY', N'Uruguay'),
('PY', N'Paraguay'),
('EC', N'Ecuador'),
('BO', N'Bolivia'),
('MA', N'Maroc'),
('DZ', N'Algérie'),
('TN', N'Tunisia'),
('NG', N'Nigeria'),
('KE', N'Kenya'),
('TZ', N'Tanzania'),
('UG', N'Uganda'),
('ET', N'Ethiopia');
GO

-- =========================================
-- BẢNG NGÔN NGỮ
-- =========================================

CREATE TABLE dbo.tNgonNgu (
    MaNN CHAR(2) NOT NULL PRIMARY KEY,           
    TenNN NVARCHAR(50) NOT NULL UNIQUE         
);
GO

INSERT INTO dbo.tNgonNgu (MaNN, TenNN)
VALUES
('VI', N'Tiếng Việt'),
('EN', N'Tiếng Anh'),
('FR', N'Tiếng Pháp'),
('ZH', N'Tiếng Trung'),
('JA', N'Tiếng Nhật'),
('KO', N'Tiếng Hàn'),
('DE', N'Tiếng Đức'),
('RU', N'Tiếng Nga'),
('BI', N'Song ngữ'),
('OT', N'Khác');
GO

CREATE TABLE dbo.tTheLoai (
    MaThL CHAR(6) NOT NULL PRIMARY KEY,
    TenThL NVARCHAR(50) NOT NULL UNIQUE
);
GO

-- =========================================
-- BẢNG THỂ LOẠI
-- =========================================

INSERT INTO dbo.tTheLoai (MaThL, TenThL)
VALUES
('THL001', N'Sách giáo trình'),
('THL002', N'Sách tham khảo'),
('THL003', N'Luận văn'),
('THL004', N'Luận án'),
('THL005', N'Đề tài nghiên cứu khoa học'),
('THL006', N'Báo'),
('THL007', N'Tạp chí'),
('THL008', N'Kỷ yếu hội thảo'),
('THL009', N'Tài liệu nội bộ'),
('THL010', N'Tài liệu điện tử');
GO

-- =========================================
-- BẢNG ĐỊNH DẠNG TÀI LIỆU
-- =========================================
CREATE TABLE dbo.tDinhDang (
    MaDD CHAR(5) NOT NULL PRIMARY KEY,          -- Mã định dạng: DD001, DD002, ...
    TenDD NVARCHAR(50) NOT NULL UNIQUE          -- Tên định dạng tài liệu
);
GO

INSERT INTO dbo.tDinhDang (MaDD, TenDD)
VALUES
('DD001', N'Sách in'),
('DD002', N'Luận văn / Luận án bản in'),
('DD003', N'PDF / Ebook'),
('DD004', N'Tài liệu Word (DOC/DOCX)'),
('DD005', N'CD / DVD'),
('DD006', N'Video học thuật'),
('DD007', N'Âm thanh (Audio / Podcast)'),
('DD008', N'Trang web / Online resource'),
('DD009', N'Bản đồ / Atlas'),
('DD010', N'Khác');
GO

-- =========================================
-- BẢNG NHÀ XUẤT BẢN
-- =========================================
CREATE TABLE dbo.tNhaXuatBan (
    MaNXB CHAR(9) NOT NULL PRIMARY KEY,             -- Mã NXB: NXB{QG}-{STT} (VD: NXBVN-001)
    MaQG CHAR(2) NOT NULL,                          -- Mã quốc gia (tham chiếu tQuocGia)
    TenNXB NVARCHAR(100) NOT NULL UNIQUE,           -- Tên nhà xuất bản
    CONSTRAINT FK_NXB_QuocGia FOREIGN KEY (MaQG) REFERENCES dbo.tQuocGia(MaQG)
);
GO

INSERT INTO dbo.tNhaXuatBan (MaNXB, MaQG, TenNXB)
VALUES
-- Việt Nam
('NXBVN-001', 'VN', N'NXB Giao thông vận tải'),
('NXBVN-002', 'VN', N'NXB Giáo dục'),
('NXBVN-003', 'VN', N'NXB Trẻ'),
('NXBVN-004', 'VN', N'NXB Kim Đồng'),
('NXBVN-005', 'VN', N'NXB Đại học Quốc gia Hà Nội'),
('NXBVN-006', 'VN', N'NXB Tổng hợp TP.HCM'),
('NXBVN-007', 'VN', N'NXB Tri thức'),
('NXBVN-008', 'VN', N'NXB Đại học Cần Thơ'),

-- Nước ngoài (ví dụ minh họa)
('NXBGB-001', 'GB', N'Penguin Random House'),
('NXBUS-001', 'US', N'Oxford University Press'),
('NXBFR-001', 'FR', N'Hachette Livre'),
('NXBJP-001', 'JP', N'Kodansha Ltd.'),
('NXBCN-001', 'CN', N'Tsinghua University Press'),
('NXBKR-001', 'KR', N'Seoul Selection'),
('NXBRU-001', 'RU', N'Prosveshcheniye Publishing House');
GO


-- =========================================
-- BẢNG TÁC GIẢ
-- =========================================
CREATE TABLE dbo.tTacGia (
    MaTG CHAR(10) NOT NULL PRIMARY KEY,             -- Mã tác giả: TG + mã quốc gia + 2 chữ số năm + STT
    MaQG CHAR(2) NOT NULL,                          -- Mã quốc gia (tham chiếu tQuocGia)
    HoDem NVARCHAR(50) NOT NULL,                    -- Họ đệm hoặc học vị
    Ten NVARCHAR(30) NOT NULL,                      -- Tên tác giả
    CONSTRAINT FK_TacGia_QuocGia FOREIGN KEY (MaQG) REFERENCES dbo.tQuocGia(MaQG)
);
GO

INSERT INTO dbo.tTacGia (MaTG, MaQG, HoDem, Ten)
VALUES
-- Tác giả Việt Nam
('TGVN25-001', 'VN', N'PGS.TS Trần Thị Ái', N'Cẩm'),
('TGVN25-002', 'VN', N'ThS Đỗ Thùy', N'Trinh'),
('TGVN25-003', 'VN', N'PGS.TS Nguyễn Hồng', N'Sơn'),
('TGVN25-004', 'VN', N'TS Nguyễn Hồng', N'Thái'),
('TGVN25-005', 'VN', N'TS Nguyễn Đức', N'Dư'),
('TGVN25-006', 'VN', N'TS Hoàng Văn', N'Thông'),
('TGVN25-007', 'VN', N'TS Lương Thái', N'Lê'),
('TGVN25-008', 'VN', N'Lê Tiến', N'Dũng'),
('TGVN25-009', 'VN', N'ThS Phạm Xuân', N'Tích'),
('TGVN25-010', 'VN', N'PGS.TS Nguyễn Văn', N'Long'),
('TGVN25-011', 'VN', N'GS.TS Đỗ Đức', N'Tuấn'),
('TGVN25-012', 'VN', N'GS.TSKH Nguyễn Hữu Việt', N'Hưng'),
('TGVN25-013', 'VN', N'PGS.TS Vũ Đình', N'Lai'),
('TGVN25-014', 'VN', N'TS Nguyễn Xuân', N'Lựu'),
('TGVN25-015', 'VN', N'TS Bùi Đình', N'Nghi'),
('TGVN25-016', 'VN', N'Tô', N'Hoài'),
('TGVN25-017', 'VN', N'Nguyễn Thị Tuyết', N'Trinh'),
('TGVN25-018', 'VN', N'PGS.TS Nguyễn Ngọc', N'Long'),
('TGVN25-019', 'VN', N'PGS.TS Nguyễn Văn', N'Tám'),
('TGVN25-020', 'VN', N'PGS.TS Trần Tuấn', N'Hiệp'),
('TGVN25-021', 'VN', N'Hạnh', N'Nguyên'),
('TGVN25-022', 'VN', N'PGS.TS Nguyễn Thị Thanh', N'Huế'),
('TGVN25-023', 'VN', N'Phạm Công', N'Luận'),

-- Tác giả nước ngoài (Anh, Mỹ, Nga, Ai Cập, ... minh họa)
('TGUS25-001', 'US', N'GS David Emory', N'Shi'),
('TGUS25-002', 'US', N'Dale', N'Carnegie'),
('TGIL25-001', 'IL', N'GS.TS Yuval Noah', N'Harari'),
('TGEG25-001', 'EG', N'GS Ahmed El', N'Hakeem'),
('TGEG25-002', 'EG', N'GS Mostafa H.', N'Ammar'),
('TGEG25-003', 'EG', N'GS Tarek N.', N'Saadawi'),
('TGGB25-001', 'GB', N'GS George Brown', N'Tindall'),
('TGRU25-001', 'RU', N'TSKHKT V. F.', N'Regionov'),
('TGRU25-002', 'RU', N'TSKHKT B. B.', N'Fitterman');
GO

-- =========================================
-- BẢNG VAI TRÒ NGƯỜI DÙNG
-- =========================================
CREATE TABLE dbo.tVaiTro (
    MaVT CHAR(3) NOT NULL PRIMARY KEY,          -- Mã vai trò: 3 ký tự (VD: QTV, QLB, QLM, QLT)
    TenVT NVARCHAR(50) NOT NULL UNIQUE          -- Tên vai trò (duy nhất)
);
GO

INSERT INTO dbo.tVaiTro (MaVT, TenVT)
VALUES
('QTV', N'Quản trị viên'),
('QLB', N'Quản lý bạn đọc'),
('QLM', N'Quản lý mượn trả'),
('QLT', N'Quản lý tài liệu');
GO

-- =========================================
-- BẢNG NHÂN VIÊN
-- =========================================
CREATE TABLE dbo.tNhanVien (
    MaNV CHAR(7) NOT NULL PRIMARY KEY, -- Ví dụ: NV25-01
    HoDem NVARCHAR(50) NOT NULL,
    Ten NVARCHAR(30) NOT NULL,
    NgaySinh DATE NOT NULL,
    GioiTinh CHAR(1) NOT NULL CHECK (GioiTinh IN ('M', 'F')),
    DiaChi NVARCHAR(200) NULL,
    SDT NVARCHAR(20) NOT NULL,
    Email NVARCHAR(200) NOT NULL,
    PhuTrach NVARCHAR(100) NULL
);
GO

INSERT INTO tNhanVien (MaNV, HoDem, Ten, NgaySinh, GioiTinh, DiaChi, SDT, Email, PhuTrach)
VALUES
('NV25-01', N'Nguyễn Đức', N'Dư', '1988-09-25', 'M', N'Hà Nội', '0912363245', 'nducdu@utc.edu.vn', N'Giám đốc'),
('NV25-02', N'Nguyễn Thanh', N'Thủy', '1985-09-27', 'F', N'Hà Nội', '0912424955', 'nthanhthuy@utc.edu.vn', N'Phó Giám đốc'),
('NV25-03', N'Nguyễn Thị', N'Hòa', '1989-07-31', 'F', N'Hà Nội', '0327902799', 'nthhoa@utc.edu.vn', N'Phòng Nghiệp Vụ'),
('NV25-04', N'Trần Thị Thu', N'Hương', '1987-08-16', 'F', N'Tuyên Quang', '0331356271', 'tthhuong@utc.edu.vn', N'Phòng Nghiệp Vụ'),
('NV25-05', N'Bùi Thị Yến', N'Hường', '1988-03-12', 'F', N'Thái Nguyên', '0897878469', 'btyhuong@utc.edu.vn', N'Phòng Nghiệp Vụ'),
('NV25-06', N'Phạm Thị Thúy', N'Nga', '1998-05-08', 'F', N'Hà Nội', '0786561875', 'pttnga@utc.edu.vn', N'Phòng đọc Luận văn, Luận Án, Đề tài NCKH, Báo, Tạp chí'),
('NV25-07', N'Phạm Ngọc Thanh', N'Quang', '1996-03-12', 'M', N'Hà Nội', '0388577967', 'pnquang@utc.edu.vn', N'Phòng đọc Luận văn, Luận Án, Đề tài NCKH, Báo, Tạp chí'),
('NV25-08', N'Nguyễn Thị Hồng', N'Khoa', '1982-10-13', 'F', N'Lào Cai', '0972936189', 'nthkhoa@utc.edu.vn', N'Phòng đọc Luận văn, Luận Án, Đề tài NCKH, Báo, Tạp chí'),
('NV25-09', N'Phạm Thiên', N'Thu', '1999-04-24', 'F', N'Hà Nội', '0794854606', 'pthu@utc.edu.vn', N'Phòng Mượn'),
('NV25-10', N'Vũ Thị Hà', N'Vân', '1996-05-06', 'F', N'Ninh Bình', '0875306860', 'vthvan@utc.edu.vn', N'Phòng Mượn'),
('NV25-11', N'Châu Mạnh', N'Quang', '2000-03-29', 'M', N'Phú Thọ', '0829773751', 'cmquang@utc.edu.vn', N'Phòng đọc Tiếng Việt'),
('NV25-12', N'Cù Việt', N'Hằng', '1995-05-09', 'F', N'Hà Nội', '0787795092', 'cvhang@utc.edu.vn', N'Phòng đọc Tiếng Việt'),
('NV25-13', N'Kim Thị', N'Hoa', '1995-03-06', 'F', N'Quảng Ninh', '0376054504', 'kthhoa@utc.edu.vn', N'Nhà sách');
GO

-- =========================================
-- BẢNG TÀI KHOẢN
-- =========================================
CREATE TABLE dbo.tTaiKhoan (
    MaTK CHAR(7) NOT NULL PRIMARY KEY, -- Ví dụ: TK25-01
    MaNV CHAR(7) NOT NULL,
    MaVT CHAR(3) NOT NULL,
    TenDangNhap NVARCHAR(255) NOT NULL UNIQUE,
    MatKhau NVARCHAR(255) NOT NULL,
    TrangThai NVARCHAR(30) NOT NULL DEFAULT(N'Hoạt động'),
    NgayTao DATE NOT NULL DEFAULT (CONVERT(date, GETDATE())),
    CONSTRAINT FK_TaiKhoan_NhanVien FOREIGN KEY (MaNV) REFERENCES dbo.tNhanVien(MaNV),
    CONSTRAINT FK_TaiKhoan_VaiTro FOREIGN KEY (MaVT) REFERENCES dbo.tVaiTro(MaVT)
);
GO

UPDATE tTaiKhoan
SET MatKhau = '@utc123456'
WHERE MaTK = 'TK25-01';


INSERT INTO tTaiKhoan (MaTK, MaNV, MaVT, TenDangNhap, MatKhau, TrangThai, NgayTao)
VALUES
-- Quản trị viên
('TK25-01', 'NV25-01', 'QTV', 'nducdu@utc.edu.vn', '@utc123456', N'Hoạt động', '2022-05-12'),

-- Quản lý bạn đọc
('TK25-02', 'NV25-02', 'QLB', 'nthanhthuy@utc.edu.vn', '@utc123456', N'Hoạt động', '2022-05-12'),

-- Quản lý tài liệu (Phòng nghiệp vụ)
('TK25-03', 'NV25-03', 'QLT', 'nthhoa@utc.edu.vn', '@utc123456', N'Hoạt động', '2022-05-12'),
('TK25-04', 'NV25-04', 'QLT', 'tthhuong@utc.edu.vn', '@utc123456', N'Hoạt động', '2023-10-24'),
('TK25-05', 'NV25-05', 'QLT', 'btyhuong@utc.edu.vn', '@utc123456', N'Hoạt động', '2023-11-02'),

-- Quản lý tài liệu (Phòng đọc luận văn, đề tài, báo, tạp chí)
('TK25-06', 'NV25-06', 'QLT', 'pttnga@utc.edu.vn', '@utc123456', N'Hoạt động', '2022-05-12'),
('TK25-07', 'NV25-07', 'QLT', 'pnquang@utc.edu.vn', '@utc123456', N'Hoạt động', '2024-03-29'),
('TK25-08', 'NV25-08', 'QLT', 'nthkhoa@utc.edu.vn', '@utc123456', N'Hoạt động', '2024-03-29'),

-- Quản lý mượn trả
('TK25-09', 'NV25-09', 'QLM', 'pthu@utc.edu.vn', '@utc123456', N'Hoạt động', '2022-05-12'),
('TK25-10', 'NV25-10', 'QLM', 'vthvan@utc.edu.vn', '@utc123456', N'Hoạt động', '2022-06-01'),

-- Quản lý bạn đọc (Phòng đọc tiếng Việt)
('TK25-11', 'NV25-11', 'QLB', 'cmquang@utc.edu.vn', '@utc123456', N'Hoạt động', '2022-05-12'),
('TK25-12', 'NV25-12', 'QLB', 'cvhang@utc.edu.vn', '@utc123456', N'Hoạt động', '2025-08-14'),

-- Nhà sách
('TK25-13', 'NV25-13', 'QLT', 'kthhoa@utc.edu.vn', '@utc123456', N'Hoạt động', '2022-05-12');
GO

-- =========================================
-- BẢNG TÀI LIỆU
-- =========================================
CREATE TABLE dbo.tTaiLieu (
    MaTL CHAR(10) NOT NULL PRIMARY KEY,         -- Ví dụ: TLVN25-001
    MaNXB CHAR(9) NOT NULL,                     -- Tham chiếu NXB: NXBVN-001, NXBGB-001,...
    MaNN CHAR(2) NOT NULL,                      -- Mã ngôn ngữ: VI, EN, RU, BI,...
    MaThL CHAR(6) NOT NULL,                     -- Thể loại: THL001, THL002,...
    MaDD CHAR(5) NOT NULL,                      -- Định dạng: DD001, DD002,...
    MaTK CHAR(7) NULL,                          -- Tài khoản nhập
    TenTL NVARCHAR(200) NOT NULL,               -- Tên tài liệu
    LanXuatBan INT NULL CHECK (LanXuatBan IS NULL OR LanXuatBan > 0),
    NamXuatBan INT NULL CHECK (NamXuatBan IS NULL OR (NamXuatBan >= 1000 AND NamXuatBan <= YEAR(GETDATE()))),
    SoTrang INT NULL CHECK (SoTrang IS NULL OR SoTrang > 0),
    KhoCo CHAR(10) NULL,
    Anh NVARCHAR(255) NULL,

    CONSTRAINT FK_TaiLieu_NXB FOREIGN KEY (MaNXB) REFERENCES dbo.tNhaXuatBan(MaNXB),
    CONSTRAINT FK_TaiLieu_NgonNgu FOREIGN KEY (MaNN) REFERENCES dbo.tNgonNgu(MaNN),
    CONSTRAINT FK_TaiLieu_TheLoai FOREIGN KEY (MaThL) REFERENCES dbo.tTheLoai(MaThL),
    CONSTRAINT FK_TaiLieu_DinhDang FOREIGN KEY (MaDD) REFERENCES dbo.tDinhDang(MaDD),
    CONSTRAINT FK_TaiLieu_TaiKhoan FOREIGN KEY (MaTK) REFERENCES dbo.tTaiKhoan(MaTK)
);
GO

INSERT INTO tTaiLieu (MaTL, MaNXB, MaNN, MaThL, MaDD, MaTK, TenTL, LanXuatBan, NamXuatBan, SoTrang, KhoCo)
VALUES
('TLVN25-001', 'NXBVN-008', 'VI', 'THL001', 'DD001', 'TK25-03', N'Kinh tế số', 1, 2023, 184, 'A4'),
('TLVN25-002', 'NXBVN-001', 'VI', 'THL001', 'DD001', 'TK25-03', N'Chức năng quản lý kinh tế của nhà nước: Từ lý luận đến thực tiễn ở Việt Nam', 1, 2024, 272, 'A3'),
('TLVN25-003', 'NXBVN-001', 'VI', 'THL003', 'DD002', 'TK25-03', N'Nghiên cứu và cải tiến kiến trúc hồ dữ liệu', 1, 2024, 79, 'A4'),
('TLVN25-004', 'NXBVN-001', 'VI', 'THL001', 'DD001', 'TK25-04', N'Toán rời rạc', 1, 2006, 191, 'A4'),
('TLVN25-005', 'NXBVN-001', 'VI', 'THL005', 'DD002', 'TK25-05', N'Nghiên cứu thuật toán và xây dựng chương trình mô phỏng', 1, 2013, 58, 'A4'),
('TLVN25-006', 'NXBVN-001', 'VI', 'THL001', 'DD001', 'TK25-03', N'Cấu tạo và nghiệp vụ đầu máy toa xe', 3, 2014, 297, 'A4'),
('TLVN25-007', 'NXBVN-005', 'VI', 'THL001', 'DD001', 'TK25-04', N'Đại số tuyến tính', 4, 2022, 336, 'A4'),
('TLVN25-008', 'NXBVN-001', 'VI', 'THL001', 'DD001', 'TK25-05', N'Sức bền vật liệu Tập 1', 2, 2018, 300, 'A4'),
('TLGB25-001', 'NXBGB-001', 'EN', 'THL002', 'DD002', 'TK25-06', N'Fundamentals of Telecommunication Networks', 1, 1994, 504, 'A4'),
('TLUS25-001', 'NXBGB-001', 'EN', 'THL002', 'DD002', 'TK25-06', N'America: A Narrative History, Volume 1', 5, 1999, 800, 'A3'),
('TLVN25-009', 'NXBVN-004', 'VI', 'THL002', 'DD003', 'TK25-07', N'Dế Mèn phiêu lưu ký', 4, 1991, 156, 'A4'),
('TLVN25-010', 'NXBVN-001', 'VI', 'THL004', 'DD005', 'TK25-08', N'Nghiên cứu đánh giá tính dư trong kết cấu cầu ở Việt Nam', 1, 2005, 193, 'A4'),
('TLVN25-011', 'NXBVN-001', 'VI', 'THL006', 'DD001', 'TK25-08', N'Tạp chí Khoa học Giao thông Vận tải', 1, 2005, 193, 'A4'),
('TLRU25-001', 'NXBGB-001', 'RU', 'THL002', 'DD001', 'TK25-07', N'Проектирование легковых автомобилей: Техническое задание, эскизный проект и общая компоновка', 1, 1980, 480, 'A4'),
('TLVN25-012', 'NXBVN-003', 'BI', 'THL009', 'DD003', 'TK25-13', N'Thỏ thông minh và giờ, phút, giây', 1, 2025, 24, 'A5'),
('TLVN25-013', 'NXBVN-002', 'VI', 'THL010', 'DD003', 'TK25-05', N'Sách giáo khoa Tiếng Nhật 12 - Tập 1', 5, 2024, 112, 'A4'),
('TLVN25-014', 'NXBVN-006', 'VI', 'THL002', 'DD006', 'TK25-07', N'Sài Gòn - Chuyện đời của phố', 9, 2020, 356, 'A5'),
('TLVN25-015', 'NXBVN-006', 'VI', 'THL002', 'DD008', 'TK25-07', N'Đắc Nhân Tâm', 53, 2020, 320, 'A5'),
('TLVN25-016', 'NXBVN-007', 'VI', 'THL008', 'DD007', 'TK25-08', N'Lược sử loài người', 22, 2023, 520, 'A5');
GO

-- =========================================
-- BẢNG PHỤ TÀI LIỆU - TÁC GIẢ
-- =========================================
CREATE TABLE dbo.tTaiLieu_TacGia (
    MaTL CHAR(10) NOT NULL,
    MaTG CHAR(10) NOT NULL,
    VaiTro NVARCHAR(50) NULL,
    PRIMARY KEY (MaTL, MaTG),
    CONSTRAINT FK_TL_TG_TaiLieu FOREIGN KEY (MaTL) REFERENCES dbo.tTaiLieu(MaTL) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT FK_TL_TG_TacGia FOREIGN KEY (MaTG) REFERENCES dbo.tTacGia(MaTG) ON UPDATE CASCADE ON DELETE CASCADE
);
GO

INSERT INTO tTaiLieu_TacGia (MaTL, MaTG, VaiTro)
VALUES
-- TLVN25-001: Kinh tế số
('TLVN25-001', 'TGVN25-001', N'Đồng chủ biên'),
('TLVN25-001', 'TGVN25-002', N'Đồng chủ biên'),

-- TLVN25-002: Chức năng quản lý kinh tế của nhà nước
('TLVN25-002', 'TGVN25-003', N'Đồng chủ biên'),
('TLVN25-002', 'TGVN25-004', N'Đồng chủ biên'),

-- TLVN25-003: Nghiên cứu và cải tiến kiến trúc hồ dữ liệu
('TLVN25-003', 'TGVN25-005', N'Hướng dẫn khoa học'),
('TLVN25-003', 'TGVN25-006', N'Tác giả'),

-- TLVN25-004: Toán rời rạc
('TLVN25-004', 'TGVN25-007', N'Chủ biên'),
('TLVN25-004', 'TGVN25-008', N'Biên soạn'),
('TLVN25-004', 'TGVN25-009', N'Biên soạn'),

-- TLVN25-005: Nghiên cứu thuật toán và mô phỏng
('TLVN25-005', 'TGVN25-010', N'Tác giả'),

-- TLVN25-006: Cấu tạo và nghiệp vụ đầu máy toa xe
('TLVN25-006', 'TGVN25-011', N'Tác giả'),

-- TLVN25-007: Đại số tuyến tính
('TLVN25-007', 'TGVN25-012', N'Tác giả'),

-- TLVN25-008: Sức bền vật liệu Tập 1
('TLVN25-008', 'TGVN25-013', N'Chủ biên'),
('TLVN25-008', 'TGVN25-014', N'Biên soạn'),
('TLVN25-008', 'TGVN25-015', N'Biên soạn'),

-- TLGB25-001: Fundamentals of Telecommunication Networks
('TLGB25-001', 'TGEG25-001', N'Đồng chủ biên'),
('TLGB25-001', 'TGEG25-002', N'Đồng chủ biên'),
('TLGB25-001', 'TGEG25-003', N'Đồng chủ biên'),

-- TLUS25-001: America: A Narrative History
('TLUS25-001', 'TGGB25-001', N'Đồng chủ biên'),

-- TLVN25-009: Dế Mèn phiêu lưu ký
('TLVN25-009', 'TGVN25-016', N'Tác giả'),

-- TLVN25-010: Nghiên cứu đánh giá tính dư trong kết cấu cầu
('TLVN25-010', 'TGVN25-017', N'Tác giả'),
('TLVN25-010', 'TGVN25-018', N'Hướng dẫn khoa học'),
('TLVN25-010', 'TGVN25-019', N'Hướng dẫn khoa học'),

-- TLVN25-011: Tạp chí KH Giao thông Vận tải
('TLVN25-011', 'TGVN25-020', N'Tổng biên tập'),

-- TLRU25-001: Проектирование легковых автомобилей
('TLRU25-001', 'TGRU25-001', N'Đồng chủ biên'),
('TLRU25-001', 'TGRU25-002', N'Đồng chủ biên'),

-- TLVN25-012: Thỏ thông minh và giờ, phút, giây
('TLVN25-012', 'TGVN25-021', N'Biên soạn'),

-- TLVN25-013: Sách giáo khoa Tiếng Nhật 12
('TLVN25-013', 'TGVN25-022', N'Chủ biên'),

-- TLVN25-014: Sài Gòn - Chuyện đời của phố
('TLVN25-014', 'TGVN25-023', N'Tác giả'),

-- TLVN25-015: Đắc Nhân Tâm
('TLVN25-015', 'TGUS25-002', N'Tác giả'),

-- TLVN25-016: Lược sử loài người
('TLVN25-016', 'TGIL25-001', N'Tác giả');
GO

-- =========================================
-- BẢNG BẢN SAO TÀI LIỆU
-- =========================================
CREATE TABLE dbo.tBanSao (
    MaBS CHAR(14) NOT NULL PRIMARY KEY,
    MaTL CHAR(10) NOT NULL,
    TrangThai NVARCHAR(30) NOT NULL DEFAULT(N'Có sẵn'), -- Có sẵn/Không có sẵn/Ngưng sử dụng
    CONSTRAINT FK_BanSao_TaiLieu FOREIGN KEY (MaTL) REFERENCES dbo.tTaiLieu(MaTL)
);
GO

INSERT INTO dbo.tBanSao (MaBS, MaTL, TrangThai)
VALUES
-- TLVN25-001: Kinh tế số
('BSVN25-001-001', 'TLVN25-001', N'Không có sẵn'),  -- Đang được mượn (GD25060002)
('BSVN25-001-002', 'TLVN25-001', N'Có sẵn'),

-- TLVN25-002: Chức năng quản lý kinh tế
('BSVN25-002-001', 'TLVN25-002', N'Không có sẵn'),  -- Đang được mượn (GD25060001)
('BSVN25-002-002', 'TLVN25-002', N'Không có sẵn'),  -- Đang được mượn (GD25060003)

-- TLVN25-003: Nghiên cứu và cải tiến kiến trúc hồ dữ liệu
('BSVN25-003-001', 'TLVN25-003', N'Có sẵn'),

-- TLVN25-004: Toán rời rạc
('BSVN25-004-001', 'TLVN25-004', N'Không có sẵn'),  -- Đang được mượn (GD25070001)
('BSVN25-004-002', 'TLVN25-004', N'Có sẵn'),

-- TLVN25-005: Nghiên cứu thuật toán và mô phỏng
('BSVN25-005-001', 'TLVN25-005', N'Có sẵn'),

-- TLVN25-006: Cấu tạo và nghiệp vụ đầu máy toa xe
('BSVN25-006-001', 'TLVN25-006', N'Có sẵn'),
('BSVN25-006-002', 'TLVN25-006', N'Có sẵn'),

-- TLVN25-007: Đại số tuyến tính
('BSVN25-007-001', 'TLVN25-007', N'Có sẵn'),

-- TLVN25-008: Sức bền vật liệu Tập 1
('BSVN25-008-001', 'TLVN25-008', N'Có sẵn'),

-- TLGB25-001: Fundamentals of Telecommunication Networks
('BSGB25-001-001', 'TLGB25-001', N'Có sẵn'),

-- TLUS25-001: America: A Narrative History
('BSUS25-001-001', 'TLUS25-001', N'Có sẵn'),

-- TLVN25-009: Dế Mèn phiêu lưu ký
('BSVN25-009-001', 'TLVN25-009', N'Không có sẵn'),  -- Đang được mượn (GD25080001)
('BSVN25-009-002', 'TLVN25-009', N'Có sẵn'),

-- TLVN25-010: Nghiên cứu đánh giá tính dư trong kết cấu cầu
('BSVN25-010-001', 'TLVN25-010', N'Có sẵn'),

-- TLVN25-011: Tạp chí KH Giao thông Vận tải
('BSVN25-011-001', 'TLVN25-011', N'Không có sẵn'),  -- Đang được mượn (GD25110001)

-- TLRU25-001: Проектирование легковых автомобилей
('BSRU25-001-001', 'TLRU25-001', N'Có sẵn'),

-- TLVN25-012: Thỏ thông minh và giờ, phút, giây
('BSVN25-012-001', 'TLVN25-012', N'Có sẵn'),

-- TLVN25-013: Sách giáo khoa Tiếng Nhật 12
('BSVN25-013-001', 'TLVN25-013', N'Không có sẵn'),  -- Đang được mượn (GD25090001)
('BSVN25-013-002', 'TLVN25-013', N'Không có sẵn'),  -- Đang được mượn (GD25090002)

-- TLVN25-014: Sài Gòn - Chuyện đời của phố
('BSVN25-014-001', 'TLVN25-014', N'Có sẵn'),

-- TLVN25-015: Đắc Nhân Tâm
('BSVN25-015-001', 'TLVN25-015', N'Có sẵn'),

-- TLVN25-016: Lược sử loài người
('BSVN25-016-001', 'TLVN25-016', N'Không có sẵn');  -- Đang được mượn (GD25100001)
GO

-- =========================================
-- BẢNG BẠN ĐỌC
-- =========================================
CREATE TABLE dbo.tBanDoc (
    MaBD CHAR(9) NOT NULL PRIMARY KEY,
    HoDem NVARCHAR(50) NOT NULL,
    Ten NVARCHAR(30) NOT NULL,
    NgaySinh DATE NOT NULL,
    GioiTinh CHAR(1) NOT NULL CHECK (GioiTinh IN ('M','F')),
    DiaChi NVARCHAR(200) NULL,
    SDT NVARCHAR(20) NOT NULL,
    Email NVARCHAR(200) NOT NULL
);
GO

INSERT INTO dbo.tBanDoc (MaBD, HoDem, Ten, NgaySinh, GioiTinh, DiaChi, SDT, Email)
VALUES
('231230821', N'Lưu Tùng', N'Lâm', '2005-07-31', 'M', N'99 Nguyễn Chí Thanh, Phường Láng, TP Hà Nội', '0393826705', 'ltlam@gmail.com'),
('231230791', N'Nguyễn Việt', N'Hoàng', '2005-02-07', 'M', N'Xã Thư Lâm, TP Hà Nội', '0388577967', 'nvhoang@gmail.com'),
('231230834', N'Nguyễn Phạm Hoàng', N'Mai', '2005-08-09', 'F', N'59 Khúc Thừa Dụ, Phường Cầu Giấy, TP Hà Nội', '0365457362', 'nphmai@gmail.com'),
('231230766', N'Vũ Thị Thanh', N'Hằng', '2005-12-21', 'F', N'59 Khúc Thừa Dụ, Phường Cầu Giấy, TP Hà Nội', '0961688109', 'vtthang@gmail.com'),
('231230940', N'Nguyễn Văn', N'Tú', '2005-11-21', 'M', N'99 Nguyễn Chí Thanh, Phường Láng, TP Hà Nội', '0399955675', 'nvtu@gmail.com'),
('231230824', N'Nguyễn Thị Thùy', N'Linh', '2005-11-03', 'F', N'36 Hoàng Đạo Thúy, Phường Yên Hòa, TP Hà Nội', '0393693366', 'nttlinh@gmail.com'),
('231220839', N'Đỗ Duy', N'Minh', '2005-04-27', 'M', N'33 Chùa Láng, Phường Láng, TP Hà Nội', '0822221992', 'dmminh@gmail.com'),
('231230754', N'Nguyễn Thị', N'Giang', '2005-01-02', 'F', N'203 Chùa Láng, Phường Láng, TP Hà Nội', '0856905686', 'ntgiang@gmail.com'),
('231220753', N'Lại Trường', N'Giang', '2005-05-09', 'M', N'36 Xuân Thủy, Phường Cầu Giấy, TP Hà Nội', '0363435295', 'ltgiang@gmail.com'),
('221133828', N'Nguyễn Hải', N'Ninh', '2004-09-12', 'M', N'99 Nguyễn Chí Thanh, Phường Láng, TP Hà Nội', '0393629763', 'nhninh@gmail.com');
GO

-- =========================================
-- BẢNG THẺ BẠN ĐỌC
-- =========================================
CREATE TABLE dbo.tTheBanDoc (
    MaTBD CHAR(12) NOT NULL PRIMARY KEY,
    MaBD CHAR(9) NOT NULL,
    MaTK CHAR(7) NOT NULL, 
    NgayCap DATE NOT NULL,
    NgayHetHan DATE NULL,
    TrangThai NVARCHAR(30) NOT NULL DEFAULT('Hoạt động'), -- Hoạt động / Khóa
    CONSTRAINT FK_TheBanDoc_BanDoc FOREIGN KEY (MaBD) REFERENCES dbo.tBanDoc(MaBD),
    CONSTRAINT FK_TheBanDoc_TaiKhoan FOREIGN KEY (MaTK) REFERENCES dbo.tTaiKhoan(MaTK), 
    CONSTRAINT CHK_TheBanDoc_NgayHetHan CHECK (NgayHetHan IS NULL OR NgayHetHan >= NgayCap)
);
GO

INSERT INTO tTheBanDoc (MaTBD, MaBD, MaTK, NgayCap, NgayHetHan, TrangThai)
VALUES
('TBD231230821', '231230821', 'TK25-11', '2023-09-11', '2028-09-30', N'Hoạt động'),
('TBD231230791', '231230791', 'TK25-11', '2023-09-11', '2028-09-30', N'Hoạt động'),
('TBD231230834', '231230834', 'TK25-12', '2023-09-11', '2028-09-30', N'Hoạt động'),
('TBD231230766', '231230766', 'TK25-12', '2023-09-11', '2028-09-30', N'Hoạt động'),
('TBD231230940', '231230940', 'TK25-11', '2023-09-11', '2028-09-30', N'Hoạt động'),
('TBD231230824', '231230824', 'TK25-12', '2023-09-11', '2028-09-30', N'Hoạt động'),
('TBD231220839', '231220839', 'TK25-11', '2023-09-11', '2028-09-30', N'Hoạt động'),
('TBD231230754', '231230754', 'TK25-12', '2023-09-11', '2028-09-30', N'Hoạt động'),
('TBD231220753', '231220753', 'TK25-11', '2023-09-11', '2028-09-30', N'Hoạt động'),
('TBD221133828', '221133828', 'TK25-12', '2022-09-12', '2027-09-30', N'Hoạt động');
GO

-- ===========================
-- Bảng giao dịch mượn - trả
-- ===========================
CREATE TABLE dbo.tGiaoDichMuonTra (
    MaGD CHAR(12) NOT NULL PRIMARY KEY,
    MaTBD CHAR(12) NOT NULL,
    MaTK CHAR(7) NOT NULL, -- Nhân viên thực hiện giao dịch (phòng QLM)
    NgayMuon DATE NOT NULL DEFAULT GETDATE(),
    NgayHenTra DATE NOT NULL,
    NgayTra DATE NULL,
    TrangThai NVARCHAR(30) NOT NULL DEFAULT('Đang mượn'), -- Đang mượn / Đã trả / Trễ hạn
    CONSTRAINT FK_GD_TheBanDoc FOREIGN KEY (MaTBD) REFERENCES dbo.tTheBanDoc(MaTBD),
    CONSTRAINT FK_GD_TaiKhoan FOREIGN KEY (MaTK) REFERENCES dbo.tTaiKhoan(MaTK),
    CONSTRAINT CHK_GD_NgayHen_CHECK CHECK (NgayHenTra >= NgayMuon),
    CONSTRAINT CHK_GD_NgayTra_CHECK CHECK (NgayTra IS NULL OR NgayTra >= NgayMuon)
);
GO

INSERT INTO tGiaoDichMuonTra (MaGD, MaTBD, MaTK, NgayMuon, NgayHenTra, NgayTra, TrangThai)
VALUES
('GD25060001', 'TBD231230821', 'TK25-09', '2025-10-01', '2025-12-26', NULL, N'Đang mượn'),
('GD25060002', 'TBD231230821', 'TK25-09', '2025-10-05', '2025-12-28', NULL, N'Đang mượn'),
('GD25060003', 'TBD231230824', 'TK25-09', '2025-10-10', '2025-12-30', NULL, N'Đang mượn'),
('GD25070001', 'TBD231230766', 'TK25-10', '2025-09-23', '2025-12-20', NULL, N'Đang mượn'),
('GD25080001', 'TBD231220839', 'TK25-09', '2025-10-09', '2025-12-26', NULL, N'Đang mượn'),
('GD25090001', 'TBD231230940', 'TK25-10', '2025-09-20', '2025-12-19', NULL, N'Đang mượn'),
('GD25090002', 'TBD231230940', 'TK25-10', '2025-10-12', '2025-12-30', NULL, N'Đang mượn'),
('GD25100001', 'TBD221133828', 'TK25-10', '2025-10-01', '2025-12-06', NULL, N'Đang mượn'),
('GD25110001', 'TBD231220753', 'TK25-09', '2025-10-01', '2025-12-15', NULL, N'Đang mượn');
GO

CREATE TABLE dbo.tGiaoDich_BanSao (
    MaGD CHAR(12) NOT NULL,
    MaBS CHAR(14) NOT NULL,
    TinhTrang BIT NOT NULL DEFAULT 0, -- 0 = Đang mượn, 1 = Đã trả
    PRIMARY KEY (MaGD, MaBS),
    CONSTRAINT FK_GDBS_GD FOREIGN KEY (MaGD) REFERENCES dbo.tGiaoDichMuonTra(MaGD) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT FK_GDBS_BS FOREIGN KEY (MaBS) REFERENCES dbo.tBanSao(MaBS) ON UPDATE CASCADE ON DELETE CASCADE
);
GO

INSERT INTO dbo.tGiaoDich_BanSao (MaGD, MaBS, TinhTrang)
VALUES
-- Giao dịch GD25060001: TBD2330821 mượn BSVN25-002-001
('GD25060001', 'BSVN25-002-001', 0),

-- Giao dịch GD25060002: TBD2330821 mượn BSVN25-001-001
('GD25060002', 'BSVN25-001-001', 0),

-- Giao dịch GD25060003: TBD2330824 mượn BSVN25-002-002
('GD25060003', 'BSVN25-002-002', 0),

-- Giao dịch GD25070001: TBD2330766 mượn BSVN25-004-001
('GD25070001', 'BSVN25-004-001', 0),

-- Giao dịch GD25080001: TBD2320839 mượn BSVN25-009-001
('GD25080001', 'BSVN25-009-001', 0),

-- Giao dịch GD25090001: TBD2330940 mượn BSVN25-013-001
('GD25090001', 'BSVN25-013-001', 0),

-- Giao dịch GD25090002: TBD2330940 mượn BSVN25-013-002
('GD25090002', 'BSVN25-013-002', 0),

-- Giao dịch GD25100001: TBD2233828 mượn BSVN25-016-001
('GD25100001', 'BSVN25-016-001', 0),

-- Giao dịch GD25110001: TBD2320753 mượn BSVN25-011-001
('GD25110001', 'BSVN25-011-001', 0);
GO
