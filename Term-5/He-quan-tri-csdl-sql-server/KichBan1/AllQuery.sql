/* Kịch bản 1: 
- Tạo login A, B
– Tạo user userA, userB tương ứng với login A, B
– Gán quyền select, update cho userA trên bảng tKhachHang của CSDL QLBanSach, 
A có quyền trao quyền này cho người khác
– Đăng nhập A để kiểm tra
– Từ A, Trao quyền select cho userB trên bảng tKhachHang của CSDL QLBanSach
– Đăng nhập B để kiểm tra
*/

-- Bước 1: Tạo login A, B
create login A with password = '123'
exec sp_addlogin B, 123

-- Bước 2: Tạo user
create user userA for login A
exec sp_adduser B, userB

-- Bước 3: Gán quyền cho userA
use QLBanSach
grant select, update on tKhachHang to userA with grant option

-- Sử dụng Query của A
-- Bước 4: Login A và kiểm tra
select * from tKhachHang
delete from tKhachHang

-- Bước 5: userA trao quyền cho userB
grant select on tKhachHang to userB

-- Sử dụng Query của B
-- Bước 6: Login B và kiểm tra
select * from tKhachHang




