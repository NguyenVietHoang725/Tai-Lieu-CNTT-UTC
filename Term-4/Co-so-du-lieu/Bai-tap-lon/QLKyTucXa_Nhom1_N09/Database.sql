-- Bảng tSinhVien
CREATE TABLE tSinhVien (
    MaSV NVARCHAR(10) PRIMARY KEY,  
    HoTen NVARCHAR(100) NOT NULL,     
    NgaySinh DATE NULL,             
    GioiTinh BIT NULL,               
    SDT VARCHAR(10) NULL            
);

-- Bảng tNhanVien
CREATE TABLE tNhanVien (
    MaNV NVARCHAR(10) PRIMARY KEY,    
    HoTen NVARCHAR(100) NOT NULL,    
    NgaySinh DATE NULL,              
    GioiTinh BIT NULL,              
    ChucVu NVARCHAR(100) NULL       
);

-- Bảng tPTTThuePhong
CREATE TABLE tPTTThuePhong (
    MaPT NVARCHAR(10) PRIMARY KEY,
    MaNV NVARCHAR(10) NOT NULL,
    MASV NVARCHAR(10) NOT NULL,
    NgayLapPhieu DATE NULL,
    TongTien DECIMAL(18, 2) NULL,   
    TrangThai BIT DEFAULT 0,

    FOREIGN KEY (MaNV) REFERENCES tNhanVien(MaNV),
    FOREIGN KEY (MASV) REFERENCES tSinhVien(MaSV)
);

-- Bảng: tLoaiPhong
CREATE TABLE tLoaiPhong (
    MaLoaiPhong NVARCHAR(10) PRIMARY KEY,
    TenLoaiPhong NVARCHAR(100) NOT NULL,
    Nam_Nu BIT NULL,
    SoNguoi INT NULL,
	DonGia DECIMAL(18, 2)
);

-- Bảng: tPhong
CREATE TABLE tPhong (
    MaPhong NVARCHAR(10) PRIMARY KEY,
    TenPhong NVARCHAR(100) NOT NULL,
    MaLoaiPhong NVARCHAR(10) NULL,

    FOREIGN KEY (MaLoaiPhong) REFERENCES tLoaiPhong(MaLoaiPhong)
);

-- Bảng: tHopDong
CREATE TABLE tHopDong (
    MaHD NVARCHAR(10) PRIMARY KEY,
    MaNV NVARCHAR(10) NOT NULL,
    MaSV NVARCHAR(10) NOT NULL,
    MaPhong NVARCHAR(10) NOT NULL,
    NgayBatDau DATE NULL,
    NgayKetThuc DATE NULL,

    FOREIGN KEY (MaNV) REFERENCES tNhanVien(MaNV),
    FOREIGN KEY (MaSV) REFERENCES tSinhVien(MaSV),
    FOREIGN KEY (MaPhong) REFERENCES tPhong(MaPhong)
);

-- Bảng: tPBTThuePhong
CREATE TABLE tPBTThuePhong (
    MaPB NVARCHAR(10) PRIMARY KEY,
    MaHD NVARCHAR(10) NOT NULL,
    NgayLapPhieu DATE NULL,
    HanThanhToan DATE NULL,
	TongTien DECIMAL(18, 2) NULL,
    TrangThai BIT NULL,

    FOREIGN KEY (MaHD) REFERENCES tHopDong(MaHD)
);

-- Bảng: tDienNuoc
CREATE TABLE tDienNuoc (
    MaCongTo NVARCHAR(10),
    ThangGhiSo DATE,
    MaPhong NVARCHAR(10) NULL,
    ChiSoDau INT NULL,
    ChiSoCuoi INT NULL,

    PRIMARY KEY (MaCongTo, ThangGhiSo),
    FOREIGN KEY (MaPhong) REFERENCES tPhong(MaPhong)
);

-- Bảng: tPTTDienNuoc
CREATE TABLE tPTTDienNuoc (
    MaPT NVARCHAR(10) PRIMARY KEY,
    MaNV NVARCHAR(10) NOT NULL,
    MaPhong NVARCHAR(10) NOT NULL,
    NgayLapPhieu DATE NULL,
    TongTien DECIMAL(18, 2) NULL,
    TrangThai BIT NULL,

    FOREIGN KEY (MaNV) REFERENCES tNhanVien(MaNV),
    FOREIGN KEY (MaPhong) REFERENCES tPhong(MaPhong)
);

-- Bảng: tPBTDienNuoc
CREATE TABLE tPBTDienNuoc (
    MaPB NVARCHAR(10) PRIMARY KEY,
    NgayLapPhieu DATE NULL,
    HanThanhToan DATE NULL,
    TongTien DECIMAL(18, 2) NULL,
    TrangThai BIT NULL
);

-- Bảng: tCTPBTDienNuoc
CREATE TABLE tCTPBTDienNuoc (
    MaPB NVARCHAR(10),
    MaCongTo NVARCHAR(10),
    ThangGhiSo DATE,
    ChiSo INT NULL,
    DonGia DECIMAL(18, 2) NULL,
    ThanhTien DECIMAL(18, 2) NULL,

    PRIMARY KEY (MaPB, MaCongTo, ThangGhiSo),
    FOREIGN KEY (MaPB) REFERENCES tPBTDienNuoc(MaPB),
    FOREIGN KEY (MaCongTo, ThangGhiSo) REFERENCES tDienNuoc(MaCongTo, ThangGhiSo)
);
