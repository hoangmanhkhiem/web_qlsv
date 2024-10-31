using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_qlsv.Migrations.QuanLySinhVienDb
{
    /// <inheritdoc />
    public partial class fix_trang_thai_nguyen_vong_int : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TrangThai",
                table: "DangKyNguyenVong",
                type: "int",
                nullable: false,
                defaultValue: -1,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "TrangThai",
                table: "DangKyNguyenVong",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: -1);
        }
    }
}
