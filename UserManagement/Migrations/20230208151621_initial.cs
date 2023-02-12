using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagement.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MENU_NAME = table.Column<string>(nullable: true),
                    MENU_LINK = table.Column<string>(nullable: true),
                    MENU_ICON = table.Column<string>(nullable: true),
                    GROUP = table.Column<string>(nullable: true),
                    DELETE_MARK = table.Column<bool>(nullable: false),
                    CREATE_BY = table.Column<string>(nullable: true),
                    CREATE_DATE = table.Column<DateTime>(nullable: false),
                    UPDATE_BY = table.Column<string>(nullable: true),
                    UPDATE_DATE = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    DELETE_MARK = table.Column<bool>(nullable: false),
                    CREATE_BY = table.Column<string>(nullable: true),
                    CREATE_DATE = table.Column<DateTime>(nullable: false),
                    UPDATE_BY = table.Column<string>(nullable: true),
                    UPDATE_DATE = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAMA_USER = table.Column<string>(nullable: true),
                    USERNAME = table.Column<string>(nullable: true),
                    PASSWORD = table.Column<string>(nullable: true),
                    EMAIL = table.Column<string>(nullable: true),
                    NO_HP = table.Column<string>(nullable: true),
                    WA = table.Column<string>(nullable: true),
                    PIN = table.Column<string>(nullable: true),
                    STATUS_USER = table.Column<string>(nullable: true),
                    DELETE_MARK = table.Column<bool>(nullable: false),
                    CREATE_BY = table.Column<string>(nullable: true),
                    CREATE_DATE = table.Column<DateTime>(nullable: false),
                    UPDATE_BY = table.Column<string>(nullable: true),
                    UPDATE_DATE = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MenuWithRole",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Menu_Id = table.Column<int>(nullable: false),
                    Role_Id = table.Column<int>(nullable: false),
                    DELETE_MARK = table.Column<bool>(nullable: false),
                    CREATE_BY = table.Column<string>(nullable: true),
                    CREATE_DATE = table.Column<DateTime>(nullable: false),
                    UPDATE_BY = table.Column<string>(nullable: true),
                    UPDATE_DATE = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuWithRole", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MenuWithRole_Menu_Menu_Id",
                        column: x => x.Menu_Id,
                        principalTable: "Menu",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuWithRole_Role_Role_Id",
                        column: x => x.Role_Id,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IErrorApplication",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<int>(nullable: false),
                    ERROT_DATE = table.Column<DateTime>(nullable: false),
                    Menu_ID = table.Column<int>(nullable: false),
                    CONTROLLER = table.Column<string>(nullable: true),
                    FUNCTION = table.Column<string>(nullable: true),
                    ERROR_LINE = table.Column<string>(nullable: true),
                    ERROR_MESSAGE = table.Column<string>(nullable: true),
                    STATUS = table.Column<string>(nullable: true),
                    PARAM = table.Column<string>(nullable: true),
                    DELETE_MARK = table.Column<bool>(nullable: false),
                    CREATE_BY = table.Column<string>(nullable: true),
                    CREATE_DATE = table.Column<DateTime>(nullable: false),
                    UPDATE_BY = table.Column<string>(nullable: true),
                    UPDATE_DATE = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IErrorApplication", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IErrorApplication_User_User_ID",
                        column: x => x.User_ID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserActivity",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Menu_ID = table.Column<int>(nullable: false),
                    DELETE_MARK = table.Column<bool>(nullable: false),
                    CREATE_BY = table.Column<string>(nullable: true),
                    CREATE_DATE = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivity", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserActivity_Menu_Menu_ID",
                        column: x => x.Menu_ID,
                        principalTable: "Menu",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserActivity_User_User_ID",
                        column: x => x.User_ID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFoto",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_USER = table.Column<int>(nullable: false),
                    FOTO = table.Column<string>(nullable: true),
                    DELETE_MARK = table.Column<bool>(nullable: false),
                    CREATE_BY = table.Column<string>(nullable: true),
                    CREATE_DATE = table.Column<DateTime>(nullable: false),
                    UPDATE_BY = table.Column<string>(nullable: true),
                    UPDATE_DATE = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFoto", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserFoto_User_ID_USER",
                        column: x => x.ID_USER,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<int>(nullable: false),
                    Role_Id = table.Column<int>(nullable: false),
                    DELETE_MARK = table.Column<bool>(nullable: false),
                    CREATE_BY = table.Column<string>(nullable: true),
                    CREATE_DATE = table.Column<DateTime>(nullable: false),
                    UPDATE_BY = table.Column<string>(nullable: true),
                    UPDATE_DATE = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_Role_Id",
                        column: x => x.Role_Id,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_User_Id",
                        column: x => x.User_Id,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IErrorApplication_User_ID",
                table: "IErrorApplication",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MenuWithRole_Menu_Id",
                table: "MenuWithRole",
                column: "Menu_Id");

            migrationBuilder.CreateIndex(
                name: "IX_MenuWithRole_Role_Id",
                table: "MenuWithRole",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivity_Menu_ID",
                table: "UserActivity",
                column: "Menu_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UserActivity_User_ID",
                table: "UserActivity",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UserFoto_ID_USER",
                table: "UserFoto",
                column: "ID_USER");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_Role_Id",
                table: "UserRole",
                column: "Role_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_User_Id",
                table: "UserRole",
                column: "User_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IErrorApplication");

            migrationBuilder.DropTable(
                name: "MenuWithRole");

            migrationBuilder.DropTable(
                name: "UserActivity");

            migrationBuilder.DropTable(
                name: "UserFoto");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
