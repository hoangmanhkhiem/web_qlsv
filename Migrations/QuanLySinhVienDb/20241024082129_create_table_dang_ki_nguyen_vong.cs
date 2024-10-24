using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_qlsv.Migrations.QuanLySinhVienDb
{
    /// <inheritdoc />
    public partial class create_table_dang_ki_nguyen_vong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DangKyNguyenVong",
                columns: table => new
                {
                    IdDangKyNguyenVong = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "(newid())"),
                    IdSinhVien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdMonHoc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DangKyNguyenVong", x => x.IdDangKyNguyenVong);
                    table.ForeignKey(
                        name: "FK_DangKyNguyenVong_MonHoc_IdMonHoc",
                        column: x => x.IdMonHoc,
                        principalTable: "MonHoc",
                        principalColumn: "IdMonHoc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DangKyNguyenVong_SinhVien_IdSinhVien",
                        column: x => x.IdSinhVien,
                        principalTable: "SinhVien",
                        principalColumn: "IdSinhVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DangKyNguyenVong_IdMonHoc",
                table: "DangKyNguyenVong",
                column: "IdMonHoc");

            migrationBuilder.CreateIndex(
                name: "IX_DangKyNguyenVong_IdSinhVien",
                table: "DangKyNguyenVong",
                column: "IdSinhVien");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DangKyNguyenVong");
        }
    }
}
