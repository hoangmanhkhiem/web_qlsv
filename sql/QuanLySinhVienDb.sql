-- Chuong trinh hoc
INSERT INTO ChuongTrinhHoc (IdChuongTrinhHoc, TenChuongTrinhHoc)
VALUES 
    (NEWID(), N'CLC Công nghệ thông tin'),
    (NEWID(), N'Công nghệ thông tin'),
    (NEWID(), N'Khoa học máy tính'),
    (NEWID(), N'Toán ứng dụng'),
    (NEWID(), N'Logistics và quản lý chuỗi cung ứng');

-- Khoa
INSERT INTO Khoa (IdKhoa, TenKhoa)
VALUES 
    ('CNTT', N'Công nghệ thông tin'),
    ('KTE', N'Kinh tế'),
    ('KHCB', N'Khoa học cơ bản'),
    ('CDB', N'Cầu đường'),
    ('DTQT', N'Đào tạo quốc tế');

-- Giao Vien
INSERT INTO dbo.GiaoVien (IdGiaoVien, TenGiaoVien, Email, SoDienThoai, IdKhoa)
VALUES
    ('dungnb01', N'Bùi Ngọc Dũng', 'dungbn@utc.edu.vn', '0915473821', 'CNTT'),
    ('luyenct01', N'Cao Thị Luyên', 'caoluyen@utc.edu.vn', '0123456789', 'CNTT'),
    ('dunglm01', N'Lại Mạnh Dũng', 'dunglm@utc.edu.vn', '0987654321', 'CNTT'),
    ('thaontt01', N'Nguyễn Thị Thanh Thảo', 'thao@utc.edu.vn', '0123456789', 'CNTT'),
    ('huongntt01', N'Nguyễn Thị Thu Hường', 'huongtt@utc.edu.vn', '0961423789', 'CNTT'),
    ('hieunt01', N'Nguyễn Trần Hiếu', 'hieunt@utc.edu.vn', '0942365478', 'CNTT'),
    ('duongnd01', N'Nguyễn Đình Dương', 'duongnd@utc.edu.vn', '0978332467', 'CNTT'),
    ('phongpd01', N'Phạm Đình Phong', 'phongnd@utc.edu.vn', '0985432167', 'CNTT'),
    ('tichpx01', N'Phạm Xuân Tích', 'tichpx@utc.edu.vn', '0987654321', 'CNTT'),
    ('tungdc01', N'Đinh Công Tùng', 'tungdc@utc.edu.vn', '0935421789', 'CNTT'),
    ('thuydtl01', N'Đào Thị Lệ Thủy', 'thuydt@utc.edu.vn', '0921374856', 'CNTT');

-- Thoi gian
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-14 15:35:00', '2024-08-14 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-21 15:35:00', '2024-08-21 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-28 15:35:00', '2024-08-28 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-04 15:35:00', '2024-09-04 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-11 15:35:00', '2024-09-11 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-18 15:35:00', '2024-09-18 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-25 15:35:00', '2024-09-25 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-02 15:35:00', '2024-10-02 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-09 15:35:00', '2024-10-09 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-16 15:35:00', '2024-10-16 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-23 15:35:00', '2024-10-23 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-30 15:35:00', '2024-10-30 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-12 15:35:00', '2024-10-12 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-19 15:35:00', '2024-10-19 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-26 15:35:00', '2024-10-26 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-11-02 15:35:00', '2024-11-02 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-11-09 15:35:00', '2024-11-09 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-11-16 15:35:00', '2024-11-16 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-13 15:35:00', '2024-08-13 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-20 15:35:00', '2024-08-20 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-17 15:35:00', '2024-08-17 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-24 15:35:00', '2024-08-24 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-27 15:35:00', '2024-08-27 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-07 15:35:00', '2024-09-07 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-14 15:35:00', '2024-09-14 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-21 15:35:00', '2024-09-21 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-28 15:35:00', '2024-09-28 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-11-23 15:35:00', '2024-11-23 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-11 13:00:00', '2024-10-11 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-18 13:00:00', '2024-10-18 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-25 13:00:00', '2024-10-25 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-11-01 13:00:00', '2024-11-01 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-11-08 13:00:00', '2024-11-08 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-11-15 13:00:00', '2024-11-15 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-12 07:00:00', '2024-10-12 09:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-19 07:00:00', '2024-10-19 09:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-26 07:00:00', '2024-10-26 09:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-11-02 07:00:00', '2024-11-02 09:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-11-09 07:00:00', '2024-11-09 09:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-11-16 07:00:00', '2024-11-16 09:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-12 13:00:00', '2024-08-12 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-19 13:00:00', '2024-08-19 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-26 13:00:00', '2024-08-26 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-06 15:35:00', '2024-09-06 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-09 13:00:00', '2024-09-09 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-16 13:00:00', '2024-09-16 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-13 15:35:00', '2024-09-13 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-20 15:35:00', '2024-09-20 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-23 13:00:00', '2024-09-23 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-30 13:00:00', '2024-09-30 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-11-18 13:00:00', '2024-11-18 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-15 09:35:00', '2024-10-15 12:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-22 09:35:00', '2024-10-22 12:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-29 09:35:00', '2024-10-29 12:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-11-05 09:35:00', '2024-11-05 12:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-13 09:35:00', '2024-08-13 12:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-20 09:35:00', '2024-08-20 12:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-27 09:35:00', '2024-08-27 12:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-03 09:35:00', '2024-09-03 12:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-10 09:35:00', '2024-09-10 12:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-17 09:35:00', '2024-09-17 12:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-24 09:35:00', '2024-09-24 12:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-01 09:35:00', '2024-10-01 12:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-08 09:35:00', '2024-10-08 12:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-12 13:00:00', '2024-10-12 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-19 13:00:00', '2024-10-19 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-26 13:00:00', '2024-10-26 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-11-02 13:00:00', '2024-11-02 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-11-09 13:00:00', '2024-11-09 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-11-16 13:00:00', '2024-11-16 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-13 13:00:00', '2024-08-13 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-20 13:00:00', '2024-08-20 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-17 13:00:00', '2024-08-17 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-24 13:00:00', '2024-08-24 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-27 13:00:00', '2024-08-27 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-07 13:00:00', '2024-09-07 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-14 13:00:00', '2024-09-14 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-21 13:00:00', '2024-09-21 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-28 13:00:00', '2024-09-28 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-05 13:00:00', '2024-10-05 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-26 15:35:00', '2024-09-26 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-03 15:35:00', '2024-10-03 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-10 15:35:00', '2024-10-10 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-17 15:35:00', '2024-10-17 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-24 15:35:00', '2024-10-24 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-31 15:35:00', '2024-10-31 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-15 15:35:00', '2024-08-15 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-22 15:35:00', '2024-08-22 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-29 15:35:00', '2024-08-29 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-05 15:35:00', '2024-09-05 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-12 15:35:00', '2024-09-12 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-19 15:35:00', '2024-09-19 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-15 13:00:00', '2024-08-15 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-22 13:00:00', '2024-08-22 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-29 13:00:00', '2024-08-29 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-05 13:00:00', '2024-09-05 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-12 13:00:00', '2024-09-12 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-19 13:00:00', '2024-09-19 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-26 13:00:00', '2024-09-26 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-03 13:00:00', '2024-10-03 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-10 13:00:00', '2024-10-10 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-17 13:00:00', '2024-10-17 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-24 13:00:00', '2024-10-24 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-31 13:00:00', '2024-10-31 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-11 15:35:00', '2024-10-11 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-18 15:35:00', '2024-10-18 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-10-25 15:35:00', '2024-10-25 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-11-01 15:35:00', '2024-11-01 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-11-08 15:35:00', '2024-11-08 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-11-15 15:35:00', '2024-11-15 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-12 15:35:00', '2024-08-12 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-19 15:35:00', '2024-08-19 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-08-26 15:35:00', '2024-08-26 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-06 13:00:00', '2024-09-06 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-09 15:35:00', '2024-09-09 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-16 15:35:00', '2024-09-16 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-13 13:00:00', '2024-09-13 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-20 13:00:00', '2024-09-20 15:25:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-09-23 15:35:00', '2024-09-23 18:00:00');
INSERT INTO ThoiGian (IdThoiGian, NgayBatDau, NgayKetThuc) VALUES (NEWID(), '2024-11-18 15:35:00', '2024-11-18 18:00:00');

-- Chuong trinh hoc - Mon hoc
INSERT INTO ChuongTrinhHoc_MonHoc (IdCTHMonHoc, IdChuongTrinhHoc, IdMonHoc)
SELECT NEWID(), 
       (SELECT IdChuongTrinhHoc FROM ChuongTrinhHoc WHERE TenChuongTrinhHoc = N'Công nghệ thông tin'),
       IdMonHoc
FROM MonHoc;

INSERT INTO ChuongTrinhHoc_MonHoc (IdCTHMonHoc, IdChuongTrinhHoc, IdMonHoc)
SELECT NEWID(), 
       (SELECT IdChuongTrinhHoc FROM ChuongTrinhHoc WHERE TenChuongTrinhHoc = N'CLC Công nghệ thông tin'),
       IdMonHoc
FROM MonHoc;


INSERT INTO SinhVien (IdSinhVien, HoTen, Lop, NgaySinh, DiaChi, IdChuongTrinhHoc, IdKhoa)
VALUES
    ('222631111', N'Hoàng Mạnh Khiêm', N'Công nghệ thông tin Việt Anh 1', '2004-10-01', N'Hà Nội', 
        (SELECT IdChuongTrinhHoc FROM ChuongTrinhHoc WHERE TenChuongTrinhHoc = N'CLC Công nghệ thông tin'), 
        (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
    ('222631159', N'Lý Trần Vinh', N'Công nghệ thông tin Việt Anh 1', '2004-05-12', N'Nghệ An', 
        (SELECT IdChuongTrinhHoc FROM ChuongTrinhHoc WHERE TenChuongTrinhHoc = N'CLC Công nghệ thông tin'), 
        (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Đào tạo quốc tế')),
    ('222631160', N'Le Van C', N'Công nghệ thông tin 1', '2002-02-15', N'Hải Phòng', 
        (SELECT IdChuongTrinhHoc FROM ChuongTrinhHoc WHERE TenChuongTrinhHoc = N'Công nghệ thông tin'), 
        (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
    ('222631161', N'Pham Thi J', N'Công nghệ thông tin 3', '2004-07-07', N'Hà Nội', 
        (SELECT IdChuongTrinhHoc FROM ChuongTrinhHoc WHERE TenChuongTrinhHoc = N'Công nghệ thông tin'), 
        (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin'));

-- Mon Hoc
INSERT INTO MonHoc (IdMonHoc, TenMonHoc, SoTinChi, SoTietHoc, IdKhoa) 
VALUES 
    ('HQTCSDL', N'Hệ quản trị cơ sở dữ liệu Oracle', 3, 45, (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
    ('LTTT', N'Lập trình trực quan', 3, 45, (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
    ('LTW', N'Lập trình Web', 3, 45, (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
    ('LSDCSVN', N'Lịch sử Đảng Cộng sản Việt Nam', 3, 45, (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
    ('MMT', N'Mạng máy tính', 3, 45, (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
    ('PTTKYC', N'Phân tích thiết kế yêu cầu', 3, 45, (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
    ('TTUD', N'Thuật toán và ứng dụng', 3, 45, (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
    ('XSTK', N'Xác suất thống kê', 3, 45, (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Khoa học cơ bản'));
-- Tạo các lớp học phần với Id và tên giáo viên chỉ định
INSERT INTO LopHocPhan (IdLopHocPhan, TenHocPhan, IdGiaoVien, IdMonHoc)
VALUES
    ('HTQTORCL_QT01', N'Hệ quản trị cơ sở dữ liệu Oracle-1-1-24(QT01)', (SELECT IdGiaoVien FROM GiaoVien WHERE TenGiaoVien = N'Bùi Ngọc Dũng'), (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Hệ quản trị cơ sở dữ liệu Oracle')),
    ('HTQTORCL_QT01_BT1', N'Hệ quản trị cơ sở dữ liệu Oracle-1-1-24(QT01.BT1)', (SELECT IdGiaoVien FROM GiaoVien WHERE TenGiaoVien = N'Bùi Ngọc Dũng'), (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Hệ quản trị cơ sở dữ liệu Oracle')),
    
    ('LTTT_QT01', N'Lập trình trực quan-1-1-24(QT01)', (SELECT IdGiaoVien FROM GiaoVien WHERE TenGiaoVien = N'Lại Mạnh Dũng'), (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Lập trình trực quan')),
    ('LTTT_QT01_BT1', N'Lập trình trực quan-1-1-24(QT01.BT1)', (SELECT IdGiaoVien FROM GiaoVien WHERE TenGiaoVien = N'Lại Mạnh Dũng'), (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Lập trình trực quan')),
    
    ('LTW_QT01', N'Lập trình Web-1-1-24(QT01)', (SELECT IdGiaoVien FROM GiaoVien WHERE TenGiaoVien = N'Cao Thị Luyên'), (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Lập trình Web')),
    ('LTW_QT01_BT1', N'Lập trình Web-1-1-24(QT01.BT1)', (SELECT IdGiaoVien FROM GiaoVien WHERE TenGiaoVien = N'Cao Thị Luyên'), (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Lập trình Web')),
    
    ('LSDCSVN_QT05', N'Lịch sử Đảng Cộng sản Việt Nam-1-1-24(QT05)', (SELECT IdGiaoVien FROM GiaoVien WHERE TenGiaoVien = N'Nguyễn Thị Thanh Thảo'), (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Lịch sử Đảng Cộng sản Việt Nam')),
    ('LSDCSVN_QT05_BT1', N'Lịch sử Đảng Cộng sản Việt Nam-1-1-24(QT05.TL1)', (SELECT IdGiaoVien FROM GiaoVien WHERE TenGiaoVien = N'Nguyễn Thị Thanh Thảo'), (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Lịch sử Đảng Cộng sản Việt Nam')),
    
    ('MMT_QT01', N'Mạng máy tính-1-1-24(QT01)', (SELECT IdGiaoVien FROM GiaoVien WHERE TenGiaoVien = N'Phạm Đình Phong'), (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Mạng máy tính')),
    ('MMT_QT01_BT1', N'Mạng máy tính-1-1-24(QT01.BT1)', (SELECT IdGiaoVien FROM GiaoVien WHERE TenGiaoVien = N'Phạm Đình Phong'), (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Mạng máy tính')),
    
    ('PTTKYC_QT01', N'Phân tích thiết kế yêu cầu-1-1-24(QT01)', (SELECT IdGiaoVien FROM GiaoVien WHERE TenGiaoVien = N'Nguyễn Đình Dương'), (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Phân tích thiết kế yêu cầu')),
    ('PTTKYC_QT01_BT1', N'Phân tích thiết kế yêu cầu-1-1-24(QT01.BT1)', (SELECT IdGiaoVien FROM GiaoVien WHERE TenGiaoVien = N'Nguyễn Đình Dương'), (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Phân tích thiết kế yêu cầu')),
    
    ('TTUD_QT01', N'Thuật toán và ứng dụng-1-1-24(QT01)', (SELECT IdGiaoVien FROM GiaoVien WHERE TenGiaoVien = N'Phạm Xuân Tích'), (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Thuật toán và ứng dụng')),
    ('TTUD_QT01_BT1', N'Thuật toán và ứng dụng-1-1-24(QT01.BT1)', (SELECT IdGiaoVien FROM GiaoVien WHERE TenGiaoVien = N'Phạm Xuân Tích'), (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Thuật toán và ứng dụng')),
    
    ('XSTK_QT01', N'Xác suất thống kê-1-1-24(QT01)', (SELECT IdGiaoVien FROM GiaoVien WHERE TenGiaoVien = N'Nguyễn Trần Hiếu'), (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Xác suất thống kê')),
    ('XSTK_QT01_BT1', N'Xác suất thống kê-1-1-24(QT01.BT1)', (SELECT IdGiaoVien FROM GiaoVien WHERE TenGiaoVien = N'Nguyễn Trần Hiếu'), (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Xác suất thống kê'));

-- Thêm sinh viên vào các lớp học phần dựa trên dữ liệu có sẵn
INSERT INTO SinhVien_LopHocPhan (IdSinhVienLopHP, IdSinhVien, IdLopHocPhan)
VALUES
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Hoàng Mạnh Khiêm'), 'LSDCSVN_QT05'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Hoàng Mạnh Khiêm'), 'LTW_QT01'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Lý Trần Vinh'),'XSTK_QT01'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Le Van C'),'MMT_QT01'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Pham Thi J'), 'TTUD_QT01'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Hoàng Mạnh Khiêm'), 'LTTT_QT01'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Lý Trần Vinh'), 'PTTKYC_QT01'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Le Van C'), 'TTUD_QT01'),
    (NEWID(), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Pham Thi J'),'XSTK_QT01' );

-- Thêm điểm cho sinh viên vào các lớp học phần dựa trên dữ liệu có sẵn
INSERT INTO Diem (IdDiem, IdLopHocPhan, IdSinhVien, DiemQuaTrinh, DiemKetThuc, DiemTongKet, LanHoc)
VALUES
    (NEWID(), (SELECT IdLopHocPhan FROM LopHocPhan WHERE TenHocPhan = N'Lập trình Web-1-1-24(QT01)'), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Hoàng Mạnh Khiêm'), 8.0, 7.5, 7.8, 1),
    (NEWID(), (SELECT IdLopHocPhan FROM LopHocPhan WHERE TenHocPhan = N'Xác suất thống kê-1-1-24(QT01.BT1)'), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Lý Trần Vinh'), 6.5, 8.0, 7.2, 1),
    (NEWID(), (SELECT IdLopHocPhan FROM LopHocPhan WHERE TenHocPhan = N'Mạng máy tính-1-1-24(QT01)'), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Le Van C'), 7.5, 9.0, 8.1, 1),
    (NEWID(), (SELECT IdLopHocPhan FROM LopHocPhan WHERE TenHocPhan = N'Thuật toán và ứng dụng-1-1-24(QT01)'), (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Pham Thi J'), 5.0, 6.5, 6.0, 1),
    (NEWID(), 'LSDCSVN_QT05', (SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Hoàng Mạnh Khiêm'), 8.0, 7.5, 4.5, 1);


-- Nguyện vọng cho các sinh viên vào các môn học
INSERT INTO DangKyNguyenVong (IdDangKyNguyenVong, IdSinhVien, IdMonHoc)
VALUES
    (NEWID(),'222631111' , 'LSDCSVN'),
    (NEWID(),'222631111' , (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Lập trình Web')),
    (NEWID(), '222631111', (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Xác suất thống kê')),
    (NEWID(), '222631159', (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Lập trình Web')),
    (NEWID(), '222631159', (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Xác suất thống kê'));


-- Tạo các bản ghi thời gian tương ứng trong bảng ThoiGian_LopHocPhan
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'HTQTORCL_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-14 15:35:00' AND NgayKetThuc = '2024-08-14 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'HTQTORCL_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-21 15:35:00' AND NgayKetThuc = '2024-08-21 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'HTQTORCL_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-28 15:35:00' AND NgayKetThuc = '2024-08-28 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'HTQTORCL_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-04 15:35:00' AND NgayKetThuc = '2024-09-04 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'HTQTORCL_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-11 15:35:00' AND NgayKetThuc = '2024-09-11 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'HTQTORCL_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-18 15:35:00' AND NgayKetThuc = '2024-09-18 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTTT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-12 15:35:00' AND NgayKetThuc = '2024-10-12 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTTT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-13 15:35:00' AND NgayKetThuc = '2024-08-13 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTTT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-17 15:35:00' AND NgayKetThuc = '2024-08-17 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTTT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-27 15:35:00' AND NgayKetThuc = '2024-08-27 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTTT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-14 15:35:00' AND NgayKetThuc = '2024-09-14 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTTT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-11-12 15:35:00' AND NgayKetThuc = '2024-11-12 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTTT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-11-23 15:35:00' AND NgayKetThuc = '2024-11-23 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-11 13:00:00' AND NgayKetThuc = '2024-10-11 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-12 07:00:00' AND NgayKetThuc = '2024-10-12 09:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-12 13:00:00' AND NgayKetThuc = '2024-08-12 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-06 15:35:00' AND NgayKetThuc = '2024-09-06 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-09 13:00:00' AND NgayKetThuc = '2024-09-09 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-13 15:35:00' AND NgayKetThuc = '2024-09-13 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-23 13:00:00' AND NgayKetThuc = '2024-09-23 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'LTW_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-11-18 13:00:00' AND NgayKetThuc = '2024-11-18 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-12 13:00:00' AND NgayKetThuc = '2024-10-12 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-13 13:00:00' AND NgayKetThuc = '2024-08-13 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-17 13:00:00' AND NgayKetThuc = '2024-08-17 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-27 13:00:00' AND NgayKetThuc = '2024-08-27 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-07 13:00:00' AND NgayKetThuc = '2024-09-07 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PTTKYC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-26 15:35:00' AND NgayKetThuc = '2024-09-26 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PTTKYC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-15 15:35:00' AND NgayKetThuc = '2024-08-15 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-13 13:00:00' AND NgayKetThuc = '2024-08-13 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-17 13:00:00' AND NgayKetThuc = '2024-08-17 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-27 13:00:00' AND NgayKetThuc = '2024-08-27 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'MMT_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-07 13:00:00' AND NgayKetThuc = '2024-09-07 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PTTKYC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-26 15:35:00' AND NgayKetThuc = '2024-09-26 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PTTKYC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-15 15:35:00' AND NgayKetThuc = '2024-08-15 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PTTKYC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-26 15:35:00' AND NgayKetThuc = '2024-09-26 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'PTTKYC_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-15 15:35:00' AND NgayKetThuc = '2024-08-15 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'TTUD_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-15 13:00:00' AND NgayKetThuc = '2024-08-15 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'TTUD_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-26 13:00:00' AND NgayKetThuc = '2024-09-26 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'XSTK_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-11 15:35:00' AND NgayKetThuc = '2024-10-11 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'XSTK_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-08-12 15:35:00' AND NgayKetThuc = '2024-08-12 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'XSTK_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-06 13:00:00' AND NgayKetThuc = '2024-09-06 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'XSTK_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-09 15:35:00' AND NgayKetThuc = '2024-09-09 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'XSTK_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-13 13:00:00' AND NgayKetThuc = '2024-09-13 15:25:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'XSTK_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-23 15:35:00' AND NgayKetThuc = '2024-09-23 18:00:00'));
INSERT INTO ThoiGian_LopHocPhan (IdThoigianLopHP, IdLopHocPhan, IdThoiGian) VALUES (NEWID(), 'XSTK_QT01', (SELECT TOP 1 IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-11-18 15:35:00' AND NgayKetThuc = '2024-11-18 18:00:00'));    