using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_qlsv.Migrations.QuanLySinhVienDb
{
    /// <inheritdoc />
    public partial class updateAddIdAllTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Diem__IdLopHocPh__5EBF139D",
                table: "Diem");

            migrationBuilder.DropForeignKey(
                name: "FK__Diem__IdSinhVien__5FB337D6",
                table: "Diem");

            migrationBuilder.DropForeignKey(
                name: "FK__DiemDanh__IdLopH__5812160E",
                table: "DiemDanh");

            migrationBuilder.DropForeignKey(
                name: "FK__DiemDanh__IdSinh__5629CD9C",
                table: "DiemDanh");

            migrationBuilder.DropForeignKey(
                name: "FK__DiemDanh__IdThoi__571DF1D5",
                table: "DiemDanh");

            migrationBuilder.DropForeignKey(
                name: "FK__LopHocPha__IdGia__4D94879B",
                table: "LopHocPhan");

            migrationBuilder.DropForeignKey(
                name: "FK__SinhVien___IdLop__6383C8BA",
                table: "SinhVien_LopHocPhan");

            migrationBuilder.DropForeignKey(
                name: "FK__SinhVien___IdSin__628FA481",
                table: "SinhVien_LopHocPhan");

            migrationBuilder.DropForeignKey(
                name: "FK__ThoiGian___IdLop__5AEE82B9",
                table: "ThoiGian_LopHocPhan");

            migrationBuilder.DropForeignKey(
                name: "FK__ThoiGian___IdTho__5BE2A6F2",
                table: "ThoiGian_LopHocPhan");

            migrationBuilder.DropPrimaryKey(
                name: "PK__ThoiGian__15C11AD9B63A9B11",
                table: "ThoiGian_LopHocPhan");

            migrationBuilder.DropPrimaryKey(
                name: "PK__ThoiGian__EDEFED40D5785B54",
                table: "ThoiGian");

            migrationBuilder.DropPrimaryKey(
                name: "PK__SinhVien__531A1B36CDEDAB4C",
                table: "SinhVien_LopHocPhan");

            migrationBuilder.DropPrimaryKey(
                name: "PK__SinhVien__93ABE5766A04A8ED",
                table: "SinhVien");

            migrationBuilder.DropPrimaryKey(
                name: "PK__LopHocPh__6A35A4EC70344C38",
                table: "LopHocPhan");

            migrationBuilder.DropPrimaryKey(
                name: "PK__GiaoVien__F4372AE328C6DBED",
                table: "GiaoVien");

            migrationBuilder.DropPrimaryKey(
                name: "PK__DiemDanh__817E0446A874C894",
                table: "DiemDanh");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Diem__72255A5ACAFEA388",
                table: "Diem");

            migrationBuilder.AlterColumn<string>(
                name: "IdThoiGian",
                table: "ThoiGian_LopHocPhan",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "IdLopHocPhan",
                table: "ThoiGian_LopHocPhan",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);

            migrationBuilder.AddColumn<string>(
                name: "IdThoiGian_LopHocPhan",
                table: "ThoiGian_LopHocPhan",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<string>(
                name: "IdLopHocPhan",
                table: "SinhVien_LopHocPhan",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "IdSinhVien",
                table: "SinhVien_LopHocPhan",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);

            migrationBuilder.AddColumn<string>(
                name: "IdSinhVien_LopHocPhan",
                table: "SinhVien_LopHocPhan",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<string>(
                name: "IdLopHocPhan",
                table: "DiemDanh",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "IdThoiGian",
                table: "DiemDanh",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "IdSinhVien",
                table: "DiemDanh",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);

            migrationBuilder.AddColumn<string>(
                name: "IdDiemDanh",
                table: "DiemDanh",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValueSql: "(newid())");

            migrationBuilder.AlterColumn<string>(
                name: "IdSinhVien",
                table: "Diem",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "IdLopHocPhan",
                table: "Diem",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);

            migrationBuilder.AddColumn<string>(
                name: "IdDiem",
                table: "Diem",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValueSql: "(newid())");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThoiGian_LopHocPhan",
                table: "ThoiGian_LopHocPhan",
                column: "IdThoiGian_LopHocPhan");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThoiGian",
                table: "ThoiGian",
                column: "IdThoiGian");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SinhVien_LopHocPhan",
                table: "SinhVien_LopHocPhan",
                column: "IdSinhVien_LopHocPhan");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SinhVien",
                table: "SinhVien",
                column: "IdSinhVien");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LopHocPhan",
                table: "LopHocPhan",
                column: "IdHocPhan");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GiaoVien",
                table: "GiaoVien",
                column: "IdGiaoVien");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiemDanh",
                table: "DiemDanh",
                column: "IdDiemDanh");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diem",
                table: "Diem",
                column: "IdDiem");

            migrationBuilder.CreateIndex(
                name: "IX_ThoiGian_LopHocPhan_IdLopHocPhan",
                table: "ThoiGian_LopHocPhan",
                column: "IdLopHocPhan");

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_LopHocPhan_IdSinhVien",
                table: "SinhVien_LopHocPhan",
                column: "IdSinhVien");

            migrationBuilder.CreateIndex(
                name: "IX_DiemDanh_IdSinhVien",
                table: "DiemDanh",
                column: "IdSinhVien");

            migrationBuilder.CreateIndex(
                name: "IX_Diem_IdLopHocPhan",
                table: "Diem",
                column: "IdLopHocPhan");

            migrationBuilder.AddForeignKey(
                name: "FK_Diem_LopHocPhan_IdLopHocPhan",
                table: "Diem",
                column: "IdLopHocPhan",
                principalTable: "LopHocPhan",
                principalColumn: "IdHocPhan");

            migrationBuilder.AddForeignKey(
                name: "FK_Diem_SinhVien_IdSinhVien",
                table: "Diem",
                column: "IdSinhVien",
                principalTable: "SinhVien",
                principalColumn: "IdSinhVien");

            migrationBuilder.AddForeignKey(
                name: "FK_DiemDanh_LopHocPhan_IdLopHocPhan",
                table: "DiemDanh",
                column: "IdLopHocPhan",
                principalTable: "LopHocPhan",
                principalColumn: "IdHocPhan");

            migrationBuilder.AddForeignKey(
                name: "FK_DiemDanh_SinhVien_IdSinhVien",
                table: "DiemDanh",
                column: "IdSinhVien",
                principalTable: "SinhVien",
                principalColumn: "IdSinhVien");

            migrationBuilder.AddForeignKey(
                name: "FK_DiemDanh_ThoiGian_IdThoiGian",
                table: "DiemDanh",
                column: "IdThoiGian",
                principalTable: "ThoiGian",
                principalColumn: "IdThoiGian");

            migrationBuilder.AddForeignKey(
                name: "FK_LopHocPhan_GiaoVien_IdGiaoVien",
                table: "LopHocPhan",
                column: "IdGiaoVien",
                principalTable: "GiaoVien",
                principalColumn: "IdGiaoVien");

            migrationBuilder.AddForeignKey(
                name: "FK_SinhVien_LopHocPhan_LopHocPhan_IdLopHocPhan",
                table: "SinhVien_LopHocPhan",
                column: "IdLopHocPhan",
                principalTable: "LopHocPhan",
                principalColumn: "IdHocPhan");

            migrationBuilder.AddForeignKey(
                name: "FK_SinhVien_LopHocPhan_SinhVien_IdSinhVien",
                table: "SinhVien_LopHocPhan",
                column: "IdSinhVien",
                principalTable: "SinhVien",
                principalColumn: "IdSinhVien");

            migrationBuilder.AddForeignKey(
                name: "FK_ThoiGian_LopHocPhan_LopHocPhan_IdLopHocPhan",
                table: "ThoiGian_LopHocPhan",
                column: "IdLopHocPhan",
                principalTable: "LopHocPhan",
                principalColumn: "IdHocPhan");

            migrationBuilder.AddForeignKey(
                name: "FK_ThoiGian_LopHocPhan_ThoiGian_IdThoiGian",
                table: "ThoiGian_LopHocPhan",
                column: "IdThoiGian",
                principalTable: "ThoiGian",
                principalColumn: "IdThoiGian");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diem_LopHocPhan_IdLopHocPhan",
                table: "Diem");

            migrationBuilder.DropForeignKey(
                name: "FK_Diem_SinhVien_IdSinhVien",
                table: "Diem");

            migrationBuilder.DropForeignKey(
                name: "FK_DiemDanh_LopHocPhan_IdLopHocPhan",
                table: "DiemDanh");

            migrationBuilder.DropForeignKey(
                name: "FK_DiemDanh_SinhVien_IdSinhVien",
                table: "DiemDanh");

            migrationBuilder.DropForeignKey(
                name: "FK_DiemDanh_ThoiGian_IdThoiGian",
                table: "DiemDanh");

            migrationBuilder.DropForeignKey(
                name: "FK_LopHocPhan_GiaoVien_IdGiaoVien",
                table: "LopHocPhan");

            migrationBuilder.DropForeignKey(
                name: "FK_SinhVien_LopHocPhan_LopHocPhan_IdLopHocPhan",
                table: "SinhVien_LopHocPhan");

            migrationBuilder.DropForeignKey(
                name: "FK_SinhVien_LopHocPhan_SinhVien_IdSinhVien",
                table: "SinhVien_LopHocPhan");

            migrationBuilder.DropForeignKey(
                name: "FK_ThoiGian_LopHocPhan_LopHocPhan_IdLopHocPhan",
                table: "ThoiGian_LopHocPhan");

            migrationBuilder.DropForeignKey(
                name: "FK_ThoiGian_LopHocPhan_ThoiGian_IdThoiGian",
                table: "ThoiGian_LopHocPhan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThoiGian_LopHocPhan",
                table: "ThoiGian_LopHocPhan");

            migrationBuilder.DropIndex(
                name: "IX_ThoiGian_LopHocPhan_IdLopHocPhan",
                table: "ThoiGian_LopHocPhan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThoiGian",
                table: "ThoiGian");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SinhVien_LopHocPhan",
                table: "SinhVien_LopHocPhan");

            migrationBuilder.DropIndex(
                name: "IX_SinhVien_LopHocPhan_IdSinhVien",
                table: "SinhVien_LopHocPhan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SinhVien",
                table: "SinhVien");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LopHocPhan",
                table: "LopHocPhan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GiaoVien",
                table: "GiaoVien");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiemDanh",
                table: "DiemDanh");

            migrationBuilder.DropIndex(
                name: "IX_DiemDanh_IdSinhVien",
                table: "DiemDanh");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diem",
                table: "Diem");

            migrationBuilder.DropIndex(
                name: "IX_Diem_IdLopHocPhan",
                table: "Diem");

            migrationBuilder.DropColumn(
                name: "IdThoiGian_LopHocPhan",
                table: "ThoiGian_LopHocPhan");

            migrationBuilder.DropColumn(
                name: "IdSinhVien_LopHocPhan",
                table: "SinhVien_LopHocPhan");

            migrationBuilder.DropColumn(
                name: "IdDiemDanh",
                table: "DiemDanh");

            migrationBuilder.DropColumn(
                name: "IdDiem",
                table: "Diem");

            migrationBuilder.AlterColumn<string>(
                name: "IdThoiGian",
                table: "ThoiGian_LopHocPhan",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdLopHocPhan",
                table: "ThoiGian_LopHocPhan",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdSinhVien",
                table: "SinhVien_LopHocPhan",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdLopHocPhan",
                table: "SinhVien_LopHocPhan",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdThoiGian",
                table: "DiemDanh",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdSinhVien",
                table: "DiemDanh",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdLopHocPhan",
                table: "DiemDanh",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdSinhVien",
                table: "Diem",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdLopHocPhan",
                table: "Diem",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__ThoiGian__15C11AD9B63A9B11",
                table: "ThoiGian_LopHocPhan",
                columns: new[] { "IdLopHocPhan", "IdThoiGian" });

            migrationBuilder.AddPrimaryKey(
                name: "PK__ThoiGian__EDEFED40D5785B54",
                table: "ThoiGian",
                column: "IdThoiGian");

            migrationBuilder.AddPrimaryKey(
                name: "PK__SinhVien__531A1B36CDEDAB4C",
                table: "SinhVien_LopHocPhan",
                columns: new[] { "IdSinhVien", "IdLopHocPhan" });

            migrationBuilder.AddPrimaryKey(
                name: "PK__SinhVien__93ABE5766A04A8ED",
                table: "SinhVien",
                column: "IdSinhVien");

            migrationBuilder.AddPrimaryKey(
                name: "PK__LopHocPh__6A35A4EC70344C38",
                table: "LopHocPhan",
                column: "IdHocPhan");

            migrationBuilder.AddPrimaryKey(
                name: "PK__GiaoVien__F4372AE328C6DBED",
                table: "GiaoVien",
                column: "IdGiaoVien");

            migrationBuilder.AddPrimaryKey(
                name: "PK__DiemDanh__817E0446A874C894",
                table: "DiemDanh",
                columns: new[] { "IdSinhVien", "IdThoiGian", "IdLopHocPhan" });

            migrationBuilder.AddPrimaryKey(
                name: "PK__Diem__72255A5ACAFEA388",
                table: "Diem",
                columns: new[] { "IdLopHocPhan", "IdSinhVien" });

            migrationBuilder.AddForeignKey(
                name: "FK__Diem__IdLopHocPh__5EBF139D",
                table: "Diem",
                column: "IdLopHocPhan",
                principalTable: "LopHocPhan",
                principalColumn: "IdHocPhan");

            migrationBuilder.AddForeignKey(
                name: "FK__Diem__IdSinhVien__5FB337D6",
                table: "Diem",
                column: "IdSinhVien",
                principalTable: "SinhVien",
                principalColumn: "IdSinhVien");

            migrationBuilder.AddForeignKey(
                name: "FK__DiemDanh__IdLopH__5812160E",
                table: "DiemDanh",
                column: "IdLopHocPhan",
                principalTable: "LopHocPhan",
                principalColumn: "IdHocPhan");

            migrationBuilder.AddForeignKey(
                name: "FK__DiemDanh__IdSinh__5629CD9C",
                table: "DiemDanh",
                column: "IdSinhVien",
                principalTable: "SinhVien",
                principalColumn: "IdSinhVien");

            migrationBuilder.AddForeignKey(
                name: "FK__DiemDanh__IdThoi__571DF1D5",
                table: "DiemDanh",
                column: "IdThoiGian",
                principalTable: "ThoiGian",
                principalColumn: "IdThoiGian");

            migrationBuilder.AddForeignKey(
                name: "FK__LopHocPha__IdGia__4D94879B",
                table: "LopHocPhan",
                column: "IdGiaoVien",
                principalTable: "GiaoVien",
                principalColumn: "IdGiaoVien");

            migrationBuilder.AddForeignKey(
                name: "FK__SinhVien___IdLop__6383C8BA",
                table: "SinhVien_LopHocPhan",
                column: "IdLopHocPhan",
                principalTable: "LopHocPhan",
                principalColumn: "IdHocPhan");

            migrationBuilder.AddForeignKey(
                name: "FK__SinhVien___IdSin__628FA481",
                table: "SinhVien_LopHocPhan",
                column: "IdSinhVien",
                principalTable: "SinhVien",
                principalColumn: "IdSinhVien");

            migrationBuilder.AddForeignKey(
                name: "FK__ThoiGian___IdLop__5AEE82B9",
                table: "ThoiGian_LopHocPhan",
                column: "IdLopHocPhan",
                principalTable: "LopHocPhan",
                principalColumn: "IdHocPhan");

            migrationBuilder.AddForeignKey(
                name: "FK__ThoiGian___IdTho__5BE2A6F2",
                table: "ThoiGian_LopHocPhan",
                column: "IdThoiGian",
                principalTable: "ThoiGian",
                principalColumn: "IdThoiGian");
        }
    }
}
