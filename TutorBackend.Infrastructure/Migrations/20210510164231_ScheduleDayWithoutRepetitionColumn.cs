using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TutorBackend.Infrastructure.Migrations
{
    public partial class ScheduleDayWithoutRepetitionColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndOfRepetition",
                table: "ScheduleDays");

            migrationBuilder.DropColumn(
                name: "IsRepeatable",
                table: "ScheduleDays");

            migrationBuilder.DropColumn(
                name: "RepeatAfterWeeks",
                table: "ScheduleDays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndOfRepetition",
                table: "ScheduleDays",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRepeatable",
                table: "ScheduleDays",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RepeatAfterWeeks",
                table: "ScheduleDays",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
