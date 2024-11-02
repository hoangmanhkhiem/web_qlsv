using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_qlsv.Migrations.QuanLySinhVienDb
{
    /// <inheritdoc />
    public partial class them_bang_nguyen_vong_doi_lich : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DangKyDoiLich",
                columns: table => new
                {
                    IdDangKyDoiLich = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "(newid())"),
                    IdThoiGian = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ThoiGianBatDauHienTai = table.Column<DateTime>(type: "datetime", nullable: false),
                    ThoiGianKetThucHienTai = table.Column<DateTime>(type: "datetime", nullable: false),
                    ThoiGianBatDauMoi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianKetThucMoi = table.Column<DateTime>(type: "datetime", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: -1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DangKyDoiLich", x => x.IdDangKyDoiLich);
                    table.ForeignKey(
                        name: "FK_DangKyDoiLich_ThoiGian_IdThoiGian",
                        column: x => x.IdThoiGian,
                        principalTable: "ThoiGian",
                        principalColumn: "IdThoiGian",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DangKyDoiLich_IdThoiGian",
                table: "DangKyDoiLich",
                column: "IdThoiGian");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DangKyDoiLich");
        }
    }
}
