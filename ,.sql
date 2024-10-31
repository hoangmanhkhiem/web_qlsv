INSERT INTO GiaoVien (IdGiaoVien, TenGiaoVien, Email, SoDienThoai, IdKhoa)
VALUES(NEWID(),N'Phạm Xuân Tích','tichpx@utc.edu.vn','0987654321',(SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
(NEWID(),N'Nguyễn Thị Thanh Thảo','thao@utc.edu.vn','0123456789',(SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
(NEWID(),N'Lại Mạnh Dũng','dunglm@utc.edu.vn','0987654321',(SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
(NEWID(),N'Cao Thị Luyên','caoluyen@utc.edu.vn','0123456789',(SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
(NEWID(), N'Nguyễn Đình Dương', 'duongnd@utc.edu.vn', '0978332467', (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
(NEWID(), N'Nguyễn Thị Thu Hường', 'huongtt@utc.edu.vn', '0961423789', (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
(NEWID(), N'Bùi Ngọc Dũng', 'dungbn@utc.edu.vn', '0915473821', (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
(NEWID(), N'Nguyễn Đình Phong', 'phongnd@utc.edu.vn', '0985432167', (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
(NEWID(), N'Đào Thị Lệ Thủy', 'thuydt@utc.edu.vn', '0921374856', (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
(NEWID(), N'Đinh Công Tùng', 'tungdc@utc.edu.vn', '0935421789', (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
(NEWID(), N'Nguyễn Trần Hiếu', 'hieunt@utc.edu.vn', '0942365478', (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin'));