using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_qlsv.Migrations.ViewDb
{
    /// <inheritdoc />
    public partial class themThuocTinhIconClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NavItems",
                table: "NavItems");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "NavItems");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "NavItems");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "NavItems");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "NavItems");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "NavItems");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "NavItems");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "NavItems");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "NavItems");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "NavItems");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "NavItems");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "NavItems");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "NavItems");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "NavItems");

            migrationBuilder.RenameTable(
                name: "NavItems",
                newName: "NavBarPages");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "NavBarPages",
                newName: "IconClass");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NavBarPages",
                table: "NavBarPages",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NavBarPages",
                table: "NavBarPages");

            migrationBuilder.RenameTable(
                name: "NavBarPages",
                newName: "NavItems");

            migrationBuilder.RenameColumn(
                name: "IconClass",
                table: "NavItems",
                newName: "UserName");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "NavItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "NavItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "NavItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "NavItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "NavItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "NavItems",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "NavItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "NavItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "NavItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "NavItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "NavItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "NavItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "NavItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NavItems",
                table: "NavItems",
                column: "Id");
        }
    }
}
