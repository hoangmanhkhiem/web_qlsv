-- 1. Bảng Khoa
INSERT INTO Khoa (TenKhoa)
VALUES 
  (N'Công nghệ thông tin'),
  (N'Kinh tế');

-- 2. Bảng ChuongTrinhHoc
INSERT INTO ChuongTrinhHoc (TenChuongTrinhHoc)
VALUES 
  (N'Kỹ thuật phần mềm'),
  (N'Kinh tế vi mô');

-- 3. Bảng SinhVien
INSERT INTO SinhVien (HoTen, Lop, NgaySinh, DiaChi, IdChuongTrinhHoc, IdKhoa)
VALUES 
  (N'Nguyễn Văn A', 'CNTT01', '2001-05-21', N'Hà Nội', 
   (SELECT IdChuongTrinhHoc FROM ChuongTrinhHoc WHERE TenChuongTrinhHoc = N'Kỹ thuật phần mềm'), 
   (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
  
  (N'Trần Thị B', 'CNTT01', '2001-06-15', N'Hải Phòng', 
   (SELECT IdChuongTrinhHoc FROM ChuongTrinhHoc WHERE TenChuongTrinhHoc = N'Kỹ thuật phần mềm'), 
   (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
  
  (N'Lê Văn C', 'KT02', '2001-07-10', N'Quảng Ninh', 
   (SELECT IdChuongTrinhHoc FROM ChuongTrinhHoc WHERE TenChuongTrinhHoc = N'Kinh tế vi mô'), 
   (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Kinh tế'));

-- 4. Bảng GiaoVien
INSERT INTO GiaoVien (TenGiaoVien, Email, SoDienThoai, IdKhoa)
VALUES 
  (N'Phạm Thị B', 'phamthib@example.com', '0123456789', 
   (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
  
  (N'Nguyễn Văn D', 'nguyenvand@example.com', '0987654321', 
   (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
  
  (N'Vũ Văn E', 'vuvane@example.com', '0345678901', 
   (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Kinh tế'));

-- 5. Bảng MonHoc
INSERT INTO MonHoc (TenMonHoc, SoTinChi, SoTietHoc, IdKhoa)
VALUES 
  (N'Lập trình C#', 3, 45, (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
  (N'Cơ sở dữ liệu', 3, 45, (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Công nghệ thông tin')),
  (N'Kinh tế vi mô', 2, 30, (SELECT IdKhoa FROM Khoa WHERE TenKhoa = N'Kinh tế'));

-- 6. Bảng LopHocPhan
INSERT INTO LopHocPhan (TenHocPhan, IdGiaoVien, IdMonHoc)
VALUES 
  (N'Lớp C# - Nhóm 1', 
   (SELECT IdGiaoVien FROM GiaoVien WHERE TenGiaoVien = N'Phạm Thị B'), 
   (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Lập trình C#')),
  
  (N'Lớp Cơ sở dữ liệu - Nhóm 1', 
   (SELECT IdGiaoVien FROM GiaoVien WHERE TenGiaoVien = N'Nguyễn Văn D'), 
   (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Cơ sở dữ liệu')),
  
  (N'Lớp Kinh tế vi mô - Nhóm 1', 
   (SELECT IdGiaoVien FROM GiaoVien WHERE TenGiaoVien = N'Vũ Văn E'), 
   (SELECT IdMonHoc FROM MonHoc WHERE TenMonHoc = N'Kinh tế vi mô'));

-- 7. Bảng ThoiGian
INSERT INTO ThoiGian (NgayBatDau, NgayKetThuc)
VALUES 
  ('2024-10-01', '2024-12-31'),
  ('2024-11-01', '2025-01-31'),
  ('2024-09-15', '2024-11-15');

-- 8. Bảng SinhVien_LopHocPhan
INSERT INTO SinhVien_LopHocPhan (IdSinhVien, IdLopHocPhan)
VALUES 
  ((SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Nguyễn Văn A'), 
   (SELECT IdLopHocPhan FROM LopHocPhan WHERE TenHocPhan = N'Lớp C# - Nhóm 1')),
  
  ((SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Nguyễn Văn A'), 
   (SELECT IdLopHocPhan FROM LopHocPhan WHERE TenHocPhan = N'Lớp Cơ sở dữ liệu - Nhóm 1')),
  
  ((SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Trần Thị B'), 
   (SELECT IdLopHocPhan FROM LopHocPhan WHERE TenHocPhan = N'Lớp C# - Nhóm 1')),
  
  ((SELECT IdSinhVien FROM SinhVien WHERE HoTen = N'Lê Văn C'), 
   (SELECT IdLopHocPhan FROM LopHocPhan WHERE TenHocPhan = N'Lớp Kinh tế vi mô - Nhóm 1'));

-- 9. Bảng ThoiGian_LopHocPhan
INSERT INTO ThoiGian_LopHocPhan (IdLopHocPhan, IdThoiGian)
VALUES 
  ((SELECT IdLopHocPhan FROM LopHocPhan WHERE TenHocPhan = N'Lớp C# - Nhóm 1'), 
   (SELECT IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-10-01')),
  
  ((SELECT IdLopHocPhan FROM LopHocPhan WHERE TenHocPhan = N'Lớp Cơ sở dữ liệu - Nhóm 1'), 
   (SELECT IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-11-01')),
  
  ((SELECT IdLopHocPhan FROM LopHocPhan WHERE TenHocPhan = N'Lớp Kinh tế vi mô - Nhóm 1'), 
   (SELECT IdThoiGian FROM ThoiGian WHERE NgayBatDau = '2024-09-15'));
