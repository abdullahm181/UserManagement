using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagement.Migrations
{
    public partial class SPCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UPDATE_BY",
                table: "UserActivity",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UPDATE_DATE",
                table: "UserActivity",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Role",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UPDATE_BY",
                table: "UserActivity");

            migrationBuilder.DropColumn(
                name: "UPDATE_DATE",
                table: "UserActivity");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Role");
        }
    }
}
