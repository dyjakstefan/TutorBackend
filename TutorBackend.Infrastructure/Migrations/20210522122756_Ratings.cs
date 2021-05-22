using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TutorBackend.Infrastructure.Migrations
{
    public partial class Ratings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Ratings");

            migrationBuilder.AddColumn<Guid>(
                name: "IssuerId",
                table: "Ratings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssuerId",
                table: "Ratings");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Ratings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
