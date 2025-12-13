exec sp_addlogin A, 123
exec sp_addlogin B, 123

use QLBanSach
exec sp_adduser B, userB
exec sp_adduser A, userA

grant select, update on tKhachHang to userA WITH GRANT OPTION