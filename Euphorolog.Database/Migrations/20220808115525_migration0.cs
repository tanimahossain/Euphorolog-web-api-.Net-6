using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Euphorolog.Database.Migrations
{
    public partial class migration0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userName = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    fullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    eMail = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    passwordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    passwordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    passChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userName);
                });

            migrationBuilder.CreateTable(
                name: "stories",
                columns: table => new
                {
                    storyId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    storyNo = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    storyTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    authorName = table.Column<string>(type: "nvarchar(55)", nullable: false),
                    openingLines = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    storyDescription = table.Column<string>(type: "nvarchar(max)", maxLength: 10005, nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stories", x => x.storyId);
                    table.ForeignKey(
                        name: "FK_stories_users_authorName",
                        column: x => x.authorName,
                        principalTable: "users",
                        principalColumn: "userName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_stories_authorName",
                table: "stories",
                column: "authorName");

            migrationBuilder.CreateIndex(
                name: "IX_users_userName",
                table: "users",
                column: "userName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stories");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
