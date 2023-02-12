using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagement.Migrations
{
    public partial class userfoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FOTO",
                table: "UserFoto");

            migrationBuilder.DropColumn(
                name: "WA",
                table: "User");

            migrationBuilder.AddColumn<byte[]>(
                name: "DataFiles",
                table: "UserFoto",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "UserFoto",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserFoto",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFiles",
                table: "UserFoto");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "UserFoto");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserFoto");

            migrationBuilder.AddColumn<string>(
                name: "FOTO",
                table: "UserFoto",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WA",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
