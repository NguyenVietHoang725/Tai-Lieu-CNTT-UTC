use QuanLyDiemTruongDaiHoc

SELECT 
	sv.MaSinhVien,
	HoDem + ' ' + Ten AS HoTen,
	AVG(lsv.DiemTKHP) as DiemTrungBinh
FROM SinhVien sv
JOIN LopHocPhan_SinhVien lsv ON lsv.MaSinhVien = sv.MaSinhVien
JOIN LopHocPhan lhp ON lhp.MaHocPhan = lsv.MaHocPhan
WHERE sv.MaLop = N'K61.CNTT2'
AND lhp.HocKy = 1
GROUP BY sv.MaSinhVien, sv.HoDem, sv.Ten

-- 4. Tạo hàm để truy xuất danh sách các sinh viên có điểm trung bình thấp hơn
-- điểm trung bình của cả lớp với mã lớp, năm học, kỳ học là tham số đầu vào
-- 5. Đưa ra bảng điểm của sinh viên với mã sinh viên là tham số đầu vào và mỗi
-- học phần chỉ đưa ra một thông tin điểm cho lần học có điểm cao nhất (điểm TKHP)
-- 6. Tạo hàm đưa ra danh sách xếp hạng các sinh viên có điểm hệ 4 từ 3.2 trở lên
-- của một khoa trong một học kỳ với mã khoa, kỳ học, năm học là tham số đầu vào
CREATE FUNCTION fn_XepHangSinhVien_Khoa
(
    @MaKhoa VARCHAR(10),
    @NamHoc VARCHAR(20),
    @HocKy INT
)
RETURNS TABLE
AS
RETURN
(
    WITH DiemSV AS (
        SELECT 
            sv.MaSinhVien,
            sv.HoDem + ' ' + sv.Ten AS HoTen,
            AVG(lsv.DiemHe4) AS DiemTBHe4
        FROM SinhVien sv
        JOIN Lop l ON sv.MaLop = l.MaLop
        JOIN LopHocPhan_SinhVien lsv ON sv.MaSinhVien = lsv.MaSinhVien
        JOIN LopHocPhan lhp ON lsv.MaLopHocPhan = lhp.MaLopHocPhan
        WHERE l.MaKhoa = @MaKhoa
          AND lhp.NamHoc = @NamHoc
          AND lhp.HocKy = @HocKy
        GROUP BY sv.MaSinhVien, sv.HoDem, sv.Ten
        HAVING AVG(lsv.DiemHe4) >= 3.2
    )
    SELECT 
        MaSinhVien,
        HoTen,
        DiemTBHe4,
        RANK() OVER (ORDER BY DiemTBHe4 DESC) AS XepHang
    FROM DiemSV
);

SELECT * 
FROM fn_XepHangSinhVien_Khoa('CNTT', '2022-2023', 2);

-- 7. Tạo hàm để liệt kê tất cả các học phần mà sinh viên được phép học cải thiện
-- điểm (có điểm C, D, F) với mã sinh viên là tham số đầu vào.
-- 8. Tạo hàm để tính và thống kê điểm trung bình của tất cả sinh viên theo từng
-- chương trình đào tạo với mã chương trình đào tạo là tham số đầu vào
-- 9. Tạo hàm thống kê số sinh viên đạt điểm A, B, C, D, F cho một học phần trong
-- một kỳ học với tham số đầu vào là mã học phần, kỳ học, năm học.