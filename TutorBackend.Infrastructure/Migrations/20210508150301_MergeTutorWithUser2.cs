using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TutorBackend.Infrastructure.Migrations
{
    public partial class MergeTutorWithUser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_TutorProfileId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TutorProfileId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TutorProfileId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TutorProfileId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TutorProfileId",
                table: "Users",
                column: "TutorProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_TutorProfileId",
                table: "Users",
                column: "TutorProfileId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
