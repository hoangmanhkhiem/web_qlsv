using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_qlsv.Migrations.QuanLySinhVienDb
{
    /// <inheritdoc />
    public partial class link_lhp_mh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LopHocPhan_IdMonHoc",
                table: "LopHocPhan",
                column: "IdMonHoc");

            migrationBuilder.AddForeignKey(
                name: "FK_LopHocPhan_MonHoc_IdMonHoc",
                table: "LopHocPhan",
                column: "IdMonHoc",
                principalTable: "MonHoc",
                principalColumn: "IdMonHoc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LopHocPhan_MonHoc_IdMonHoc",
                table: "LopHocPhan");

            migrationBuilder.DropIndex(
                name: "IX_LopHocPhan_IdMonHoc",
                table: "LopHocPhan");
        }
    }
}
