using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_qlsv.Migrations.QuanLySinhVienDb
{
    /// <inheritdoc />
    public partial class gioi_han_thoi_gian_lop_hoc_phan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ThoiGianBatDau",
                table: "LopHocPhan",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ThoiGianKetThuc",
                table: "LopHocPhan",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThoiGianBatDau",
                table: "LopHocPhan");

            migrationBuilder.DropColumn(
                name: "ThoiGianKetThuc",
                table: "LopHocPhan");
        }
    }
}
