using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Euphorolog.Database.Migrations
{
    public partial class migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    fullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    eMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    passChanged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    passChangedflag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userName);
                });

            migrationBuilder.CreateTable(
                name: "stories",
                columns: table => new
                {
                    storyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    storyNo = table.Column<int>(type: "int", nullable: false),
                    storyTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    authorName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    openingLines = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    storyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
