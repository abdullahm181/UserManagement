using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagement.Migrations
{
    public partial class updateMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MENU_LINK",
                table: "Menu");

            migrationBuilder.AddColumn<string>(
                name: "MENU_CONTROLLER",
                table: "Menu",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MENU_VIEW",
                table: "Menu",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MENU_CONTROLLER",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "MENU_VIEW",
                table: "Menu");

            migrationBuilder.AddColumn<string>(
                name: "MENU_LINK",
                table: "Menu",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
