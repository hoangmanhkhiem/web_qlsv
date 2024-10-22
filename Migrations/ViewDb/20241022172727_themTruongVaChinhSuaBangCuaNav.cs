using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_qlsv.Migrations.ViewDb
{
    /// <inheritdoc />
    public partial class themTruongVaChinhSuaBangCuaNav : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NavItems",
                table: "NavItems");

            migrationBuilder.RenameTable(
                name: "NavItems",
                newName: "ViewNavItem");

            migrationBuilder.AddColumn<string>(
                name: "IconClass",
                table: "ViewNavItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ViewNavItem",
                table: "ViewNavItem",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ViewNavItem",
                table: "ViewNavItem");

            migrationBuilder.DropColumn(
                name: "IconClass",
                table: "ViewNavItem");

            migrationBuilder.RenameTable(
                name: "ViewNavItem",
                newName: "NavItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NavItems",
                table: "NavItems",
                column: "Id");
        }
    }
}
