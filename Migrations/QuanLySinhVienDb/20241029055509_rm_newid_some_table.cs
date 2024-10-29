using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_qlsv.Migrations.QuanLySinhVienDb
{
    /// <inheritdoc />
    public partial class rm_newid_some_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdMonHoc",
                table: "MonHoc",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<string>(
                name: "IdLopHocPhan",
                table: "LopHocPhan",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<string>(
                name: "IdGiaoVien",
                table: "GiaoVien",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValueSql: "(newid())");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdMonHoc",
                table: "MonHoc",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "IdLopHocPhan",
                table: "LopHocPhan",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "IdGiaoVien",
                table: "GiaoVien",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValueSql: "(newid())",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
