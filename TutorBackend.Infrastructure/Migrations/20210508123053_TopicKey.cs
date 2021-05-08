using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TutorBackend.Infrastructure.Migrations
{
    public partial class TopicKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TutorTopics_Topics_TopicsId",
                table: "TutorTopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TutorTopics",
                table: "TutorTopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topics",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "TopicsId",
                table: "TutorTopics");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "TopicReferenceId",
                table: "Topics");

            migrationBuilder.AddColumn<string>(
                name: "TopicsName",
                table: "TutorTopics",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Topics",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TutorTopics",
                table: "TutorTopics",
                columns: new[] { "TopicsName", "TutorsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topics",
                table: "Topics",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_TutorTopics_Topics_TopicsName",
                table: "TutorTopics",
                column: "TopicsName",
                principalTable: "Topics",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TutorTopics_Topics_TopicsName",
                table: "TutorTopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TutorTopics",
                table: "TutorTopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topics",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "TopicsName",
                table: "TutorTopics");

            migrationBuilder.AddColumn<Guid>(
                name: "TopicsId",
                table: "TutorTopics",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Topics",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Topics",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TopicReferenceId",
                table: "Topics",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TutorTopics",
                table: "TutorTopics",
                columns: new[] { "TopicsId", "TutorsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topics",
                table: "Topics",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TutorTopics_Topics_TopicsId",
                table: "TutorTopics",
                column: "TopicsId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
