using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Euphorolog.Database.Migrations
{
    public partial class migration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "passChanged",
                table: "users",
                newName: "passChangedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "passChangedAt",
                table: "users",
                newName: "passChanged");
        }
    }
}
