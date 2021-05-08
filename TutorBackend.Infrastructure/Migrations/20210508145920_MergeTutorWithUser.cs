using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TutorBackend.Infrastructure.Migrations
{
    public partial class MergeTutorWithUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleDays_Tutors_TutorId",
                table: "ScheduleDays");

            migrationBuilder.DropForeignKey(
                name: "FK_TutorTopics_Tutors_TutorsId",
                table: "TutorTopics");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Tutors_TutorProfileId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Tutors");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HasLocalLessons",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasRemoteLessons",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleDays_Users_TutorId",
                table: "ScheduleDays",
                column: "TutorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TutorTopics_Users_TutorsId",
                table: "TutorTopics",
                column: "TutorsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_TutorProfileId",
                table: "Users",
                column: "TutorProfileId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleDays_Users_TutorId",
                table: "ScheduleDays");

            migrationBuilder.DropForeignKey(
                name: "FK_TutorTopics_Users_TutorsId",
                table: "TutorTopics");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_TutorProfileId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HasLocalLessons",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HasRemoteLessons",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Tutors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasLocalLessons = table.Column<bool>(type: "bit", nullable: false),
                    HasRemoteLessons = table.Column<bool>(type: "bit", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tutors_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleDays_Tutors_TutorId",
                table: "ScheduleDays",
                column: "TutorId",
                principalTable: "Tutors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TutorTopics_Tutors_TutorsId",
                table: "TutorTopics",
                column: "TutorsId",
                principalTable: "Tutors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Tutors_TutorProfileId",
                table: "Users",
                column: "TutorProfileId",
                principalTable: "Tutors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
