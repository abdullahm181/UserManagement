using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagement.Migrations
{
    public partial class updateIAppErrorModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Menu_ID",
                table: "IErrorApplication");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Menu_ID",
                table: "IErrorApplication",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
