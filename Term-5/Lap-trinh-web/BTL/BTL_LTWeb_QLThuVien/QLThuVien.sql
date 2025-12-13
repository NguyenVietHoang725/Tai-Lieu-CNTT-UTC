
-- Data Bảng tác giả
INSERT INTO tTacGia (MaTG, HoDem, Ten)
VALUES
-- MaTG = MTG + Quốc gia (VN) + Dãy số tăng dần từ 001 
('MTGVN001', N'PGS.TS Trần Thị Ái', N'Cẩm'),
('MTGVN002', N'Ths Đỗ Thùy', N'Trinh'),
('MTGVN003', N'PGS.TS Nguyễn Hồng', N'Sơn'),
('MTGVN004', N'TS Nguyễn Hồng', N'Thái'),
('MTGVN005', N'TS Nguyễn Đức', N'Dư'),
('MTGVN006', N'TS Hoàng Văn', N'Thông'),
('MTGVN007', N'TS Lương Thái', N'Lê'),
('MTGVN008', N'Lê Tiến', N'Dũng'),
('MTGVN009', N'ThS Phạm Xuân', N'Tích'),
('MTGVN010', N'PGS.TS Nguyễn Văn', N'Long'),
('MTGVN011', N'GS.TS Đỗ Đức', N'Tuấn'),
('MTGVN012', N'GS.TSKH Nguyễn Hữu Việt', N'Hưng'),
('MTGVN013', N'PGS.TS Vũ Đình', N'Lai'),
('MTGVN014', N'TS Nguyễn Xuân', N'Lựu'),
('MTGVN015', N'TS Bùi Đình', N'Nghi'),
('MTGNN001', N'GS Ahmed El', N'Hakeem'),
('MTGNN002', N'GS Mostafa H.', N'Ammar'),
('MTGNN003', N'GS Tarek N.', N'Saadawi'),
('MTGNN004', N'GS George Brown', N'Tindall'),
('MTGNN005', N'GS David Emory', N'Shi'),
('MTGVN016', N'Tô', N'Hoài'),
('MTGVN017', N'Nguyễn Thị Tuyết', N'Trinh'),
('MTGVN018', N'PGS.TS Nguyễn Ngọc', N'Long'),
('MTGVN019', N'PGS.TS Nguyễn Văn', N'Tám'),
('MTGVN020', N'PGS.TS Trần Tuấn', N'Hiệp'),
('MTGNN006', N'TSKHKT V. F.', N'Regionov'),
('MTGNN007', N'TSKHKT B. B.', N'Fitterman'),
('MTGVN021', N'Hạnh', N'Nguyên'),
('MTGVN022', N'PGS.TS Nguyễn Thị Thanh', N'Huế'),
('MTGVN023', N'Phạm Công', N'Luận'),
('MTGNN008', N'Dale', N'Carnegie'),
('MTGNN009', N'GS.TS Yuval Noah', N'Harari');

-- Data Bảng nhà xuất bản
INSERT INTO tNhaXuatBan (MaNXB, TenNXB)
VALUES
-- MaNXB = NXB + Quốc gia (VN) + Dãy số tăng dần từ 01
('NXBVN01', N'NXB Giao thông vận tải'),
('NXBVN02', N'NXB Giáo dục'),
('NXBVN03', N'NXB Trẻ'),
('NXBVN04', N'NXB Kim Đồng'),
('NXBVN05', N'NXB Đại học Quốc gia Hà Nội'),
('NXBVN06', N'NXB Tổng hợp TP.HCM'),
('NXBVN07', N'NXB Tri thức'),
('NXBVN08', N'NXB Đại học Cần Thơ'),
('NXBNN01', N'NXB Nước Ngoài');

-- Data Bảng ngôn ngữ
INSERT INTO tNgonNgu (MaNN, TenNN)
VALUES
-- MaNN = Viết tắt của tiếng bằng 2 ký tự
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

-- Data Bảng thể loại
INSERT INTO tTheLoai (MaThL, TenThL)
VALUES
-- MaThL = Viết tắt của thể loại bằng 2 ký tự
('GT', N'Sách giáo trình'),
('TK', N'Sách tham khảo'),
('LV', N'Luận văn'),
('LA', N'Luận án'),
('DT', N'Đề tài nghiên cứu khoa học'),
('BA', N'Báo'),
('TC', N'Tạp chí'),
('KY', N'Kỷ yếu hội thảo'),
('NB', N'Tài liệu nội bộ'),
('DTU', N'Tài liệu điện tử');

-- Data Bảng định dạng
INSERT INTO tDinhDang (MaDD, TenDD)
VALUES
-- MaDD = DD + Dãy số tăng dần từ 01
('DD01', N'Sách in'),
('DD02', N'Luận văn / Luận án bản in'),
('DD03', N'PDF / Ebook'),
('DD04', N'Tài liệu Word (DOC/DOCX)'),
('DD05', N'CD / DVD'),
('DD06', N'Video học thuật'),
('DD07', N'Âm thanh (Audio / Podcast)'),
('DD08', N'Trang web / Online resource'),
('DD09', N'Bản đồ / Atlas'),
('DD10', N'Khác');

-- Data Bảng tài liệu
INSERT INTO tTaiLieu (MaTL, MaNXB, MaNN, MaThL, MaDD, TenTL, LanXuatBan, NamXuatBan, SoTrang, KhoCo)
VALUES
-- MaTL = MTL + Quốc gia (VN, NN) + Dãy số tăng dần từ 001
('MTLVN001', 'NXBVN08', 'VI', 'GT', 'DD01', N'Kinh tế số', 1, 2023, 184, 'A4'),
('MTLVN002', 'NXBVN01', 'VI', 'GT', 'DD01', N'Chức năng quản lý kinh tế của nhà nước: Từ lý luận đến thực tiễn ở Việt Nam', 1, 2024, 272, 'A3'),
('MTLVN003', 'NXBVN01', 'VI', 'LV', 'DD02', N'Nghiên xây dựng và cải tiến kiến trúc hồ dữ liệu', 1, 2024, 79, 'A4'),
('MTLVN004', 'NXBVN01', 'VI', 'GT', 'DD01', N'Toán rời rạc', 1, 2006, 191, 'A4'),
('MTLVN005', 'NXBVN01', 'VI', 'DT', 'DD02', N'Nghiên cứu thuật toán và xây dựng chương trình mô phỏng một số thuật toán phục vụ giảng dạy lý thuyết đồ thị và toán học rời rạc', 1, 2013, 58, 'A4'),
('MTLVN006', 'NXBVN01', 'VI', 'GT', 'DD01', N'Cấu tạo và nghiệp vụ đầu máy toa xe', 3, 2014, 297, 'A4'),
('MTLVN007', 'NXBVN05', 'VI', 'GT', 'DD01', N'Đại số tuyến tính', 4, 2022, 336, 'A4'),
('MTLVN008', 'NXBVN01', 'VI', 'GT', 'DD01', N'Sức bền vật liệu T1', 2, 2018, 300, 'A4'),
('MTLNN001', 'NXBNN01', 'EN', 'TK', 'DD02', N'Fundamentals of telecommunication networks', 1, 1994, 504, 'A4'),
('MTLNN002', 'NXBNN01', 'EN', 'TK', 'DD02', N'America: A Narrative History, Volume 1', 5, 1999, 800, 'A3'),
('MTLVN009', 'NXBVN04', 'VI', 'TK', 'DD03', N'Dế Mèn phiêu lưu ký', 4, 1991, 156, 'A4'),
('MTLVN010', 'NXBVN01', 'VI', 'LA', 'DD05', N'Nghiên cứu đánh giá tính dư trong kết cấu cầu ở Việt Nam', 1, 2005, 193, 'A4'),
('MTLVN011', 'NXBVN01', 'VI', 'BA', 'DD01', N'Tạp chí khoa học Giao thông Vận Tải', 1, 2005, 193, 'A4'),
('MTLNN003', 'NXBNN01', 'RU', 'TK', 'DD01', N'Проектирование легковых автомобилей: Техническое задание, эскизный проект и общая компоновка', 1, 1980, 480, 'A4'),
('MTLVN012', 'NXBVN03', 'BI', 'NB', 'DD03', N'Thỏ thông minh và giờ, phút, giây', 1, 2025, 24, 'A5'),
('MTLVN013', 'NXBVN02', 'VI', 'DTU', 'DD03', N'Sách giáo khoa Tiếng Nhật 12 T1', 5, 2024, 112, 'A4'),
('MTLVN014', 'NXBVN06', 'VI', 'TK', 'DD06', N'Sài Gòn - Chuyện đời của phố', 9, 2020, 356, 'A5'),
('MTLNN004', 'NXBVN06', 'VI', 'TK', 'DD08', N'Đắc Nhân Tâm', 53, 2020, 320, 'A5'),
('MTLNN005', 'NXBVN07', 'VI', 'KY', 'DD07', N'Lược sử loài người', 22, 2023, 520, 'A5');

-- Data Bảng trung gian: Tài liệu - Tác giả
INSERT INTO tTaiLieu_TacGia (MaTL, MaTG, VaiTro)
VALUES
('MTLVN001', 'MTGVN001', N'Đồng chủ biên'),
('MTLVN001', 'MTGVN002', N'Đồng chủ biên'),
('MTLVN002', 'MTGVN003', N'Đồng chủ biên'),
('MTLVN002', 'MTGVN004', N'Đồng chủ biên'),
('MTLVN003', 'MTGVN005', N'Hướng dẫn khoa học'),
('MTLVN003', 'MTGVN006', N'Tác giả'),
('MTLVN004', 'MTGVN007', N'Chủ biên'),
('MTLVN004', 'MTGVN008', N'Biên soạn'),
('MTLVN004', 'MTGVN009', N'Biên soạn'),
('MTLVN005', 'MTGVN010', N'Tác giả'),
('MTLVN006', 'MTGVN011', N'Tác giả'),
('MTLVN007', 'MTGVN012', N'Tác giả'),
('MTLVN008', 'MTGVN013', N'Chủ biên'),
('MTLVN008', 'MTGVN014', N'Biên soạn'),
('MTLVN008', 'MTGVN015', N'Biên soạn'),
('MTLNN001', 'MTGNN001', N'Đồng chủ biên'),
('MTLNN001', 'MTGNN002', N'Đồng chủ biên'),
('MTLNN001', 'MTGNN003', N'Đồng chủ biên'),
('MTLNN002', 'MTGNN0004', N'Đồng chủ biên'),
('MTLNN002', 'MTGNN005', N'Đồng chủ biên'),
('MTLVN009', 'MTGVN016', N'Tác giả'),
('MTLVN010', 'MTGVN017', N'Tác giả'),
('MTLVN010', 'MTGVN018', N'Hướng dẫn khoa học'),
('MTLVN010', 'MTGVN019', N'Hướng dẫn khoa học'),
('MTLVN011', 'MTGVN020', N'Tổng biên tập'),
('MTLNN003', 'MTGNN006', N'Đồng chủ biên'),
('MTLNN003', 'MTGNN007', N'Đồng chủ biên'),
('MTLVN012', 'MTGVN021', N'Biên soạn'),
('MTLNN004', 'MTGNN008', N'Chủ biên'),
('MTLNN005', 'MTGNN009', N'Tác giả');

-- Data Bảng bản sao tài liệu
INSERT INTO tBanSao (MaBS, MaTL, TinhTrang)
VALUES
('BSVN001001', 'MTLVN001', N'Đã được mượn'),
('BSVN001002', 'MTLVN001', N'Chưa được mượn'),
('BSVN001003', 'MTLVN001', N'Chưa được mượn'),
('BSVN002001', 'MTLVN002', N'Đã được mượn'),
('BSVN002002', 'MTLVN002', N'Đã được mượn'),
('BSVN002003', 'MTLVN002', N'Đã được mượn'),
('BSVN002004', 'MTLVN002', N'Đã được mượn'),
('BSVN003001', 'MTLVN003', N'Chưa được mượn'),
('BSVN004001', 'MTLVN004', N'Đã được mượn'),
('BSVN004002', 'MTLVN004', N'Đã được mượn'),
('BSVN004003', 'MTLVN004', N'Chưa được mượn'),
('BSVN004004', 'MTLVN004', N'Chưa được mượn'),
('BSVN004005', 'MTLVN004', N'Chưa được mượn'),
('BSVN006001', 'MTLVN006', N'Chưa được mượn'),
('BSVN006002', 'MTLVN006', N'Chưa được mượn'),
('BSVN006003', 'MTLVN006', N'Chưa được mượn'),
('BSNN001001', 'MTLNN001', N'Chưa được mượn'),
('BSNN002001', 'MTLNN002', N'Đã được mượn'),
('BSVN013001', 'MTLVN013', N'Đã được mượn'),
('BSVN013002', 'MTLVN013', N'Đã được mượn'),
('BSVN009001', 'MTLVN009', N'Chưa được mượn');

-- Data Bảng bạn đọc
INSERT INTO tBanDoc (MaBD, HoDem, Ten, NgaySinh, GioiTinh, DiaChi, SDT, Email)
VALUES
-- MaBD = Mã sinh viên
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

-- Data Bảng thẻ bạn đọc
INSERT INTO tTheBanDoc (MaTBD, MaBD, NgayCap, NgayHetHan, TrangThai)
VALUES
-- MaTBD = TBD + Năm nhập học (22, 23) + 5 số cuối của mã sinh viên
('TBD2330821', '231230821', '2023-09-11', '2028-09-30', N'Hoạt động'),
('TBD2330791', '231230791', '2023-09-11', '2028-09-30', N'Hoạt động'),
('TBD2330834', '231230834', '2023-09-11', '2028-09-30', N'Hoạt động'),
('TBD2330766', '231230766', '2023-09-11', '2028-09-30', N'Hoạt động'),
('TBD2330940', '231230940', '2023-09-11', '2028-09-30', N'Hoạt động'),
('TBD2330824', '231230824', '2023-09-11', '2028-09-30', N'Hoạt động'),
('TBD2320839', '231220839', '2023-09-11', '2028-09-30', N'Hoạt động'),
('TBD2330754', '231230754', '2023-09-11', '2028-09-30', N'Hoạt động'),
('TBD2320753', '231220753', '2023-09-11', '2028-09-30', N'Hoạt động'),
('TBD2233828', '221133828', '2022-09-12', '2027-09-30', N'Hoạt động');

-- Data Bảng vai trò
INSERT INTO tVaiTro (MaVT, TenVT, MoTa)
VALUES
-- MaVT = Viết tắt của vai trò bằng 3 ký tự
('QTV', N'Quản trị viên', N'Quản lý toàn bộ hệ thống, tài khoản và phân quyền.'),
('QLB', N'Quản lý bạn đọc', N'Quản lý hồ sơ bạn đọc, thẻ thư viện và thông tin cá nhân.'),
('QLM', N'Quản lý mượn trả', N'Theo dõi, xử lý mượn và trả tài liệu.'),
('QLT', N'Quản lý tài liệu', N'Quản lý nhập, cập nhật, phân loại và tình trạng tài liệu.');

-- Data Bảng nhân viên
INSERT INTO tNhanVien (MaNV, HoDem, Ten, NgaySinh, GioiTinh, DiaChi, SDT, Email, PhuTrach)
VALUES
-- MaNV = NV + Dãy số tăng dần từ 01
('NV01', N'Nguyễn Đức', N'Dư', '1988-09-25', 'M', N'Hà Nội', '0912363245', 'nducdu@utc.edu.vn', N'Giám đốc'),
('NV02', N'Nguyễn Thanh', N'Thủy', '1985-09-27', 'F', N'Hà Nội', '0912424955', 'nthanhthuy@utc.edu.vn', N'Phó Giám đốc'),
('NV03', N'Nguyễn Thị', N'Hòa', '1989-07-31', 'F', N'Hà Nội', '0327902799', 'nthhoa@utc.edu.vn', N'Phòng Nghiệp Vụ'),
('NV04', N'Trần Thị Thu', N'Hương', '1987-08-16', 'F', N'Tuyên Quang', '0331356271', 'tthhuong@utc.edu.vn', N'Phòng Nghiệp Vụ'),
('NV05', N'Bùi Thị Yến', N'Hường', '1988-03-12', 'F', N'Thái Nguyên', '0897878469', 'btyhuong@utc.edu.vn', N'Phòng Nghiệp Vụ'),
('NV06', N'Phạm Thị Thúy', N'Nga', '1998-05-08', 'F', N'Hà Nội', '0786561875', 'pttnga@utc.edu.vn', N'Phòng đọc Luận văn, Luận Án, Đề tài NCKH, Báo, Tạp chí'),
('NV07', N'Phạm Ngọc Thanh', N'Quang', '1996-03-12', 'M', N'Hà Nội', '0388577967', 'pnquang@utc.edu.vn', N'Phòng đọc Luận văn, Luận Án, Đề tài NCKH, Báo, Tạp chí'),
('NV08', N'Nguyễn Thị Hồng', N'Khoa', '1982-10-13', 'F', N'Lào Cai', '0972936189', 'nthkhoa@utc.edu.vn', N'Phòng đọc Luận văn, Luận Án, Đề tài NCKH, Báo, Tạp chí'),
('NV09', N'Phạm Thiên', N'Thu', '1999-04-24', 'F', N'Hà Nội', '0794854606', 'pthu@utc.edu.vn', N'Phòng Mượn'),
('NV10', N'Vũ Thị Hà', N'Vân', '1996-05-06', 'F', N'Ninh Bình', '0875306860', 'vthvan@utc.edu.vn', N'Phòng Mượn'),
('NV11', N'Châu Mạnh', N'Quang', '2000-03-29', 'M', N'Phú Thọ', '0829773751', 'cmquang@utc.edu.vn', N'Phòng đọc Tiếng Việt'),
('NV12', N'Cù Việt', N'Hằng', '1995-05-09', 'F', N'Hà Nội', '0787795092', 'cvhang@utc.edu.vn', N'Phòng đọc Tiếng Việt'),
('NV13', N'Kim Thị', N'Hoa', '1995-03-06', 'F', N'Quảng Ninh', '0376054504', 'kthhoa@utc.edu.vn', N'Nhà sách');

-- Data Bảng tài khoản
INSERT INTO tTaiKhoan (MaTK, MaNV, MaVT, TenDangNhap, MatKhau, TrangThai, NgayTao)
VALUES
-- Quản trị viên
('TK01', 'NV01', 'QTV', 'nducdu@utc.edu.vn', '@utc123456', N'Hoạt động', '2022-05-12'),

-- Quản lý bạn đọc
('TK02', 'NV02', 'QLB', 'nthanhthuy@utc.edu.vn', '@utc123456', N'Hoạt động', '2022-05-12'),

-- Quản lý tài liệu (Phòng nghiệp vụ)
('TK03', 'NV03', 'QLT', 'nthhoa@utc.edu.vn', '@utc123456', N'Hoạt động', '2022-05-12'),
('TK04', 'NV04', 'QLT', 'tthhuong@utc.edu.vn', '@utc123456', N'Hoạt động', '2023-10-24'),
('TK05', 'NV05', 'QLT', 'btyhuong@utc.edu.vn', '@utc123456', N'Hoạt động', '2023-11-02'),

-- Quản lý tài liệu (Phòng đọc luận văn, đề tài, báo, tạp chí)
('TK06', 'NV06', 'QLT', 'pttnga@utc.edu.vn', '@utc123456', N'Hoạt động', '2022-05-12'),
('TK07', 'NV07', 'QLT', 'pnquang@utc.edu.vn', '@utc123456', N'Hoạt động', '2024-03-29'),
('TK08', 'NV08', 'QLT', 'nthkhoa@utc.edu.vn', '@utc123456', N'Hoạt động', '2024-03-29'),

-- Quản lý mượn trả
('TK09', 'NV09', 'QLM', 'pthu@utc.edu.vn', '@utc123456', N'Hoạt động', '2022-05-12'),
('TK10', 'NV10', 'QLM', 'vthvan@utc.edu.vn', '@utc123456', N'Hoạt động', '2022-06-01'),

-- Quản lý bạn đọc (Phòng đọc tiếng Việt)
('TK11', 'NV11', 'QLB', 'cmquang@utc.edu.vn', '@utc123456', N'Hoạt động', '2022-05-12'),
('TK12', 'NV12', 'QLB', 'cvhang@utc.edu.vn', '@utc123456', N'Hoạt động', '2025-08-14'),

-- Nhà sách (tạm phân loại quản lý tài liệu)
('TK13', 'NV13', 'QLT', 'kthhoa@utc.edu.vn', '@utc123456', N'Hoạt động', '2022-05-12');

-- Data Bảng giao dịch mượn trả
INSERT INTO tGiaoDichMuonTra (MaGD, MaTBD, MaTK, NgayMuon, NgayHenTra, NgayTra, TrangThai)
VALUES
-- MaGD = GD + năm GD (25) + Dãy số tăng dần của TK + Dãy số tăng dần từ 0001
('GD25060001', 'TBD2330821', 'TK06', '2025-10-01', '2025-12-26', NULL, N'Đang mượn'),
('GD25060002', 'TBD2330821', 'TK06', '2025-10-01', '2025-12-26', NULL, N'Đang mượn'),
('GD25060003', 'TBD2330821', 'TK06', '2025-10-01', '2025-12-26', NULL, N'Đang mượn'),
('GD25070001', 'TBD2330766', 'TK07', '2025-09-23', '2025-12-20', NULL, N'Đang mượn'),
('GD25070002', 'TBD2330940', 'TK07', '2025-09-20', '2025-12-19', NULL, N'Đang mượn'),
('GD25080001', 'TBD2330940', 'TK08', '2025-10-12', '2025-12-30', NULL, N'Đang mượn'),
('GD25090001', 'TBD2320839', 'TK09', '2025-10-09', '2025-12-26', NULL, N'Đang mượn'),
('GD25100001', 'TBD2320753', 'TK10', '2025-09-30', '2025-12-26', NULL, N'Đang mượn'),
('GD25100002', 'TBD2233828', 'TK10', '2025-10-01', '2025-12-06', NULL, N'Đang mượn'),
('GD25110001', 'TBD2320753', 'TK11', '2025-10-01', '2025-12-15', NULL, N'Đang mượn');

-- Data Bảng trung gian: Giao dịch mượn trả - Bản sao tài liệu
INSERT INTO tGiaoDich_BanSao (MaGD, MaBS, TinhTrangMuon, TinhTrangTra)
VALUES
('GD25060001', 'BSVN002001', N'Đã được mượn', N'Chưa trả'),
('GD25070002', 'BSVN004001', N'Đã được mượn', N'Chưa trả'),
('GD25080001', 'BSVN002003', N'Đã được mượn', N'Chưa trả'),
('GD25070001', 'BSVN002001', N'Đã được mượn', N'Chưa trả'),
('GD25110001', 'BSVN001001', N'Đã được mượn', N'Chưa trả');