using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagement.Migrations
{
    public partial class changeUserActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActivity_Menu_Menu_ID",
                table: "UserActivity");

            migrationBuilder.DropIndex(
                name: "IX_UserActivity_Menu_ID",
                table: "UserActivity");

            migrationBuilder.DropColumn(
                name: "Menu_ID",
                table: "UserActivity");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "UserActivity");

            migrationBuilder.AddColumn<string>(
                name: "MENU_CONTROLLER",
                table: "UserActivity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MENU_VIEW",
                table: "UserActivity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Params",
                table: "UserActivity",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MENU_CONTROLLER",
                table: "UserActivity");

            migrationBuilder.DropColumn(
                name: "MENU_VIEW",
                table: "UserActivity");

            migrationBuilder.DropColumn(
                name: "Params",
                table: "UserActivity");

            migrationBuilder.AddColumn<int>(
                name: "Menu_ID",
                table: "UserActivity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "UserActivity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserActivity_Menu_ID",
                table: "UserActivity",
                column: "Menu_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivity_Menu_Menu_ID",
                table: "UserActivity",
                column: "Menu_ID",
                principalTable: "Menu",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
