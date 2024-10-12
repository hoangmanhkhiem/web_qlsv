using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_qlsv.Migrations.QuanLySinhVienDb
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GiaoVien",
                columns: table => new
                {
                    IdGiaoVien = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(newid())"),
                    TenGiaoVien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ChuyenMon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GiaoVien__F4372AE328C6DBED", x => x.IdGiaoVien);
                });

            migrationBuilder.CreateTable(
                name: "SinhVien",
                columns: table => new
                {
                    IdSinhVien = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(newid())"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lop = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ChuyenNganh = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "date", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SinhVien__93ABE5766A04A8ED", x => x.IdSinhVien);
                });

            migrationBuilder.CreateTable(
                name: "ThoiGian",
                columns: table => new
                {
                    IdThoiGian = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(newid())"),
                    Ngay = table.Column<DateTime>(type: "date", nullable: false),
                    ThoiGianBD = table.Column<TimeSpan>(type: "time", nullable: false),
                    ThoiGianKT = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ThoiGian__EDEFED40D5785B54", x => x.IdThoiGian);
                });

            migrationBuilder.CreateTable(
                name: "LopHocPhan",
                columns: table => new
                {
                    IdHocPhan = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValueSql: "(newid())"),
                    TenHocPhan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdGiaoVien = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LopHocPh__6A35A4EC70344C38", x => x.IdHocPhan);
                    table.ForeignKey(
                        name: "FK__LopHocPha__IdGia__4D94879B",
                        column: x => x.IdGiaoVien,
                        principalTable: "GiaoVien",
                        principalColumn: "IdGiaoVien");
                });

            migrationBuilder.CreateTable(
                name: "Diem",
                columns: table => new
                {
                    IdLopHocPhan = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    IdSinhVien = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    DiemQuaTrinh = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    DiemKetThuc = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    DiemTongKet = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Diem__72255A5ACAFEA388", x => new { x.IdLopHocPhan, x.IdSinhVien });
                    table.ForeignKey(
                        name: "FK__Diem__IdLopHocPh__5EBF139D",
                        column: x => x.IdLopHocPhan,
                        principalTable: "LopHocPhan",
                        principalColumn: "IdHocPhan");
                    table.ForeignKey(
                        name: "FK__Diem__IdSinhVien__5FB337D6",
                        column: x => x.IdSinhVien,
                        principalTable: "SinhVien",
                        principalColumn: "IdSinhVien");
                });

            migrationBuilder.CreateTable(
                name: "DiemDanh",
                columns: table => new
                {
                    IdSinhVien = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    IdThoiGian = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    IdLopHocPhan = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    DaDiemDanh = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DiemDanh__817E0446A874C894", x => new { x.IdSinhVien, x.IdThoiGian, x.IdLopHocPhan });
                    table.ForeignKey(
                        name: "FK__DiemDanh__IdLopH__5812160E",
                        column: x => x.IdLopHocPhan,
                        principalTable: "LopHocPhan",
                        principalColumn: "IdHocPhan");
                    table.ForeignKey(
                        name: "FK__DiemDanh__IdSinh__5629CD9C",
                        column: x => x.IdSinhVien,
                        principalTable: "SinhVien",
                        principalColumn: "IdSinhVien");
                    table.ForeignKey(
                        name: "FK__DiemDanh__IdThoi__571DF1D5",
                        column: x => x.IdThoiGian,
                        principalTable: "ThoiGian",
                        principalColumn: "IdThoiGian");
                });

            migrationBuilder.CreateTable(
                name: "SinhVien_LopHocPhan",
                columns: table => new
                {
                    IdSinhVien = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    IdLopHocPhan = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SinhVien__531A1B36CDEDAB4C", x => new { x.IdSinhVien, x.IdLopHocPhan });
                    table.ForeignKey(
                        name: "FK__SinhVien___IdLop__6383C8BA",
                        column: x => x.IdLopHocPhan,
                        principalTable: "LopHocPhan",
                        principalColumn: "IdHocPhan");
                    table.ForeignKey(
                        name: "FK__SinhVien___IdSin__628FA481",
                        column: x => x.IdSinhVien,
                        principalTable: "SinhVien",
                        principalColumn: "IdSinhVien");
                });

            migrationBuilder.CreateTable(
                name: "ThoiGian_LopHocPhan",
                columns: table => new
                {
                    IdLopHocPhan = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    IdThoiGian = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ThoiGian__15C11AD9B63A9B11", x => new { x.IdLopHocPhan, x.IdThoiGian });
                    table.ForeignKey(
                        name: "FK__ThoiGian___IdLop__5AEE82B9",
                        column: x => x.IdLopHocPhan,
                        principalTable: "LopHocPhan",
                        principalColumn: "IdHocPhan");
                    table.ForeignKey(
                        name: "FK__ThoiGian___IdTho__5BE2A6F2",
                        column: x => x.IdThoiGian,
                        principalTable: "ThoiGian",
                        principalColumn: "IdThoiGian");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diem_IdSinhVien",
                table: "Diem",
                column: "IdSinhVien");

            migrationBuilder.CreateIndex(
                name: "IX_DiemDanh_IdLopHocPhan",
                table: "DiemDanh",
                column: "IdLopHocPhan");

            migrationBuilder.CreateIndex(
                name: "IX_DiemDanh_IdThoiGian",
                table: "DiemDanh",
                column: "IdThoiGian");

            migrationBuilder.CreateIndex(
                name: "IX_LopHocPhan_IdGiaoVien",
                table: "LopHocPhan",
                column: "IdGiaoVien");

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_LopHocPhan_IdLopHocPhan",
                table: "SinhVien_LopHocPhan",
                column: "IdLopHocPhan");

            migrationBuilder.CreateIndex(
                name: "IX_ThoiGian_LopHocPhan_IdThoiGian",
                table: "ThoiGian_LopHocPhan",
                column: "IdThoiGian");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diem");

            migrationBuilder.DropTable(
                name: "DiemDanh");

            migrationBuilder.DropTable(
                name: "SinhVien_LopHocPhan");

            migrationBuilder.DropTable(
                name: "ThoiGian_LopHocPhan");

            migrationBuilder.DropTable(
                name: "SinhVien");

            migrationBuilder.DropTable(
                name: "LopHocPhan");

            migrationBuilder.DropTable(
                name: "ThoiGian");

            migrationBuilder.DropTable(
                name: "GiaoVien");
        }
    }
}
