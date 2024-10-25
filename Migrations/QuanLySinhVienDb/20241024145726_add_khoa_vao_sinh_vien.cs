using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_qlsv.Migrations.QuanLySinhVienDb
{
    /// <inheritdoc />
    public partial class add_khoa_vao_sinh_vien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdKhoa",
                table: "SinhVien",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_IdKhoa",
                table: "SinhVien",
                column: "IdKhoa");

            migrationBuilder.AddForeignKey(
                name: "FK_SinhVien_Khoa_IdKhoa",
                table: "SinhVien",
                column: "IdKhoa",
                principalTable: "Khoa",
                principalColumn: "IdKhoa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SinhVien_Khoa_IdKhoa",
                table: "SinhVien");

            migrationBuilder.DropIndex(
                name: "IX_SinhVien_IdKhoa",
                table: "SinhVien");

            migrationBuilder.DropColumn(
                name: "IdKhoa",
                table: "SinhVien");
        }
    }
}
