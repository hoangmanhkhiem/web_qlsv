using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_qlsv.Migrations.QuanLySinhVienDb
{
    /// <inheritdoc />
    public partial class fix_db_la_cuoi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdSinhVien",
                table: "SinhVien",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<string>(
                name: "IdKhoa",
                table: "Khoa",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<string>(
                name: "IdDiem",
                table: "Diem",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdSinhVien",
                table: "SinhVien",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "IdKhoa",
                table: "Khoa",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "IdDiem",
                table: "Diem",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValueSql: "(newid())");
        }
    }
}
