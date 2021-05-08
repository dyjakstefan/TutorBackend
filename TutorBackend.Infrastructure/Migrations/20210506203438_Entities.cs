using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TutorBackend.Infrastructure.Migrations
{
    public partial class Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "Username");

            migrationBuilder.AddColumn<Guid>(
                name: "TutorProfileId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tutors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasRemoteLessons = table.Column<bool>(type: "bit", nullable: false),
                    HasLocalLessons = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "ScheduleDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsRepeatable = table.Column<bool>(type: "bit", nullable: false),
                    RepeatAfterWeeks = table.Column<int>(type: "int", nullable: false),
                    EndOfRepetition = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TutorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleDays_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TutorTopics",
                columns: table => new
                {
                    TopicsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TutorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorTopics", x => new { x.TopicsId, x.TutorsId });
                    table.ForeignKey(
                        name: "FK_TutorTopics_Topics_TopicsId",
                        column: x => x.TopicsId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorTopics_Tutors_TutorsId",
                        column: x => x.TutorsId,
                        principalTable: "Tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsReserved = table.Column<bool>(type: "bit", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    IsRepeatable = table.Column<bool>(type: "bit", nullable: false),
                    RepeatAfterWeeks = table.Column<int>(type: "int", nullable: false),
                    RepeatAfterDays = table.Column<int>(type: "int", nullable: false),
                    EndOfRepetition = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduleDayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_ScheduleDays_ScheduleDayId",
                        column: x => x.ScheduleDayId,
                        principalTable: "ScheduleDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_TutorProfileId",
                table: "Users",
                column: "TutorProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_ScheduleDayId",
                table: "Lessons",
                column: "ScheduleDayId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDays_TutorId",
                table: "ScheduleDays",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorTopics_TutorsId",
                table: "TutorTopics",
                column: "TutorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Tutors_TutorProfileId",
                table: "Users",
                column: "TutorProfileId",
                principalTable: "Tutors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Tutors_TutorProfileId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "TutorTopics");

            migrationBuilder.DropTable(
                name: "ScheduleDays");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "Tutors");

            migrationBuilder.DropIndex(
                name: "IX_Users_TutorProfileId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TutorProfileId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "UserName");

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
