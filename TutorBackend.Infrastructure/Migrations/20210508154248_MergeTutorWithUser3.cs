using Microsoft.EntityFrameworkCore.Migrations;

namespace TutorBackend.Infrastructure.Migrations
{
    public partial class MergeTutorWithUser3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Users",
                newName: "UserType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserType",
                table: "Users",
                newName: "Discriminator");
        }
    }
}
