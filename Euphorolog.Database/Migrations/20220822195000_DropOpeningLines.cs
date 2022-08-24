using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Euphorolog.Database.Migrations
{
    public partial class DropOpeningLines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "openingLines",
                table: "stories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "openingLines",
                table: "stories",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
